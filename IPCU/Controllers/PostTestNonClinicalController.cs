using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using IPCU.Models;
using IPCU.Data;
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
            "24 hours",                                                                          // Question 5
            "Prevent the spread of infections among patients, healthcare workers, and visitors", // Question 6
            "Masks should fully cover the nose and mouth at all times inside the hospital"       // Question 7
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
            // Validate the model
            if (!ModelState.IsValid)
            {
                return View("Quiz", model);
            }

            int totalScore = 0;
            int totalQuestions = 0;

            // Check matching type answers (Section I)
            if (selectedMatchingAnswers != null)
            {
                for (int i = 0; i < correctMatchingAnswers.Length && i < selectedMatchingAnswers.Length; i++)
                {
                    if (selectedMatchingAnswers[i] == correctMatchingAnswers[i])
                    {
                        totalScore++;
                    }
                    totalQuestions++;
                }
            }
            else
            {
                totalQuestions += correctMatchingAnswers.Length;
            }

            // Check multiple choice answers (Section II)
            if (selectedMultipleChoiceAnswers != null)
            {
                for (int i = 0; i < correctMultipleChoiceAnswers.Length && i < selectedMultipleChoiceAnswers.Length; i++)
                {
                    if (selectedMultipleChoiceAnswers[i] == correctMultipleChoiceAnswers[i])
                    {
                        totalScore++;
                    }
                    totalQuestions++;
                }
            }
            else
            {
                totalQuestions += correctMultipleChoiceAnswers.Length;
            }

            // Check waste segregation answers (Section III)
            if (selectedWasteSegregation != null)
            {
                for (int i = 0; i < correctWasteSegregationAnswers.Length && i < selectedWasteSegregation.Length; i++)
                {
                    if (selectedWasteSegregation[i] == correctWasteSegregationAnswers[i])
                    {
                        totalScore++;
                    }
                    totalQuestions++;
                }
            }
            else
            {
                totalQuestions += correctWasteSegregationAnswers.Length;
            }

            // Store the score in the model
            model.POSTSCORE = totalScore;

            // Calculate percentage (for display purposes)
            double percentageScore = (double)totalScore / totalQuestions * 100;

            // Save to database
            try
            {
                _context.PostTestNonCLinicals.Add(model);
                _context.SaveChanges();
            }
            catch (System.Exception ex)
            {
                // Log the exception
                ModelState.AddModelError("", "Unable to save the quiz result. Please try again.");
                return View("Quiz", model);
            }

            // Redirect to the result page with the score
            return RedirectToAction("QuizResult", new
            {
                id = model.Id,
                score = totalScore,
                totalQuestions = totalQuestions,
                percentageScore = percentageScore
            });
        }

        /// <summary>
        /// Displays the quiz result page.
        /// </summary>
        /// <param name="id">The ID of the submitted quiz.</param>
        /// <param name="score">The computed score from the quiz.</param>
        /// <param name="totalQuestions">The total number of questions.</param>
        /// <param name="percentageScore">The percentage score.</param>
        /// <returns>The QuizResult view.</returns>
        public IActionResult QuizResult(int id, int score, int totalQuestions, double percentageScore)
        {
            ViewBag.Id = id;
            ViewBag.Score = score;
            ViewBag.TotalQuestions = totalQuestions;
            ViewBag.PercentageScore = percentageScore;
            ViewBag.PassingScore = 70; // Assuming 70% is passing
            ViewBag.Passed = percentageScore >= 70;

            return View();
        }
    }
}