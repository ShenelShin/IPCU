using Microsoft.AspNetCore.Mvc;
using IPCU.Models;
namespace IPCU.Controllers
{
    public class InfectionChecklistController : Controller
    {
        public ActionResult Index()
        {
            var model = new SSTInfectionModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Submit(SSTInfectionModel model)
        {
            if (ModelState.IsValid)
            {
                // Handle form submission logic here (save, process, etc.)
                TempData["Success"] = "Checklist submitted successfully!";
                return RedirectToAction("Index");
            }
            return View("Index", model);
        }
    }
}