using IPCU.Models;
using Microsoft.AspNetCore.Mvc;
using IPCU.Data;
using IPCU.Models.InfectionControl.Models; // Replace with your actual DbContext namespace

namespace IPCU.Controllers
{
    public class InfectionControlMonitoringFormsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InfectionControlMonitoringFormsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Submit(InfectionControlMonitoringForm model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            _context.InfectionControlMonitoringForm.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Table()
{
            var forms = _context.InfectionControlMonitoringForms.ToList();

            if (forms == null || !forms.Any())
            {
                // Log or handle the case where no data is returned.
                // For example, return an empty list to the view.
                TempData["NoDataMessage"] = "No records found.";
            }

            return View(forms); // This will pass the data to the view
        }

    }
}
