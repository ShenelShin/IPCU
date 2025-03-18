using System;
using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class TrainingEvaluation
    {
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

        // ✅ Add FinalRating Here
        [Required]
        public double FinalRating { get; set; }

        // Program Facilitation Rating
        public double FlowFollowed { get; set; }
        public double RulesEstablished { get; set; }
        public double InitiateDiscussion { get; set; }
        public double TechnicalCapability { get; set; }

        // Subject Matter Expert Evaluation
        public double ContentOrganization { get; set; }
        public double ObjectiveStated { get; set; }
        public double ContentQuality { get; set; }
        public double FlowOfTopic { get; set; }
        public double RelevanceOfTopic { get; set; }
        public double PracticeApplication { get; set; }
        public double LearningActivities { get; set; }
        public double VisualAids { get; set; }

        // Speaker Evaluation
        public double PresentKnowledge { get; set; }
        public double BalancePrinciples { get; set; }
        public double AddressClarifications { get; set; }

        // Trainer Characteristics
        public double Preparedness { get; set; }
        public double TeachingPersonality { get; set; }
        public double EstablishRapport { get; set; }
        public double RespectForParticipants { get; set; }
        public double VoicePersonality { get; set; }
        public double TimeManagement { get; set; }

        [Required]
        public string SMELecturer { get; set; }

        public string SuggestionsForImprovement { get; set; }

        public string SayToSpeaker { get; set; }
    }

}
