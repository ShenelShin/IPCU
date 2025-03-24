using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class SSTInfectionModel
    {
        // Primary Key
        [Key]
        public int SSTID { get; set; }  // You can use int, or change to Guid if preferred

        // Patient info fields
        public string Fname { get; set; }
        public string Mname { get; set; }
        public string Lname { get; set; }
        public string HospitalNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int Age { get; set; }
    public string UnitWardArea { get; set; }
    public string MainService { get; set; }
    public DateTime DateOfEvent { get; set; }
    public string Investigator { get; set; }
    public DateTime DateOfAdmission { get; set; }
    public string Disposition { get; set; }
    public string Gender { get; set; }
    public string Classification { get; set; }
    public bool MDRO { get; set; }
    public string InfectionType { get; set; }
    public string InfectionClassification { get; set; }

        // Infection Details - Burn Infection
        public bool BurnAppearanceChange { get; set; }
        public bool BurnOrganismIdentified { get; set; }
        public string BurnCultureDate { get; set; }
        public string BurnCultureResults { get; set; }

        // Decubitus Ulcer Infection
        public bool DecubitusErythema { get; set; }
        public bool DecubitusTenderness { get; set; }
        public bool DecubitusSwelling { get; set; }
        public bool DecubitusOrganismIdentified { get; set; }
        public string DecubitusCultureDate { get; set; }
        public string DecubitusCultureResults { get; set; }

        // ST-Soft Tissue Infection
        public bool STOrganismIdentified { get; set; }
        public bool STDrainage { get; set; }
        public bool STAbscess { get; set; }
        public string STCultureDate { get; set; }
        public string STCultureResults { get; set; }

        // Skin-Skin Infection
        public bool SkinPurulentDrainage { get; set; }
        public bool SkinVesicles { get; set; }
        public bool SkinPustules { get; set; }
        public bool SkinBoils { get; set; }



        public bool LocalizedPainTenderness { get; set; }
        public bool LocalizedSwelling { get; set; }
        public bool LocalizedErythema { get; set; }
        public bool LocalizedHeat { get; set; }


        public bool OrganismIdentifiedFromAspiration { get; set; }
        public bool MultinucleatedGiantCellsSeen { get; set; }
        public bool DiagnosticAntibodyTiter { get; set; }
        public string SkinCultureDate { get; set; }
        public string SkinCultureResults { get; set; }
    }
}