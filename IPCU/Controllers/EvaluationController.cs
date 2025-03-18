using Microsoft.AspNetCore.Mvc;
using IPCU.Data;
using IPCU.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace IPCU.Controllers
{
    public class EvaluationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvaluationController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(EvaluationViewModel evaluationViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Evaluations.Add(evaluationViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evaluationViewModel);
        }
        public async Task<IActionResult> SummaryReport()
        {
            var summaryData = await _context.Evaluations
                .GroupBy(e => e.TrainingDate)
                .Select(g => new EvaluationSummaryViewModel
                {
                    TrainingDate = g.Key,
                    TotalParticipants = g.Count(),
                    MaleCount = g.Count(e => e.Sex == "Male"),
                    FemaleCount = g.Count(e => e.Sex == "Female"),

                    // Calculate the overall final rating as an average of all numeric rating fields
                    FinalRating = (g.Average(e => e.FlowFollowed) +
                                  g.Average(e => e.RulesEstablished) +
                                  g.Average(e => e.InitiateDiscussion) +
                                  g.Average(e => e.TechnicalCapability) +
                                  g.Average(e => e.ContentOrganization) +
                                  g.Average(e => e.ObjectiveStated) +
                                  g.Average(e => e.ContentQuality) +
                                  g.Average(e => e.FlowOfTopic) +
                                  g.Average(e => e.RelevanceOfTopic) +
                                  g.Average(e => e.PracticeApplication) +
                                  g.Average(e => e.LearningActivities) +
                                  g.Average(e => e.VisualAids) +
                                  g.Average(e => e.PresentKnowledge) +
                                  g.Average(e => e.BalancePrinciples) +
                                  g.Average(e => e.AddressClarifications) +
                                  g.Average(e => e.Preparedness) +
                                  g.Average(e => e.TeachingPersonality) +
                                  g.Average(e => e.EstablishRapport) +
                                  g.Average(e => e.RespectForParticipants) +
                                  g.Average(e => e.VoicePersonality) +
                                  g.Average(e => e.TimeManagement)) / 21,

                    AverageFlowFollowed = g.Average(e => e.FlowFollowed),
                    AverageRulesEstablished = g.Average(e => e.RulesEstablished),
                    AverageInitiateDiscussion = g.Average(e => e.InitiateDiscussion),
                    AverageTechnicalCapability = g.Average(e => e.TechnicalCapability),
                    AverageContentOrganization = g.Average(e => e.ContentOrganization),
                    AverageObjectiveStated = g.Average(e => e.ObjectiveStated),
                    AverageContentQuality = g.Average(e => e.ContentQuality),
                    AverageFlowOfTopic = g.Average(e => e.FlowOfTopic),
                    AverageRelevanceOfTopic = g.Average(e => e.RelevanceOfTopic),
                    AveragePracticeApplication = g.Average(e => e.PracticeApplication),
                    AverageLearningActivities = g.Average(e => e.LearningActivities),
                    AverageVisualAids = g.Average(e => e.VisualAids),
                    AveragePresentKnowledge = g.Average(e => e.PresentKnowledge),
                    AverageBalancePrinciples = g.Average(e => e.BalancePrinciples),
                    AverageAddressClarifications = g.Average(e => e.AddressClarifications),
                    AveragePreparedness = g.Average(e => e.Preparedness),
                    AverageTeachingPersonality = g.Average(e => e.TeachingPersonality),
                    AverageEstablishRapport = g.Average(e => e.EstablishRapport),
                    AverageRespectForParticipants = g.Average(e => e.RespectForParticipants),
                    AverageVoicePersonality = g.Average(e => e.VoicePersonality),
                    AverageTimeManagement = g.Average(e => e.TimeManagement),

                    CombinedSuggestions = string.Join("; ", g.Select(e => e.SuggestionsForImprovement).Where(s => !string.IsNullOrEmpty(s))),
                    CombinedSayToSpeaker = string.Join("; ", g.Select(e => e.SayToSpeaker).Where(s => !string.IsNullOrEmpty(s)))
                })
                .ToListAsync();

            return View(summaryData);
        }

    }
}
