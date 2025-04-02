using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPCU.Controllers
{
    public class PediatricVAEChecklistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public PediatricVAEChecklistController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // Display the form
        public async Task<IActionResult> Index(string hospNum)
        {
            var model = new PediatricVAEChecklist();
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

        // Handle form submission
        [HttpPost]
        public IActionResult Submit(PediatricVAEChecklist model)
        {
            if (ModelState.IsValid)
            {
                _context.PediatricVAEChecklist.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }

        // Get all Pediatric VAE checklist submissions
        public IActionResult PatientIndex()
        {
            var model = _context.PediatricVAEChecklist.ToList();
            return View("table", model);
        }



        // Action to display the details of a specific checklist item
        public async Task<IActionResult> Details(int id)
        {
            var checklist = await _context.PediatricVAEChecklist
                                          .FirstOrDefaultAsync(c => c.Id == id);

            if (checklist == null)
            {
                return NotFound();
            }

            return View(checklist);
        }




    }
}