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
    }
}
