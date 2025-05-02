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
using OfficeOpenXml.Style;
using OfficeOpenXml;
using IPCU.Services;

namespace IPCU.Controllers
{
    public class ICNPatientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PatientDbContext _patientContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ICNPatientController> _logger;


        public ICNPatientController(
            ApplicationDbContext context,
            PatientDbContext patientContext,
            UserManager<ApplicationUser> userManager,
            ILogger<ICNPatientController> logger)
        {
            _context = context;
            _patientContext = patientContext;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Display list of patients in the ICN's assigned area who have been admitted for 48+ hours
        public async Task<IActionResult> Index()
        {
            try
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

                // Create a list to store our results
                var patients = new List<PatientViewModel>();

                // Use raw SQL to query the database directly with correct table names
                // Added LIMIT 50 to restrict the number of returned patients
                using (var command = _patientContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
                SELECT TOP 50000 p.HospNum, p.IdNum, p.AdmType, p.AdmLocation, p.AdmDate, p.RoomID, p.Age, 
                       m.LastName, m.FirstName, m.MiddleName, m.Sex, m.CivilStatus, m.PatientType, 
                       m.EmailAddress, m.cellnum
                FROM tbpatient p
                LEFT JOIN tbmaster m ON p.HospNum = m.HospNum
                WHERE p.AdmDate <= @fortyEightHoursAgo 
                AND p.DeathDate IS NULL
                ORDER BY p.AdmDate DESC";

                    // Add parameters
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@fortyEightHoursAgo";
                    parameter.Value = fortyEightHoursAgo;
                    command.Parameters.Add(parameter);

                    // Ensure connection is open
                    if (command.Connection.State != System.Data.ConnectionState.Open)
                    {
                        await command.Connection.OpenAsync();
                    }

                    // Execute query
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var hospNum = reader["HospNum"]?.ToString();
                            var admLocation = reader["AdmLocation"]?.ToString();

                            // Skip if not in assigned areas
                            if (assignedAreas.Any() && !string.IsNullOrEmpty(admLocation) && !assignedAreas.Contains(admLocation))
                            {
                                continue;
                            }

                            var patient = new PatientViewModel
                            {
                                HospNum = hospNum,
                                IdNum = reader["IdNum"]?.ToString() ?? string.Empty,
                                LastName = reader["LastName"]?.ToString() ?? string.Empty,
                                FirstName = reader["FirstName"]?.ToString() ?? string.Empty,
                                MiddleName = reader["MiddleName"]?.ToString() ?? string.Empty,
                                AdmType = reader["AdmType"]?.ToString() ?? string.Empty,
                                AdmLocation = admLocation ?? string.Empty,
                                AdmDate = reader["AdmDate"] as DateTime?,
                                RoomID = reader["RoomID"]?.ToString() ?? string.Empty,
                                Age = reader["Age"]?.ToString() ?? string.Empty,
                                Sex = reader["Sex"]?.ToString() ?? string.Empty,
                                CivilStatus = reader["CivilStatus"]?.ToString() ?? string.Empty,
                                PatientType = reader["PatientType"]?.ToString() ?? string.Empty,
                                EmailAddress = reader["EmailAddress"]?.ToString() ?? string.Empty,
                                CellNum = reader["cellnum"]?.ToString() ?? string.Empty,
                                AdmissionDuration = reader["AdmDate"] != DBNull.Value ?
                                    (int)Math.Round((DateTime.Now - (DateTime)reader["AdmDate"]).TotalDays) : 0,
                                PatientName = $"{reader["LastName"]?.ToString() ?? ""}, {reader["FirstName"]?.ToString() ?? ""} {reader["MiddleName"]?.ToString() ?? ""}"
                            };

                            patients.Add(patient);
                        }
                    }
                }

                // Get HAI data from ApplicationDbContext (_context) where tbPatientHAI is located
                if (patients.Any())
                {
                    var hospNums = patients.Select(p => p.HospNum).ToList();

                    // Create a dictionary for HAI data
                    var haiDict = new Dictionary<string, (bool HaiStatus, int HaiCount)>();

                    // Process in batches of 2000 to stay under SQL Server's 2100 parameter limit
                    const int batchSize = 2000;

                    for (int batchStart = 0; batchStart < hospNums.Count; batchStart += batchSize)
                    {
                        // Get the current batch
                        var currentBatch = hospNums
                            .Skip(batchStart)
                            .Take(batchSize)
                            .ToList();

                        if (!currentBatch.Any())
                            continue;

                        // Use the table-valued parameter approach with string join instead of parameters
                        // This is safe because hospNum values are controlled by the application
                        string hospNumsString = "'" + string.Join("','", currentBatch.Select(h => h.Replace("'", "''"))) + "'";

                        using (var command = _context.Database.GetDbConnection().CreateCommand())
                        {
                            command.CommandText = $@"
                                SELECT HospNum, HaiStatus, HaiCount 
                                FROM tbPatientHAI 
                                WHERE HospNum IN ({hospNumsString})";

                            // Ensure connection is open
                            if (command.Connection.State != System.Data.ConnectionState.Open)
                            {
                                await command.Connection.OpenAsync();
                            }

                            // Execute query
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var hospNum = reader["HospNum"].ToString();
                                    var haiStatus = reader["HaiStatus"] != DBNull.Value && Convert.ToBoolean(reader["HaiStatus"]);
                                    var haiCount = reader["HaiCount"] != DBNull.Value ? Convert.ToInt32(reader["HaiCount"]) : 0;

                                    haiDict[hospNum] = (haiStatus, haiCount);
                                }
                            }
                        }
                    }

                    // Update patients with HAI data
                    foreach (var patient in patients)
                    {
                        if (haiDict.TryGetValue(patient.HospNum, out var hai))
                        {
                            patient.HaiStatus = hai.HaiStatus;
                            patient.HaiCount = hai.HaiCount;
                        }
                    }
                }

                // Sort the results as needed
                patients = patients
                    .OrderBy(p => p.AdmLocation)
                    .ThenBy(p => p.RoomID)
                    .ThenBy(p => p.LastName)
                    .ThenBy(p => p.FirstName)
                    .ToList();

                return View(patients);
            }
            catch (Exception ex)
            {
                // Add detailed error info
                _logger.LogError(ex, "Error in ICNPatientController.Index");

                ViewBag.ErrorMessage = "An error occurred while loading patient data.";
                ViewBag.ExceptionMessage = ex.Message;
                ViewBag.StackTrace = ex.StackTrace;

                if (ex.InnerException != null)
                {
                    ViewBag.InnerExceptionMessage = ex.InnerException.Message;
                    ViewBag.InnerStackTrace = ex.InnerException.StackTrace;
                }

                return View(new List<PatientViewModel>());
            }
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
                                             .FirstOrDefault(f => f.HospitalNumber == p.HospNum),
                                         GIInfectionForm = _context.GIInfectionChecklists
                                             .FirstOrDefault(f => f.HospitalNumber == p.HospNum),
                                         SSIForm = _context.SurgicalSiteInfectionChecklist
                                             .FirstOrDefault(f => f.HospitalNumber == p.HospNum)
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
            if (forms.GIInfectionForm != null) count++;
            if (forms.SSIForm != null) count++;
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
        //post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddConnectedDevice(AddConnectedDeviceViewModel model)
        {
            // Custom validation for DeviceClass
            if ((model.DeviceType == "CL" || model.DeviceType == "IUC") && string.IsNullOrEmpty(model.DeviceClass))
            {
                ModelState.AddModelError("DeviceClass", "The Device Class field is required for this device type.");
            }

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
                DeviceClass = (model.DeviceType == "CL" || model.DeviceType == "IUC") ? model.DeviceClass : null,
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
            // Custom validation for DeviceClass
            if ((model.DeviceType == "CL" || model.DeviceType == "IUC") && string.IsNullOrEmpty(model.DeviceClass))
            {
                ModelState.AddModelError("DeviceClass", "The Device Class field is required for this device type.");
            }

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
            device.DeviceClass = (device.DeviceType == "CL" || device.DeviceType == "IUC") ? model.DeviceClass : null;

            // Save changes
            await _context.SaveChangesAsync();

            // Redirect back to the connected devices list
            return RedirectToAction(nameof(ConnectedDevices), new { id = model.IdNum });
        }

        public IActionResult DeviceMonitoringReport()
        {
            return RedirectToAction("Index", "DeviceMonitoringReport");
        }

        // Action method to generate and return the PDF for download
        public IActionResult GenerateMechanicalVentilatorPDF()
        {
            return this.GenerateMechanicalVentilatorMonitoringPDF();
        }

        // Action method to generate and show the PDF in browser (print-friendly)
        public IActionResult PrintMechanicalVentilatorPDF()
        {
            var service = new MechanicalVentilatorReportService();
            var pdfData = service.GenerateMechanicalVentilatorReport();

            // Return with inline content disposition to display in browser
            return File(
                pdfData,
                "application/pdf",
                $"MechanicalVentilatorMonitoring_{DateTime.Now:yyyyMMdd}.pdf",
                true // Set inline disposition
            );
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

                //// Add explicit debug logging
                //Console.WriteLine($"Before update - HaiStatus: {patientMaster.HaiStatus}, HaiCount: {patientMaster.HaiCount}");

                //// Update HAI status
                //patientMaster.HaiStatus = true; // Set HAI status to true

                ////// Simply increment the count since it's a non-nullable int
                ////patientMaster.HaiCount += 1;

                //Console.WriteLine($"After update - HaiStatus: {patientMaster.HaiStatus}, HaiCount: {patientMaster.HaiCount}");

                //Console.WriteLine($"After update - HaiStatus: {patientMaster.HaiStatus}, HaiCount: {patientMaster.HaiCount}");

                //Console.WriteLine($"After update - HaiStatus: {patientMaster.HaiStatus}, HaiCount: {patientMaster.HaiCount}");

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

        // GET: HAI Checklist for Infection Control Nurse
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

            if (patientMaster == null)
            {
                // Redirect to details if patient doesn't exist
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
                                         //HaiStatus = m.HaiStatus,
                                         //HaiCount = m.HaiCount
                                     })
                                .FirstOrDefaultAsync();

            patientInfo.PatientName = $"{patientInfo.LastName}, {patientInfo.FirstName} {patientInfo.MiddleName}";

            // Get connected devices for this patient
            var connectedDevices = await _context.DeviceConnected
                .Where(d => d.HospNum == patient.HospNum)
                .ToListAsync();

            // Create device flags for conditional display of infection forms
            var deviceFlags = new HAIDeviceFlags
            {
                HasCentralLine = connectedDevices.Any(d => d.DeviceType == DeviceTypes.CentralLine),
                HasIndwellingUrinaryCatheter = connectedDevices.Any(d => d.DeviceType == DeviceTypes.IndwellingUrinaryCatheter),
                HasMechanicalVentilator = connectedDevices.Any(d => d.DeviceType == DeviceTypes.MechanicalVentilator)
                // Add more device flags as needed
            };

            // Get checklists and insertion forms info
            var clInsertionForm = await _context.Insertion
                                     .Where(i => i.HospitalNumber == patient.HospNum)
                                     .OrderByDescending(i => i.Id)
                                     .FirstOrDefaultAsync();

            var clMaintenanceChecklists = await _context.DailyCentralLineMaintenanceChecklists
                                           .Where(d => d.Patient == patientInfo.PatientName)
                                           .OrderByDescending(d => d.DateAndTimeOfMonitoring)
                                           .ToListAsync();

            // Get IUC insertion form info
            var iucInsertionForm = await _context.Insertion
                                     .Where(i => i.HospitalNumber == patient.HospNum && i.CatheterType.Contains("Urinary"))
                                     .OrderByDescending(i => i.Id)
                                     .FirstOrDefaultAsync();

            var iucMaintenanceChecklists = await _context.DailyCentralLineMaintenanceChecklists
                               .Where(d => d.Patient == patientInfo.PatientName)
                               .OrderByDescending(d => d.DateAndTimeOfMonitoring)
                               .ToListAsync();

            // Create the view model with patient info, device flags, and checklists
            var viewModel = new HAIChecklistViewModel
            {
                Patient = patientInfo,
                DeviceFlags = deviceFlags,
                CentralLineInsertionForm = clInsertionForm,
                CentralLineMaintenanceChecklists = clMaintenanceChecklists,
                IndwellingUrinaryCatheterInsertion = iucInsertionForm,
                IndwellingUrinaryCatheterMaintenanceChecklists = iucMaintenanceChecklists,

            };

            // Return the HAI checklist view
            return View(viewModel);
        }

        // Helper action to redirect to Insertions controller for Central Line insertion form
        public IActionResult CreateCentralLineInsertionForm(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            // Create TempData to store patient information for the Insertions controller
            TempData["PatientId"] = id;

            // Redirect to the create action in InsertionsController
            return RedirectToAction("Create", "Insertions");
        }

        // Helper action to redirect to DailyCentralLineMaintenanceChecklists controller
        public IActionResult CreateCentralLineMaintenance(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            // Create TempData to store patient information for the maintenance controller
            TempData["PatientId"] = id;

            // Redirect to the create action in DailyCentralLineMaintenanceChecklistsController
            return RedirectToAction("Create", "DailyCentralLineMaintenanceChecklists");
        }


        //HAILineList Reporting
        public async Task<IActionResult> GenerateHAILineListReport()
        {
            // Get the current logged-in user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Get the user's assigned areas
            var assignedAreas = user.AssignedArea?.Split(',').Select(a => a.Trim()).ToList() ?? new List<string>();

            // Get all HAI forms for patients in the user's assigned areas
            var haiEntries = new List<HAILineListViewModel>();

            // First, get all patients with HAI status in assigned areas
            var eligiblePatients = await (from p in _context.Patients
                                          join m in _context.PatientMasters on p.HospNum equals m.HospNum
                                          where //m.HaiStatus == true &&
                                                assignedAreas.Contains(p.AdmLocation) &&
                                                p.DeathDate == null // Exclude deceased patients
                                          select new
                                          {
                                              HospNum = p.HospNum,
                                              IdNum = p.IdNum,
                                              LastName = m.LastName,
                                              FirstName = m.FirstName,
                                              MiddleName = m.MiddleName,
                                              BirthDate = m.BirthDate,
                                              Age = p.Age,
                                              Sex = m.Sex,
                                              AdmDate = p.AdmDate,
                                              AdmLocation = p.AdmLocation,
                                              RoomID = p.RoomID,
                                              //HaiStatus = m.HaiStatus,
                                              //HaiCount = m.HaiCount
                                          })
                              .ToListAsync();

            foreach (var patient in eligiblePatients)
            {
                // Get classification and service info
                var classificationAndService = GetClassificationAndServiceInfo(patient.HospNum);

                // Get all forms for this patient and create separate entries

                // 1. Check Cardiovascular System Infection forms
                var cardioForms = await _context.CardiovascularSystemInfection
                    .Where(f => f.HospitalNumber == patient.HospNum)
                    .ToListAsync();

                foreach (var form in cardioForms)
                {
                    var centralLineInfo = GetCentralLineInfoForForm("VASC", patient.HospNum);
                    var mdroInfo = GetMDROInfoForForm("VASC", patient.HospNum, form.Id);
                    var outcomeInfo = GetPatientOutcome(patient.HospNum);

                    haiEntries.Add(new HAILineListViewModel
                    {
                        HospNum = patient.HospNum,
                        PatientName = $"{patient.LastName}, {patient.FirstName} {patient.MiddleName}",
                        BirthDate = patient.BirthDate,
                        Age = patient.Age,
                        Unit = patient.AdmLocation,
                        Room = patient.RoomID,
                        AdmissionDate = patient.AdmDate,
                        Classification = classificationAndService.Classification,
                        MainService = classificationAndService.MainService,
                        EventDate = form.DateOfEvent,
                        //HaiStatus = patient.HaiStatus,
                        HaiType = "VASC",
                        SpecificHaiClassification = form.Classification ?? "",
                        CLAccess = centralLineInfo,
                        IsMDRO = mdroInfo.IsMDRO,
                        MDROOrganism = mdroInfo.Organism,
                        Outcome = outcomeInfo.Status,
                        DischargeDate = outcomeInfo.Date,
                        DateCreated = form.DateCreated,
                    });
                }

                // 2. Check Soft Tissue Infection forms
                var sstForms = await _context.SSTInfectionModels
                    .Where(f => f.HospitalNumber == patient.HospNum)
                    .ToListAsync();

                foreach (var form in sstForms)
                {
                    var centralLineInfo = GetCentralLineInfoForForm("SST", patient.HospNum);
                    var mdroInfo = GetMDROInfoForForm("SST", patient.HospNum, form.SSTID);
                    var outcomeInfo = GetPatientOutcome(patient.HospNum);

                    haiEntries.Add(new HAILineListViewModel
                    {
                        HospNum = patient.HospNum,
                        PatientName = $"{patient.LastName}, {patient.FirstName} {patient.MiddleName}",
                        BirthDate = patient.BirthDate,
                        Age = patient.Age,
                        Unit = patient.AdmLocation,
                        Room = patient.RoomID,
                        AdmissionDate = patient.AdmDate,
                        Classification = classificationAndService.Classification,
                        MainService = classificationAndService.MainService,
                        EventDate = form.DateOfEvent,
                        //HaiStatus = patient.HaiStatus,
                        HaiType = "SST",
                        SpecificHaiClassification = form.Classification ?? "Soft Tissue",
                        CLAccess = centralLineInfo,
                        IsMDRO = mdroInfo.IsMDRO,
                        MDROOrganism = mdroInfo.Organism,
                        Outcome = outcomeInfo.Status,
                        DischargeDate = outcomeInfo.Date,
                        DateCreated = form.DateCreated,
                    });
                }

                // 3. Check Laboratory Confirmed BSI forms
                var lcbiForms = await _context.LaboratoryConfirmedBSI
                    .Where(f => f.HospitalNumber == patient.HospNum)
                    .ToListAsync();

                foreach (var form in lcbiForms)
                {
                    var mdroInfo = GetMDROInfoForForm("LCBI", patient.HospNum, form.Id);
                    var outcomeInfo = GetPatientOutcome(patient.HospNum);

                    haiEntries.Add(new HAILineListViewModel
                    {
                        HospNum = patient.HospNum,
                        PatientName = $"{patient.LastName}, {patient.FirstName} {patient.MiddleName}",
                        BirthDate = patient.BirthDate,
                        Age = patient.Age,
                        Unit = patient.AdmLocation,
                        Room = patient.RoomID,
                        AdmissionDate = patient.AdmDate,
                        Classification = form.Classification ?? classificationAndService.Classification,
                        MainService = form.MainService ?? classificationAndService.MainService,
                        EventDate = form.DateOfEvent,
                        //HaiStatus = patient.HaiStatus,
                        HaiType = "LCBI",
                        SpecificHaiClassification = form.TypeClass ?? "",
                        CLAccess = form.centralline ?? "No",
                        IsMDRO = mdroInfo.IsMDRO,
                        MDROOrganism = mdroInfo.Organism,
                        Outcome = outcomeInfo.Status,
                        DischargeDate = outcomeInfo.Date,
                        DateCreated = form.DateCreated,
                    });
                }

                // 4. Check UTI forms
                var utiForms = await _context.UTIModels
                    .Where(f => f.HospitalNumber == patient.HospNum)
                    .ToListAsync();

                foreach (var form in utiForms)
                {
                    var centralLineInfo = GetCentralLineInfoForForm("UTI", patient.HospNum);
                    var outcomeInfo = GetPatientOutcome(patient.HospNum);

                    bool isMdro = !string.IsNullOrEmpty(form.MDRO) &&
                        form.MDRO.Equals("Yes", StringComparison.OrdinalIgnoreCase);

                    haiEntries.Add(new HAILineListViewModel
                    {
                        HospNum = patient.HospNum,
                        PatientName = $"{patient.LastName}, {patient.FirstName} {patient.MiddleName}",
                        BirthDate = patient.BirthDate,
                        Age = patient.Age,
                        Unit = patient.AdmLocation,
                        Room = patient.RoomID,
                        AdmissionDate = patient.AdmDate,
                        Classification = form.Classification ?? classificationAndService.Classification,
                        MainService = form.MainService ?? classificationAndService.MainService,
                        EventDate = form.DateOfEvent,
                        //HaiStatus = patient.HaiStatus,
                        HaiType = "UTI",
                        SpecificHaiClassification = form.TypeClass ?? "",
                        CLAccess = centralLineInfo,
                        IsMDRO = isMdro,
                        MDROOrganism = isMdro ? ExtractOrganism(form.CultureResults) : "",
                        Outcome = outcomeInfo.Status,
                        DischargeDate = outcomeInfo.Date,
                        DateCreated = form.DateCreated,
                    });
                }

                // 5. Check SSI forms
                var ssiForms = await _context.SurgicalSiteInfectionChecklist
                    .Where(f => f.HospitalNumber == patient.HospNum)
                    .ToListAsync();

                foreach (var form in ssiForms)
                {
                    var centralLineInfo = GetCentralLineInfoForForm("SSI", patient.HospNum);
                    var outcomeInfo = GetPatientOutcome(patient.HospNum);

                    bool isMdro = !string.IsNullOrEmpty(form.MDRO) &&
                        form.MDRO.Equals("Yes", StringComparison.OrdinalIgnoreCase);

                    haiEntries.Add(new HAILineListViewModel
                    {
                        HospNum = patient.HospNum,
                        PatientName = $"{patient.LastName}, {patient.FirstName} {patient.MiddleName}",
                        BirthDate = patient.BirthDate,
                        Age = patient.Age,
                        Unit = patient.AdmLocation,
                        Room = patient.RoomID,
                        AdmissionDate = patient.AdmDate,
                        Classification = form.Classification ?? classificationAndService.Classification,
                        MainService = form.MainService ?? classificationAndService.MainService,
                        EventDate = form.DateOfEvent,
                        //HaiStatus = patient.HaiStatus,
                        HaiType = "SSI",
                        SpecificHaiClassification = form.TypeClass ?? "",
                        CLAccess = centralLineInfo,
                        IsMDRO = isMdro,
                        MDROOrganism = isMdro ? ExtractOrganism(form.CultureResults) : "",
                        Outcome = outcomeInfo.Status,
                        DischargeDate = outcomeInfo.Date,
                        DateCreated = form.DateCreated,
                        SurgeryDone = form.SurgeryDone
                    });
                }

                // 6. Check Pneumonia forms
                var pneuForms = await _context.Pneumonias
                    .Where(f => f.HospitalNumber == patient.HospNum)
                    .ToListAsync();

                foreach (var form in pneuForms)
                {
                    var centralLineInfo = GetCentralLineInfoForForm("PNEU", patient.HospNum);
                    var outcomeInfo = GetPatientOutcome(patient.HospNum);

                    bool isMdro = !string.IsNullOrEmpty(form.MDRO) &&
                        form.MDRO.Equals("Yes", StringComparison.OrdinalIgnoreCase);

                    haiEntries.Add(new HAILineListViewModel
                    {
                        HospNum = patient.HospNum,
                        PatientName = $"{patient.LastName}, {patient.FirstName} {patient.MiddleName}",
                        BirthDate = patient.BirthDate,
                        Age = patient.Age,
                        Unit = patient.AdmLocation,
                        Room = patient.RoomID,
                        AdmissionDate = patient.AdmDate,
                        Classification = form.Classification ?? classificationAndService.Classification,
                        MainService = form.MainService ?? classificationAndService.MainService,
                        EventDate = form.DateOfEvent,
                        //HaiStatus = patient.HaiStatus,
                        HaiType = "PNEU",
                        SpecificHaiClassification = form.TypeClass ?? "",
                        CLAccess = centralLineInfo,
                        IsMDRO = isMdro,
                        MDROOrganism = isMdro ? ExtractOrganism(form.CultureResults2_1) : "",
                        Outcome = outcomeInfo.Status,
                        DischargeDate = outcomeInfo.Date,
                        DateCreated = form.DateCreated,
                    });
                }

                // 7. Check VAE forms
                var vaeForms = await _context.VentilatorEventChecklists
                    .Where(f => f.HospitalNumber == patient.HospNum)
                    .ToListAsync();

                foreach (var form in vaeForms)
                {
                    var centralLineInfo = GetCentralLineInfoForForm("VAE", patient.HospNum);
                    var outcomeInfo = GetPatientOutcome(patient.HospNum);

                    haiEntries.Add(new HAILineListViewModel
                    {
                        HospNum = patient.HospNum,
                        PatientName = $"{patient.LastName}, {patient.FirstName} {patient.MiddleName}",
                        BirthDate = patient.BirthDate,
                        Age = patient.Age,
                        Unit = patient.AdmLocation,
                        Room = patient.RoomID,
                        AdmissionDate = patient.AdmDate,
                        Classification = form.Classification ?? classificationAndService.Classification,
                        MainService = form.MainService ?? classificationAndService.MainService,
                        EventDate = form.DateOfEvent,
                        //HaiStatus = patient.HaiStatus,
                        HaiType = "VAE",
                        SpecificHaiClassification = DetermineVAEClassification(form),
                        CLAccess = centralLineInfo,
                        IsMDRO = form.MDRO == "Yes",
                        MDROOrganism = form.MDRO == "Yes" ? (form.MDROOrganism ?? "") : "",
                        Outcome = outcomeInfo.Status,
                        DischargeDate = outcomeInfo.Date,
                        DateCreated = DateTime.Now,
                    });
                }

                // 8. Check USI forms
                var usiForms = await _context.Usi
                    .Where(f => f.HospitalNumber == patient.HospNum)
                    .ToListAsync();

                foreach (var form in usiForms)
                {
                    var centralLineInfo = GetCentralLineInfoForForm("USI", patient.HospNum);
                    var outcomeInfo = GetPatientOutcome(patient.HospNum);

                    bool isMdro = !string.IsNullOrEmpty(form.MDRO) &&
                        form.MDRO.Equals("Yes", StringComparison.OrdinalIgnoreCase);

                    haiEntries.Add(new HAILineListViewModel
                    {
                        HospNum = patient.HospNum,
                        PatientName = $"{patient.LastName}, {patient.FirstName} {patient.MiddleName}",
                        BirthDate = patient.BirthDate,
                        Age = patient.Age,
                        Unit = patient.AdmLocation,
                        Room = patient.RoomID,
                        AdmissionDate = patient.AdmDate,
                        Classification = form.Classification ?? classificationAndService.Classification,
                        MainService = form.MainService ?? classificationAndService.MainService,
                        EventDate = form.DateOfEvent,
                        //HaiStatus = patient.HaiStatus,
                        HaiType = "USI",
                        SpecificHaiClassification = form.TypeClass ?? "",
                        CLAccess = centralLineInfo,
                        IsMDRO = isMdro,
                        MDROOrganism = isMdro ? ExtractOrganism(form.CultureResults) : "",
                        Outcome = outcomeInfo.Status,
                        DischargeDate = outcomeInfo.Date,
                        DateCreated = form.DateCreated,
                    });
                }

                // After the existing forms processing, add these two new sections:

                //9.Check GI Infection forms
               var giForms = await _context.GIInfectionChecklists
                   .Where(f => f.HospitalNumber == patient.HospNum)
                   .ToListAsync();

                foreach (var form in giForms)
                {
                    var centralLineInfo = GetCentralLineInfoForForm("GI", patient.HospNum);
                    var outcomeInfo = GetPatientOutcome(patient.HospNum);

                    bool isMdro = !string.IsNullOrEmpty(form.MDRO) &&
                        form.MDRO.Equals("Yes", StringComparison.OrdinalIgnoreCase);

                    haiEntries.Add(new HAILineListViewModel
                    {
                        HospNum = patient.HospNum,
                        PatientName = $"{patient.LastName}, {patient.FirstName} {patient.MiddleName}",
                        BirthDate = patient.BirthDate,
                        Age = patient.Age,
                        Unit = patient.AdmLocation,
                        Room = patient.RoomID,
                        AdmissionDate = patient.AdmDate,
                        Classification = form.Classification ?? classificationAndService.Classification,
                        MainService = form.MainService ?? classificationAndService.MainService,
                        EventDate = form.DateOfEvent,
                        //HaiStatus = patient.HaiStatus,
                        HaiType = "GI",
                        SpecificHaiClassification = form.TypeClass ?? "",
                        CLAccess = centralLineInfo,
                        IsMDRO = isMdro,
                        MDROOrganism = form.MDRO == "Yes" ? (form.MDROOrganism ?? "") : "",
                        Outcome = outcomeInfo.Status,
                        DischargeDate = outcomeInfo.Date,
                        DateCreated = form.DateCreated,
                    });
                }

                // 10. Check Pediatric VAE forms
                var pvaeForms = await _context.PediatricVAEChecklist
                    .Where(f => f.HospitalNumber == patient.HospNum)
                    .ToListAsync();

                foreach (var form in pvaeForms)
                {
                    var centralLineInfo = GetCentralLineInfoForForm("PVAE", patient.HospNum);
                    var outcomeInfo = GetPatientOutcome(patient.HospNum);

                    haiEntries.Add(new HAILineListViewModel
                    {
                        HospNum = patient.HospNum,
                        PatientName = $"{patient.LastName}, {patient.FirstName} {patient.MiddleName}",
                        BirthDate = patient.BirthDate,
                        Age = patient.Age,
                        Unit = patient.AdmLocation,
                        Room = patient.RoomID,
                        AdmissionDate = patient.AdmDate,
                        Classification = form.Classification ?? classificationAndService.Classification,
                        MainService = form.MainService ?? classificationAndService.MainService,
                        EventDate = form.DateOfEvent,
                        //HaiStatus = patient.HaiStatus,
                        HaiType = "PVAE",
                        SpecificHaiClassification = form.TypeClass ?? "",
                        CLAccess = centralLineInfo,
                        IsMDRO = form.MDRO == "Yes",
                        MDROOrganism = form.MDRO == "Yes" ? (form.MDROOrganism ?? "") : "",
                        Outcome = outcomeInfo.Status,
                        DischargeDate = outcomeInfo.Date,
                        DateCreated = form.DateCreated,
                    });
                }

            }

            // Sort all entries
            haiEntries = haiEntries
                .OrderBy(e => e.Unit)
                .ThenBy(e => e.Room)
                .ThenBy(e => e.PatientName)
                .ThenBy(e => e.HaiType)
                .ToList();

            // Create the Excel package
            using (var package = new ExcelPackage())
            {
                // Add a worksheet
                var worksheet = package.Workbook.Worksheets.Add("HAI Line List");

                // Add a title row for the report
                worksheet.Cells[1, 1].Value = "HEALTHCARE-ASSOCIATED INFECTION (HAI) LINE LIST REPORT";
                using (var titleRange = worksheet.Cells[1, 1, 1, 19])
                {
                    titleRange.Merge = true;
                    titleRange.Style.Font.Bold = true;
                    titleRange.Style.Font.Size = 14;
                    titleRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    titleRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    titleRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(79, 129, 189));
                    titleRange.Style.Font.Color.SetColor(System.Drawing.Color.White);
                }

                // Add report date
                worksheet.Cells[2, 1].Value = $"Report Generated: {DateTime.Now:MMMM dd, yyyy hh:mm tt}";
                using (var dateRange = worksheet.Cells[2, 1, 2, 18])
                {
                    dateRange.Merge = true;
                    dateRange.Style.Font.Italic = true;
                    dateRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                }

                // Add assigned areas
                worksheet.Cells[3, 1].Value = $"Areas Covered: {string.Join(", ", assignedAreas)}";
                using (var areasRange = worksheet.Cells[3, 1, 3, 18])
                {
                    areasRange.Merge = true;
                    areasRange.Style.Font.Italic = true;
                }

                // Empty row for spacing
                int headerRow = 5;

                // Add headers
                worksheet.Cells[headerRow, 1].Value = "HOSPITAL NUMBER";
                worksheet.Cells[headerRow, 2].Value = "PATIENT NAME";
                worksheet.Cells[headerRow, 3].Value = "BIRTHDAY";
                worksheet.Cells[headerRow, 4].Value = "AGE";
                worksheet.Cells[headerRow, 5].Value = "UNIT";
                worksheet.Cells[headerRow, 6].Value = "ROOM";
                worksheet.Cells[headerRow, 7].Value = "ADMISSION";
                worksheet.Cells[headerRow, 8].Value = "PATIENT CLASSIFICATION";
                worksheet.Cells[headerRow, 9].Value = "MAIN SERVICE";
                worksheet.Cells[headerRow, 10].Value = "EVENT DATE";
                worksheet.Cells[headerRow, 11].Value = "HAI";
                worksheet.Cells[headerRow, 12].Value = "HAI TYPE";
                worksheet.Cells[headerRow, 13].Value = "SPECIFIC HAI CLASSIFICATION";
                worksheet.Cells[headerRow, 14].Value = "CL ACCESS";
                worksheet.Cells[headerRow, 15].Value = "MDRO";
                worksheet.Cells[headerRow, 16].Value = "ORGANISM (if MDRO)";
                worksheet.Cells[headerRow, 17].Value = "OUTCOME";
                worksheet.Cells[headerRow, 18].Value = "DATE";
                worksheet.Cells[headerRow, 19].Value = "SURGERY DONE";

                // Style the header row
                using (var range = worksheet.Cells[headerRow, 1, headerRow, 19])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(79, 129, 189)); // Blue header
                    range.Style.Font.Color.SetColor(System.Drawing.Color.White);
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                }

                // Add data
                int row = headerRow + 1;
                bool hasData = false;
                foreach (var entry in haiEntries)
                {
                    hasData = true;
                    worksheet.Cells[row, 1].Value = entry.HospNum;
                    worksheet.Cells[row, 2].Value = entry.PatientName;
                    worksheet.Cells[row, 3].Value = entry.BirthDate;
                    worksheet.Cells[row, 3].Style.Numberformat.Format = "dd-MMM-yy";
                    worksheet.Cells[row, 4].Value = entry.Age;
                    worksheet.Cells[row, 5].Value = entry.Unit;
                    worksheet.Cells[row, 6].Value = entry.Room;
                    worksheet.Cells[row, 7].Value = entry.AdmissionDate;
                    worksheet.Cells[row, 7].Style.Numberformat.Format = "dd-MMM-yy";
                    worksheet.Cells[row, 8].Value = entry.Classification;
                    worksheet.Cells[row, 9].Value = entry.MainService;
                    worksheet.Cells[row, 10].Value = entry.EventDate;
                    worksheet.Cells[row, 10].Style.Numberformat.Format = "dd-MMM-yy";
                    worksheet.Cells[row, 11].Value = entry.HaiStatus ? "Yes" : "No";
                    worksheet.Cells[row, 12].Value = entry.HaiType;
                    worksheet.Cells[row, 13].Value = entry.SpecificHaiClassification;
                    worksheet.Cells[row, 14].Value = entry.CLAccess;
                    worksheet.Cells[row, 15].Value = entry.IsMDRO ? "Yes" : "No";
                    worksheet.Cells[row, 16].Value = entry.MDROOrganism;
                    worksheet.Cells[row, 17].Value = entry.Outcome;
                    worksheet.Cells[row, 18].Value = entry.DateCreated;
                    worksheet.Cells[row, 18].Style.Numberformat.Format = "dd-MMM-yy";
                    worksheet.Cells[row, 19].Value = entry.HaiType == "SSI" ? entry.SurgeryDone : "NA";

                    // Apply alternate row coloring for better readability
                    if (row % 2 == 0)
                    {
                        using (var range = worksheet.Cells[row, 1, row, 19])
                        {
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(240, 240, 240));
                        }
                    }

                    row++;
                }

                // If no data, add a notification row
                if (!hasData)
                {
                    worksheet.Cells[row, 1].Value = "No HAI patients found in your assigned areas.";
                    using (var range = worksheet.Cells[row, 1, row, 19])
                    {
                        range.Merge = true;
                        range.Style.Font.Italic = true;
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                }

                // Apply borders and styling to data cells
                if (hasData)
                {
                    using (var range = worksheet.Cells[headerRow, 1, row - 1, 19])
                    {
                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    }
                }

                // Add summary statistics at the bottom
                row += 2; // Add some space
                worksheet.Cells[row, 1].Value = "SUMMARY STATISTICS";
                using (var summaryHeaderRange = worksheet.Cells[row, 1, row, 19])
                {
                    summaryHeaderRange.Merge = true;
                    summaryHeaderRange.Style.Font.Bold = true;
                    summaryHeaderRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    summaryHeaderRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    summaryHeaderRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                }

                row++;
                worksheet.Cells[row, 1].Value = "Total HAI Entries:";
                worksheet.Cells[row, 2].Value = haiEntries.Count;

                row++;
                worksheet.Cells[row, 1].Value = "Total Unique Patients:";
                worksheet.Cells[row, 2].Value = haiEntries.Select(e => e.HospNum).Distinct().Count();

                // Group by HAI Type
                row++;
                worksheet.Cells[row, 1].Value = "By HAI Type:";
                using (var typeHeaderRange = worksheet.Cells[row, 1, row, 2])
                {
                    typeHeaderRange.Merge = true;
                    typeHeaderRange.Style.Font.Bold = true;
                }

                var haiTypeGroups = haiEntries
                    .GroupBy(p => p.HaiType)
                    .Where(g => !string.IsNullOrEmpty(g.Key))
                    .OrderByDescending(g => g.Count());

                row++;
                foreach (var group in haiTypeGroups)
                {
                    worksheet.Cells[row, 2].Value = group.Key;
                    worksheet.Cells[row, 3].Value = group.Count();
                    row++;
                }

                // Group by Unit
                row++;
                worksheet.Cells[row, 1].Value = "By Unit:";
                using (var unitHeaderRange = worksheet.Cells[row, 1, row, 2])
                {
                    unitHeaderRange.Merge = true;
                    unitHeaderRange.Style.Font.Bold = true;
                }

                var unitGroups = haiEntries
                    .GroupBy(p => p.Unit)
                    .Where(g => !string.IsNullOrEmpty(g.Key))
                    .OrderByDescending(g => g.Count());

                row++;
                foreach (var group in unitGroups)
                {
                    worksheet.Cells[row, 2].Value = group.Key;
                    worksheet.Cells[row, 3].Value = group.Count();
                    row++;
                }

                // Group by MDRO Status
                row++;
                worksheet.Cells[row, 1].Value = "MDRO Status:";
                using (var mdroHeaderRange = worksheet.Cells[row, 1, row, 2])
                {
                    mdroHeaderRange.Merge = true;
                    mdroHeaderRange.Style.Font.Bold = true;
                }

                var mdroCount = haiEntries.Count(p => p.IsMDRO);
                worksheet.Cells[row + 1, 2].Value = "MDRO Positive";
                worksheet.Cells[row + 1, 3].Value = mdroCount;
                worksheet.Cells[row + 2, 2].Value = "MDRO Negative";
                worksheet.Cells[row + 2, 3].Value = haiEntries.Count - mdroCount;

                // Auto-fit columns
                worksheet.Cells.AutoFitColumns();

                // Set column widths for better readability
                worksheet.Column(2).Width = 25; // Patient Name
                worksheet.Column(8).Width = 22; // Patient Classification
                worksheet.Column(9).Width = 20; // Main Service
                worksheet.Column(13).Width = 30; // Specific HAI Classification
                worksheet.Column(16).Width = 20; // MDRO Organism

                // Set print settings
                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                worksheet.PrinterSettings.FitToPage = true;
                worksheet.PrinterSettings.FitToWidth = 1;
                worksheet.PrinterSettings.FitToHeight = 0;

                // Add header and footer
                worksheet.HeaderFooter.OddHeader.LeftAlignedText = "HAI Line List Report";
                worksheet.HeaderFooter.OddHeader.RightAlignedText = $"Generated: {DateTime.Now:yyyy-MM-dd}";
                worksheet.HeaderFooter.OddFooter.RightAlignedText = "Page &P of &N";
                worksheet.HeaderFooter.OddFooter.LeftAlignedText = $"User: {user.UserName}";

                // Generate the report as a byte array
                var reportBytes = package.GetAsByteArray();

                // Return the Excel file
                return File(
                    reportBytes,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"HAI_Line_List_{DateTime.Now:yyyyMMdd}.xlsx");
            }
        }

        // Helper methods for retrieving patient and HAI information

        // New helper method to get central line info for a specific form type
        private string GetCentralLineInfoForForm(string formType, string hospNum)
        {
            // For LCBI forms, the central line info is stored directly
            if (formType == "LCBI")
            {
                var lcbiForm = _context.LaboratoryConfirmedBSI
                    .FirstOrDefault(f => f.HospitalNumber == hospNum);

                if (lcbiForm != null)
                {
                    return lcbiForm.centralline ?? "No";
                }
            }

            // For other form types, we need to check if there's any associated central line
            // This could be expanded with more specific logic per form type
            return "No";
        }

        // New helper method to get MDRO info for a specific form type and ID
        private (bool IsMDRO, string Organism) GetMDROInfoForForm(string formType, string hospNum, int formId)
        {
            switch (formType)
            {
                case "VASC":
                    var cardioForm = _context.CardiovascularSystemInfection
                        .FirstOrDefault(f => f.HospitalNumber == hospNum && f.Id == formId);
                    //if (cardioForm != null && cardioForm.MDRO)
                    //{
                    //    return (true, cardioForm.MDROOrganism ?? "Not specified");
                    //}
                    break;

                case "SST":
                    var sstForm = _context.SSTInfectionModels
                        .FirstOrDefault(f => f.HospitalNumber == hospNum && f.SSTID == formId);
                    if (sstForm != null && sstForm.MDRO)
                    {
                        var organism = "";
                        if (!string.IsNullOrEmpty(sstForm.SkinCultureResults))
                            organism = ExtractOrganism(sstForm.SkinCultureResults);
                        else if (!string.IsNullOrEmpty(sstForm.STCultureResults))
                            organism = ExtractOrganism(sstForm.STCultureResults);
                        else if (!string.IsNullOrEmpty(sstForm.DecubitusCultureResults))
                            organism = ExtractOrganism(sstForm.DecubitusCultureResults);

                        return (true, organism);
                    }
                    break;

                case "LCBI":
                    var lcbiForm = _context.LaboratoryConfirmedBSI
                        .FirstOrDefault(f => f.HospitalNumber == hospNum && f.Id == formId);
                    if (lcbiForm != null)
                    {
                        bool isMdro = !string.IsNullOrEmpty(lcbiForm.MDRO) &&
                            lcbiForm.MDRO.Equals("Yes", StringComparison.OrdinalIgnoreCase);

                        return (isMdro, isMdro ? ExtractOrganism(lcbiForm.CultureResults ?? "") : "");
                    }
                    break;
            }

            // Default return for no MDRO
            return (false, "");
        }

        // Helper method to extract organism information from culture results
        private string ExtractOrganism(string cultureResults)
        {
            if (string.IsNullOrEmpty(cultureResults))
                return "";

            // This is a simplified extraction - adjust based on how culture results are formatted
            // For example, if culture results contain "Organism: E. coli", extract "E. coli"
            if (cultureResults.Contains("Organism:"))
            {
                var parts = cultureResults.Split(new[] { "Organism:" }, StringSplitOptions.None);
                if (parts.Length > 1)
                {
                    var organism = parts[1].Trim();
                    // Further clean up if needed
                    if (organism.Contains("\n"))
                        organism = organism.Split('\n')[0].Trim();
                    return organism;
                }
            }

            // If no specific format, return the first 50 characters or less
            return cultureResults.Length <= 50 ? cultureResults : cultureResults.Substring(0, 50) + "...";
        }

        // Helper method to get patient outcome information
        private (string Status, DateTime? Date) GetPatientOutcome(string hospNum)
        {
            var patient = _context.Patients
                .FirstOrDefault(p => p.HospNum == hospNum);

            if (patient == null)
                return ("Unknown", null);

            if (patient.DeathDate.HasValue)
                return ("Expired", patient.DeathDate);

            //if (patient.DischargeDate.HasValue)
            //    return ("Discharged", patient.DischargeDate);

            // Patient is still admitted
            return ("Still Admitted", null);
        }

        // Helper method to get patient classification and service information
        private (string Classification, string MainService) GetClassificationAndServiceInfo(string hospNum)
        {
            // Default values
            string classification = "Regular";
            string mainService = "General";

            // Try to get classification from Patient model if available
            var patient = _context.Patients
                .FirstOrDefault(p => p.HospNum == hospNum);

            if (patient != null && !string.IsNullOrEmpty(patient.AdmType))
            {
                classification = patient.AdmType;
            }

            // Check if we have any VAE forms to get main service
            var vaeForm = _context.VentilatorEventChecklists
                .Where(v => v.HospitalNumber == hospNum)
                .OrderByDescending(v => v.DateOfEvent)
                .FirstOrDefault();

            if (vaeForm != null && !string.IsNullOrEmpty(vaeForm.MainService))
            {
                mainService = vaeForm.MainService;
            }

            // Check other forms as fallback for main service
            if (mainService == "General")
            {
                // Try to find main service in other form types
                // Example with LCBI forms
                var lcbiForm = _context.LaboratoryConfirmedBSI
                    .Where(l => l.HospitalNumber == hospNum)
                    .OrderByDescending(l => l.DateOfEvent)
                    .FirstOrDefault();

                if (lcbiForm != null && !string.IsNullOrEmpty(lcbiForm.MainService))
                {
                    mainService = lcbiForm.MainService;
                }
            }

            return (classification, mainService);
        }

        // Helper method to determine VAE classification
        // Helper method to determine VAE classification
        private string DetermineVAEClassification(VentilatorEventChecklist form)
        {
            // TypeClass already contains the VAE classification (PVAP, IVAC, or VAC)
            if (!string.IsNullOrEmpty(form.TypeClass))
            {
                return form.TypeClass;
            }

            // As a fallback, we can determine classification based on the checkboxes
            // Check if we have PVAP criteria met
            bool hasPvap = form.Pvap1Endo || form.Pvap1Lung || form.Pvap1Bronch || form.Pvap1Specimen ||
                          form.Pvap2Sputum || form.Pvap2Endo || form.Pvap2Lung || form.Pvap2Bronch || form.Pvap2Specimen ||
                          form.Pvap3Organism || form.Pvap3Lung || form.Pvap3Legionella || form.Pvap3Viral;

            // Check if we have IVAC criteria met
            bool hasIvac = form.IVac1 || form.IVac2 || form.IVac3;

            // Check if we have VAC criteria met
            bool hasVac = form.Vac1 || form.Vac2;

            // Determine classification based on criteria hierarchy
            if (hasVac && hasIvac && hasPvap)
                return "PVAP";
            else if (hasVac && hasIvac)
                return "IVAC";
            else if (hasVac)
                return "VAC";
            else
                return "Undetermined";
        }

        // remove comment when PVAE is done
        //private string DeterminePVAEClassification(PediatricVAEChecklist form)
        //{
        //    // If TypeClass already contains the classification, use it
        //    if (!string.IsNullOrEmpty(form.TypeClass))
        //    {
        //        return form.TypeClass;
        //    }

        //    // As a fallback, determine classification based on fields
        //    // This should be adjusted based on your specific model structure
        //    // The below is a placeholder implementation
        //    bool hasPVAP = form.PVAP || !string.IsNullOrEmpty(form.PVAPDetails);
        //    bool hasVAC = form.VAC || !string.IsNullOrEmpty(form.VACDetails);

        //    if (hasPVAP)
        //        return "PVAP";
        //    else if (hasVAC)
        //        return "VAC";
        //    else
        //        return "Undetermined";
        //}
    }
    }