using System;
using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class PatientViewModel
    {
        public string HospNum { get; set; }
        public string IdNum { get; set; }
        public string PatientName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        [Display(Name = "Admission Type")]
        public string AdmType { get; set; }

        [Display(Name = "Location")]
        public string AdmLocation { get; set; }

        [Display(Name = "Admission Date")]
        [DataType(DataType.DateTime)]
        public DateTime? AdmDate { get; set; }

        [Display(Name = "Room")]
        public string RoomID { get; set; }

        public string Age { get; set; }

        public string Sex { get; set; }

        [Display(Name = "Civil Status")]
        public string CivilStatus { get; set; }

        [Display(Name = "Patient Type")]
        public string PatientType { get; set; }

        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Display(Name = "Mobile Number")]
        public string CellNum { get; set; }

        [Display(Name = "Days Since Admission")]
        public int AdmissionDuration { get; set; }

        [Display(Name = "HAI Status")]
        public bool HaiStatus { get; set; }

        [Display(Name = "HAI Count")]
        public int HaiCount { get; set; }

        public ICollection<VitalSigns> VitalSigns { get; set; }

        public PatientViewModel()
        {
            VitalSigns = new List<VitalSigns>();
        }
    }
}