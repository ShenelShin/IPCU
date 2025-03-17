using Microsoft.AspNetCore.Mvc;
using IPCU.Models;
using IPCU.Data;

namespace IPCU.Controllers
{
    public class PreTestClinicalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PreTestClinicalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Correct answers for all 23 quiz questions
        private readonly int[] correctAnswers =
        {
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };

        public IActionResult Quiz()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SubmitQuiz(PreTestClinical model, int[] selectedAnswers)
        {
            if (selectedAnswers == null || selectedAnswers.Length < correctAnswers.Length)
            {
                // Fill missing answers with -1 (or default value)
                selectedAnswers = Enumerable.Repeat(-1, correctAnswers.Length).ToArray();
            }

            Console.WriteLine("Selected Answers: " + string.Join(", ", selectedAnswers));
            Console.WriteLine("Correct Answers: " + string.Join(", ", correctAnswers));

            int score = 0;

            for (int i = 0; i < correctAnswers.Length; i++)
            {
                if (i < selectedAnswers.Length && selectedAnswers[i] == correctAnswers[i])
                {
                    score++;
                }
            }

            model.PRETCSCORE = score;
            _context.PreTestClinicals.Add(model);
            _context.SaveChanges();

            return RedirectToAction("QuizResult", new { score = model.PRETCSCORE });
        }

        public IActionResult QuizResult(int score)
        {
            ViewBag.Score = score;
            return View();
        }
    }
}
