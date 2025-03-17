using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignedAreaToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedArea",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedArea",
                table: "AspNetUsers");
        }
    }
}
