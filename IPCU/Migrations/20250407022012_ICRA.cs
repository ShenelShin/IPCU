using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IPCU.Migrations
{
    /// <inheritdoc />
    public partial class ICRA : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ICRA",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectNameAndDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractorRepresentativeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelephoneOrMobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecificSiteOfActivity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScopeOfWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstimatedDuration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConstructionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientRiskGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreventiveMeasures = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiskGroup = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Below_Noise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Below_Vibration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Below_Dust = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Below_Ventilation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Below_Pressuraztion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Below_Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Below_Mechanical = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Below_MedicalGas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Below_HotColdWater = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Below_Other = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Above_Noise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Above_Vibration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Above_Dust = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Above_Ventilation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Above_Pressuraztion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Above_Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Above_Mechanical = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Above_MedicalGas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Above_HotColdWater = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Above_Other = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lateral_Noise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lateral_Vibration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lateral_Dust = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lateral_Ventilation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lateral_Pressuraztion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lateral_Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lateral_Mechanical = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lateral_MedicalGas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lateral_HotColdWater = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lateral_Other = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Behind_Noise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Behind_Vibration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Behind_Dust = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Behind_Ventilation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Behind_Pressuraztion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Behind_Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Behind_Mechanical = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Behind_MedicalGas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Behind_HotColdWater = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Behind_Other = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Front_Noise = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Front_Vibration = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Front_Dust = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Front_Ventilation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Front_Pressuraztion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Front_Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Front_Mechanical = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Front_MedicalGas = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Front_HotColdWater = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Front_Other = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RiskofWater = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks_RiskofWater = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShouldWork = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks_ShouldWork = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanSupplyAir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks_CanSupplyAir = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HaveTraffic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks_HaveTraffic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CanPatientCare = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks_CanPatientCare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AreMeasures = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks_AreMeasures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdditionalComments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractorSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EngineeringSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ICPSign = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitAreaRep = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ICRA", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ICRA");
        }
    }
}
