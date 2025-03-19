using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class CreatePatientModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbmaster",
                columns: table => new
                {
                    HospNum = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    MiddleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AccountNum = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true),
                    CivilStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Age = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    PatientStatus = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    SeniorCitizenWithID = table.Column<bool>(type: "bit", nullable: false),
                    cellnum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmpNum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PatientType = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbmaster", x => x.HospNum);
                });

            migrationBuilder.CreateTable(
                name: "tbpatient",
                columns: table => new
                {
                    IdNum = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    HospNum = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    AdmType = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    AdmLocation = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    AdmDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Age = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    RoomID = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    DeathType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DeathDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbpatient", x => x.IdNum);
                    table.ForeignKey(
                        name: "FK_tbpatient_tbmaster_HospNum",
                        column: x => x.HospNum,
                        principalTable: "tbmaster",
                        principalColumn: "HospNum",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbpatient_HospNum",
                table: "tbpatient",
                column: "HospNum");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbpatient");

            migrationBuilder.DropTable(
                name: "tbmaster");
        }
    }
}
