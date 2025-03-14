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
        private readonly int[] correctAnswers = { 0, 0, 1 }; // Example: Question 1: option 0, Question 2: option 0, etc.

        /// <summary>
        /// Displays the quiz form.
        /// </summary>
        /// <returns>The Quiz view.</returns>
        /// 

        public IActionResult Quiz()
        {
            return View();
        }


        private readonly string[] matchingCorrectAnswers = { "Hand Washing", "Hand Washing", "Hand Washing", "Hand Rubbing", "Hand Washing", "Hand Washing", "Hand Washing", "Hand Rubbing" };
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

                model.PRETNONCSCORE = score;

                _context.PreTestNonClinicals.Add(model);
                _context.SaveChanges();

                return RedirectToAction("QuizResult", new { score = model.PRETNONCSCORE });
            }

            return View("Quiz", model);
        }


        /// <param name="score">The computed score from the quiz.</param>

        public IActionResult QuizResult(int score)
        {
            ViewBag.Score = score;
            return View();
        }
    }
}
