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
        private readonly PatientDbContext _patientContext;

        public DeviceMonitoringReportService(
            ApplicationDbContext context,
            PatientDbContext patientContext)
        {
            _context = context;
            _patientContext = patientContext;
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
            // Use raw SQL to query the tbpatient table in the proper context
            using (var command = _patientContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"
                    SELECT COUNT(DISTINCT p.HospNum) as DeviceCount
                    FROM tbpatient p
                    JOIN tbmaster m ON p.HospNum = m.HospNum
                    JOIN " + _context.Database.GetDbConnection().Database + @".dbo.DeviceConnected d ON p.HospNum = d.HospNum
                    JOIN Build_File..tbCoRoom r ON p.RoomID = r.RoomID 
                    JOIN Build_File..tbCoStation s ON r.StationId = s.StationId
                    WHERE s.Station = @Area
                        AND d.DeviceType = @DeviceType
                        AND (@DeviceClass IS NULL OR d.DeviceClass = @DeviceClass)
                        AND d.DeviceInsert <= @Date
                        AND (d.DeviceRemove IS NULL OR d.DeviceRemove >= @Date)";

                // Add parameters
                var areaParam = command.CreateParameter();
                areaParam.ParameterName = "@Area";
                areaParam.Value = area;
                command.Parameters.Add(areaParam);

                var deviceTypeParam = command.CreateParameter();
                deviceTypeParam.ParameterName = "@DeviceType";
                deviceTypeParam.Value = deviceType;
                command.Parameters.Add(deviceTypeParam);

                var deviceClassParam = command.CreateParameter();
                deviceClassParam.ParameterName = "@DeviceClass";
                deviceClassParam.Value = deviceClass == null ? DBNull.Value : (object)deviceClass;
                command.Parameters.Add(deviceClassParam);

                var dateParam = command.CreateParameter();
                dateParam.ParameterName = "@Date";
                dateParam.Value = date;
                command.Parameters.Add(dateParam);

                // Ensure connection is open
                if (command.Connection.State != System.Data.ConnectionState.Open)
                {
                    await command.Connection.OpenAsync();
                }

                // Execute query
                var result = await command.ExecuteScalarAsync();
                return result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
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