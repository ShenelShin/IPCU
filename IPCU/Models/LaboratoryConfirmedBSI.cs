using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class LaboratoryConfirmedBSI
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

        public int Age { get; set; }

        [Display(Name = "Unit/Ward/Area")]
        public string UnitWardArea { get; set; }

        [Display(Name = "Main Service")]
        public string MainService { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Event")]
        public DateTime? DateOfEvent { get; set; }

        public string Investigator { get; set; }

       public string? centralline { get; set; }


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


        // LCBI 1 Criteria
        [Display(Name = "Identified from one or more blood specimens obtained by a culture")]
        public bool IdentifiedByCulture { get; set; }

        [Display(Name = "Identified to the genus or species level by non-culture based microbiologic testing")]
        public bool IdentifiedByNonCulture { get; set; }

        [Required]
        [Display(Name = "Organism(s) identified in blood is not related to an infection at another site")]
        public bool OrganismNotFromAnotherSite { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Culture Date")]
        public DateTime? CultureDate { get; set; }

        [Display(Name = "Culture Results")]
        public string? CultureResults { get; set; }



        // LCBI 2 Criteria
        [Display(Name = "Fever (>38°C)")]
        public bool Fever { get; set; }

        [Display(Name = "Hypotension")]
        public bool Hypotension { get; set; }

        [Display(Name = "Chills")]
        public bool Chills { get; set; }

        [Display(Name = "Organism(s) identified in blood is not related to an infection at another site")]
        public bool lcbi2OrganismNotFromAnotherSite { get; set; }

        [Display(Name = "The same organism is identified by a culture from two or more blood specimens collected on separate occasions")]
        public bool TwoOrMorePositiveCultures { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Culture Date")]
        public DateTime? lcbi2CultureDate { get; set; }

        [Display(Name = "Culture Results")]
        public string? lcbi2CultureResults { get; set; }




        // LCBI 3 Criteria
        [Display(Name = "Fever (>38°C)")]
        public bool lcbi3Fever { get; set; }

        [Display(Name = "Hypothermia")]
        public bool Hypothermia { get; set; }

        [Display(Name = "Apnea")]
        public bool Apnea { get; set; }

        [Display(Name = "Bradycardia")]
        public bool Bradycardia { get; set; }

        [Display(Name = "Organism(s) identified in blood is not related to an infection at another site")]
        public bool lcbi3OrganismNotFromAnotherSite { get; set; }

        [Display(Name = "The same organism is identified by a culture from two or more blood specimens collected on separate occasions")]
        public bool lcbi3TwoOrMorePositiveCultures { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Culture Date")]
        public DateTime? lcbi3CultureDate { get; set; }

        [Display(Name = "Culture Results")]
        public string? lcbi3CultureResults { get; set; }


        //MBI-LCBI1

        [Display(Name = "Identified from one or more blood specimens obtained by a culture")]
        public bool mbilcbi1IdentifiedByCulture { get; set; }

        [Display(Name = "Identified to the genus or species level by non-culture based microbiologic testing")]
        public bool mbilcbi1IdentifiedByNonCulture { get; set; }


        [Display(Name = "ONLY intestinal organisms from MBI organism list are identified")]
        public bool OnlyIntestinalOrganisms { get; set; }

        // Additional conditions
        [Display(Name = "Grade III or IV gastrointestinal graft versus host disease")]
        public bool mbilcbi1GraftVsHostDisease { get; set; }

        [Display(Name = "≥1-liter diarrhea in a 24-hour period (or ≥20 mL/kg in a 24-hour period for patients <18 years of age)")]
        public bool mbilcbi1Diarrhea { get; set; }

        [Display(Name = "Neutropenic (ANC and/or WBC values <500 cells/mm3 collected within 7-day period)")]
        public bool mbilcbi1Neutropenic { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Culture Date")]
        public DateTime? mbilcbi1CultureDate { get; set; }

        [Display(Name = "Culture Results")]
        public string? mbilcbi1CultureResults { get; set; }






        //mbi-lcbi2

        [Display(Name = "Viridans Group ONLY Streptococcus and/or Rothia spp. alone but no other organisms")]
        public bool ViridansGroupOnly { get; set; }

        // Additional conditions
        [Display(Name = "Grade III or IV gastrointestinal graft versus host disease")]
        public bool mbilcbi2GraftVsHostDisease { get; set; }

        [Display(Name = "≥1-liter diarrhea in a 24-hour period (or ≥20 mL/kg in a 24-hour period for patients <18 years of age)")]
        public bool mbilcbi2Diarrhea { get; set; }

        [Display(Name = "Neutropenic (ANC and/or WBC values <500 cells/mm3 collected within 7-day period)")]
        public bool mbilcbi2Neutropenic { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Culture Date")]
        public DateTime? mbilcbi2CultureDate { get; set; }

        [Display(Name = "Culture Results")]
        public string? mbilcbi2CultureResults { get; set; }



        //mbi-lcbi3

        [Display(Name = "Viridans Group ONLY Streptococcus and/or Rothia spp. alone but no other organisms")]
        public bool mbilcbi3ViridansGroupOnly { get; set; }

        // Additional conditions
        [Display(Name = "Grade III or IV gastrointestinal graft versus host disease")]
        public bool mbilcbi3GraftVsHostDisease { get; set; }

        [Display(Name = "≥1-liter diarrhea in a 24-hour period (or ≥20 mL/kg in a 24-hour period for patients <18 years of age)")]
        public bool mbilcbi3Diarrhea { get; set; }

        [Display(Name = "Neutropenic (ANC and/or WBC values <500 cells/mm3 collected within 7-day period)")]
        public bool mbilcbi3Neutropenic { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Culture Date")]
        public DateTime? mbilcbi3CultureDate { get; set; }

        [Display(Name = "Culture Results")]
        public string? mbilcbi3CultureResults { get; set; }

    }
}
