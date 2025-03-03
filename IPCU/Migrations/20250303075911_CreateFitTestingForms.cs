using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class CreateFitTestingForms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FitTestingForm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HCW_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DUO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Limitation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fit_Test_Solution = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sensitivity_Test = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Respiratory_Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Normal_Breathing = table.Column<bool>(type: "bit", nullable: false),
                    Deep_Breathing = table.Column<bool>(type: "bit", nullable: false),
                    Turn_head_side_to_side = table.Column<bool>(type: "bit", nullable: false),
                    Move_head_up_and_down = table.Column<bool>(type: "bit", nullable: false),
                    Reading = table.Column<bool>(type: "bit", nullable: false),
                    Bending_Jogging = table.Column<bool>(type: "bit", nullable: false),
                    Normal_Breathing_2 = table.Column<bool>(type: "bit", nullable: false),
                    Name_of_Fit_Tester = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DUO_Tester = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FitTestingForm", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FitTestingForm");
        }
    }
}
