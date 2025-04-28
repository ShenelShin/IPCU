using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class changeTC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "DateOfObservation",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsBathroomFloorScrubbed",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsCleaningSolutionPrepared",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsColorCodedWasteEmptied",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsDoorFrameWiped",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsEquipmentAndCartPrepared",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsFaucetAndHandlesCleaned",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsGlovesRemovedAndHandHygieneDone",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsHandHygieneAfterPPE",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsHandHygieneAndGlovesDone",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsHighTouchAreasWiped",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsInfectiousWasteRemoved",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsLooseDebrisPickedUp",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsMirrorCleaned",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsOtherBathroomSurfacesCleaned",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsPPERemoved",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsProperAttireAndPPEWorn",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsRoomFloorMopped",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsSignageChecked",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsSinkAreaCleaned",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsSpillSoakedWithSolution",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsToiletAndFlushHandlesCleaned",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsUsedClothsDisposed",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsVerticalSurfacesWiped",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsWallsCleaned",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsWasteContainersEmptied",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "IsWindowSillAndWindowCleaned",
                table: "TCSkillsChecklist");

            migrationBuilder.RenameColumn(
                name: "UnitAreaStaffSignature",
                table: "TCSkillsChecklist",
                newName: "TrafficcontrolRestrictedToConstructionWorkersSelection");

            migrationBuilder.RenameColumn(
                name: "RecommendationsOrActions",
                table: "TCSkillsChecklist",
                newName: "TrafficcontrolDoorsAndExitsFreeOfDebrisSelection");

            migrationBuilder.RenameColumn(
                name: "PreCleaningItems",
                table: "TCSkillsChecklist",
                newName: "TelephoneOrMobileNumber");

            migrationBuilder.RenameColumn(
                name: "PostCleaningItems",
                table: "TCSkillsChecklist",
                newName: "SpecificSiteOfActivity");

            migrationBuilder.RenameColumn(
                name: "ObserverName",
                table: "TCSkillsChecklist",
                newName: "ScopeOfWork");

            migrationBuilder.RenameColumn(
                name: "Area",
                table: "TCSkillsChecklist",
                newName: "ProjectareaTrashInAppropriateContainerSelection");

            migrationBuilder.AddColumn<string>(
                name: "AirHandlingUnitRunningComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AirHandlingUnitRunningSelection",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AirhandlingAirExhaustedToAppropriateAreaComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AirhandlingAirExhaustedToAppropriateAreaSelection",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AirhandlingMaintenanceLabelVisibleComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AirhandlingMaintenanceLabelVisibleSelection",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AirhandlingNegativeAirMonitoredComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AirhandlingNegativeAirMonitoredSelection",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AirhandlingWindowsClosedBehindBarrierComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AirhandlingWindowsClosedBehindBarrierSelection",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BarrierDoorsSealedComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BarrierDoorsSealedSelection",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BarrierFloorCleanComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BarrierFloorCleanSelection",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BarrierICRA",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BarrierICRAComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BarrierTapeAdheringComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BarrierTapeAdheringSelection",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BarrierWalkOffMatsComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BarrierWalkOffMatsSelection",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContractorRepresentativeName",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ContractorSign",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DresscodeProtectiveClothingWornComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DresscodeProtectiveClothingWornSelection",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DresscodeWorkersClothingCleanUponExitingComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DresscodeWorkersClothingCleanUponExitingSelection",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EngineeringSign",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EstimatedDuration",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ICPSign",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectNameAndDescription",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectReferenceNumber",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ProjectStartDate",
                table: "TCSkillsChecklist",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectareaAirVentsSealedOrDuctworkCappedComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectareaAirVentsSealedOrDuctworkCappedSelection",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectareaDebrisRemovedInCoveredContainerDailyComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectareaDebrisRemovedInCoveredContainerDailySelection",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectareaDesignatedConstructionRouteOrMapPostedComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectareaDesignatedConstructionRouteOrMapPostedSelection",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectareaHEPAFilteredVacuumOnJobsiteComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectareaHEPAFilteredVacuumOnJobsiteSelection",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectareaRoutineCleaningDoneOnSiteComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectareaRoutineCleaningDoneOnSiteSelection",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProjectareaTrashInAppropriateContainerComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrafficcontrolDoorsAndExitsFreeOfDebrisComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrafficcontrolRestrictedToConstructionWorkersComments",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitAreaRep",
                table: "TCSkillsChecklist",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AirHandlingUnitRunningComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "AirHandlingUnitRunningSelection",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "AirhandlingAirExhaustedToAppropriateAreaComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "AirhandlingAirExhaustedToAppropriateAreaSelection",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "AirhandlingMaintenanceLabelVisibleComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "AirhandlingMaintenanceLabelVisibleSelection",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "AirhandlingNegativeAirMonitoredComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "AirhandlingNegativeAirMonitoredSelection",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "AirhandlingWindowsClosedBehindBarrierComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "AirhandlingWindowsClosedBehindBarrierSelection",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "BarrierDoorsSealedComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "BarrierDoorsSealedSelection",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "BarrierFloorCleanComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "BarrierFloorCleanSelection",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "BarrierICRA",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "BarrierICRAComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "BarrierTapeAdheringComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "BarrierTapeAdheringSelection",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "BarrierWalkOffMatsComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "BarrierWalkOffMatsSelection",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ContractorRepresentativeName",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ContractorSign",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "DresscodeProtectiveClothingWornComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "DresscodeProtectiveClothingWornSelection",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "DresscodeWorkersClothingCleanUponExitingComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "DresscodeWorkersClothingCleanUponExitingSelection",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "EngineeringSign",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "EstimatedDuration",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ICPSign",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ProjectNameAndDescription",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ProjectReferenceNumber",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ProjectStartDate",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ProjectareaAirVentsSealedOrDuctworkCappedComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ProjectareaAirVentsSealedOrDuctworkCappedSelection",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ProjectareaDebrisRemovedInCoveredContainerDailyComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ProjectareaDebrisRemovedInCoveredContainerDailySelection",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ProjectareaDesignatedConstructionRouteOrMapPostedComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ProjectareaDesignatedConstructionRouteOrMapPostedSelection",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ProjectareaHEPAFilteredVacuumOnJobsiteComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ProjectareaHEPAFilteredVacuumOnJobsiteSelection",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ProjectareaRoutineCleaningDoneOnSiteComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ProjectareaRoutineCleaningDoneOnSiteSelection",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ProjectareaTrashInAppropriateContainerComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "TrafficcontrolDoorsAndExitsFreeOfDebrisComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "TrafficcontrolRestrictedToConstructionWorkersComments",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "UnitAreaRep",
                table: "TCSkillsChecklist");

            migrationBuilder.RenameColumn(
                name: "TrafficcontrolRestrictedToConstructionWorkersSelection",
                table: "TCSkillsChecklist",
                newName: "UnitAreaStaffSignature");

            migrationBuilder.RenameColumn(
                name: "TrafficcontrolDoorsAndExitsFreeOfDebrisSelection",
                table: "TCSkillsChecklist",
                newName: "RecommendationsOrActions");

            migrationBuilder.RenameColumn(
                name: "TelephoneOrMobileNumber",
                table: "TCSkillsChecklist",
                newName: "PreCleaningItems");

            migrationBuilder.RenameColumn(
                name: "SpecificSiteOfActivity",
                table: "TCSkillsChecklist",
                newName: "PostCleaningItems");

            migrationBuilder.RenameColumn(
                name: "ScopeOfWork",
                table: "TCSkillsChecklist",
                newName: "ObserverName");

            migrationBuilder.RenameColumn(
                name: "ProjectareaTrashInAppropriateContainerSelection",
                table: "TCSkillsChecklist",
                newName: "Area");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TCSkillsChecklist",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfObservation",
                table: "TCSkillsChecklist",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsBathroomFloorScrubbed",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsCleaningSolutionPrepared",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsColorCodedWasteEmptied",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDoorFrameWiped",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEquipmentAndCartPrepared",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsFaucetAndHandlesCleaned",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsGlovesRemovedAndHandHygieneDone",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHandHygieneAfterPPE",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHandHygieneAndGlovesDone",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHighTouchAreasWiped",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsInfectiousWasteRemoved",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsLooseDebrisPickedUp",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsMirrorCleaned",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOtherBathroomSurfacesCleaned",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPPERemoved",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsProperAttireAndPPEWorn",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRoomFloorMopped",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSignageChecked",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSinkAreaCleaned",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSpillSoakedWithSolution",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsToiletAndFlushHandlesCleaned",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsUsedClothsDisposed",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVerticalSurfacesWiped",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWallsCleaned",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWasteContainersEmptied",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsWindowSillAndWindowCleaned",
                table: "TCSkillsChecklist",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
