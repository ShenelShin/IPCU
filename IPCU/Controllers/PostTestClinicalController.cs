using Microsoft.AspNetCore.Mvc;
using IPCU.Models;
using System.Linq;
using IPCU.Data;

namespace IPCU.Controllers
{
    public class PostTestClinicalController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostTestClinicalController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new PostTestClinical());
        }

        [HttpPost]
        public IActionResult Submit(PostTestClinical model, string[] Question18, string[] Question19, string[] Question20, string[] Question21,
     string[] Question22, string[] Question23, string[] Question24, string[] Question25, string[] Question26,
     string[] Question27, string[] Question28, string[] Question29)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            try
            {
                // Compute the score
                float score = 0.0f;
                string[] correctAnswers = {
            "Airborne precaution", "Airborne precaution", "Airborne precaution", "Airborne precaution",
            "Airborne precaution", "Airborne precaution", "Airborne precaution", "Airborne precaution",
            "Standard Precautions", "8 ACH", "Within the week", "Gown-Gloves-Mask-Goggles",
            "Reduce hospital costs", "5-10 seconds", "Surgical mask", "1 foot", "Wearing a mask"
        };

                for (int i = 1; i <= correctAnswers.Length; i++)
                {
                    if (Request.Form[$"Question{i}"] == correctAnswers[i - 1])
                    {
                        score += 1.0f;
                    }
                }

                // Validate multi-select answers
                string[][] multiSelectQuestions = { Question18, Question19, Question20, Question21, Question22, Question23, Question24 };
                foreach (var question in multiSelectQuestions)
                {
                    if (question != null && question.Contains("Single room only") && question.Contains("Ward type"))
                    {
                        score += 1.0f;
                    }
                }

                string[][] multiSelectQuestions2 = { Question25, Question26, Question27, Question28, Question29 };
                foreach (var question in multiSelectQuestions2)
                {
                    if (question != null && question.Contains("Gloves") && question.Contains("Isolation Gown"))
                    {
                        score += 1.8f;
                    }
                }

                // Check if an entry already exists for the given EmployeeId
                var existingEntry = _context.TrainingSummaries.FirstOrDefault(ts => ts.EmployeeId == model.EmployeeId);

                if (existingEntry != null)
                {
                    // Update the existing record
                    existingEntry.PostScore = (int)score;
                    existingEntry.DateCreated = DateTime.Now;
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
                        PreScore = 0, // Assuming this is a Post-Test, PreScore remains 0
                        PostScore = (int)score,
                        DateCreated = DateTime.Now
                    };

                    _context.TrainingSummaries.Add(trainingSummary);
                }

                // Save changes to the database
                _context.SaveChanges();

                // Redirect to Success action with the model ID
                return RedirectToAction("Success", new { id = model.EmployeeId });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                ModelState.AddModelError("", "An error occurred while saving the data.");
                return View("Index", model);
            }
        }

        [HttpGet("Success")]
        public IActionResult Success(int id)
        {
            var model = _context.TrainingSummaries.Find(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
