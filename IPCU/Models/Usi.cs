using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class Usi
    {
        public int Id { get; set; } // Primary key

        public string Fname { get; set; } // First Name
        public string Lname { get; set; } // Last Name
        public string Mname { get; set; } // Middle Name
        public string HospitalNumber { get; set; } // Foreign Key to Hospital Records
        public DateTime DateOfBirth { get; set; } // Date of Birth

        [Display(Name = "Date Created")]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
        public int Age { get; set; } // Age
        public string UnitWardArea { get; set; } // Unit/Ward/Area
        public string? MainService { get; set; } // Main Service
        public DateTime DateOfEvent { get; set; } // Date of Event
        public string Investigator { get; set; } // Investigator
        public DateTime DateOfAdmission { get; set; } // Date of Admission
                                                      // Disposition Information
        public string Disposition { get; set; } // Mortality, Discharged, Still Admitted, Transferred In, Transferred Out
        public DateTime DispositionDate { get; set; } // Date of Disposition
        public string? DispositionTransfer { get; set; } // Transfer Details

        // Additional Patient Detailsffff
        public string Gender { get; set; } // Male, Femalef
        public string Classification { get; set; } // Pay, Servicef
        public string MDRO { get; set; } // Yes, No
        public string? MDROOrganism { get; set; }

        public string TypeClass { get; set; }
        //start of forms

        //At least one (1) of the following criteria (group 1)
        public bool PatientOrganism { get; set; }
        public bool PatientAbscess { get; set; }
        //Patient has one (1) of the following signs or symptoms: (group 2)
        public bool Fever1 { get; set; }
        public bool LocalizedPain { get; set; }
        //parent of LocalizedPain
        public bool PurulentDrainage { get; set; }
        public bool Organism { get; set; }
        //Patient ≤1year of age has at least one of the following signs or symptoms: (group 3)
        public bool PatienLessthan1year { get; set; }

        public bool Fever2 { get; set; }
        public bool Hypothermia { get; set; }
        public bool Apnea { get; set; }
        public bool Bradycardia { get; set; }
        public bool Lethargy { get; set; }
        public bool Vomiting { get; set; }
        //AND at least one of the following: group4
        public bool PurulentDrainage2 { get; set; }
        public bool Organism2 { get; set; }
        public DateTime? CultureDate { get; set; }
        public string? CultureResults { get; set; }
    }
}
