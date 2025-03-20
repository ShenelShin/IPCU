using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class EvaluationSummary2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationSummaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalEvaluations = table.Column<int>(type: "int", nullable: false),
                    MaleCount = table.Column<int>(type: "int", nullable: false),
                    FemaleCount = table.Column<int>(type: "int", nullable: false),
                    AvgFlowFollowed = table.Column<double>(type: "float", nullable: false),
                    AvgRulesEstablished = table.Column<double>(type: "float", nullable: false),
                    AvgInitiateDiscussion = table.Column<double>(type: "float", nullable: false),
                    AvgTechnicalCapability = table.Column<double>(type: "float", nullable: false),
                    AvgContentOrganization = table.Column<double>(type: "float", nullable: false),
                    AvgObjectiveStated = table.Column<double>(type: "float", nullable: false),
                    AvgContentQuality = table.Column<double>(type: "float", nullable: false),
                    AvgFlowOfTopic = table.Column<double>(type: "float", nullable: false),
                    AvgRelevanceOfTopic = table.Column<double>(type: "float", nullable: false),
                    AvgPracticeApplication = table.Column<double>(type: "float", nullable: false),
                    AvgLearningActivities = table.Column<double>(type: "float", nullable: false),
                    AvgVisualAids = table.Column<double>(type: "float", nullable: false),
                    AvgPresentKnowledge = table.Column<double>(type: "float", nullable: false),
                    AvgBalancePrinciples = table.Column<double>(type: "float", nullable: false),
                    AvgAddressClarifications = table.Column<double>(type: "float", nullable: false),
                    AvgPreparedness = table.Column<double>(type: "float", nullable: false),
                    AvgTeachingPersonality = table.Column<double>(type: "float", nullable: false),
                    AvgEstablishRapport = table.Column<double>(type: "float", nullable: false),
                    AvgRespectForParticipants = table.Column<double>(type: "float", nullable: false),
                    AvgVoicePersonality = table.Column<double>(type: "float", nullable: false),
                    AvgTimeManagement = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationSummaries", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationSummaries");
        }
    }
}
