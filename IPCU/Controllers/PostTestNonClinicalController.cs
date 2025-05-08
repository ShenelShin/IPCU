using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IPCU.Controllers
{
    public class PostTestNonClinicalController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Inject the database context
        public PostTestNonClinicalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Correct answers for the matching type questions (Section I)
        private readonly string[] correctMatchingAnswers = {
            "Hand Rubbing",      // After signing documents
            "Hand Washing",      // After using the bathroom
            "Hand Washing",      // Before drinking any medication
            "Hand Washing",      // After paying/handling money
            "Hand Washing",      // After covering cough with hands
            "Hand Washing",      // Before eating
            "Hand Rubbing",      // After an attendance time-in (biometrics)
            "Hand Washing"       // After sneezing
        };

        // Correct answers for the multiple choice questions (Section II)
        private readonly string[] correctMultipleChoiceAnswers = {
            "Frequent and proper hand hygiene",                                                  // Question 1
            "Masks should fully cover the nose and mouth at all times inside the hospital",      // Question 2
            "20-30 seconds",                                                                     // Question 3
            "Prevent the spread of infections among patients, healthcare workers, and visitors", // Question 4
            "1 hour"                                                                           // Question 5
        };

        // Correct answers for the waste segregation questions (Section III)
        private readonly string[] correctWasteSegregationAnswers = {
            "Yellow waste bin",  // Used face mask
            "Green waste bin",   // Banana peel
            "Black waste bin",   // Shredded paper
            "Black waste bin",   // Plastic bags
            "Black waste bin",   // Styrofoam
            "Yellow waste bin",  // Used gloves
            "Black waste bin"    // Paper cups
        };

        /// <summary>
        /// Displays the quiz form.
        /// </summary>
        /// <returns>The Quiz view.</returns>
        public IActionResult Quiz()
        {
            return View();
        }

        /// <summary>
        /// Handles the submission of the quiz form.
        /// </summary>
        /// <returns>Redirects to the QuizResult view with the computed score.</returns>
        [HttpPost]
        public IActionResult SubmitQuiz(PostTestNonClinical model,
     string[] selectedMatchingAnswers,
     string[] selectedMultipleChoiceAnswers,
     string[] selectedWasteSegregation)
        {
            if (!ModelState.IsValid)
            {
                return View("Quiz", model);
            }

            int totalScore = 0;
            int totalQuestions = 20; // Hardcoded total number of questions

            // Validate Matching Type Answers (Section I)
            if (selectedMatchingAnswers != null)
            {
                for (int i = 0; i < correctMatchingAnswers.Length && i < selectedMatchingAnswers.Length; i++)
                {
                    if (selectedMatchingAnswers[i] == correctMatchingAnswers[i])
                    {
                        totalScore++;
                    }
                }
            }

            // Validate Multiple Choice Answers (Section II)
            if (selectedMultipleChoiceAnswers != null)
            {
                for (int i = 0; i < correctMultipleChoiceAnswers.Length && i < selectedMultipleChoiceAnswers.Length; i++)
                {
                    if (selectedMultipleChoiceAnswers[i] == correctMultipleChoiceAnswers[i])
                    {
                        totalScore++;
                    }
                }
            }

            // Validate Waste Segregation Answers (Section III)
            if (selectedWasteSegregation != null)
            {
                for (int i = 0; i < correctWasteSegregationAnswers.Length && i < selectedWasteSegregation.Length; i++)
                {
                    if (selectedWasteSegregation[i] == correctWasteSegregationAnswers[i])
                    {
                        totalScore++;
                    }
                }
            }

            // Calculate Rate (Percentage) and cast to float
            float rate = (float)((totalScore / (double)totalQuestions) * 100);

            // Check if an entry for this EmployeeId already exists
            var existingEntry = _context.TrainingSummaries.FirstOrDefault(ts => ts.EmployeeId == model.EmployeeId);

            if (existingEntry != null)
            {
                // Update existing record
                existingEntry.PostScore = totalScore;
                existingEntry.Rate = rate; // Save computed rate as float
                existingEntry.DateCreated = DateTime.Now;
                _context.TrainingSummaries.Update(existingEntry);
            }
            else
            {
                // Create a new record
                var trainingSummary = new TrainingSummary
                {
                    FullName = model.FullName,
                    EmployeeId = model.EmployeeId,
                    AgeGroup = model.AgeGroup,
                    Sex = model.Sex,
                    PostScore_Total = 20f,
                    PWD = model.PWD,
                    CivilStatus = model.CivilStatus,
                    Department = model.Department,
                    PostScore = totalScore,
                    Rate = rate, // Save computed rate as float
                    DateCreated = DateTime.Now
                };

                _context.TrainingSummaries.Add(trainingSummary);
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Unable to save the quiz result. Please try again.");
                return View("Quiz", model);
            }

            return RedirectToAction("QuizResult", new { score = totalScore, totalQuestions = totalQuestions, rate = rate });
        }

        public IActionResult QuizResult(int score, int totalQuestions)
        {
            ViewBag.Score = score;

            return View("~/Views/PostTestNonClinicals/QuizResult.cshtml");
        }

    }
}