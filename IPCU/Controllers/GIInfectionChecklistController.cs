using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IPCU.Data;
using IPCU.Models;

namespace IPCU.Controllers
{
    public class GIInfectionChecklistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GIInfectionChecklistController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Submit(GIInfectionChecklist model, string[] TypeClassification)
        {
            if (TypeClassification != null && TypeClassification.Length > 0)
            {
                model.TypeClassification = string.Join(", ", TypeClassification);
            }

            if (!ModelState.IsValid)
            {
                // Debugging: Print ModelState errors
                foreach (var error in ModelState)
                {
                    foreach (var subError in error.Value.Errors)
                    {
                        Console.WriteLine($"Field: {error.Key}, Error: {subError.ErrorMessage}");
                    }
                }

                Console.WriteLine("Model state is invalid!");
                return View("Index", model);
            }

            _context.GIInfectionChecklists.Add(model);
            await _context.SaveChangesAsync();

            Console.WriteLine("Saved successfully!");
            return RedirectToAction("Index");
        }


    }
}
