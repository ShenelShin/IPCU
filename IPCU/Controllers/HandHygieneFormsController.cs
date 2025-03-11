using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IPCU.Data;
using IPCU.Models;
using System.Diagnostics;
using IPCU.Services;

namespace IPCU.Controllers
{
    public class HandHygieneFormsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HandHygieneFormsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HandHygieneForms
        public async Task<IActionResult> Index()
        {
            var forms = await _context.HandHygieneForms
                .Include(f => f.Activities)  // Load related HHActivity records
                .ToListAsync();

            return View(forms);
        }

        // GET: HandHygieneForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var handHygieneForm = await _context.HandHygieneForms
                .Include(h => h.Activities)  // Load related HHActivity records
                .FirstOrDefaultAsync(m => m.HHId == id);

            if (handHygieneForm == null) return NotFound();

            return View(handHygieneForm);
        }

        // GET: HandHygieneForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HandHygieneForms/Create
        // POST: HandHygieneForms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HandHygieneForm handHygieneForm)
        {
            // Debug: Check what's coming in from the form
            Debug.WriteLine($"Received form data: Area={handHygieneForm.Area}, Observer={handHygieneForm.Observer}");

            // Ensure HHId is NOT set manually
            ModelState.Remove("HHId");  // This line prevents validation on HHId

            // Debug: Check ModelState before validation
            if (!ModelState.IsValid)
            {
                Debug.WriteLine("ModelState is invalid. Validation errors:");
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Debug.WriteLine($"- {state.Key}: {error.ErrorMessage}");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.HandHygieneForms.Add(handHygieneForm);
                    await _context.SaveChangesAsync();
                    Debug.WriteLine($"Form saved successfully with ID: {handHygieneForm.HHId}");
                    return RedirectToAction("AddActivities", new { id = handHygieneForm.HHId });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error saving form: {ex.Message}");
                    ModelState.AddModelError("", $"Database error: {ex.Message}");
                    return View(handHygieneForm);
                }
            }

            // If we got here, something failed, redisplay form
            return View(handHygieneForm);
        }






        public IActionResult AddActivities(int id)
        {
            var handHygieneForm = _context.HandHygieneForms.Find(id);
            if (handHygieneForm == null)
            {
                return NotFound();
            }

            ViewBag.HHId = id; // Pass the ID to the view
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddActivities(int HHId, string Activity, string BeforeHandRub, string BeforeHandWash,
                                                      string AfterHandRub, string AfterHandWash, string Gloves)
        {
            // Check if the required field is populated
            if (string.IsNullOrEmpty(Activity))
            {
                ModelState.AddModelError("Activity", "Activity information is required.");
                ViewBag.HHId = HHId;
                return View();
            }

            try
            {
                // Create a new activity object manually from the form fields
                var activity = new HHActivity
                {
                    HHId = HHId,
                    Activity = Activity,
                    BeforeHandRub = BeforeHandRub,
                    BeforeHandWash = BeforeHandWash,
                    AfterHandRub = AfterHandRub,
                    AfterHandWash = AfterHandWash,
                    Gloves = Gloves ?? "False"  // Handle checkbox (if not checked, it won't be in the request)
                };

                // Add the activity to the database
                _context.HHActivities.Add(activity);
                await _context.SaveChangesAsync();

                // Get the form with its current activities to display in the view
                var handHygieneForm = await _context.HandHygieneForms
                    .Include(f => f.Activities)
                    .FirstOrDefaultAsync(f => f.HHId == HHId);

                // Clear the ModelState so the form resets for the next activity
                ModelState.Clear();

                // Set success message
                TempData["SuccessMessage"] = "Activity added successfully. You can add another activity or finish.";

                // Return to the same page with the form ID
                ViewBag.HHId = HHId;
                ViewBag.Activities = handHygieneForm?.Activities;

                return View();
            }
            catch (Exception ex)
            {
                // Log the exception and show error
                ModelState.AddModelError("", $"Error saving activity: {ex.Message}");
                ViewBag.HHId = HHId;
                return View();
            }
        }

        // Add a new action method to finish adding activities
        public async Task<IActionResult> FinishActivities(int id)
        {
            var handHygieneForm = await _context.HandHygieneForms
                .Include(f => f.Activities)
                .FirstOrDefaultAsync(f => f.HHId == id);

            if (handHygieneForm == null) return NotFound();

            // Calculate and update compliance data
            UpdateComplianceData(handHygieneForm);

            // Save changes to database
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = id });
        }

        // Helper method to update compliance data
        private void UpdateComplianceData(HandHygieneForm form)
        {
            // Calculate total opportunities and compliant actions
            form.TotalObservedOpportunities = CalculateTotalOpportunities(form);
            form.TotalCompliantActions = CalculateCompliantActions(form);

            // Calculate compliance rate (handle division by zero)
            if (form.TotalObservedOpportunities > 0)
            {
                // Store as a decimal (not multiplied by 100)
                // This will be displayed as percentage with ToString("P2") in the view
                form.ComplianceRate = (decimal)form.TotalCompliantActions / form.TotalObservedOpportunities;
            }
            else
            {
                form.ComplianceRate = 0;
            }
        }



        // Helper methods for compliance calculation
        private int CalculateTotalOpportunities(HandHygieneForm form)
        {
            int count = 0;
            foreach (var activity in form.Activities)
            {
                // Count all non-empty entries as opportunities
                if (!string.IsNullOrEmpty(activity.BeforeHandRub)) count += CountEntries(activity.BeforeHandRub);
                if (!string.IsNullOrEmpty(activity.BeforeHandWash)) count += CountEntries(activity.BeforeHandWash);
                if (!string.IsNullOrEmpty(activity.AfterHandRub)) count += CountEntries(activity.AfterHandRub);
                if (!string.IsNullOrEmpty(activity.AfterHandWash)) count += CountEntries(activity.AfterHandWash);
            }
            return count;
        }

        private int CalculateCompliantActions(HandHygieneForm form)
        {
            int count = 0;
            foreach (var activity in form.Activities)
            {
                // Count entries marked with ✓ as compliant
                count += CountCompliantEntries(activity.BeforeHandRub);
                count += CountCompliantEntries(activity.BeforeHandWash);
                count += CountCompliantEntries(activity.AfterHandRub);
                count += CountCompliantEntries(activity.AfterHandWash);
            }
            return count;
        }

        private int CountEntries(string data)
        {
            if (string.IsNullOrEmpty(data)) return 0;
            return data.Split(';').Length;
        }

        private int CountCompliantEntries(string data)
        {
            if (string.IsNullOrEmpty(data)) return 0;
            int compliantCount = 0;
            var entries = data.Split(';');
            foreach (var entry in entries)
            {
                if (string.IsNullOrEmpty(entry)) continue;
                var parts = entry.Split(',');
                if (parts.Length == 2 && parts[1] == "✓")
                {
                    compliantCount++;
                }
            }
            return compliantCount;
        }




        // GET: HandHygieneForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var handHygieneForm = await _context.HandHygieneForms
                .Include(f => f.Activities)  // Ensure activities are loaded
                .FirstOrDefaultAsync(f => f.HHId == id);

            if (handHygieneForm == null) return NotFound();

            return View(handHygieneForm);
        }


        // POST: HandHygieneForms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HandHygieneForm handHygieneForm)
        {
            if (id != handHygieneForm.HHId) return NotFound();

            // Log received data for debugging
            System.Diagnostics.Debug.WriteLine($"Received form edit for HHId: {id} with {handHygieneForm.Activities?.Count ?? 0} activities");

            try
            {
                // First retrieve the existing form without activities
                var existingForm = await _context.HandHygieneForms
                    .FirstOrDefaultAsync(f => f.HHId == id);

                if (existingForm == null) return NotFound();

                // Update basic form properties
                _context.Entry(existingForm).CurrentValues.SetValues(handHygieneForm);

                // Handle activities separately
                // First, get existing activities
                var existingActivities = await _context.HHActivities
                    .Where(a => a.HHId == id)
                    .ToListAsync();

                // Process activities from the form
                if (handHygieneForm.Activities != null)
                {
                    var activityIdsToKeep = new List<int>();

                    foreach (var activity in handHygieneForm.Activities)
                    {
                        if (activity.ActId > 0) // Existing activity
                        {
                            var existingActivity = existingActivities.FirstOrDefault(a => a.ActId == activity.ActId);
                            if (existingActivity != null)
                            {
                                // Update existing activity
                                _context.Entry(existingActivity).CurrentValues.SetValues(activity);
                                activityIdsToKeep.Add(activity.ActId);
                            }
                        }
                        else // New activity
                        {
                            activity.HHId = id;
                            activity.Gloves = activity.Gloves ?? "False";
                            _context.HHActivities.Add(activity);
                        }
                    }

                    // Remove activities not in the updated list
                    foreach (var activity in existingActivities)
                    {
                        if (!activityIdsToKeep.Contains(activity.ActId))
                        {
                            _context.HHActivities.Remove(activity);
                        }
                    }
                }
                else
                {
                    // If no activities were submitted, keep existing ones
                    handHygieneForm.Activities = existingActivities;
                }

                // Reload all activities to recalculate compliance
                var updatedForm = await _context.HandHygieneForms
                    .Include(f => f.Activities)
                    .FirstOrDefaultAsync(f => f.HHId == id);

                // Calculate and update compliance data
                UpdateComplianceData(updatedForm);

                // Save all changes
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception
                System.Diagnostics.Debug.WriteLine($"Error saving form: {ex.Message}");
                ModelState.AddModelError("", $"Error saving form: {ex.Message}");
            }

            // If we got this far, something failed - reload activities and redisplay
            handHygieneForm.Activities = await _context.HHActivities
                .Where(a => a.HHId == id)
                .ToListAsync();

            return View(handHygieneForm);
        }



        // GET: HandHygieneForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var handHygieneForm = await _context.HandHygieneForms
                .FirstOrDefaultAsync(m => m.HHId == id);

            if (handHygieneForm == null) return NotFound();

            return View(handHygieneForm);
        }

        // POST: HandHygieneForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var handHygieneForm = await _context.HandHygieneForms.FindAsync(id);
            if (handHygieneForm != null)
            {
                _context.HandHygieneForms.Remove(handHygieneForm);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool HandHygieneFormExists(int id)
        {
            return _context.HandHygieneForms.Any(e => e.HHId == id);
        }

        // GET: HandHygieneForms/GeneratePdf/5
        public async Task<IActionResult> GeneratePdf(int? id)
        {
            if (id == null) return NotFound();

            var handHygieneForm = await _context.HandHygieneForms
                .Include(f => f.Activities)  // Load related HHActivity records
                .FirstOrDefaultAsync(m => m.HHId == id);

            if (handHygieneForm == null) return NotFound();

            // Create the PDF service
            var pdfService = new HandHygienePdfService();

            // Generate the PDF
            var pdfBytes = pdfService.GeneratePdf(handHygieneForm);

            // Return the PDF for inline viewing in the browser instead of downloading
            return File(
                pdfBytes,
                "application/pdf",
                $"HandHygieneForm_{id}.pdf",
                false  // false means "inline" (preview) instead of "attachment" (download)
            );
        }
    }
}
