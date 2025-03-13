using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class PreClinicalQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PreTestClinical",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgeGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PWD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CivilStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PRETCSCORE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreTestClinical", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreTestClinical");
        }
    }
}
