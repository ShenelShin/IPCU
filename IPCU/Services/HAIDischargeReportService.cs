using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IPCU.Data;
using IPCU.Models;

namespace IPCU.Services
{
    public class HAIDischargeReportService
    {
        private readonly ApplicationDbContext _context;

        public HAIDischargeReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, List<HAIDischargeData>>> GetHAIDischargeDataForYearAsync(int year)
        {
            // Dictionary to hold all areas with their monthly data
            var result = new Dictionary<string, List<HAIDischargeData>>();

            // Get all distinct areas from patient movements
            var areas = await _context.PatientMovements
                .Where(m => m.MovementDate.Year == year)
                .Select(m => m.Area)
                .Distinct()
                .OrderBy(a => a)
                .ToListAsync();

            foreach (var area in areas)
            {
                var areaMonthlyData = new List<HAIDischargeData>();
                for (int month = 1; month <= 12; month++)
                {
                    // Get date range for this month
                    var startDate = new DateTime(year, month, 1);
                    var endDate = startDate.AddMonths(1).AddDays(-1);

                    // Get discharge counts for this area and month
                    var movementData = await _context.PatientMovements
                        .Where(m => m.Area == area &&
                                  m.MovementDate >= startDate &&
                                  m.MovementDate <= endDate)
                        .ToListAsync();

                    int totalDischarges = movementData.Sum(m => m.SentHomeCount + m.MortalityCount + m.TransferOutCount);

                    // Simplify HAI cases counting - just count patients with HAI status = true in this area
                    var haiCases = await _context.PatientMasters
                        .Join(_context.Patients,
                              pm => pm.HospNum,
                              p => p.HospNum,
                              (pm, p) => new { PatientMaster = pm, Patient = p })
                        .Where(x => //x.PatientMaster.HaiStatus == true &&
                                    x.Patient.AdmLocation == area &&
                                    x.Patient.AdmDate.HasValue &&
                                    x.Patient.AdmDate.Value >= startDate &&
                                    x.Patient.AdmDate.Value <= endDate)
                        .CountAsync();

                    // Create data object
                    var monthData = new HAIDischargeData
                    {
                        Area = area,
                        Month = month,
                        Year = year,
                        Cases = haiCases,
                        Discharges = totalDischarges
                    };

                    areaMonthlyData.Add(monthData);
                }

                result[area] = areaMonthlyData;
            }

            return result;
        }

        // Helper method to get HAI data for a specific month
        public async Task<Dictionary<string, HAIDischargeData>> GetHAIDischargeDataForMonthAsync(int year, int month)
        {
            var yearData = await GetHAIDischargeDataForYearAsync(year);

            // Filter to just the requested month
            var result = new Dictionary<string, HAIDischargeData>();
            foreach (var area in yearData.Keys)
            {
                var monthData = yearData[area].FirstOrDefault(d => d.Month == month);
                if (monthData != null)
                {
                    result[area] = monthData;
                }
            }

            return result;
        }
    }
}