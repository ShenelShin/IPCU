using System;
using System.Collections.Generic;

namespace IPCU.Models
{
    // Model to hold monthly HAI report data
    public class MonthlyHaiReportData
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int VentilatorDays { get; set; } // Total ventilator days (MV)


        // Lists of specific infection types
        public List<HaiCaseData> DeviceAssociatedInfections { get; set; }
        public List<HaiCaseData> VentilatorAssociatedEvents { get; set; }
        public List<HaiCaseData> NonDeviceAssociatedInfections { get; set; }
        public List<HaiCaseData> SiteSpecificInfections { get; set; }

        // Totals
        public HaiCaseData DeviceAssociatedTotal { get; set; }
        public HaiCaseData NonDeviceAssociatedTotal { get; set; }
        public HaiCaseData SsiTotal { get; set; }
        public HaiCaseData OverallHaiRate { get; set; }
        public HaiCaseData DohRate { get; set; }

        // Helper method to get month name
        public string MonthName => new DateTime(Year, Month, 1).ToString("MMMM");
    }

    // Model to hold quarterly HAI report data
    public class QuarterlyHaiReportData
    {
        public int Year { get; set; }
        public int Quarter { get; set; }
        public List<MonthlyHaiReportData> MonthlyReports { get; set; }
        public int TotalPatientDays { get; set; }
        public decimal QuarterlyRate { get; set; }

        // Helper method to get quarter range
        public string QuarterRange
        {
            get
            {
                int startMonth = (Quarter - 1) * 3 + 1;
                string startMonthName = new DateTime(Year, startMonth, 1).ToString("MMMM");
                string endMonthName = new DateTime(Year, startMonth + 2, 1).ToString("MMMM");
                return $"{startMonthName} - {endMonthName}";
            }
        }
    }

    // Model to hold individual HAI case data
    public class HaiCaseData
    {
        public string InfectionType { get; set; }
        public int CaseCount { get; set; }
        public int DeviceDays { get; set; }
        public decimal Rate { get; set; }

        // Formatted rate for display
        public string FormattedRate
        {
            get
            {
                // Different display format based on infection type
                if (InfectionType.Contains("VAE") ||
                    InfectionType.Contains("CLABSI") ||
                    InfectionType.Contains("CAUTI") ||
                    InfectionType.Contains("PVAP"))
                {
                    // Device-associated rates are displayed per 1000 device days
                    return Rate.ToString("0.00");
                }
                else
                {
                    // Non-device rates are displayed as percentages
                    return Rate.ToString("0.00") + "%";
                }
            }
        }
    }

    // View model for manual entry of HAI data
    public class ManualHaiEntryViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }

        // Dictionary to store manual inputs for non-device associated infections
        public Dictionary<string, int> ManualCaseCounts { get; set; }
        public Dictionary<string, int> ManualDeviceDays { get; set; }

        public ManualHaiEntryViewModel()
        {
            ManualCaseCounts = new Dictionary<string, int>();
            ManualDeviceDays = new Dictionary<string, int>();

            // Initialize with all the manual entry fields
            var manualEntryFields = new[]
            {
                "PD Peritonitis (rate)",
                "PD Peritonitis (device days)",
                "Superficial incisional (SIP, SIS)",
                "Organ/Space SSI",
                "Deep Incisional Primary (DIP, DIS)",
                "Non-VAP or Pneumonia",
                "PNU1",
                "PNU2",
                "Laboratory confirmed Blood Stream Infections (BSI)",
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

            foreach (var field in manualEntryFields)
            {
                ManualCaseCounts[field] = 0;

                // Only some fields need manual device days
                if (field == "PD Peritonitis (device days)" ||
                    field == "PD Peritonitis (rate)" ||
                    field == "Surgical Site Infections (SSI)")
                {
                    ManualDeviceDays[field] = 0;
                }
            }
        }
    }
}