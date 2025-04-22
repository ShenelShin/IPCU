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
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var report = new MonthlyHaiReportData
            {
                Year = year,
                Month = month,
                DeviceAssociatedInfections = new List<HaiCaseData>(),
                VentilatorAssociatedEvents = new List<HaiCaseData>(),
                NonDeviceAssociatedInfections = new List<HaiCaseData>(),
                SiteSpecificInfections = new List<HaiCaseData>()
            };

            var deviceDays = await CalculateDeviceDaysAsync(startDate, endDate);
            var haiCounts = await GetHaiCountsAsync(startDate, endDate);
            var patientDays = await CalculatePatientDaysAsync(startDate, endDate);

            PopulateDeviceAssociatedInfections(report, haiCounts, deviceDays);
            PopulateVentilatorAssociatedEvents(report, haiCounts, deviceDays);
            PopulateNonDeviceAssociatedInfections(report, haiCounts, patientDays);
            PopulateSiteSpecificInfections(report, haiCounts, patientDays);
            CalculateTotals(report, patientDays);

            return report;
        }

        private async Task<Dictionary<string, int>> CalculateDeviceDaysAsync(DateTime startDate, DateTime endDate)
        {
            var deviceDays = new Dictionary<string, int>();

            var devices = await _context.DeviceConnected
                .Where(d => d.DeviceInsert <= endDate &&
                           (d.DeviceRemove == null || d.DeviceRemove >= startDate))
                .ToListAsync();

            foreach (var device in devices)
            {
                var effectiveStart = device.DeviceInsert > startDate ? device.DeviceInsert : startDate;
                var effectiveEnd = device.DeviceRemove.HasValue && device.DeviceRemove < endDate
                    ? device.DeviceRemove.Value
                    : endDate;

                var days = (effectiveEnd - effectiveStart).Days + 1;

                if (!deviceDays.ContainsKey(device.DeviceType))
                    deviceDays[device.DeviceType] = 0;

                deviceDays[device.DeviceType] += days;
            }

            return deviceDays;
        }

        private async Task<Dictionary<string, int>> GetHaiCountsAsync(DateTime startDate, DateTime endDate)
        {
            var counts = new Dictionary<string, int>();

            // Initialize all infection type counters
            var infectionTypes = new[] {
        "PVAP", "CLABSI", "CAUTI", "VAE", "SSI", "BSI", "CVS", "UTI",
        "Pneumonia", "USI", "GI", "SST", "BJI", "CNS", "EENT", "REPR",
        "PD_Peritonitis", "CDI", "GE", "LRI", "ST", "SKIN", "DECU", "VASC",
        "SIP", "DIP", "OS", "PNEU1", "PNEU2", "PNEU3", "VAC", "IVAC"
    };

            foreach (var type in infectionTypes)
                counts[type] = 0;

            // Count PVAP directly from PediatricVAEChecklist table
            counts["PVAP"] = await _context.PediatricVAEChecklist
                .Where(f => f.DateCreated >= startDate && f.DateCreated <= endDate)
                .CountAsync();

            // Count VAE and subtypes
            var vaeCases = await _context.VentilatorEventChecklists
                .Where(f => f.DateCreated >= startDate && f.DateCreated <= endDate)
                .ToListAsync();

            counts["VAE"] = vaeCases.Count;
            counts["VAC"] = vaeCases.Count(v => v.TypeClass == "VAC");
            counts["IVAC"] = vaeCases.Count(v => v.TypeClass == "IVAC");

            // Count BSI and CLABSI
            var bsiCases = await _context.LaboratoryConfirmedBSI
                .Where(f => f.DateCreated >= startDate && f.DateCreated <= endDate)
                .ToListAsync();

            counts["BSI"] = bsiCases.Count;
            counts["CLABSI"] = bsiCases.Count(f => f.centralline == "ForMedications");

            // BJI, CNS, EENT, REPR, LRI from BSI forms based on TypeClass
            counts["BJI"] = bsiCases.Count(f => f.TypeClass == "BJI");
            counts["CNS"] = bsiCases.Count(f => f.TypeClass == "CNS");
            counts["EENT"] = bsiCases.Count(f => f.TypeClass == "EENT");
            counts["REPR"] = bsiCases.Count(f => f.TypeClass == "REPR");
            counts["LRI"] = bsiCases.Count(f => f.TypeClass == "LRI");

            // Count UTI and CAUTI
            var utiCases = await _context.UTIModels
                .Where(f => f.DateCreated >= startDate && f.DateCreated <= endDate)
                .ToListAsync();

            counts["UTI"] = utiCases.Count;
            counts["CAUTI"] = utiCases.Count(f => f.CatheterPresent == true);

            // Count SSI and subtypes
            var ssiCases = await _context.SurgicalSiteInfectionChecklist
                .Where(f => f.DateCreated >= startDate && f.DateCreated <= endDate)
                .ToListAsync();

            counts["SSI"] = ssiCases.Count;
            counts["SIP"] = ssiCases.Count(f => f.TypeClass == "SIP");
            counts["DIP"] = ssiCases.Count(f => f.TypeClass == "DIP");
            counts["OS"] = ssiCases.Count(f => f.TypeClass == "OS");

            // Count Pneumonia and subtypes
            var pneumoniaCases = await _context.Pneumonias
                .Where(f => f.DateCreated >= startDate && f.DateCreated <= endDate)
                .ToListAsync();

            counts["Pneumonia"] = pneumoniaCases.Count;
            counts["PNEU1"] = pneumoniaCases.Count(f => f.TypeClass == "PNEU1");
            counts["PNEU2"] = pneumoniaCases.Count(f => f.TypeClass == "PNEU2");
            counts["PNEU3"] = pneumoniaCases.Count(f => f.TypeClass == "PNEU3");

            // Count GI infections and subtypes
            var giCases = await _context.GIInfectionChecklists
                .Where(f => f.DateCreated >= startDate && f.DateCreated <= endDate)
                .ToListAsync();

            counts["GI"] = giCases.Count;
            counts["CDI"] = giCases.Count(f => f.TypeClass == "CDI");
            counts["GE"] = giCases.Count(f => f.TypeClass == "GE");

            // Count SST infections and subtypes
            var sstCases = await _context.SSTInfectionModels
                .Where(f => f.DateCreated >= startDate && f.DateCreated <= endDate)
                .ToListAsync();

            counts["SST"] = sstCases.Count;
            counts["ST"] = sstCases.Count(f => f.InfectionType == "SoftTissue");
            counts["SKIN"] = sstCases.Count(f => f.InfectionType == "Skin");
            counts["DECU"] = sstCases.Count(f => f.InfectionType == "Decubitus");

            // Count CVS infections and VASC subtype
            var cvsCases = await _context.CardiovascularSystemInfection
                .Where(f => f.DateCreated >= startDate && f.DateCreated <= endDate)
                .ToListAsync();

            counts["CVS"] = cvsCases.Count;
            counts["VASC"] = cvsCases.Count(f => f.TypeClass == "Vascular");

            // Count USI directly
            counts["USI"] = await _context.Usi
                .Where(f => f.DateCreated >= startDate && f.DateCreated <= endDate)
                .CountAsync();

            return counts;
        }

        private async Task<int> CalculatePatientDaysAsync(DateTime startDate, DateTime endDate)
        {
            var patients = await _context.Patients
                .Where(p => p.AdmDate.HasValue &&
                            p.AdmDate <= endDate &&
                            (p.DeathDate == null || p.DeathDate >= startDate))
                .ToListAsync();

            int totalDays = 0;
            foreach (var patient in patients)
            {
                var effectiveStart = patient.AdmDate > startDate ? patient.AdmDate.Value : startDate;
                var effectiveEnd = patient.DeathDate.HasValue && patient.DeathDate < endDate
                    ? patient.DeathDate.Value
                    : endDate;

                totalDays += (effectiveEnd - effectiveStart).Days + 1;
            }

            return totalDays;
        }

        private void PopulateDeviceAssociatedInfections(MonthlyHaiReportData report,
            Dictionary<string, int> haiCounts, Dictionary<string, int> deviceDays)
        {
            // PVAP
            int pvapCount = haiCounts["PVAP"];
            int mvDays = deviceDays.GetValueOrDefault("MV");
            report.DeviceAssociatedInfections.Add(new HaiCaseData
            {
                InfectionType = "Possible Ventilator Associated Pneumonia (PVAP)",
                CaseCount = pvapCount,
                DeviceDays = mvDays,
                Rate = CalculateRate(pvapCount, mvDays, 1000)
            });

            // CLABSI
            int clabsiCount = haiCounts["BSI"] - haiCounts["CLABSI"];
            int clDays = deviceDays.GetValueOrDefault("CL");
            report.DeviceAssociatedInfections.Add(new HaiCaseData
            {
                InfectionType = "Central line-associated Bloodstream Infection (CLABSI)",
                CaseCount = clabsiCount,
                DeviceDays = clDays,
                Rate = CalculateRate(clabsiCount, clDays, 1000)
            });

            // CAUTI
            int cautiCount = haiCounts["CAUTI"];
            int ucDays = deviceDays.GetValueOrDefault("IUC");
            report.DeviceAssociatedInfections.Add(new HaiCaseData
            {
                InfectionType = "Catheter-associated Urinary Tract Infection (CAUTI)",
                CaseCount = cautiCount,
                DeviceDays = ucDays,
                Rate = CalculateRate(cautiCount, ucDays, 1000)
            });
        }

        private void PopulateVentilatorAssociatedEvents(MonthlyHaiReportData report,
            Dictionary<string, int> haiCounts, Dictionary<string, int> deviceDays)
        {
            int mvDays = deviceDays.GetValueOrDefault("MV");
            int vacCount = haiCounts["VAC"];
            int ivacCount = haiCounts["IVAC"];
            int pvapCount = haiCounts["PVAP"];

            report.VentilatorAssociatedEvents.Add(new HaiCaseData
            {
                InfectionType = "Ventilator Associated Condition (VAC)",
                CaseCount = vacCount,
                DeviceDays = mvDays,
                Rate = CalculateRate(vacCount, mvDays, 1000)
            });

            report.VentilatorAssociatedEvents.Add(new HaiCaseData
            {
                InfectionType = "Possible Ventilator Associated Pneumonia (PVAP)",
                CaseCount = pvapCount,
                DeviceDays = mvDays,
                Rate = CalculateRate(pvapCount, mvDays, 1000)
            });

            // Total VAE Rate
            int totalVae = vacCount + ivacCount + pvapCount;
            report.VentilatorAssociatedEvents.Add(new HaiCaseData
            {
                InfectionType = "VAE Rate per 1000",
                CaseCount = totalVae,
                DeviceDays = mvDays,
                Rate = CalculateRate(totalVae, mvDays, 1000)
            });
        }

        private void PopulateNonDeviceAssociatedInfections(MonthlyHaiReportData report,
            Dictionary<string, int> haiCounts, int patientDays)
        {
            // SSI
            report.NonDeviceAssociatedInfections.Add(new HaiCaseData
            {
                InfectionType = "Surgical Site Infections (SSI)",
                CaseCount = haiCounts["SSI"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["SSI"], patientDays)
            });

            // SSI Breakdown
            report.NonDeviceAssociatedInfections.Add(new HaiCaseData
            {
                InfectionType = "Superficial incisional (SIP, SIS)",
                CaseCount = haiCounts["SIP"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["SIP"], patientDays)
            });

            report.NonDeviceAssociatedInfections.Add(new HaiCaseData
            {
                InfectionType = "Deep Incisional Primary (DIP, DIS)",
                CaseCount = haiCounts["DIP"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["DIP"], patientDays)
            });

            report.NonDeviceAssociatedInfections.Add(new HaiCaseData
            {
                InfectionType = "Organ/Space SSI",
                CaseCount = haiCounts["OS"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["OS"], patientDays)
            });

            //// PD Peritonitis - Uncommented
            //int pdDays = _context.PeritonealDialysisRecords
            //    .Where(p => p.DateCreated >= new DateTime(report.Year, report.Month, 1) &&
            //               p.DateCreated <= new DateTime(report.Year, report.Month, 1).AddMonths(1).AddDays(-1))
            //    .Sum(p => p.DialysisDays);

            //report.NonDeviceAssociatedInfections.Add(new HaiCaseData
            //{
            //    InfectionType = "PD Peritonitis (rate)",
            //    CaseCount = haiCounts["PD_Peritonitis"],
            //    DeviceDays = pdDays,
            //    Rate = CalculateRate(haiCounts["PD_Peritonitis"], pdDays)
            //});

            //report.NonDeviceAssociatedInfections.Add(new HaiCaseData
            //{
            //    InfectionType = "PD Peritonitis (device days)",
            //    CaseCount = 0,
            //    DeviceDays = pdDays,
            //    Rate = 0
            //});

            // Non-VAP Pneumonia
            int nonVapCount = haiCounts["Pneumonia"];
            report.NonDeviceAssociatedInfections.Add(new HaiCaseData
            {
                InfectionType = "Non-VAP Pneumonia",
                CaseCount = nonVapCount,
                DeviceDays = patientDays,
                Rate = CalculateRate(nonVapCount, patientDays)
            });

            // Non-CLABSI BSI
            int nonClabsiCount = haiCounts["BSI"] - haiCounts["CLABSI"];
            report.NonDeviceAssociatedInfections.Add(new HaiCaseData
            {
                InfectionType = "Non-CLABSI BSI",
                CaseCount = nonClabsiCount,
                DeviceDays = patientDays,
                Rate = CalculateRate(nonClabsiCount, patientDays)
            });

            // Non-CAUTI UTI
            int nonCautiCount = haiCounts["UTI"] - haiCounts["CAUTI"];
            report.NonDeviceAssociatedInfections.Add(new HaiCaseData
            {
                InfectionType = "Non-CAUTI UTI (SUTI 1b)",
                CaseCount = nonCautiCount,
                DeviceDays = patientDays,
                Rate = CalculateRate(nonCautiCount, patientDays)
            });
        }

        private void PopulateSiteSpecificInfections(MonthlyHaiReportData report,
            Dictionary<string, int> haiCounts, int patientDays)
        {
            // Bone and Joint
            report.SiteSpecificInfections.Add(new HaiCaseData
            {
                InfectionType = "Bone and Joint Infection (BJI)",
                CaseCount = haiCounts["BJI"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["BJI"], patientDays)
            });

            // CVS
            report.SiteSpecificInfections.Add(new HaiCaseData
            {
                InfectionType = "Cardiovascular (CVS) System Infection",
                CaseCount = haiCounts["CVS"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["CVS"], patientDays)
            });

            // CVS-VASC
            report.SiteSpecificInfections.Add(new HaiCaseData
            {
                InfectionType = "CVS-VASC",
                CaseCount = haiCounts["VASC"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["VASC"], patientDays)
            });

            // CNS
            report.SiteSpecificInfections.Add(new HaiCaseData
            {
                InfectionType = "Central Nervous System (CNS)",
                CaseCount = haiCounts["CNS"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["CNS"], patientDays)
            });

            // EENT
            report.SiteSpecificInfections.Add(new HaiCaseData
            {
                InfectionType = "Eye, Ear, Nose Throat, or Mouth (EENT)",
                CaseCount = haiCounts["EENT"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["EENT"], patientDays)
            });

            // GI
            report.SiteSpecificInfections.Add(new HaiCaseData
            {
                InfectionType = "Gastrointestinal System Infection (GI)",
                CaseCount = haiCounts["GI"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["GI"], patientDays)
            });

            // CDI
            report.SiteSpecificInfections.Add(new HaiCaseData
            {
                InfectionType = "CDI- Clostridioides difficile",
                CaseCount = haiCounts["CDI"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["CDI"], patientDays)
            });

            // GE
            report.SiteSpecificInfections.Add(new HaiCaseData
            {
                InfectionType = "GE-Gastroenteritis",
                CaseCount = haiCounts["GE"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["GE"], patientDays)
            });

            // LRI
            report.SiteSpecificInfections.Add(new HaiCaseData
            {
                InfectionType = "Lower Respiratory Infection (LRI)",
                CaseCount = haiCounts["LRI"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["LRI"], patientDays)
            });

            // REPR
            report.SiteSpecificInfections.Add(new HaiCaseData
            {
                InfectionType = "Reproductive Tract Infection (REPR)",
                CaseCount = haiCounts["REPR"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["REPR"], patientDays)
            });

            // SST
            report.SiteSpecificInfections.Add(new HaiCaseData
            {
                InfectionType = "SST",
                CaseCount = haiCounts["SST"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["SST"], patientDays)
            });

            // ST
            report.SiteSpecificInfections.Add(new HaiCaseData
            {
                InfectionType = "SST",
                CaseCount = haiCounts["ST"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["ST"], patientDays)
            });

            // SKIN
            report.SiteSpecificInfections.Add(new HaiCaseData
            {
                InfectionType = "SST",
                CaseCount = haiCounts["SKIN"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["SKIN"], patientDays)
            });

            // DECU
            report.SiteSpecificInfections.Add(new HaiCaseData
            {
                InfectionType = "DECU",
                CaseCount = haiCounts["DECU"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["DECU"], patientDays)
            });

            // USI
            report.SiteSpecificInfections.Add(new HaiCaseData
            {
                InfectionType = "Urinary System Infection (USI)",
                CaseCount = haiCounts["USI"],
                DeviceDays = patientDays,
                Rate = CalculateRate(haiCounts["USI"], patientDays)
            });
        }

        private void CalculateTotals(MonthlyHaiReportData report, int patientDays)
        {
            // Device Associated Total
            report.DeviceAssociatedTotal = new HaiCaseData
            {
                InfectionType = "Device-associated",
                CaseCount = report.DeviceAssociatedInfections.Sum(i => i.CaseCount),
                DeviceDays = patientDays,
                Rate = CalculateRate(report.DeviceAssociatedInfections.Sum(i => i.CaseCount), patientDays)
            };

            // Non-Device Associated Total
            report.NonDeviceAssociatedTotal = new HaiCaseData
            {
                InfectionType = "Non-Device associated",
                CaseCount = report.NonDeviceAssociatedInfections.Sum(i => i.CaseCount),
                DeviceDays = patientDays,
                Rate = CalculateRate(report.NonDeviceAssociatedInfections.Sum(i => i.CaseCount), patientDays)
            };

            // SSI Total
            report.SsiTotal = new HaiCaseData
            {
                InfectionType = "Surgical Site Infections (SSI)",
                CaseCount = report.NonDeviceAssociatedInfections
                    .Where(i => i.InfectionType.Contains("SSI") || i.InfectionType.Contains("incisional"))
                    .Sum(i => i.CaseCount),
                DeviceDays = patientDays,
                Rate = CalculateRate(
                    report.NonDeviceAssociatedInfections
                        .Where(i => i.InfectionType.Contains("SSI") || i.InfectionType.Contains("incisional"))
                        .Sum(i => i.CaseCount),
                    patientDays)
            };

            // Overall HAI Rate
            report.OverallHaiRate = new HaiCaseData
            {
                InfectionType = "OVERALL HAI RATE (DBM)",
                CaseCount = report.DeviceAssociatedTotal.CaseCount +
                           report.NonDeviceAssociatedTotal.CaseCount +
                           report.SiteSpecificInfections.Sum(i => i.CaseCount),
                DeviceDays = patientDays,
                Rate = CalculateRate(
                    report.DeviceAssociatedTotal.CaseCount +
                    report.NonDeviceAssociatedTotal.CaseCount +
                    report.SiteSpecificInfections.Sum(i => i.CaseCount),
                    patientDays)
            };

            // DOH Rate
            report.DohRate = new HaiCaseData
            {
                InfectionType = "DOH RATE (Device-Based, and SSI)",
                CaseCount = report.DeviceAssociatedTotal.CaseCount + report.SsiTotal.CaseCount,
                DeviceDays = patientDays,
                Rate = CalculateRate(
                    report.DeviceAssociatedTotal.CaseCount + report.SsiTotal.CaseCount,
                    patientDays)
            };
        }

        private decimal CalculateRate(int numerator, int denominator, int multiplier = 100)
        {
            return denominator > 0 ? (decimal)numerator * multiplier / denominator : 0;
        }

        public async Task<QuarterlyHaiReportData> GenerateQuarterlyReportAsync(int year, int quarter)
        {
            int startMonth = (quarter - 1) * 3 + 1;
            var monthlyReports = new List<MonthlyHaiReportData>();

            for (int i = 0; i < 3; i++)
            {
                monthlyReports.Add(await GenerateMonthlyReportAsync(year, startMonth + i));
            }

            return new QuarterlyHaiReportData
            {
                Year = year,
                Quarter = quarter,
                MonthlyReports = monthlyReports,
                TotalPatientDays = monthlyReports.Sum(r => r.DeviceAssociatedTotal.DeviceDays),
                QuarterlyRate = CalculateRate(
                    monthlyReports.Sum(r => r.OverallHaiRate.CaseCount),
                    monthlyReports.Sum(r => r.DeviceAssociatedTotal.DeviceDays))
            };
        }

        public async Task<AnnualHaiReportData> GenerateAnnualReportAsync(int year)
        {
            var monthlyReports = new List<MonthlyHaiReportData>();

            // Generate reports for all 12 months
            for (int month = 1; month <= 12; month++)
            {
                monthlyReports.Add(await GenerateMonthlyReportAsync(year, month));
            }

            // Create quarterly reports for reference
            var quarterlyReports = new List<QuarterlyHaiReportData>();
            for (int quarter = 1; quarter <= 4; quarter++)
            {
                quarterlyReports.Add(await GenerateQuarterlyReportAsync(year, quarter));
            }

            // Calculate annual totals and rates
            int totalPatientDays = monthlyReports.Sum(r => r.DeviceAssociatedTotal.DeviceDays);
            int totalDeviceAssociatedInfections = monthlyReports.Sum(r => r.DeviceAssociatedTotal.CaseCount);
            int totalNonDeviceAssociatedInfections = monthlyReports.Sum(r => r.NonDeviceAssociatedTotal.CaseCount);
            int totalSsiInfections = monthlyReports.Sum(r => r.SsiTotal.CaseCount);
            int totalSiteSpecificInfections = monthlyReports.Sum(r =>
                r.SiteSpecificInfections.Sum(i => i.CaseCount));
            int totalInfections = totalDeviceAssociatedInfections + totalNonDeviceAssociatedInfections + totalSiteSpecificInfections;

            // Calculate device days
            var deviceDaysByType = new Dictionary<string, int>();
            foreach (var month in monthlyReports)
            {
                foreach (var infection in month.DeviceAssociatedInfections)
                {
                    if (!deviceDaysByType.ContainsKey(infection.InfectionType))
                        deviceDaysByType[infection.InfectionType] = 0;

                    deviceDaysByType[infection.InfectionType] += infection.DeviceDays;
                }
            }

            // Create annual summary data
            var annualDeviceAssociatedSummary = new List<HaiCaseData>();
            var annualVaeSummary = new List<HaiCaseData>();
            var annualNonDeviceAssociatedSummary = new List<HaiCaseData>();
            var annualSiteSpecificSummary = new List<HaiCaseData>();

            // Aggregate device-associated infections
            var deviceTypes = monthlyReports.SelectMany(m => m.DeviceAssociatedInfections)
                .Select(i => i.InfectionType)
                .Distinct();

            foreach (var deviceType in deviceTypes)
            {
                int caseCount = monthlyReports.Sum(m =>
                    m.DeviceAssociatedInfections
                     .FirstOrDefault(i => i.InfectionType == deviceType)?.CaseCount ?? 0);
                int deviceDays = deviceDaysByType.GetValueOrDefault(deviceType, 0);

                annualDeviceAssociatedSummary.Add(new HaiCaseData
                {
                    InfectionType = deviceType,
                    CaseCount = caseCount,
                    DeviceDays = deviceDays,
                    Rate = CalculateRate(caseCount, deviceDays, 1000)
                });
            }

            // Aggregate VAE data
            var vaeTypes = monthlyReports.SelectMany(m => m.VentilatorAssociatedEvents)
                .Select(i => i.InfectionType)
                .Distinct();

            foreach (var vaeType in vaeTypes)
            {
                int caseCount = monthlyReports.Sum(m =>
                    m.VentilatorAssociatedEvents
                     .FirstOrDefault(i => i.InfectionType == vaeType)?.CaseCount ?? 0);
                int deviceDays = monthlyReports.Sum(m =>
                    m.VentilatorAssociatedEvents
                     .FirstOrDefault(i => i.InfectionType == vaeType)?.DeviceDays ?? 0);

                annualVaeSummary.Add(new HaiCaseData
                {
                    InfectionType = vaeType,
                    CaseCount = caseCount,
                    DeviceDays = deviceDays,
                    Rate = CalculateRate(caseCount, deviceDays, 1000)
                });
            }

            // Aggregate non-device-associated infections
            var nonDeviceTypes = monthlyReports.SelectMany(m => m.NonDeviceAssociatedInfections)
                .Select(i => i.InfectionType)
                .Distinct();

            foreach (var infectionType in nonDeviceTypes)
            {
                int caseCount = monthlyReports.Sum(m =>
                    m.NonDeviceAssociatedInfections
                     .FirstOrDefault(i => i.InfectionType == infectionType)?.CaseCount ?? 0);

                annualNonDeviceAssociatedSummary.Add(new HaiCaseData
                {
                    InfectionType = infectionType,
                    CaseCount = caseCount,
                    DeviceDays = totalPatientDays,
                    Rate = CalculateRate(caseCount, totalPatientDays)
                });
            }

            // Aggregate site-specific infections
            var siteTypes = monthlyReports.SelectMany(m => m.SiteSpecificInfections)
                .Select(i => i.InfectionType)
                .Distinct();

            foreach (var siteType in siteTypes)
            {
                int caseCount = monthlyReports.Sum(m =>
                    m.SiteSpecificInfections
                     .FirstOrDefault(i => i.InfectionType == siteType)?.CaseCount ?? 0);

                annualSiteSpecificSummary.Add(new HaiCaseData
                {
                    InfectionType = siteType,
                    CaseCount = caseCount,
                    DeviceDays = totalPatientDays,
                    Rate = CalculateRate(caseCount, totalPatientDays)
                });
            }

            // Create the summary totals
            var deviceAssociatedTotal = new HaiCaseData
            {
                InfectionType = "Device-associated",
                CaseCount = totalDeviceAssociatedInfections,
                DeviceDays = totalPatientDays,
                Rate = CalculateRate(totalDeviceAssociatedInfections, totalPatientDays)
            };

            var nonDeviceAssociatedTotal = new HaiCaseData
            {
                InfectionType = "Non-Device associated",
                CaseCount = totalNonDeviceAssociatedInfections,
                DeviceDays = totalPatientDays,
                Rate = CalculateRate(totalNonDeviceAssociatedInfections, totalPatientDays)
            };

            var ssiTotal = new HaiCaseData
            {
                InfectionType = "Surgical Site Infections (SSI)",
                CaseCount = totalSsiInfections,
                DeviceDays = totalPatientDays,
                Rate = CalculateRate(totalSsiInfections, totalPatientDays)
            };

            var overallHaiRate = new HaiCaseData
            {
                InfectionType = "OVERALL HAI RATE (DBM)",
                CaseCount = totalInfections,
                DeviceDays = totalPatientDays,
                Rate = CalculateRate(totalInfections, totalPatientDays)
            };

            var dohRate = new HaiCaseData
            {
                InfectionType = "DOH RATE (Device-Based, and SSI)",
                CaseCount = totalDeviceAssociatedInfections + totalSsiInfections,
                DeviceDays = totalPatientDays,
                Rate = CalculateRate(totalDeviceAssociatedInfections + totalSsiInfections, totalPatientDays)
            };

            // Return annual report data
            return new AnnualHaiReportData
            {
                Year = year,
                MonthlyReports = monthlyReports,
                QuarterlyReports = quarterlyReports,
                AnnualDeviceAssociatedInfections = annualDeviceAssociatedSummary,
                AnnualVentilatorAssociatedEvents = annualVaeSummary,
                AnnualNonDeviceAssociatedInfections = annualNonDeviceAssociatedSummary,
                AnnualSiteSpecificInfections = annualSiteSpecificSummary,
                DeviceAssociatedTotal = deviceAssociatedTotal,
                NonDeviceAssociatedTotal = nonDeviceAssociatedTotal,
                SsiTotal = ssiTotal,
                OverallHaiRate = overallHaiRate,
                DohRate = dohRate,
                TotalPatientDays = totalPatientDays
            };
        }

        public class AnnualHaiReportData
        {
            public int Year { get; set; }
            public List<MonthlyHaiReportData> MonthlyReports { get; set; }
            public List<QuarterlyHaiReportData> QuarterlyReports { get; set; }

            public List<HaiCaseData> AnnualDeviceAssociatedInfections { get; set; }
            public List<HaiCaseData> AnnualVentilatorAssociatedEvents { get; set; }
            public List<HaiCaseData> AnnualNonDeviceAssociatedInfections { get; set; }
            public List<HaiCaseData> AnnualSiteSpecificInfections { get; set; }

            public HaiCaseData DeviceAssociatedTotal { get; set; }
            public HaiCaseData NonDeviceAssociatedTotal { get; set; }
            public HaiCaseData SsiTotal { get; set; }
            public HaiCaseData OverallHaiRate { get; set; }
            public HaiCaseData DohRate { get; set; }

            public int TotalPatientDays { get; set; }
        }
    }
}