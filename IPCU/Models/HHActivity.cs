using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class HHActivity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActId { get; set; }  // Primary Key (Auto-increment)

        [ForeignKey("HandHygieneForm")]
        public int HHId { get; set; }  // Foreign Key referencing HandHygieneForm

        [Required, StringLength(255)]
        public string Activity { get; set; }

        [StringLength(255)]
        public string? BeforeHandRub { get; set; }

        [StringLength(255)]
        public string? BeforeHandWash { get; set; }

        [StringLength(255)]
        public string? AfterHandRub { get; set; }

        [StringLength(255)]
        public string? AfterHandWash { get; set; }

        [StringLength(255)]
        public string Gloves { get; set; }

        // Navigation Property - Each activity belongs to one form
        public HandHygieneForm HandHygieneForm { get; set; }
    }
}
