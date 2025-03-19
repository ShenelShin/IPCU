namespace IPCU.Models
{
    public class EvaluationSummaryViewModel
    {
        public DateTime TrainingDate { get; set; }
        public int TotalParticipants { get; set; }
        public int MaleCount { get; set; }
        public int FemaleCount { get; set; }
        public double FinalRating { get; set; } // Overall rating from all rating fields
        public double? PostTestEvaluationGrade { get; set; } // New field for average Rate


        // Existing rating averages
        public double AverageFlowFollowed { get; set; }
        public double AverageRulesEstablished { get; set; }
        public double AverageInitiateDiscussion { get; set; }
        public double AverageTechnicalCapability { get; set; } // program
        public double AverageContentOrganization { get; set; } //speaker
        public double AverageObjectiveStated { get; set; }
        public double AverageContentQuality { get; set; }
        public double AverageFlowOfTopic { get; set; }
        public double AverageRelevanceOfTopic { get; set; }
        public double AveragePracticeApplication { get; set; }
        public double AverageLearningActivities { get; set; }
        public double AverageVisualAids { get; set; }
        public double AveragePresentKnowledge { get; set; }
        public double AverageBalancePrinciples { get; set; }
        public double AverageAddressClarifications { get; set; }
        public double AveragePreparedness { get; set; }
        public double AverageTeachingPersonality { get; set; }
        public double AverageEstablishRapport { get; set; }
        public double AverageRespectForParticipants { get; set; }
        public double AverageVoicePersonality { get; set; }
        public double AverageTimeManagement { get; set; }

        public string CombinedSuggestions { get; set; }

        public string CombinedSayToSpeaker { get; set; }
        public double ProgramAverage { get; set; }
        public double SpeakerAverage { get; set; }
        public string Venue { get; set; }
        public string Personnel { get; set; }
        public string SMELecturer { get; set; }

    }
}