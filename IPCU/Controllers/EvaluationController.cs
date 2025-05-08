using Microsoft.AspNetCore.Mvc;
using IPCU.Data;
using IPCU.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;
using System.IO;
using System.Threading.Tasks;
using IPCU.Services;
using DocumentFormat.OpenXml.Office2010.PowerPoint;
using QuestPDF.Fluent;

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
     .ToListAsync(); // Bring data to memory first

            var evaluationGroups = evaluations
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
                    CombinedSayToSpeaker = string.Join("; ", g.Select(e => e.SayToSpeaker).Where(s => !string.IsNullOrEmpty(s))),

                    SMELecturer = g
                        .Where(e => !string.IsNullOrEmpty(e.SMELecturer))
                        .GroupBy(e => e.SMELecturer)
                        .OrderByDescending(group => group.Count())
                        .Select(group => group.Key)
                        .FirstOrDefault(),

                    Venue = g
                        .Where(e => !string.IsNullOrEmpty(e.Venue))
                        .GroupBy(e => e.Venue)
                        .OrderByDescending(group => group.Count())
                        .Select(group => group.Key)
                        .FirstOrDefault(),
                })
                .ToList();


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
            foreach (var eval in evaluationGroups)
            {
                var summary = trainingSummaries.FirstOrDefault(ts => ts.DateCreated == eval.TrainingDate.Date);
                if (summary != null)
                {
                    eval.PostTestEvaluationGrade = summary.AverageRate;
                }
            }

            return View(evaluationGroups);
        }
        public async Task<IActionResult> ExportToExcel()
        {
            // Get evaluation summaries
            var evaluations = await _context.Evaluations
               .GroupBy(e => e.TrainingDate)
               .Select(g => new EvaluationSummaryViewModel
               {
                   TrainingDate = g.Key,
                   TotalParticipants = g.Count(),
                   MaleCount = g.Count(e => e.Sex == "Male"),
                   FemaleCount = g.Count(e => e.Sex == "Female"),
                   //SMELecturer = g.Select(e => e.SMELecturer).FirstOrDefault(), // Ensure this field is selected
                   //Venue = g.Select(e => e.Venue).FirstOrDefault(), // Ensure this field is selected
                   SMELecturer = g
                        .Where(e => !string.IsNullOrEmpty(e.SMELecturer)) // Filter out null/empty values
                        .GroupBy(e => e.SMELecturer)                     // Group by SMELecturer
                        .OrderByDescending(group => group.Count())       // Order groups by count (descending)
                        .Select(group => group.Key)                      // Select the most common value
                        .FirstOrDefault(),                               // Get the majority answer or null if none exists

                    Venue = g
                        .Where(e => !string.IsNullOrEmpty(e.Venue))      // Filter out null/empty values
                        .GroupBy(e => e.Venue)                           // Group by Venue
                        .OrderByDescending(group => group.Count())       // Order groups by count (descending)
                        .Select(group => group.Key)                      // Select the most common value
                        .FirstOrDefault(),                               // Get the majority answer or null if none exists


                   // Add missing average calculations
                   AverageFlowFollowed = g.Average(e => (double?)e.FlowFollowed) ?? 0,
                   AverageRulesEstablished = g.Average(e => (double?)e.RulesEstablished) ?? 0,
                   AverageInitiateDiscussion = g.Average(e => (double?)e.InitiateDiscussion) ?? 0,
                   AverageTechnicalCapability = g.Average(e => (double?)e.TechnicalCapability) ?? 0, //program


                   AverageContentOrganization = g.Average(e => (double?)e.ContentOrganization) ?? 0,
                   AverageObjectiveStated = g.Average(e => (double?)e.ObjectiveStated) ?? 0,
                   AverageContentQuality = g.Average(e => (double?)e.ContentQuality) ?? 0,
                   AverageFlowOfTopic = g.Average(e => (double?)e.FlowOfTopic) ?? 0,
                   AverageRelevanceOfTopic = g.Average(e => (double?)e.RelevanceOfTopic) ?? 0,
                   AveragePracticeApplication = g.Average(e => (double?)e.PracticeApplication) ?? 0,
                   AverageLearningActivities = g.Average(e => (double?)e.LearningActivities) ?? 0,
                   AverageVisualAids = g.Average(e => (double?)e.VisualAids) ?? 0,
                   AveragePresentKnowledge = g.Average(e => (double?)e.PresentKnowledge) ?? 0,
                   AverageBalancePrinciples = g.Average(e => (double?)e.BalancePrinciples) ?? 0,
                   AverageAddressClarifications = g.Average(e => (double?)e.AddressClarifications) ?? 0,
                   AverageTeachingPersonality = g.Average(e => (double?)e.Preparedness) ?? 0,
                   AveragePreparedness = g.Average(e => (double?)e.TeachingPersonality) ?? 0,
                   AverageEstablishRapport = g.Average(e => (double?)e.EstablishRapport) ?? 0,
                   AverageRespectForParticipants = g.Average(e => (double?)e.RespectForParticipants) ?? 0,
                   AverageVoicePersonality = g.Average(e => (double?)e.VoicePersonality) ?? 0,
                   AverageTimeManagement = g.Average(e => (double?)e.TimeManagement) ?? 0,

                   FinalRating = Math.Round((
                                            g.Average(e => e.FlowFollowed) +
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
                                            g.Average(e => e.TimeManagement)) / 21, 2)

               })
               .ToListAsync();


            // Get TrainingSummaries
            var trainingSummaries = await _context.TrainingSummaries
                .GroupBy(t => t.DateCreated.Date)
                .Select(g => new
                {
                    DateCreated = g.Key,
                    AverageRate = g.Average(t => t.Rate)
                })
                .ToListAsync();

            // Merge PostTestEvaluationGrade
            foreach (var eval in evaluations)
            {
                var summary = trainingSummaries.FirstOrDefault(ts => ts.DateCreated == eval.TrainingDate.Date);
                if (summary != null)
                {
                    eval.PostTestEvaluationGrade = summary.AverageRate;
                }
            }

            // Group by Year and Semester
            var groupedData = evaluations
                .GroupBy(e => new { Year = e.TrainingDate.Year, Semester = e.TrainingDate.Month <= 6 ? "Jan-Jun" : "Jul-Dec" })
                .OrderBy(g => g.Key.Year)
                .ThenBy(g => g.Key.Semester)
                .ToList();

            // Create Excel file
            using var workbook = new XLWorkbook();

            foreach (var yearGroup in groupedData.GroupBy(g => g.Key.Year))
            {
                var year = yearGroup.Key;
                var worksheet = workbook.Worksheets.Add(year.ToString());

                int row = 1;

                foreach (var semesterGroup in yearGroup)
                {
                    var semester = semesterGroup.Key.Semester;

                    // Add Title
                    worksheet.Cell(row, 1).Value = $"{year} - {semester}";
                    worksheet.Range(row, 1, row, 8).Merge().Style.Font.SetBold().Font.FontSize = 14;
                    row++;
                    // Add Headers (First Row)
                    var headers = new[]
                    {
                        "BASIC INFECTION PREVENTION AND CONTROL TRAINING"
                    };

                    // Merge the first row headers for better structure
                    // BASIC INFECTION PREVENTION AND CONTROL TRAINING
                    worksheet.Cell(row, 1).Value = "BASIC INFECTION\nPREVENTION AND CONTROL TRAINING";
                    worksheet.Cell(row, 1).Style.Alignment.WrapText = true;
                    worksheet.Range(row, 1, row, 4).Merge();
                    worksheet.Range(row, 1, row, 4).Style.Font.SetBold();
                    worksheet.Range(row, 1, row, 4).Style.Font.FontSize = 11;
                    worksheet.Range(row, 1, row, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range(row, 1, row, 4).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                    // Program Header
                    worksheet.Cell(row, 5).Value = "Program";
                    worksheet.Range(row, 5, row, 9).Merge();
                    worksheet.Range(row, 5, row, 9).Style.Font.SetBold();
                    worksheet.Range(row, 5, row, 9).Style.Font.FontSize = 11;
                    worksheet.Range(row, 5, row, 9).Style.Fill.SetBackgroundColor(XLColor.FromArgb(255, 228, 196)); // Light Peach
                    worksheet.Range(row, 5, row, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range(row, 5, row, 9).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                    // Speaker Header
                    worksheet.Cell(row, 10).Value = "Speaker";
                    worksheet.Range(row, 10, row, 27).Merge();
                    worksheet.Range(row, 10, row, 27).Style.Font.SetBold();
                    worksheet.Range(row, 10, row, 27).Style.Font.FontSize = 11;
                    worksheet.Range(row, 10, row, 27).Style.Fill.SetBackgroundColor(XLColor.FromArgb(193, 255, 193)); // Light Pastel Green
                    worksheet.Range(row, 10, row, 27).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range(row, 10, row, 27).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);

                    // Overall Header
                    worksheet.Cell(row, 28).Value = "Overall";
                    worksheet.Range(row, 28, row, 28).Merge();
                    worksheet.Range(row, 28, row, 28).Style.Font.SetBold();
                    worksheet.Range(row, 28, row, 28).Style.Font.FontSize = 11;
                    worksheet.Range(row, 28, row, 28).Style.Fill.SetBackgroundColor(XLColor.FromArgb(255, 255, 204)); // Light Pastel Yellow
                    worksheet.Range(row, 28, row, 28).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    worksheet.Range(row, 28, row, 28).Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);




                    for (int i = 0; i < headers.Length; i++)
                    {
                        var cell = worksheet.Cell(row, i + 1);
                        cell.Value = headers[i];
                        cell.Style.Font.SetBold();
                        cell.Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        cell.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                    }

                    row++; // Move to the second row

                    // Add Second Row of Headers
                    var subHeaders = new[]
                    {
                        "Date",
                        "Total Participants",
                        "Male",
                        "Female",
                        "1", //AverageFlowFollowed
                        "2", //AverageRulesEstablished
                        "3", //AverageInitiateDiscussion
                        "4", //AverageTechnicalCapability
                        "AVG",
                        "1",
                        "2",
                        "3",
                        "4",
                        "5",
                        "6",
                        "7",
                        "8",
                        "1",
                        "2",
                        "3",
                        "1",
                        "2",
                        "3",
                        "4",
                        "5",
                        "6",
                        "AVG",
                        "Final AVG",
                        "Venue",
                        "Speaker Name",
                        "Post Test Evaluation Grade"
                    };

                    for (int i = 0; i < subHeaders.Length; i++)
                    {
                        var cell = worksheet.Cell(row, i + 1);
                        cell.Value = subHeaders[i];
                        cell.Style.Font.SetBold();
                        cell.Style.Fill.SetBackgroundColor(XLColor.LightGray);
                        cell.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        cell.Style.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                        cell.Style.Alignment.WrapText = true;
                    }

                    // Increase row height for visibility
                    worksheet.Row(row - 1).Height = 25; // First header row
                    worksheet.Row(row).Height = 30; // Second header row

                    row++; // Move to next row (for data insertion)


                    // Add Data
                    foreach (var eval in semesterGroup)
                    {
                        worksheet.Cell(row, 1).Value = eval.TrainingDate.ToString("MMMM d, yyyy");
                        worksheet.Cell(row, 2).Value = eval.TotalParticipants;
                        worksheet.Cell(row, 3).Value = eval.MaleCount;
                        worksheet.Cell(row, 4).Value = eval.FemaleCount;
                        worksheet.Cell(row, 5).Value = eval.AverageFlowFollowed; //start of program
                        worksheet.Cell(row, 6).Value = eval.AverageRulesEstablished;
                        worksheet.Cell(row, 7).Value = eval.AverageInitiateDiscussion;
                        worksheet.Cell(row, 8).Value = eval.AverageTechnicalCapability;
                        double averageProgram = Math.Round(eval.AverageFlowFollowed + eval.AverageRulesEstablished + eval.AverageInitiateDiscussion + eval.AverageTechnicalCapability) / 4;
                        worksheet.Cell(row, 9).Value = averageProgram;
                        worksheet.Cell(row, 10).Value = eval.AverageContentOrganization; //start of speaker
                        worksheet.Cell(row, 11).Value = eval.AverageObjectiveStated ;
                        worksheet.Cell(row, 12).Value = eval.AverageContentQuality; 
                        worksheet.Cell(row, 13).Value = eval.AverageFlowOfTopic;
                        worksheet.Cell(row, 14).Value = eval.AverageRelevanceOfTopic;
                        worksheet.Cell(row, 15).Value = eval.AveragePracticeApplication;
                        worksheet.Cell(row, 16).Value = eval.AverageLearningActivities;
                        worksheet.Cell(row, 17).Value = eval.AverageVisualAids;
                        worksheet.Cell(row, 18).Value = eval.AveragePresentKnowledge;
                        worksheet.Cell(row, 19).Value = eval.AverageBalancePrinciples;
                        worksheet.Cell(row, 20).Value = eval.AverageAddressClarifications;
                        worksheet.Cell(row, 21).Value = eval.AveragePreparedness;
                        worksheet.Cell(row, 22).Value = eval.AverageTeachingPersonality;
                        worksheet.Cell(row, 23).Value = eval.AverageEstablishRapport;
                        worksheet.Cell(row, 24).Value = eval.AverageRespectForParticipants;
                        worksheet.Cell(row, 25).Value = eval.AverageVoicePersonality;
                        worksheet.Cell(row, 26).Value = eval.AverageTimeManagement;
                        double averageSpeaker = Math.Round(
                             (eval.AverageContentOrganization + eval.AverageObjectiveStated + eval.AverageContentQuality +
                              eval.AverageFlowOfTopic + eval.AverageRelevanceOfTopic + eval.AveragePracticeApplication + eval.AverageLearningActivities + eval.AverageVisualAids +
                              eval.AveragePresentKnowledge + eval.AverageBalancePrinciples +
                              eval.AverageAddressClarifications + eval.AveragePreparedness + eval.AverageTeachingPersonality + eval.AverageEstablishRapport +
                              eval.AverageRespectForParticipants + eval.AverageVoicePersonality + eval.AverageTimeManagement) / 17, 2);
                        worksheet.Cell(row, 27).Value = averageSpeaker;

                        worksheet.Cell(row, 28).Value = eval.FinalRating;
                        worksheet.Cell(row, 29).Value = eval.Venue;
                        worksheet.Cell(row, 30).Value = eval.SMELecturer;


                        row++;
                    }

                    row += 2; // Add some spacing before next semester
                }

                worksheet.Columns().AdjustToContents(); // Auto adjust column width
            }

            // Save file
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            var content = stream.ToArray();

            return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Evaluation_Summary.xlsx");
        }

        public async Task<IActionResult> GeneratePdf(string id)
        {
            // Parse the ID (if it's a date, convert it back to DateTime)
            var trainingDate = DateTime.ParseExact(id, "yyyyMMdd", null);

            // Retrieve the specific evaluation summary
            var evaluation = await _context.Evaluations
                .Where(e => e.TrainingDate == trainingDate)
                .GroupBy(e => e.TrainingDate)
                .Select(g => new EvaluationSummaryViewModel
                {
                    TrainingDate = g.Key,
                    TotalParticipants = g.Count(),
                    MaleCount = g.Count(e => e.Sex == "Male"),
                    FemaleCount = g.Count(e => e.Sex == "Female"),
                    AverageFlowFollowed = g.Average(e => (double?)e.FlowFollowed) ?? 0,
                    AverageRulesEstablished = g.Average(e => (double?)e.RulesEstablished) ?? 0,
                    AverageInitiateDiscussion = g.Average(e => (double?)e.InitiateDiscussion) ?? 0,
                    AverageTechnicalCapability = g.Average(e => (double?)e.TechnicalCapability) ?? 0,
                    AverageContentOrganization = g.Average(e => (double?)e.ContentOrganization) ?? 0,
                    AverageObjectiveStated = g.Average(e => (double?)e.ObjectiveStated) ?? 0,
                    AverageContentQuality = g.Average(e => (double?)e.ContentQuality) ?? 0,
                    AverageFlowOfTopic = g.Average(e => (double?)e.FlowOfTopic) ?? 0,
                    AverageRelevanceOfTopic = g.Average(e => (double?)e.RelevanceOfTopic) ?? 0,
                    AveragePracticeApplication = g.Average(e => (double?)e.PracticeApplication) ?? 0,
                    AverageLearningActivities = g.Average(e => (double?)e.LearningActivities) ?? 0,
                    AverageVisualAids = g.Average(e => (double?)e.VisualAids) ?? 0,
                    AveragePresentKnowledge = g.Average(e => (double?)e.PresentKnowledge) ?? 0,
                    AverageBalancePrinciples = g.Average(e => (double?)e.BalancePrinciples) ?? 0,
                    AverageAddressClarifications = g.Average(e => (double?)e.AddressClarifications) ?? 0,
                    AverageTeachingPersonality = g.Average(e => (double?)e.Preparedness) ?? 0,
                    AveragePreparedness = g.Average(e => (double?)e.TeachingPersonality) ?? 0,
                    AverageEstablishRapport = g.Average(e => (double?)e.EstablishRapport) ?? 0,
                    AverageRespectForParticipants = g.Average(e => (double?)e.RespectForParticipants) ?? 0,
                    AverageVoicePersonality = g.Average(e => (double?)e.VoicePersonality) ?? 0,
                    AverageTimeManagement = g.Average(e => (double?)e.TimeManagement) ?? 0,

                    FinalRating = Math.Round((
                        g.Average(e => e.FlowFollowed) +
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
                        g.Average(e => e.TimeManagement)) / 21, 2),
                    SMELecturer = g
                        .Where(e => !string.IsNullOrEmpty(e.SMELecturer)) // Filter out null/empty values
                        .GroupBy(e => e.SMELecturer)                     // Group by SMELecturer
                        .OrderByDescending(group => group.Count())       // Order groups by count (descending)
                        .Select(group => group.Key)                      // Select the most common value
                        .FirstOrDefault(),                               // Get the majority answer or null if none exists

                    Venue = g
                        .Where(e => !string.IsNullOrEmpty(e.Venue))      // Filter out null/empty values
                        .GroupBy(e => e.Venue)                           // Group by Venue
                        .OrderByDescending(group => group.Count())       // Order groups by count (descending)
                        .Select(group => group.Key)                      // Select the most common value
                        .FirstOrDefault(),                               // Get the majority answer or null if none exists


                    CombinedSuggestions = string.Join("; ", g.Where(e => !string.IsNullOrEmpty(e.SuggestionsForImprovement))
                                     .Select(e => e.SuggestionsForImprovement)),

                    CombinedSayToSpeaker = string.Join("; ", g.Where(e => !string.IsNullOrEmpty(e.SayToSpeaker))
                                      .Select(e => e.SayToSpeaker))
                })
                .FirstOrDefaultAsync();

            if (evaluation == null)
            {
                return NotFound("Evaluation not found.");
            }

            // Retrieve the post-evaluation average for the specific batch
            var trainingSummary = await _context.TrainingSummaries
                .Where(t => t.DateCreated.Date == trainingDate.Date)
                .Select(t => (double?)t.Rate) // Use nullable double to handle empty results
                .AverageAsync() ?? 0; // Provide a default value of 0 if the sequence is empty

            // Assign the post-evaluation average to the EvaluationSummaryViewModel
            evaluation.PostTestEvaluationGrade = trainingSummary;

            // Create an instance of the document manually
            var document = new EvaluationReportDocument(new List<EvaluationSummaryViewModel> { evaluation });

            // Render the PDF to a byte array
            var pdfBytes = document.GeneratePdf();

            // Return the PDF file as a download
            return File(pdfBytes, "application/pdf", $"Evaluation_Summary_{id}.pdf");
        }


    }
}
