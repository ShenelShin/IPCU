using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class PostTestClinic2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "POSTCSCORE",
                table: "PostTestClinicals",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "POSTCSCORE",
                table: "PostTestClinicals",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
