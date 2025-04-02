using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPCU.Models
{
    public class VentilatorEventChecklist
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
        public DateTime DateOfBirth { get; set; }

        [Required]
        public int Age { get; set; }

        public string UwArea { get; set; }

        public string MainService { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfEvent { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfIntubation { get; set; }

        public string NameOfInvestigator { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfAdmission { get; set; }

        public string? Disposition { get; set; }

        [DataType(DataType.Date)]
        public DateTime DispositionDate { get; set; }

        public string? DispositionTransfer { get; set; }

        public string Gender { get; set; }

        public string Classification { get; set; }

        public bool MDRO { get; set; } // Multi-Drug Resistant Organism (

        public string MDROOrganism { get; set; }

        public string TypeClass { get; set; } // Concatenated values if multiple

        public string VaeRemarks { get; set; }

        public bool Vac1 { get; set; }

        public bool Vac2 { get; set; }

        public bool IVac1 { get; set; }

        public bool IVac2 { get; set; }

        public bool IVac3 { get; set; }

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

    }
}
