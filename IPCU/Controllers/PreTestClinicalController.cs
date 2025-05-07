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
        public IActionResult Submit(PreTestClinical model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Compute the score
                    float score = 0.0f;
                    float totalPossibleScore = 25.0f; // Total maximum score

                    // Check answers for each question - based on the form structure
                    // Questions 1-12: Clinical precautions for specific conditions (1 point each)
                    if (Request.Form["Question1"] == "2") score += 1.25f; // COVID-19: Droplet precaution
                    if (Request.Form["Question2"] == "3") score += 1.25f; // Tetanus: Standard precaution
                    if (Request.Form["Question3"] == "2") score += 1.25f; // Diphtheria: Droplet precaution
                    if (Request.Form["Question4"] == "1") score += 1.25f; // MRSA: Contact precaution
                    if (Request.Form["Question5"] == "3") score += 1.25f; // HIV: Standard precaution
                    if (Request.Form["Question6"] == "0") score += 1.25f; // Varicella: Airborne precaution
                    if (Request.Form["Question7"] == "2") score += 1.25f; // Pertussis: Droplet precaution
                    if (Request.Form["Question8"] == "2") score += 1.25f; // Human Metapneumovirus: Droplet precaution
                    if (Request.Form["Question9"] == "0") score += 1.25f; // Pulmonary TB: Airborne precaution
                    if (Request.Form["Question10"] == "0") score += 1.25f; // Measles: Airborne precaution
                    if (Request.Form["Question11"] == "1") score += 1.25f; // CRE: Contact precaution
                    if (Request.Form["Question12"] == "3") score += 1.25f; // Extrapulmonary TB: Standard precaution

                    // Question 13: PPE donning sequence (1 point)
                    if (Request.Form["Question13"] == "2") score += 1.0f; // Gown → Mask → Goggles → Gloves

                    // Question 14: Primary goal of IPC (1 point)
                    if (Request.Form["Question14"] == "2") score += 1.0f; // Prevent the spread of infections...

                    // Question 15: 5 Moments for Hand Hygiene (checkboxes) (3 points)
                    // Correct answers are: 0, 1, 4, 6, 8 (before touching patient, before procedure, after procedure, after touching patient, after surroundings)
                    string[] correctHandHygieneAnswers = { "0", "1", "4", "6", "8" };
                    string[] selectedHandHygiene = Request.Form["Question15"].ToArray();

                    // Check if all correct answers are selected and no incorrect answers
                    if (selectedHandHygiene != null &&
                        correctHandHygieneAnswers.All(a => selectedHandHygiene.Contains(a)) &&
                        selectedHandHygiene.All(a => correctHandHygieneAnswers.Contains(a)))
                    {
                        score += 5.0f;
                    }

                    // Question 16: Most effective way to prevent infections (1 point)
                    if (Request.Form["Question16"] == "1") score += 1.0f; // Frequent and proper hand hygiene

                    // Question 17: Cohorting distance (1 point)
                    if (Request.Form["Question17"] == "2") score += 1.0f; // 3 feet (1 meter)

                    // Question 18: C. difficile precautions (1 point)
                    if (Request.Form["Question18"] == "2") score += 1.0f; // Contact Precautions

                    // Question 19: Ventilation rate (1 point)
                    if (Request.Form["Question19"] == "2") score += 1.0f; // 12 air changes per hour (ACH)

                    // Question 20: Hand rubbing duration (1 point)
                    if (Request.Form["Question20"] == "2") score += 1.0f; // 20-30 seconds

                    // Question 21: TB PPE (1 point)
                    if (Request.Form["Question21"] == "1") score += 1.0f; // N95 respirator

                    // Question 22: Needle stick reporting (1 point)
                    if (Request.Form["Question22"] == "1") score += 1.0f; // 1 hour

                    // Question 23: PPE doffing sequence (1 point)
                    if (Request.Form["Question23"] == "3") score += 1.0f; // Gloves → Gown → Goggles → Mask

                    // Round the raw score to whole number FIRST
                    int roundedScore = (int)Math.Round(score);

                    // Then calculate percentage using the rounded score
                    int percentageScore = (int)Math.Round((roundedScore / totalPossibleScore) * 100);

                    // Check if EmployeeId already exists
                    var existingEntry = _context.TrainingSummaries.FirstOrDefault(ts => ts.EmployeeId == model.EmployeeId);

                    if (existingEntry != null)
                    {
                        // Store rounded values
                        existingEntry.PreScore = roundedScore;
                        existingEntry.Rate = percentageScore; // If you have this field
                        existingEntry.DateCreated = DateTime.Now;
                        _context.Update(existingEntry);
                    }
                    else
                    {
                        // Create new record with rounded values
                        var trainingSummary = new TrainingSummary
                        {
                            EmployeeId = model.EmployeeId,
                            FullName = model.FullName,
                            // [other properties...]
                            PreScore = roundedScore,
                            PreScore_Total = totalPossibleScore,
                            Rate = percentageScore, // If you have this field
                            DateCreated = DateTime.Now
                        };
                        _context.TrainingSummaries.Add(trainingSummary);
                    }

                    _context.SaveChanges();

                    // Redirect to Success view with the record ID
                    var entryId = existingEntry != null ? existingEntry.Id : _context.TrainingSummaries
                        .FirstOrDefault(ts => ts.EmployeeId == model.EmployeeId)?.Id;

                    return RedirectToAction("QuizResult", new { id = entryId });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    ModelState.AddModelError("", "An error occurred while saving the data.");
                }
            }
            return View("Index", model);
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