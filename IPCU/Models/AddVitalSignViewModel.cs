using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class AddVitalSignViewModel
    {
        public string IdNum { get; set; }
        public string PatientName { get; set; }
        public string HospNum { get; set; }
        public string AdmLocation { get; set; }
        public string RoomID { get; set; }

        [Required(ErrorMessage = "Please select a vital sign type")]
        public string VitalSign { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        public string VitalSignValue { get; set; }

        [Required(ErrorMessage = "Please enter a date")]
        [Display(Name = "Date/Time")]
        public DateTime VitalSignDate { get; set; } = DateTime.Now;

        // Options for the dropdown
        public List<string> VitalSignOptions { get; set; } = new List<string>
        {
            "TEMP",
            "RR",
            "WBC",
            "Other Symptoms"
        };
    }
}
