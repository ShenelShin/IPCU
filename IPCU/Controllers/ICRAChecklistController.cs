using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPCU.Data;
using IPCU.Models;

namespace IPCU.Controllers
{
    [Authorize(Roles = "Admin,ICN")]
    public class ICRAChecklistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ICRAChecklistController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ICRAChecklist
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icra = await _context.ICRA.FindAsync(id);
            if (icra == null)
            {
                return NotFound();
            }

            // Get all checklists for this ICRA
            var checklists = await _context.TCSkillsChecklist
                .Where(c => c.ICRAId == id)
                .ToListAsync();

            ViewBag.ICRA = icra;
            ViewBag.ICRAId = id;
            ViewBag.ChecklistCount = checklists.Count;
            ViewBag.HasPostConstruction = await _context.PostConstruction.AnyAsync(p => p.ICRAId == id);

            return View(checklists);
        }

        // GET: ICRAChecklist/Create/5
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icra = await _context.ICRA.FindAsync(id);
            if (icra == null)
            {
                return NotFound();
            }

            // Check if ICRA is completed (has all required signatures)
            bool isCompleted = false;
            if (icra.PreventiveMeasures.Contains("3") ||
                icra.PreventiveMeasures.Contains("4") ||
                icra.PreventiveMeasures.Contains("5") ||
                icra.PreventiveMeasures.ToLower().Contains("class iii") ||
                icra.PreventiveMeasures.ToLower().Contains("class iv") ||
                icra.PreventiveMeasures.ToLower().Contains("class v"))
            {
                // High risk - needs all signatures
                isCompleted = !string.IsNullOrEmpty(icra.EngineeringSign) &&
                              !string.IsNullOrEmpty(icra.ICPSign) &&
                              !string.IsNullOrEmpty(icra.UnitAreaRep);
            }
            else
            {
                // Low risk - needs ICP sign
                isCompleted = !string.IsNullOrEmpty(icra.ICPSign);
            }

            if (!isCompleted)
            {
                TempData["ErrorMessage"] = "Checklists can only be created for completed ICRAs.";
                return RedirectToAction("AdminICN", "ICRADashboard");
            }

            // Create a new checklist with prefilled ICRA information
            var checklist = new TCSkillsChecklist
            {
                ICRAId = icra.Id,
                Area = icra.SpecificSiteOfActivity,
                Date = DateTime.Now
            };

            ViewBag.ICRA = icra;
            return View(checklist);
        }

        // POST: ICRAChecklist/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ICRAId,Area,ObserverName,Date,IsEquipmentAndCartPrepared,IsCleaningSolutionPrepared,IsProperAttireAndPPEWorn,IsHandHygieneAndGlovesDone,IsSignageChecked,IsSpillSoakedWithSolution,IsWallsCleaned,IsDoorFrameWiped,IsWindowSillAndWindowCleaned,IsHighTouchAreasWiped,IsVerticalSurfacesWiped,IsLooseDebrisPickedUp,IsRoomFloorMopped,IsUsedClothsDisposed,IsWasteContainersEmptied,IsInfectiousWasteRemoved,IsMirrorCleaned,IsSinkAreaCleaned,IsFaucetAndHandlesCleaned,IsToiletAndFlushHandlesCleaned,IsOtherBathroomSurfacesCleaned,IsBathroomFloorScrubbed,IsColorCodedWasteEmptied,IsPPERemoved,IsHandHygieneAfterPPE,IsGlovesRemovedAndHandHygieneDone,PreCleaningItems,PostCleaningItems,RecommendationsOrActions,UnitAreaStaffSignature,DateOfObservation")] TCSkillsChecklist tcSkillsChecklist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tcSkillsChecklist);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Checklist created successfully";
                return RedirectToAction(nameof(Index), new { id = tcSkillsChecklist.ICRAId });
            }

            // If model state is invalid, reload the ICRA info
            var icra = await _context.ICRA.FindAsync(tcSkillsChecklist.ICRAId);
            ViewBag.ICRA = icra;
            return View(tcSkillsChecklist);
        }

        // GET: ICRAChecklist/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklist = await _context.TCSkillsChecklist
                .Include(c => c.ICRA)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (checklist == null)
            {
                return NotFound();
            }

            return View(checklist);
        }

        // GET: ICRAChecklist/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklist = await _context.TCSkillsChecklist
                .Include(c => c.ICRA)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (checklist == null)
            {
                return NotFound();
            }

            ViewBag.ICRA = checklist.ICRA;
            return View(checklist);
        }

        // POST: ICRAChecklist/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ICRAId,Area,ObserverName,Date,IsEquipmentAndCartPrepared,IsCleaningSolutionPrepared,IsProperAttireAndPPEWorn,IsHandHygieneAndGlovesDone,IsSignageChecked,IsSpillSoakedWithSolution,IsWallsCleaned,IsDoorFrameWiped,IsWindowSillAndWindowCleaned,IsHighTouchAreasWiped,IsVerticalSurfacesWiped,IsLooseDebrisPickedUp,IsRoomFloorMopped,IsUsedClothsDisposed,IsWasteContainersEmptied,IsInfectiousWasteRemoved,IsMirrorCleaned,IsSinkAreaCleaned,IsFaucetAndHandlesCleaned,IsToiletAndFlushHandlesCleaned,IsOtherBathroomSurfacesCleaned,IsBathroomFloorScrubbed,IsColorCodedWasteEmptied,IsPPERemoved,IsHandHygieneAfterPPE,IsGlovesRemovedAndHandHygieneDone,PreCleaningItems,PostCleaningItems,RecommendationsOrActions,UnitAreaStaffSignature,DateOfObservation")] TCSkillsChecklist tcSkillsChecklist)
        {
            if (id != tcSkillsChecklist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tcSkillsChecklist);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Checklist updated successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TCSkillsChecklistExists(tcSkillsChecklist.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = tcSkillsChecklist.ICRAId });
            }

            var icra = await _context.ICRA.FindAsync(tcSkillsChecklist.ICRAId);
            ViewBag.ICRA = icra;
            return View(tcSkillsChecklist);
        }

        // GET: ICRAChecklist/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var checklist = await _context.TCSkillsChecklist
                .Include(c => c.ICRA)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (checklist == null)
            {
                return NotFound();
            }

            return View(checklist);
        }

        // POST: ICRAChecklist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var checklist = await _context.TCSkillsChecklist.FindAsync(id);
            if (checklist == null)
            {
                return NotFound();
            }

            int icraId = checklist.ICRAId;
            _context.TCSkillsChecklist.Remove(checklist);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Checklist deleted successfully";
            return RedirectToAction(nameof(Index), new { id = icraId });
        }

        private bool TCSkillsChecklistExists(int id)
        {
            return _context.TCSkillsChecklist.Any(e => e.Id == id);
        }
    }
}