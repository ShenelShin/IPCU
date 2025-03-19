using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPCU.Models
{
    [Table("tbmaster")]
    public class PatientMaster
    {
        [Key]
        [StringLength(8)]
        public string HospNum { get; set; }

        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [StringLength(8)]
        public string AccountNum { get; set; }

        [StringLength(1)]
        public string Sex { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        [StringLength(1)]
        public string CivilStatus { get; set; }

        [StringLength(3)]
        public string Age { get; set; }

        [StringLength(1)]
        public string PatientStatus { get; set; }

        public bool SeniorCitizenWithID { get; set; }

        [StringLength(20)]
        public string cellnum { get; set; }

        [StringLength(20)]
        public string? EmpNum { get; set; }

        [StringLength(3)]
        public string PatientType { get; set; }

        [StringLength(40)]
        public string EmailAddress { get; set; }
    }
}