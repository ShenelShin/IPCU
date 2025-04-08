using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IPCU.Models;
using IPCU.Data;

namespace IPCU.Services
{
    public class HaiReportService
    {
        private readonly ApplicationDbContext _context;

        public HaiReportService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MonthlyHaiReportData> GenerateMonthlyReportAsync(int year, int month)
        {
            var report = new MonthlyHaiReportData
            {
                Year = year,
                Month = month,
                DeviceAssociatedInfections = new List<HaiCaseData>(),
                VentilatorAssociatedEvents = new List<HaiCaseData>(),
                NonDeviceAssociatedInfections = new List<HaiCaseData>(),
                SiteSpecificInfections = new List<HaiCaseData>()
            };

            // Get first and last day of the month
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            // Calculate device days
            var deviceDays = await CalculateDeviceDaysAsync(startDate, endDate);

            // Get HAI cases
            var haiCases = await GetHaiCasesAsync(startDate, endDate);

            // Calculate patient days (can be discharge days depending on your implementation)
            var patientDays = await CalculatePatientDaysAsync(startDate, endDate);

            // Populate device-associated infections
            PopulateDeviceAssociatedInfections(report, haiCases, deviceDays);

            // Populate ventilator-associated events
            PopulateVentilatorAssociatedEvents(report, haiCases, deviceDays);

            // Populate non-device associated infections
            PopulateNonDeviceAssociatedInfections(report, haiCases, patientDays);

            // Populate site-specific infections
            PopulateSiteSpecificInfections(report, haiCases, patientDays);

            // Calculate totals
            CalculateTotals(report, patientDays);

            return report;
        }

        private async Task<Dictionary<string, int>> CalculateDeviceDaysAsync(DateTime startDate, DateTime endDate)
        {
            var deviceDays = new Dictionary<string, int>();

            // Query all devices that were connected during the period
            var devices = await _context.DeviceConnected
                .Where(d =>
                    (d.DeviceInsert <= endDate) &&
                    (d.DeviceRemove == null || d.DeviceRemove >= startDate))
                .ToListAsync();

            // Calculate days for each device type
            foreach (var device in devices)
            {
                int days = CalculateDeviceDaysInPeriod(device, startDate, endDate);

                if (!deviceDays.ContainsKey(device.DeviceType))
                    deviceDays[device.DeviceType] = 0;

                deviceDays[device.DeviceType] += days;
            }

            return deviceDays;
        }

        private int CalculateDeviceDaysInPeriod(DeviceConnected device, DateTime startDate, DateTime endDate)
        {
            // Calculate the intersection of the device period with the reporting period
            var effectiveStartDate = device.DeviceInsert > startDate ? device.DeviceInsert : startDate;
            var effectiveEndDate = device.DeviceRemove.HasValue && device.DeviceRemove < endDate
                ? device.DeviceRemove.Value
                : endDate;

            // Calculate days (inclusive of start and end date)
            return (effectiveEndDate - effectiveStartDate).Days + 1;
        }

        private async Task<List<PatientMaster>> GetHaiCasesAsync(DateTime startDate, DateTime endDate)
        {
            // Get all patients with HAI status true
            return await _context.PatientMasters
                .Where(p => p.HaiStatus == true)
                .Join(_context.Patients,
                      pm => pm.HospNum,
                      p => p.HospNum,
                      (pm, p) => new { PatientMaster = pm, Patient = p })
                .Where(x =>
                    x.Patient.AdmDate.HasValue &&
                    x.Patient.AdmDate <= endDate &&
                    (x.Patient.DeathDate == null || x.Patient.DeathDate >= startDate))
                .Select(x => x.PatientMaster)
                .ToListAsync();
        }

        private async Task<int> CalculatePatientDaysAsync(DateTime startDate, DateTime endDate)
        {
            // This can be either discharge days or patient days
            // Here we're calculating patient days based on admission/discharge dates
            var patients = await _context.Patients
                .Where(p =>
                    p.AdmDate.HasValue &&
                    p.AdmDate <= endDate &&
                    (p.DeathDate == null || p.DeathDate >= startDate))
                .ToListAsync();

            int patientDays = 0;

            foreach (var patient in patients)
            {
                var effectiveStartDate = patient.AdmDate > startDate ? patient.AdmDate.Value : startDate;
                var effectiveEndDate = patient.DeathDate.HasValue && patient.DeathDate < endDate
                    ? patient.DeathDate.Value
                    : endDate;

                patientDays += (effectiveEndDate - effectiveStartDate).Days + 1;
            }

            return patientDays;
        }

        private void PopulateDeviceAssociatedInfections(MonthlyHaiReportData report, List<PatientMaster> haiCases, Dictionary<string, int> deviceDays)
        {
            // For now we're just setting up the structure with device days
            // In a real implementation, you would categorize the HAI cases by type

            // PVAP
            report.DeviceAssociatedInfections.Add(new HaiCaseData
            {
                InfectionType = "Possible Ventilator Associated Pneumonia (PVAP)",
                CaseCount = 0, // This would be filtered from haiCases
                DeviceDays = deviceDays.ContainsKey("MV") ? deviceDays["MV"] : 0,
                Rate = 0 // Will be calculated when actual cases are added
            });

            // CLABSI
            report.DeviceAssociatedInfections.Add(new HaiCaseData
            {
                InfectionType = "Central line-associated Bloodstream Infection (CLABSI)",
                CaseCount = 0, // This would be filtered from haiCases
                DeviceDays = deviceDays.ContainsKey("CL") ? deviceDays["CL"] : 0,
                Rate = 0 // Will be calculated when actual cases are added
            });

            // CAUTI
            report.DeviceAssociatedInfections.Add(new HaiCaseData
            {
                InfectionType = "Catheter-associated Urinary Tract Infection (CAUTI)",
                CaseCount = 0, // This would be filtered from haiCases
                DeviceDays = deviceDays.ContainsKey("IUC") ? deviceDays["IUC"] : 0,
                Rate = 0 // Will be calculated when actual cases are added
            });
        }

        private void PopulateVentilatorAssociatedEvents(MonthlyHaiReportData report, List<PatientMaster> haiCases, Dictionary<string, int> deviceDays)
        {
            int mvDays = deviceDays.ContainsKey("MV") ? deviceDays["MV"] : 0;

            // VAC
            report.VentilatorAssociatedEvents.Add(new HaiCaseData
            {
                InfectionType = "Ventilator Associated Condition (VAC)",
                CaseCount = 0,
                DeviceDays = mvDays,
                Rate = 0
            });

            // IVAC
            report.VentilatorAssociatedEvents.Add(new HaiCaseData
            {
                InfectionType = "Infection-related Ventilator Associated Complication (IVAC)",
                CaseCount = 0,
                DeviceDays = mvDays,
                Rate = 0
            });

            // PVAP
            report.VentilatorAssociatedEvents.Add(new HaiCaseData
            {
                InfectionType = "Possible Ventilator Associated Pneumonia (PVAP)",
                CaseCount = 0,
                DeviceDays = mvDays,
                Rate = 0
            });

            // VAE Rate
            report.VentilatorAssociatedEvents.Add(new HaiCaseData
            {
                InfectionType = "VAE Rate per 1000",
                CaseCount = 0,
                DeviceDays = mvDays,
                Rate = 0
            });
        }

        private void PopulateNonDeviceAssociatedInfections(MonthlyHaiReportData report, List<PatientMaster> haiCases, int patientDays)
        {
            // This would normally be populated from a form or database with manual inputs
            // For now, we're just creating the structure

            // SSI
            report.NonDeviceAssociatedInfections.Add(new HaiCaseData
            {
                InfectionType = "Surgical Site Infections (SSI)",
                CaseCount = 0,
                DeviceDays = patientDays, // Using patient days here
                Rate = 0
            });

            // PD Peritonitis
            report.NonDeviceAssociatedInfections.Add(new HaiCaseData
            {
                InfectionType = "PD Peritonitis (rate)",
                CaseCount = 0,
                DeviceDays = 0, // Manual input
                Rate = 0
            });

            // Add other non-device associated infections
            var nonDeviceTypes = new[]
            {
                "PD Peritonitis (device days)",
                "Superficial incisional (SIP, SIS)",
                "Organ/Space SSI",
                "Deep Incisional Primary (DIP, DIS)",
                "Non-VAP or Pneumonia",
                "PNU1",
                "PNU2",
                "Laboratory confirmed Blood Stream Infections (BSI)",
                "Non-CAUTI (SUTI 1b)"
            };

            foreach (var type in nonDeviceTypes)
            {
                report.NonDeviceAssociatedInfections.Add(new HaiCaseData
                {
                    InfectionType = type,
                    CaseCount = 0,
                    DeviceDays = type == "Non-CAUTI (SUTI 1b)" ? patientDays : 0, // Most are manual inputs
                    Rate = 0
                });
            }
        }

        private void PopulateSiteSpecificInfections(MonthlyHaiReportData report, List<PatientMaster> haiCases, int patientDays)
        {
            // Site-specific infections are usually manual inputs
            var siteSpecificTypes = new[]
            {
                "Bone and Joint Infection (BJI)",
                "Cardiovascular (CVS) System Infection",
                "CVS-VASC",
                "Central Nervous System (CNS)",
                "Eye, Ear, Nose Throat, or Mouth (EENT)",
                "Gastrointestinal System Infection (GI)",
                "CDI- Clostridioides difficile",
                "GE-Gastroenteritis",
                "Lower Respiratory Infection (LRI)",
                "Reproductive Tract Infection (REPR)",
                "Skin and Soft Tissue (SST) Infection",
                "ST-Soft Tissue Infection",
                "SKIN-Skin infection",
                "DECU",
                "Urinary System Infection (USI)"
            };

            foreach (var type in siteSpecificTypes)
            {
                report.SiteSpecificInfections.Add(new HaiCaseData
                {
                    InfectionType = type,
                    CaseCount = 0,
                    DeviceDays = patientDays, // Using patient days for denominator
                    Rate = 0
                });
            }
        }

        private void CalculateTotals(MonthlyHaiReportData report, int patientDays)
        {
            // Calculate rates for each infection
            foreach (var infection in report.DeviceAssociatedInfections)
            {
                if (infection.DeviceDays > 0)
                    infection.Rate = (decimal)infection.CaseCount * 1000 / infection.DeviceDays;
            }

            foreach (var infection in report.VentilatorAssociatedEvents)
            {
                if (infection.DeviceDays > 0)
                    infection.Rate = (decimal)infection.CaseCount * 1000 / infection.DeviceDays;
            }

            foreach (var infection in report.NonDeviceAssociatedInfections)
            {
                if (infection.DeviceDays > 0)
                    infection.Rate = (decimal)infection.CaseCount * 100 / infection.DeviceDays;
            }

            foreach (var infection in report.SiteSpecificInfections)
            {
                if (infection.DeviceDays > 0)
                    infection.Rate = (decimal)infection.CaseCount * 100 / infection.DeviceDays;
            }

            // Calculate totals
            report.DeviceAssociatedTotal = new HaiCaseData
            {
                InfectionType = "Device-associated",
                CaseCount = report.DeviceAssociatedInfections.Sum(i => i.CaseCount),
                DeviceDays = patientDays,
                Rate = patientDays > 0 ? (decimal)report.DeviceAssociatedInfections.Sum(i => i.CaseCount) * 100 / patientDays : 0
            };

            report.NonDeviceAssociatedTotal = new HaiCaseData
            {
                InfectionType = "Non-Device associated",
                CaseCount = report.NonDeviceAssociatedInfections.Sum(i => i.CaseCount),
                DeviceDays = patientDays,
                Rate = patientDays > 0 ? (decimal)report.NonDeviceAssociatedInfections.Sum(i => i.CaseCount) * 100 / patientDays : 0
            };

            report.SsiTotal = new HaiCaseData
            {
                InfectionType = "Surgical Site Infections (SSI)",
                CaseCount = report.NonDeviceAssociatedInfections
                    .Where(i => i.InfectionType.Contains("SSI") || i.InfectionType.Contains("incisional"))
                    .Sum(i => i.CaseCount),
                DeviceDays = patientDays,
                Rate = patientDays > 0 ? (decimal)report.NonDeviceAssociatedInfections
                    .Where(i => i.InfectionType.Contains("SSI") || i.InfectionType.Contains("incisional"))
                    .Sum(i => i.CaseCount) * 100 / patientDays : 0
            };

            // Overall HAI rate
            report.OverallHaiRate = new HaiCaseData
            {
                InfectionType = "OVERALL HAI RATE (DBM)",
                CaseCount = report.DeviceAssociatedInfections.Sum(i => i.CaseCount) +
                           report.NonDeviceAssociatedInfections.Sum(i => i.CaseCount) +
                           report.SiteSpecificInfections.Sum(i => i.CaseCount),
                DeviceDays = patientDays,
                Rate = patientDays > 0 ? (decimal)(report.DeviceAssociatedInfections.Sum(i => i.CaseCount) +
                        report.NonDeviceAssociatedInfections.Sum(i => i.CaseCount) +
                        report.SiteSpecificInfections.Sum(i => i.CaseCount)) * 100 / patientDays : 0
            };

            // DOH rate (Device-Based and SSI)
            report.DohRate = new HaiCaseData
            {
                InfectionType = "DOH RATE (Device-Based, and SSI)",
                CaseCount = report.DeviceAssociatedInfections.Sum(i => i.CaseCount) +
                           report.NonDeviceAssociatedInfections
                               .Where(i => i.InfectionType.Contains("SSI") || i.InfectionType.Contains("incisional"))
                               .Sum(i => i.CaseCount),
                DeviceDays = patientDays,
                Rate = patientDays > 0 ? (decimal)(report.DeviceAssociatedInfections.Sum(i => i.CaseCount) +
                        report.NonDeviceAssociatedInfections
                            .Where(i => i.InfectionType.Contains("SSI") || i.InfectionType.Contains("incisional"))
                            .Sum(i => i.CaseCount)) * 100 / patientDays : 0
            };
        }

        public async Task<QuarterlyHaiReportData> GenerateQuarterlyReportAsync(int year, int quarter)
        {
            // Calculate the months in the quarter
            int startMonth = (quarter - 1) * 3 + 1;

            var monthlyReports = new List<MonthlyHaiReportData>();

            // Generate monthly reports for all months in the quarter
            for (int i = 0; i < 3; i++)
            {
                monthlyReports.Add(await GenerateMonthlyReportAsync(year, startMonth + i));
            }

            // Create quarterly report
            var quarterlyReport = new QuarterlyHaiReportData
            {
                Year = year,
                Quarter = quarter,
                MonthlyReports = monthlyReports,
                TotalPatientDays = monthlyReports.Sum(r => r.DeviceAssociatedTotal.DeviceDays)
            };

            // Calculate aggregated quarterly data
            CalculateQuarterlyTotals(quarterlyReport);

            return quarterlyReport;
        }

        private void CalculateQuarterlyTotals(QuarterlyHaiReportData report)
        {
            int totalCases = report.MonthlyReports.Sum(r => r.OverallHaiRate.CaseCount);
            int totalPatientDays = report.TotalPatientDays;

            report.QuarterlyRate = totalPatientDays > 0
                ? (decimal)totalCases * 100 / totalPatientDays
                : 0;
        }
    }
}