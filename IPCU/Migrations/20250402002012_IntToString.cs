using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class IntToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "HospNum",
                table: "VentilatorEventChecklists",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "HospitalNumber",
                table: "Usi",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "tbmaster",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "SurgicalSiteInfectionChecklist",
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
                    DateOfOperation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Investigator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfAdmission = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispositionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DispositionTransfer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDRO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvolvesOnlySkinAndSubcutaneousTissue = table.Column<bool>(type: "bit", nullable: false),
                    PurulentDrainage = table.Column<bool>(type: "bit", nullable: false),
                    OrganismsIdentified = table.Column<bool>(type: "bit", nullable: false),
                    SuperficialIncisionOpened = table.Column<bool>(type: "bit", nullable: false),
                    NoCulturePerformed = table.Column<bool>(type: "bit", nullable: false),
                    PatientHasSymptoms = table.Column<bool>(type: "bit", nullable: false),
                    LocalizedPainOrTenderness = table.Column<bool>(type: "bit", nullable: false),
                    LocalizedSwelling = table.Column<bool>(type: "bit", nullable: false),
                    Erythema = table.Column<bool>(type: "bit", nullable: false),
                    Heat = table.Column<bool>(type: "bit", nullable: false),
                    DiagnosisByPhysician = table.Column<bool>(type: "bit", nullable: false),
                    CultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvolvesDeepSoftTissues = table.Column<bool>(type: "bit", nullable: false),
                    IDSTPurulentDrainage = table.Column<bool>(type: "bit", nullable: false),
                    DeepIncisionOpenedOrDehisces = table.Column<bool>(type: "bit", nullable: false),
                    IDSTSuperficialIncisionOpened = table.Column<bool>(type: "bit", nullable: false),
                    IDSTOrganismsIdentified = table.Column<bool>(type: "bit", nullable: false),
                    IDSTPatientHasSymptoms = table.Column<bool>(type: "bit", nullable: false),
                    Fever = table.Column<bool>(type: "bit", nullable: false),
                    IDSTLocalizedPainOrTenderness = table.Column<bool>(type: "bit", nullable: false),
                    AbscessOrEvidenceOfInfection = table.Column<bool>(type: "bit", nullable: false),
                    IDSTCultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IDSTCultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvolvesDeeperThanFascialOrMuscleLayers = table.Column<bool>(type: "bit", nullable: false),
                    OSPurulentDrainage = table.Column<bool>(type: "bit", nullable: false),
                    OSOrganismsIdentified = table.Column<bool>(type: "bit", nullable: false),
                    OSAbscessOrEvidenceOfInfection = table.Column<bool>(type: "bit", nullable: false),
                    MeetsSpecificOrganSpaceInfectionCriteria = table.Column<bool>(type: "bit", nullable: false),
                    OSCultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OSCultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Subclass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BURN_PatientChange = table.Column<bool>(type: "bit", nullable: false),
                    CDI_Positve = table.Column<bool>(type: "bit", nullable: false),
                    CDI_Patient = table.Column<bool>(type: "bit", nullable: false),
                    DECU_Patienthastwo = table.Column<bool>(type: "bit", nullable: false),
                    GE_PatientAcute = table.Column<bool>(type: "bit", nullable: false),
                    GE_PatientAtleastTwo = table.Column<bool>(type: "bit", nullable: false),
                    GE_EntericIdentified = table.Column<bool>(type: "bit", nullable: false),
                    GE_EntericDetected = table.Column<bool>(type: "bit", nullable: false),
                    GE_Diagnostic = table.Column<bool>(type: "bit", nullable: false),
                    GIT_AnAbscess = table.Column<bool>(type: "bit", nullable: false),
                    GIT_AbscessGastrointestinal = table.Column<bool>(type: "bit", nullable: false),
                    GIT_OrganismIdentifiedDrainage = table.Column<bool>(type: "bit", nullable: false),
                    GIT_OrganismSeen = table.Column<bool>(type: "bit", nullable: false),
                    GIT_OrganismIdentifiedBloody = table.Column<bool>(type: "bit", nullable: false),
                    GIT_ImagingTest = table.Column<bool>(type: "bit", nullable: false),
                    IAB_PatientOrganism = table.Column<bool>(type: "bit", nullable: false),
                    IAB_PatientAtleastOne = table.Column<bool>(type: "bit", nullable: false),
                    IAB_PatientAtleastTwo = table.Column<bool>(type: "bit", nullable: false),
                    IAB_OrganismSeen = table.Column<bool>(type: "bit", nullable: false),
                    IAB_OrganismIdentified = table.Column<bool>(type: "bit", nullable: false),
                    OREP_PatientOrganism = table.Column<bool>(type: "bit", nullable: false),
                    OREP_PatientAbscess = table.Column<bool>(type: "bit", nullable: false),
                    OREP_PatientSuspected = table.Column<bool>(type: "bit", nullable: false),
                    OREP_OrganismIdentified = table.Column<bool>(type: "bit", nullable: false),
                    OREP_Physician = table.Column<bool>(type: "bit", nullable: false),
                    SSI_AtleastOne = table.Column<bool>(type: "bit", nullable: false),
                    SSI_PatientAtleastTwo = table.Column<bool>(type: "bit", nullable: false),
                    SSI_Organism = table.Column<bool>(type: "bit", nullable: false),
                    SSI_Multinucleated = table.Column<bool>(type: "bit", nullable: false),
                    SSI_Diagnostic = table.Column<bool>(type: "bit", nullable: false),
                    ST_PatientOrganism = table.Column<bool>(type: "bit", nullable: false),
                    ST_PatienPurulent = table.Column<bool>(type: "bit", nullable: false),
                    ST_Abscess = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurgicalSiteInfectionChecklist", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SurgicalSiteInfectionChecklist");

            migrationBuilder.AlterColumn<int>(
                name: "HospNum",
                table: "VentilatorEventChecklists",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "HospitalNumber",
                table: "Usi",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BirthDate",
                table: "tbmaster",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
