using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IPCU.Migrations
{
    public partial class AddAntibiotic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create Diagnostics table
            migrationBuilder.CreateTable(
                name: "tbdiagnostics",
                columns: table => new
                {
                    DiagId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCollection = table.Column<DateTime>(type: "date", nullable: true),
                    SourceSite = table.Column<string>(maxLength: 100, nullable: true),
                    IsolateFindingsResult = table.Column<string>(maxLength: 500, nullable: true),
                    HospNum = table.Column<string>(maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbdiagnostics", x => x.DiagId);
                    table.ForeignKey(
                        name: "FK_tbdiagnostics_tbmaster_HospNum",
                        column: x => x.HospNum,
                        principalTable: "tbmaster",
                        principalColumn: "HospNum",
                        onDelete: ReferentialAction.Restrict);
                });

            // Create Antibiotics table
            migrationBuilder.CreateTable(
                name: "tbantibiotics",
                columns: table => new
                {
                    AntibioticId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    DateAdded = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbantibiotics", x => x.AntibioticId);
                });

            // Create DiagnosticsTreatments junction table
            migrationBuilder.CreateTable(
                name: "tbdiagnosticstreatments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiagId = table.Column<int>(nullable: false),
                    AntibioticId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbdiagnosticstreatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbdiagnosticstreatments_tbantibiotics_AntibioticId",
                        column: x => x.AntibioticId,
                        principalTable: "tbantibiotics",
                        principalColumn: "AntibioticId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbdiagnosticstreatments_tbdiagnostics_DiagId",
                        column: x => x.DiagId,
                        principalTable: "tbdiagnostics",
                        principalColumn: "DiagId",
                        onDelete: ReferentialAction.Cascade);
                });

            // Create indexes for better query performance
            migrationBuilder.CreateIndex(
                name: "IX_tbdiagnostics_HospNum",
                table: "tbdiagnostics",
                column: "HospNum");

            migrationBuilder.CreateIndex(
                name: "IX_tbdiagnosticstreatments_AntibioticId",
                table: "tbdiagnosticstreatments",
                column: "AntibioticId");

            migrationBuilder.CreateIndex(
                name: "IX_tbdiagnosticstreatments_DiagId",
                table: "tbdiagnosticstreatments",
                column: "DiagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop tables in reverse order to handle dependencies
            migrationBuilder.DropTable(
                name: "tbdiagnosticstreatments");

            migrationBuilder.DropTable(
                name: "tbantibiotics");

            migrationBuilder.DropTable(
                name: "tbdiagnostics");
        }
    }
}