using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPCU.Data;
using IPCU.Models;
using IPCU.Services;

namespace IPCU.Controllers
{
    public class TrainingSummaryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingSummaryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TrainingSummary
        public async Task<IActionResult> Index()
        {
            return View(await _context.TrainingEvaluation.ToListAsync());
        }

        // GET: TrainingSummary/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingEvaluation = await _context.TrainingEvaluation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingEvaluation == null)
            {
                return NotFound();
            }

            return View(trainingEvaluation);
        }

        // GET: TrainingSummary/Create
        public IActionResult Create()
        {
            return View();
        }



        // POST: TrainingSummary/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Venue,DateOfTraining,TrainingMethodology,ProfessionalCategory,TotalParticipantsMale,TotalParticipantsFemale,PostTestEvaluationGrade,FinalRating,FlowFollowed,RulesEstablished,InitiateDiscussion,TechnicalCapability,ContentOrganization,ObjectiveStated,ContentQuality,FlowOfTopic,RelevanceOfTopic,PracticeApplication,LearningActivities,VisualAids,PresentKnowledge,BalancePrinciples,AddressClarifications,Preparedness,TeachingPersonality,EstablishRapport,RespectForParticipants,VoicePersonality,TimeManagement,SuggestionsForImprovement")] TrainingEvaluation trainingEvaluation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trainingEvaluation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trainingEvaluation);
        }

        // GET: TrainingSummary/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingEvaluation = await _context.TrainingEvaluation.FindAsync(id);
            if (trainingEvaluation == null)
            {
                return NotFound();
            }
            return View(trainingEvaluation);
        }

        // POST: TrainingSummary/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Venue,DateOfTraining,TrainingMethodology,ProfessionalCategory,TotalParticipantsMale,TotalParticipantsFemale,PostTestEvaluationGrade,FinalRating,FlowFollowed,RulesEstablished,InitiateDiscussion,TechnicalCapability,ContentOrganization,ObjectiveStated,ContentQuality,FlowOfTopic,RelevanceOfTopic,PracticeApplication,LearningActivities,VisualAids,PresentKnowledge,BalancePrinciples,AddressClarifications,Preparedness,TeachingPersonality,EstablishRapport,RespectForParticipants,VoicePersonality,TimeManagement,SuggestionsForImprovement")] TrainingEvaluation trainingEvaluation)
        {
            if (id != trainingEvaluation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingEvaluation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingEvaluationExists(trainingEvaluation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(trainingEvaluation);
        }

        // GET: TrainingSummary/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingEvaluation = await _context.TrainingEvaluation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trainingEvaluation == null)
            {
                return NotFound();
            }

            return View(trainingEvaluation);
        }

        // POST: TrainingSummary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingEvaluation = await _context.TrainingEvaluation.FindAsync(id);
            if (trainingEvaluation != null)
            {
                _context.TrainingEvaluation.Remove(trainingEvaluation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrainingEvaluationExists(int id)
        {
            return _context.TrainingEvaluation.Any(e => e.Id == id);
        }
        public async Task<IActionResult> GeneratePdf(int id)
        {
            var training = await _context.TrainingEvaluation.FindAsync(id);
            if (training == null)
            {
                return NotFound();
            }

            var pdfBytes = TrainingEvaluationPdfService.GeneratePdf(training);

            Response.Headers["Content-Disposition"] = "inline"; // Ensures inline preview

            return File(pdfBytes, "application/pdf");
        }

    }
}
