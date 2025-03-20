using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IPCU.Data;
using IPCU.Models;
using Microsoft.EntityFrameworkCore;

namespace IPCU.Controllers
{
    public class VentilatorEventChecklistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VentilatorEventChecklistController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(VentilatorEventChecklist model, string[] TypeClass)
        {
            if (ModelState.IsValid)
            {
                // Concatenate selected TypeClass values into a single string
                if (TypeClass != null && TypeClass.Length > 0)
                {
                    model.TypeClass = string.Join(", ", TypeClass);
                }

                // Add and save the model to the database
                _context.VentilatorEventChecklists.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            // If the model is invalid, return to the form with the entered data
            return View("Index", model);
        }
    }
}