using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IPCU.Controllers
{
    public class GIInfectionChecklistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GIInfectionChecklistController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public async Task<IActionResult> Index(string hospNum)
        {
            var model = new GIInfectionChecklist();
            // Get current user's name for investigator
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                model.NameOfInvestigator = $"{currentUser.FirstName} {currentUser.Initial} {currentUser.LastName}".Trim();
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
                    model.HospNum = patientInfo.PatientMaster.HospNum;
                    model.Gender = patientInfo.PatientMaster.Sex == "M" ? "Male" : "Female";
                    model.FName = patientInfo.PatientMaster.FirstName;
                    model.MName = patientInfo.PatientMaster.MiddleName;
                    model.LName = patientInfo.PatientMaster.LastName;
                    model.DateOfBirth = patientInfo.PatientMaster.BirthDate;
                    model.UnitWardArea = patientInfo.Patients.AdmLocation;
                    model.DateOfAdmission = patientInfo.Patients.AdmDate.Value;
                    model.Age = int.Parse(patientInfo.Patients.Age);


                    // Add other fields you want to auto-fill
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Submit(GIInfectionChecklist model, string[] TypeClassification)
        {
            if (TypeClassification != null && TypeClassification.Length > 0)
            {
                model.TypeClassification = string.Join(", ", TypeClassification);
            }

            if (!ModelState.IsValid)
            {
                // Debugging: Print ModelState errors
                foreach (var error in ModelState)
                {
                    foreach (var subError in error.Value.Errors)
                    {
                        Console.WriteLine($"Field: {error.Key}, Error: {subError.ErrorMessage}");
                    }
                }

                Console.WriteLine("Model state is invalid!");
                return View("Index", model);
            }

            _context.GIInfectionChecklists.Add(model);
            await _context.SaveChangesAsync();

            Console.WriteLine("Saved successfully!");
            return RedirectToAction("Index");
        }


    }
}
