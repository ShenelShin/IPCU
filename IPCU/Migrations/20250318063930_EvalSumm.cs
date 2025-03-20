using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class EvalSumm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalRating",
                table: "TrainingEvaluation");

            migrationBuilder.DropColumn(
                name: "PostTestEvaluationGrade",
                table: "TrainingEvaluation");

            migrationBuilder.DropColumn(
                name: "FinalRating",
                table: "EvaluationViewModel");

            migrationBuilder.AddColumn<int>(
                name: "TotalResponses",
                table: "TrainingEvaluation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "EvaluationViewModel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalResponses",
                table: "TrainingEvaluation");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "EvaluationViewModel");

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

            migrationBuilder.AddColumn<double>(
                name: "FinalRating",
                table: "EvaluationViewModel",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
