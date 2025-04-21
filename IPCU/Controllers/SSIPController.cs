using IPCU.Models;
using Microsoft.AspNetCore.Mvc;

namespace IPCU.Controllers
{
    public class SSIPController : Controller
    {
        // GET: /SSIP/
        [HttpGet]
        public IActionResult Index()
        {
            var model = new SSIP();
            return View(model);
        }

        // POST: /SSIP/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(SSIP model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Save the model to a file, database, or process it as needed
                TempData["Message"] = "Checklist submitted successfully!";
                return RedirectToAction("Success");
            }

            return View(model);
        }

        // GET: /SSIP/Success
        public IActionResult Success()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }
    }
}
