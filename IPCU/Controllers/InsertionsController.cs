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
    public class InsertionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsertionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Insertions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Insertion.ToListAsync());
        }

        // GET: Insertions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insertion = await _context.Insertion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insertion == null)
            {
                return NotFound();
            }

            return View(insertion);
        }

        // GET: Insertions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Insertions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientLastName,PatientFirstName,PatientMiddleName,Age,Sex,Birthday,PatientDiagnosis,HospitalNumber,ReasonforInsertion,ProcedureOperator,NumberofLumens,ProcedureLocation,CatheterType,Classification,Optimal,ExplainWhyAlternate,Left,Right,AnatomyIs,ChestWall,COPD,Emergency,Anesthesiologist,Coagulopathy,Dialysis,OperatorTraining,ObtainInformedConsent,ObtainInformedConsentReminder,ConfirmHandHygiene,ConfirmHandHygieneReminder,UseFullBarrier,UseFullBarrierReminder,PerformSkin,PerformSkinReminder,AllowSite,AllowSiteReminder,UseSterile,UseSterileReminder,Maintain,MaintainReminder,Monitor,MonitorReminder,CleanBlood,CleanBloodReminder,ProcedureNotes,Observer,Operator")] Insertion insertion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insertion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insertion);
        }

        // GET: Insertions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insertion = await _context.Insertion.FindAsync(id);
            if (insertion == null)
            {
                return NotFound();
            }
            return View(insertion);
        }

        // POST: Insertions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatientLastName,PatientFirstName,PatientMiddleName,Age,Sex,Birthday,PatientDiagnosis,HospitalNumber,ReasonforInsertion,ProcedureOperator,NumberofLumens,ProcedureLocation,CatheterType,Classification,Optimal,ExplainWhyAlternate,Left,Right,AnatomyIs,ChestWall,COPD,Emergency,Anesthesiologist,Coagulopathy,Dialysis,OperatorTraining,ObtainInformedConsent,ObtainInformedConsentReminder,ConfirmHandHygiene,ConfirmHandHygieneReminder,UseFullBarrier,UseFullBarrierReminder,PerformSkin,PerformSkinReminder,AllowSite,AllowSiteReminder,UseSterile,UseSterileReminder,Maintain,MaintainReminder,Monitor,MonitorReminder,CleanBlood,CleanBloodReminder,ProcedureNotes,Observer,Operator")] Insertion insertion)
        {
            if (id != insertion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insertion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsertionExists(insertion.Id))
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
            return View(insertion);
        }

        // GET: Insertions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var insertion = await _context.Insertion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (insertion == null)
            {
                return NotFound();
            }

            return View(insertion);
        }

        // POST: Insertions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var insertion = await _context.Insertion.FindAsync(id);
            if (insertion != null)
            {
                _context.Insertion.Remove(insertion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsertionExists(int id)
        {
            return _context.Insertion.Any(e => e.Id == id);
        }
    }
}
