using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPCU.Models
{
    [Table("tbpatient")]
    public class Patient
    {
        [StringLength(8)]
        public string HospNum { get; set; }

        [Key]
        [StringLength(10)]
        public string IdNum { get; set; }

        [StringLength(30)]
        public string AdmType { get; set; }

        [StringLength(30)]
        public string AdmLocation { get; set; }

        public DateTime? AdmDate { get; set; }

        [StringLength(3)]
        public string Age { get; set; }

        [StringLength(8)]
        public string RoomID { get; set; }

        [StringLength(20)]
        public string? DeathType { get; set; }

        [StringLength(30)]
        public string EmailAddress { get; set; }

        public DateTime? DeathDate { get; set; }

        public DateTime? DateCreated { get; set; }
    }
}
