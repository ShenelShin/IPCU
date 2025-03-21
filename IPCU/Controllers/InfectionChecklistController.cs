using Microsoft.AspNetCore.Mvc;
using IPCU.Models;
using IPCU.Data;
namespace IPCU.Controllers
{
    public class InfectionChecklistController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Inject DbContext via constructor
        public InfectionChecklistController(ApplicationDbContext context)
        {
            _context = context;
        }

        public ActionResult Index(string hospNum)
        {
            var model = new SSTInfectionModel();

            // If hospital number is provided, pre-fill patient info
            if (!string.IsNullOrEmpty(hospNum))
            {
                // Get patient data from database
                var patientMaster = _context.PatientMasters
                    .FirstOrDefault(p => p.HospNum == hospNum);

                if (patientMaster != null)
                {
                    // Pre-fill the model with patient data
                    model.HospitalNumber = hospNum;
                    model.Fname = patientMaster.FirstName;
                    model.Mname = patientMaster.MiddleName;
                    model.Lname = patientMaster.LastName;
                    model.Gender = patientMaster.Sex;

                    // string to int putangina
                    if (int.TryParse(patientMaster.Age, out int ageValue))
                    {
                        model.Age = ageValue;
                    }
                    else
                    {
                        // Handle the case where Age is not a valid integer
                        model.Age = 0; // or some other default value
                    }


                    // null kasi bdate ko fuck goddamit
                    if (patientMaster.BirthDate.HasValue)
                    {
                        model.DateOfBirth = patientMaster.BirthDate.Value;
                    }
                    else
                    {
                        // null shit
                        model.DateOfBirth = DateTime.MinValue; 
                    }

                    // You could also query for other relevant patient information
                    var patient = _context.Patients
                        .FirstOrDefault(p => p.HospNum == hospNum);

                    if (patient != null)
                    {
                        model.UnitWardArea = patient.AdmLocation;

                        // Pre-fill other fields as needed
                    }
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