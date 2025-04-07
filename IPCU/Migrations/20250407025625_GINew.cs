using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class GINew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MName",
                table: "GIInfectionChecklists",
                newName: "Mname");

            migrationBuilder.RenameColumn(
                name: "LName",
                table: "GIInfectionChecklists",
                newName: "Lname");

            migrationBuilder.RenameColumn(
                name: "FName",
                table: "GIInfectionChecklists",
                newName: "Fname");

            migrationBuilder.RenameColumn(
                name: "TypeClassification",
                table: "GIInfectionChecklists",
                newName: "TypeClass");

            migrationBuilder.RenameColumn(
                name: "NameOfInvestigator",
                table: "GIInfectionChecklists",
                newName: "Investigator");

            migrationBuilder.RenameColumn(
                name: "HospNum",
                table: "GIInfectionChecklists",
                newName: "HospitalNumber");

            migrationBuilder.RenameColumn(
                name: "DispositionArea",
                table: "GIInfectionChecklists",
                newName: "DispositionTransfer");

            migrationBuilder.AlterColumn<string>(
                name: "MDRO",
                table: "GIInfectionChecklists",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DispositionDate",
                table: "GIInfectionChecklists",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Disposition",
                table: "GIInfectionChecklists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfEvent",
                table: "GIInfectionChecklists",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "GIInfectionChecklists",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAdmission",
                table: "GIInfectionChecklists",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Mname",
                table: "GIInfectionChecklists",
                newName: "MName");

            migrationBuilder.RenameColumn(
                name: "Lname",
                table: "GIInfectionChecklists",
                newName: "LName");

            migrationBuilder.RenameColumn(
                name: "Fname",
                table: "GIInfectionChecklists",
                newName: "FName");

            migrationBuilder.RenameColumn(
                name: "TypeClass",
                table: "GIInfectionChecklists",
                newName: "TypeClassification");

            migrationBuilder.RenameColumn(
                name: "Investigator",
                table: "GIInfectionChecklists",
                newName: "NameOfInvestigator");

            migrationBuilder.RenameColumn(
                name: "HospitalNumber",
                table: "GIInfectionChecklists",
                newName: "HospNum");

            migrationBuilder.RenameColumn(
                name: "DispositionTransfer",
                table: "GIInfectionChecklists",
                newName: "DispositionArea");

            migrationBuilder.AlterColumn<bool>(
                name: "MDRO",
                table: "GIInfectionChecklists",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DispositionDate",
                table: "GIInfectionChecklists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Disposition",
                table: "GIInfectionChecklists",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfEvent",
                table: "GIInfectionChecklists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfBirth",
                table: "GIInfectionChecklists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfAdmission",
                table: "GIInfectionChecklists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
