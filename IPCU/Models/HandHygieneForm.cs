using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace IPCU.Models
{
    public class HandHygieneForm
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HHId { get; set; }  // Primary Key (Auto-increment)

        [Required, StringLength(100)]
        public string Area { get; set; }

        [Required, StringLength(255)]
        public string Observer { get; set; }

        [Required]
        public DateTime Date { get; set; }  // DATE
              

        [Required]
        public TimeSpan Time { get; set; }  // TIME type

        [StringLength(255)]
        public string? Name { get; set; }

        [Required, StringLength(50)]
        public string HCWType { get; set; }

        [StringLength(50)]
        public string? RoomType { get; set; }

        public bool Isolation { get; set; }  // BOOLEAN type

        [StringLength(100)]
        public string? IsolationPrecaution { get; set; }

        [Required, StringLength(255)]
        public string ObsvPatientCare { get; set; }

        [Required, StringLength(100)]
        public string ObsvPatientEnvironment { get; set; }

        [StringLength(100)]
        public string? EnvironmentResource { get; set; }

        [Required, StringLength(255)]
        public string ObsvPatientContact { get; set; }

        public int TotalCompliantActions { get; set; }

        public int TotalObservedOpportunities { get; set; }

        public decimal ComplianceRate { get; set; }

        // Navigation Property - One form has many activities
        public List<HHActivity> Activities { get; set; } = new List<HHActivity>();
    }
}
