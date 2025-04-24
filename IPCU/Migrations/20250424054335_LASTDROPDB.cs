using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class LASTDROPDB : Migration
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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    MDROOrganism = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "ConstructionSites",
                columns: table => new
                {
                    CSID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectNameAndDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractorRepresentativeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneOrMobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecificSiteOfActivity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScopeOfWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstimatedDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarrierICRA = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarrierICRAComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarrierDoorsSealedSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarrierDoorsSealedComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarrierFloorCleanSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarrierFloorCleanComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarrierWalkOffMatsSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarrierWalkOffMatsComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BarrierTapeAdheringSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BarrierTapeAdheringComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirhandlingWindowsClosedBehindBarrierSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirhandlingWindowsClosedBehindBarrierComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirhandlingNegativeAirMonitoredSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirhandlingNegativeAirMonitoredComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirHandlingUnitRunningSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirHandlingUnitRunningComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirhandlingMaintenanceLabelVisibleSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirhandlingMaintenanceLabelVisibleComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AirhandlingAirExhaustedToAppropriateAreaSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AirhandlingAirExhaustedToAppropriateAreaComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaHEPAFilteredVacuumOnJobsiteSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectareaHEPAFilteredVacuumOnJobsiteComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaDebrisRemovedInCoveredContainerDailySelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectareaDebrisRemovedInCoveredContainerDailyComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaDesignatedConstructionRouteOrMapPostedSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectareaDesignatedConstructionRouteOrMapPostedComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaTrashInAppropriateContainerSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectareaTrashInAppropriateContainerComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaRoutineCleaningDoneOnSiteSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectareaRoutineCleaningDoneOnSiteComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectareaAirVentsSealedOrDuctworkCappedSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectareaAirVentsSealedOrDuctworkCappedComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrafficcontrolRestrictedToConstructionWorkersSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrafficcontrolRestrictedToConstructionWorkersComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrafficcontrolDoorsAndExitsFreeOfDebrisSelection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrafficcontrolDoorsAndExitsFreeOfDebrisComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DresscodeProtectiveClothingWornSelection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DresscodeProtectiveClothingWornComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DresscodeWorkersClothingCleanUponExitingSelection = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DresscodeWorkersClothingCleanUponExitingComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractorSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineeringSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ICPSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitAreaRep = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionSites", x => x.CSID);
                });

            migrationBuilder.CreateTable(
                name: "DailyCentralLineMaintenanceChecklists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaOrUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAndTimeOfMonitoring = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssessedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InitialPlacement = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Implanted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Injection = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Dateadministration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Necessityassessed = table.Column<bool>(type: "bit", nullable: false),
                    Injectionsites = table.Column<bool>(type: "bit", nullable: false),
                    Capschanged = table.Column<bool>(type: "bit", nullable: false),
                    Insertionsite = table.Column<bool>(type: "bit", nullable: false),
                    Dressingintact = table.Column<bool>(type: "bit", nullable: false),
                    Dressingchanged = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumCompliant = table.Column<int>(type: "int", nullable: true),
                    TotalObserved = table.Column<int>(type: "int", nullable: true),
                    ComplianceRate = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyCentralLineMaintenanceChecklists", x => x.Id);
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
                name: "GIInfectionChecklists",
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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Investigator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfAdmission = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispositionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DispositionTransfer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDRO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDROOrganism = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CDItoxin = table.Column<bool>(type: "bit", nullable: false),
                    CDIpseudomembranous = table.Column<bool>(type: "bit", nullable: false),
                    GEdiarrhea = table.Column<bool>(type: "bit", nullable: false),
                    GENausea = table.Column<bool>(type: "bit", nullable: false),
                    GEVomiting = table.Column<bool>(type: "bit", nullable: false),
                    GENAbdominal = table.Column<bool>(type: "bit", nullable: false),
                    GENFever = table.Column<bool>(type: "bit", nullable: false),
                    GENHeadache = table.Column<bool>(type: "bit", nullable: false),
                    GENpenteric1 = table.Column<bool>(type: "bit", nullable: false),
                    GENenteric2 = table.Column<bool>(type: "bit", nullable: false),
                    GENantibody = table.Column<bool>(type: "bit", nullable: false),
                    IABorganism = table.Column<bool>(type: "bit", nullable: false),
                    IABAbscess1 = table.Column<bool>(type: "bit", nullable: false),
                    IABAbscess2 = table.Column<bool>(type: "bit", nullable: false),
                    IABFever = table.Column<bool>(type: "bit", nullable: false),
                    IABHypotension = table.Column<bool>(type: "bit", nullable: false),
                    IABNausea = table.Column<bool>(type: "bit", nullable: false),
                    IABVomiting = table.Column<bool>(type: "bit", nullable: false),
                    IABAbdominal = table.Column<bool>(type: "bit", nullable: false),
                    IABtransaminase = table.Column<bool>(type: "bit", nullable: false),
                    IABJaundice = table.Column<bool>(type: "bit", nullable: false),
                    IABOrgintraabdominal = table.Column<bool>(type: "bit", nullable: false),
                    IABmicrobiologic = table.Column<bool>(type: "bit", nullable: false),
                    GITAbscess1 = table.Column<bool>(type: "bit", nullable: false),
                    GITAbscess2 = table.Column<bool>(type: "bit", nullable: false),
                    GITblood = table.Column<bool>(type: "bit", nullable: false),
                    GITNausea = table.Column<bool>(type: "bit", nullable: false),
                    GITVomiting = table.Column<bool>(type: "bit", nullable: false),
                    GITPainTend = table.Column<bool>(type: "bit", nullable: false),
                    GITFEVER = table.Column<bool>(type: "bit", nullable: false),
                    GITOdynophagia = table.Column<bool>(type: "bit", nullable: false),
                    GITDysphagia = table.Column<bool>(type: "bit", nullable: false),
                    GITDrain = table.Column<bool>(type: "bit", nullable: false),
                    GITGram = table.Column<bool>(type: "bit", nullable: false),
                    GITmicrobiologic = table.Column<bool>(type: "bit", nullable: false),
                    GITgastrointestinal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GIInfectionChecklists", x => x.Id);
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
                name: "ICRA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectNameAndDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractorRepresentativeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneOrMobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecificSiteOfActivity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConstructionEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScopeOfWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstimatedDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConstructionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientRiskGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreventiveMeasures = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiskGroup_Below = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RiskGroup_Above = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RiskGroup_Lateral = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RiskGroup_Behind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RiskGroup_Front = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalNumber_Below = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalNumber_Above = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalNumber_Lateral = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalNumber_Behind = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocalNumber_Front = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Below_Noise = table.Column<bool>(type: "bit", nullable: false),
                    Below_Vibration = table.Column<bool>(type: "bit", nullable: false),
                    Below_Dust = table.Column<bool>(type: "bit", nullable: false),
                    Below_Ventilation = table.Column<bool>(type: "bit", nullable: false),
                    Below_Pressuraztion = table.Column<bool>(type: "bit", nullable: false),
                    Below_Data = table.Column<bool>(type: "bit", nullable: false),
                    Below_Mechanical = table.Column<bool>(type: "bit", nullable: false),
                    Below_MedicalGas = table.Column<bool>(type: "bit", nullable: false),
                    Below_HotColdWater = table.Column<bool>(type: "bit", nullable: false),
                    Below_Other = table.Column<bool>(type: "bit", nullable: false),
                    Above_Noise = table.Column<bool>(type: "bit", nullable: false),
                    Above_Vibration = table.Column<bool>(type: "bit", nullable: false),
                    Above_Dust = table.Column<bool>(type: "bit", nullable: false),
                    Above_Ventilation = table.Column<bool>(type: "bit", nullable: false),
                    Above_Pressuraztion = table.Column<bool>(type: "bit", nullable: false),
                    Above_Data = table.Column<bool>(type: "bit", nullable: false),
                    Above_Mechanical = table.Column<bool>(type: "bit", nullable: false),
                    Above_MedicalGas = table.Column<bool>(type: "bit", nullable: false),
                    Above_HotColdWater = table.Column<bool>(type: "bit", nullable: false),
                    Above_Other = table.Column<bool>(type: "bit", nullable: false),
                    Lateral_Noise = table.Column<bool>(type: "bit", nullable: false),
                    Lateral_Vibration = table.Column<bool>(type: "bit", nullable: false),
                    Lateral_Dust = table.Column<bool>(type: "bit", nullable: false),
                    Lateral_Ventilation = table.Column<bool>(type: "bit", nullable: false),
                    Lateral_Pressuraztion = table.Column<bool>(type: "bit", nullable: false),
                    Lateral_Data = table.Column<bool>(type: "bit", nullable: false),
                    Lateral_Mechanical = table.Column<bool>(type: "bit", nullable: false),
                    Lateral_MedicalGas = table.Column<bool>(type: "bit", nullable: false),
                    Lateral_HotColdWater = table.Column<bool>(type: "bit", nullable: false),
                    Lateral_Other = table.Column<bool>(type: "bit", nullable: false),
                    Behind_Noise = table.Column<bool>(type: "bit", nullable: false),
                    Behind_Vibration = table.Column<bool>(type: "bit", nullable: false),
                    Behind_Dust = table.Column<bool>(type: "bit", nullable: false),
                    Behind_Ventilation = table.Column<bool>(type: "bit", nullable: false),
                    Behind_Pressuraztion = table.Column<bool>(type: "bit", nullable: false),
                    Behind_Data = table.Column<bool>(type: "bit", nullable: false),
                    Behind_Mechanical = table.Column<bool>(type: "bit", nullable: false),
                    Behind_MedicalGas = table.Column<bool>(type: "bit", nullable: false),
                    Behind_HotColdWater = table.Column<bool>(type: "bit", nullable: false),
                    Behind_Other = table.Column<bool>(type: "bit", nullable: false),
                    Front_Noise = table.Column<bool>(type: "bit", nullable: false),
                    Front_Vibration = table.Column<bool>(type: "bit", nullable: false),
                    Front_Dust = table.Column<bool>(type: "bit", nullable: false),
                    Front_Ventilation = table.Column<bool>(type: "bit", nullable: false),
                    Front_Pressuraztion = table.Column<bool>(type: "bit", nullable: false),
                    Front_Data = table.Column<bool>(type: "bit", nullable: false),
                    Front_Mechanical = table.Column<bool>(type: "bit", nullable: false),
                    Front_MedicalGas = table.Column<bool>(type: "bit", nullable: false),
                    Front_HotColdWater = table.Column<bool>(type: "bit", nullable: false),
                    Front_Other = table.Column<bool>(type: "bit", nullable: false),
                    RiskofWater = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks_RiskofWater = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShouldWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks_ShouldWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanSupplyAir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks_CanSupplyAir = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HaveTraffic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks_HaveTraffic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanPatientCare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks_CanPatientCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreMeasures = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks_AreMeasures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractorSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineeringSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ICPSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitAreaRep = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICRA", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InfectionControlMonitoringForm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaOrUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateAndTimeOfMonitoring = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssessedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientExamRoom_FloorsWallsClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientExamRoom_FloorsWallsClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientExamRoom_FloorsWallsClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientExamRoom_FloorsWallsClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientExamRoom_FloorsWallsClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientExamRoom_CurtainsClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientExamRoom_CurtainsClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientExamRoom_CurtainsClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientExamRoom_CurtainsClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientExamRoom_CurtainsClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientExamRoom_SinkClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientExamRoom_SinkClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientExamRoom_SinkClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientExamRoom_SinkClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientExamRoom_SinkClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientExamRoom_SoapDispenser_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientExamRoom_SoapDispenser_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientExamRoom_SoapDispenser_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientExamRoom_SoapDispenser_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientExamRoom_SoapDispenser_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientExamRoom_AlcoholSanitizers_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientExamRoom_AlcoholSanitizers_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientExamRoom_AlcoholSanitizers_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientExamRoom_AlcoholSanitizers_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientExamRoom_AlcoholSanitizers_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientExamRoom_PPEAvailable_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientExamRoom_PPEAvailable_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientExamRoom_PPEAvailable_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientExamRoom_PPEAvailable_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientExamRoom_PPEAvailable_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientExamRoom_NoSuppliesUnderSinks_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientExamRoom_NoSuppliesUnderSinks_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientExamRoom_NoSuppliesUnderSinks_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientExamRoom_NoSuppliesUnderSinks_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientExamRoom_NoSuppliesUnderSinks_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientExamRoom_NoFoodInCareAreas_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientExamRoom_NoFoodInCareAreas_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientExamRoom_NoFoodInCareAreas_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientExamRoom_NoFoodInCareAreas_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PatientExamRoom_NoFoodInCareAreas_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_ChartsStored_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_ChartsStored_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_ChartsStored_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_ChartsStored_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkArea_ChartsStored_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_NoFoodOrDrinks_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_NoFoodOrDrinks_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_NoFoodOrDrinks_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_NoFoodOrDrinks_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkArea_NoFoodOrDrinks_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_FansClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_FansClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_FansClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_FansClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkArea_FansClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_ACClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_ACClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_ACClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_ACClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkArea_ACClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_ExhaustFansClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_ExhaustFansClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_ExhaustFansClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_ExhaustFansClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkArea_ExhaustFansClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_CeilingClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_CeilingClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_CeilingClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_CeilingClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkArea_CeilingClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_WallsClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_WallsClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_WallsClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_WallsClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkArea_WallsClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_CountersClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_CountersClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_CountersClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_CountersClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkArea_CountersClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_FloorsClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_FloorsClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_FloorsClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_FloorsClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkArea_FloorsClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_DoorsWindowsClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_DoorsWindowsClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_DoorsWindowsClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_DoorsWindowsClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkArea_DoorsWindowsClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_CleanBathroom_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_CleanBathroom_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkArea_CleanBathroom_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkArea_CleanBathroom_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkArea_CleanBathroom_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hallway_FloorsWallsClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hallway_FloorsWallsClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hallway_FloorsWallsClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hallway_FloorsWallsClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Hallway_FloorsWallsClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hallway_NoObstruction_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hallway_NoObstruction_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hallway_NoObstruction_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hallway_NoObstruction_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Hallway_NoObstruction_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaitingArea_FurnitureClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaitingArea_FurnitureClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaitingArea_FurnitureClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaitingArea_FurnitureClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WaitingArea_FurnitureClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaitingArea_TrashDisposed_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaitingArea_TrashDisposed_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaitingArea_TrashDisposed_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaitingArea_TrashDisposed_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WaitingArea_TrashDisposed_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaitingArea_FoodInDesignatedArea_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaitingArea_FoodInDesignatedArea_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WaitingArea_FoodInDesignatedArea_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaitingArea_FoodInDesignatedArea_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WaitingArea_FoodInDesignatedArea_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Equipment_StorageClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Equipment_StorageClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Equipment_StorageClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Equipment_StorageClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Equipment_StorageClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_CleanSoiledSeparation_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_CleanSoiledSeparation_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_CleanSoiledSeparation_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_CleanSoiledSeparation_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UtilityRoom_CleanSoiledSeparation_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_FloorsWallsClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_FloorsWallsClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_FloorsWallsClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_FloorsWallsClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UtilityRoom_FloorsWallsClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_NoSuppliesOnFloor_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_NoSuppliesOnFloor_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_NoSuppliesOnFloor_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_NoSuppliesOnFloor_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UtilityRoom_NoSuppliesOnFloor_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_SuppliesFromCeiling_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_SuppliesFromCeiling_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_SuppliesFromCeiling_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_SuppliesFromCeiling_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UtilityRoom_SuppliesFromCeiling_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_NoSuppliesUnderSink_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_NoSuppliesUnderSink_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_NoSuppliesUnderSink_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_NoSuppliesUnderSink_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UtilityRoom_NoSuppliesUnderSink_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_NoSuppliesInBathrooms_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_NoSuppliesInBathrooms_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_NoSuppliesInBathrooms_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_NoSuppliesInBathrooms_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UtilityRoom_NoSuppliesInBathrooms_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_FIFOStocks_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_FIFOStocks_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_FIFOStocks_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_FIFOStocks_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UtilityRoom_FIFOStocks_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_SuppliesNotExpired_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_SuppliesNotExpired_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_SuppliesNotExpired_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_SuppliesNotExpired_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UtilityRoom_SuppliesNotExpired_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_SterileTraysClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_SterileTraysClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UtilityRoom_SterileTraysClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UtilityRoom_SterileTraysClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UtilityRoom_SterileTraysClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoiledRoom_FloorsWallsClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoiledRoom_FloorsWallsClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoiledRoom_FloorsWallsClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoiledRoom_FloorsWallsClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoiledRoom_FloorsWallsClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoiledRoom_NoPatientSupplies_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoiledRoom_NoPatientSupplies_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoiledRoom_NoPatientSupplies_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoiledRoom_NoPatientSupplies_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoiledRoom_NoPatientSupplies_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoiledRoom_LinenBagged_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoiledRoom_LinenBagged_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoiledRoom_LinenBagged_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoiledRoom_LinenBagged_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SoiledRoom_LinenBagged_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WasteMgmt_ColorBinsAvailable_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WasteMgmt_ColorBinsAvailable_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WasteMgmt_ColorBinsAvailable_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WasteMgmt_ColorBinsAvailable_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WasteMgmt_ColorBinsAvailable_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WasteMgmt_ProperDisposal_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WasteMgmt_ProperDisposal_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WasteMgmt_ProperDisposal_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WasteMgmt_ProperDisposal_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WasteMgmt_ProperDisposal_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WasteMgmt_BinsNotOverfilled_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WasteMgmt_BinsNotOverfilled_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WasteMgmt_BinsNotOverfilled_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WasteMgmt_BinsNotOverfilled_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WasteMgmt_BinsNotOverfilled_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WasteMgmt_BinsClean_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WasteMgmt_BinsClean_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WasteMgmt_BinsClean_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WasteMgmt_BinsClean_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WasteMgmt_BinsClean_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WasteMgmt_SharpsLabeled_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WasteMgmt_SharpsLabeled_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WasteMgmt_SharpsLabeled_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WasteMgmt_SharpsLabeled_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WasteMgmt_SharpsLabeled_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WasteMgmt_SharpsNotOverfilled_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WasteMgmt_SharpsNotOverfilled_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WasteMgmt_SharpsNotOverfilled_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WasteMgmt_SharpsNotOverfilled_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WasteMgmt_SharpsNotOverfilled_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refrigerator_TempChecked_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Refrigerator_TempChecked_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Refrigerator_TempChecked_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refrigerator_TempChecked_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Refrigerator_TempChecked_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refrigerator_Dedicated_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Refrigerator_Dedicated_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Refrigerator_Dedicated_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Refrigerator_Dedicated_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Refrigerator_Dedicated_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medications_NoExpired_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medications_NoExpired_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medications_NoExpired_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medications_NoExpired_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Medications_NoExpired_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medications_VialsDated_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medications_VialsDated_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medications_VialsDated_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medications_VialsDated_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Medications_VialsDated_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medications_VaccinesStored_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medications_VaccinesStored_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medications_VaccinesStored_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medications_VaccinesStored_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Medications_VaccinesStored_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medications_NoLooseNeedles_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medications_NoLooseNeedles_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medications_NoLooseNeedles_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medications_NoLooseNeedles_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Medications_NoLooseNeedles_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Misc_SpecimensLabeled_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Misc_SpecimensLabeled_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Misc_SpecimensLabeled_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Misc_SpecimensLabeled_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Misc_SpecimensLabeled_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Misc_StaffHygienePPE_Finding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Misc_StaffHygienePPE_ActionDone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Misc_StaffHygienePPE_Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Misc_StaffHygienePPE_FuDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Misc_StaffHygienePPE_FuRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitAreaStaffSignature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FollowUpDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfectionControlMonitoringForm", x => x.Id);
                });

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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    MDROOrganism = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "MDROrderSheets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Diagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HospitalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecimenType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMRSA = table.Column<bool>(type: "bit", nullable: false),
                    IsVRE = table.Column<bool>(type: "bit", nullable: false),
                    IsMRSE = table.Column<bool>(type: "bit", nullable: false),
                    IsESBL = table.Column<bool>(type: "bit", nullable: false),
                    IsCRE = table.Column<bool>(type: "bit", nullable: false),
                    IsMDRSpneu = table.Column<bool>(type: "bit", nullable: false),
                    IsMDRGNB = table.Column<bool>(type: "bit", nullable: false),
                    CollectionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExplainSituationToPatient = table.Column<bool>(type: "bit", nullable: false),
                    PlaceInSingleRoom = table.Column<bool>(type: "bit", nullable: false),
                    PutContactSignage = table.Column<bool>(type: "bit", nullable: false),
                    LimitVisitors = table.Column<bool>(type: "bit", nullable: false),
                    DedicateEquipment = table.Column<bool>(type: "bit", nullable: false),
                    EnsureHandRubAvailability = table.Column<bool>(type: "bit", nullable: false),
                    EnsurePPEAvailability = table.Column<bool>(type: "bit", nullable: false),
                    AssignDedicatedStaff = table.Column<bool>(type: "bit", nullable: false),
                    DiscardPPEBeforeExit = table.Column<bool>(type: "bit", nullable: false),
                    DisinfectHorizontalSurfaces = table.Column<bool>(type: "bit", nullable: false),
                    DisinfectHighTouchAreas = table.Column<bool>(type: "bit", nullable: false),
                    TerminalCleaning = table.Column<bool>(type: "bit", nullable: false),
                    LiftPrecautionsWithIPCApproval = table.Column<bool>(type: "bit", nullable: false),
                    PhysicianSignatureAndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NurseSignatureAndDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MDROrderSheets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NoticeOfReferral",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOA = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DOD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InitialDiagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HAAT = table.Column<bool>(type: "bit", nullable: false),
                    Communicable = table.Column<bool>(type: "bit", nullable: false),
                    Fever = table.Column<bool>(type: "bit", nullable: false),
                    PostOp = table.Column<bool>(type: "bit", nullable: false),
                    ReAdmitted = table.Column<bool>(type: "bit", nullable: false),
                    Laboratory = table.Column<bool>(type: "bit", nullable: false),
                    Radiology = table.Column<bool>(type: "bit", nullable: false),
                    Referredby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredbyDnT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Receivedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedbyDnT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoticeOfReferral", x => x.Id);
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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    UnitWardArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfIntubation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Investigator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfAdmission = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispositionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DispositionTransfer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDRO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDROOrganism = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    HospitalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    UnitWardArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Investigator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfAdmission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispositionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DispositionTransfer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDRO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDROOrganism = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PNEU_Subclass = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    NonCultureDiagnostic3 = table.Column<bool>(type: "bit", nullable: false),
                    LaboratoryCriteria3 = table.Column<bool>(type: "bit", nullable: false)
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
                name: "SSIP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientDiagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HospitalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Operation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surgeon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nurse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ORLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SexGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AntimicrobialProphylaxisYes = table.Column<bool>(type: "bit", nullable: false),
                    AntimicrobialProphylaxisReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurgicalHandScrubYes = table.Column<bool>(type: "bit", nullable: false),
                    SurgicalHandScrubReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AppropriatePPEYes = table.Column<bool>(type: "bit", nullable: false),
                    AppropriatePPEReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientCoveredYes = table.Column<bool>(type: "bit", nullable: false),
                    PatientCoveredReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrapeSeparatesInstrumentsYes = table.Column<bool>(type: "bit", nullable: false),
                    DrapeSeparatesInstrumentsReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProperVentilationYes = table.Column<bool>(type: "bit", nullable: false),
                    ProperVentilationReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurfacesCleanedYes = table.Column<bool>(type: "bit", nullable: false),
                    SurfacesCleanedReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstrumentsSterilizedYes = table.Column<bool>(type: "bit", nullable: false),
                    InstrumentsSterilizedReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FootTrafficMinimizedYes = table.Column<bool>(type: "bit", nullable: false),
                    FootTrafficMinimizedReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SkinCleanedPriorYes = table.Column<bool>(type: "bit", nullable: false),
                    SkinCleanedPriorReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HairRemovedYes = table.Column<bool>(type: "bit", nullable: false),
                    HairRemovedReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGlucoseMonitoredYes = table.Column<bool>(type: "bit", nullable: false),
                    BloodGlucoseMonitoredReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostOpWoundCareYes = table.Column<bool>(type: "bit", nullable: false),
                    PostOpWoundCareReminder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    procedurenotes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSIP", x => x.Id);
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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    UnitWardArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Investigator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfAdmission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispositionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DispositionTransfer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDRO = table.Column<bool>(type: "bit", nullable: false),
                    MDROOrganism = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InfectionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BurnAppearanceChange = table.Column<bool>(type: "bit", nullable: false),
                    BurnOrganismIdentified = table.Column<bool>(type: "bit", nullable: false),
                    BurnCultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BurnCultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DecubitusErythema = table.Column<bool>(type: "bit", nullable: false),
                    DecubitusTenderness = table.Column<bool>(type: "bit", nullable: false),
                    DecubitusSwelling = table.Column<bool>(type: "bit", nullable: false),
                    DecubitusOrganismIdentified = table.Column<bool>(type: "bit", nullable: false),
                    DecubitusCultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DecubitusCultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STOrganismIdentified = table.Column<bool>(type: "bit", nullable: false),
                    STDrainage = table.Column<bool>(type: "bit", nullable: false),
                    STAbscess = table.Column<bool>(type: "bit", nullable: false),
                    STCultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    STCultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    SkinCultureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SkinCultureResults = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSTInfectionModels", x => x.SSTID);
                });

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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    UnitWardArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfOperation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Investigator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurgeryDone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfAdmission = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DispositionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DispositionTransfer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDRO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDROOrganism = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false),
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
                name: "tbPatientMovement",
                columns: table => new
                {
                    MovementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovementDate = table.Column<DateTime>(type: "date", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdmissionCount = table.Column<int>(type: "int", nullable: false),
                    TransferInCount = table.Column<int>(type: "int", nullable: false),
                    SentHomeCount = table.Column<int>(type: "int", nullable: false),
                    MortalityCount = table.Column<int>(type: "int", nullable: false),
                    TransferOutCount = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbPatientMovement", x => x.MovementId);
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
                    HospitalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    MDROOrganism = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    MDROOrganism = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                name: "VAEMonitoringChecklist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AreaUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssessedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateandTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObservedStaff = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadofBed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IfTracheostomy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrcalCare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HandHygiene = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AfterCare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SterileWater = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnsuretoUse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Condensateinthe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntubationKits = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CleanandDirty = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberofComplaint = table.Column<int>(type: "int", nullable: true),
                    TotalObserved = table.Column<int>(type: "int", nullable: true),
                    ComplianceRate = table.Column<float>(type: "real", nullable: true),
                    Accomplishedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewedandApproved = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VAEMonitoringChecklist", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VentilatorEventChecklists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HospitalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    MDROOrganism = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofIntubation = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TypeClass = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    PvapResult = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VaeRemarks = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "PostConstruction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ICRAId = table.Column<int>(type: "int", nullable: false),
                    ProjectReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectNameAndDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecificSiteOfActivity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstimatedDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeforeHoarding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeforeHoardingDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FacilityBased = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FacilityBasedDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AfterRemoval = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AfterRemovalDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhereRequired = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhereRequiredlDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaIs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaIsDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntegrityofWalls = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntegrityofWallsDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SurfaceinPatient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SurfaceinPatientDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaSurfaces = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaSurfacesDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IfPlumbinghasbeenAffected = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IfPlumbinghasbeenAffectedDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlumbingifAffected = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlumbingifAffectedDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectHandWashing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectHandWashingDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FaucetAerators = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FaucetAeratorsDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CeilingTiles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CeilingTilesDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HVACSystems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HVACSystemsDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorrectRoomPressurization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectRoomPressurizationDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllMechanicalSpaces = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllMechanicalSpacesDC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCompleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContractorSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineeringSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ICPSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitAreaRep = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostConstruction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostConstruction_ICRA_ICRAId",
                        column: x => x.ICRAId,
                        principalTable: "ICRA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TCSkillsChecklist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ICRAId = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObserverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEquipmentAndCartPrepared = table.Column<bool>(type: "bit", nullable: false),
                    IsCleaningSolutionPrepared = table.Column<bool>(type: "bit", nullable: false),
                    IsProperAttireAndPPEWorn = table.Column<bool>(type: "bit", nullable: false),
                    IsHandHygieneAndGlovesDone = table.Column<bool>(type: "bit", nullable: false),
                    IsSignageChecked = table.Column<bool>(type: "bit", nullable: false),
                    IsSpillSoakedWithSolution = table.Column<bool>(type: "bit", nullable: false),
                    IsWallsCleaned = table.Column<bool>(type: "bit", nullable: false),
                    IsDoorFrameWiped = table.Column<bool>(type: "bit", nullable: false),
                    IsWindowSillAndWindowCleaned = table.Column<bool>(type: "bit", nullable: false),
                    IsHighTouchAreasWiped = table.Column<bool>(type: "bit", nullable: false),
                    IsVerticalSurfacesWiped = table.Column<bool>(type: "bit", nullable: false),
                    IsLooseDebrisPickedUp = table.Column<bool>(type: "bit", nullable: false),
                    IsRoomFloorMopped = table.Column<bool>(type: "bit", nullable: false),
                    IsUsedClothsDisposed = table.Column<bool>(type: "bit", nullable: false),
                    IsWasteContainersEmptied = table.Column<bool>(type: "bit", nullable: false),
                    IsInfectiousWasteRemoved = table.Column<bool>(type: "bit", nullable: false),
                    IsMirrorCleaned = table.Column<bool>(type: "bit", nullable: false),
                    IsSinkAreaCleaned = table.Column<bool>(type: "bit", nullable: false),
                    IsFaucetAndHandlesCleaned = table.Column<bool>(type: "bit", nullable: false),
                    IsToiletAndFlushHandlesCleaned = table.Column<bool>(type: "bit", nullable: false),
                    IsOtherBathroomSurfacesCleaned = table.Column<bool>(type: "bit", nullable: false),
                    IsBathroomFloorScrubbed = table.Column<bool>(type: "bit", nullable: false),
                    IsColorCodedWasteEmptied = table.Column<bool>(type: "bit", nullable: false),
                    IsPPERemoved = table.Column<bool>(type: "bit", nullable: false),
                    IsHandHygieneAfterPPE = table.Column<bool>(type: "bit", nullable: false),
                    IsGlovesRemovedAndHandHygieneDone = table.Column<bool>(type: "bit", nullable: false),
                    PreCleaningItems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostCleaningItems = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecommendationsOrActions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitAreaStaffSignature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfObservation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TCSkillsChecklist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TCSkillsChecklist_ICRA_ICRAId",
                        column: x => x.ICRAId,
                        principalTable: "ICRA",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceConnected",
                columns: table => new
                {
                    DeviceId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    DeviceType = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DeviceClass = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
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
                name: "tbPatientMovementDetail",
                columns: table => new
                {
                    MovementDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovementId = table.Column<int>(type: "int", nullable: false),
                    HospNum = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    IdNum = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MovementType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SourceArea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DestinationArea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MovementDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbPatientMovementDetail", x => x.MovementDetailId);
                    table.ForeignKey(
                        name: "FK_tbPatientMovementDetail_tbPatientMovement_MovementId",
                        column: x => x.MovementId,
                        principalTable: "tbPatientMovement",
                        principalColumn: "MovementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbPatientMovementDetail_tbmaster_HospNum",
                        column: x => x.HospNum,
                        principalTable: "tbmaster",
                        principalColumn: "HospNum",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_PostConstruction_ICRAId",
                table: "PostConstruction",
                column: "ICRAId");

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
                name: "IX_tbPatientMovement_MovementDate_Area",
                table: "tbPatientMovement",
                columns: new[] { "MovementDate", "Area" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbPatientMovementDetail_HospNum",
                table: "tbPatientMovementDetail",
                column: "HospNum");

            migrationBuilder.CreateIndex(
                name: "IX_tbPatientMovementDetail_MovementId",
                table: "tbPatientMovementDetail",
                column: "MovementId");

            migrationBuilder.CreateIndex(
                name: "IX_TCSkillsChecklist_ICRAId",
                table: "TCSkillsChecklist",
                column: "ICRAId");

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
                name: "ConstructionSites");

            migrationBuilder.DropTable(
                name: "DailyCentralLineMaintenanceChecklists");

            migrationBuilder.DropTable(
                name: "DeviceConnected");

            migrationBuilder.DropTable(
                name: "EvaluationViewModel");

            migrationBuilder.DropTable(
                name: "FitTestingFormHistory");

            migrationBuilder.DropTable(
                name: "GIInfectionChecklists");

            migrationBuilder.DropTable(
                name: "HandHygieneComplianceSummary");

            migrationBuilder.DropTable(
                name: "HHActivities");

            migrationBuilder.DropTable(
                name: "InfectionControlMonitoringForm");

            migrationBuilder.DropTable(
                name: "Insertion");

            migrationBuilder.DropTable(
                name: "LaboratoryConfirmedBSI");

            migrationBuilder.DropTable(
                name: "MDROrderSheets");

            migrationBuilder.DropTable(
                name: "NoticeOfReferral");

            migrationBuilder.DropTable(
                name: "PatientForms");

            migrationBuilder.DropTable(
                name: "PediatricVAEChecklist");

            migrationBuilder.DropTable(
                name: "Pneumonias");

            migrationBuilder.DropTable(
                name: "PostConstruction");

            migrationBuilder.DropTable(
                name: "PostTestClinicals");

            migrationBuilder.DropTable(
                name: "PostTestNonCLinicals");

            migrationBuilder.DropTable(
                name: "PreTestClinicals");

            migrationBuilder.DropTable(
                name: "PreTestNonClinicals");

            migrationBuilder.DropTable(
                name: "SSIP");

            migrationBuilder.DropTable(
                name: "SSTInfectionModels");

            migrationBuilder.DropTable(
                name: "SurgicalSiteInfectionChecklist");

            migrationBuilder.DropTable(
                name: "tbdiagnosticstreatments");

            migrationBuilder.DropTable(
                name: "tbpatient");

            migrationBuilder.DropTable(
                name: "tbPatientMovementDetail");

            migrationBuilder.DropTable(
                name: "TCSkillsChecklist");

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
                name: "VAEMonitoringChecklist");

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
                name: "tbPatientMovement");

            migrationBuilder.DropTable(
                name: "ICRA");

            migrationBuilder.DropTable(
                name: "tbmaster");
        }
    }
}
