using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class PostTestNonClinical
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; }

        [Required]
        public string EmployeeId { get; set; }

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

        public int POSTNONSCORE { get; set; } // Store the computed score

        // Additional properties that don't need to be stored in the database
        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public int TotalQuestions { get; set; }

        [System.ComponentModel.DataAnnotations.Schema.NotMapped]
        public double PercentageScore { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;

    }
}