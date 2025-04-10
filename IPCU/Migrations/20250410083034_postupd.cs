using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class postupd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AfterRemovalDate",
                table: "PostConstruction");

            migrationBuilder.DropColumn(
                name: "AllMechanicalSpacesDate",
                table: "PostConstruction");

            migrationBuilder.DropColumn(
                name: "AreaIsDate",
                table: "PostConstruction");

            migrationBuilder.DropColumn(
                name: "AreaSurfacesDate",
                table: "PostConstruction");

            migrationBuilder.DropColumn(
                name: "BeforeHoardingDate",
                table: "PostConstruction");

            migrationBuilder.DropColumn(
                name: "CeilingTilesDate",
                table: "PostConstruction");

            migrationBuilder.DropColumn(
                name: "CorrectHandWashingDate",
                table: "PostConstruction");

            migrationBuilder.DropColumn(
                name: "CorrectRoomPressurizationDate",
                table: "PostConstruction");

            migrationBuilder.DropColumn(
                name: "FacilityBasedDate",
                table: "PostConstruction");

            migrationBuilder.DropColumn(
                name: "FaucetAeratorsDate",
                table: "PostConstruction");

            migrationBuilder.DropColumn(
                name: "HVACSystemsDate",
                table: "PostConstruction");

            migrationBuilder.DropColumn(
                name: "IfPlumbinghasbeenAffectedDate",
                table: "PostConstruction");

            migrationBuilder.DropColumn(
                name: "IntegrityofWallsDate",
                table: "PostConstruction");

            migrationBuilder.DropColumn(
                name: "PlumbingifAffectedDate",
                table: "PostConstruction");

            migrationBuilder.DropColumn(
                name: "SurfaceinPatientDate",
                table: "PostConstruction");

            migrationBuilder.RenameColumn(
                name: "WhereRequiredDate",
                table: "PostConstruction",
                newName: "DateCompleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateCompleted",
                table: "PostConstruction",
                newName: "WhereRequiredDate");

            migrationBuilder.AddColumn<DateTime>(
                name: "AfterRemovalDate",
                table: "PostConstruction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AllMechanicalSpacesDate",
                table: "PostConstruction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AreaIsDate",
                table: "PostConstruction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AreaSurfacesDate",
                table: "PostConstruction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BeforeHoardingDate",
                table: "PostConstruction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CeilingTilesDate",
                table: "PostConstruction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CorrectHandWashingDate",
                table: "PostConstruction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CorrectRoomPressurizationDate",
                table: "PostConstruction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FacilityBasedDate",
                table: "PostConstruction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FaucetAeratorsDate",
                table: "PostConstruction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HVACSystemsDate",
                table: "PostConstruction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IfPlumbinghasbeenAffectedDate",
                table: "PostConstruction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "IntegrityofWallsDate",
                table: "PostConstruction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PlumbingifAffectedDate",
                table: "PostConstruction",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SurfaceinPatientDate",
                table: "PostConstruction",
                type: "datetime2",
                nullable: true);
        }
    }
}
