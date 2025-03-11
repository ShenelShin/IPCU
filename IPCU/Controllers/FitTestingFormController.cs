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

        public async Task<IActionResult> Index(int? page, bool? filterExpiring, string testResult)
        {
            int pageSize = 20;
            int pageNumber = page ?? 1;
            var fitTestingForm = _context.FitTestingForm.AsQueryable();

            // Check if filterExpiring is true
            if (filterExpiring == true)
            {
                DateTime today = DateTime.Today;
                DateTime thresholdDate = today.AddDays(30);
                fitTestingForm = fitTestingForm
                    .Where(f => f.ExpiringAt >= today && f.ExpiringAt <= thresholdDate);
            }

            if (!string.IsNullOrEmpty(testResult))
            {
                fitTestingForm = fitTestingForm.Where(f => f.Test_Results == testResult);
            }

            // Order the data in descending order (e.g., by ExpiringAt)
            fitTestingForm = fitTestingForm.OrderByDescending(f => f.ExpiringAt);

            var pagedList = fitTestingForm.ToPagedList(pageNumber, pageSize);

            // Store selected filters
            ViewData["FilterExpiring"] = filterExpiring;
            ViewData["SelectedTestResult"] = testResult;

            return View(pagedList);
        }




        // GET: FitTestingForm/Details/5
        public IActionResult Details(int id)
        {
            // Fetch the main FitTestingForm record
            var fitTestingForm = _context.FitTestingForm.FirstOrDefault(f => f.Id == id);
            if (fitTestingForm == null)
            {
                return NotFound();
            }

            // Fetch the history of attempts for the given form
            var history = _context.FitTestingFormHistory
                .Where(h => h.FitTestingFormId == id)
                .OrderBy(h => h.SubmittedAt) // Ensure chronological order
                .ToList();

            // Assign attempts based on their position in the history
            ViewData["FirstAttempt"] = history.ElementAtOrDefault(0);
            ViewData["SecondAttempt"] = history.ElementAtOrDefault(1);
            ViewData["LastAttempt"] = history.Count > 2 ? history.LastOrDefault() : null;

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
                    // Set the SubmittedAt and ExpiringAt fields
                    fitTestingForm.SubmittedAt = DateTime.Now;
                    fitTestingForm.ExpiringAt = fitTestingForm.SubmittedAt.AddYears(1); // Set ExpiringAt

                    // Add the new FitTestingForm record
                    _context.Add(fitTestingForm);
                    await _context.SaveChangesAsync();

                    // Save the initial state to FitTestingFormHistory
                    var history = new FitTestingFormHistory
                    {
                        FitTestingFormId = fitTestingForm.Id,
                        Fit_Test_Solution = fitTestingForm.Fit_Test_Solution,
                        Sensitivity_Test = fitTestingForm.Sensitivity_Test,
                        Respiratory_Type = fitTestingForm.Respiratory_Type,
                        Model = fitTestingForm.Model,
                        Size = fitTestingForm.Size,
                        Normal_Breathing = fitTestingForm.Normal_Breathing,
                        Deep_Breathing = fitTestingForm.Deep_Breathing,
                        Turn_head_side_to_side = fitTestingForm.Turn_head_side_to_side,
                        Move_head_up_and_down = fitTestingForm.Move_head_up_and_down,
                        Reading = fitTestingForm.Reading,
                        Bending_Jogging = fitTestingForm.Bending_Jogging,
                        Normal_Breathing_2 = fitTestingForm.Normal_Breathing_2,
                        Test_Results = fitTestingForm.Test_Results,
                        SubmittedAt = fitTestingForm.SubmittedAt
                    };

                    _context.FitTestingFormHistory.Add(history);
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

            var pdfService = new FitTestingFormPdfService(_context);
            var pdfBytes = pdfService.GeneratePdf(form);

            return File(pdfBytes, "application/pdf"); // This ensures the browser previews it properly
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitFitTest(int id, FitTestingForm updatedForm)
        {
            var fitTest = _context.FitTestingForm.FirstOrDefault(f => f.Id == id);
            if (fitTest != null && fitTest.SubmissionCount < fitTest.MaxRetakes)
            {
                // Update the main form with the new data FIRST
                fitTest.Fit_Test_Solution = updatedForm.Fit_Test_Solution;
                fitTest.Sensitivity_Test = updatedForm.Sensitivity_Test;
                fitTest.Respiratory_Type = updatedForm.Respiratory_Type;
                fitTest.Model = updatedForm.Model;
                fitTest.Size = updatedForm.Size;
                fitTest.Normal_Breathing = updatedForm.Normal_Breathing;
                fitTest.Deep_Breathing = updatedForm.Deep_Breathing;
                fitTest.Turn_head_side_to_side = updatedForm.Turn_head_side_to_side;
                fitTest.Move_head_up_and_down = updatedForm.Move_head_up_and_down;
                fitTest.Reading = updatedForm.Reading;
                fitTest.Bending_Jogging = updatedForm.Bending_Jogging;
                fitTest.Normal_Breathing_2 = updatedForm.Normal_Breathing_2;

                // Update the submission count and submission date
                fitTest.SubmissionCount++;
                fitTest.SubmittedAt = DateTime.Now; // Update the submission date for the main form

                // Save the updated FitTestingForm to the database
                _context.SaveChanges(); // Save the updated main form

                // NOW, save the current state to FitTestingFormHistory
                var history = new FitTestingFormHistory
                {
                    FitTestingFormId = fitTest.Id,
                    Fit_Test_Solution = fitTest.Fit_Test_Solution, // Use the updated data
                    Sensitivity_Test = fitTest.Sensitivity_Test,
                    Respiratory_Type = fitTest.Respiratory_Type,
                    Model = fitTest.Model,
                    Size = fitTest.Size,
                    Normal_Breathing = fitTest.Normal_Breathing,
                    Deep_Breathing = fitTest.Deep_Breathing,
                    Turn_head_side_to_side = fitTest.Turn_head_side_to_side,
                    Move_head_up_and_down = fitTest.Move_head_up_and_down,
                    Reading = fitTest.Reading,
                    Bending_Jogging = fitTest.Bending_Jogging,
                    Normal_Breathing_2 = fitTest.Normal_Breathing_2,
                    Test_Results = fitTest.Test_Results,
                    SubmittedAt = fitTest.SubmittedAt // Use the updated submission date
                };

                // Add the history entry to the database
                _context.FitTestingFormHistory.Add(history);
                _context.SaveChanges(); // Save history entry
            }

            return RedirectToAction("Details", new { id });
        }


    }
}
