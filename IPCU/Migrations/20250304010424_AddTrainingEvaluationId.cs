using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainingEvaluationId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    FlowFollowed = table.Column<int>(type: "int", nullable: false),
                    RulesEstablished = table.Column<int>(type: "int", nullable: false),
                    InitiateDiscussion = table.Column<int>(type: "int", nullable: false),
                    TechnicalCapability = table.Column<int>(type: "int", nullable: false),
                    ContentOrganization = table.Column<int>(type: "int", nullable: false),
                    ObjectiveStated = table.Column<int>(type: "int", nullable: false),
                    ContentQuality = table.Column<int>(type: "int", nullable: false),
                    FlowOfTopic = table.Column<int>(type: "int", nullable: false),
                    RelevanceOfTopic = table.Column<int>(type: "int", nullable: false),
                    PracticeApplication = table.Column<int>(type: "int", nullable: false),
                    LearningActivities = table.Column<int>(type: "int", nullable: false),
                    VisualAids = table.Column<int>(type: "int", nullable: false),
                    PresentKnowledge = table.Column<int>(type: "int", nullable: false),
                    BalancePrinciples = table.Column<int>(type: "int", nullable: false),
                    AddressClarifications = table.Column<int>(type: "int", nullable: false),
                    Preparedness = table.Column<int>(type: "int", nullable: false),
                    TeachingPersonality = table.Column<int>(type: "int", nullable: false),
                    EstablishRapport = table.Column<int>(type: "int", nullable: false),
                    RespectForParticipants = table.Column<int>(type: "int", nullable: false),
                    VoicePersonality = table.Column<int>(type: "int", nullable: false),
                    TimeManagement = table.Column<int>(type: "int", nullable: false),
                    SuggestionsForImprovement = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingEvaluation", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingEvaluation");
        }
    }
}
