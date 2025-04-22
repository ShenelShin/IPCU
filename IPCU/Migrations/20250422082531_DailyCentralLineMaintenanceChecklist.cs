using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class DailyCentralLineMaintenanceChecklist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyCentralLineMaintenanceChecklists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaOrUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAndTimeOfMonitoring = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssessedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InitialPlacement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Implanted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Injection = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dateadministration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Necessityassessed = table.Column<bool>(type: "bit", nullable: false),
                    Injectionsites = table.Column<bool>(type: "bit", nullable: false),
                    Capschanged = table.Column<bool>(type: "bit", nullable: false),
                    Insertionsite = table.Column<bool>(type: "bit", nullable: false),
                    Dressingintact = table.Column<bool>(type: "bit", nullable: false),
                    Dressingchanged = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumCompliant = table.Column<int>(type: "int", nullable: false),
                    TotalObserved = table.Column<int>(type: "int", nullable: false),
                    ComplianceRate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyCentralLineMaintenanceChecklists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MDROrderSheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HospitalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecimenType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMRSA = table.Column<bool>(type: "bit", nullable: false),
                    IsVRE = table.Column<bool>(type: "bit", nullable: false),
                    IsMRSE = table.Column<bool>(type: "bit", nullable: false),
                    IsESBL = table.Column<bool>(type: "bit", nullable: false),
                    IsCRE = table.Column<bool>(type: "bit", nullable: false),
                    IsMDRSpneu = table.Column<bool>(type: "bit", nullable: false),
                    IsMDRGNB = table.Column<bool>(type: "bit", nullable: false),
                    CollectionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExplainSituationToPatient = table.Column<bool>(type: "bit", nullable: false),
                    PlaceInSingleRoom = table.Column<bool>(type: "bit", nullable: false),
                    PutContactSignage = table.Column<bool>(type: "bit", nullable: false),
                    LimitVisitors = table.Column<bool>(type: "bit", nullable: false),
                    DedicateEquipment = table.Column<bool>(type: "bit", nullable: false),
                    EnsureHandRubAvailability = table.Column<bool>(type: "bit", nullable: false),
                    EnsurePPEAvailability = table.Column<bool>(type: "bit", nullable: false),
                    AssignDedicatedStaff = table.Column<bool>(type: "bit", nullable: false),
                    DiscardPPEBeforeExit = table.Column<bool>(type: "bit", nullable: false),
                    DisinfectHorizontalSurfaces = table.Column<bool>(type: "bit", nullable: false),
                    DisinfectHighTouchAreas = table.Column<bool>(type: "bit", nullable: false),
                    TerminalCleaning = table.Column<bool>(type: "bit", nullable: false),
                    LiftPrecautionsWithIPCApproval = table.Column<bool>(type: "bit", nullable: false),
                    PhysicianSignatureAndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NurseSignatureAndDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MDROrderSheets", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DailyCentralLineMaintenanceChecklists");

            migrationBuilder.DropTable(
                name: "MDROrderSheets");
        }
    }
}
