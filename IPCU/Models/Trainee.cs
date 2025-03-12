using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class Trainee
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Sex { get; set; }

        [Required]
        public string Department { get; set; }

        public string N95 { get; set; }

        [Required]
        public string IDPrinting { get; set; } // Dropdown (Done, Pending, Certificate)

        public string BasicInfection { get; set; }

        public string ProperHand { get; set; }

        [Required]
        public string GloveRemoval { get; set; } // Dropdown (PASSED, W/POWDER)

        public int? PreTestScore { get; set; }

        public int? PostTestScore { get; set; }

        public decimal? Rate { get; set; }

        public string TrainingReport { get; set; }
    }
}