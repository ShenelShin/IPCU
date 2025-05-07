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
        [Column(TypeName = "varchar(8)")]
        public string HospNum { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? LastName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? FirstName { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? MiddleName { get; set; }

        [StringLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string? AccountNum { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? HouseStreet { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? Barangay { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? ZipCode { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string? Sex { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string? CivilStatus { get; set; }

        [StringLength(500)]
        [Column(TypeName = "varchar(500)")]
        public string? Occupation { get; set; }

        [StringLength(3)]
        [Column(TypeName = "varchar(3)")]
        public string? Age { get; set; }

        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string? TelNum { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string? PatientStatus { get; set; }

        [StringLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string? OPDNum { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? XRayNum { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? UltraNum { get; set; }

        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string? SSSGSISNum { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? CTNum { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? BloodType { get; set; }

        [Column(TypeName = "bit")]
        public bool SeniorCitizen { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? FileNum { get; set; }

        public int? ControlNum { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? Title { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? PrepaidNum { get; set; }

        [StringLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string? SeqNum { get; set; }

        [Column(TypeName = "bit")]
        public bool SeniorCitizenWithID { get; set; }

        [StringLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string? TemporaryAddress { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? Citizenship { get; set; }

        [StringLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string? MSSClassification { get; set; }

        public DateTime? MSSDiscountValidity { get; set; }

        public DateTime? MSSDiscountExpiry { get; set; }

        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string? MSSNum { get; set; }

        public DateTime? GovDiscountExpiry { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? MRINum { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? CardNumber { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? MedsysHealthRecID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? UserID { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? Password { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? HouseStreetTemp { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? BarangayTemp { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? ZipCodeTemp { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? TelNumTemp { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? VerifiedBy { get; set; }

        public DateTime? VerifiedByDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? CellNum3 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? CellNum2 { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? CityMunicipalityPSGC { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? Country { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? CountryTemp { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? CityMunicipalityPSGCTemp { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? BarangayCaptainTemp { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? cellnum { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? email { get; set; }

        [StringLength(5)]
        [Column(TypeName = "varchar(5)")]
        public string? isbdayestimated { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? NucNum { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? OBNum { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? MammoNum { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? Province { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? SeniorCitizenID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? SCBookletNumber { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? EmpNum { get; set; }

        [Column(TypeName = "bit")]
        public bool? CDHHActive { get; set; }

        [StringLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string? CDHHStatus { get; set; }

        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string? CDHAccountNum { get; set; }

        [StringLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string? CDHRelation { get; set; }

        [Column(TypeName = "bit")]
        public bool? PortalAccess { get; set; }

        [StringLength(3)]
        [Column(TypeName = "varchar(3)")]
        public string? PatientType { get; set; }

        [StringLength(40)]
        [Column(TypeName = "varchar(40)")]
        public string? EmailAddress { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? GSISNum { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? SSSNum { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string? HospPlan { get; set; }

        [StringLength(400)]
        [Column(TypeName = "varchar(400)")]
        public string? Educational { get; set; }

        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string? OP_filenum { get; set; }

        [StringLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string? ForeignAddress { get; set; }

        public DateTime? DateCreated { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? PWDID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? TINNumber { get; set; }

        [StringLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string? SpouseAddress { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? CreatedBy { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? OLD_ACCOUNTNUM { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? Own_PHICMemberNumber { get; set; }

        // Navigation property to Patient records
        public virtual ICollection<Patient> Patients { get; set; }

        // Navigation property to the HAI data
        public virtual PatientHAI? HAIData { get; set; }
    }
}