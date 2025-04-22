using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class AddSSIPPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SSIP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientDiagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HospitalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Operation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surgeon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nurse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ORLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SexGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AntimicrobialProphylaxisYes = table.Column<bool>(type: "bit", nullable: false),
                    AntimicrobialProphylaxisReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurgicalHandScrubYes = table.Column<bool>(type: "bit", nullable: false),
                    SurgicalHandScrubReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppropriatePPEYes = table.Column<bool>(type: "bit", nullable: false),
                    AppropriatePPEReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientCoveredYes = table.Column<bool>(type: "bit", nullable: false),
                    PatientCoveredReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrapeSeparatesInstrumentsYes = table.Column<bool>(type: "bit", nullable: false),
                    DrapeSeparatesInstrumentsReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProperVentilationYes = table.Column<bool>(type: "bit", nullable: false),
                    ProperVentilationReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurfacesCleanedYes = table.Column<bool>(type: "bit", nullable: false),
                    SurfacesCleanedReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstrumentsSterilizedYes = table.Column<bool>(type: "bit", nullable: false),
                    InstrumentsSterilizedReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FootTrafficMinimizedYes = table.Column<bool>(type: "bit", nullable: false),
                    FootTrafficMinimizedReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkinCleanedPriorYes = table.Column<bool>(type: "bit", nullable: false),
                    SkinCleanedPriorReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HairRemovedYes = table.Column<bool>(type: "bit", nullable: false),
                    HairRemovedReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGlucoseMonitoredYes = table.Column<bool>(type: "bit", nullable: false),
                    BloodGlucoseMonitoredReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostOpWoundCareYes = table.Column<bool>(type: "bit", nullable: false),
                    PostOpWoundCareReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    procedurenotes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSIP", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SSIP");
        }
    }
}
