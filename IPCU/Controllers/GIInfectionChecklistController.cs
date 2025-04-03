using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IPCU.Data;
using IPCU.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task<IActionResult> PatientIndex(string hospNum)
        {
            if (string.IsNullOrEmpty(hospNum))
            {
                return NotFound("Hospital Number is required.");
            }

            var patients = await _context.GIInfectionChecklists
                                         .Where(p => p.HospNum == hospNum)
                                         .ToListAsync();

            return View(patients);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.GIInfectionChecklists
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

    }
}
