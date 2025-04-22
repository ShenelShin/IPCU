using IPCU.Models;
using Microsoft.AspNetCore.Mvc;

namespace IPCU.Controllers
{
    public class MDROrderSheetController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new MDROrderSheet();
            return View(model);
        }

        // POST: /SSIP/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(MDROrderSheet model)
        {
            if (ModelState.IsValid)
            {
                // TODO: Save the model to a file, database, or process it as needed
                TempData["Message"] = "Checklist submitted successfully!";
                return RedirectToAction("Success");
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MDROrderSheet model)
        {
            if (ModelState.IsValid)
            {
                TempData["Message"] = "Checklist submitted successfully!";
                return RedirectToAction("Success");
            }

            return View("Index", model); // re-display the form with errors
        }

        // GET: /SSIP/Success
        public IActionResult Success()
        {
            ViewBag.Message = TempData["Message"];
            return View();
        }
    }
}
