using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPCU.Controllers
{
    public class CardiovascularSystemInfectionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CardiovascularSystemInfectionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // Display the form
        public async Task<IActionResult> Index(string hospNum)
        {
            var model = new CardiovascularSystemInfection();

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
                    model.HospitalNumber = patientInfo.PatientMaster.HospNum;
                    model.Gender = patientInfo.PatientMaster.Sex == "M" ? "Male" : "Female";
                    model.Fname = patientInfo.PatientMaster.FirstName;
                    model.Mname = patientInfo.PatientMaster.MiddleName;
                    model.Lname = patientInfo.PatientMaster.LastName;
                    model.DateOfBirth = patientInfo.PatientMaster.BirthDate;
                    model.UnitWardArea = patientInfo.Patients.AdmLocation;
                    model.DateOfAdmission = patientInfo.Patients.AdmDate;
                    model.Age = int.Parse(patientInfo.Patients.Age);


                    // Add other fields you want to auto-fill
                }
            }

            return View(model);
        }

        // Submit the form
        [HttpPost]
        public IActionResult Submit(CardiovascularSystemInfection model)
        {

            if (ModelState.IsValid)
            {
                _context.CardiovascularSystemInfection.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }
    }
}
