using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class PretestNonclinical : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PWD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CivilStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AfterCoveringCough = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AfterHandlingMoney = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AfterSneezing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AfterAttendanceTimeIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeforeEating = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AfterUsingBathroom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeforeDrinkingMedication = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AfterSigningDocuments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsedFaceMasks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaskUseReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NeedleStickReportTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InfectionPrevention = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HandRubbingTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IPCGoal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProperMaskUse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestForms", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestForms");
        }
    }
}
