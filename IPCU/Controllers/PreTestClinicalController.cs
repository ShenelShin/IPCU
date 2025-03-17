using Microsoft.AspNetCore.Mvc;
using IPCU.Models;
using System.Linq;
using IPCU.Data;

namespace IPCU.Controllers
{
    public class PreTestClinicalController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Inject the database context
        public PreTestClinicalController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Quiz()
        {
            return View(new PreTestClinical());
        }
        [HttpPost]
        public IActionResult Submit(PreTestClinical model, string[] selectedAnswers14)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Compute the score
                    int score = 0;
                    if (Request.Form["selectedAnswers[0]"] == "0") // Correct answer for Q1
                        score += 1;
                    if (Request.Form["selectedAnswers[1]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[2]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[3]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[4]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[5]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[6]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[7]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[8]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[9]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[10]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[11]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[12]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[13]"] == "0")
                        score += 1;
                    if (selectedAnswers14 != null && selectedAnswers14.Contains("0") && selectedAnswers14.Contains("1"))
                        score += 1;
                    if (Request.Form["selectedAnswers[15]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[16]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[17]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[18]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[19]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[20]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[21]"] == "0")
                        score += 1;
                    if (Request.Form["selectedAnswers[22]"] == "0")
                        score += 1;

                    // Check if there's already an entry for the same EmployeeId
                    var existingEntry = _context.TrainingSummaries
                        .FirstOrDefault(ts => ts.EmployeeId == model.EmployeeId);

                    if (existingEntry != null)
                    {
                        // Update the existing record
                        existingEntry.FullName = model.FullName;
                        existingEntry.AgeGroup = model.AgeGroup;
                        existingEntry.Sex = model.Sex;
                        existingEntry.PWD = model.PWD;
                        existingEntry.CivilStatus = model.CivilStatus;
                        existingEntry.Department = model.Department;
                        existingEntry.PreScore = score; // Update PreScore with the new score
                        existingEntry.DateCreated = DateTime.Now; // Update the date
                        _context.TrainingSummaries.Update(existingEntry);
                    }
                    else
                    {
                        // Create a new record if no existing entry is found
                        var trainingSummary = new TrainingSummary
                        {
                            FullName = model.FullName,
                            EmployeeId = model.EmployeeId,
                            AgeGroup = model.AgeGroup,
                            Sex = model.Sex,
                            PWD = model.PWD,
                            CivilStatus = model.CivilStatus,
                            Department = model.Department,
                            PreScore = score, // Save the computed score as PreScore
                            PostScore = 0,    // Assuming this is a Pre-Test, PostScore is set to 0
                            DateCreated = DateTime.Now
                        };

                        _context.TrainingSummaries.Add(trainingSummary);
                    }

                    // Save changes to the database
                    _context.SaveChanges();

                    // Redirect to the QuizResult action with the ID
                    var entryId = existingEntry != null ? existingEntry.Id : _context.TrainingSummaries
                        .FirstOrDefault(ts => ts.EmployeeId == model.EmployeeId)?.Id;

                    return RedirectToAction("QuizResult", new { id = entryId });
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine(ex.InnerException?.Message);
                    ModelState.AddModelError("", "An error occurred while saving the data.");
                }
            }
            return View("Quiz", model);
        }


        public IActionResult QuizResult(int id)
        {
            var model = _context.TrainingSummaries.Find(id);
            if (model == null)
            {
                return RedirectToAction("Quiz"); // Redirect if no model is found
            }
            return View(model);
        }
    }
}
