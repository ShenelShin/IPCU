using System;

namespace IPCU.Models
{
    public class FitTestingFormHistory
    {
        public int Id { get; set; } // Primary key

        public int FitTestingFormId { get; set; } // Foreign key to the main form
        public FitTestingForm FitTestingForm { get; set; } // Navigation property

        public string Fit_Test_Solution { get; set; }
        public string Sensitivity_Test { get; set; }
        public string Respiratory_Type { get; set; }
        public string Model { get; set; }
        public string Size { get; set; }
        public bool Normal_Breathing { get; set; }
        public bool Deep_Breathing { get; set; }
        public bool Turn_head_side_to_side { get; set; }
        public bool Move_head_up_and_down { get; set; }
        public bool Reading { get; set; }
        public bool Bending_Jogging { get; set; }
        public bool Normal_Breathing_2 { get; set; }
        public string Test_Results { get; set; }

        public DateTime SubmittedAt { get; set; } // Timestamp for the history record
    }
}
