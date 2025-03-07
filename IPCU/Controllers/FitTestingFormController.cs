using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPCU.Data;
using IPCU.Models;
using X.PagedList;
using X.PagedList.Mvc.Core;
using X.PagedList.Extensions;

namespace IPCU.Controllers
{
    public class FitTestingFormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FitTestingFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FitTestingForm

    public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 20; // Number of records per page
            int pageNumber = page ?? 1; // If no page number is specified, default to 1

            var fitTestingForms = await _context.FitTestingForm.ToListAsync();
            var pagedList = fitTestingForms.ToPagedList(pageNumber, pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = pagedList.PageCount;

            return View(pagedList);
        }


    // GET: FitTestingForm/Details/5
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: FitTestingForm/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FitTestingForm fitTestingForm, string? OtherLimitation)
        {
            // Remove OtherLimitation from ModelState since it's not in our model
            ModelState.Remove("OtherLimitation");

            try
            {
                var limitations = Request.Form["Limitation"].ToList();

                if (limitations != null && limitations.Any())
                {
                    // If "Other" is selected and has a value
                    if (limitations.Contains("Other"))
                    {
                        if (string.IsNullOrWhiteSpace(OtherLimitation))
                        {
                            ModelState.AddModelError("Limitation", "Please specify the other limitation");
                            return View(fitTestingForm);
                        }
                        limitations.Remove("Other");
                        limitations.Add(OtherLimitation.Trim());
                    }

                    // Concatenate the limitations into a single string
                    fitTestingForm.Limitation = string.Join(", ", limitations.Where(x => !string.IsNullOrWhiteSpace(x)));
                }
                else
                {
                    fitTestingForm.Limitation = "None";
                }

                if (ModelState.IsValid)
                {
                    fitTestingForm.SubmittedAt = DateTime.Now;
                    fitTestingForm.ExpiringAt = fitTestingForm.SubmittedAt.AddYears(1); // Set ExpiringAt
                    _context.Add(fitTestingForm);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error processing limitations: " + ex.Message);
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


    }
}
