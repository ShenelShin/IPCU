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
                                         AdmLocation = p.AdmLocation,
                                         RoomID = p.RoomID
                                     })
                                   .FirstOrDefaultAsync();

                if (patient != null)
                {
                    // Create a maintenance checklist model with pre-populated patient data
                    var checklistModel = new DailyCentralLineMaintenanceChecklist
                    {
                        AreaOrUnit = patient.AdmLocation,
                        DateAndTimeOfMonitoring = DateTime.Now,
                        Patient = $"{patient.LastName}, {patient.FirstName} {patient.MiddleName}",
                        Bed = patient.RoomID
                    };

                    // Keep the patient ID in TempData for the post action
                    TempData.Keep("PatientId");

                    return View(checklistModel);
                }
            }

            // If no patient ID or patient not found, return empty form
            return View();
        }

        // POST: DailyCentralLineMaintenanceChecklists/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AreaOrUnit,DateAndTimeOfMonitoring,AssessedBy,Patient,Bed,IntitialPlacement,Implanted,Injection,Dateadministration,Necessityassessed,Injectionsites,Capschanged,Insertionsite,Dressingintact,Dressingchanged,Remarks,NumCompliant,TotalObserved,ComplianceRate")] DailyCentralLineMaintenanceChecklist dailyCentralLineMaintenanceChecklist)
        {
            if (ModelState.IsValid)
            {
                // Calculate compliance rate if not provided
                if (dailyCentralLineMaintenanceChecklist.NumCompliant > 0 && dailyCentralLineMaintenanceChecklist.TotalObserved > 0)
                {
                    dailyCentralLineMaintenanceChecklist.ComplianceRate =
                        (int)(((decimal)dailyCentralLineMaintenanceChecklist.NumCompliant / (decimal)dailyCentralLineMaintenanceChecklist.TotalObserved) * 100);
                }

                _context.Add(dailyCentralLineMaintenanceChecklist);
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
                    // Recalculate compliance rate if values provided
                    if (dailyCentralLineMaintenanceChecklist.NumCompliant > 0 && dailyCentralLineMaintenanceChecklist.TotalObserved > 0)
                    {
                        dailyCentralLineMaintenanceChecklist.ComplianceRate =
                            (int)Math.Round(((decimal)dailyCentralLineMaintenanceChecklist.NumCompliant / (decimal)dailyCentralLineMaintenanceChecklist.TotalObserved) * 100);
                    }

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

                // Check if we need to redirect back to HAI checklist
                if (TempData["PatientId"] != null)
                {
                    string patientId = TempData["PatientId"].ToString();
                    return RedirectToAction("HaiChecklist", "ICNPatient", new { id = patientId });
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

        // GET: View patient-specific maintenance checklists
        public async Task<IActionResult> PatientChecklists(string patientName)
        {
            if (string.IsNullOrEmpty(patientName))
            {
                return NotFound();
            }

            var checklists = await _context.DailyCentralLineMaintenanceChecklists
                                  .Where(c => c.Patient == patientName)
                                  .OrderByDescending(c => c.DateAndTimeOfMonitoring)
                                  .ToListAsync();

            ViewData["PatientName"] = patientName;

            return View(checklists);
        }
    }
}