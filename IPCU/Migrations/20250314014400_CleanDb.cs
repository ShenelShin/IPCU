using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class CleanDb : Migration
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
                name: "FitTestingForm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                    PRETCSCORE = table.Column<int>(type: "int", nullable: false)
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
                    PRETNONCSCORE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreTestNonClinicals", x => x.Id);
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
                name: "IX_FitTestingFormHistory_FitTestingFormId",
                table: "FitTestingFormHistory",
                column: "FitTestingFormId");

            migrationBuilder.CreateIndex(
                name: "IX_HHActivities_HHId",
                table: "HHActivities",
                column: "HHId");
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
                name: "FitTestingFormHistory");

            migrationBuilder.DropTable(
                name: "HandHygieneComplianceSummary");

            migrationBuilder.DropTable(
                name: "HHActivities");

            migrationBuilder.DropTable(
                name: "PatientForms");

            migrationBuilder.DropTable(
                name: "PreTestClinicals");

            migrationBuilder.DropTable(
                name: "PreTestNonClinicals");

            migrationBuilder.DropTable(
                name: "Trainees");

            migrationBuilder.DropTable(
                name: "TrainingEvaluation");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "FitTestingForm");

            migrationBuilder.DropTable(
                name: "HandHygieneForms");
        }
    }
}
