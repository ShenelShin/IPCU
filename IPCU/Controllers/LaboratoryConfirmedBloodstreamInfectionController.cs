using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Mvc;

namespace IPCU.Controllers
{
    public class LaboratoryConfirmedBloodstreamInfectionController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LaboratoryConfirmedBloodstreamInfectionController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = new LaboratoryConfirmedBSI();
            return View(model);
        }
        [HttpPost]

        public IActionResult Submit(LaboratoryConfirmedBSI model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.LaboratoryConfirmedBSI.Add(model);
                    Console.WriteLine("Saving changes to the database...");
                    _context.SaveChanges();
                    Console.WriteLine("Data saved successfully.");
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving data: {ex.Message}");
                }
            }

            // Log errors if ModelState is invalid
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine($"Validation Error: {error.ErrorMessage}");
            }

            return View("Index", model);
        }

    }
}
