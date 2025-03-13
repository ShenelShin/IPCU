using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class PreClinicalQuiz3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PreTestClinical",
                table: "PreTestClinical");

            migrationBuilder.RenameTable(
                name: "PreTestClinical",
                newName: "PreTestClinicals");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreTestClinicals",
                table: "PreTestClinicals",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PreTestClinicals",
                table: "PreTestClinicals");

            migrationBuilder.RenameTable(
                name: "PreTestClinicals",
                newName: "PreTestClinical");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PreTestClinical",
                table: "PreTestClinical",
                column: "Id");
        }
    }
}
