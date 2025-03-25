using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class LaboratoryConfirmedBSI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LaboratoryConfirmedBSI",
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
                    centralline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfAdmission = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispositionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DispositionTransfer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDRO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentifiedByCulture = table.Column<bool>(type: "bit", nullable: false),
                    IdentifiedByNonCulture = table.Column<bool>(type: "bit", nullable: false),
                    OrganismNotFromAnotherSite = table.Column<bool>(type: "bit", nullable: false),
                    CultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fever = table.Column<bool>(type: "bit", nullable: false),
                    Hypotension = table.Column<bool>(type: "bit", nullable: false),
                    Chills = table.Column<bool>(type: "bit", nullable: false),
                    lcbi2OrganismNotFromAnotherSite = table.Column<bool>(type: "bit", nullable: false),
                    TwoOrMorePositiveCultures = table.Column<bool>(type: "bit", nullable: false),
                    lcbi2CultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lcbi2CultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lcbi3Fever = table.Column<bool>(type: "bit", nullable: false),
                    Hypothermia = table.Column<bool>(type: "bit", nullable: false),
                    Apnea = table.Column<bool>(type: "bit", nullable: false),
                    Bradycardia = table.Column<bool>(type: "bit", nullable: false),
                    lcbi3OrganismNotFromAnotherSite = table.Column<bool>(type: "bit", nullable: false),
                    lcbi3TwoOrMorePositiveCultures = table.Column<bool>(type: "bit", nullable: false),
                    lcbi3CultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    lcbi3CultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mbilcbi1IdentifiedByCulture = table.Column<bool>(type: "bit", nullable: false),
                    mbilcbi1IdentifiedByNonCulture = table.Column<bool>(type: "bit", nullable: false),
                    OnlyIntestinalOrganisms = table.Column<bool>(type: "bit", nullable: false),
                    mbilcbi1GraftVsHostDisease = table.Column<bool>(type: "bit", nullable: false),
                    mbilcbi1Diarrhea = table.Column<bool>(type: "bit", nullable: false),
                    mbilcbi1Neutropenic = table.Column<bool>(type: "bit", nullable: false),
                    mbilcbi1CultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    mbilcbi1CultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViridansGroupOnly = table.Column<bool>(type: "bit", nullable: false),
                    mbilcbi2GraftVsHostDisease = table.Column<bool>(type: "bit", nullable: false),
                    mbilcbi2Diarrhea = table.Column<bool>(type: "bit", nullable: false),
                    mbilcbi2Neutropenic = table.Column<bool>(type: "bit", nullable: false),
                    mbilcbi2CultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    mbilcbi2CultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    mbilcbi3ViridansGroupOnly = table.Column<bool>(type: "bit", nullable: false),
                    mbilcbi3GraftVsHostDisease = table.Column<bool>(type: "bit", nullable: false),
                    mbilcbi3Diarrhea = table.Column<bool>(type: "bit", nullable: false),
                    mbilcbi3Neutropenic = table.Column<bool>(type: "bit", nullable: false),
                    mbilcbi3CultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    mbilcbi3CultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryConfirmedBSI", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LaboratoryConfirmedBSI");
        }
    }
}
