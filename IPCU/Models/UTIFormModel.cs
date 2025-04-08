namespace IPCU.Models;
using System.ComponentModel.DataAnnotations;


    public class UTIFormModel
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
    [Display(Name = "IUC Insertion Date")]
    public DateTime? IUCInsertDate { get; set; }

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
    public string TypeClass { get; set; } // SUTI 1A(Catheter), SUTI 1B, etc.




    // Catheter presence options
    public bool? CatheterPresent { get; set; }
    public bool? CatheterRemovedBeforeEvent { get; set; }
    // Symptoms checkboxes
    public bool? HasFever { get; set; }
    public bool? HasSuprapubicTenderness { get; set; }
    public bool? HasCostovertebralPain { get; set; }
    public bool? HasUrinaryUrgency { get; set; }
    public bool? HasUrinaryFrequency { get; set; }
    public bool? HasDysuria { get; set; }
    public bool? HasUrineCultureWithTwoSpecies { get; set; }
    // Culture details
    [DataType(DataType.Date)]
    public DateTime? CultureDate { get; set; }
     public string? CultureResults { get; set; }




    // Catheter status
    public bool? SUTI1b_HadCatheterLessThan2Days { get; set; }
    public bool? SUTI1b_NoCatheterInPlace { get; set; }
    // Symptoms checkboxes
    public bool? SUTI1b_Fever { get; set; }
    public bool? SUTI1b_SuprapubicTenderness { get; set; }
    public bool? SUTI1b_CostovertebralPain { get; set; }
    public bool? SUTI1b_UrinaryUrgency { get; set; }
    public bool? SUTI1b_UrinaryFrequency { get; set; }
    public bool? SUTI1b_Dysuria { get; set; }
    // Urine culture species condition checkbox
    public bool? SUTI1b_HasValidUrineCulture { get; set; }
    // Culture details
    [DataType(DataType.Date)]
    public DateTime? SUTI1b_CultureDate { get; set; }
    public string? SUTI1b_CultureResults { get; set; }










    // Age and catheter checkbox
    public bool? SUTI2_IsOneYearOrLess { get; set; }
    public bool? SUTI2_Fever { get; set; }
    public bool? SUTI2_Hypothermia { get; set; }
    public bool? SUTI2_Apnea { get; set; }
    public bool? SUTI2_Bradycardia { get; set; }
    public bool? SUTI2_Lethargy { get; set; }
    public bool? SUTI2_Vomiting { get; set; }
    public bool? SUTI2_SuprapubicTenderness { get; set; }
   
    public bool? SUTI2_HasValidUrineCulture { get; set; }

    [DataType(DataType.Date)]
    public DateTime? SUTI2_CultureDate { get; set; }
    public string? SUTI2_CultureResults { get; set; }






    // No signs or symptoms checkbox
    public bool? ABUTI_NoSymptoms { get; set; }
    // Urine culture checkbox
    public bool? ABUTI_ValidUrineCulture { get; set; }
    // Organism identification checkbox
    public bool? ABUTI_OrganismIdentified { get; set; }
    // Culture details
    [DataType(DataType.Date)]
    public DateTime? ABUTI_CultureDate { get; set; }
    public string? ABUTI_CultureResults { get; set; }

}