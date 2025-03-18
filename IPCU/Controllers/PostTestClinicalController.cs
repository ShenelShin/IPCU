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
        public IActionResult Submit(PostTestClinical model, string[] Question18, string[] Question19, string[] Question20, string[] Question21, string[] Question22, string[] Question23, string[] Question24,
                    string[] Question25, string[] Question26, string[] Question27, string[] Question28, string[] Question29)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Compute the score
                    float score = 0.0f;
                    if (Request.Form["Question1"] == "Airborne precaution")
                        score += 1.25f;
                    if (Request.Form["Question2"] == "Airborne precaution")
                        score += 1.25f;
                    if (Request.Form["Question3"] == "Airborne precaution")
                        score += 1.25f;
                    if (Request.Form["Question4"] == "Airborne precaution")
                        score += 1.25f;
                    if (Request.Form["Question5"] == "Airborne precaution")
                        score += 1.25f;
                    if (Request.Form["Question6"] == "Airborne precaution")
                        score += 1.25f;
                    if (Request.Form["Question7"] == "Airborne precaution")
                        score += 1.25f;
                    if (Request.Form["Question8"] == "Airborne precaution")
                        score += 1.25f;
                    if (Request.Form["Question9"] == "Standard Precautions")
                        score += 1.0f;
                    if (Request.Form["Question10"] == "8 ACH")
                        score += 1.0f;
                    if (Request.Form["Question11"] == "Within the week")
                        score += 1.0f;
                    if (Request.Form["Question12"] == "Gown-Gloves-Mask-Goggles")
                        score += 1.0f;
                    if (Request.Form["Question13"] == "Reduce hospital costs")
                        score += 1.0f;
                    if (Request.Form["Question14"] == "5-10 seconds")
                        score += 1.0f;
                    if (Request.Form["Question15"] == "Surgical mask")
                        score += 1.0f;
                    if (Request.Form["Question16"] == "1 foot")
                        score += 1.0f;
                    if (Request.Form["Question17"] == "Wearing a mask")
                        score += 1.0f;
                    if (Question18 != null && Question18.Contains("Single room only") && Question18.Contains("Ward type"))
                        score += 1.0f;
                    if (Question19 != null && Question19.Contains("Single room only") && Question19.Contains("Ward type"))
                        score += 1.0f;
                    if (Question20 != null && Question20.Contains("Single room only") && Question20.Contains("Ward type"))
                        score += 1.0f;
                    if (Question21 != null && Question21.Contains("Single room only") && Question21.Contains("Ward type"))
                        score += 1.0f;
                    if (Question22 != null && Question22.Contains("Single room only") && Question22.Contains("Ward type"))
                        score += 1.0f;
                    if (Question23 != null && Question23.Contains("Single room only") && Question23.Contains("Ward type"))
                        score += 1.0f;
                    if (Question24 != null && Question24.Contains("Single room only") && Question24.Contains("Ward type"))
                        score += 1.0f;
                    if (Question25 != null && Question25.Contains("Gloves") && Question25.Contains("Isolation Gown"))
                        score += 1.8f;
                    if (Question26 != null && Question26.Contains("Gloves") && Question26.Contains("Isolation Gown"))
                        score += 1.8f;
                    if (Question27 != null && Question27.Contains("Gloves") && Question27.Contains("Isolation Gown"))
                        score += 1.8f;
                    if (Question28 != null && Question28.Contains("Gloves") && Question28.Contains("Isolation Gown"))
                        score += 1.8f;
                    if (Question29 != null && Question29.Contains("Gloves") && Question29.Contains("Isolation Gown"))
                        score += 1.8f;

                    // Calculate Rate (Percentage)
                    float maxScore = 35.0f;
                    float rate = (score / maxScore) * 100.0f;

                    // Check if there's an existing entry for the same EmployeeId
                    var existingEntry = _context.TrainingSummaries
                        .FirstOrDefault(ts => ts.EmployeeId == model.EmployeeId);

                    if (existingEntry != null)
                    {
                        // Update existing record
                        existingEntry.PostScore = score;
                        existingEntry.Rate = rate; // Save Rate
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
                            PostScore_Total = 35f,
                            CivilStatus = model.CivilStatus,
                            Department = model.Department,
                            PostScore = score,
                            Rate = rate,  // Store computed Rate
                            DateCreated = DateTime.Now
                        };

                        _context.TrainingSummaries.Add(trainingSummary);
                    }

                    // Save changes to the database
                    _context.SaveChanges();

                    // Redirect to the Success action with the ID
                    var entryId = existingEntry != null ? existingEntry.Id : _context.TrainingSummaries
                        .FirstOrDefault(ts => ts.EmployeeId == model.EmployeeId)?.Id;

                    return RedirectToAction("Success", new { id = entryId });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException?.Message);
                    ModelState.AddModelError("", "An error occurred while saving the data.");
                }
            }
            return View("Index", model);
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
