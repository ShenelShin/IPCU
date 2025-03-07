using Microsoft.EntityFrameworkCore.Migrations;

namespace IPCU.Migrations
{
    public partial class AddEnvironmentResourceToHandHygieneForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EnvironmentResource",
                table: "HandHygieneForms", // Adjust this table name if different in your context
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnvironmentResource",
                table: "HandHygieneForms"); // Adjust this table name if different in your context
        }
    }
}