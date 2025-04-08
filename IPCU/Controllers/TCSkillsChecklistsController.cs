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
        public async Task<IActionResult> Index()
        {
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: TCSkillsChecklists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Area,ObserverName,Date,IsEquipmentAndCartPrepared,IsCleaningSolutionPrepared,IsProperAttireAndPPEWorn,IsHandHygieneAndGlovesDone,IsSignageChecked,IsSpillSoakedWithSolution,IsWallsCleaned,IsDoorFrameWiped,IsWindowSillAndWindowCleaned,IsHighTouchAreasWiped,IsVerticalSurfacesWiped,IsLooseDebrisPickedUp,IsRoomFloorMopped,IsUsedClothsDisposed,IsWasteContainersEmptied,IsInfectiousWasteRemoved,IsMirrorCleaned,IsSinkAreaCleaned,IsFaucetAndHandlesCleaned,IsToiletAndFlushHandlesCleaned,IsOtherBathroomSurfacesCleaned,IsBathroomFloorScrubbed,IsColorCodedWasteEmptied,IsPPERemoved,IsHandHygieneAfterPPE,IsGlovesRemovedAndHandHygieneDone,PreCleaningItems,PostCleaningItems,RecommendationsOrActions,UnitAreaStaffSignature,DateOfObservation")] TCSkillsChecklist tCSkillsChecklist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tCSkillsChecklist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
