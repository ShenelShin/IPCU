using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class vaemoni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Equipment_StorageClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Equipment_StorageClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Hallway_FloorsWallsClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hallway_FloorsWallsClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Hallway_NoObstruction_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hallway_NoObstruction_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Medications_NoExpired_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medications_NoExpired_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Medications_NoLooseNeedles_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medications_NoLooseNeedles_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Medications_VaccinesStored_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medications_VaccinesStored_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Medications_VialsDated_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medications_VialsDated_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Misc_SpecimensLabeled_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Misc_SpecimensLabeled_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Misc_StaffHygienePPE_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Misc_StaffHygienePPE_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PatientExamRoom_AlcoholSanitizers_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientExamRoom_AlcoholSanitizers_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PatientExamRoom_CurtainsClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientExamRoom_CurtainsClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PatientExamRoom_FloorsWallsClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientExamRoom_FloorsWallsClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PatientExamRoom_NoFoodInCareAreas_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientExamRoom_NoFoodInCareAreas_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PatientExamRoom_NoSuppliesUnderSinks_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientExamRoom_NoSuppliesUnderSinks_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PatientExamRoom_PPEAvailable_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientExamRoom_PPEAvailable_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PatientExamRoom_SinkClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientExamRoom_SinkClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PatientExamRoom_SoapDispenser_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientExamRoom_SoapDispenser_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Refrigerator_Dedicated_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Refrigerator_Dedicated_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Refrigerator_TempChecked_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Refrigerator_TempChecked_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SoiledRoom_FloorsWallsClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoiledRoom_FloorsWallsClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SoiledRoom_LinenBagged_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoiledRoom_LinenBagged_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SoiledRoom_NoPatientSupplies_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoiledRoom_NoPatientSupplies_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UtilityRoom_CleanSoiledSeparation_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UtilityRoom_CleanSoiledSeparation_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UtilityRoom_FIFOStocks_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UtilityRoom_FIFOStocks_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UtilityRoom_FloorsWallsClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UtilityRoom_FloorsWallsClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UtilityRoom_NoSuppliesInBathrooms_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UtilityRoom_NoSuppliesInBathrooms_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UtilityRoom_NoSuppliesOnFloor_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UtilityRoom_NoSuppliesOnFloor_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UtilityRoom_NoSuppliesUnderSink_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UtilityRoom_NoSuppliesUnderSink_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UtilityRoom_SterileTraysClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UtilityRoom_SterileTraysClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UtilityRoom_SuppliesFromCeiling_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UtilityRoom_SuppliesFromCeiling_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UtilityRoom_SuppliesNotExpired_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UtilityRoom_SuppliesNotExpired_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WaitingArea_FoodInDesignatedArea_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WaitingArea_FoodInDesignatedArea_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WaitingArea_FurnitureClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WaitingArea_FurnitureClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WaitingArea_TrashDisposed_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WaitingArea_TrashDisposed_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WasteMgmt_BinsClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WasteMgmt_BinsClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WasteMgmt_BinsNotOverfilled_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WasteMgmt_BinsNotOverfilled_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WasteMgmt_ColorBinsAvailable_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WasteMgmt_ColorBinsAvailable_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WasteMgmt_ProperDisposal_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WasteMgmt_ProperDisposal_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WasteMgmt_SharpsLabeled_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WasteMgmt_SharpsLabeled_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WasteMgmt_SharpsNotOverfilled_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WasteMgmt_SharpsNotOverfilled_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkArea_ACClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkArea_ACClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkArea_CeilingClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkArea_CeilingClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkArea_ChartsStored_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkArea_ChartsStored_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkArea_CleanBathroom_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkArea_CleanBathroom_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkArea_CountersClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkArea_CountersClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkArea_DoorsWindowsClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkArea_DoorsWindowsClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkArea_ExhaustFansClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkArea_ExhaustFansClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkArea_FansClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkArea_FansClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkArea_FloorsClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkArea_FloorsClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkArea_NoFoodOrDrinks_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkArea_NoFoodOrDrinks_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WorkArea_WallsClean_FuDate",
                table: "InfectionControlMonitoringForm",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkArea_WallsClean_FuRemarks",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VAEMonitoringChecklist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssessedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateandTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObservedStaff = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadofBed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IfTracheostomy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrcalCare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HandHygiene = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AfterCare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SterileWater = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnsuretoUse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Condensateinthe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntubationKits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CleanandDirty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberofComplaint = table.Column<int>(type: "int", nullable: true),
                    TotalObserved = table.Column<int>(type: "int", nullable: true),
                    ComplianceRate = table.Column<float>(type: "real", nullable: true),
                    Accomplishedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewedandApproved = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VAEMonitoringChecklist", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VAEMonitoringChecklist");

            migrationBuilder.DropColumn(
                name: "Equipment_StorageClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Equipment_StorageClean_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Hallway_FloorsWallsClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Hallway_FloorsWallsClean_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Hallway_NoObstruction_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Hallway_NoObstruction_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Medications_NoExpired_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Medications_NoExpired_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Medications_NoLooseNeedles_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Medications_NoLooseNeedles_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Medications_VaccinesStored_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Medications_VaccinesStored_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Medications_VialsDated_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Medications_VialsDated_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Misc_SpecimensLabeled_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Misc_SpecimensLabeled_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Misc_StaffHygienePPE_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Misc_StaffHygienePPE_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "PatientExamRoom_AlcoholSanitizers_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "PatientExamRoom_AlcoholSanitizers_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "PatientExamRoom_CurtainsClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "PatientExamRoom_CurtainsClean_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "PatientExamRoom_FloorsWallsClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "PatientExamRoom_FloorsWallsClean_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "PatientExamRoom_NoFoodInCareAreas_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "PatientExamRoom_NoFoodInCareAreas_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "PatientExamRoom_NoSuppliesUnderSinks_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "PatientExamRoom_NoSuppliesUnderSinks_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "PatientExamRoom_PPEAvailable_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "PatientExamRoom_PPEAvailable_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "PatientExamRoom_SinkClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "PatientExamRoom_SinkClean_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "PatientExamRoom_SoapDispenser_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "PatientExamRoom_SoapDispenser_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Refrigerator_Dedicated_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Refrigerator_Dedicated_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Refrigerator_TempChecked_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Refrigerator_TempChecked_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "SoiledRoom_FloorsWallsClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "SoiledRoom_FloorsWallsClean_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "SoiledRoom_LinenBagged_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "SoiledRoom_LinenBagged_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "SoiledRoom_NoPatientSupplies_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "SoiledRoom_NoPatientSupplies_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_CleanSoiledSeparation_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_CleanSoiledSeparation_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_FIFOStocks_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_FIFOStocks_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_FloorsWallsClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_FloorsWallsClean_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_NoSuppliesInBathrooms_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_NoSuppliesInBathrooms_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_NoSuppliesOnFloor_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_NoSuppliesOnFloor_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_NoSuppliesUnderSink_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_NoSuppliesUnderSink_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_SterileTraysClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_SterileTraysClean_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_SuppliesFromCeiling_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_SuppliesFromCeiling_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_SuppliesNotExpired_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "UtilityRoom_SuppliesNotExpired_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WaitingArea_FoodInDesignatedArea_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WaitingArea_FoodInDesignatedArea_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WaitingArea_FurnitureClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WaitingArea_FurnitureClean_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WaitingArea_TrashDisposed_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WaitingArea_TrashDisposed_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WasteMgmt_BinsClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WasteMgmt_BinsClean_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WasteMgmt_BinsNotOverfilled_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WasteMgmt_BinsNotOverfilled_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WasteMgmt_ColorBinsAvailable_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WasteMgmt_ColorBinsAvailable_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WasteMgmt_ProperDisposal_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WasteMgmt_ProperDisposal_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WasteMgmt_SharpsLabeled_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WasteMgmt_SharpsLabeled_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WasteMgmt_SharpsNotOverfilled_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WasteMgmt_SharpsNotOverfilled_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_ACClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_ACClean_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_CeilingClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_CeilingClean_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_ChartsStored_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_ChartsStored_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_CleanBathroom_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_CleanBathroom_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_CountersClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_CountersClean_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_DoorsWindowsClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_DoorsWindowsClean_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_ExhaustFansClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_ExhaustFansClean_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_FansClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_FansClean_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_FloorsClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_FloorsClean_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_NoFoodOrDrinks_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_NoFoodOrDrinks_FuRemarks",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_WallsClean_FuDate",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "WorkArea_WallsClean_FuRemarks",
                table: "InfectionControlMonitoringForm");
        }
    }
}
