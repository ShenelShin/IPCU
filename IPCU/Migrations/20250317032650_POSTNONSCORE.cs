using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class POSTNONSCORE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "POSTSCORE",
                table: "PostTestNonCLinicals",
                newName: "POSTNONSCORE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "POSTNONSCORE",
                table: "PostTestNonCLinicals",
                newName: "POSTSCORE");
        }
    }
}
