using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class Insertion1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Insertion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientDiagnosis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HospitalNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonforInsertion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcedureOperator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberofLumens = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcedureLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatheterType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Optimal = table.Column<bool>(type: "bit", nullable: false),
                    ExplainWhyAlternate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Left = table.Column<bool>(type: "bit", nullable: false),
                    Right = table.Column<bool>(type: "bit", nullable: false),
                    AnatomyIs = table.Column<bool>(type: "bit", nullable: false),
                    ChestWall = table.Column<bool>(type: "bit", nullable: false),
                    COPD = table.Column<bool>(type: "bit", nullable: false),
                    Emergency = table.Column<bool>(type: "bit", nullable: false),
                    Anesthesiologist = table.Column<bool>(type: "bit", nullable: false),
                    Coagulopathy = table.Column<bool>(type: "bit", nullable: false),
                    Dialysis = table.Column<bool>(type: "bit", nullable: false),
                    OperatorTraining = table.Column<bool>(type: "bit", nullable: false),
                    ObtainInformedConsent = table.Column<bool>(type: "bit", nullable: false),
                    ObtainInformedConsentReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmHandHygiene = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmHandHygieneReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UseFullBarrier = table.Column<bool>(type: "bit", nullable: false),
                    UseFullBarrierReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PerformSkin = table.Column<bool>(type: "bit", nullable: false),
                    PerformSkinReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowSite = table.Column<bool>(type: "bit", nullable: false),
                    AllowSiteReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UseSterile = table.Column<bool>(type: "bit", nullable: false),
                    UseSterileReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Maintain = table.Column<bool>(type: "bit", nullable: false),
                    MaintainReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Monitor = table.Column<bool>(type: "bit", nullable: false),
                    MonitorReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CleanBlood = table.Column<bool>(type: "bit", nullable: false),
                    CleanBloodReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcedureNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Operator = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insertion", x => x.Id);
                });


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Insertion");

          
        }
    }
}
