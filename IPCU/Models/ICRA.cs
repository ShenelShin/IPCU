using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace IPCU.Models
{
    public class ICRA
    {
        [Key]
        public int Id { get; set; }
        public string ProjectReferenceNumber { get; set; }
        public string ProjectNameAndDescription { get; set; }

        public string ContractorRepresentativeName { get; set; }
        public string TelephoneOrMobileNumber { get; set; }

        public string SpecificSiteOfActivity { get; set; }

        public string ScopeOfWork { get; set; }

        public DateTime? ProjectStartDate { get; set; }
        public string EstimatedDuration { get; set; }
        public string ConstructionType { get; set; }
        public string PatientRiskGroup { get; set; }
        public string PreventiveMeasures { get; set; }
        public string RiskGroup { get; set; }
        public string LocalNumber { get; set; }
        public bool Below_Noise { get; set; }
        public bool Below_Vibration { get; set; }
        public bool Below_Dust { get; set; }
        public bool Below_Ventilation { get; set; }
        public bool Below_Pressuraztion { get; set; }
        public bool Below_Data { get; set; }
        public bool Below_Mechanical { get; set; }
        public bool Below_MedicalGas { get; set; }
        public bool Below_HotColdWater { get; set; }
        public bool Below_Other { get; set; }
        public bool Above_Noise { get; set; }
        public bool Above_Vibration { get; set; }
        public bool Above_Dust { get; set; }
        public bool Above_Ventilation { get; set; }
        public bool Above_Pressuraztion { get; set; }
        public bool Above_Data { get; set; }
        public bool Above_Mechanical { get; set; }
        public bool Above_MedicalGas { get; set; }
        public bool Above_HotColdWater { get; set; }
        public bool Above_Other { get; set; }
        public bool Lateral_Noise { get; set; }
        public bool Lateral_Vibration { get; set; }
        public bool Lateral_Dust { get; set; }
        public bool Lateral_Ventilation { get; set; }
        public bool Lateral_Pressuraztion { get; set; }
        public bool Lateral_Data { get; set; }
        public bool Lateral_Mechanical { get; set; }
        public bool Lateral_MedicalGas { get; set; }
        public bool Lateral_HotColdWater { get; set; }
        public bool Lateral_Other { get; set; }
        public bool Behind_Noise { get; set; }
        public bool Behind_Vibration { get; set; }
        public bool Behind_Dust { get; set; }
        public bool Behind_Ventilation { get; set; }
        public bool Behind_Pressuraztion { get; set; }
        public bool Behind_Data { get; set; }
        public bool Behind_Mechanical { get; set; }
        public bool Behind_MedicalGas { get; set; }
        public bool Behind_HotColdWater { get; set; }
        public bool Behind_Other { get; set; }
        public bool Front_Noise { get; set; }
        public bool Front_Vibration { get; set; }
        public bool Front_Dust { get; set; }
        public bool Front_Ventilation { get; set; }
        public bool Front_Pressuraztion { get; set; }
        public bool Front_Data { get; set; }
        public bool Front_Mechanical { get; set; }
        public bool Front_MedicalGas { get; set; }
        public bool Front_HotColdWater { get; set; }
        public bool Front_Other { get; set; }
        public string RiskofWater { get; set; }
        public string? Remarks_RiskofWater { get; set; }
        public string ShouldWork { get; set; }
        public string? Remarks_ShouldWork { get; set; }
        public string CanSupplyAir { get; set; }
        public string? Remarks_CanSupplyAir { get; set; }
        public string HaveTraffic { get; set; }
        public string? Remarks_HaveTraffic { get; set; }
        public string CanPatientCare { get; set; }
        public string? Remarks_CanPatientCare { get; set; }
        public string AreMeasures { get; set; }
        public string? Remarks_AreMeasures { get; set; }
        public string? AdditionalComments { get; set; }
        public string? ContractorSign { get; set; }
        public string? EngineeringSign { get; set; }
        public string? ICPSign { get; set; }
        public string? UnitAreaRep { get; set; }




    }
}
