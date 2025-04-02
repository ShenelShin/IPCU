using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IPCU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IPCU.Data;

namespace IPCU.Controllers
{
    public class ICNPatientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ICNPatientController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Display list of patients in the ICN's assigned area who have been admitted for 48+ hours
        public async Task<IActionResult> Index()
        {
            // Get the current logged-in user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Get the user's assigned areas
            var assignedAreas = user.AssignedArea?.Split(',').Select(a => a.Trim()).ToList() ?? new List<string>();

            // Calculate the date 48 hours ago
            var fortyEightHoursAgo = DateTime.Now.AddHours(-48);

            // Get patients who have been admitted for at least 48 hours and are in the user's assigned areas
            var patients = await (from p in _context.Patients
                                  join m in _context.PatientMasters on p.HospNum equals m.HospNum
                                  where p.AdmDate <= fortyEightHoursAgo &&
                                        assignedAreas.Contains(p.AdmLocation) &&
                                        p.DeathDate == null // Exclude deceased patients
                                                            // Order by location, room, and last name directly (not the formatted string)
                                  orderby p.AdmLocation, p.RoomID, m.LastName, m.FirstName
                                  select new PatientViewModel
                                  {
                                      HospNum = p.HospNum,
                                      IdNum = p.IdNum,
                                      // Store the name parts separately to format after EF Core query execution
                                      LastName = m.LastName,
                                      FirstName = m.FirstName,
                                      MiddleName = m.MiddleName,
                                      AdmType = p.AdmType,
                                      AdmLocation = p.AdmLocation,
                                      AdmDate = p.AdmDate,
                                      RoomID = p.RoomID,
                                      Age = p.Age,
                                      Sex = m.Sex,
                                      AdmissionDuration = EF.Functions.DateDiffDay(p.AdmDate.Value, DateTime.Now),
                                      HaiStatus = m.HaiStatus,
                                      HaiCount = m.HaiCount
                                  })
                                 .ToListAsync();

            // Format the patient name after the database query is complete
            foreach (var patient in patients)
            {
                patient.PatientName = $"{patient.LastName}, {patient.FirstName} {patient.MiddleName}";
            }

            return View(patients);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var assignedAreas = user.AssignedArea?.Split(',').Select(a => a.Trim()).ToList() ?? new List<string>();

            // Get the patient details with all related infection forms
            var patient = await (from p in _context.Patients
                                 join m in _context.PatientMasters on p.HospNum equals m.HospNum
                                 where p.IdNum == id &&
                                       assignedAreas.Contains(p.AdmLocation)
                                 select new PatientViewModel
                                 {
                                     HospNum = p.HospNum,
                                     IdNum = p.IdNum,
                                     LastName = m.LastName,
                                     FirstName = m.FirstName,
                                     MiddleName = m.MiddleName,
                                     AdmType = p.AdmType,
                                     AdmLocation = p.AdmLocation,
                                     AdmDate = p.AdmDate,
                                     RoomID = p.RoomID,
                                     Age = p.Age,
                                     Sex = m.Sex,
                                     CivilStatus = m.CivilStatus,
                                     PatientType = m.PatientType,
                                     EmailAddress = m.EmailAddress,
                                     CellNum = m.cellnum,
                                     AdmissionDuration = EF.Functions.DateDiffDay(p.AdmDate.Value, DateTime.Now),
                                     VitalSigns = _context.VitalSigns.Where(v => v.HospNum == m.HospNum).ToList(),
                                     InfectionForms = new InfectionFormsInfo
                                     {
                                         CardiovascularForm = _context.CardiovascularSystemInfection
                                             .FirstOrDefault(f => f.HospitalNumber == p.HospNum),
                                         SSTForm = _context.SSTInfectionModels
                                             .FirstOrDefault(f => f.HospitalNumber == p.HospNum),
                                         LCBIForm = _context.LaboratoryConfirmedBSI
                                             .FirstOrDefault(f => f.HospitalNumber == p.HospNum),
                                         PVAEForm = _context.PediatricVAEChecklist
                                             .FirstOrDefault(f => f.HospitalNumber == p.HospNum),
                                         UTIForm = _context.UTIModels
                                             .FirstOrDefault(f => f.HospitalNumber == p.HospNum),
                                         PneumoniaForm = _context.Pneumonias
                                             .FirstOrDefault(f => f.HospitalNumber == p.HospNum),
                                         USIForm = _context.Usi
                                             .FirstOrDefault(f => f.HospitalNumber == p.HospNum),
                                         VAEForm = _context.VentilatorEventChecklists
                                             .FirstOrDefault(f => f.HospNum == p.HospNum)
                                     }
                                 })
                                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            // Format the patient name
            patient.PatientName = $"{patient.LastName}, {patient.FirstName} {patient.MiddleName}";

            // Update HAI count based on existing forms
            patient.HaiCount = CountHaiForms(patient.InfectionForms);

            return View(patient);
        }

        private int CountHaiForms(InfectionFormsInfo forms)
        {
            int count = 0;
            if (forms.CardiovascularForm != null) count++;
            if (forms.SSTForm != null) count++;
            if (forms.LCBIForm != null) count++;
            if (forms.PVAEForm != null) count++;
            if (forms.UTIForm != null) count++;
            if (forms.PneumoniaForm != null) count++;
            if (forms.USIForm != null) count++;
            if (forms.VAEForm != null) count++;
            return count;
        }


        // GET: Show vital signs for a specific patient
        public async Task<IActionResult> VitalSigns(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            // Get the current logged-in user to check assigned areas
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Get the patient to verify they're in an assigned area and to get the HospNum
            var patient = await _context.Patients
                .Where(p => p.IdNum == id)
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            var assignedAreas = user.AssignedArea?.Split(',').Select(a => a.Trim()).ToList() ?? new List<string>();
            if (!assignedAreas.Contains(patient.AdmLocation))
            {
                return Forbid();
            }

            // Get vital signs for this patient
            var vitalSigns = await _context.VitalSigns
                .Where(v => v.HospNum == patient.HospNum)
                .OrderByDescending(v => v.VitalSignDate)
                .ToListAsync();

            // Get basic patient info for display at the top of the page
            var patientInfo = await (from p in _context.Patients
                                     join m in _context.PatientMasters on p.HospNum equals m.HospNum
                                     where p.IdNum == id
                                     select new PatientViewModel
                                     {
                                         HospNum = p.HospNum,
                                         IdNum = p.IdNum,
                                         LastName = m.LastName,
                                         FirstName = m.FirstName,
                                         MiddleName = m.MiddleName,
                                         AdmLocation = p.AdmLocation,
                                         RoomID = p.RoomID
                                     })
                                  .FirstOrDefaultAsync();

            patientInfo.PatientName = $"{patientInfo.LastName}, {patientInfo.FirstName} {patientInfo.MiddleName}";

            // Create a view model that contains both patient info and vital signs
            var viewModel = new PatientVitalSignsViewModel
            {
                Patient = patientInfo,
                VitalSigns = vitalSigns
            };

            return View(viewModel);
        }

        // GET: Display form to add a new vital sign
        public async Task<IActionResult> AddVitalSign(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            // Get the current logged-in user to check assigned areas
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Get the patient to verify they're in an assigned area
            var patient = await _context.Patients
                .Where(p => p.IdNum == id)
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            var assignedAreas = user.AssignedArea?.Split(',').Select(a => a.Trim()).ToList() ?? new List<string>();
            if (!assignedAreas.Contains(patient.AdmLocation))
            {
                return Forbid();
            }

            // Get basic patient info for the form
            var patientInfo = await (from p in _context.Patients
                                     join m in _context.PatientMasters on p.HospNum equals m.HospNum
                                     where p.IdNum == id
                                     select new PatientViewModel
                                     {
                                         IdNum = p.IdNum,
                                         HospNum = p.HospNum,
                                         AdmLocation = p.AdmLocation,
                                         RoomID = p.RoomID,
                                         LastName = m.LastName,
                                         FirstName = m.FirstName,
                                         MiddleName = m.MiddleName
                                     })
                               .FirstOrDefaultAsync();

            patientInfo.PatientName = $"{patientInfo.LastName}, {patientInfo.FirstName} {patientInfo.MiddleName}";

            // Create the view model for adding vital signs
            var viewModel = new AddVitalSignViewModel
            {
                IdNum = patientInfo.IdNum,
                HospNum = patientInfo.HospNum,
                AdmLocation = patientInfo.AdmLocation,
                RoomID = patientInfo.RoomID,
                PatientName = patientInfo.PatientName,
                VitalSignDate = DateTime.Now
            };

            return View(viewModel);
        }

        // POST: Process the form submission to add a new vital sign
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVitalSign(AddVitalSignViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Create a new vital sign record
            var vitalSign = new VitalSigns
            {
                HospNum = model.HospNum,
                VitalSign = model.VitalSign,
                VitalSignValue = model.VitalSignValue,
                VitalSignDate = model.VitalSignDate
            };

            // Add and save the new vital sign
            _context.VitalSigns.Add(vitalSign);
            await _context.SaveChangesAsync();

            // Redirect back to the vital signs list
            return RedirectToAction(nameof(VitalSigns), new { id = model.IdNum });
        }

        // GET: Show connected devices for a specific patient
        public async Task<IActionResult> ConnectedDevices(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            // Get the current logged-in user to check assigned areas
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Get the patient to verify they're in an assigned area and to get the HospNum
            var patient = await _context.Patients
                .Where(p => p.IdNum == id)
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            var assignedAreas = user.AssignedArea?.Split(',').Select(a => a.Trim()).ToList() ?? new List<string>();
            if (!assignedAreas.Contains(patient.AdmLocation))
            {
                return Forbid();
            }

            // Get connected devices for this patient
            var connectedDevices = await _context.DeviceConnected
                .Where(d => d.HospNum == patient.HospNum)
                .OrderByDescending(d => d.DeviceInsert)
                .ToListAsync();

            // Get basic patient info for display at the top of the page
            var patientInfo = await (from p in _context.Patients
                                     join m in _context.PatientMasters on p.HospNum equals m.HospNum
                                     where p.IdNum == id
                                     select new PatientViewModel
                                     {
                                         HospNum = p.HospNum,
                                         IdNum = p.IdNum,
                                         LastName = m.LastName,
                                         FirstName = m.FirstName,
                                         MiddleName = m.MiddleName,
                                         AdmLocation = p.AdmLocation,
                                         RoomID = p.RoomID
                                     })
                                  .FirstOrDefaultAsync();

            patientInfo.PatientName = $"{patientInfo.LastName}, {patientInfo.FirstName} {patientInfo.MiddleName}";

            // Create a view model that contains both patient info and connected devices
            var viewModel = new PatientDevicesViewModel
            {
                Patient = patientInfo,
                ConnectedDevices = connectedDevices
            };

            return View(viewModel);
        }

        // GET: Display form to add a new connected device
        public async Task<IActionResult> AddConnectedDevice(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            // Get the current logged-in user to check assigned areas
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Get the patient to verify they're in an assigned area
            var patient = await _context.Patients
                .Where(p => p.IdNum == id)
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            var assignedAreas = user.AssignedArea?.Split(',').Select(a => a.Trim()).ToList() ?? new List<string>();
            if (!assignedAreas.Contains(patient.AdmLocation))
            {
                return Forbid();
            }

            // Get basic patient info for the form
            var patientInfo = await (from p in _context.Patients
                                     join m in _context.PatientMasters on p.HospNum equals m.HospNum
                                     where p.IdNum == id
                                     select new PatientViewModel
                                     {
                                         IdNum = p.IdNum,
                                         HospNum = p.HospNum,
                                         AdmLocation = p.AdmLocation,
                                         RoomID = p.RoomID,
                                         LastName = m.LastName,
                                         FirstName = m.FirstName,
                                         MiddleName = m.MiddleName
                                     })
                               .FirstOrDefaultAsync();

            patientInfo.PatientName = $"{patientInfo.LastName}, {patientInfo.FirstName} {patientInfo.MiddleName}";

            // Create the view model for adding connected device
            var viewModel = new AddConnectedDeviceViewModel
            {
                IdNum = patientInfo.IdNum,
                HospNum = patientInfo.HospNum,
                AdmLocation = patientInfo.AdmLocation,
                RoomID = patientInfo.RoomID,
                PatientName = patientInfo.PatientName,
                DeviceInsert = DateTime.Now.Date
            };

            return View(viewModel);
        }

        // POST: Process the form submission to add a new connected device
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddConnectedDevice(AddConnectedDeviceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Create a new device record
            var device = new DeviceConnected
            {
                DeviceId = Guid.NewGuid().ToString(),
                HospNum = model.HospNum,
                DeviceType = model.DeviceType,
                DeviceInsert = model.DeviceInsert,
                DeviceRemove = model.DeviceRemove
            };

            // Add and save the new device
            _context.DeviceConnected.Add(device);
            await _context.SaveChangesAsync();

            // Redirect back to the connected devices list
            return RedirectToAction(nameof(ConnectedDevices), new { id = model.IdNum });
        }

        // GET: Display form to update a connected device (mainly to set removal date)
        public async Task<IActionResult> UpdateConnectedDevice(string id, string deviceId)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(deviceId))
            {
                return NotFound();
            }

            // Get the current logged-in user to check assigned areas
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Get the patient to verify they're in an assigned area
            var patient = await _context.Patients
                .Where(p => p.IdNum == id)
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            var assignedAreas = user.AssignedArea?.Split(',').Select(a => a.Trim()).ToList() ?? new List<string>();
            if (!assignedAreas.Contains(patient.AdmLocation))
            {
                return Forbid();
            }

            // Get the device
            var device = await _context.DeviceConnected
                .Where(d => d.DeviceId == deviceId && d.HospNum == patient.HospNum)
                .FirstOrDefaultAsync();

            if (device == null)
            {
                return NotFound();
            }

            // Get basic patient info for the form
            var patientInfo = await (from p in _context.Patients
                                     join m in _context.PatientMasters on p.HospNum equals m.HospNum
                                     where p.IdNum == id
                                     select new PatientViewModel
                                     {
                                         IdNum = p.IdNum,
                                         HospNum = p.HospNum,
                                         AdmLocation = p.AdmLocation,
                                         RoomID = p.RoomID,
                                         LastName = m.LastName,
                                         FirstName = m.FirstName,
                                         MiddleName = m.MiddleName
                                     })
                              .FirstOrDefaultAsync();

            patientInfo.PatientName = $"{patientInfo.LastName}, {patientInfo.FirstName} {patientInfo.MiddleName}";

            // Create the view model for updating connected device
            var viewModel = new UpdateConnectedDeviceViewModel
            {
                DeviceId = device.DeviceId,
                IdNum = patientInfo.IdNum,
                HospNum = patientInfo.HospNum,
                AdmLocation = patientInfo.AdmLocation,
                RoomID = patientInfo.RoomID,
                PatientName = patientInfo.PatientName,
                DeviceType = device.DeviceType,
                DeviceInsert = device.DeviceInsert,
                DeviceRemove = device.DeviceRemove ?? DateTime.Now.Date
            };

            return View(viewModel);
        }

        // POST: Process the form submission to update a connected device
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateConnectedDevice(UpdateConnectedDeviceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Get the device
            var device = await _context.DeviceConnected
                .Where(d => d.DeviceId == model.DeviceId)
                .FirstOrDefaultAsync();

            if (device == null)
            {
                return NotFound();
            }

            // Update the device
            device.DeviceRemove = model.DeviceRemove;

            // Save changes
            await _context.SaveChangesAsync();

            // Redirect back to the connected devices list
            return RedirectToAction(nameof(ConnectedDevices), new { id = model.IdNum });
        }

        // Add this action method to ICNPatientController
        // Modify your UpdateHaiStatus method like this:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateHaiStatus(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            try
            {
                // Get the current logged-in user to check assigned areas
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound();
                }

                // Get the patient
                var patient = await _context.Patients
                    .Where(p => p.IdNum == id)
                    .FirstOrDefaultAsync();

                if (patient == null)
                {
                    return NotFound();
                }

                var assignedAreas = user.AssignedArea?.Split(',').Select(a => a.Trim()).ToList() ?? new List<string>();
                if (!assignedAreas.Contains(patient.AdmLocation))
                {
                    return Forbid();
                }

                // Get the PatientMaster record
                var patientMaster = await _context.PatientMasters
                    .FirstOrDefaultAsync(m => m.HospNum == patient.HospNum);

                if (patientMaster == null)
                {
                    return NotFound();
                }

                // Add explicit debug logging
                Console.WriteLine($"Before update - HaiStatus: {patientMaster.HaiStatus}, HaiCount: {patientMaster.HaiCount}");

                // Update HAI status
                patientMaster.HaiStatus = true; // Set HAI status to true

                //// Simply increment the count since it's a non-nullable int
                //patientMaster.HaiCount += 1;

                Console.WriteLine($"After update - HaiStatus: {patientMaster.HaiStatus}, HaiCount: {patientMaster.HaiCount}");

                Console.WriteLine($"After update - HaiStatus: {patientMaster.HaiStatus}, HaiCount: {patientMaster.HaiCount}");

                Console.WriteLine($"After update - HaiStatus: {patientMaster.HaiStatus}, HaiCount: {patientMaster.HaiCount}");

                // Mark entity as modified to force update
                _context.Entry(patientMaster).State = EntityState.Modified;

                // Save changes
                int rowsAffected = await _context.SaveChangesAsync();
                Console.WriteLine($"SaveChanges completed. Rows affected: {rowsAffected}");

                if (rowsAffected > 0)
                {
                    TempData["SuccessMessage"] = "Patient has been marked as having an HAI.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to update HAI status. No records were modified.";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating HAI status: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");

                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        // Add this action method to ICNPatientController
        public async Task<IActionResult> HaiChecklist(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            // Get the current logged-in user to check assigned areas
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Get the patient to verify they're in an assigned area
            var patient = await _context.Patients
                .Where(p => p.IdNum == id)
                .FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            var assignedAreas = user.AssignedArea?.Split(',').Select(a => a.Trim()).ToList() ?? new List<string>();
            if (!assignedAreas.Contains(patient.AdmLocation))
            {
                return Forbid();
            }

            // Get patient master data
            var patientMaster = await _context.PatientMasters
                .Where(m => m.HospNum == patient.HospNum)
                .FirstOrDefaultAsync();

            if (patientMaster == null || !patientMaster.HaiStatus)
            {
                // Redirect to details if patient doesn't have HAI status
                return RedirectToAction(nameof(Details), new { id });
            }

            // Get basic patient info for the HAI checklist page
            var patientInfo = await (from p in _context.Patients
                                     join m in _context.PatientMasters on p.HospNum equals m.HospNum
                                     where p.IdNum == id
                                     select new PatientViewModel
                                     {
                                         IdNum = p.IdNum,
                                         HospNum = p.HospNum,
                                         AdmLocation = p.AdmLocation,
                                         RoomID = p.RoomID,
                                         LastName = m.LastName,
                                         FirstName = m.FirstName,
                                         MiddleName = m.MiddleName,
                                         HaiStatus = m.HaiStatus,
                                         HaiCount = m.HaiCount
                                     })
                               .FirstOrDefaultAsync();

            patientInfo.PatientName = $"{patientInfo.LastName}, {patientInfo.FirstName} {patientInfo.MiddleName}";

            // Return the HAI checklist view (which you'll create later)
            return View(patientInfo);
        }
    }
}