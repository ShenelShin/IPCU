using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class ICRARISK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalNumber",
                table: "ICRA");

            migrationBuilder.DropColumn(
                name: "RiskGroup",
                table: "ICRA");

            migrationBuilder.AddColumn<string>(
                name: "LocalNumber_Above",
                table: "ICRA",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalNumber_Behind",
                table: "ICRA",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalNumber_Below",
                table: "ICRA",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalNumber_Front",
                table: "ICRA",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LocalNumber_Lateral",
                table: "ICRA",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RiskGroup_Above",
                table: "ICRA",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RiskGroup_Behind",
                table: "ICRA",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RiskGroup_Below",
                table: "ICRA",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RiskGroup_Front",
                table: "ICRA",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RiskGroup_Lateral",
                table: "ICRA",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalNumber_Above",
                table: "ICRA");

            migrationBuilder.DropColumn(
                name: "LocalNumber_Behind",
                table: "ICRA");

            migrationBuilder.DropColumn(
                name: "LocalNumber_Below",
                table: "ICRA");

            migrationBuilder.DropColumn(
                name: "LocalNumber_Front",
                table: "ICRA");

            migrationBuilder.DropColumn(
                name: "LocalNumber_Lateral",
                table: "ICRA");

            migrationBuilder.DropColumn(
                name: "RiskGroup_Above",
                table: "ICRA");

            migrationBuilder.DropColumn(
                name: "RiskGroup_Behind",
                table: "ICRA");

            migrationBuilder.DropColumn(
                name: "RiskGroup_Below",
                table: "ICRA");

            migrationBuilder.DropColumn(
                name: "RiskGroup_Front",
                table: "ICRA");

            migrationBuilder.DropColumn(
                name: "RiskGroup_Lateral",
                table: "ICRA");

            migrationBuilder.AddColumn<string>(
                name: "LocalNumber",
                table: "ICRA",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RiskGroup",
                table: "ICRA",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
