using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace IPCU.Models
{
    public class PostConstruction
    {
        [Key]
        public int Id { get; set; }

        public int ICRAId { get; set; }
        [ForeignKey("ICRAId")]
        public virtual ICRA? ICRA { get; set; }
        public string ProjectReferenceNumber { get; set; }
        public string ProjectNameAndDescription { get; set; }
        public string SpecificSiteOfActivity { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public string EstimatedDuration { get; set; }
        //post construction cleaning
        public string BeforeHoarding { get; set; }
        public string? BeforeHoardingDC { get; set; }
        public string FacilityBased { get; set; }
        public string? FacilityBasedDC { get; set; }
        public string AfterRemoval { get; set; }
        public string? AfterRemovalDC { get; set; }
        public string WhereRequired { get; set; }
        public string? WhereRequiredlDC { get; set; }
        //finishes
        public string AreaIs { get; set; }
        public string? AreaIsDC { get; set; }
        public string IntegrityofWalls { get; set; }
        public string? IntegrityofWallsDC { get; set; }
        public string SurfaceinPatient { get; set; }
        public string? SurfaceinPatientDC { get; set; }
        public string AreaSurfaces { get; set; }
        public string? AreaSurfacesDC { get; set; }

        //Infrastructure
        public string IfPlumbinghasbeenAffected { get; set; }
        public string? IfPlumbinghasbeenAffectedDC { get; set; }
        public string PlumbingifAffected { get; set; }
        public string? PlumbingifAffectedDC { get; set; }
        public string CorrectHandWashing { get; set; }
        public string? CorrectHandWashingDC { get; set; }
        public string FaucetAerators { get; set; }
        public string? FaucetAeratorsDC { get; set; }
        public string CeilingTiles { get; set; }
        public string? CeilingTilesDC { get; set; }
        public string HVACSystems { get; set; }
        public string? HVACSystemsDC { get; set; }
        public string CorrectRoomPressurization { get; set; }
        public string? CorrectRoomPressurizationDC { get; set; }
        public string AllMechanicalSpaces { get; set; }
        public string? AllMechanicalSpacesDC { get; set; }
        public DateTime? DateCompleted { get; set; }

        //signators
        public string? ContractorSign { get; set; }
        public string? EngineeringSign { get; set; }
        public string? ICPSign { get; set; }
        public string? UnitAreaRep { get; set; }
    }
}
