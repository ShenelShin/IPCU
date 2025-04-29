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
        public async Task<IActionResult> Create()
        {
            // Check if a patient ID was provided via TempData
            if (TempData["PatientId"] != null)
            {
                var patientId = TempData["PatientId"].ToString();

                // Get patient information to pre-populate the form
                var patient = await (from p in _context.Patients
                                     join m in _context.PatientMasters on p.HospNum equals m.HospNum
                                     where p.IdNum == patientId
                                     select new
                                     {
                                         HospNum = p.HospNum,
                                         LastName = m.LastName,
                                         FirstName = m.FirstName,
                                         MiddleName = m.MiddleName,
                                         //Age = p.Age,
                                         Sex = m.Sex,
                                         //Birthday = m.Birthday
                                     })
                                   .FirstOrDefaultAsync();

                if (patient != null)
                {
                    // Create an insertion model with pre-populated patient data
                    var insertionModel = new Insertion
                    {
                        HospitalNumber = patient.HospNum,
                        PatientLastName = patient.LastName,
                        PatientFirstName = patient.FirstName,
                        PatientMiddleName = patient.MiddleName,
                        //Age = patient.Age,
                        Sex = patient.Sex,
                        //Birthday = patient.Birthday
                    };

                    // Keep the patient ID in TempData for the post action
                    TempData.Keep("PatientId");

                    return View(insertionModel);
                }
            }

            // If no patient ID or patient not found, return empty form
            return View();
        }

        // POST: Insertions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientLastName,PatientFirstName,PatientMiddleName,Age,Sex,Birthday,PatientDiagnosis,HospitalNumber,ReasonforInsertion,ProcedureOperator,NumberofLumens,ProcedureLocation,CatheterType,Classification,Optimal,ExplainWhyAlternate,Left,Right,AnatomyIs,ChestWall,COPD,Emergency,Anesthesiologist,Coagulopathy,Dialysis,OperatorTraining,ObtainInformedConsent,ObtainInformedConsentReminder,ConfirmHandHygiene,ConfirmHandHygieneReminder,UseFullBarrier,UseFullBarrierReminder,PerformSkin,PerformSkinReminder,AllowSite,AllowSiteReminder,UseSterile,UseSterileReminder,Maintain,MaintainReminder,Monitor,MonitorReminder,CleanBlood,CleanBloodReminder,ProcedureNotes,Observer,Operator")] Insertion insertion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insertion);
                await _context.SaveChangesAsync();

                // Check if this was created from HaiChecklist
                if (TempData["PatientId"] != null)
                {
                    string patientId = TempData["PatientId"].ToString();

                    // Redirect back to the patient's HAI checklist
                    return RedirectToAction("HaiChecklist", "ICNPatient", new { id = patientId });
                }

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

                // Check if we need to redirect back to HAI checklist
                if (TempData["PatientId"] != null)
                {
                    string patientId = TempData["PatientId"].ToString();
                    return RedirectToAction("HaiChecklist", "ICNPatient", new { id = patientId });
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