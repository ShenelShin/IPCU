using Microsoft.AspNetCore.Mvc;
using IPCU.Models;
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

        // Correct answers for the quiz
        private readonly int[] correctAnswers = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, }; // Example: Question 1: option 0, Question 2: option 0, etc.

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
        /// <param name="model">The PreTestClinical model containing user details.</param>
        /// <param name="selectedAnswers">Array of selected answers (indices).</param>
        /// <returns>Redirects to the QuizResult view with the computed score.</returns>
        [HttpPost]
        public IActionResult SubmitQuiz(PreTestClinical model, int[] selectedAnswers)
        {
            // Validate the model
            if (ModelState.IsValid)
            {
                // Compute the score based on the selected answers
                int score = 0;
                for (int i = 0; i < correctAnswers.Length; i++)
                {
                    if (i < selectedAnswers.Length && selectedAnswers[i] == correctAnswers[i])
                    {
                        score++;
                    }
                }

                // Store the score in the model
                model.PRETCSCORE = score;

                // Save the model to the database
                _context.PreTestClinicals.Add(model);
                _context.SaveChanges();

                // Redirect to the result page with the score
                return RedirectToAction("QuizResult", new { score = model.PRETCSCORE });
            }

            // If validation fails, redisplay the quiz form
            return View("Quiz", model);
        }

        /// <summary>
        /// Displays the quiz result page.
        /// </summary>
        /// <param name="score">The computed score from the quiz.</param>
        /// <returns>The QuizResult view.</returns>
        public IActionResult QuizResult(int score)
        {
            ViewBag.Score = score;
            return View();
        }
    }
}
