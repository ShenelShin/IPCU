using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class AddFinalRatingToTrainingEvaluation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalResponses",
                table: "TrainingEvaluation");

            migrationBuilder.AddColumn<double>(
                name: "FinalRating",
                table: "TrainingEvaluation",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PostTestEvaluationGrade",
                table: "TrainingEvaluation",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalRating",
                table: "TrainingEvaluation");

            migrationBuilder.DropColumn(
                name: "PostTestEvaluationGrade",
                table: "TrainingEvaluation");

            migrationBuilder.AddColumn<int>(
                name: "TotalResponses",
                table: "TrainingEvaluation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
