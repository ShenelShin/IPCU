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

        public ActionResult Index()
        {
            var model = new SSTInfectionModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Submit()
        {
            var model = new SSTInfectionModel();

            // Reading values manually
            model.Fname = Request.Form["Fname"];
            model.Mname = Request.Form["Mname"];
            model.Lname = Request.Form["Lname"];
            model.HospitalNumber = Request.Form["HospitalNumber"];
            model.UnitWardArea = Request.Form["UnitWardArea"];
            model.MainService = Request.Form["MainService"];
            model.Investigator = Request.Form["Investigator"];
            model.Disposition = Request.Form["Disposition"];
            model.Gender = Request.Form["Gender"];
            model.Classification = Request.Form["Classification"];
            model.InfectionType = Request.Form["InfectionType"];
            model.InfectionClassification = Request.Form["InfectionClassification"];

            // Date and int parsing
            bool valid = true; // Flag to track validity

            if (string.IsNullOrWhiteSpace(model.Fname) ||
                string.IsNullOrWhiteSpace(model.Lname) ||
                string.IsNullOrWhiteSpace(model.HospitalNumber) ||
                string.IsNullOrWhiteSpace(model.UnitWardArea) ||
                string.IsNullOrWhiteSpace(model.MainService) ||
                string.IsNullOrWhiteSpace(model.Investigator) ||
                string.IsNullOrWhiteSpace(model.Disposition) ||
                string.IsNullOrWhiteSpace(model.Gender) ||
                string.IsNullOrWhiteSpace(model.Classification) ||
                string.IsNullOrWhiteSpace(model.InfectionType) ||
                string.IsNullOrWhiteSpace(model.InfectionClassification))
            {
                valid = false;
                ModelState.AddModelError("", "All required fields must be filled!");
            }

            // DateOfBirth check
            if (DateTime.TryParse(Request.Form["DateOfBirth"], out DateTime dob))
            {
                model.DateOfBirth = dob;
            }
            else
            {
                valid = false;
                ModelState.AddModelError("DateOfBirth", "Invalid Date of Birth!");
            }

            // Age check
            if (int.TryParse(Request.Form["Age"], out int age))
            {
                model.Age = age;
            }
            else
            {
                valid = false;
                ModelState.AddModelError("Age", "Invalid Age!");
            }

            // DateOfEvent
            if (DateTime.TryParse(Request.Form["DateOfEvent"], out DateTime eventDate))
            {
                model.DateOfEvent = eventDate;
            }
            else
            {
                valid = false;
                ModelState.AddModelError("DateOfEvent", "Invalid Date of Event!");
            }

            // DateOfAdmission
            if (DateTime.TryParse(Request.Form["DateOfAdmission"], out DateTime admissionDate))
            {
                model.DateOfAdmission = admissionDate;
            }
            else
            {
                valid = false;
                ModelState.AddModelError("DateOfAdmission", "Invalid Date of Admission!");
            }

            // Boolean fields
            model.MDRO = Request.Form["MDRO"] == "true";

            // Final Validation Check
            if (!valid)
            {
                TempData["Error"] = "Please fill all required fields correctly!";
                return View("Index", model);
            }


            // Burn Infection
            model.BurnAppearanceChange = Request.Form["BurnAppearanceChange"] == "true";
            model.BurnOrganismIdentified = Request.Form["BurnOrganismIdentified"] == "true";
            model.BurnCultureDate = Request.Form["BurnCultureDate"];
            model.BurnCultureResults = Request.Form["BurnCultureResults"];

            // Decubitus Ulcer Infection
            model.DecubitusErythema = Request.Form["DecubitusErythema"] == "true";
            model.DecubitusTenderness = Request.Form["DecubitusTenderness"] == "true";
            model.DecubitusSwelling = Request.Form["DecubitusSwelling"] == "true";
            model.DecubitusOrganismIdentified = Request.Form["DecubitusOrganismIdentified"] == "true";
            model.DecubitusCultureDate = Request.Form["DecubitusCultureDate"];
            model.DecubitusCultureResults = Request.Form["DecubitusCultureResults"];

            // ST-Soft Tissue Infection
            model.STOrganismIdentified = Request.Form["STOrganismIdentified"] == "true";
            model.STDrainage = Request.Form["STDrainage"] == "true";
            model.STAbscess = Request.Form["STAbscess"] == "true";
            model.STCultureDate = Request.Form["STCultureDate"];
            model.STCultureResults = Request.Form["STCultureResults"];

            // Skin-Skin Infection
            model.SkinPurulentDrainage = Request.Form["SkinPurulentDrainage"] == "true";
            model.SkinVesicles = Request.Form["SkinVesicles"] == "true";
            model.SkinPustules = Request.Form["SkinPustules"] == "true";
            model.SkinBoils = Request.Form["SkinBoils"] == "true";

            model.LocalizedPainTenderness = Request.Form["LocalizedPainTenderness"] == "true";
            model.LocalizedSwelling = Request.Form["LocalizedSwelling"] == "true";
            model.LocalizedErythema = Request.Form["LocalizedErythema"] == "true";
            model.LocalizedHeat = Request.Form["LocalizedHeat"] == "true";

            model.OrganismIdentifiedFromAspiration = Request.Form["OrganismIdentifiedFromAspiration"] == "true";
            model.MultinucleatedGiantCellsSeen = Request.Form["MultinucleatedGiantCellsSeen"] == "true";
            model.DiagnosticAntibodyTiter = Request.Form["DiagnosticAntibodyTiter"] == "true";
            model.SkinCultureDate = Request.Form["SkinCultureDate"];
            model.SkinCultureResults = Request.Form["SkinCultureResults"];

            // Save to DB
            _context.SSTInfectionModels.Add(model);
            _context.SaveChanges();

            TempData["Success"] = "Checklist submitted successfully!";
            return RedirectToAction("Index");
        }
    }
}