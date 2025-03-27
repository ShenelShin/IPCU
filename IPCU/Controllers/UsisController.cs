using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Identity;

namespace IPCU.Controllers
{
    public class UsisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public UsisController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Usis
        public async Task<IActionResult> Index(string hospNum)
        {
            var model = new Usi();

            // Get current user's name for investigator
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                model.Investigator = $"{currentUser.FirstName} {currentUser.Initial} {currentUser.LastName}".Trim();
            }

            if (!string.IsNullOrEmpty(hospNum))
            {
                // Get patient info where HospNum matches
                var patientInfo = await (from pm in _context.PatientMasters
                                         join p in _context.Patients
                                         on pm.HospNum equals p.HospNum
                                         where pm.HospNum == hospNum
                                         select new
                                         {
                                             PatientMaster = pm,
                                             Patients = p
                                         }).FirstOrDefaultAsync();

                if (patientInfo != null)
                {
                    if (int.TryParse(patientInfo.PatientMaster.HospNum, out int hospitalNumber))
                    {
                        model.HospitalNumber = hospitalNumber;
                    }
                    else
                    {
                        // Handle the case where HospNum is not a valid integer
                        // You could set a default value or handle the error
                        model.HospitalNumber = 0; // or another appropriate default value
                        ModelState.AddModelError("HospitalNumber", "Invalid hospital number format");
                    }
                    model.Gender = patientInfo.PatientMaster.Sex == "M" ? "Male" : "Female";
                    model.Fname = patientInfo.PatientMaster.FirstName;
                    model.Mname = patientInfo.PatientMaster.MiddleName;
                    model.Lname = patientInfo.PatientMaster.LastName;
                    model.DateOfBirth = patientInfo.PatientMaster.BirthDate;
                    model.UnitWardArea = patientInfo.Patients.AdmLocation;

                    // null kasi bdate ko fuck goddamit
                    if (patientInfo.Patients.AdmDate.HasValue)
                    {
                        model.DateOfBirth = patientInfo.Patients.AdmDate.Value;
                    }
                    else
                    {
                        // null shit
                        model.DateOfAdmission = DateTime.MinValue;
                    }
                    model.Age = int.Parse(patientInfo.Patients.Age);


                    // Add other fields you want to auto-fill
                }
            }

            return View("Create", model);
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
