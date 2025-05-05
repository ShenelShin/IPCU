using System;
using System.ComponentModel.DataAnnotations;
namespace IPCU.Models
{
    public class PatientViewModel
    {
        public string HospNum { get; set; } = string.Empty;
        public string IdNum { get; set; } = string.Empty;
        public string PatientName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;

        [Display(Name = "Admission Type")]
        public string AdmType { get; set; } = string.Empty;

        [Display(Name = "Location")]
        public string AdmLocation { get; set; } = string.Empty;

        [Display(Name = "Admission Date")]
        [DataType(DataType.DateTime)]
        public DateTime? AdmDate { get; set; }

        [Display(Name = "Room")]
        public string RoomID { get; set; } = string.Empty;

        public string Age { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;

        [Display(Name = "Civil Status")]
        public string CivilStatus { get; set; } = string.Empty;

        [Display(Name = "Patient Type")]
        public string PatientType { get; set; } = string.Empty;

        [Display(Name = "Email")]
        public string EmailAddress { get; set; } = string.Empty;

        [Display(Name = "Mobile Number")]
        public string CellNum { get; set; } = string.Empty;

        [Display(Name = "Days Since Admission")]
        public int AdmissionDuration { get; set; }

        [Display(Name = "HAI Status")]
        public bool HaiStatus { get; set; }

        [Display(Name = "HAI Count")]
        public int HaiCount { get; set; }

        public ICollection<VitalSigns> VitalSigns { get; set; } = new List<VitalSigns>();
        public InfectionFormsInfo? InfectionForms { get; set; }
        public PatientMovementViewModel? PatientMovements { get; set; }

        public string RoomDescription { get; set; }
        public string StationId { get; set; }
        public string StationName { get; set; }

        public PatientViewModel()
        {
            VitalSigns = new List<VitalSigns>();
        }
    }
}