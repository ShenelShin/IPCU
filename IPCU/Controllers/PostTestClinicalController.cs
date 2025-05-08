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
        public IActionResult Submit(PostTestClinical model, string[] Question18, string[] Question19, string[] Question20,
    string[] Question21, string[] Question22, string[] Question23, string[] Question24,
    string[] Question25, string[] Question26, string[] Question27, string[] Question28, string[] Question29)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            try
            {
                float score = 0.0f;
                float totalPossibleScore = 29.0f;

                // Single-answer questions (1 point each)
                if (Request.Form["Question1"] == "3") score += 1.25f;
                if (Request.Form["Question2"] == "2") score += 1.25f;
                if (Request.Form["Question3"] == "1") score += 1.25f;
                if (Request.Form["Question4"] == "2") score += 1.25f;
                if (Request.Form["Question5"] == "0") score += 1.25f;
                if (Request.Form["Question6"] == "2") score += 1.25f;
                if (Request.Form["Question7"] == "3") score += 1.25f;
                if (Request.Form["Question8"] == "3") score += 1.25f;

                if (Request.Form["Question9"] == "1") score += 1.0f;
                if (Request.Form["Question10"] == "2") score += 1.0f;
                if (Request.Form["Question11"] == "1") score += 1.0f;
                if (Request.Form["Question12"] == "3") score += 1.0f;
                if (Request.Form["Question13"] == "2") score += 1.0f;
                if (Request.Form["Question14"] == "2") score += 1.0f;
                if (Request.Form["Question15"] == "1") score += 1.0f;
                if (Request.Form["Question16"] == "2") score += 1.0f;
                if (Request.Form["Question17"] == "1") score += 1.0f;

                if (Request.Form["Question18"] == "2") score += 1.0f;
                if (Request.Form["Question19"] == "0") score += 1.0f;
                if (Request.Form["Question20"] == "0") score += 1.0f;
                if (Request.Form["Question21"] == "2") score += 1.0f;
                if (Request.Form["Question22"] == "2") score += 1.0f;
                if (Request.Form["Question23"] == "0") score += 1.0f;
                if (Request.Form["Question24"] == "1") score += 1.0f;

                if (Request.Form["Question25"] == "0") score += 1.8f;

                if (Request.Form["Question26"] == "0") score += 1.8f;

                // Multi-answer questions (1 point each)

                string[] q27 = Request.Form["Question27"].ToString().Split(','); // correct: 0,1
                string[] correct27 = { "0", "1" };
                if (q27 != null && correct27.All(q27.Contains) && q27.All(correct27.Contains)) score += 1.88f;

                string[] q28 = Request.Form["Question28"].ToString().Split(','); // correct: 0,3,4
                string[] correct28 = { "0", "3", "4" };
                if (q28 != null && correct28.All(q28.Contains) && q28.All(correct28.Contains)) score += 1.8f;

                string[] q29 = Request.Form["Question29"].ToString().Split(','); // correct: 0,2,4
                string[] correct29 = { "0", "2", "4" };
                if (q29 != null && correct29.All(q29.Contains) && q29.All(correct29.Contains)) score += 1.8f;


                // 1. First round the raw score to whole number
                float roundedScore = (float)Math.Round(score);  // This removes all decimals

                // 2. Then calculate percentage using the rounded score
                float percentageScore = (roundedScore / totalPossibleScore) * 100;
                percentageScore = (float)Math.Round(percentageScore);  // Remove decimal places from percentage

                // 3. Save results with fully rounded values
                var existingEntry = _context.TrainingSummaries
                    .FirstOrDefault(ts => ts.EmployeeId == model.EmployeeId);

                if (existingEntry != null)
                {
                    existingEntry.PostScore = roundedScore;  // Use the pre-rounded score
                    existingEntry.Rate = percentageScore;
                    existingEntry.DateCreated = DateTime.Now;
                    _context.Update(existingEntry);
                }
                else
                {
                    _context.TrainingSummaries.Add(new TrainingSummary
                    {
                        FullName = model.FullName,
                        EmployeeId = model.EmployeeId,
                        AgeGroup = model.AgeGroup,
                        Sex = model.Sex,
                        PWD = model.PWD,
                        CivilStatus = model.CivilStatus,
                        Department = model.Department,
                        PostScore_Total = totalPossibleScore,
                        PostScore = roundedScore,  // Use the pre-rounded score
                        Rate = percentageScore,
                        DateCreated = DateTime.Now
                    });
                }

                // 4. Pass the rounded values to your view
                ViewBag.DisplayScore = ((int)roundedScore).ToString();  // Force integer display
                ViewBag.DisplayTotal = ((int)totalPossibleScore).ToString();

                _context.SaveChanges();

                var entryId = existingEntry?.Id ?? _context.TrainingSummaries
                    .FirstOrDefault(ts => ts.EmployeeId == model.EmployeeId)?.Id;

                return RedirectToAction("Success", new { id = entryId });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n{ex.StackTrace}");
                ModelState.AddModelError("", "An error occurred while processing your submission.");
                return View("Index", model);
            }
        }

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
