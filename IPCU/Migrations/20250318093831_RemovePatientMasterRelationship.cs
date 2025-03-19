using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class RemovePatientMasterRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
               name: "FK_tbpatient_tbmaster_HospNum",
               table: "tbpatient");

            migrationBuilder.DropIndex(
                name: "IX_tbpatient_HospNum",
                table: "tbpatient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tbpatient_HospNum",
                table: "tbpatient",
                column: "HospNum");

            migrationBuilder.AddForeignKey(
                name: "FK_tbpatient_tbmaster_HospNum",
                table: "tbpatient",
                column: "HospNum",
                principalTable: "tbmaster",
                principalColumn: "HospNum",
                onDelete: ReferentialAction.Restrict);

        }
    }
}
