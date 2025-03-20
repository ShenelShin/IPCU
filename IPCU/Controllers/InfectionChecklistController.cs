using Microsoft.AspNetCore.Mvc;
using IPCU.Models;
using IPCU.Data;
namespace IPCU.Controllers
{
    public class InfectionChecklistController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Inject DbContext via constructor
        public InfectionChecklistController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            var model = new SSTInfectionModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Submit(SSTInfectionModel model)
        {
            if (ModelState.IsValid)
            {
                // Add to database
                _context.SSTInfectionModels.Add(model);
                _context.SaveChanges(); // Save

                TempData["Success"] = "Checklist submitted successfully!";
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }
    }
}