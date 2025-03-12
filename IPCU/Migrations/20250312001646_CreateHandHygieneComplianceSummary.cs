using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class CreateHandHygieneComplianceSummary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HandHygieneComplianceSummary",
                columns: table => new
                {
                    SummaryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<DateTime>(nullable: false),
                    SummaryType = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    TotalCompliantActions = table.Column<int>(nullable: false),
                    TotalObservedOpportunities = table.Column<int>(nullable: false),
                    ComplianceRate = table.Column<decimal>(nullable: false),
                    GeneratedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HandHygieneComplianceSummary", x => x.SummaryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HandHygieneComplianceSummary");
        }
    }
}
