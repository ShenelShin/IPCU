using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace IPCU.Controllers
{
    public class TestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context)
        {
            _context = context;
        }

        private static readonly Dictionary<string, string> correctAnswers = new()
        {
            { "AfterCoveringCough", "Hand Washing" },
            { "AfterHandlingMoney", "Hand Washing" },
            { "AfterSneezing", "Hand Washing" },
            { "AfterAttendanceTimeIn", "Hand Rubbing" },
            { "BeforeEating", "Hand Washing" },
            { "AfterUsingBathroom", "Hand Washing" },
            { "BeforeDrinkingMedication", "Hand Washing" },
            { "AfterSigningDocuments", "Hand Rubbing" },
            { "UsedFaceMasks", "Yellow waste bin" },
            { "MaskUseReason", "To protect patients and healthcare workers from respiratory infections, including influenza, tuberculosis, and other airborne diseases" },
            { "NeedleStickReportTime", "1 hour" },
            { "InfectionPrevention", "Frequent and proper hand hygiene" },
            { "HandRubbingTime", "20-30 seconds" },
            { "IPCGoal", "Prevent the spread of infections among patients, healthcare workers, and visitors" },
            { "ProperMaskUse", "Masks should fully cover the nose and mouth at all times inside the hospital" }
        };

        public IActionResult TakeTest()
        {
            return View();
        }

        public IActionResult Instructions()
        {
            return View();
        }
        public IActionResult Instructions2()
        {
            return View();
        }

        public IActionResult StartExam()
        {
            return View("Form");
        }

        [HttpPost]
        public IActionResult SubmitForm(TestFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", model);
            }

            int score = 0;

            foreach (var answer in correctAnswers)
            {
                var property = model.GetType().GetProperty(answer.Key);
                if (property != null)
                {
                    var userAnswer = property.GetValue(model)?.ToString();
                    if (userAnswer == answer.Value)
                    {
                        score++;
                    }
                }
            }

            model.Score = score;

            _context.TestForms.Add(model);
            _context.SaveChanges();

            ViewBag.Score = score;
            return View("Result", model);
        }

        public IActionResult TestResult(int id)
        {
            var testResult = _context.TestForms.FirstOrDefault(t => t.Id == id);
            if (testResult == null)
            {
                return NotFound();
            }

            return View(testResult);
        }
    }
}
