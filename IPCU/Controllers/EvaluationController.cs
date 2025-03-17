using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPCU.Data;
using IPCU.Models;

namespace IPCU.Controllers
{
    public class EvaluationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EvaluationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Evaluation
        public async Task<IActionResult> Index()
        {
            return View(await _context.EvaluationViewModel.ToListAsync());
        }

        public IActionResult Form()
        {
            return View();
        }

        // GET: Evaluation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluationViewModel = await _context.EvaluationViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluationViewModel == null)
            {
                return NotFound();
            }

            return View(evaluationViewModel);
        }

        // GET: Evaluation/Create
        public IActionResult Create()
        {
            return View("Form");
        }

        // POST: Evaluation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Email,TrainingDate,Venue,ServiceClassification,FinalRating,FlowFollowed,RulesEstablished,InitiateDiscussion,TechnicalCapability,ContentOrganization,ObjectiveStated,ContentQuality,FlowOfTopic,RelevanceOfTopic,PracticeApplication,LearningActivities,VisualAids,PresentKnowledge,BalancePrinciples,AddressClarifications,Preparedness,TeachingPersonality,EstablishRapport,RespectForParticipants,VoicePersonality,TimeManagement,SMELecturer,SuggestionsForImprovement,SayToSpeaker")] EvaluationViewModel evaluationViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evaluationViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evaluationViewModel);
        }

        // GET: Evaluation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluationViewModel = await _context.EvaluationViewModel.FindAsync(id);
            if (evaluationViewModel == null)
            {
                return NotFound();
            }
            return View(evaluationViewModel);
        }

        // POST: Evaluation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Email,TrainingDate,Venue,ServiceClassification,FinalRating,FlowFollowed,RulesEstablished,InitiateDiscussion,TechnicalCapability,ContentOrganization,ObjectiveStated,ContentQuality,FlowOfTopic,RelevanceOfTopic,PracticeApplication,LearningActivities,VisualAids,PresentKnowledge,BalancePrinciples,AddressClarifications,Preparedness,TeachingPersonality,EstablishRapport,RespectForParticipants,VoicePersonality,TimeManagement,SMELecturer,SuggestionsForImprovement,SayToSpeaker")] EvaluationViewModel evaluationViewModel)
        {
            if (id != evaluationViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evaluationViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvaluationViewModelExists(evaluationViewModel.Id))
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
            return View(evaluationViewModel);
        }

        // GET: Evaluation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluationViewModel = await _context.EvaluationViewModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evaluationViewModel == null)
            {
                return NotFound();
            }

            return View(evaluationViewModel);
        }

        // POST: Evaluation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evaluationViewModel = await _context.EvaluationViewModel.FindAsync(id);
            if (evaluationViewModel != null)
            {
                _context.EvaluationViewModel.Remove(evaluationViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvaluationViewModelExists(int id)
        {
            return _context.EvaluationViewModel.Any(e => e.Id == id);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(EvaluationViewModel evaluationViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evaluationViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(evaluationViewModel);
        }
    }
}
