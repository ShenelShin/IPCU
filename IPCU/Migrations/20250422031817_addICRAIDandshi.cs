using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class addICRAIDandshi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ICRAId",
                table: "TCSkillsChecklist",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ICRAId",
                table: "PostConstruction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TCSkillsChecklist_ICRAId",
                table: "TCSkillsChecklist",
                column: "ICRAId");

            migrationBuilder.CreateIndex(
                name: "IX_PostConstruction_ICRAId",
                table: "PostConstruction",
                column: "ICRAId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostConstruction_ICRA_ICRAId",
                table: "PostConstruction",
                column: "ICRAId",
                principalTable: "ICRA",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TCSkillsChecklist_ICRA_ICRAId",
                table: "TCSkillsChecklist",
                column: "ICRAId",
                principalTable: "ICRA",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostConstruction_ICRA_ICRAId",
                table: "PostConstruction");

            migrationBuilder.DropForeignKey(
                name: "FK_TCSkillsChecklist_ICRA_ICRAId",
                table: "TCSkillsChecklist");

            migrationBuilder.DropIndex(
                name: "IX_TCSkillsChecklist_ICRAId",
                table: "TCSkillsChecklist");

            migrationBuilder.DropIndex(
                name: "IX_PostConstruction_ICRAId",
                table: "PostConstruction");

            migrationBuilder.DropColumn(
                name: "ICRAId",
                table: "TCSkillsChecklist");

            migrationBuilder.DropColumn(
                name: "ICRAId",
                table: "PostConstruction");
        }
    }
}
