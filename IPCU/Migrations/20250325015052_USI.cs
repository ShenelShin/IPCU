using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class USI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HospitalNumber = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    UnitWardArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainService = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Investigator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfAdmission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispositionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DispositionTransfer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDRO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientOrganism = table.Column<bool>(type: "bit", nullable: false),
                    PatientAbscess = table.Column<bool>(type: "bit", nullable: false),
                    Fever1 = table.Column<bool>(type: "bit", nullable: false),
                    LocalizedPain = table.Column<bool>(type: "bit", nullable: false),
                    PurulentDrainage = table.Column<bool>(type: "bit", nullable: false),
                    Organism = table.Column<bool>(type: "bit", nullable: false),
                    PatienLessthan1year = table.Column<bool>(type: "bit", nullable: false),
                    Fever2 = table.Column<bool>(type: "bit", nullable: false),
                    Hypothermia = table.Column<bool>(type: "bit", nullable: false),
                    Apnea = table.Column<bool>(type: "bit", nullable: false),
                    Bradycardia = table.Column<bool>(type: "bit", nullable: false),
                    Lethargy = table.Column<bool>(type: "bit", nullable: false),
                    Vomiting = table.Column<bool>(type: "bit", nullable: false),
                    PurulentDrainage2 = table.Column<bool>(type: "bit", nullable: false),
                    Organism2 = table.Column<bool>(type: "bit", nullable: false),
                    CultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usi", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usi");
        }
    }
}
