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
using ClosedXML.Excel;


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
        public IActionResult Reports()
        {
            var trainingSummaries = _context.TrainingSummaries
                .GroupJoin(
                    _context.FitTestingForm,
                    ts => ts.EmployeeId,
                    ft => ft.EmployeeId,
                    (ts, fitTests) => new
                    {
                        ts.Id,
                        ts.FullName,
                        ts.EmployeeId,
                        ts.AgeGroup,
                        ts.Department,
                        Respiratory_Type = fitTests.Select(ft => ft.Respiratory_Type).FirstOrDefault(),
                        ts.PreScore,
                        ts.PostScore_Total,
                        ts.PreScore_Total,
                        ts.PostScore,
                        ts.ProperHand,
                        ts.GloveRemoval,
                        ts.IDPrinting,
                        ts.TrainingReport,
                        ts.DateCreated,
                        ts.Rate
                    })
                .ToList()
                .Select(ts => new TrainingSummary // Convert anonymous type to model
                {
                    Id = ts.Id,
                    FullName = ts.FullName,
                    EmployeeId = ts.EmployeeId,
                    AgeGroup = ts.AgeGroup,
                    Department = ts.Department,
                    Respiratory_Type = ts.Respiratory_Type ?? "N/A",
                    PreScore = ts.PreScore,
                    PostScore = ts.PostScore,
                    PostScore_Total = ts.PostScore_Total,
                    PreScore_Total = ts.PreScore_Total,
                    ProperHand = ts.ProperHand,
                    GloveRemoval = ts.GloveRemoval,
                    IDPrinting = ts.IDPrinting,
                    TrainingReport = ts.TrainingReport,
                    DateCreated = ts.DateCreated,
                    Rate = ts.Rate
                }).ToList();

            return View(trainingSummaries);
        }



        [HttpPost]
        public IActionResult SaveActionSelection(int id, string actionType, string selectedOption)
        {
            var trainingSummary = _context.TrainingSummaries.Find(id);
            if (trainingSummary != null)
            {
                switch (actionType)
                {
                    case "Proper Hand":
                        trainingSummary.ProperHand = selectedOption;
                        break;
                    case "Glove Removal":
                        trainingSummary.GloveRemoval = selectedOption;
                        break;
                    case "ID Printing":
                        trainingSummary.IDPrinting = selectedOption;
                        break;
                }

                // ✅ Check if all actions are completed
                if (!string.IsNullOrEmpty(trainingSummary.ProperHand) &&
                    !string.IsNullOrEmpty(trainingSummary.GloveRemoval) &&
                    !string.IsNullOrEmpty(trainingSummary.IDPrinting))
                {
                    trainingSummary.TrainingReport = "Done";
                }

                _context.SaveChanges();
            }

            return RedirectToAction("Reports");
        }

        [HttpPost]
        public IActionResult UpdateTrainingReport(int id, string TrainingReport)
        {
            var trainingSummary = _context.TrainingSummaries.Find(id);
            if (trainingSummary != null)
            {
                trainingSummary.TrainingReport = TrainingReport;
                _context.SaveChanges();
            }
            return RedirectToAction("Reports");
        }
        public IActionResult ExportToExcel()
        {
            var trainingSummaries = _context.TrainingSummaries
                .GroupJoin(
                    _context.FitTestingForm,
                    ts => ts.EmployeeId,
                    ft => ft.EmployeeId,
                    (ts, fitTests) => new
                    {
                        ts.FullName,
                        ts.EmployeeId,
                        ts.AgeGroup,
                        ts.Department,
                        Respiratory_Type = fitTests.Select(ft => ft.Respiratory_Type).FirstOrDefault(),
                        ts.PreScore,
                        ts.PostScore,
                        ts.ProperHand,
                        ts.GloveRemoval,
                        ts.IDPrinting,
                        ts.TrainingReport,
                        ts.DateCreated,
                        ts.PostScore_Total,
                        ts.PreScore_Total,
                        ts.Rate

                    })
                .ToList()
                .Select(ts => new TrainingSummary // Convert anonymous type to model
                {
                    FullName = ts.FullName,
                    EmployeeId = ts.EmployeeId,
                    AgeGroup = ts.AgeGroup,
                    Department = ts.Department,
                    Respiratory_Type = ts.Respiratory_Type ?? "N/A",
                    PreScore = ts.PreScore,
                    PostScore = ts.PostScore,
                    ProperHand = ts.ProperHand,
                    GloveRemoval = ts.GloveRemoval,
                    IDPrinting = ts.IDPrinting,
                    TrainingReport = ts.TrainingReport,
                    DateCreated = ts.DateCreated,
                    Rate = ts.Rate,
                    PostScore_Total = ts.PostScore_Total,
                    PreScore_Total = ts.PreScore_Total

                }).ToList();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Training Summary");

                // Add headers
                worksheet.Cell(1, 1).Value = "Full Name";
                worksheet.Cell(1, 2).Value = "Employee ID";
                worksheet.Cell(1, 3).Value = "Age Group";
                worksheet.Cell(1, 4).Value = "Department";
                worksheet.Cell(1, 5).Value = "Respiratory Type";
                worksheet.Cell(1, 6).Value = "Score";
                worksheet.Cell(1, 7).Value = "Pre Test";
                worksheet.Cell(1, 8).Value = "Score";
                worksheet.Cell(1, 9).Value = "Post Test";
                worksheet.Cell(1, 10).Value = "Rate";
                worksheet.Cell(1, 11).Value = "Proper Hand";
                worksheet.Cell(1, 12).Value = "Glove Removal";
                worksheet.Cell(1, 13).Value = "ID Printing";
                worksheet.Cell(1, 14).Value = "Training Report";
                worksheet.Cell(1, 15).Value = "Date Created";

                // Style header row
                worksheet.Row(1).Style.Font.Bold = true;

                // Add data
                int row = 2;
                foreach (var item in trainingSummaries)
                {
                    worksheet.Cell(row, 1).Value = item.FullName;
                    worksheet.Cell(row, 2).Value = item.EmployeeId;
                    worksheet.Cell(row, 3).Value = item.AgeGroup;
                    worksheet.Cell(row, 4).Value = item.Department;
                    worksheet.Cell(row, 5).Value = item.Respiratory_Type ?? "N/A"; // Now should be correct
                    worksheet.Cell(row, 6).Value = item.PreScore;
                    worksheet.Cell(row, 7).Value = item.PreScore_Total;
                    worksheet.Cell(row, 8).Value = item.PostScore;
                    worksheet.Cell(row, 9).Value = item.PostScore_Total;
                    worksheet.Cell(row, 10).Value = item.Rate + "%";
                    worksheet.Cell(row, 11).Value = item.ProperHand;
                    worksheet.Cell(row, 12).Value = item.GloveRemoval;
                    worksheet.Cell(row, 13).Value = item.IDPrinting;
                    worksheet.Cell(row, 14).Value = item.TrainingReport;
                    worksheet.Cell(row, 15).Value = item.DateCreated.ToString("MMMM dd, yyyy");
                    row++;
                }

                // Adjust column width
                worksheet.Columns().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "TrainingSummary.xlsx");
                }
            }
        }

    }
}

