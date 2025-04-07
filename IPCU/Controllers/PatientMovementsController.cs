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
    public class PatientMovementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientMovementsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PatientMovement
        public async Task<IActionResult> Index(DateTime? date)
        {
            // Default to today if no date specified
            var selectedDate = date ?? DateTime.Today;

            var movements = await _context.PatientMovements
                .Where(m => m.MovementDate == selectedDate.Date)
                .OrderBy(m => m.Area)
                .ToListAsync();

            // Convert to view models for display
            var viewModels = movements.Select(m => new PatientMovementViewModel
            {
                MovementDate = m.MovementDate,
                Area = m.Area,
                AdmissionCount = m.AdmissionCount,
                TransferInCount = m.TransferInCount,
                TotalArrivals = m.AdmissionCount + m.TransferInCount,
                SentHomeCount = m.SentHomeCount,
                MortalityCount = m.MortalityCount,
                TransferOutCount = m.TransferOutCount,
                TotalDischarges = m.SentHomeCount + m.MortalityCount + m.TransferOutCount,
                NetChange = (m.AdmissionCount + m.TransferInCount) - (m.SentHomeCount + m.MortalityCount + m.TransferOutCount),
                Notes = m.Notes
            }).ToList();

            // Prepare date selection
            ViewBag.SelectedDate = selectedDate;

            return View(viewModels);
        }

        // GET: PatientMovement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientMovement = await _context.PatientMovements
                .FirstOrDefaultAsync(m => m.MovementId == id);

            if (patientMovement == null)
            {
                return NotFound();
            }

            // Get associated details if any
            var details = await _context.PatientMovementDetails
                .Include(d => d.Patient)
                .Where(d => d.MovementId == id)
                .ToListAsync();

            ViewBag.Details = details;

            return View(patientMovement);
        }

        // GET: PatientMovement/Create
        public IActionResult Create()
        {
            // Prepopulate areas from existing data or provide defaults
            var areas = _context.PatientMovements
                .Select(m => m.Area)
                .Distinct()
                .ToList();

            // Default to today's date
            var model = new PatientMovement
            {
                MovementDate = DateTime.Today
            };

            return View(model);
        }

        // POST: PatientMovement/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovementDate,Area,AdmissionCount,TransferInCount,SentHomeCount,MortalityCount,TransferOutCount,Notes,CreatedBy,ModifiedBy")] PatientMovement patientMovement)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Check if entry already exists for this date and area
                    var existing = await _context.PatientMovements
                        .FirstOrDefaultAsync(m => m.MovementDate == patientMovement.MovementDate &&
                                                 m.Area == patientMovement.Area);

                    if (existing != null)
                    {
                        ModelState.AddModelError("", "An entry already exists for this date and area.");
                        return View(patientMovement);
                    }

                    // Set audit fields
                    var username = User.Identity?.Name ?? "System";
                    patientMovement.DateCreated = DateTime.Now;
                    patientMovement.CreatedBy = username;
                    patientMovement.ModifiedBy = username; // Also set ModifiedBy to fulfill database requirement

                    _context.Add(patientMovement);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { date = patientMovement.MovementDate });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                ModelState.AddModelError("", "Error saving: " + ex.Message);
            }

            return View(patientMovement);
        }

        // GET: PatientMovement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientMovement = await _context.PatientMovements.FindAsync(id);
            if (patientMovement == null)
            {
                return NotFound();
            }

            ViewBag.Areas = new SelectList(await _context.PatientMovements.Select(m => m.Area).Distinct().ToListAsync());
            return View(patientMovement);
        }

        // POST: PatientMovement/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovementId,MovementDate,Area,AdmissionCount,TransferInCount,SentHomeCount,MortalityCount,TransferOutCount,Notes,DateCreated,CreatedBy")] PatientMovement patientMovement)
        {
            if (id != patientMovement.MovementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update audit fields
                    patientMovement.DateModified = DateTime.Now;
                    patientMovement.ModifiedBy = User.Identity.Name;

                    _context.Update(patientMovement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientMovementExists(patientMovement.MovementId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { date = patientMovement.MovementDate });
            }

            ViewBag.Areas = new SelectList(await _context.PatientMovements.Select(m => m.Area).Distinct().ToListAsync());
            return View(patientMovement);
        }

        // GET: PatientMovement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientMovement = await _context.PatientMovements
                .FirstOrDefaultAsync(m => m.MovementId == id);

            if (patientMovement == null)
            {
                return NotFound();
            }

            return View(patientMovement);
        }

        // POST: PatientMovement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patientMovement = await _context.PatientMovements.FindAsync(id);

            // Also delete any detail records
            var details = await _context.PatientMovementDetails
                .Where(d => d.MovementId == id)
                .ToListAsync();

            _context.PatientMovementDetails.RemoveRange(details);
            _context.PatientMovements.Remove(patientMovement);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { date = patientMovement.MovementDate });
        }

        // GET: PatientMovement/Report
        public async Task<IActionResult> Report(DateTime? startDate, DateTime? endDate, string area)
        {
            // Default to last 7 days if no dates specified
            var end = endDate ?? DateTime.Today;
            var start = startDate ?? end.AddDays(-7);

            var query = _context.PatientMovements
                .Where(m => m.MovementDate >= start && m.MovementDate <= end);

            if (!string.IsNullOrEmpty(area))
            {
                query = query.Where(m => m.Area == area);
            }

            var movements = await query
                .OrderBy(m => m.MovementDate)
                .ThenBy(m => m.Area)
                .ToListAsync();

            // Convert to view models
            var viewModels = movements.Select(m => new PatientMovementViewModel
            {
                MovementDate = m.MovementDate,
                Area = m.Area,
                AdmissionCount = m.AdmissionCount,
                TransferInCount = m.TransferInCount,
                TotalArrivals = m.AdmissionCount + m.TransferInCount,
                SentHomeCount = m.SentHomeCount,
                MortalityCount = m.MortalityCount,
                TransferOutCount = m.TransferOutCount,
                TotalDischarges = m.SentHomeCount + m.MortalityCount + m.TransferOutCount,
                NetChange = (m.AdmissionCount + m.TransferInCount) - (m.SentHomeCount + m.MortalityCount + m.TransferOutCount),
                Notes = m.Notes
            }).ToList();

            // Get distinct areas for filter
            ViewBag.Areas = new SelectList(await _context.PatientMovements.Select(m => m.Area).Distinct().ToListAsync());
            ViewBag.StartDate = start;
            ViewBag.EndDate = end;
            ViewBag.SelectedArea = area;

            return View(viewModels);
        }

        private bool PatientMovementExists(int id)
        {
            return _context.PatientMovements.Any(e => e.MovementId == id);
        }
    }
}