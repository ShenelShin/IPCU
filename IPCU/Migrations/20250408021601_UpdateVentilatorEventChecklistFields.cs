using Microsoft.EntityFrameworkCore.Migrations;

namespace IPCU.Migrations
{
    public partial class UpdateVentilatorEventChecklistFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Rename columns to match the target schema with correct casing
            migrationBuilder.RenameColumn(
                name: "FName",
                table: "VentilatorEventChecklists",
                newName: "Fname");

            migrationBuilder.RenameColumn(
                name: "MName",
                table: "VentilatorEventChecklists",
                newName: "Mname");

            migrationBuilder.RenameColumn(
                name: "LName",
                table: "VentilatorEventChecklists",
                newName: "Lname");

            migrationBuilder.RenameColumn(
                name: "HospNum",
                table: "VentilatorEventChecklists",
                newName: "HospitalNumber");

            migrationBuilder.RenameColumn(
                name: "UwArea",
                table: "VentilatorEventChecklists",
                newName: "UnitWardArea");

            migrationBuilder.RenameColumn(
                name: "NameOfInvestigator",
                table: "VentilatorEventChecklists",
                newName: "Investigator");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Restore original column names with original casing
            migrationBuilder.RenameColumn(
                name: "Fname",
                table: "VentilatorEventChecklists",
                newName: "FName");

            migrationBuilder.RenameColumn(
                name: "Mname",
                table: "VentilatorEventChecklists",
                newName: "MName");

            migrationBuilder.RenameColumn(
                name: "Lname",
                table: "VentilatorEventChecklists",
                newName: "LName");

            migrationBuilder.RenameColumn(
                name: "HospitalNumber",
                table: "VentilatorEventChecklists",
                newName: "HospNum");

            migrationBuilder.RenameColumn(
                name: "UnitWardArea",
                table: "VentilatorEventChecklists",
                newName: "UwArea");

            migrationBuilder.RenameColumn(
                name: "Investigator",
                table: "VentilatorEventChecklists",
                newName: "NameOfInvestigator");
        }
    }
}