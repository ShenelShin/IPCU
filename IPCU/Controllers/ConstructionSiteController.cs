using Microsoft.AspNetCore.Mvc;
using IPCU.Data; // or wherever your ApplicationDbContext lives


using IPCU.Models; // <- Make sure this namespace matches your actual model location

namespace IPCU.Controllers
{
    public class ConstructionSiteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConstructionSiteController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            // Create an empty model to avoid NullReferenceException
            var model = new ConstructionSite();

            // Later, replace this with data from a database
            return View(model);
        }

        [HttpPost]
        public IActionResult SubmitForm(ConstructionSite model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            // Save to database
            _context.ConstructionSites.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index"); // or Index, if you prefer
        }


        public IActionResult List()
        {
            var sites = _context.ConstructionSites.ToList();
            return View(sites);
        }

        public IActionResult Details(int id)
        {
            var constructionSite = _context.ConstructionSites.FirstOrDefault(cs => cs.CSID == id);
            if (constructionSite == null)
            {
                return NotFound();
            }
            return View(constructionSite);
        }

    }
}
