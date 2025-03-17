using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Mvc;

namespace IPCU.Controllers
{
    public class PreTestNonClinicalController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Inject the database context
        public PreTestNonClinicalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Correct answers for the quiz
        private readonly string[] matchingCorrectAnswers =
        {
            "Hand Washing", "Hand Washing", "Hand Washing", "Hand Rubbing",
            "Hand Washing", "Hand Washing", "Hand Washing", "Hand Rubbing"
        };

        private readonly string[] multipleChoiceCorrectAnswers =
        {
            "Yellow waste bin",
            "To protect patients and healthcare workers from respiratory infections, including influenza, tuberculosis, and other airborne diseases",
            "1 hour",
            "Frequent and proper hand hygiene",
            "20-30 seconds",
            "Prevent the spread of infections among patients, healthcare workers, and visitors",
            "Masks should fully cover the nose and mouth at all times inside the hospital"
        };

        /// <summary>
        /// Displays the quiz form.
        /// </summary>
        /// <returns>The Quiz view.</returns>
        public IActionResult Quiz()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitQuiz(PreTestNonClinical model, string[] selectedMatchingAnswers, string[] selectedMultipleChoiceAnswers)
        {
            if (ModelState.IsValid)
            {
                int score = 0;

                // Validate Matching Type Answers
                for (int i = 0; i < matchingCorrectAnswers.Length; i++)
                {
                    if (i < selectedMatchingAnswers.Length && selectedMatchingAnswers[i] == matchingCorrectAnswers[i])
                    {
                        score++;
                    }
                }

                // Validate Multiple Choice Answers
                for (int i = 0; i < multipleChoiceCorrectAnswers.Length; i++)
                {
                    if (i < selectedMultipleChoiceAnswers.Length && selectedMultipleChoiceAnswers[i] == multipleChoiceCorrectAnswers[i])
                    {
                        score++;
                    }
                }

                // Check if there's already an entry for the same EmployeeId
                var existingEntry = _context.TrainingSummaries
                    .FirstOrDefault(ts => ts.EmployeeId == model.EmployeeId);

                if (existingEntry != null)
                {
                    // Update the existing record
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

                // Redirect to the QuizResult action with the score
                return RedirectToAction("QuizResult", new { score = score });
            }

            return View("Quiz", model);
        }


        /// <summary>
        /// Displays the quiz result page.
        /// </summary>
        /// <param name="score">The computed score from the quiz.</param>
        /// <returns>The QuizResult view.</returns>
        public IActionResult QuizResult(int score)
        {
            int totalQuestions = matchingCorrectAnswers.Length + multipleChoiceCorrectAnswers.Length; // Total number of questions
            double percentageScore = (double)score / totalQuestions * 100; // Calculate percentage
            bool passed = percentageScore >= 75; // Define passing criteria (e.g., 75%)

            // Pass data to the view using ViewBag
            ViewBag.Score = score;
            ViewBag.TotalQuestions = totalQuestions;
            ViewBag.PercentageScore = percentageScore;
            ViewBag.Passed = passed;

            return View();
        }
    }
}
