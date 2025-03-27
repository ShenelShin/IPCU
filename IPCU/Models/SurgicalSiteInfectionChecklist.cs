using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class SurgicalSiteInfectionChecklist
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

        [DataType(DataType.Date)]
        [Display(Name = "Date of Operation")]
        public DateTime? DateOfOperation { get; set; }

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

        [Required]
        [Display(Name = "Type & Classification")]
        public string TypeClass { get; set; } // Only SIP DIS SIS DIP



        //uperficialIncisionalSSI(SIP,SIS)*
        public bool InvolvesOnlySkinAndSubcutaneousTissue { get; set; }
        public bool PurulentDrainage { get; set; }
        public bool OrganismsIdentified { get; set; }

        public bool SuperficialIncisionOpened { get; set; }
        public bool NoCulturePerformed { get; set; } // Missing field added!

        public bool PatientHasSymptoms { get; set; } // New flag to indicate symptoms exist

        public bool LocalizedPainOrTenderness { get; set; }
        public bool LocalizedSwelling { get; set; }
        public bool Erythema { get; set; }
        public bool Heat { get; set; }

        public bool DiagnosisByPhysician { get; set; }
        public DateTime? CultureDate { get; set; }
        public string? CultureResults { get; set; }




        //DeepIncisionalSSI(DIP,DIS)


        public bool InvolvesDeepSoftTissues { get; set; }

        public bool IDSTPurulentDrainage { get; set; }
        public bool DeepIncisionOpenedOrDehisces { get; set; }

        public bool IDSTSuperficialIncisionOpened { get; set; }
        public bool IDSTOrganismsIdentified { get; set; }


        public bool IDSTPatientHasSymptoms { get; set; } // Ensures at least one            symptom is present
        public bool Fever { get; set; }
        public bool IDSTLocalizedPainOrTenderness { get; set; }

        public bool AbscessOrEvidenceOfInfection { get; set; }

        public DateTime? IDSTCultureDate { get; set; }
        public string? IDSTCultureResults { get; set; }





        // Organ/SpaceSSI(O/S)

        public bool InvolvesDeeperThanFascialOrMuscleLayers { get; set; }

        public bool OSPurulentDrainage { get; set; }
        public bool OSOrganismsIdentified { get; set; }
        public bool OSAbscessOrEvidenceOfInfection { get; set; }

        public bool MeetsSpecificOrganSpaceInfectionCriteria { get; set; }

        public DateTime? OSCultureDate { get; set; }
        public string? OSCultureResults { get; set; }



    }
}
