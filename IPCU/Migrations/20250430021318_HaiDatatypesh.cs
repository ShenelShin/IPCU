using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class HaiDatatypesh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HaiCount",
                table: "tbmaster");

            migrationBuilder.DropColumn(
                name: "HaiStatus",
                table: "tbmaster");

            migrationBuilder.CreateTable(
                name: "tbPatientHAI",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospNum = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    HaiStatus = table.Column<bool>(type: "bit", nullable: false),
                    HaiCount = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbPatientHAI", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbPatientHAI_tbmaster_HospNum",
                        column: x => x.HospNum,
                        principalTable: "tbmaster",
                        principalColumn: "HospNum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbPatientHAI_HospNum",
                table: "tbPatientHAI",
                column: "HospNum",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbPatientHAI");

            migrationBuilder.AddColumn<int>(
                name: "HaiCount",
                table: "tbmaster",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HaiStatus",
                table: "tbmaster",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
