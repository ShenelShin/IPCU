using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;
using DocumentFormat.OpenXml.Drawing;

namespace IPCU.Models
{
    public class GIInfectionChecklist
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

        public int Age { get; set; }

        public string UnitWardArea { get; set; }

        [Display(Name = "Main Service")]
        public string MainService { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Event")]
        public DateTime? DateOfEvent { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }


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

        [Required]
        [Display(Name = "Type & Classification")]
        public string TypeClass { get; set; } // CDI, GE, GIT, IAB


        // CDI
        public bool CDItoxin { get; set;  }

        public bool CDIpseudomembranous { get; set; }


        // GE
        public bool GEdiarrhea { get; set; }

        public bool GENausea { get; set; }

        public bool GEVomiting { get; set; }

        public bool GENAbdominal { get; set; }

        public bool GENFever { get; set; }

        public bool GENHeadache { get; set; }

        public bool GENpenteric1 { get; set; }

        public bool GENenteric2 { get; set; }

        public bool GENantibody { get; set; }

        // IAB
        public bool IABorganism { get; set; }

        public bool IABAbscess1 { get; set; }

        public bool IABAbscess2 { get; set; }

        public bool IABFever { get; set; }

        public bool IABHypotension { get; set; }

        public bool IABNausea { get; set; }

        public bool IABVomiting { get; set; }

        public bool IABAbdominal { get; set; }
        
        public bool IABtransaminase { get; set; }

        public bool IABJaundice { get; set; }

        public bool IABOrgintraabdominal { get; set; }

        public bool IABmicrobiologic { get; set; }

        // GIT

        public bool GITAbscess1 { get; set; }

        public bool GITAbscess2 { get; set; }

        public bool GITblood { get; set; }

        public bool GITNausea { get; set; }

        public bool GITVomiting { get; set; }

        public bool GITPainTend { get; set; }

        public bool GITFEVER { get; set; }

        public bool GITOdynophagia { get; set; }

        public bool GITDysphagia { get; set; }

        public bool GITDrain { get; set; }

        public bool GITGram { get; set; }

        public bool GITmicrobiologic { get; set; }

        public bool GITgastrointestinal { get; set; }
    }
}
