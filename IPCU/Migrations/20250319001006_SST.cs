using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class SST : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SSTInfectionModels",
                columns: table => new
                {
                    SSTID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HospitalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    UnitWardArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Investigator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfAdmission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDRO = table.Column<bool>(type: "bit", nullable: false),
                    InfectionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InfectionClassification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BurnAppearanceChange = table.Column<bool>(type: "bit", nullable: false),
                    BurnOrganismIdentified = table.Column<bool>(type: "bit", nullable: false),
                    BurnCultureDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BurnCultureResults = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DecubitusErythema = table.Column<bool>(type: "bit", nullable: false),
                    DecubitusTenderness = table.Column<bool>(type: "bit", nullable: false),
                    DecubitusSwelling = table.Column<bool>(type: "bit", nullable: false),
                    DecubitusOrganismIdentified = table.Column<bool>(type: "bit", nullable: false),
                    DecubitusCultureDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DecubitusCultureResults = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    STOrganismIdentified = table.Column<bool>(type: "bit", nullable: false),
                    STDrainage = table.Column<bool>(type: "bit", nullable: false),
                    STAbscess = table.Column<bool>(type: "bit", nullable: false),
                    STCultureDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    STCultureResults = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkinPurulentDrainage = table.Column<bool>(type: "bit", nullable: false),
                    SkinVesicles = table.Column<bool>(type: "bit", nullable: false),
                    SkinPustules = table.Column<bool>(type: "bit", nullable: false),
                    SkinBoils = table.Column<bool>(type: "bit", nullable: false),
                    LocalizedPainTenderness = table.Column<bool>(type: "bit", nullable: false),
                    LocalizedSwelling = table.Column<bool>(type: "bit", nullable: false),
                    LocalizedErythema = table.Column<bool>(type: "bit", nullable: false),
                    LocalizedHeat = table.Column<bool>(type: "bit", nullable: false),
                    OrganismIdentifiedFromAspiration = table.Column<bool>(type: "bit", nullable: false),
                    MultinucleatedGiantCellsSeen = table.Column<bool>(type: "bit", nullable: false),
                    DiagnosticAntibodyTiter = table.Column<bool>(type: "bit", nullable: false),
                    SkinCultureDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkinCultureResults = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSTInfectionModels", x => x.SSTID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SSTInfectionModels");
        }
    }
}
