using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class DailyCentralLineMaintenanceChecklist
    {

        // Input fields sa taas
        [Key]
        public int Id { get; set; }

        [Display(Name = "AREA/UNIT: ")]
        public string AreaOrUnit { get; set; }
        [Display(Name = "DATE AND TIME OF MONITORING")]
        public DateTime DateAndTimeOfMonitoring { get; set; }
        [Display(Name = "ASSESSED BY: ")]
        public string AssessedBy { get; set; }

        // Patient
        [Display(Name = "Patient: ")]
        public string Patient { get; set; }

        [Display(Name = "Bed: ")]
        public string Bed { get; set; }

        [Display(Name = "Date of initial line placement: ")]
        public DateTime InitialPlacement { get; set; }

        [Display(Name = "Date implanted port accessed: ")]
        public DateTime Implanted { get; set; }

        [Display(Name = "Date injection caps last changed: ")]
        public DateTime Injection { get; set; }

        [Display(Name = "Date administration set and add-on devices last changed: ")]
        public DateTime Dateadministration { get; set; }

        // Checkbox

        public bool Necessityassessed { get; set; }
        public bool Injectionsites { get; set; }
        public bool Capschanged { get; set; }
        public bool Insertionsite { get; set; }
        public bool Dressingintact { get; set; }
        public bool Dressingchanged { get; set; }

        // Sa baba Remarks etc..
        [Display(Name = "REMARKS/Other Observations")]
        public string? Remarks { get; set; }

        [Display(Name = "NUMBER OF COMPLIANT ACTION")]
        public int? NumCompliant { get; set; }

        [Display(Name = "TOTAL OBSERVED")]
        public int? TotalObserved { get; set; }

        [Display(Name = "COMPLIANCE RATE (%)")]
        public int? ComplianceRate { get; set; }
    }
}
