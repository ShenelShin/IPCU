using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class AddDeviceConnected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceConnected",
                columns: table => new
                {
                    DeviceId = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    DeviceType = table.Column<string>(type: "nvarchar(3)", maxLength: 40, nullable: false),
                    DeviceInsert = table.Column<DateTime>(type: "date", nullable: false),
                    DeviceRemove = table.Column<DateTime>(type: "date", nullable: true),
                    HospNum = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceConnected", x => x.DeviceId);
                    table.ForeignKey(
                        name: "FK_DeviceConnected_tbmaster_HospNum",
                        column: x => x.HospNum,
                        principalTable: "tbmaster",
                        principalColumn: "HospNum",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceConnected_HospNum",
                table: "DeviceConnected",
                column: "HospNum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceConnected");
        }
    }
}
