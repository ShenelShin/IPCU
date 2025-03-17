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
using X.PagedList;
using X.PagedList.Mvc.Core;
using X.PagedList.Extensions;
using Microsoft.AspNetCore.Authorization;


namespace IPCU.Controllers
{
    [Authorize]
    public class TrainingSummaryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrainingSummaryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchTitle, string searchVenue, string searchCategory, DateTime? searchDate, int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;

            var query = _context.TrainingEvaluation.AsQueryable();

            // Apply filtering
            if (!string.IsNullOrEmpty(searchTitle))
                query = query.Where(t => t.Title.Contains(searchTitle));
            if (!string.IsNullOrEmpty(searchVenue))
                query = query.Where(t => t.Venue.Contains(searchVenue));
            if (!string.IsNullOrEmpty(searchCategory))
                query = query.Where(t => t.ProfessionalCategory.Contains(searchCategory));
            if (searchDate.HasValue)
                query = query.Where(t => t.DateOfTraining.Date == searchDate.Value.Date);

            // Fetch list first (async) and apply pagination later
            var trainingList = await query.OrderBy(t => t.DateOfTraining)
                                          .AsNoTracking()
                                          .ToListAsync();

            var paginatedList = trainingList.ToPagedList(pageNumber, pageSize);

            return View(paginatedList);
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
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }



        // POST: TrainingSummary/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("Id,Title,Venue,DateOfTraining,TrainingMethodology,ProfessionalCategory,TotalParticipantsMale,TotalParticipantsFemale,PostTestEvaluationGrade,FinalRating,FlowFollowed,RulesEstablished,InitiateDiscussion,TechnicalCapability,ContentOrganization,ObjectiveStated,ContentQuality,FlowOfTopic,RelevanceOfTopic,PracticeApplication,LearningActivities,VisualAids,PresentKnowledge,BalancePrinciples,AddressClarifications,Preparedness,TeachingPersonality,EstablishRapport,RespectForParticipants,VoicePersonality,TimeManagement,SMELecturer,SuggestionsForImprovement, SayToSpeaker")] TrainingEvaluation trainingEvaluation)
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Venue,DateOfTraining,TrainingMethodology,ProfessionalCategory,TotalParticipantsMale,TotalParticipantsFemale,PostTestEvaluationGrade,FinalRating,FlowFollowed,RulesEstablished,InitiateDiscussion,TechnicalCapability,ContentOrganization,ObjectiveStated,ContentQuality,FlowOfTopic,RelevanceOfTopic,PracticeApplication,LearningActivities,VisualAids,PresentKnowledge,BalancePrinciples,AddressClarifications,Preparedness,TeachingPersonality,EstablishRapport,RespectForParticipants,VoicePersonality,TimeManagement,SMELecturer,SuggestionsForImprovement, SayToSpeaker")] TrainingEvaluation trainingEvaluation)
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
        public async Task<IActionResult> Reports()
        {
            // Fetch all PreTestClinical and PostTestClinical data
            var preTestClinicals = await _context.PreTestClinicals.ToListAsync();
            var postTestClinicals = await _context.PostTestClinicals.ToListAsync();

            // Perform grouping and select the latest PreTestClinical and PostTestClinical in memory
            var clinicalData = preTestClinicals
                .GroupBy(pre => pre.EmployeeId)
                .Select(grouped =>
                {
                    var latestPre = grouped.OrderByDescending(pre => pre.DateCreated).FirstOrDefault();
                    var latestPost = postTestClinicals
                        .Where(post => post.EmployeeId == grouped.Key)
                        .OrderByDescending(post => post.DateCreated)
                        .FirstOrDefault();

                    return new
                    {
                        latestPre.FullName,
                        latestPre.EmployeeId,
                        latestPre.AgeGroup,
                        latestPre.Sex,
                        latestPre.PWD,
                        latestPre.CivilStatus,
                        latestPre.Department,
                        PRETCSCORE = latestPre.PRETCSCORE,
                        POSTCSCORE = latestPost != null ? latestPost.POSTCSCORE : (float?)null,
                        latestPre.DateCreated

                    };
                })
                .ToList();

            // Fetch all PreTestNonClinical and PostTestNonClinical data
            var preTestNonClinicals = await _context.PreTestNonClinicals.ToListAsync();
            var postTestNonClinicals = await _context.PostTestNonCLinicals.ToListAsync();

            // Perform grouping and select the latest PreTestNonClinical and PostTestNonClinical in memory
            var nonClinicalData = preTestNonClinicals
                .GroupBy(preNon => preNon.EmployeeId)
                .Select(grouped =>
                {
                    var latestPreNon = grouped.OrderByDescending(preNon => preNon.DateCreated).FirstOrDefault();
                    var latestPostNon = postTestNonClinicals
                        .Where(postNon => postNon.EmployeeId == grouped.Key)
                        .OrderByDescending(postNon => postNon.DateCreated)
                        .FirstOrDefault();

                    return new
                    {
                        latestPreNon.FullName,
                        latestPreNon.EmployeeId,
                        latestPreNon.AgeGroup,
                        latestPreNon.Sex,
                        latestPreNon.PWD,
                        latestPreNon.CivilStatus,
                        latestPreNon.Department,
                        PRETNONCSCORE = latestPreNon.PRETNONCSCORE,
                        POSTNONSCORE = latestPostNon != null ? latestPostNon.POSTNONSCORE : (int?)null,
                        latestPreNon.DateCreated
                    };
                })
                .ToList();

            // Combine both datasets into ViewData
            ViewData["ClinicalData"] = clinicalData;
            ViewData["NonClinicalData"] = nonClinicalData;

            return View();
        }

    }
}
