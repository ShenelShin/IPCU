using System.ComponentModel.DataAnnotations;

namespace IPCU.Models

{
    public class TestFormModel
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

        // Matching Type Questions
        [Required(ErrorMessage = "Please select an answer for 'After covering cough with hands'.")]
        [Display(Name = "After covering cough with hands")]
        public string? AfterCoveringCough { get; set; }

        [Required(ErrorMessage = "Please select an answer for 'After handling money'.")]
        [Display(Name = "After handling money")]
        public string? AfterHandlingMoney { get; set; }

        [Required(ErrorMessage = "Please select an answer for 'After sneezing'.")]
        [Display(Name = "After sneezing")]
        public string? AfterSneezing { get; set; }

        [Required(ErrorMessage = "Please select an answer for 'After an attendance time-in'.")]
        [Display(Name = "After an attendance time-in")]
        public string? AfterAttendanceTimeIn { get; set; }

        [Required(ErrorMessage = "Please select an answer for 'Before eating'.")]
        [Display(Name = "Before eating")]
        public string? BeforeEating { get; set; }

        [Required(ErrorMessage = "Please select an answer for 'After using the bathroom'.")]
        [Display(Name = "After using the bathroom")]
        public string? AfterUsingBathroom { get; set; }

        [Required(ErrorMessage = "Please select an answer for 'Before drinking any medication'.")]
        [Display(Name = "Before drinking any medication")]
        public string? BeforeDrinkingMedication { get; set; }

        [Required(ErrorMessage = "Please select an answer for 'After signing documents'.")]
        [Display(Name = "After signing documents")]
        public string? AfterSigningDocuments { get; set; }

        // Multiple Choice Questions
        [Required(ErrorMessage = "Please select where you put used face masks.")]
        [Display(Name = "Where do you put used face masks?")]
        public string? UsedFaceMasks { get; set; }

        [Required(ErrorMessage = "Please select a reason why masks are required.")]
        [Display(Name = "Why is mask use still required inside the hospital?")]
        public string? MaskUseReason { get; set; }

        [Required(ErrorMessage = "Please select a time frame for reporting a needle stick injury.")]
        [Display(Name = "If a healthcare worker sustained a needle stick injury, he/she should report within?")]
        public string? NeedleStickReportTime { get; set; }

        [Required(ErrorMessage = "Please select the best way to prevent the spread of infections.")]
        [Display(Name = "Which of the following is the most effective way to prevent the spread of infections?")]
        public string? InfectionPrevention { get; set; }

        [Required(ErrorMessage = "Please select the required duration for hand rubbing.")]
        [Display(Name = "Alcohol-based hand rubbing should be done for how many seconds?")]
        public string? HandRubbingTime { get; set; }

        [Required(ErrorMessage = "Please select the primary goal of IPC.")]
        [Display(Name = "What is the primary goal of infection prevention and control (IPC) in a healthcare setting?")]
        public string? IPCGoal { get; set; }

        [Required(ErrorMessage = "Please select the proper mask usage guideline.")]
        [Display(Name = "Which of the following best describes the proper use of masks in a healthcare facility?")]
        public string? ProperMaskUse { get; set; }

        [Required]
        public int Score { get; set; }  // Store the computed score

    }
}
