using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IPCU.Models
{
    public class EvaluationSummary
    {
        [Key]  // Make sure to import System.ComponentModel.DataAnnotations
        public int Id { get; set; }  // Add this as a primary key
        public DateTime TrainingDate { get; set; }
        public string Venue { get; set; }
        public int TotalEvaluations { get; set; }

        // Gender counts
        public int MaleCount { get; set; }
        public int FemaleCount { get; set; }

        // Program Facilitation
        public double AvgFlowFollowed { get; set; }
        public double AvgRulesEstablished { get; set; }
        public double AvgInitiateDiscussion { get; set; }
        public double AvgTechnicalCapability { get; set; }

        // Subject Matter Expert Evaluation
        public double AvgContentOrganization { get; set; }
        public double AvgObjectiveStated { get; set; }
        public double AvgContentQuality { get; set; }
        public double AvgFlowOfTopic { get; set; }
        public double AvgRelevanceOfTopic { get; set; }
        public double AvgPracticeApplication { get; set; }
        public double AvgLearningActivities { get; set; }
        public double AvgVisualAids { get; set; }

        // Speaker Evaluation
        public double AvgPresentKnowledge { get; set; }
        public double AvgBalancePrinciples { get; set; }
        public double AvgAddressClarifications { get; set; }

        // Trainer Characteristics
        public double AvgPreparedness { get; set; }
        public double AvgTeachingPersonality { get; set; }
        public double AvgEstablishRapport { get; set; }
        public double AvgRespectForParticipants { get; set; }
        public double AvgVoicePersonality { get; set; }
        public double AvgTimeManagement { get; set; }
    }

    public class EvaluationProcessor
    {
        public static List<EvaluationSummary> GenerateSummary(List<EvaluationViewModel> evaluations)
        {
            var summaryList = evaluations
                .GroupBy(e => e.TrainingDate)
                .Select(group => new EvaluationSummary
                {
                    TrainingDate = group.Key,
                    Venue = group.First().Venue,
                    TotalEvaluations = group.Count(),

                    // Gender count
                    MaleCount = group.Count(e => e.Sex?.ToLower() == "male"),
                    FemaleCount = group.Count(e => e.Sex?.ToLower() == "female"),

                    // Compute averages
                    AvgFlowFollowed = group.Average(e => e.FlowFollowed),
                    AvgRulesEstablished = group.Average(e => e.RulesEstablished),
                    AvgInitiateDiscussion = group.Average(e => e.InitiateDiscussion),
                    AvgTechnicalCapability = group.Average(e => e.TechnicalCapability),

                    AvgContentOrganization = group.Average(e => e.ContentOrganization),
                    AvgObjectiveStated = group.Average(e => e.ObjectiveStated),
                    AvgContentQuality = group.Average(e => e.ContentQuality),
                    AvgFlowOfTopic = group.Average(e => e.FlowOfTopic),
                    AvgRelevanceOfTopic = group.Average(e => e.RelevanceOfTopic),
                    AvgPracticeApplication = group.Average(e => e.PracticeApplication),
                    AvgLearningActivities = group.Average(e => e.LearningActivities),
                    AvgVisualAids = group.Average(e => e.VisualAids),

                    AvgPresentKnowledge = group.Average(e => e.PresentKnowledge),
                    AvgBalancePrinciples = group.Average(e => e.BalancePrinciples),
                    AvgAddressClarifications = group.Average(e => e.AddressClarifications),

                    AvgPreparedness = group.Average(e => e.Preparedness),
                    AvgTeachingPersonality = group.Average(e => e.TeachingPersonality),
                    AvgEstablishRapport = group.Average(e => e.EstablishRapport),
                    AvgRespectForParticipants = group.Average(e => e.RespectForParticipants),
                    AvgVoicePersonality = group.Average(e => e.VoicePersonality),
                    AvgTimeManagement = group.Average(e => e.TimeManagement),
                })
                .ToList();

            return summaryList;
        }
    }
}
