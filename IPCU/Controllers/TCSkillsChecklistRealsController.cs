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
    public class TCSkillsChecklistRealsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TCSkillsChecklistRealsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TCSkillsChecklistReals
        public async Task<IActionResult> Index()
        {
            return View(await _context.TCSkillsChecklistReal.ToListAsync());
        }

        // GET: TCSkillsChecklistReals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCSkillsChecklistReal = await _context.TCSkillsChecklistReal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tCSkillsChecklistReal == null)
            {
                return NotFound();
            }

            return View(tCSkillsChecklistReal);
        }

        // GET: TCSkillsChecklistReals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TCSkillsChecklistReals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Area,ObserverName,Date,IsEquipmentAndCartPrepared,IsCleaningSolutionPrepared,IsProperAttireAndPPEWorn,IsHandHygieneAndGlovesDone,IsSignageChecked,IsSpillSoakedWithSolution,IsWallsCleaned,IsDoorFrameWiped,IsWindowSillAndWindowCleaned,IsHighTouchAreasWiped,IsVerticalSurfacesWiped,IsLooseDebrisPickedUp,IsRoomFloorMopped,IsUsedClothsDisposed,IsWasteContainersEmptied,IsInfectiousWasteRemoved,IsMirrorCleaned,IsSinkAreaCleaned,IsFaucetAndHandlesCleaned,IsToiletAndFlushHandlesCleaned,IsOtherBathroomSurfacesCleaned,IsBathroomFloorScrubbed,IsColorCodedWasteEmptied,IsPPERemoved,IsHandHygieneAfterPPE,IsGlovesRemovedAndHandHygieneDone,PreCleaningItems,PostCleaningItems,RecommendationsOrActions,UnitAreaStaffSignature,DateOfObservation")] TCSkillsChecklistReal tCSkillsChecklistReal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tCSkillsChecklistReal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tCSkillsChecklistReal);
        }

        // GET: TCSkillsChecklistReals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCSkillsChecklistReal = await _context.TCSkillsChecklistReal.FindAsync(id);
            if (tCSkillsChecklistReal == null)
            {
                return NotFound();
            }
            return View(tCSkillsChecklistReal);
        }

        // POST: TCSkillsChecklistReals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Area,ObserverName,Date,IsEquipmentAndCartPrepared,IsCleaningSolutionPrepared,IsProperAttireAndPPEWorn,IsHandHygieneAndGlovesDone,IsSignageChecked,IsSpillSoakedWithSolution,IsWallsCleaned,IsDoorFrameWiped,IsWindowSillAndWindowCleaned,IsHighTouchAreasWiped,IsVerticalSurfacesWiped,IsLooseDebrisPickedUp,IsRoomFloorMopped,IsUsedClothsDisposed,IsWasteContainersEmptied,IsInfectiousWasteRemoved,IsMirrorCleaned,IsSinkAreaCleaned,IsFaucetAndHandlesCleaned,IsToiletAndFlushHandlesCleaned,IsOtherBathroomSurfacesCleaned,IsBathroomFloorScrubbed,IsColorCodedWasteEmptied,IsPPERemoved,IsHandHygieneAfterPPE,IsGlovesRemovedAndHandHygieneDone,PreCleaningItems,PostCleaningItems,RecommendationsOrActions,UnitAreaStaffSignature,DateOfObservation")] TCSkillsChecklistReal tCSkillsChecklistReal)
        {
            if (id != tCSkillsChecklistReal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tCSkillsChecklistReal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TCSkillsChecklistRealExists(tCSkillsChecklistReal.Id))
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
            return View(tCSkillsChecklistReal);
        }

        // GET: TCSkillsChecklistReals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCSkillsChecklistReal = await _context.TCSkillsChecklistReal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tCSkillsChecklistReal == null)
            {
                return NotFound();
            }

            return View(tCSkillsChecklistReal);
        }

        // POST: TCSkillsChecklistReals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tCSkillsChecklistReal = await _context.TCSkillsChecklistReal.FindAsync(id);
            if (tCSkillsChecklistReal != null)
            {
                _context.TCSkillsChecklistReal.Remove(tCSkillsChecklistReal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TCSkillsChecklistRealExists(int id)
        {
            return _context.TCSkillsChecklistReal.Any(e => e.Id == id);
        }
    }
}
