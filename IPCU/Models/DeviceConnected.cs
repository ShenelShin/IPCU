using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPCU.Models
{
    [Table("DeviceConnected")]
    public class DeviceConnected
    {
        [Key]
        [StringLength(36)]
        public string DeviceId { get; set; }

        [Required]
        [StringLength(40)]
        public string DeviceType { get; set; } // CL, IUC, MV

        [Column(TypeName = "date")]
        public DateTime DeviceInsert { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DeviceRemove { get; set; }

        [Required]
        [StringLength(8)]
        public string HospNum { get; set; }

        [ForeignKey("HospNum")]
        public virtual PatientMaster PatientMaster { get; set; }
    }
}