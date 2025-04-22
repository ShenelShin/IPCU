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
    public class TCSkillsChecklistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TCSkillsChecklistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TCSkillsChecklists
        public async Task<IActionResult> Index(int? icraId)
        {
            // If an ICRA ID is provided, filter checklists by that ICRA
            if (icraId.HasValue)
            {
                var icra = await _context.ICRA.FindAsync(icraId.Value);
                if (icra != null)
                {
                    ViewBag.ICRA = icra;
                    return View(await _context.TCSkillsChecklist.Where(c => c.ICRAId == icraId.Value).ToListAsync());
                }
            }

            // Otherwise, return all checklists
            return View(await _context.TCSkillsChecklist.ToListAsync());
        }

        // GET: TCSkillsChecklists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCSkillsChecklist = await _context.TCSkillsChecklist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tCSkillsChecklist == null)
            {
                return NotFound();
            }

            return View(tCSkillsChecklist);
        }

        // GET: TCSkillsChecklists/Create
        public IActionResult Create(int? icraId)
        {
            var checklist = new TCSkillsChecklist();

            if (icraId.HasValue)
            {
                var icra = _context.ICRA.Find(icraId.Value);
                if (icra != null)
                {
                    checklist.ICRAId = icra.Id;
                    ViewBag.ICRANumber = icra.ProjectReferenceNumber; // Add this line
                    checklist.Area = icra.SpecificSiteOfActivity;
                    checklist.Date = DateTime.Now;
                    checklist.DateOfObservation = DateTime.Now;
                }
            }

            return View(checklist);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ICRAId,Area,ObserverName,Date,IsEquipmentAndCartPrepared,IsCleaningSolutionPrepared,IsProperAttireAndPPEWorn,IsHandHygieneAndGlovesDone,IsSignageChecked,IsSpillSoakedWithSolution,IsWallsCleaned,IsDoorFrameWiped,IsWindowSillAndWindowCleaned,IsHighTouchAreasWiped,IsVerticalSurfacesWiped,IsLooseDebrisPickedUp,IsRoomFloorMopped,IsUsedClothsDisposed,IsWasteContainersEmptied,IsInfectiousWasteRemoved,IsMirrorCleaned,IsSinkAreaCleaned,IsFaucetAndHandlesCleaned,IsToiletAndFlushHandlesCleaned,IsOtherBathroomSurfacesCleaned,IsBathroomFloorScrubbed,IsColorCodedWasteEmptied,IsPPERemoved,IsHandHygieneAfterPPE,IsGlovesRemovedAndHandHygieneDone,PreCleaningItems,PostCleaningItems,RecommendationsOrActions,UnitAreaStaffSignature,DateOfObservation")] TCSkillsChecklist tCSkillsChecklist)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Find the ICRA and attach it to the context
                    if (tCSkillsChecklist.ICRAId > 0)
                    {
                        // Set the ICRA to null to avoid validation issues
                        tCSkillsChecklist.ICRA = null;
                    }

                    _context.Add(tCSkillsChecklist);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    // Log the error
                    ModelState.AddModelError("", "Unable to save changes. " + ex.Message);

                    // If there's an inner exception, add it too
                    if (ex.InnerException != null)
                    {
                        ModelState.AddModelError("", "Details: " + ex.InnerException.Message);
                    }
                }
            }
            else
            {
                // Add all validation errors to ModelState for debugging
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { Key = x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage).ToList() })
                    .ToList();

                foreach (var error in errors)
                {
                    ModelState.AddModelError("", $"Field: {error.Key}, Errors: {string.Join(", ", error.Errors)}");
                }
            }

            // If we get here, something went wrong - repopulate ICRA data for the view
            if (tCSkillsChecklist.ICRAId > 0)
            {
                var icra = _context.ICRA.Find(tCSkillsChecklist.ICRAId);
                if (icra != null)
                {
                    ViewBag.ICRANumber = icra.ProjectReferenceNumber;
                }
            }

            return View(tCSkillsChecklist);
        }

        // GET: TCSkillsChecklists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCSkillsChecklist = await _context.TCSkillsChecklist.FindAsync(id);
            if (tCSkillsChecklist == null)
            {
                return NotFound();
            }
            return View(tCSkillsChecklist);
        }

        // POST: TCSkillsChecklists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Area,ObserverName,Date,IsEquipmentAndCartPrepared,IsCleaningSolutionPrepared,IsProperAttireAndPPEWorn,IsHandHygieneAndGlovesDone,IsSignageChecked,IsSpillSoakedWithSolution,IsWallsCleaned,IsDoorFrameWiped,IsWindowSillAndWindowCleaned,IsHighTouchAreasWiped,IsVerticalSurfacesWiped,IsLooseDebrisPickedUp,IsRoomFloorMopped,IsUsedClothsDisposed,IsWasteContainersEmptied,IsInfectiousWasteRemoved,IsMirrorCleaned,IsSinkAreaCleaned,IsFaucetAndHandlesCleaned,IsToiletAndFlushHandlesCleaned,IsOtherBathroomSurfacesCleaned,IsBathroomFloorScrubbed,IsColorCodedWasteEmptied,IsPPERemoved,IsHandHygieneAfterPPE,IsGlovesRemovedAndHandHygieneDone,PreCleaningItems,PostCleaningItems,RecommendationsOrActions,UnitAreaStaffSignature,DateOfObservation")] TCSkillsChecklist tCSkillsChecklist)
        {
            if (id != tCSkillsChecklist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tCSkillsChecklist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TCSkillsChecklistExists(tCSkillsChecklist.Id))
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
            return View(tCSkillsChecklist);
        }

        // GET: TCSkillsChecklists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCSkillsChecklist = await _context.TCSkillsChecklist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tCSkillsChecklist == null)
            {
                return NotFound();
            }

            return View(tCSkillsChecklist);
        }

        // POST: TCSkillsChecklists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tCSkillsChecklist = await _context.TCSkillsChecklist.FindAsync(id);
            if (tCSkillsChecklist != null)
            {
                _context.TCSkillsChecklist.Remove(tCSkillsChecklist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TCSkillsChecklistExists(int id)
        {
            return _context.TCSkillsChecklist.Any(e => e.Id == id);
        }
    }
}
