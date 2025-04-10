using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class TCSkillsChecklist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TCSkillsChecklist",
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
                    table.PrimaryKey("PK_TCSkillsChecklist", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TCSkillsChecklist");
        }
    }
}
