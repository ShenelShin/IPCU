using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class TCCons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstructionSites");

            migrationBuilder.CreateTable(
                name: "TCSkillsChecklistReal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObserverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEquipmentAndCartPrepared = table.Column<bool>(type: "bit", nullable: false),
                    IsCleaningSolutionPrepared = table.Column<bool>(type: "bit", nullable: false),
                    IsProperAttireAndPPEWorn = table.Column<bool>(type: "bit", nullable: false),
                    IsHandHygieneAndGlovesDone = table.Column<bool>(type: "bit", nullable: false),
                    IsSignageChecked = table.Column<bool>(type: "bit", nullable: false),
                    IsSpillSoakedWithSolution = table.Column<bool>(type: "bit", nullable: false),
                    IsWallsCleaned = table.Column<bool>(type: "bit", nullable: false),
                    IsDoorFrameWiped = table.Column<bool>(type: "bit", nullable: false),
                    IsWindowSillAndWindowCleaned = table.Column<bool>(type: "bit", nullable: false),
                    IsHighTouchAreasWiped = table.Column<bool>(type: "bit", nullable: false),
                    IsVerticalSurfacesWiped = table.Column<bool>(type: "bit", nullable: false),
                    IsLooseDebrisPickedUp = table.Column<bool>(type: "bit", nullable: false),
                    IsRoomFloorMopped = table.Column<bool>(type: "bit", nullable: false),
                    IsUsedClothsDisposed = table.Column<bool>(type: "bit", nullable: false),
                    IsWasteContainersEmptied = table.Column<bool>(type: "bit", nullable: false),
                    IsInfectiousWasteRemoved = table.Column<bool>(type: "bit", nullable: false),
                    IsMirrorCleaned = table.Column<bool>(type: "bit", nullable: false),
                    IsSinkAreaCleaned = table.Column<bool>(type: "bit", nullable: false),
                    IsFaucetAndHandlesCleaned = table.Column<bool>(type: "bit", nullable: false),
                    IsToiletAndFlushHandlesCleaned = table.Column<bool>(type: "bit", nullable: false),
                    IsOtherBathroomSurfacesCleaned = table.Column<bool>(type: "bit", nullable: false),
                    IsBathroomFloorScrubbed = table.Column<bool>(type: "bit", nullable: false),
                    IsColorCodedWasteEmptied = table.Column<bool>(type: "bit", nullable: false),
                    IsPPERemoved = table.Column<bool>(type: "bit", nullable: false),
                    IsHandHygieneAfterPPE = table.Column<bool>(type: "bit", nullable: false),
                    IsGlovesRemovedAndHandHygieneDone = table.Column<bool>(type: "bit", nullable: false),
                    PreCleaningItems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCleaningItems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecommendationsOrActions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitAreaStaffSignature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfObservation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCSkillsChecklistReal", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TCSkillsChecklistReal");

            migrationBuilder.CreateTable(
                name: "ConstructionSites",
                columns: table => new
                {
                    CSID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AirHandlingUnitRunningComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirHandlingUnitRunningSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirhandlingAirExhaustedToAppropriateAreaComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirhandlingAirExhaustedToAppropriateAreaSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirhandlingMaintenanceLabelVisibleComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirhandlingMaintenanceLabelVisibleSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirhandlingNegativeAirMonitoredComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirhandlingNegativeAirMonitoredSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirhandlingWindowsClosedBehindBarrierComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirhandlingWindowsClosedBehindBarrierSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarrierDoorsSealedComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarrierDoorsSealedSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarrierFloorCleanComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarrierFloorCleanSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarrierICRA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarrierICRAComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarrierTapeAdheringComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarrierTapeAdheringSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarrierWalkOffMatsComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarrierWalkOffMatsSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractorRepresentativeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractorSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DresscodeProtectiveClothingWornComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DresscodeProtectiveClothingWornSelection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DresscodeWorkersClothingCleanUponExitingComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DresscodeWorkersClothingCleanUponExitingSelection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineeringSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimatedDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ICPSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectNameAndDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProjectareaAirVentsSealedOrDuctworkCappedComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaAirVentsSealedOrDuctworkCappedSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectareaDebrisRemovedInCoveredContainerDailyComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaDebrisRemovedInCoveredContainerDailySelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectareaDesignatedConstructionRouteOrMapPostedComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaDesignatedConstructionRouteOrMapPostedSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectareaHEPAFilteredVacuumOnJobsiteComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaHEPAFilteredVacuumOnJobsiteSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectareaRoutineCleaningDoneOnSiteComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaRoutineCleaningDoneOnSiteSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectareaTrashInAppropriateContainerComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaTrashInAppropriateContainerSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScopeOfWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecificSiteOfActivity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneOrMobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrafficcontrolDoorsAndExitsFreeOfDebrisComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrafficcontrolDoorsAndExitsFreeOfDebrisSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrafficcontrolRestrictedToConstructionWorkersComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrafficcontrolRestrictedToConstructionWorkersSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitAreaRep = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionSites", x => x.CSID);
                });
        }
    }
}
