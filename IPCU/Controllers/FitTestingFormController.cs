using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPCU.Data;
using IPCU.Models;

namespace IPCU.Controllers
{
    [Route("FitTestingForm")]
    public class FitTestingFormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FitTestingFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FitTestingForm
        [HttpGet("")]
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10, string expiryFilter = "")
        {
            // Get the total number of records
            var fitTests = _context.FitTestingForm.AsQueryable();
            DateTime today = DateTime.Today;

            if (!string.IsNullOrEmpty(expiryFilter))
            {
                switch (expiryFilter)
                {
                    case "1day":
                        fitTests = fitTests.Where(f => f.SubmittedAt.AddDays(30) >= today && f.SubmittedAt.AddDays(30) <= today.AddDays(1));
                        break;
                    case "1week":
                        fitTests = fitTests.Where(f => f.SubmittedAt.AddDays(30) >= today && f.SubmittedAt.AddDays(30) <= today.AddDays(7));
                        break;
                    case "2weeks":
                        fitTests = fitTests.Where(f => f.SubmittedAt.AddDays(30) >= today && f.SubmittedAt.AddDays(30) <= today.AddDays(14));
                        break;
                    default:
                        break; // No filtering applied
                }


            }


            var totalRecords = await fitTests.CountAsync();

            var paginatedData = await fitTests
                .OrderBy(f => f.Id) // Optional: Order by a specific column
                .Skip((page - 1) * pageSize) // Skip the records of previous pages
                .Take(pageSize) // Take only the records for the current page
                .ToListAsync();

            // Pass pagination details to the view using ViewBag
            ViewBag.CurrentPage = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
            ViewBag.ExpiryFilter = expiryFilter;

            return View(paginatedData);
        }



        // GET: FitTestingForm/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitTestingForm = await _context.FitTestingForm
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fitTestingForm == null)
            {
                return NotFound();
            }

            return View(fitTestingForm);
        }

        // GET: FitTestingForm/Create
        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: FitTestingForm/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FitTestingForm fitTestingForm, string OtherLimitation)
        {
            if (ModelState.IsValid)
            {
                // If "Other" is checked and has a value, add it to Limitation list
                if (!string.IsNullOrWhiteSpace(OtherLimitation) && fitTestingForm.Limitation.Contains("Other"))
                {
                    fitTestingForm.Limitation.Remove("Other"); // Remove "Other" text
                    fitTestingForm.Limitation.Add(OtherLimitation); // Add the custom input
                }

                _context.Add(fitTestingForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fitTestingForm);
        }

        // GET: FitTestingForm/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitTestingForm = await _context.FitTestingForm.FindAsync(id);
            if (fitTestingForm == null)
            {
                return NotFound();
            }
            return View(fitTestingForm);
        }

        // POST: FitTestingForm/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HCW_Name,DUO,Limitation,Fit_Test_Solution,Sensitivity_Test,Respiratory_Type,Model,Size,Normal_Breathing,Deep_Breathing,Turn_head_side_to_side,Move_head_up_and_down,Reading,Bending_Jogging,Normal_Breathing_2,Test_Results,Name_of_Fit_Tester,DUO_Tester,SubmittedAt")] FitTestingForm fitTestingForm)
        {
            if (id != fitTestingForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fitTestingForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FitTestingFormExists(fitTestingForm.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(fitTestingForm);
        }

        // GET: FitTestingForm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitTestingForm = await _context.FitTestingForm
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fitTestingForm == null)
            {
                return NotFound();
            }

            return View(fitTestingForm);
        }

        // POST: FitTestingForm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fitTestingForm = await _context.FitTestingForm.FindAsync(id);
            if (fitTestingForm != null)
            {
                _context.FitTestingForm.Remove(fitTestingForm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FitTestingFormExists(int id)
        {
            return _context.FitTestingForm.Any(e => e.Id == id);
        }

        [HttpGet("GeneratePdf/{id}")]
        public IActionResult GeneratePdf(int id)
        {
            var form = _context.FitTestingForm.FirstOrDefault(f => f.Id == id);
            if (form == null) return NotFound();

            var pdfService = new FitTestingFormPdfService();
            var pdfBytes = pdfService.GeneratePdf(form);

            return File(pdfBytes, "application/pdf"); // This ensures the browser previews it properly
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitFitTest(int id, FitTestingForm updatedForm)
        {
            var fitTest = _context.FitTestingForm.FirstOrDefault(f => f.Id == id);
            if (fitTest != null)
            {
                // Update the breathing and movement test fields
                fitTest.Normal_Breathing = updatedForm.Normal_Breathing;
                fitTest.Deep_Breathing = updatedForm.Deep_Breathing;
                fitTest.Turn_head_side_to_side = updatedForm.Turn_head_side_to_side;
                fitTest.Move_head_up_and_down = updatedForm.Move_head_up_and_down;
                fitTest.Reading = updatedForm.Reading;
                fitTest.Bending_Jogging = updatedForm.Bending_Jogging;
                fitTest.Normal_Breathing_2 = updatedForm.Normal_Breathing_2;

                // Increment the submission count
                fitTest.SubmissionCount++;

                // Save changes to the database
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet("Test")]
        public IActionResult Test()
        {
            return Content("Test page is working!");
        }

    }
}
