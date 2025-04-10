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
    public class PostConstructionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PostConstructionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PostConstructions
        public async Task<IActionResult> Index()
        {
            return View(await _context.PostConstruction.ToListAsync());
        }

        // GET: PostConstructions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postConstruction = await _context.PostConstruction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postConstruction == null)
            {
                return NotFound();
            }

            return View(postConstruction);
        }

        // GET: PostConstructions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PostConstructions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectReferenceNumber,ProjectNameAndDescription,SpecificSiteOfActivity,ProjectStartDate,EstimatedDuration,BeforeHoarding,BeforeHoardingDC,FacilityBased,FacilityBasedDC,FacilityBasedDate,AfterRemoval,AfterRemovalDC,AfterRemovalDate,WhereRequired,WhereRequiredlDC,WhereRequiredDate,AreaIs,AreaIsDC,IntegrityofWalls,IntegrityofWallsDC,SurfaceinPatient,SurfaceinPatientDC,AreaSurfaces,IfPlumbinghasbeenAffected,IfPlumbinghasbeenAffectedDC,IfPlumbinghasbeenAffectedDate,PlumbingifAffected,PlumbingifAffectedDC,CorrectHandWashing,CorrectHandWashingDC,FaucetAerators,FaucetAeratorsDC,CeilingTiles,CeilingTilesDC,HVACSystems,HVACSystemsDC,CorrectRoomPressurization,CorrectRoomPressurizationDC,AllMechanicalSpaces,AllMechanicalSpacesDC,ContractorSign,EngineeringSign,ICPSign,UnitAreaRep,DateCompleted")] PostConstruction postConstruction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postConstruction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(postConstruction);
        }

        // GET: PostConstructions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postConstruction = await _context.PostConstruction.FindAsync(id);
            if (postConstruction == null)
            {
                return NotFound();
            }
            return View(postConstruction);
        }

        // POST: PostConstructions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        // GET: PostConstructions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postConstruction = await _context.PostConstruction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postConstruction == null)
            {
                return NotFound();
            }

            return View(postConstruction);
        }

        // POST: PostConstructions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postConstruction = await _context.PostConstruction.FindAsync(id);
            if (postConstruction != null)
            {
                _context.PostConstruction.Remove(postConstruction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostConstructionExists(int id)
        {
            return _context.PostConstruction.Any(e => e.Id == id);
        }
    }
}
