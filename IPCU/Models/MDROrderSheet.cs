using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class MDROrderSheet
    {
        [Key]
        public int Id { get; set; }

        public string PatientLastName { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientMiddleName { get; set; }

        [Display(Name = "Patient Diagnosis")]
        public string Diagnosis { get; set; }

        [Display(Name = "Hospital Number")]
        public string HospitalNumber { get; set; }

        [Display(Name = "Sex/Gender")]
        public string Sex { get; set; } // "Male" or "Female"

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        public int? Age { get; set; }

        [Display(Name = "Classification")]
        public string Classification { get; set; } // "Pay" or "Service"

        // Specimen Details
        [Display(Name = "Specimen Type")]
        public string SpecimenType { get; set; }

        // Pathogen Checkboxes
        public bool IsMRSA { get; set; }
        public bool IsVRE { get; set; }
        public bool IsMRSE { get; set; }
        public bool IsESBL { get; set; }
        public bool IsCRE { get; set; }
        public bool IsMDRSpneu { get; set; }
        public bool IsMDRGNB { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Date and Time of Collection")]
        public DateTime CollectionDateTime { get; set; }


        // Infection Control Measures
        public bool ExplainSituationToPatient { get; set; }
        public bool PlaceInSingleRoom { get; set; }
        public bool PutContactSignage { get; set; }
        public bool LimitVisitors { get; set; }
        public bool DedicateEquipment { get; set; }
        public bool EnsureHandRubAvailability { get; set; }
        public bool EnsurePPEAvailability { get; set; }
        public bool AssignDedicatedStaff { get; set; }
        public bool DiscardPPEBeforeExit { get; set; }

        public bool DisinfectHorizontalSurfaces { get; set; }
        public bool DisinfectHighTouchAreas { get; set; }
        public bool TerminalCleaning { get; set; }
        public bool LiftPrecautionsWithIPCApproval { get; set; }





        // Signatories
       

        [Display(Name = "Physician Signature and Date")]
        public string PhysicianSignatureAndDate { get; set; }



        [Display(Name = "Nurse Signature and Date")]
        public string NurseSignatureAndDate { get; set; }
    }
}
