using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class HAI : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmployeeID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Initial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserValidUntil = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefaultDepartment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectionID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HEMOStationID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignedArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "EvaluationViewModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceClassification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlowFollowed = table.Column<double>(type: "float", nullable: false),
                    RulesEstablished = table.Column<double>(type: "float", nullable: false),
                    InitiateDiscussion = table.Column<double>(type: "float", nullable: false),
                    TechnicalCapability = table.Column<double>(type: "float", nullable: false),
                    ContentOrganization = table.Column<double>(type: "float", nullable: false),
                    ObjectiveStated = table.Column<double>(type: "float", nullable: false),
                    ContentQuality = table.Column<double>(type: "float", nullable: false),
                    FlowOfTopic = table.Column<double>(type: "float", nullable: false),
                    RelevanceOfTopic = table.Column<double>(type: "float", nullable: false),
                    PracticeApplication = table.Column<double>(type: "float", nullable: false),
                    LearningActivities = table.Column<double>(type: "float", nullable: false),
                    VisualAids = table.Column<double>(type: "float", nullable: false),
                    PresentKnowledge = table.Column<double>(type: "float", nullable: false),
                    BalancePrinciples = table.Column<double>(type: "float", nullable: false),
                    AddressClarifications = table.Column<double>(type: "float", nullable: false),
                    Preparedness = table.Column<double>(type: "float", nullable: false),
                    TeachingPersonality = table.Column<double>(type: "float", nullable: false),
                    EstablishRapport = table.Column<double>(type: "float", nullable: false),
                    RespectForParticipants = table.Column<double>(type: "float", nullable: false),
                    VoicePersonality = table.Column<double>(type: "float", nullable: false),
                    TimeManagement = table.Column<double>(type: "float", nullable: false),
                    SMELecturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuggestionsForImprovement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SayToSpeaker = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationViewModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FitTestingForm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HCW_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DUO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Professional_Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Limitation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fit_Test_Solution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sensitivity_Test = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Respiratory_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Normal_Breathing = table.Column<bool>(type: "bit", nullable: false),
                    Deep_Breathing = table.Column<bool>(type: "bit", nullable: false),
                    Turn_head_side_to_side = table.Column<bool>(type: "bit", nullable: false),
                    Move_head_up_and_down = table.Column<bool>(type: "bit", nullable: false),
                    Reading = table.Column<bool>(type: "bit", nullable: false),
                    Bending_Jogging = table.Column<bool>(type: "bit", nullable: false),
                    Normal_Breathing_2 = table.Column<bool>(type: "bit", nullable: false),
                    Test_Results = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name_of_Fit_Tester = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DUO_Tester = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiringAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SubmissionCount = table.Column<int>(type: "int", nullable: false),
                    MaxRetakes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitTestingForm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HandHygieneComplianceSummary",
                columns: table => new
                {
                    SummaryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SummaryType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalCompliantActions = table.Column<int>(type: "int", nullable: false),
                    TotalObservedOpportunities = table.Column<int>(type: "int", nullable: false),
                    ComplianceRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GeneratedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HandHygieneComplianceSummary", x => x.SummaryId);
                });

            migrationBuilder.CreateTable(
                name: "HandHygieneForms",
                columns: table => new
                {
                    HHId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Observer = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    HCWType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Isolation = table.Column<bool>(type: "bit", nullable: false),
                    IsolationPrecaution = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ObsvPatientCare = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ObsvPatientEnvironment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EnvironmentResource = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ObsvPatientContact = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TotalCompliantActions = table.Column<int>(type: "int", nullable: false),
                    TotalObservedOpportunities = table.Column<int>(type: "int", nullable: false),
                    ComplianceRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HandHygieneForms", x => x.HHId);
                });

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
                    DispositionTransfer = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "PatientForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suffix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Disease = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Room = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Building = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NurseFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NurseMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NurseLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NurseSuffix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PediatricVAEChecklist",
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
                    DateOfIntubation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Investigator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfAdmission = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispositionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DispositionTransfer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDRO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FiO2Increase = table.Column<bool>(type: "bit", nullable: false),
                    MAPIncrease = table.Column<bool>(type: "bit", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PediatricVAEChecklist", x => x.Id);
                });

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
                    MainService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Investigator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfAdmission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispositionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DispositionTransfer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDRO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PNEU_Subclass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersistentorProgressive1_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fever1_1 = table.Column<bool>(type: "bit", nullable: false),
                    Leukopenia1_1 = table.Column<bool>(type: "bit", nullable: false),
                    Leukocytosis1_1 = table.Column<bool>(type: "bit", nullable: false),
                    Adults70old1_1 = table.Column<bool>(type: "bit", nullable: false),
                    NewPurulentSputum1_1 = table.Column<bool>(type: "bit", nullable: false),
                    WorseningCoughOrDyspnea1_1 = table.Column<bool>(type: "bit", nullable: false),
                    RalesOrBronchialBreathSounds1_1 = table.Column<bool>(type: "bit", nullable: false),
                    WorseningGasExchange1_1 = table.Column<bool>(type: "bit", nullable: false),
                    PersistentorProgressive1_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorseningGasExchange1_2 = table.Column<bool>(type: "bit", nullable: false),
                    TemperatureInstability1_2 = table.Column<bool>(type: "bit", nullable: false),
                    LeukopeniaOrLeukocytosis1_2 = table.Column<bool>(type: "bit", nullable: false),
                    NewPurulentSputum1_2 = table.Column<bool>(type: "bit", nullable: false),
                    ApneaOrTachypneaOrNasalFlaring1_2 = table.Column<bool>(type: "bit", nullable: false),
                    WheezingRalesOrRhonchi1_2 = table.Column<bool>(type: "bit", nullable: false),
                    Cough1_2 = table.Column<bool>(type: "bit", nullable: false),
                    BradycardiaOrTachycardia1_2 = table.Column<bool>(type: "bit", nullable: false),
                    PersistentorProgressive1_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fever1_3 = table.Column<bool>(type: "bit", nullable: false),
                    Leukopenia1_3 = table.Column<bool>(type: "bit", nullable: false),
                    NewPurulentSputum1_3 = table.Column<bool>(type: "bit", nullable: false),
                    NewWorseningCough1_3 = table.Column<bool>(type: "bit", nullable: false),
                    RalesorBronchial1_3 = table.Column<bool>(type: "bit", nullable: false),
                    WorseningGas1_3 = table.Column<bool>(type: "bit", nullable: false),
                    PersistentorProgressive2_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    PersistentorProgressive2_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    PersistentorProgressive3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
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

            migrationBuilder.CreateTable(
                name: "PostTestClinicals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PWD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CivilStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    POSTCSCORE = table.Column<float>(type: "real", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTestClinicals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostTestNonCLinicals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PWD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CivilStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    POSTNONSCORE = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTestNonCLinicals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreTestClinicals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PWD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CivilStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRETCSCORE = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreTestClinicals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PreTestNonClinicals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PWD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CivilStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRETNONCSCORE = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreTestNonClinicals", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "tbantibiotics",
                columns: table => new
                {
                    AntibioticId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DateAdded = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbantibiotics", x => x.AntibioticId);
                });

            migrationBuilder.CreateTable(
                name: "tbmaster",
                columns: table => new
                {
                    HospNum = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountNum = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true),
                    CivilStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Age = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    PatientStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    SeniorCitizenWithID = table.Column<bool>(type: "bit", nullable: false),
                    cellnum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EmpNum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PatientType = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    HaiStatus = table.Column<bool>(type: "bit", nullable: false),
                    HaiCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbmaster", x => x.HospNum);
                });

            migrationBuilder.CreateTable(
                name: "tbpatient",
                columns: table => new
                {
                    IdNum = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HospNum = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    AdmType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AdmLocation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    AdmDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Age = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    RoomID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    DeathType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DeathDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbpatient", x => x.IdNum);
                });

            migrationBuilder.CreateTable(
                name: "Trainees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    N95 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDPrinting = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasicInfection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProperHand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GloveRemoval = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreTestScore = table.Column<int>(type: "int", nullable: true),
                    PostTestScore = table.Column<int>(type: "int", nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TrainingReport = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingEvaluation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfTraining = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrainingMethodology = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfessionalCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalParticipantsMale = table.Column<int>(type: "int", nullable: false),
                    TotalParticipantsFemale = table.Column<int>(type: "int", nullable: false),
                    PostTestEvaluationGrade = table.Column<double>(type: "float", nullable: false),
                    FinalRating = table.Column<double>(type: "float", nullable: false),
                    FlowFollowed = table.Column<double>(type: "float", nullable: false),
                    RulesEstablished = table.Column<double>(type: "float", nullable: false),
                    InitiateDiscussion = table.Column<double>(type: "float", nullable: false),
                    TechnicalCapability = table.Column<double>(type: "float", nullable: false),
                    ContentOrganization = table.Column<double>(type: "float", nullable: false),
                    ObjectiveStated = table.Column<double>(type: "float", nullable: false),
                    ContentQuality = table.Column<double>(type: "float", nullable: false),
                    FlowOfTopic = table.Column<double>(type: "float", nullable: false),
                    RelevanceOfTopic = table.Column<double>(type: "float", nullable: false),
                    PracticeApplication = table.Column<double>(type: "float", nullable: false),
                    LearningActivities = table.Column<double>(type: "float", nullable: false),
                    VisualAids = table.Column<double>(type: "float", nullable: false),
                    PresentKnowledge = table.Column<double>(type: "float", nullable: false),
                    BalancePrinciples = table.Column<double>(type: "float", nullable: false),
                    AddressClarifications = table.Column<double>(type: "float", nullable: false),
                    Preparedness = table.Column<double>(type: "float", nullable: false),
                    TeachingPersonality = table.Column<double>(type: "float", nullable: false),
                    EstablishRapport = table.Column<double>(type: "float", nullable: false),
                    RespectForParticipants = table.Column<double>(type: "float", nullable: false),
                    VoicePersonality = table.Column<double>(type: "float", nullable: false),
                    TimeManagement = table.Column<double>(type: "float", nullable: false),
                    SMELecturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuggestionsForImprovement = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SayToSpeaker = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingEvaluation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingSummaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PWD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CivilStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreScore = table.Column<float>(type: "real", nullable: false),
                    PreScore_Total = table.Column<float>(type: "real", nullable: false),
                    PostScore = table.Column<float>(type: "real", nullable: false),
                    PostScore_Total = table.Column<float>(type: "real", nullable: false),
                    Rate = table.Column<float>(type: "real", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProperHand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GloveRemoval = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDPrinting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrainingReport = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSummaries", x => x.Id);
                });

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
                    DispositionTransfer = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    CultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    SUTI1b_CultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    SUTI2_CultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ABUTI_NoSymptoms = table.Column<bool>(type: "bit", nullable: true),
                    ABUTI_ValidUrineCulture = table.Column<bool>(type: "bit", nullable: true),
                    ABUTI_OrganismIdentified = table.Column<bool>(type: "bit", nullable: true),
                    ABUTI_CultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ABUTI_CultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UTIModels", x => x.Id);
                });

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

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FitTestingFormHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FitTestingFormId = table.Column<int>(type: "int", nullable: false),
                    Fit_Test_Solution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sensitivity_Test = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Respiratory_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Normal_Breathing = table.Column<bool>(type: "bit", nullable: false),
                    Deep_Breathing = table.Column<bool>(type: "bit", nullable: false),
                    Turn_head_side_to_side = table.Column<bool>(type: "bit", nullable: false),
                    Move_head_up_and_down = table.Column<bool>(type: "bit", nullable: false),
                    Reading = table.Column<bool>(type: "bit", nullable: false),
                    Bending_Jogging = table.Column<bool>(type: "bit", nullable: false),
                    Normal_Breathing_2 = table.Column<bool>(type: "bit", nullable: false),
                    Test_Results = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitTestingFormHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FitTestingFormHistory_FitTestingForm_FitTestingFormId",
                        column: x => x.FitTestingFormId,
                        principalTable: "FitTestingForm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HHActivities",
                columns: table => new
                {
                    ActId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HHId = table.Column<int>(type: "int", nullable: false),
                    Activity = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BeforeHandRub = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BeforeHandWash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AfterHandRub = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AfterHandWash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Gloves = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HHActivities", x => x.ActId);
                    table.ForeignKey(
                        name: "FK_HHActivities_HandHygieneForms_HHId",
                        column: x => x.HHId,
                        principalTable: "HandHygieneForms",
                        principalColumn: "HHId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceConnected",
                columns: table => new
                {
                    DeviceId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    DeviceType = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DeviceInsert = table.Column<DateTime>(type: "date", nullable: false),
                    DeviceRemove = table.Column<DateTime>(type: "date", nullable: true),
                    HospNum = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceConnected", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_DeviceConnected_tbmaster_HospNum",
                        column: x => x.HospNum,
                        principalTable: "tbmaster",
                        principalColumn: "HospNum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbdiagnostics",
                columns: table => new
                {
                    DiagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCollection = table.Column<DateTime>(type: "date", nullable: false),
                    SourceSite = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsolateFindingsResult = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    HospNum = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbdiagnostics", x => x.DiagId);
                    table.ForeignKey(
                        name: "FK_tbdiagnostics_tbmaster_HospNum",
                        column: x => x.HospNum,
                        principalTable: "tbmaster",
                        principalColumn: "HospNum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VitalSigns",
                columns: table => new
                {
                    VitalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VitalSign = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VitalSignValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VitalSignDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HospNum = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VitalSigns", x => x.VitalId);
                    table.ForeignKey(
                        name: "FK_VitalSigns_tbmaster_HospNum",
                        column: x => x.HospNum,
                        principalTable: "tbmaster",
                        principalColumn: "HospNum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbdiagnosticstreatments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiagId = table.Column<int>(type: "int", nullable: false),
                    AntibioticId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbdiagnosticstreatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbdiagnosticstreatments_tbantibiotics_AntibioticId",
                        column: x => x.AntibioticId,
                        principalTable: "tbantibiotics",
                        principalColumn: "AntibioticId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbdiagnosticstreatments_tbdiagnostics_DiagId",
                        column: x => x.DiagId,
                        principalTable: "tbdiagnostics",
                        principalColumn: "DiagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceConnected_HospNum",
                table: "DeviceConnected",
                column: "HospNum");

            migrationBuilder.CreateIndex(
                name: "IX_FitTestingFormHistory_FitTestingFormId",
                table: "FitTestingFormHistory",
                column: "FitTestingFormId");

            migrationBuilder.CreateIndex(
                name: "IX_HHActivities_HHId",
                table: "HHActivities",
                column: "HHId");

            migrationBuilder.CreateIndex(
                name: "IX_tbdiagnostics_HospNum",
                table: "tbdiagnostics",
                column: "HospNum");

            migrationBuilder.CreateIndex(
                name: "IX_tbdiagnosticstreatments_AntibioticId",
                table: "tbdiagnosticstreatments",
                column: "AntibioticId");

            migrationBuilder.CreateIndex(
                name: "IX_tbdiagnosticstreatments_DiagId",
                table: "tbdiagnosticstreatments",
                column: "DiagId");

            migrationBuilder.CreateIndex(
                name: "IX_VitalSigns_HospNum",
                table: "VitalSigns",
                column: "HospNum");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CardiovascularSystemInfection");

            migrationBuilder.DropTable(
                name: "DeviceConnected");

            migrationBuilder.DropTable(
                name: "EvaluationViewModel");

            migrationBuilder.DropTable(
                name: "FitTestingFormHistory");

            migrationBuilder.DropTable(
                name: "HandHygieneComplianceSummary");

            migrationBuilder.DropTable(
                name: "HHActivities");

            migrationBuilder.DropTable(
                name: "LaboratoryConfirmedBSI");

            migrationBuilder.DropTable(
                name: "PatientForms");

            migrationBuilder.DropTable(
                name: "PediatricVAEChecklist");

            migrationBuilder.DropTable(
                name: "Pneumonias");

            migrationBuilder.DropTable(
                name: "PostTestClinicals");

            migrationBuilder.DropTable(
                name: "PostTestNonCLinicals");

            migrationBuilder.DropTable(
                name: "PreTestClinicals");

            migrationBuilder.DropTable(
                name: "PreTestNonClinicals");

            migrationBuilder.DropTable(
                name: "SSTInfectionModels");

            migrationBuilder.DropTable(
                name: "tbdiagnosticstreatments");

            migrationBuilder.DropTable(
                name: "tbpatient");

            migrationBuilder.DropTable(
                name: "Trainees");

            migrationBuilder.DropTable(
                name: "TrainingEvaluation");

            migrationBuilder.DropTable(
                name: "TrainingSummaries");

            migrationBuilder.DropTable(
                name: "Usi");

            migrationBuilder.DropTable(
                name: "UTIModels");

            migrationBuilder.DropTable(
                name: "VentilatorEventChecklists");

            migrationBuilder.DropTable(
                name: "VitalSigns");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "FitTestingForm");

            migrationBuilder.DropTable(
                name: "HandHygieneForms");

            migrationBuilder.DropTable(
                name: "tbantibiotics");

            migrationBuilder.DropTable(
                name: "tbdiagnostics");

            migrationBuilder.DropTable(
                name: "tbmaster");
        }
    }
}
