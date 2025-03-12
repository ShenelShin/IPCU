using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPCU.Models
{
    public class HandHygieneComplianceSummary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SummaryId { get; set; }

        [Required]
        public DateTime Month { get; set; }

        [Required]
        public string SummaryType { get; set; } // "Area", "NurseArea", "Profession", "Observer"

        [Required]
        public string Category { get; set; } // Area name, profession type, or observer name

        public int TotalCompliantActions { get; set; }

        public int TotalObservedOpportunities { get; set; }

        public decimal ComplianceRate { get; set; }

        [Required]
        public DateTime GeneratedDate { get; set; }
    }
}
