using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class Pneunolast2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersistentorProgressive",
                table: "Pneumonias");

            migrationBuilder.AddColumn<string>(
                name: "PersistentorProgressive1_1",
                table: "Pneumonias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersistentorProgressive1_2",
                table: "Pneumonias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersistentorProgressive1_3",
                table: "Pneumonias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersistentorProgressive2_1",
                table: "Pneumonias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersistentorProgressive2_2",
                table: "Pneumonias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersistentorProgressive3",
                table: "Pneumonias",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersistentorProgressive1_1",
                table: "Pneumonias");

            migrationBuilder.DropColumn(
                name: "PersistentorProgressive1_2",
                table: "Pneumonias");

            migrationBuilder.DropColumn(
                name: "PersistentorProgressive1_3",
                table: "Pneumonias");

            migrationBuilder.DropColumn(
                name: "PersistentorProgressive2_1",
                table: "Pneumonias");

            migrationBuilder.DropColumn(
                name: "PersistentorProgressive2_2",
                table: "Pneumonias");

            migrationBuilder.DropColumn(
                name: "PersistentorProgressive3",
                table: "Pneumonias");

            migrationBuilder.AddColumn<string>(
                name: "PersistentorProgressive",
                table: "Pneumonias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
