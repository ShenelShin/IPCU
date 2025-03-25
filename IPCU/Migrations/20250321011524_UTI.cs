using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class UTI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UTIModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HospitalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    UnitWardArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Investigator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IUCInsertDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfAdmission = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispositionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DispositionTransfer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDRO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatheterPresent = table.Column<bool>(type: "bit", nullable: true),
                    CatheterRemovedBeforeEvent = table.Column<bool>(type: "bit", nullable: true),
                    HasFever = table.Column<bool>(type: "bit", nullable: true),
                    HasSuprapubicTenderness = table.Column<bool>(type: "bit", nullable: true),
                    HasCostovertebralPain = table.Column<bool>(type: "bit", nullable: true),
                    HasUrinaryUrgency = table.Column<bool>(type: "bit", nullable: true),
                    HasUrinaryFrequency = table.Column<bool>(type: "bit", nullable: true),
                    HasDysuria = table.Column<bool>(type: "bit", nullable: true),
                    HasUrineCultureWithTwoSpecies = table.Column<bool>(type: "bit", nullable: true),
                    CultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CultureResults = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SUTI1b_HadCatheterLessThan2Days = table.Column<bool>(type: "bit", nullable: true),
                    SUTI1b_NoCatheterInPlace = table.Column<bool>(type: "bit", nullable: true),
                    SUTI1b_Fever = table.Column<bool>(type: "bit", nullable: true),
                    SUTI1b_SuprapubicTenderness = table.Column<bool>(type: "bit", nullable: true),
                    SUTI1b_CostovertebralPain = table.Column<bool>(type: "bit", nullable: true),
                    SUTI1b_UrinaryUrgency = table.Column<bool>(type: "bit", nullable: true),
                    SUTI1b_UrinaryFrequency = table.Column<bool>(type: "bit", nullable: true),
                    SUTI1b_Dysuria = table.Column<bool>(type: "bit", nullable: true),
                    SUTI1b_HasValidUrineCulture = table.Column<bool>(type: "bit", nullable: true),
                    SUTI1b_CultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SUTI1b_CultureResults = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SUTI2_IsOneYearOrLess = table.Column<bool>(type: "bit", nullable: true),
                    SUTI2_Fever = table.Column<bool>(type: "bit", nullable: true),
                    SUTI2_Hypothermia = table.Column<bool>(type: "bit", nullable: true),
                    SUTI2_Apnea = table.Column<bool>(type: "bit", nullable: true),
                    SUTI2_Bradycardia = table.Column<bool>(type: "bit", nullable: true),
                    SUTI2_Lethargy = table.Column<bool>(type: "bit", nullable: true),
                    SUTI2_Vomiting = table.Column<bool>(type: "bit", nullable: true),
                    SUTI2_SuprapubicTenderness = table.Column<bool>(type: "bit", nullable: true),
                    SUTI2_HasValidUrineCulture = table.Column<bool>(type: "bit", nullable: true),
                    SUTI2_CultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SUTI2_CultureResults = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ABUTI_NoSymptoms = table.Column<bool>(type: "bit", nullable: true),
                    ABUTI_ValidUrineCulture = table.Column<bool>(type: "bit", nullable: true),
                    ABUTI_OrganismIdentified = table.Column<bool>(type: "bit", nullable: true),
                    ABUTI_CultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ABUTI_CultureResults = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UTIModels", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UTIModels");
        }
    }
}
