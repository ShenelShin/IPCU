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
    public class VAEMonitoringChecklistsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VAEMonitoringChecklistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VAEMonitoringChecklists
        public async Task<IActionResult> Index()
        {
            return View(await _context.VAEMonitoringChecklist.ToListAsync());
        }

        // GET: VAEMonitoringChecklists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vAEMonitoringChecklist = await _context.VAEMonitoringChecklist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vAEMonitoringChecklist == null)
            {
                return NotFound();
            }

            return View(vAEMonitoringChecklist);
        }

        // GET: VAEMonitoringChecklists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VAEMonitoringChecklists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AreaUnit,AssessedBy,DateandTime,PatientName,Bed,ObservedStaff,HeadofBed,IfTracheostomy,OrcalCare,HandHygiene,AfterCare,SterileWater,EnsuretoUse,Condensateinthe,IntubationKits,CleanandDirty,Remarks,NumberofComplaint,TotalObserved,ComplianceRate,Accomplishedby,ReviewedandApproved")] VAEMonitoringChecklist vAEMonitoringChecklist)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vAEMonitoringChecklist);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vAEMonitoringChecklist);
        }

        // GET: VAEMonitoringChecklists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vAEMonitoringChecklist = await _context.VAEMonitoringChecklist.FindAsync(id);
            if (vAEMonitoringChecklist == null)
            {
                return NotFound();
            }
            return View(vAEMonitoringChecklist);
        }

        // POST: VAEMonitoringChecklists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AreaUnit,AssessedBy,DateandTime,PatientName,Bed,ObservedStaff,HeadofBed,IfTracheostomy,OrcalCare,HandHygiene,AfterCare,SterileWater,EnsuretoUse,Condensateinthe,IntubationKits,CleanandDirty,Remarks,NumberofComplaint,TotalObserved,ComplianceRate,Accomplishedby,ReviewedandApproved")] VAEMonitoringChecklist vAEMonitoringChecklist)
        {
            if (id != vAEMonitoringChecklist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vAEMonitoringChecklist);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VAEMonitoringChecklistExists(vAEMonitoringChecklist.Id))
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
            return View(vAEMonitoringChecklist);
        }

        // GET: VAEMonitoringChecklists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vAEMonitoringChecklist = await _context.VAEMonitoringChecklist
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vAEMonitoringChecklist == null)
            {
                return NotFound();
            }

            return View(vAEMonitoringChecklist);
        }

        // POST: VAEMonitoringChecklists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vAEMonitoringChecklist = await _context.VAEMonitoringChecklist.FindAsync(id);
            if (vAEMonitoringChecklist != null)
            {
                _context.VAEMonitoringChecklist.Remove(vAEMonitoringChecklist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VAEMonitoringChecklistExists(int id)
        {
            return _context.VAEMonitoringChecklist.Any(e => e.Id == id);
        }
    }
}
