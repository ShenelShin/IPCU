using System;
using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class FitTestingForm
    {
        [Key]
        public int Id { get; set; }

        // Name of Healthcare Worker (HCW):
        [Required]
        public string HCW_Name { get; set; }

        // Department/Unit/Office:
        [Required]
        public string DUO { get; set; }

        // Limitations
        public string Limitation { get; set; }

        // Fit Test Solution used
        [Required]
        public string Fit_Test_Solution { get; set; }

        // Sensitivity Test
        [Required]
        public string Sensitivity_Test { get; set; }

        // Respirator Type/Manufacturer
        [Required]
        public string Respiratory_Type { get; set; }

        // Model
        [Required]
        public string Model { get; set; }

        // Size
        [Required]
        public int Size { get; set; }

        // Normal Breathing 
        [Required]
        public bool Normal_Breathing { get; set; }

        // Deep Breathing
        [Required]
        public bool Deep_Breathing { get; set; }

        // Turn head side to side
        [Required]
        public bool Turn_head_side_to_side { get; set; }

        // Move head up and down  
        [Required]
        public bool Move_head_up_and_down { get; set; }

        // Reading
        [Required]
        public bool Reading { get; set; }

        // Bending/Jogging 
        [Required]
        public bool Bending_Jogging { get; set; }

        // Normal Breathing (second test)
        [Required]
        public bool Normal_Breathing_2 { get; set; }

        // Name of Fit Tester 
        [Required]
        public string Name_of_Fit_Tester { get; set; }

        // DUO of the Tester
        [Required]
        public string DUO_Tester { get; set; }

        // Timestamp for when the form is submitted
        [Required]
        public DateTime SubmittedAt { get; set; } = DateTime.Now; // Automatically sets the current timestamp
    }
}
