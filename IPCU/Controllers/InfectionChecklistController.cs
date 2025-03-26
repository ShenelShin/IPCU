using Microsoft.AspNetCore.Mvc;
using IPCU.Models;
using IPCU.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace IPCU.Controllers
{
    public class InfectionChecklistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        // Inject DbContext via constructor
        public InfectionChecklistController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public async Task<IActionResult> Index(string hospNum)
        {
            var model = new SSTInfectionModel();

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
                    // null kasi bdate ko fuck goddamit
                    if (patientInfo.PatientMaster.BirthDate.HasValue)
                    {
                        model.DateOfBirth = patientInfo.PatientMaster.BirthDate.Value;
                    }
                    else
                    {
                        // null shit
                        model.DateOfBirth = DateTime.MinValue;
                    }
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

            return View(model);
        }

        [HttpPost]
        public IActionResult Submit(SSTInfectionModel model)
        {
            if (ModelState.IsValid)
            {
                // Add the infection record to database
                _context.SSTInfectionModels.Add(model);

                // Find the patient master record
                var patientMaster = _context.PatientMasters
                    .FirstOrDefault(p => p.HospNum == model.HospitalNumber);

                if (patientMaster != null)
                {
                    // Increment the HAI count
                    patientMaster.HaiCount += 1;

                    // Update the patient master record
                    _context.PatientMasters.Update(patientMaster);
                }

                // Save all changes to the database
                _context.SaveChanges();

                TempData["Success"] = "Checklist submitted successfully! HAI count has been updated.";
                return RedirectToAction("Index");
            }
            return View("Index", model);
        }
    }
}