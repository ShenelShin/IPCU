using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class merge3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationSummaries");

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

            migrationBuilder.CreateIndex(
                name: "IX_DeviceConnected_HospNum",
                table: "DeviceConnected",
                column: "HospNum");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceConnected");

            migrationBuilder.CreateTable(
                name: "EvaluationSummaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AvgAddressClarifications = table.Column<double>(type: "float", nullable: false),
                    AvgBalancePrinciples = table.Column<double>(type: "float", nullable: false),
                    AvgContentOrganization = table.Column<double>(type: "float", nullable: false),
                    AvgContentQuality = table.Column<double>(type: "float", nullable: false),
                    AvgEstablishRapport = table.Column<double>(type: "float", nullable: false),
                    AvgFlowFollowed = table.Column<double>(type: "float", nullable: false),
                    AvgFlowOfTopic = table.Column<double>(type: "float", nullable: false),
                    AvgInitiateDiscussion = table.Column<double>(type: "float", nullable: false),
                    AvgLearningActivities = table.Column<double>(type: "float", nullable: false),
                    AvgObjectiveStated = table.Column<double>(type: "float", nullable: false),
                    AvgPracticeApplication = table.Column<double>(type: "float", nullable: false),
                    AvgPreparedness = table.Column<double>(type: "float", nullable: false),
                    AvgPresentKnowledge = table.Column<double>(type: "float", nullable: false),
                    AvgRelevanceOfTopic = table.Column<double>(type: "float", nullable: false),
                    AvgRespectForParticipants = table.Column<double>(type: "float", nullable: false),
                    AvgRulesEstablished = table.Column<double>(type: "float", nullable: false),
                    AvgTeachingPersonality = table.Column<double>(type: "float", nullable: false),
                    AvgTechnicalCapability = table.Column<double>(type: "float", nullable: false),
                    AvgTimeManagement = table.Column<double>(type: "float", nullable: false),
                    AvgVisualAids = table.Column<double>(type: "float", nullable: false),
                    AvgVoicePersonality = table.Column<double>(type: "float", nullable: false),
                    FemaleCount = table.Column<int>(type: "int", nullable: false),
                    MaleCount = table.Column<int>(type: "int", nullable: false),
                    TotalEvaluations = table.Column<int>(type: "int", nullable: false),
                    TrainingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationSummaries", x => x.Id);
                });
        }
    }
}
