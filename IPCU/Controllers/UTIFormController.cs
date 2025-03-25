using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Mvc;

namespace IPCU.Controllers
{

    public class UTIFormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UTIFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new UTIFormModel();
            return View(model); // No need to specify view name
        }
        [HttpPost]
        public IActionResult Submit(UTIFormModel model)
        {
            var nullableFields = new List<string>
            {
                "CultureResults",
                "SUTI1b_CultureResults",
                "SUTI2_CultureResults",
                "ABUTI_CultureResults"
            };

            // Loop through each field, clear validation errors, set empty string if empty
            foreach (var field in nullableFields)
            {
                if (string.IsNullOrWhiteSpace(ModelState[field]?.AttemptedValue))
                {
                    // Set empty string instead of null (non-nullable fields need non-null value)
                    typeof(UTIFormModel).GetProperty(field)?.SetValue(model, "");
                }

                // Clear validation error for this field
                ModelState[field]?.Errors.Clear();
            }

            if (ModelState.IsValid)
            {
                _context.UTIModels.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }
    }
}
