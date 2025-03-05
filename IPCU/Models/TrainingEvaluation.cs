using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class TrainingEvaluation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfTraining { get; set; }

        [Required]
        public string TrainingMethodology { get; set; }

        [Required]
        public string ProfessionalCategory { get; set; }

        [Required]
        public int TotalParticipantsMale { get; set; }

        [Required]
        public int TotalParticipantsFemale { get; set; }

        [Required]
        public double PostTestEvaluationGrade { get; set; }

        // Program Facilitation Rating
        public int FlowFollowed { get; set; }
        public int RulesEstablished { get; set; }
        public int InitiateDiscussion { get; set; }
        public int TechnicalCapability { get; set; }

        // Subject Matter Expert Evaluation
        public int ContentOrganization { get; set; }
        public int ObjectiveStated { get; set; }
        public int ContentQuality { get; set; }
        public int FlowOfTopic { get; set; }
        public int RelevanceOfTopic { get; set; }
        public int PracticeApplication { get; set; }
        public int LearningActivities { get; set; }
        public int VisualAids { get; set; }

        // Speaker Evaluation
        public int PresentKnowledge { get; set; }
        public int BalancePrinciples { get; set; }
        public int AddressClarifications { get; set; }

        // Trainer Characteristics
        public int Preparedness { get; set; }
        public int TeachingPersonality { get; set; }
        public int EstablishRapport { get; set; }
        public int RespectForParticipants { get; set; }
        public int VoicePersonality { get; set; }
        public int TimeManagement { get; set; }

        public string SuggestionsForImprovement { get; set; }
    }
}
