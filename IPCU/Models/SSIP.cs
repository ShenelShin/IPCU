namespace IPCU.Models
{
    public class SSIP
    {
        public int Id { get; set; }
        public string PatientLastName { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientMiddleName { get; set; }

        public string PatientDiagnosis { get; set; }

        public string HospitalNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? Age { get; set; }

        public string Operation { get; set; }

        public string Surgeon { get; set; }

        public string Nurse { get; set; }

        public string ORLocation { get; set; }

        public string SexGender { get; set; } // Expected values: "Male", "Female"

        public string Classification { get; set; } // Expected values: "Pay", "Service"





        // Surgical Site Bundle Checklist
        public bool AntimicrobialProphylaxisYes { get; set; }
        public string? AntimicrobialProphylaxisReminder { get; set; }

        public bool SurgicalHandScrubYes { get; set; }
        public string? SurgicalHandScrubReminder { get; set; }

        public bool AppropriatePPEYes { get; set; }
        public string? AppropriatePPEReminder { get; set; }

        public bool PatientCoveredYes { get; set; }
        public string? PatientCoveredReminder { get; set; }

        public bool DrapeSeparatesInstrumentsYes { get; set; }
        public string? DrapeSeparatesInstrumentsReminder { get; set; }

        public bool ProperVentilationYes { get; set; }
        public string? ProperVentilationReminder { get; set; }

        public bool SurfacesCleanedYes { get; set; }
        public string? SurfacesCleanedReminder { get; set; }

        public bool InstrumentsSterilizedYes { get; set; }
        public string? InstrumentsSterilizedReminder { get; set; }

        public bool FootTrafficMinimizedYes { get; set; }
        public string? FootTrafficMinimizedReminder { get; set; }

        public bool SkinCleanedPriorYes { get; set; }
        public string? SkinCleanedPriorReminder { get; set; }

        public bool HairRemovedYes { get; set; }
        public string? HairRemovedReminder { get; set; }

        public bool BloodGlucoseMonitoredYes { get; set; }
        public string? BloodGlucoseMonitoredReminder { get; set; }

        public bool PostOpWoundCareYes { get; set; }
        public string? PostOpWoundCareReminder { get; set; }


        public string? procedurenotes { get; set; }
    }
}
