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
    public class DailyCentralLineMaintenanceChecklistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DailyCentralLineMaintenanceChecklistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DailyCentralLineMaintenanceChecklists
        public async Task<IActionResult> Index()
        {
            return View(await _context.DailyCentralLineMaintenanceChecklists.ToListAsync());
        }

        // GET: DailyCentralLineMaintenanceChecklists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyCentralLineMaintenanceChecklist = await _context.DailyCentralLineMaintenanceChecklists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dailyCentralLineMaintenanceChecklist == null)
            {
                return NotFound();
            }

            return View(dailyCentralLineMaintenanceChecklist);
        }

        // GET: DailyCentralLineMaintenanceChecklists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DailyCentralLineMaintenanceChecklists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AreaOrUnit,DateAndTimeOfMonitoring,AssessedBy,Patient,Bed,IntitialPlacement,Implanted,Injection,Dateadministration,Necessityassessed,Injectionsites,Capschanged,Insertionsite,Dressingintact,Dressingchanged,Remarks,NumCompliant,TotalObserved,ComplianceRate")] DailyCentralLineMaintenanceChecklist dailyCentralLineMaintenanceChecklist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dailyCentralLineMaintenanceChecklist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dailyCentralLineMaintenanceChecklist);
        }

        // GET: DailyCentralLineMaintenanceChecklists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyCentralLineMaintenanceChecklist = await _context.DailyCentralLineMaintenanceChecklists.FindAsync(id);
            if (dailyCentralLineMaintenanceChecklist == null)
            {
                return NotFound();
            }
            return View(dailyCentralLineMaintenanceChecklist);
        }

        // POST: DailyCentralLineMaintenanceChecklists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AreaOrUnit,DateAndTimeOfMonitoring,AssessedBy,Patient,Bed,IntitialPlacement,Implanted,Injection,Dateadministration,Necessityassessed,Injectionsites,Capschanged,Insertionsite,Dressingintact,Dressingchanged,Remarks,NumCompliant,TotalObserved,ComplianceRate")] DailyCentralLineMaintenanceChecklist dailyCentralLineMaintenanceChecklist)
        {
            if (id != dailyCentralLineMaintenanceChecklist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dailyCentralLineMaintenanceChecklist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DailyCentralLineMaintenanceChecklistExists(dailyCentralLineMaintenanceChecklist.Id))
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
            return View(dailyCentralLineMaintenanceChecklist);
        }

        // GET: DailyCentralLineMaintenanceChecklists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dailyCentralLineMaintenanceChecklist = await _context.DailyCentralLineMaintenanceChecklists
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dailyCentralLineMaintenanceChecklist == null)
            {
                return NotFound();
            }

            return View(dailyCentralLineMaintenanceChecklist);
        }

        // POST: DailyCentralLineMaintenanceChecklists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dailyCentralLineMaintenanceChecklist = await _context.DailyCentralLineMaintenanceChecklists.FindAsync(id);
            if (dailyCentralLineMaintenanceChecklist != null)
            {
                _context.DailyCentralLineMaintenanceChecklists.Remove(dailyCentralLineMaintenanceChecklist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DailyCentralLineMaintenanceChecklistExists(int id)
        {
            return _context.DailyCentralLineMaintenanceChecklists.Any(e => e.Id == id);
        }
    }
}
