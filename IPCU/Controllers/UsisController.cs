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
    public class UsisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Usis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Usi.ToListAsync());
        }

        // GET: Usis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usi = await _context.Usi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usi == null)
            {
                return NotFound();
            }

            return View(usi);
        }

        // GET: Usis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fname,Lname,Mname,HospitalNumber,DateOfBirth,Age,UnitWardArea,MainService,DateOfEvent,Investigator,DateOfAdmission,Disposition,DispositionDate,DispositionTransfer,Gender,Classification,MDRO,TypeClass,PatientOrganism,PatientAbscess,Fever1,LocalizedPain,PurulentDrainage,Organism,PatienLessthan1year,Fever2,Hypothermia,Apnea,Bradycardia,Lethargy,Vomiting,PurulentDrainage2,Organism2,CultureDate,CultureResults")] Usi usi)
        {
            if (ModelState.IsValid)
            {
                // Set TypeClass value to "USI" before saving
                usi.TypeClass = "USI";

                _context.Add(usi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usi);
        }



        // GET: Usis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usi = await _context.Usi.FindAsync(id);
            if (usi == null)
            {
                return NotFound();
            }
            return View(usi);
        }

        // POST: Usis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fname,Lname,Mname,HospitalNumber,DateOfBirth,Age,UnitWardArea,MainService,DateOfEvent,Investigator,DateOfAdmission,Disposition,DispositionDate,DispositionTransfer,Gender,Classification,MDRO,TypeClass,PatientOrganism,PatientAbscess,Fever1,LocalizedPain,PurulentDrainage,Organism,PatienLessthan1year,Fever2,Hypothermia,Apnea,Bradycardia,Lethargy,Vomiting,PurulentDrainage2,Organism2,CultureDate,CultureResults")] Usi usi)
        {
            if (id != usi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsiExists(usi.Id))
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
            return View(usi);
        }

        // GET: Usis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usi = await _context.Usi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usi == null)
            {
                return NotFound();
            }

            return View(usi);
        }

        // POST: Usis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usi = await _context.Usi.FindAsync(id);
            if (usi != null)
            {
                _context.Usi.Remove(usi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsiExists(int id)
        {
            return _context.Usi.Any(e => e.Id == id);
        }
    }
}
