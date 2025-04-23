using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class Insertion
    {
        [Key]
        public int Id { get; set; }
        public string PatientLastName { get; set; }
        public string PatientFirstName { get; set; }
        public string? PatientMiddleName { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }

        public DateTime Birthday { get; set; }

        public string? PatientDiagnosis { get; set; }
        public string? HospitalNumber { get; set; }
        public string ReasonforInsertion { get; set; }
        public string ProcedureOperator { get; set; }
        public string NumberofLumens { get; set; }
        public string ProcedureLocation { get; set; }
        public string CatheterType { get; set; }
        public string Classification { get; set; }
        public bool Optimal { get; set; }
        public string? ExplainWhyAlternate { get; set; }
        public bool Left { get; set; }
        public bool Right { get; set; }
        public bool AnatomyIs { get; set; }
        public bool ChestWall { get; set; }
        public bool COPD { get; set; }
        public bool Emergency { get; set; }
        public bool Anesthesiologist { get; set; }
        public bool Coagulopathy { get; set; }
        public bool Dialysis { get; set; }
        public bool OperatorTraining { get; set; }
        //BEFORE THE PROCEDURE
        public bool ObtainInformedConsent { get; set; }
        public string? ObtainInformedConsentReminder { get; set; }
        public bool ConfirmHandHygiene { get; set; }
        public string? ConfirmHandHygieneReminder { get; set; }
        public bool UseFullBarrier { get; set; }
        public string? UseFullBarrierReminder { get; set; }
        public bool PerformSkin { get; set; }
        public string? PerformSkinReminder { get; set; }
        public bool AllowSite { get; set; }
        public string? AllowSiteReminder { get; set; }
        public bool UseSterile { get; set; }
        public string? UseSterileReminder { get; set; }
        //During
        public bool Maintain { get; set; }
        public string? MaintainReminder { get; set; }
        public bool Monitor { get; set; }
        public string? MonitorReminder { get; set; }
        //III
        public bool CleanBlood { get; set; }
        public string? CleanBloodReminder { get; set; }
        public string? ProcedureNotes  { get; set; }
        public string? Observer { get; set; }
        public string? Operator { get; set; }

    }
}
