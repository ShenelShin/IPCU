using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class postconstruction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostConstruction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectNameAndDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecificSiteOfActivity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstimatedDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeforeHoarding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeforeHoardingDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BeforeHoardingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FacilityBased = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityBasedDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityBasedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AfterRemoval = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AfterRemovalDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AfterRemovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WhereRequired = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhereRequiredlDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhereRequiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AreaIs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaIsDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaIsDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IntegrityofWalls = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntegrityofWallsDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntegrityofWallsDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SurfaceinPatient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurfaceinPatientDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurfaceinPatientDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AreaSurfaces = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IfPlumbinghasbeenAffected = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IfPlumbinghasbeenAffectedDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfPlumbinghasbeenAffectedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlumbingifAffected = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlumbingifAffectedDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlumbingifAffectedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CorrectHandWashing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectHandWashingDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectHandWashingDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FaucetAerators = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaucetAeratorsDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaucetAeratorsDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CeilingTiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CeilingTilesDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CeilingTilesDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HVACSystems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HVACSystemsDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HVACSystemsDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CorrectRoomPressurization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectRoomPressurizationDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectRoomPressurizationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AllMechanicalSpaces = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllMechanicalSpacesDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllMechanicalSpacesDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractorSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineeringSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ICPSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitAreaRep = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostConstruction", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostConstruction");
        }
    }
}
