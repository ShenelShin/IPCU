using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IPCU.Data;
using IPCU.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace IPCU.Controllers
{
    public class VentilatorEventChecklistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public VentilatorEventChecklistController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public async Task<IActionResult> Index(string hospNum)
        {
            var model = new VentilatorEventChecklist();

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
                    if (int.TryParse(patientInfo.PatientMaster.HospNum, out int hospitalNumber))
                    {
                        model.HospNum = hospitalNumber;
                    }
                    else
                    {
                        // Handle the case where HospNum is not a valid integer
                        // You could set a default value or handle the error
                        model.HospNum = 0; // or another appropriate default value
                        ModelState.AddModelError("HospitalNumber", "Invalid hospital number format");
                    }
                    model.Gender = patientInfo.PatientMaster.Sex == "M" ? "Male" : "Female";
                    model.FName = patientInfo.PatientMaster.FirstName;
                    model.MName = patientInfo.PatientMaster.MiddleName;
                    model.LName = patientInfo.PatientMaster.LastName;
                    model.DateOfBirth = patientInfo.PatientMaster.BirthDate;

                    model.UwArea = patientInfo.Patients.AdmLocation;

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(VentilatorEventChecklist model, string[] TypeClass)
        {
            if (ModelState.IsValid)
            {
                // Concatenate selected TypeClass values into a single string
                if (TypeClass != null && TypeClass.Length > 0)
                {
                    model.TypeClass = string.Join(", ", TypeClass);
                }

                // Add and save the model to the database
                _context.VentilatorEventChecklists.Add(model);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            // If the model is invalid, return to the form with the entered data
            return View("Index", model);
        }
    }
}