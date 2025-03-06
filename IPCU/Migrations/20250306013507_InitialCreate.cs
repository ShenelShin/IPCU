using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HandHygieneForms",
                columns: table => new
                {
                    HHId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Observer = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    HCWType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Isolation = table.Column<bool>(type: "bit", nullable: false),
                    IsolationPrecaution = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ObsvPatientCare = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ObsvPatientEnvironment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ObsvPatientContact = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TotalCompliantActions = table.Column<int>(type: "int", nullable: false),
                    TotalObservedOpportunities = table.Column<int>(type: "int", nullable: false),
                    ComplianceRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HandHygieneForms", x => x.HHId);
                });

            migrationBuilder.CreateTable(
                name: "HHActivities",
                columns: table => new
                {
                    ActId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HHId = table.Column<int>(type: "int", nullable: false),
                    Activity = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BeforeHandRub = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BeforeHandWash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AfterHandRub = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AfterHandWash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Gloves = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HHActivities", x => x.ActId);
                    table.ForeignKey(
                        name: "FK_HHActivities_HandHygieneForms_HHId",
                        column: x => x.HHId,
                        principalTable: "HandHygieneForms",
                        principalColumn: "HHId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HHActivities_HHId",
                table: "HHActivities",
                column: "HHId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HHActivities");

            migrationBuilder.DropTable(
                name: "HandHygieneForms");
        }
    }
}
