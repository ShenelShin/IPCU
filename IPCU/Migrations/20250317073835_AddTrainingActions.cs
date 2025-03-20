using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainingActions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SelectedOption",
                table: "TrainingSummaries",
                newName: "ProperHand");

            migrationBuilder.RenameColumn(
                name: "SelectedAction",
                table: "TrainingSummaries",
                newName: "IDPrinting");

            migrationBuilder.AddColumn<string>(
                name: "GloveRemoval",
                table: "TrainingSummaries",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GloveRemoval",
                table: "TrainingSummaries");

            migrationBuilder.RenameColumn(
                name: "ProperHand",
                table: "TrainingSummaries",
                newName: "SelectedOption");

            migrationBuilder.RenameColumn(
                name: "IDPrinting",
                table: "TrainingSummaries",
                newName: "SelectedAction");
        }
    }
}
