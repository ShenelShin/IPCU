using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class PediatricVAEChecklist
    {


        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string Fname { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Lname { get; set; }

        [Display(Name = "Middle Name")]
        public string Mname { get; set; }

        [Required]
        [Display(Name = "Hospital Number")]
        public string HospitalNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }

        public int Age { get; set; }

        [Display(Name = "Unit/Ward/Area")]
        public string UnitWardArea { get; set; }

        [Display(Name = "Main Service")]
        public string MainService { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Event")]
        public DateTime? DateOfEvent { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Intubation")]
        public DateTime? DateOfIntubation { get; set; }
        public string Investigator { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Admission")]
        public DateTime? DateOfAdmission { get; set; }

        [Required]
        public string Disposition { get; set; } // Consider using enum later

        [DataType(DataType.Date)]
        [Display(Name = "Disposition Date")]
        public DateTime? DispositionDate { get; set; }

        [Display(Name = "Disposition Transfer")]
        public string? DispositionTransfer { get; set; }

        [Required]
        public string Gender { get; set; } // Male / Female

        [Required]
        public string Classification { get; set; } // Pay / Service

        [Required]
        public string MDRO { get; set; } // Yes / No
        public string? MDROOrganism { get; set; }


        [Required]
        [Display(Name = "Type & Classification")]
        public string TypeClass { get; set; } // Only PedVAE





        // === New Fields based on the image ===

        [Display(Name = "Daily minimum FiO2 increase ≥ 0.25 (25 points) for ≥ 2 days")]
        public bool FiO2Increase { get; set; }

        [Display(Name = "Daily minimum MAP values ≥ 4 cm H2O for ≥ 2 days")]
        public bool MAPIncrease { get; set; }

        [Display(Name = "Comments/Notes")]
        public string? Comments { get; set; }
    }
}
