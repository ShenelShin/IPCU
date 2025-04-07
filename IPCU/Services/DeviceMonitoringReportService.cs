using IPCU.Controllers;
using IPCU.Data;
using IPCU.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPCU.Services
{
    public class DeviceMonitoringReportService
    {
        private readonly ApplicationDbContext _context;

        public DeviceMonitoringReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DeviceMonitoringReportData> GenerateReportData(string area, int year, int month)
        {
            // Create date range for the report (full month)
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            // Get the days in the month
            var daysInMonth = DateTime.DaysInMonth(year, month);
            var dailyData = new List<DailyDeviceData>();

            // For each day of the month
            for (int day = 1; day <= daysInMonth; day++)
            {
                var currentDate = new DateTime(year, month, day);

                // Get device counts for this day
                var iucNonKTCount = await GetDeviceCountForDay(area, currentDate, "IUC", "Non-KT");
                var iucKTCount = await GetDeviceCountForDay(area, currentDate, "IUC", "KT");
                var clNonHDCount = await GetDeviceCountForDay(area, currentDate, "CL", "Non-HD");
                var clHDCount = await GetDeviceCountForDay(area, currentDate, "CL", "HD");
                var mvCount = await GetDeviceCountForDay(area, currentDate, "MV", null);

                // Get patient movement data for this day
                var movementData = await GetPatientMovementForDay(area, currentDate);

                // Create daily data object
                var dailyEntry = new DailyDeviceData
                {
                    Date = currentDate,
                    IUCNonKTCount = iucNonKTCount,
                    IUCKTCount = iucKTCount,
                    CLNonHDCount = clNonHDCount,
                    CLHDCount = clHDCount,
                    MVCount = mvCount,

                    // Patient movement data
                    AdmissionCount = movementData?.AdmissionCount ?? 0,
                    TransferInCount = movementData?.TransferInCount ?? 0,
                    SentHomeCount = movementData?.SentHomeCount ?? 0,
                    MortalityCount = movementData?.MortalityCount ?? 0,
                    TransferOutCount = movementData?.TransferOutCount ?? 0
                };

                dailyData.Add(dailyEntry);
            }

            // Create the report data
            var reportData = new DeviceMonitoringReportData
            {
                Area = area,
                Month = month,
                Year = year,
                DailyData = dailyData
            };

            return reportData;
        }

        private async Task<int> GetDeviceCountForDay(string area, DateTime date, string deviceType, string deviceClass)
        {
            // Find all patients in this area with this device type/class active on this date
            var patientsWithDevice = await (from p in _context.Patients
                                            join d in _context.DeviceConnected on p.HospNum equals d.HospNum
                                            where p.AdmLocation == area
                                                && d.DeviceType == deviceType
                                                && (deviceClass == null || d.DeviceClass == deviceClass)
                                                && d.DeviceInsert <= date
                                                && (d.DeviceRemove == null || d.DeviceRemove >= date)
                                            select p.HospNum).Distinct().CountAsync();

            return patientsWithDevice;
        }

        private async Task<PatientMovement> GetPatientMovementForDay(string area, DateTime date)
        {
            // Get the patient movement record for this area and date
            return await _context.PatientMovements
                .Where(m => m.Area == area && m.MovementDate.Date == date.Date)
                .FirstOrDefaultAsync();
        }
    }
}