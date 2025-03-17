using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class TrainingSummary
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; }

        [Required]
        public string EmployeeId { get; set; } // Unique identifier for employees

        [Required(ErrorMessage = "Age Group is required.")]
        public string AgeGroup { get; set; }

        [Required(ErrorMessage = "Sex is required.")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "PWD status is required.")]
        public string PWD { get; set; }

        [Required(ErrorMessage = "Civil Status is required.")]
        public string CivilStatus { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        public string Department { get; set; }

        public int PreScore { get; set; }  // Store the computed pre-test score (both clinical and non-clinical)
        public int PostScore { get; set; } // Store the computed post-test score (both clinical and non-clinical)

        public DateTime DateCreated { get; set; } = DateTime.Now; // Automatically set the creation date
        public string? ProperHand { get; set; } // Stores "Pass" or "Fail"
        public string? GloveRemoval { get; set; } // Stores "Pass" or "Fail"
        public string? IDPrinting { get; set; } // Stores "Pass" or "Fail"
        public string? TrainingReport { get; set; } // New Column


    }

}
