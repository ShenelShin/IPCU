using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class CardivascularSystemInfection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardiovascularSystemInfection",
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
                    DateOfAdmission = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispositionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DispositionTransfer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDRO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganismIdentified = table.Column<bool>(type: "bit", nullable: false),
                    GrossEvidenceOfInfection = table.Column<bool>(type: "bit", nullable: false),
                    Symptom_Fever = table.Column<bool>(type: "bit", nullable: false),
                    Symptom_Pain = table.Column<bool>(type: "bit", nullable: false),
                    Symptom_Erythema = table.Column<bool>(type: "bit", nullable: false),
                    Symptom_HeatAtVascularSite = table.Column<bool>(type: "bit", nullable: false),
                    MoreThan15Colonies = table.Column<bool>(type: "bit", nullable: false),
                    Infant_Fever = table.Column<bool>(type: "bit", nullable: false),
                    Infant_Hypothermia = table.Column<bool>(type: "bit", nullable: false),
                    Infant_Apnea = table.Column<bool>(type: "bit", nullable: false),
                    Infant_Bradycardia = table.Column<bool>(type: "bit", nullable: false),
                    Infant_Lethargy = table.Column<bool>(type: "bit", nullable: false),
                    Infant_Pain = table.Column<bool>(type: "bit", nullable: false),
                    Infant_Erythema = table.Column<bool>(type: "bit", nullable: false),
                    Infant_HeatAtVascularSite = table.Column<bool>(type: "bit", nullable: false),
                    Infant_MoreThan15Colonies = table.Column<bool>(type: "bit", nullable: false),
                    CultureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CultureResults = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardiovascularSystemInfection", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardiovascularSystemInfection");
        }
    }
}
