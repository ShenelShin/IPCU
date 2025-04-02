using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class GICHECKLIST : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GIInfectionChecklists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HospNum = table.Column<int>(type: "int", nullable: false),
                    DateOfAdmission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Disposition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DispositionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DispositionArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitWardArea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainService = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfEvent = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NameOfInvestigator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Classification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MDRO = table.Column<bool>(type: "bit", nullable: false),
                    TypeClassification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CDItoxin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CDIpseudomembranous = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GEdiarrhea = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GENausea = table.Column<bool>(type: "bit", nullable: false),
                    GEVomiting = table.Column<bool>(type: "bit", nullable: false),
                    GENAbdominal = table.Column<bool>(type: "bit", nullable: false),
                    GENFever = table.Column<bool>(type: "bit", nullable: false),
                    GENHeadache = table.Column<bool>(type: "bit", nullable: false),
                    GENpenteric1 = table.Column<bool>(type: "bit", nullable: false),
                    GENenteric2 = table.Column<bool>(type: "bit", nullable: false),
                    GENantibody = table.Column<bool>(type: "bit", nullable: false),
                    IABorganism = table.Column<bool>(type: "bit", nullable: false),
                    IABAbscess1 = table.Column<bool>(type: "bit", nullable: false),
                    IABAbscess2 = table.Column<bool>(type: "bit", nullable: false),
                    IABFever = table.Column<bool>(type: "bit", nullable: false),
                    IABHypotension = table.Column<bool>(type: "bit", nullable: false),
                    IABNausea = table.Column<bool>(type: "bit", nullable: false),
                    IABVomiting = table.Column<bool>(type: "bit", nullable: false),
                    IABAbdominal = table.Column<bool>(type: "bit", nullable: false),
                    IABtransaminase = table.Column<bool>(type: "bit", nullable: false),
                    IABJaundice = table.Column<bool>(type: "bit", nullable: false),
                    IABOrgintraabdominal = table.Column<bool>(type: "bit", nullable: false),
                    IABmicrobiologic = table.Column<bool>(type: "bit", nullable: false),
                    GITAbscess1 = table.Column<bool>(type: "bit", nullable: false),
                    GITAbscess2 = table.Column<bool>(type: "bit", nullable: false),
                    GITblood = table.Column<bool>(type: "bit", nullable: false),
                    GITNausea = table.Column<bool>(type: "bit", nullable: false),
                    GITVomiting = table.Column<bool>(type: "bit", nullable: false),
                    GITPainTend = table.Column<bool>(type: "bit", nullable: false),
                    GITFEVER = table.Column<bool>(type: "bit", nullable: false),
                    GITOdynophagia = table.Column<bool>(type: "bit", nullable: false),
                    GITDysphagia = table.Column<bool>(type: "bit", nullable: false),
                    GITDrain = table.Column<bool>(type: "bit", nullable: false),
                    GITGram = table.Column<bool>(type: "bit", nullable: false),
                    GITmicrobiologic = table.Column<bool>(type: "bit", nullable: false),
                    GITgastrointestinal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GIInfectionChecklists", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GIInfectionChecklists");
        }
    }
}
