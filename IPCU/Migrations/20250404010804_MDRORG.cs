using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class MDRORG : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MDROOrganism",
                table: "VentilatorEventChecklists",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "MDROOrganism",
                table: "UTIModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MDROOrganism",
                table: "Usi",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MDROOrganism",
                table: "SurgicalSiteInfectionChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MDROOrganism",
                table: "SSTInfectionModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MDROOrganism",
                table: "Pneumonias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MDROOrganism",
                table: "PediatricVAEChecklist",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MDROOrganism",
                table: "LaboratoryConfirmedBSI",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MDROOrganism",
                table: "GIInfectionChecklists",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MDROOrganism",
                table: "CardiovascularSystemInfection",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MDROOrganism",
                table: "UTIModels");

            migrationBuilder.DropColumn(
                name: "MDROOrganism",
                table: "Usi");

            migrationBuilder.DropColumn(
                name: "MDROOrganism",
                table: "SurgicalSiteInfectionChecklist");

            migrationBuilder.DropColumn(
                name: "MDROOrganism",
                table: "SSTInfectionModels");

            migrationBuilder.DropColumn(
                name: "MDROOrganism",
                table: "Pneumonias");

            migrationBuilder.DropColumn(
                name: "MDROOrganism",
                table: "PediatricVAEChecklist");

            migrationBuilder.DropColumn(
                name: "MDROOrganism",
                table: "LaboratoryConfirmedBSI");

            migrationBuilder.DropColumn(
                name: "MDROOrganism",
                table: "GIInfectionChecklists");

            migrationBuilder.DropColumn(
                name: "MDROOrganism",
                table: "CardiovascularSystemInfection");

            migrationBuilder.AlterColumn<string>(
                name: "MDROOrganism",
                table: "VentilatorEventChecklists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
