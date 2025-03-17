using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class PostTestClinical
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; }

        [Required]
        public string EmployeeId { get; set; } // New field

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
        public float POSTCSCORE { get; set; }  // Store the computed score
    }
}
