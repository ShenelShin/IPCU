using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class ENVIz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "EnvironmentalzControl");

            migrationBuilder.DropColumn(
                name: "Fname",
                table: "EnvironmentalzControl");

            migrationBuilder.DropColumn(
                name: "Lname",
                table: "EnvironmentalzControl");

            migrationBuilder.DropColumn(
                name: "Mname",
                table: "EnvironmentalzControl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "EnvironmentalzControl",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Fname",
                table: "EnvironmentalzControl",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lname",
                table: "EnvironmentalzControl",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mname",
                table: "EnvironmentalzControl",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
