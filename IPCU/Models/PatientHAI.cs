using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPCU.Models
{
    [Table("tbPatientHAI")]
    public class PatientHAI
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(8)]
        public string HospNum { get; set; }

        public bool HaiStatus { get; set; } = false;

        public int HaiCount { get; set; } = 0;

        [Column(TypeName = "datetime")]
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        // Navigation property to establish the relationship
        [ForeignKey("HospNum")]
        public virtual PatientMaster? Patient { get; set; }
    }
}