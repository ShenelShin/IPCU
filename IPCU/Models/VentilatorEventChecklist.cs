using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPCU.Models
{
    public class VentilatorEventChecklist
    {

        // Patient Info
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

        public string UnitWardArea { get; set; }

        [Display(Name = "Main Service")]
        public string MainService { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Event")]
        public DateTime? DateOfEvent { get; set; }

        public string Investigator { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Admission")]
        public DateTime? DateOfAdmission { get; set; }

        [Required]
        public string Disposition { get; set; } // Enum could be used (Mortality, Discharged, Still Admitted, etc.)

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

        [Display(Name = "MDRO Organism")]
        public string? MDROOrganism { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = " Date of Intubation:")]
        public DateTime? DateofIntubation { get; set; }

        [Required]
        [Display(Name = "Type & Classification")]
        public string TypeClass { get; set; }


        // VAC
        public bool Vac1 { get; set; }

        public bool Vac2 { get; set; }


        // IVAC
        public bool IVac1 { get; set; }

        public bool IVac2 { get; set; }

        public bool IVac3 { get; set; }


        // PVAP
        public bool Pvap1Endo { get; set; }

        public bool Pvap1Lung { get; set; }

        public bool Pvap1Bronch { get; set; }

        public bool Pvap1Specimen { get; set; }


        public bool Pvap2Sputum { get; set; }

        public bool Pvap2Endo { get; set; }

        public bool Pvap2Lung { get; set; }

        public bool Pvap2Bronch { get; set; }

        public bool Pvap2Specimen { get; set; }


        public bool Pvap3Organism { get; set; }

        public bool Pvap3Lung { get; set; }

        public bool Pvap3Legionella { get; set; }

        public bool Pvap3Viral { get; set; }



        [DataType(DataType.Date)]
        public DateTime? PvapCultureDate { get; set; }

        public string PvapResult { get; set; }

        public string VaeRemarks { get; set; }

    }
}
