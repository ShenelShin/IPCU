using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class Environment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "TCSkillsChecklistReal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Fname",
                table: "TCSkillsChecklistReal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lname",
                table: "TCSkillsChecklistReal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mname",
                table: "TCSkillsChecklistReal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Fname",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lname",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Mname",
                table: "InfectionControlMonitoringForm",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "TCSkillsChecklistReal");

            migrationBuilder.DropColumn(
                name: "Fname",
                table: "TCSkillsChecklistReal");

            migrationBuilder.DropColumn(
                name: "Lname",
                table: "TCSkillsChecklistReal");

            migrationBuilder.DropColumn(
                name: "Mname",
                table: "TCSkillsChecklistReal");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Fname",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Lname",
                table: "InfectionControlMonitoringForm");

            migrationBuilder.DropColumn(
                name: "Mname",
                table: "InfectionControlMonitoringForm");
        }
    }
}
