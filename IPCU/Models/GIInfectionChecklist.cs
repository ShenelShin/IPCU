using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;
using DocumentFormat.OpenXml.Drawing;

namespace IPCU.Models
{
    public class GIInfectionChecklist
    {

        [Key]
        public int Id { get; set; } // Primary Key

        [Required]
        public string FName { get; set; }

        public string MName { get; set; }

        [Required]
        public string LName { get; set; }

        [Required]
        public string HospNum { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfAdmission { get; set; }

        public string? Disposition { get; set; }

        [DataType(DataType.Date)]
        public DateTime DispositionDate { get; set; }

        public string? DispositionArea { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public int Age { get; set; }

        public string Gender { get; set; }

        public string UnitWardArea { get; set; }

        public string MainService { get; set; }

        public DateTime DateOfEvent { get; set; }

        public string NameOfInvestigator { get; set; }

        public string Classification { get; set; }
        public bool MDRO { get; set; } // Multi-Drug Resistant Organism

        public string TypeClassification { get; set; }

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
