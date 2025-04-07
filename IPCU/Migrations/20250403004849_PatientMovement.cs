using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class PatientMovement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbPatientMovement",
                columns: table => new
                {
                    MovementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovementDate = table.Column<DateTime>(type: "date", nullable: false),
                    Area = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdmissionCount = table.Column<int>(type: "int", nullable: false),
                    TransferInCount = table.Column<int>(type: "int", nullable: false),
                    SentHomeCount = table.Column<int>(type: "int", nullable: false),
                    MortalityCount = table.Column<int>(type: "int", nullable: false),
                    TransferOutCount = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbPatientMovement", x => x.MovementId);
                });

            migrationBuilder.CreateTable(
                name: "tbPatientMovementDetail",
                columns: table => new
                {
                    MovementDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MovementId = table.Column<int>(type: "int", nullable: false),
                    HospNum = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    IdNum = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MovementType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SourceArea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DestinationArea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MovementDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbPatientMovementDetail", x => x.MovementDetailId);
                    table.ForeignKey(
                        name: "FK_tbPatientMovementDetail_tbPatientMovement_MovementId",
                        column: x => x.MovementId,
                        principalTable: "tbPatientMovement",
                        principalColumn: "MovementId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbPatientMovementDetail_tbmaster_HospNum",
                        column: x => x.HospNum,
                        principalTable: "tbmaster",
                        principalColumn: "HospNum",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbPatientMovement_MovementDate_Area",
                table: "tbPatientMovement",
                columns: new[] { "MovementDate", "Area" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbPatientMovementDetail_HospNum",
                table: "tbPatientMovementDetail",
                column: "HospNum");

            migrationBuilder.CreateIndex(
                name: "IX_tbPatientMovementDetail_MovementId",
                table: "tbPatientMovementDetail",
                column: "MovementId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbPatientMovementDetail");

            migrationBuilder.DropTable(
                name: "tbPatientMovement");
        }
    }
}
