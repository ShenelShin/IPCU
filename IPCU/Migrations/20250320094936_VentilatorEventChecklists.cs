using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class VentilatorEventChecklists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VentilatorEventChecklists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HospNum = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    UwArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfIntubation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NameOfInvestigator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfAdmission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DispositionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DispositionTransfer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDRO = table.Column<bool>(type: "bit", nullable: false),
                    MDROOrganism = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaeRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vac1 = table.Column<bool>(type: "bit", nullable: false),
                    Vac2 = table.Column<bool>(type: "bit", nullable: false),
                    IVac1 = table.Column<bool>(type: "bit", nullable: false),
                    IVac2 = table.Column<bool>(type: "bit", nullable: false),
                    IVac3 = table.Column<bool>(type: "bit", nullable: false),
                    Pvap1Endo = table.Column<bool>(type: "bit", nullable: false),
                    Pvap1Lung = table.Column<bool>(type: "bit", nullable: false),
                    Pvap1Bronch = table.Column<bool>(type: "bit", nullable: false),
                    Pvap1Specimen = table.Column<bool>(type: "bit", nullable: false),
                    Pvap2Sputum = table.Column<bool>(type: "bit", nullable: false),
                    Pvap2Endo = table.Column<bool>(type: "bit", nullable: false),
                    Pvap2Lung = table.Column<bool>(type: "bit", nullable: false),
                    Pvap2Bronch = table.Column<bool>(type: "bit", nullable: false),
                    Pvap2Specimen = table.Column<bool>(type: "bit", nullable: false),
                    Pvap3Organism = table.Column<bool>(type: "bit", nullable: false),
                    Pvap3Lung = table.Column<bool>(type: "bit", nullable: false),
                    Pvap3Legionella = table.Column<bool>(type: "bit", nullable: false),
                    Pvap3Viral = table.Column<bool>(type: "bit", nullable: false),
                    PvapCultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PvapResult = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VentilatorEventChecklists", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VentilatorEventChecklists");
        }
    }
}
