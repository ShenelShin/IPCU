using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class CardiovascularSystemInfection
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
        public string? MDROOrganism { get; set; }



        [Required]
        [Display(Name = "Type & Classification")]
        public string TypeClass { get; set; } //Only VASC checked










        // General (Adult) Criteria
        [Required]
        [Display(Name = "Patient has organism(s) from extracted arteries or veins identified by aculture or non-culture based microbiologic testing method")]
        public bool OrganismIdentified { get; set; }

        [Required]
        [Display(Name = "Patient has evidence of arterial or venous infection on gross anatomic or histopathologic exam")]
        public bool GrossEvidenceOfInfection { get; set; }

        // Adult Symptoms
       
        [Required]
        public bool Symptom_Fever { get; set; }


        [Required]
        public bool Symptom_Pain { get; set; }

        [Required]
        public bool Symptom_Erythema { get; set; }

        [Required]
        public bool Symptom_HeatAtVascularSite { get; set; }




        [Required]
        [Display(Name = "More than 15 colonies cultured from intravascular cannula tip using semiquantitative culture method)]")]
        public bool MoreThan15Colonies { get; set; }




        // Infant Symptoms
        [Required]
        public bool Infant_Fever { get; set; }

        [Required]
        public bool Infant_Hypothermia { get; set; }

        [Required]
        public bool Infant_Apnea { get; set; }

        [Required]
        public bool Infant_Bradycardia { get; set; }

        [Required]
        public bool Infant_Lethargy { get; set; }

        [Required]
        public bool Infant_Pain { get; set; }

        [Required]
        public bool Infant_Erythema { get; set; }

        [Required]
        public bool Infant_HeatAtVascularSite { get; set; }

        [Required]
        [Display(Name = "More than 15 colonies cultured from intravascular cannula tip using semi quantitative culture method")]
        public bool Infant_MoreThan15Colonies { get; set; }

        // =============================
        // Culture
        // =============================
        [DataType(DataType.Date)]
        [Display(Name = "Culture Date")]
        public DateTime CultureDate { get; set; }

        [Required]
        [Display(Name = "Culture Results")]
        public string CultureResults { get; set; } 
    }

}

