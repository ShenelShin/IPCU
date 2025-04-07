using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class ConstructionSites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConstructionSites",
                columns: table => new
                {
                    CSID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectNameAndDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractorRepresentativeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneOrMobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecificSiteOfActivity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScopeOfWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstimatedDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarrierICRA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarrierICRAComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarrierDoorsSealedSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarrierDoorsSealedComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarrierFloorCleanSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarrierFloorCleanComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarrierWalkOffMatsSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarrierWalkOffMatsComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarrierTapeAdheringSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarrierTapeAdheringComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirhandlingWindowsClosedBehindBarrierSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirhandlingWindowsClosedBehindBarrierComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirhandlingNegativeAirMonitoredSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirhandlingNegativeAirMonitoredComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirHandlingUnitRunningSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirHandlingUnitRunningComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirhandlingMaintenanceLabelVisibleSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirhandlingMaintenanceLabelVisibleComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirhandlingAirExhaustedToAppropriateAreaSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirhandlingAirExhaustedToAppropriateAreaComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaHEPAFilteredVacuumOnJobsiteSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectareaHEPAFilteredVacuumOnJobsiteComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaDebrisRemovedInCoveredContainerDailySelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectareaDebrisRemovedInCoveredContainerDailyComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaDesignatedConstructionRouteOrMapPostedSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectareaDesignatedConstructionRouteOrMapPostedComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaTrashInAppropriateContainerSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectareaTrashInAppropriateContainerComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaRoutineCleaningDoneOnSiteSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectareaRoutineCleaningDoneOnSiteComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaAirVentsSealedOrDuctworkCappedSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectareaAirVentsSealedOrDuctworkCappedComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrafficcontrolRestrictedToConstructionWorkersSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrafficcontrolRestrictedToConstructionWorkersComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrafficcontrolDoorsAndExitsFreeOfDebrisSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrafficcontrolDoorsAndExitsFreeOfDebrisComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DresscodeProtectiveClothingWornSelection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DresscodeProtectiveClothingWornComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DresscodeWorkersClothingCleanUponExitingSelection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DresscodeWorkersClothingCleanUponExitingComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractorSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineeringSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ICPSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitAreaRep = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionSites", x => x.CSID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConstructionSites");
        }
    }
}
