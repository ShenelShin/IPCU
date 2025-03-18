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
            var evaluations = await _context.Evaluations
                .GroupBy(e => e.TrainingDate)
                .Select(g => new EvaluationSummaryViewModel
                {
                    TrainingDate = g.Key,
                    TotalParticipants = g.Count(),
                    MaleCount = g.Count(e => e.Sex == "Male"),
                    FemaleCount = g.Count(e => e.Sex == "Female"),

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

                    CombinedSuggestions = string.Join("; ", g.Select(e => e.SuggestionsForImprovement).Where(s => !string.IsNullOrEmpty(s))),
                    CombinedSayToSpeaker = string.Join("; ", g.Select(e => e.SayToSpeaker).Where(s => !string.IsNullOrEmpty(s)))
                })
                .ToListAsync();

            // Retrieve the TrainingSummaries grouped by DateCreated
            var trainingSummaries = await _context.TrainingSummaries
                .GroupBy(t => t.DateCreated.Date) // Remove time component
                .Select(g => new
                {
                    DateCreated = g.Key, // Now this contains only the date
                    AverageRate = g.Average(t => t.Rate)
                })
                .ToListAsync();


            // Merge TrainingSummaries into Evaluations based on TrainingDate = DateCreated
            foreach (var eval in evaluations)
            {
                Console.WriteLine($"Checking for TrainingDate: {eval.TrainingDate.Date}");

                var summary = trainingSummaries.FirstOrDefault(ts => ts.DateCreated == eval.TrainingDate.Date);

                if (summary != null)
                {
                    Console.WriteLine($"Found match for {eval.TrainingDate.Date} → Average Rate: {summary.AverageRate}");
                    eval.PostTestEvaluationGrade = summary.AverageRate;
                }
                else
                {
                    Console.WriteLine($"No match found for {eval.TrainingDate.Date}");
                }
            }
            return View(evaluations);
        }

    }
}
