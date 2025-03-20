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
    public class PatientMastersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientMastersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PatientMasters
        public async Task<IActionResult> Index()
        {
            return View(await _context.PatientMasters.ToListAsync());
        }

        // GET: PatientMasters/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientMaster = await _context.PatientMasters
                .FirstOrDefaultAsync(m => m.HospNum == id);
            if (patientMaster == null)
            {
                return NotFound();
            }

            return View(patientMaster);
        }

        // GET: PatientMasters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PatientMasters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HospNum,LastName,FirstName,MiddleName,AccountNum,Sex,BirthDate,CivilStatus,Age,PatientStatus,SeniorCitizenWithID,cellnum,EmpNum,PatientType,EmailAddress")] PatientMaster patientMaster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientMaster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patientMaster);
        }

        // GET: PatientMasters/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientMaster = await _context.PatientMasters.FindAsync(id);
            if (patientMaster == null)
            {
                return NotFound();
            }
            return View(patientMaster);
        }

        // POST: PatientMasters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("HospNum,LastName,FirstName,MiddleName,AccountNum,Sex,BirthDate,CivilStatus,Age,PatientStatus,SeniorCitizenWithID,cellnum,EmpNum,PatientType,EmailAddress")] PatientMaster patientMaster)
        {
            if (id != patientMaster.HospNum)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientMaster);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientMasterExists(patientMaster.HospNum))
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
            return View(patientMaster);
        }

        // GET: PatientMasters/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patientMaster = await _context.PatientMasters
                .FirstOrDefaultAsync(m => m.HospNum == id);
            if (patientMaster == null)
            {
                return NotFound();
            }

            return View(patientMaster);
        }

        // POST: PatientMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var patientMaster = await _context.PatientMasters.FindAsync(id);
            if (patientMaster != null)
            {
                _context.PatientMasters.Remove(patientMaster);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientMasterExists(string id)
        {
            return _context.PatientMasters.Any(e => e.HospNum == id);
        }
    }
}
