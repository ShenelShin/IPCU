using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class AddFitTestingFormHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FitTestingFormHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FitTestingFormId = table.Column<int>(type: "int", nullable: false),
                    Fit_Test_Solution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sensitivity_Test = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Respiratory_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Normal_Breathing = table.Column<bool>(type: "bit", nullable: false),
                    Deep_Breathing = table.Column<bool>(type: "bit", nullable: false),
                    Turn_head_side_to_side = table.Column<bool>(type: "bit", nullable: false),
                    Move_head_up_and_down = table.Column<bool>(type: "bit", nullable: false),
                    Reading = table.Column<bool>(type: "bit", nullable: false),
                    Bending_Jogging = table.Column<bool>(type: "bit", nullable: false),
                    Normal_Breathing_2 = table.Column<bool>(type: "bit", nullable: false),
                    Test_Results = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitTestingFormHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FitTestingFormHistory_FitTestingForm_FitTestingFormId",
                        column: x => x.FitTestingFormId,
                        principalTable: "FitTestingForm",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FitTestingFormHistory_FitTestingFormId",
                table: "FitTestingFormHistory",
                column: "FitTestingFormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FitTestingFormHistory");
        }
    }
}
