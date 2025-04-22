using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class NOR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NoticeOfReferral",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreaUnit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DOD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InitialDiagnosis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HAAT = table.Column<bool>(type: "bit", nullable: false),
                    Communicable = table.Column<bool>(type: "bit", nullable: false),
                    Fever = table.Column<bool>(type: "bit", nullable: false),
                    PostOp = table.Column<bool>(type: "bit", nullable: false),
                    ReAdmitted = table.Column<bool>(type: "bit", nullable: false),
                    Laboratory = table.Column<bool>(type: "bit", nullable: false),
                    Radiology = table.Column<bool>(type: "bit", nullable: false),
                    Referredby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReferredbyDnT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Receivedby = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedbyDnT = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoticeOfReferral", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoticeOfReferral");
        }
    }
}
