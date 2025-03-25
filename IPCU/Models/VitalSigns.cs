using IPCU.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace IPCU.Models
{
    public class VitalSigns
    {
        [Key]
        public int VitalId { get; set; }
        [Required]
        public string VitalSign { get; set; }
        [Required]
        public string VitalSignValue { get; set; }
        [Required]
        public DateTime VitalSignDate { get; set; }
        [Required]
        [StringLength(8)]
        public string HospNum { get; set; }
        [ForeignKey("HospNum")]
        public virtual PatientMaster PatientMaster { get; set; }
    }
}