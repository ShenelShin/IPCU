using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPCU.Controllers
{
    public class LaboratoryConfirmedBloodstreamInfectionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LaboratoryConfirmedBloodstreamInfectionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        public async Task<IActionResult> Index(string hospNum)
        {
            var model = new LaboratoryConfirmedBSI();
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
        public async Task<IActionResult> PatientIndex(string hospNum)
        {
            if (string.IsNullOrEmpty(hospNum))
            {
                return NotFound("Hospital Number is required.");
            }

            var patients = await _context.LaboratoryConfirmedBSI
                                         .Where(p => p.HospitalNumber == hospNum)
                                         .ToListAsync();

            return View(patients);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.LaboratoryConfirmedBSI
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }
        [HttpPost]

        public IActionResult Submit(LaboratoryConfirmedBSI model, string[] TypeClass)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Concatenate checkbox values with commas
                    model.TypeClass = TypeClass != null ? string.Join(",", TypeClass) : "";

                    _context.LaboratoryConfirmedBSI.Add(model);
                    Console.WriteLine("Saving changes to the database...");
                    _context.SaveChanges();
                    Console.WriteLine("Data saved successfully.");

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving data: {ex.Message}");
                }
            }

            // Log errors if ModelState is invalid
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine($"Validation Error: {error.ErrorMessage}");
            }

            return View("Index", model);
        }

    }
}
