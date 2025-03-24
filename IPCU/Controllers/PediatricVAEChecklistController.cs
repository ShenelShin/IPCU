using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Mvc;

namespace IPCU.Controllers
{
    public class PediatricVAEChecklistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PediatricVAEChecklistController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display the form
        public IActionResult Index()
        {
            var model = new PediatricVAEChecklist();
            return View(model);
        }

        // Handle form submission
        [HttpPost]
        public IActionResult Submit(PediatricVAEChecklist model)
        {
            if (ModelState.IsValid)
            {
                _context.PediatricVAEChecklist.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }
    }
}