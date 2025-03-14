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
                    if (Request.Form["Question1"] == "Airborne precaution") // Correct answer for Q1
                        score += 1.0f;
                    if (Request.Form["Question2"] == "Airborne precaution")
                        score += 1.0f;
                    if (Request.Form["Question3"] == "Airborne precaution")
                        score += 1.0f;
                    if (Request.Form["Question4"] == "Airborne precaution")
                        score += 1.0f;
                    if (Request.Form["Question5"] == "Airborne precaution")
                        score += 1.0f;
                    if (Request.Form["Question6"] == "Airborne precaution")
                        score += 1.0f;
                    if (Request.Form["Question7"] == "Airborne precaution")
                        score += 1.0f;
                    if (Request.Form["Question8"] == "Airborne precaution")
                        score += 1.0f;
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

                    model.POSTCSCORE = score;

                    // Save to database
                    _context.PostTestClinicals.Add(model);
                    _context.SaveChanges();

                    // Pass the model ID to the Success action
                    return RedirectToAction("Success", new { id = model.Id });
                }
                catch (Exception ex)
                {
                    // Log the exception
                    Console.WriteLine(ex.InnerException?.Message);
                    ModelState.AddModelError("", "An error occurred while saving the data.");
                }
            }
            return View("Index", model);
        }

        public IActionResult Success(int id)
        {
            var model = _context.PostTestClinicals.Find(id);
            if (model == null)
            {
                return RedirectToAction("Index"); // Redirect if no model is found
            }
            return View(model);
        }
    }
}
