using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Mvc;

namespace IPCU.Controllers
{
    public class CardiovascularSystemInfectionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CardiovascularSystemInfectionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Display the form
        public IActionResult Index()
        {
            var model = new CardiovascularSystemInfection();
            return View(model);
        }

        // Submit the form
        [HttpPost]
        public IActionResult Submit(CardiovascularSystemInfection model)
        {

            if (ModelState.IsValid)
            {
                _context.CardiovascularSystemInfection.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }
    }
}
