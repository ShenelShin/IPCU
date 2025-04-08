using System.ComponentModel.DataAnnotations;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace IPCU.Models
{
    public class PostConstruction
    {
        [Key]
        public int Id { get; set; }
        public string ProjectReferenceNumber { get; set; }
        public string ProjectNameAndDescription { get; set; }
        public string SpecificSiteOfActivity { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public string EstimatedDuration { get; set; }
        //post construction cleaning
        public string BeforeHoarding { get; set; }
        public string? BeforeHoardingDC { get; set; }
        public DateTime? BeforeHoardingDate { get; set; }

        public string FacilityBased { get; set; }
        public string? FacilityBasedDC { get; set; }
        public DateTime? FacilityBasedDate { get; set; }
        public string AfterRemoval { get; set; }
        public string? AfterRemovalDC { get; set; }
        public DateTime? AfterRemovalDate { get; set; }
        public string WhereRequired { get; set; }
        public string? WhereRequiredlDC { get; set; }
        public DateTime? WhereRequiredDate { get; set; }
        //finishes
        public string AreaIs { get; set; }
        public string? AreaIsDC { get; set; }
        public DateTime? AreaIsDate { get; set; }
        public string IntegrityofWalls { get; set; }
        public string? IntegrityofWallsDC { get; set; }
        public DateTime? IntegrityofWallsDate { get; set; }
        public string SurfaceinPatient { get; set; }
        public string? SurfaceinPatientDC { get; set; }
        public DateTime? SurfaceinPatientDate { get; set; }
        public string AreaSurfaces { get; set; }
        public string? AreaSurfacesDC { get; set; }
        public DateTime? AreaSurfacesDate { get; set; }

        //Infrastructure
        public string IfPlumbinghasbeenAffected { get; set; }
        public string? IfPlumbinghasbeenAffectedDC { get; set; }
        public DateTime? IfPlumbinghasbeenAffectedDate { get; set; }
        public string PlumbingifAffected { get; set; }
        public string? PlumbingifAffectedDC { get; set; }
        public DateTime? PlumbingifAffectedDate { get; set; }
        public string CorrectHandWashing { get; set; }
        public string? CorrectHandWashingDC { get; set; }
        public DateTime? CorrectHandWashingDate { get; set; }
        public string FaucetAerators { get; set; }
        public string? FaucetAeratorsDC { get; set; }
        public DateTime? FaucetAeratorsDate { get; set; }
        public string CeilingTiles { get; set; }
        public string? CeilingTilesDC { get; set; }
        public DateTime? CeilingTilesDate { get; set; }
        public string HVACSystems { get; set; }
        public string? HVACSystemsDC { get; set; }
        public DateTime? HVACSystemsDate { get; set; }
        public string CorrectRoomPressurization { get; set; }
        public string? CorrectRoomPressurizationDC { get; set; }
        public DateTime? CorrectRoomPressurizationDate { get; set; }
        public string AllMechanicalSpaces { get; set; }
        public string? AllMechanicalSpacesDC { get; set; }
        public DateTime? AllMechanicalSpacesDate { get; set; }
        //signators
        public string? ContractorSign { get; set; }
        public string? EngineeringSign { get; set; }
        public string? ICPSign { get; set; }
        public string? UnitAreaRep { get; set; }
    }
}
