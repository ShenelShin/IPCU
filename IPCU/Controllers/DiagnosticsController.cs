using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPCU.Controllers
{
    public class DiagnosticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiagnosticsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Diagnostics
        public async Task<IActionResult> Index(string hospNum)
        {
            if (string.IsNullOrEmpty(hospNum))
            {
                return RedirectToAction("Index", "Home");
            }

            var patient = await _context.PatientMasters.FirstOrDefaultAsync(p => p.HospNum == hospNum);
            if (patient == null)
            {
                return NotFound();
            }

            var diagnostics = await _context.Diagnostics
                .Include(d => d.Treatments)
                .ThenInclude(t => t.Antibiotic)
                .Where(d => d.HospNum == hospNum)
                .ToListAsync();

            ViewBag.PatientName = $"{patient.FirstName} {patient.LastName}";
            ViewBag.HospNum = hospNum;

            return View(diagnostics);
        }

        public IActionResult Create(string hospNum)
        {
            if (string.IsNullOrEmpty(hospNum))
            {
                return RedirectToAction("Index", "Home");
            }

            var patient = _context.PatientMasters.FirstOrDefault(p => p.HospNum == hospNum);
            if (patient == null)
            {
                return NotFound();
            }

            ViewBag.PatientName = $"{patient.FirstName} {patient.LastName}";
            ViewBag.HospNum = hospNum;

            return View(new Diagnostics
            {
                HospNum = hospNum,
                DateCollection = DateTime.Now,
                Treatments = new List<DiagnosticsTreatment>()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DateCollection,SourceSite,IsolateFindingsResult,HospNum")] Diagnostics diagnostics)
        {
            // Remove any potential existing errors about Treatments
            if (ModelState.ContainsKey("Treatments"))
            {
                ModelState.Remove("Treatments");
            }

            // Important - Remove Patient validation error if it exists
            if (ModelState.ContainsKey("Patient"))
            {
                ModelState.Remove("Patient");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Initialize the treatments collection if null
                    if (diagnostics.Treatments == null)
                    {
                        diagnostics.Treatments = new List<DiagnosticsTreatment>();
                    }

                    // Find and attach the patient explicitly (optional but can help)
                    var patient = await _context.PatientMasters.FirstOrDefaultAsync(p => p.HospNum == diagnostics.HospNum);
                    if (patient != null)
                    {
                        // If your model includes the Patient navigation property
                        diagnostics.Patient = patient;
                    }

                    _context.Add(diagnostics);
                    Console.WriteLine("About to save changes...");
                    var result = await _context.SaveChangesAsync();
                    Console.WriteLine($"SaveChanges result: {result}");

                    // After successful save, redirect to add antibiotics
                    return RedirectToAction(nameof(AddAntibiotics), new { id = diagnostics.DiagId });
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception type: {ex.GetType().Name}");
                    Console.WriteLine($"Exception message: {ex.Message}");
                    Console.WriteLine($"Stack trace: {ex.StackTrace}");

                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                        ModelState.AddModelError("", $"Inner exception: {ex.InnerException.Message}");
                    }

                    ModelState.AddModelError("", $"Unable to save changes: {ex.Message}");
                }
            }
            else
            {
                foreach (var state in ModelState)
                {
                    if (state.Value.Errors.Count > 0)
                    {
                        Console.WriteLine($"Error in {state.Key}: {state.Value.Errors[0].ErrorMessage}");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PatientName = _context.PatientMasters
                .Where(p => p.HospNum == diagnostics.HospNum)
                .Select(p => $"{p.FirstName} {p.LastName}")
                .FirstOrDefault();
            ViewBag.HospNum = diagnostics.HospNum;

            return View(diagnostics);
        }

        // GET: Diagnostics/AddAntibiotics/5
        public async Task<IActionResult> AddAntibiotics(int id)
        {
            var diagnostics = await _context.Diagnostics
                .Include(d => d.Treatments)
                .ThenInclude(t => t.Antibiotic)
                .FirstOrDefaultAsync(m => m.DiagId == id);

            if (diagnostics == null)
            {
                return NotFound();
            }

            // Get selected antibiotics IDs
            var selectedAntibiotics = diagnostics.Treatments?.Select(t => t.AntibioticId).ToArray() ?? new int[0];
            ViewBag.SelectedAntibiotics = selectedAntibiotics;

            // Get patient name for the view
            ViewBag.PatientName = _context.PatientMasters
                .Where(p => p.HospNum == diagnostics.HospNum)
                .Select(p => $"{p.FirstName} {p.LastName}")
                .FirstOrDefault();

            return View(diagnostics);
        }

        // POST: Diagnostics/AddAntibiotics
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAntibiotics(int diagId, string hospNum, string newAntibiotic, int[] selectedAntibiotics)
        {
            var diagnostics = await _context.Diagnostics.FindAsync(diagId);
            if (diagnostics == null)
            {
                return NotFound();
            }

            try
            {
                // Remove all existing treatments for this diagnostic
                var existingTreatments = await _context.DiagnosticsTreatments
                    .Where(t => t.DiagId == diagId)
                    .ToListAsync();

                _context.DiagnosticsTreatments.RemoveRange(existingTreatments);
                await _context.SaveChangesAsync();

                // Process new antibiotic (if any)
                if (!string.IsNullOrEmpty(newAntibiotic))
                {
                    var sanitizedName = newAntibiotic.Trim();

                    // Check if it already exists
                    var existingAntibiotic = await _context.Antibiotics
                        .FirstOrDefaultAsync(a => a.Name.ToLower() == sanitizedName.ToLower());

                    int antibioticId;

                    if (existingAntibiotic == null)
                    {
                        // Create new antibiotic
                        var antibiotic = new Antibiotic
                        {
                            Name = sanitizedName,
                            DateAdded = DateTime.Now
                        };
                        _context.Antibiotics.Add(antibiotic);
                        await _context.SaveChangesAsync();

                        antibioticId = antibiotic.AntibioticId;
                    }
                    else
                    {
                        antibioticId = existingAntibiotic.AntibioticId;
                    }

                    // Add this antibiotic as treatment if not already in selectedAntibiotics
                    if (selectedAntibiotics == null || !selectedAntibiotics.Contains(antibioticId))
                    {
                        var treatment = new DiagnosticsTreatment
                        {
                            DiagId = diagId,
                            AntibioticId = antibioticId
                        };
                        _context.DiagnosticsTreatments.Add(treatment);
                    }
                }

                // Process selected antibiotics
                if (selectedAntibiotics != null && selectedAntibiotics.Length > 0)
                {
                    foreach (var antibioticId in selectedAntibiotics)
                    {
                        // Skip any invalid IDs
                        if (antibioticId <= 0)
                            continue;

                        // Check if this antibiotic exists
                        var antibioticExists = await _context.Antibiotics.AnyAsync(a => a.AntibioticId == antibioticId);
                        if (!antibioticExists)
                            continue;

                        var treatment = new DiagnosticsTreatment
                        {
                            DiagId = diagId,
                            AntibioticId = antibioticId
                        };
                        _context.DiagnosticsTreatments.Add(treatment);
                    }
                }

                // Final save for treatments
                await _context.SaveChangesAsync();

                // Set a success message in TempData
                TempData["SuccessMessage"] = "Antibiotics saved successfully";

                return RedirectToAction(nameof(Index), new { hospNum });
            }
            catch (Exception ex)
            {
                // Log the exception message for debugging
                Console.WriteLine($"Exception type: {ex.GetType().Name}");
                Console.WriteLine($"Exception message: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");

                ModelState.AddModelError("", $"Unable to save changes: {ex.Message}");

                // If inner exception exists, also log it
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                    ModelState.AddModelError("", $"Inner exception: {ex.InnerException.Message}");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.SelectedAntibiotics = selectedAntibiotics ?? new int[0];
            ViewBag.PatientName = _context.PatientMasters
                .Where(p => p.HospNum == hospNum)
                .Select(p => $"{p.FirstName} {p.LastName}")
                .FirstOrDefault();

            // Reload the diagnostic and return to view
            diagnostics = await _context.Diagnostics
                .Include(d => d.Treatments)
                .ThenInclude(t => t.Antibiotic)
                .FirstOrDefaultAsync(m => m.DiagId == diagId);

            return View(diagnostics);
        }

        // GET: Diagnostics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostics = await _context.Diagnostics
                .Include(d => d.Treatments)
                .ThenInclude(t => t.Antibiotic)
                .FirstOrDefaultAsync(m => m.DiagId == id);

            if (diagnostics == null)
            {
                return NotFound();
            }

            ViewBag.SelectedAntibiotics = diagnostics.Treatments.Select(t => t.AntibioticId).ToArray();
            ViewBag.PatientName = _context.PatientMasters
                .Where(p => p.HospNum == diagnostics.HospNum)
                .Select(p => $"{p.FirstName} {p.LastName}")
                .FirstOrDefault();
            ViewBag.HospNum = diagnostics.HospNum;

            return View(diagnostics);
        }

        // POST: Diagnostics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiagId,DateCollection,SourceSite,IsolateFindingsResult,HospNum")] Diagnostics diagnostics)
        {
            if (id != diagnostics.DiagId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update the diagnostic
                    _context.Update(diagnostics);
                    await _context.SaveChangesAsync();

                    // Redirect to add antibiotics for editing
                    return RedirectToAction(nameof(AddAntibiotics), new { id = diagnostics.DiagId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosticsExists(diagnostics.DiagId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception message for debugging
                    Console.WriteLine($"Exception type: {ex.GetType().Name}");
                    Console.WriteLine($"Exception message: {ex.Message}");
                    Console.WriteLine($"Stack trace: {ex.StackTrace}");

                    ModelState.AddModelError("", $"Unable to save changes: {ex.Message}");

                    // If inner exception exists, also log it
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                        ModelState.AddModelError("", $"Inner exception: {ex.InnerException.Message}");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PatientName = _context.PatientMasters
                .Where(p => p.HospNum == diagnostics.HospNum)
                .Select(p => $"{p.FirstName} {p.LastName}")
                .FirstOrDefault();
            ViewBag.HospNum = diagnostics.HospNum;

            return View(diagnostics);
        }

        // GET: Diagnostics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostics = await _context.Diagnostics
                .Include(d => d.Patient)
                .Include(d => d.Treatments)
                .ThenInclude(t => t.Antibiotic)
                .FirstOrDefaultAsync(m => m.DiagId == id);

            if (diagnostics == null)
            {
                return NotFound();
            }

            return View(diagnostics);
        }

        // POST: Diagnostics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var diagnostics = await _context.Diagnostics.FindAsync(id);
                string hospNum = null;

                if (diagnostics != null)
                {
                    hospNum = diagnostics.HospNum;

                    // Remove associated treatments first
                    var treatments = await _context.DiagnosticsTreatments
                        .Where(t => t.DiagId == id)
                        .ToListAsync();

                    _context.DiagnosticsTreatments.RemoveRange(treatments);

                    // Then remove the diagnostic
                    _context.Diagnostics.Remove(diagnostics);
                    await _context.SaveChangesAsync();

                    // Add success message
                    TempData["SuccessMessage"] = "Diagnostic record deleted successfully";
                }

                return RedirectToAction(nameof(Index), new { hospNum });
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error deleting diagnostic: {ex.Message}");
                TempData["ErrorMessage"] = "An error occurred while deleting the diagnostic record";

                // Get the hospital number to redirect back to index
                var diagnostics = await _context.Diagnostics.FindAsync(id);
                string hospNum = diagnostics?.HospNum;

                return RedirectToAction(nameof(Index), new { hospNum });
            }
        }

        private bool DiagnosticsExists(int id)
        {
            return _context.Diagnostics.Any(e => e.DiagId == id);
        }

        // API endpoint for autocomplete
        [HttpGet]
        public async Task<IActionResult> GetAntibiotics(string term)
        {
            var termLower = term?.ToLower() ?? "";
            var antibiotics = await _context.Antibiotics
                .Where(a => string.IsNullOrEmpty(termLower) || a.Name.ToLower().Contains(termLower))
                .OrderBy(a => a.Name)
                .Select(a => new { id = a.AntibioticId, text = a.Name })
                .ToListAsync();

            return Json(antibiotics);
        }

        // API endpoint for adding new antibiotics via AJAX
        [HttpPost]
        public async Task<IActionResult> AddAntibiotic(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Antibiotic name is required");
            }

            var sanitizedName = name.Trim();

            // Check if it already exists
            var existingAntibiotic = await _context.Antibiotics
                .FirstOrDefaultAsync(a => a.Name.ToLower() == sanitizedName.ToLower());

            if (existingAntibiotic != null)
            {
                return Json(new { id = existingAntibiotic.AntibioticId, name = existingAntibiotic.Name, exists = true });
            }

            // Create new antibiotic
            var antibiotic = new Antibiotic
            {
                Name = sanitizedName,
                DateAdded = DateTime.Now
            };

            _context.Antibiotics.Add(antibiotic);
            await _context.SaveChangesAsync();

            return Json(new { id = antibiotic.AntibioticId, name = antibiotic.Name, exists = false });
        }
    }
}