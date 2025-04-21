using System;
using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class VAEMonitoringChecklist
    {
        [Key]
        public int Id { get; set; }
        public string AreaUnit { get; set; }
        public string AssessedBy { get; set; }
        public DateTime DateandTime { get; set; }
        public string PatientName { get; set; }
        public string Bed { get; set; }
        public string ObservedStaff { get; set; }
        public string HeadofBed { get; set; }
        public string IfTracheostomy { get; set; }
        public string OrcalCare { get; set; }
        public string HandHygiene { get; set; }
        public string AfterCare { get; set; }
        public string SterileWater { get; set; }
        public string EnsuretoUse { get; set; }
        public string Condensateinthe { get; set; }
        public string IntubationKits { get; set; }
        public string CleanandDirty { get; set; }
        public string? Remarks { get; set; }
        public int? NumberofComplaint { get; set; }
        public int? TotalObserved { get; set; }
        public float? ComplianceRate { get; set; }
        public string? Accomplishedby { get; set; }
        public string? ReviewedandApproved { get; set; }

    }

}
