using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class PatientForm
    {
        [Key]
        public int Id { get; set; }

        // Patient Name
        [Required]
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string? Suffix { get; set; }

        // Patient Details
        [Required]
        public string Disease { get; set; }

        [Required]
        public string Status { get; set; }

        public string? Room { get; set; }

        public string? Building { get; set; }

        // Nurse In Charge
        [Required]
        public string NurseFirstName { get; set; }

        public string? NurseMiddleName { get; set; }

        [Required]
        public string NurseLastName { get; set; }

        public string? NurseSuffix { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Sex { get; set; } // "Male" or "Female"
    }
}
