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
    [Authorize(Roles = "ICN, Admin")]
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
                                      AdmissionDuration = EF.Functions.DateDiffDay(p.AdmDate.Value, DateTime.Now)
                                  })
                                 .ToListAsync();

            // Format the patient name after the database query is complete
            foreach (var patient in patients)
            {
                patient.PatientName = $"{patient.LastName}, {patient.FirstName} {patient.MiddleName}";
            }

            return View(patients);
        }

        // GET: Show details of a specific patient
        public async Task<IActionResult> Details(string id)
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

            var assignedAreas = user.AssignedArea?.Split(',').Select(a => a.Trim()).ToList() ?? new List<string>();

            // Get the patient details
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
                                     AdmissionDuration = EF.Functions.DateDiffDay(p.AdmDate.Value, DateTime.Now)
                                 })
                               .FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            // Format the patient name after the database query
            patient.PatientName = $"{patient.LastName}, {patient.FirstName} {patient.MiddleName}";

            return View(patient);
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
    }
}