using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class TrainingEvaluation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    table.PrimaryKey("PK_EvaluationViewModel", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationViewModel");
        }
    }
}
