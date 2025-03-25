using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class Pneumoniaagain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pneumonias",
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
                    MDROOrganism = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PNEU_Subclass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersistentorProgressive = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fever1_1 = table.Column<bool>(type: "bit", nullable: false),
                    Leukopenia1_1 = table.Column<bool>(type: "bit", nullable: false),
                    Leukocytosis1_1 = table.Column<bool>(type: "bit", nullable: false),
                    Adults70old1_1 = table.Column<bool>(type: "bit", nullable: false),
                    NewPurulentSputum1_1 = table.Column<bool>(type: "bit", nullable: false),
                    WorseningCoughOrDyspnea1_1 = table.Column<bool>(type: "bit", nullable: false),
                    RalesOrBronchialBreathSounds1_1 = table.Column<bool>(type: "bit", nullable: false),
                    WorseningGasExchange1_1 = table.Column<bool>(type: "bit", nullable: false),
                    WorseningGasExchange1_2 = table.Column<bool>(type: "bit", nullable: false),
                    TemperatureInstability1_2 = table.Column<bool>(type: "bit", nullable: false),
                    LeukopeniaOrLeukocytosis1_2 = table.Column<bool>(type: "bit", nullable: false),
                    NewPurulentSputum1_2 = table.Column<bool>(type: "bit", nullable: false),
                    ApneaOrTachypneaOrNasalFlaring1_2 = table.Column<bool>(type: "bit", nullable: false),
                    WheezingRalesOrRhonchi1_2 = table.Column<bool>(type: "bit", nullable: false),
                    Cough1_2 = table.Column<bool>(type: "bit", nullable: false),
                    BradycardiaOrTachycardia1_2 = table.Column<bool>(type: "bit", nullable: false),
                    Fever1_3 = table.Column<bool>(type: "bit", nullable: false),
                    Leukopenia1_3 = table.Column<bool>(type: "bit", nullable: false),
                    NewPurulentSputum1_3 = table.Column<bool>(type: "bit", nullable: false),
                    NewWorseningCough1_3 = table.Column<bool>(type: "bit", nullable: false),
                    RalesorBronchial1_3 = table.Column<bool>(type: "bit", nullable: false),
                    WorseningGas1_3 = table.Column<bool>(type: "bit", nullable: false),
                    Fever2_1 = table.Column<bool>(type: "bit", nullable: false),
                    Leukopenia12_1 = table.Column<bool>(type: "bit", nullable: false),
                    Leukocytosis2_1 = table.Column<bool>(type: "bit", nullable: false),
                    Adults70old2_1 = table.Column<bool>(type: "bit", nullable: false),
                    NewPurulentSputum2_1 = table.Column<bool>(type: "bit", nullable: false),
                    WorseningCoughOrDyspnea2_1 = table.Column<bool>(type: "bit", nullable: false),
                    RalesOrBronchialBreathSounds2_1 = table.Column<bool>(type: "bit", nullable: false),
                    WorseningGasExchange2_1 = table.Column<bool>(type: "bit", nullable: false),
                    OrganismIdentifiedFromBlood_2_1 = table.Column<bool>(type: "bit", nullable: false),
                    PositiveQuantitativeCultureLRT_2_1 = table.Column<bool>(type: "bit", nullable: false),
                    BALCellsContainIntracellularBacteria_2_1 = table.Column<bool>(type: "bit", nullable: false),
                    PositiveQuantitativeCultureLungTissue_2_1 = table.Column<bool>(type: "bit", nullable: false),
                    HistopathologicExam2_1 = table.Column<bool>(type: "bit", nullable: false),
                    AbscessFormationOrFoci_2_1 = table.Column<bool>(type: "bit", nullable: false),
                    EvidenceOfFungalInvasion_2_1 = table.Column<bool>(type: "bit", nullable: false),
                    CultureDate2_1 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CultureResults2_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fever2_2 = table.Column<bool>(type: "bit", nullable: false),
                    Leukopenia12_2 = table.Column<bool>(type: "bit", nullable: false),
                    Leukocytosis2_2 = table.Column<bool>(type: "bit", nullable: false),
                    Adults70old2_2 = table.Column<bool>(type: "bit", nullable: false),
                    NewPurulentSputum2_2 = table.Column<bool>(type: "bit", nullable: false),
                    WorseningCoughOrDyspnea2_2 = table.Column<bool>(type: "bit", nullable: false),
                    RalesOrBronchialBreathSounds2_2 = table.Column<bool>(type: "bit", nullable: false),
                    WorseningGasExchange2_2 = table.Column<bool>(type: "bit", nullable: false),
                    VirusBordetellaLegionella2_2 = table.Column<bool>(type: "bit", nullable: false),
                    FourfoldrisePaired2_2 = table.Column<bool>(type: "bit", nullable: false),
                    FourfoldriseLegionella2_2 = table.Column<bool>(type: "bit", nullable: false),
                    DetectionofLegionella2_2 = table.Column<bool>(type: "bit", nullable: false),
                    CultureDate2_2 = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CultureResults2_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fever3 = table.Column<bool>(type: "bit", nullable: false),
                    Adults70old3 = table.Column<bool>(type: "bit", nullable: false),
                    NewPurulentSputum3 = table.Column<bool>(type: "bit", nullable: false),
                    WorseningCoughOrDyspnea3 = table.Column<bool>(type: "bit", nullable: false),
                    RalesOrBronchialBreathSounds3 = table.Column<bool>(type: "bit", nullable: false),
                    WorseningGasExchange3 = table.Column<bool>(type: "bit", nullable: false),
                    Hemoptysis3 = table.Column<bool>(type: "bit", nullable: false),
                    PleuriticChestPain3 = table.Column<bool>(type: "bit", nullable: false),
                    IdentificationCandida3 = table.Column<bool>(type: "bit", nullable: false),
                    EvidenceofFungi3 = table.Column<bool>(type: "bit", nullable: false),
                    DirectMicroscopicExam3 = table.Column<bool>(type: "bit", nullable: false),
                    PositiveCultureFungi3 = table.Column<bool>(type: "bit", nullable: false),
                    NonCultureDiagnostic3 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pneumonias", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pneumonias");
        }
    }
}
