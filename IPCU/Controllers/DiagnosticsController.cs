using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPCU.Controllers
    {
        public class DiagnosticsController : Controller
        {
            private readonly ApplicationDbContext _context;
            private readonly PatientDbContext _patientContext;
            private readonly ILogger<DiagnosticsController> _logger;

            public DiagnosticsController(ApplicationDbContext context, PatientDbContext patientContext, ILogger<DiagnosticsController> logger)
            {
                _patientContext = patientContext;
                _context = context;
                _logger = logger;
            }

        // GET: Diagnostics
        // Modify the Index action in DiagnosticsController.cs to ensure hospNum is properly saved to ViewBag
        // GET: Diagnostics
        // GET: Diagnostics
        // GET: Diagnostics
        public async Task<IActionResult> Index(string hospNum)
        {
            // Add debug logging
            _logger.LogInformation("Index action called with hospNum: {HospNum}", hospNum);

            if (string.IsNullOrEmpty(hospNum))
            {
                _logger.LogWarning("Hospital number is null or empty, redirecting to Home");
                return RedirectToAction("Index", "Home");
            }

            try
            {
                // First get patient information from _patientContext
                string patientName = string.Empty;

                // Try to get from PatientMasters in ApplicationDbContext first
                var patient = await _patientContext.PatientMasters.FirstOrDefaultAsync(p => p.HospNum == hospNum);

                if (patient != null)
                {
                    patientName = $"{patient.FirstName} {patient.LastName}";
                    _logger.LogInformation("Found patient in PatientMasters: {PatientName}", patientName);
                }
                else
                {
                    // Fall back to PatientDbContext for tbmaster table
                    try
                    {
                        using (var command = _patientContext.Database.GetDbConnection().CreateCommand())
                        {
                            command.CommandText = @"
                        SELECT LastName, FirstName 
                        FROM tbmaster 
                        WHERE HospNum = @HospNum";

                            var parameter = command.CreateParameter();
                            parameter.ParameterName = "@HospNum";
                            parameter.Value = hospNum;
                            command.Parameters.Add(parameter);

                            if (command.Connection.State != System.Data.ConnectionState.Open)
                            {
                                await command.Connection.OpenAsync();
                            }

                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                if (await reader.ReadAsync())
                                {
                                    var lastName = reader["LastName"]?.ToString() ?? string.Empty;
                                    var firstName = reader["FirstName"]?.ToString() ?? string.Empty;
                                    patientName = $"{firstName} {lastName}";
                                    _logger.LogInformation("Found patient in tbmaster: {PatientName}", patientName);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error querying tbmaster for hospNum: {HospNum}", hospNum);
                    }
                }

                if (string.IsNullOrEmpty(patientName))
                {
                    _logger.LogWarning("Patient not found for hospNum: {HospNum}", hospNum);
                    patientName = $"Patient #{hospNum}";
                }

                // Get diagnostics using raw SQL
                List<Diagnostics> diagnostics = new List<Diagnostics>();

                try
                {
                    // First get all diagnostic IDs for this hospital number
                    var diagIds = new List<int>();
                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = "SELECT DiagId FROM tbdiagnostics WHERE HospNum = @HospNum";
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = "@HospNum";
                        parameter.Value = hospNum;
                        command.Parameters.Add(parameter);

                        if (command.Connection.State != System.Data.ConnectionState.Open)
                        {
                            await command.Connection.OpenAsync();
                        }

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                diagIds.Add(reader.GetInt32(0));
                            }
                        }
                    }

                    _logger.LogInformation("Found {DiagCount} diagnostic records for hospNum: {HospNum}",
                        diagIds.Count, hospNum);

                    if (diagIds.Any())
                    {
                        // Now fetch the full diagnostics with treatments and antibiotics
                        // We'll do this in two steps to avoid complex joins in raw SQL

                        // Step 1: Get all diagnostics
                        var diagnosticsDict = new Dictionary<int, Diagnostics>();
                        using (var command = _context.Database.GetDbConnection().CreateCommand())
                        {
                            command.CommandText = "SELECT DiagId, DateCollection, SourceSite, IsolateFindingsResult FROM tbdiagnostics WHERE DiagId IN (" +
                                                 string.Join(",", diagIds) + ") ORDER BY DateCollection DESC";

                            if (command.Connection.State != System.Data.ConnectionState.Open)
                            {
                                await command.Connection.OpenAsync();
                            }

                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var diag = new Diagnostics
                                    {
                                        DiagId = reader.GetInt32(0),
                                        DateCollection = reader.GetDateTime(1),
                                        SourceSite = reader.IsDBNull(2) ? null : reader.GetString(2),
                                        IsolateFindingsResult = reader.IsDBNull(3) ? null : reader.GetString(3),
                                        HospNum = hospNum,
                                        Treatments = new List<DiagnosticsTreatment>()
                                    };
                                    diagnosticsDict.Add(diag.DiagId, diag);
                                }
                            }
                        }

                        // Step 2: Get all treatments with antibiotics for these diagnostics
                        using (var command = _context.Database.GetDbConnection().CreateCommand())
                        {
                            command.CommandText = @"
                        SELECT dt.DiagId, dt.Id, dt.AntibioticId, a.Name, a.DateAdded 
                        FROM tbdiagnosticstreatments dt
                        INNER JOIN tbantibiotics a ON dt.AntibioticId = a.AntibioticId
                        WHERE dt.DiagId IN (" + string.Join(",", diagIds) + ")";

                            if (command.Connection.State != System.Data.ConnectionState.Open)
                            {
                                await command.Connection.OpenAsync();
                            }

                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    var diagId = reader.GetInt32(0);
                                    if (diagnosticsDict.TryGetValue(diagId, out var diag))
                                    {
                                        var treatment = new DiagnosticsTreatment
                                        {
                                            Id = reader.GetInt32(1),
                                            DiagId = diagId,
                                            AntibioticId = reader.GetInt32(2),
                                            Antibiotic = new Antibiotic
                                            {
                                                AntibioticId = reader.GetInt32(2),
                                                Name = reader.GetString(3),
                                                DateAdded = reader.GetDateTime(4)
                                            }
                                        };
                                        diag.Treatments.Add(treatment);
                                    }
                                }
                            }
                        }

                        diagnostics = diagnosticsDict.Values.ToList();
                        _logger.LogInformation("Successfully loaded {DiagCount} diagnostics with treatments",
                            diagnostics.Count);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching diagnostics for hospNum: {HospNum}", hospNum);
                }

                ViewBag.PatientName = patientName;
                ViewBag.HospNum = hospNum;
                _logger.LogInformation("Setting ViewBag.HospNum to: {HospNum}", hospNum);

                // Find the last antibiotic update date
                var latestTreatment = diagnostics
                    .SelectMany(d => d.Treatments ?? new List<DiagnosticsTreatment>())
                    .OrderByDescending(dt => dt.Diagnostic?.DateCollection)
                    .FirstOrDefault();

                ViewBag.LastAntibioticUpdate = latestTreatment?.Diagnostic?.DateCollection;

                return View(diagnostics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DiagnosticsController.Index for hospital number {HospNum}: {ErrorMessage}",
                    hospNum, ex.Message);

                ViewBag.ErrorMessage = "An error occurred while loading diagnostic data.";
                ViewBag.ExceptionMessage = ex.Message;
                ViewBag.StackTrace = ex.StackTrace;
                ViewBag.HospNum = hospNum;

                return View(new List<Diagnostics>());
            }
        }

        // Modify the Create action to handle cases where hospNum is not provided
        public IActionResult Create(string hospNum)
        {
            _logger.LogInformation("Create action called with hospNum: {HospNum}", hospNum);

            if (string.IsNullOrEmpty(hospNum))
            {
                _logger.LogWarning("Hospital number is null or empty for Create action");
                TempData["ErrorMessage"] = "Hospital number is required to create a diagnostic record.";
                return RedirectToAction("Index", "Patient");
            }

            try
            {
                string patientName = string.Empty;

                // First try to get patient from PatientMasters as this seems more reliable
                var patient = _patientContext.PatientMasters.FirstOrDefault(p => p.HospNum == hospNum);

                if (patient != null)
                {
                    patientName = $"{patient.FirstName} {patient.LastName}";
                }
                else
                {
                    // Fallback to raw SQL, but use the correct database/connection context
                    // Make sure this connection has access to tbmaster table
                    try
                    {
                        using (var command = _patientContext.Database.GetDbConnection().CreateCommand())
                        {
                            command.CommandText = @"
                    SELECT TOP 1 LastName, FirstName 
                    FROM tbmaster
                    WHERE HospNum = @HospNum";

                            // Add parameter
                            var parameter = command.CreateParameter();
                            parameter.ParameterName = "@HospNum";
                            parameter.Value = hospNum;
                            command.Parameters.Add(parameter);

                            // Ensure connection is open
                            if (command.Connection.State != System.Data.ConnectionState.Open)
                            {
                                command.Connection.Open();
                            }

                            // Execute query
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    var lastName = reader["LastName"]?.ToString() ?? string.Empty;
                                    var firstName = reader["FirstName"]?.ToString() ?? string.Empty;
                                    patientName = $"{firstName} {lastName}";
                                }
                            }
                        }
                    }
                    catch (Exception sqlEx)
                    {
                        _logger.LogWarning(sqlEx, "Failed to query tbmaster table: {Message}", sqlEx.Message);
                        patientName = "Unknown Patient";
                    }
                }

                if (string.IsNullOrEmpty(patientName))
                {
                    patientName = "Unknown Patient";
                }

                ViewBag.PatientName = patientName;
                ViewBag.HospNum = hospNum;
                _logger.LogInformation("Setting ViewBag.HospNum to: {HospNum} in Create action", hospNum);

                return View(new Diagnostics
                {
                    HospNum = hospNum,
                    DateCollection = DateTime.Now,
                    Treatments = new List<DiagnosticsTreatment>()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in DiagnosticsController.Create for hospital number {HospNum}: {ErrorMessage}", hospNum, ex.Message);

                ViewBag.ErrorMessage = "An error occurred while preparing the creation form.";
                ViewBag.ExceptionMessage = ex.Message;
                ViewBag.StackTrace = ex.StackTrace;

                // Create a minimal model to display the form
                ViewBag.HospNum = hospNum;
                return View(new Diagnostics
                {
                    HospNum = hospNum,
                    DateCollection = DateTime.Now
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DateCollection,SourceSite,IsolateFindingsResult,HospNum")] Diagnostics diagnostics, string action)
        {
            // Remove any potential existing errors about Treatments
            if (ModelState.ContainsKey("Treatments"))
            {
                ModelState.Remove("Treatments");
            }

            // Important - Remove Patient validation error if it exists
            if (ModelState.ContainsKey("Patient"))
            {
                ModelState.Remove("Patient");
            }

            // Debug info to log the state of the model
            _logger.LogInformation("Diagnostic creation attempted for HospNum: {HospNum}", diagnostics.HospNum);

            // Check ModelState validity
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState is invalid for Create Diagnostic");
                foreach (var state in ModelState)
                {
                    if (state.Value.Errors.Count > 0)
                    {
                        _logger.LogWarning("Error in {Field}: {ErrorMessage}", state.Key, state.Value.Errors[0].ErrorMessage);
                    }
                }
            }

            // Make sure HospNum is not null or empty
            if (string.IsNullOrEmpty(diagnostics.HospNum))
            {
                _logger.LogError("HospNum is null or empty in Create Diagnostic");
                ModelState.AddModelError("HospNum", "Hospital number cannot be empty");

                // Set a default patient name for the view
                ViewBag.PatientName = "Unknown Patient";
                ViewBag.HospNum = diagnostics.HospNum;
                return View(diagnostics);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Initialize the treatments collection if null
                    if (diagnostics.Treatments == null)
                    {
                        diagnostics.Treatments = new List<DiagnosticsTreatment>();
                    }

                    _context.Add(diagnostics);
                    _logger.LogInformation("About to save changes for new diagnostic record: {HospNum}", diagnostics.HospNum);
                    var result = await _context.SaveChangesAsync();
                    _logger.LogInformation("SaveChanges result: {Result}", result);

                    // Check which button was clicked
                    if (action == "BackToIndex")
                    {
                        // Redirect back to the index if the "Save and Return to List" button was clicked
                        return RedirectToAction(nameof(Index), new { hospNum = diagnostics.HospNum });
                    }
                    else
                    {
                        // Default behavior: redirect to add antibiotics
                        return RedirectToAction(nameof(AddAntibiotics), new { id = diagnostics.DiagId });
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving diagnostic record for {HospNum}", diagnostics.HospNum);

                    if (ex.InnerException != null)
                    {
                        ModelState.AddModelError("", $"Inner exception: {ex.InnerException.Message}");
                    }
                    ModelState.AddModelError("", $"Unable to save changes: {ex.Message}");
                }
            }

            // If we got this far, something failed, redisplay form
            string patientName = string.Empty;

            try
            {
                // Use raw SQL to get patient information
                using (var command = _patientContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
        SELECT LastName, FirstName 
        FROM tbmaster 
        WHERE HospNum = @HospNum";

                    // Add parameter
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@HospNum";
                    parameter.Value = diagnostics.HospNum ?? string.Empty; // Safeguard against null
                    command.Parameters.Add(parameter);

                    // Ensure connection is open
                    if (command.Connection.State != System.Data.ConnectionState.Open)
                    {
                        command.Connection.Open();
                    }

                    // Execute query
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var lastName = reader["LastName"]?.ToString() ?? string.Empty;
                            var firstName = reader["FirstName"]?.ToString() ?? string.Empty;
                            patientName = $"{firstName} {lastName}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving patient info for {HospNum}", diagnostics.HospNum);
                patientName = "Unknown Patient";
            }

            // If no patient name found, try PatientMasters as fallback
            if (string.IsNullOrEmpty(patientName))
            {
                try
                {
                    var patient = _patientContext.PatientMasters.FirstOrDefault(p => p.HospNum == diagnostics.HospNum);
                    if (patient != null)
                    {
                        patientName = $"{patient.FirstName} {patient.LastName}";
                    }
                    else
                    {
                        patientName = "Unknown Patient";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error retrieving patient from PatientMasters for {HospNum}", diagnostics.HospNum);
                    patientName = "Unknown Patient";
                }
            }

            ViewBag.PatientName = patientName;
            ViewBag.HospNum = diagnostics.HospNum;


            // Add a diagnostic message to help troubleshoot
            ViewBag.ModelStateErrors = string.Join(", ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));

            return View(diagnostics);
        }
        // GET: Diagnostics/AddAntibiotics/5
        public async Task<IActionResult> AddAntibiotics(int id)
        {
            try
            {
                // Load diagnostic with treatments and antibiotics
                var diagnostics = await _context.Diagnostics
                    .Include(d => d.Treatments)
                        .ThenInclude(t => t.Antibiotic)
                    .FirstOrDefaultAsync(m => m.DiagId == id);

                if (diagnostics == null)
                {
                    _logger.LogWarning("Diagnostic not found with ID: {DiagId}", id);
                    return NotFound();
                }

                // Get all available antibiotics for the dropdown
                var allAntibiotics = await _context.Antibiotics
                    .OrderBy(a => a.Name)
                    .ToListAsync();

                // Get selected antibiotics IDs
                var selectedAntibiotics = diagnostics.Treatments?
                    .Select(t => t.AntibioticId)
                    .ToArray() ?? Array.Empty<int>();

                ViewBag.SelectedAntibiotics = selectedAntibiotics;
                ViewBag.AntibioticsList = new SelectList(allAntibiotics, "AntibioticId", "Name");

                // Get patient name
                string patientName = await GetPatientName(diagnostics.HospNum);
                ViewBag.PatientName = patientName;

                return View(diagnostics);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading AddAntibiotics for diagnostic ID: {DiagId}", id);
                TempData["ErrorMessage"] = "An error occurred while loading antibiotics data.";
                return RedirectToAction(nameof(Index), new { hospNum = await GetHospNumFromDiagnosticId(id) });
            }
        }

        // POST: Diagnostics/AddAntibiotics
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAntibiotics(int diagId, string hospNum, string newAntibiotic, int[] selectedAntibiotics)
        {
            try
            {
                // Get the diagnostic record
                var diagnostic = await _context.Diagnostics
                    .Include(d => d.Treatments)
                    .FirstOrDefaultAsync(d => d.DiagId == diagId);

                if (diagnostic == null)
                {
                    return NotFound();
                }

                // Add new antibiotic if provided
                if (!string.IsNullOrWhiteSpace(newAntibiotic))
                {
                    var sanitizedName = newAntibiotic.Trim();
                    var existingAntibiotic = await _context.Antibiotics
                        .FirstOrDefaultAsync(a => a.Name.ToLower() == sanitizedName.ToLower());

                    if (existingAntibiotic == null)
                    {
                        // Create new antibiotic
                        var antibiotic = new Antibiotic
                        {
                            Name = sanitizedName,
                            DateAdded = DateTime.Now
                        };
                        _context.Antibiotics.Add(antibiotic);
                        await _context.SaveChangesAsync();

                        // Add to selected list
                        if (selectedAntibiotics == null)
                        {
                            selectedAntibiotics = new int[] { antibiotic.AntibioticId };
                        }
                        else
                        {
                            selectedAntibiotics = selectedAntibiotics.Append(antibiotic.AntibioticId).ToArray();
                        }
                    }
                    else if (selectedAntibiotics == null || !selectedAntibiotics.Contains(existingAntibiotic.AntibioticId))
                    {
                        // Add existing antibiotic to selection if not already selected
                        if (selectedAntibiotics == null)
                        {
                            selectedAntibiotics = new int[] { existingAntibiotic.AntibioticId };
                        }
                        else
                        {
                            selectedAntibiotics = selectedAntibiotics.Append(existingAntibiotic.AntibioticId).ToArray();
                        }
                    }
                }

                // Remove existing treatments
                var existingTreatments = await _context.DiagnosticsTreatments
                    .Where(t => t.DiagId == diagId)
                    .ToListAsync();

                if (existingTreatments.Any())
                {
                    _context.DiagnosticsTreatments.RemoveRange(existingTreatments);
                }

                // Add new treatments for selected antibiotics
                if (selectedAntibiotics != null && selectedAntibiotics.Length > 0)
                {
                    // Use raw SQL to fetch antibiotics instead of LINQ to avoid OPENJSON issues
                    List<Antibiotic> antibioticsToAdd = new List<Antibiotic>();

                    // Build a parameterized SQL query manually to avoid SQL injection
                    if (selectedAntibiotics.Length > 0)
                    {
                        // Build a SQL query with proper IN clause
                        string idList = string.Join(",", selectedAntibiotics);
                        string sql = $"SELECT AntibioticId, Name, DateAdded FROM tbantibiotics WHERE AntibioticId IN ({idList})";

                        // Execute raw SQL query
                        using (var command = _context.Database.GetDbConnection().CreateCommand())
                        {
                            command.CommandText = sql;

                            if (command.Connection.State != System.Data.ConnectionState.Open)
                                await command.Connection.OpenAsync();

                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                while (await reader.ReadAsync())
                                {
                                    antibioticsToAdd.Add(new Antibiotic
                                    {
                                        AntibioticId = reader.GetInt32(0),
                                        Name = reader.GetString(1),
                                        DateAdded = reader.IsDBNull(2) ? DateTime.Now : reader.GetDateTime(2)
                                    });
                                }
                            }
                        }
                    }

                    // Add new treatments
                    foreach (var antibiotic in antibioticsToAdd)
                    {
                        var treatment = new DiagnosticsTreatment
                        {
                            DiagId = diagId,
                            AntibioticId = antibiotic.AntibioticId,
                        };
                        _context.DiagnosticsTreatments.Add(treatment);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Diagnostics", new { hospNum });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating antibiotics for diagnostic ID: {DiagId}", diagId);
                TempData["ErrorMessage"] = "An error occurred while saving antibiotics. Please try again.";
                return RedirectToAction("AddAntibioticsToLatest", new { hospNum });
            }
        }

        // Helper method to get patient name
        private async Task<string> GetPatientName(string hospNum)
        {
            if (string.IsNullOrEmpty(hospNum))
                return "Unknown Patient";

            try
            {
                // Try to get from PatientMasters first
                var patient = await _patientContext.PatientMasters.FirstOrDefaultAsync(p => p.HospNum == hospNum);
                if (patient != null)
                    return $"{patient.FirstName} {patient.LastName}";

                // Fall back to tbmaster
                using (var command = _patientContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = "SELECT LastName, FirstName FROM tbmaster WHERE HospNum = @HospNum";
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@HospNum";
                    parameter.Value = hospNum;
                    command.Parameters.Add(parameter);

                    if (command.Connection.State != System.Data.ConnectionState.Open)
                        await command.Connection.OpenAsync();

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var lastName = reader["LastName"]?.ToString() ?? string.Empty;
                            var firstName = reader["FirstName"]?.ToString() ?? string.Empty;
                            return $"{firstName} {lastName}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting patient name for hospNum: {HospNum}", hospNum);
            }

            return $"Patient #{hospNum}";
        }

        // Helper method to get hospital number from diagnostic ID
        private async Task<string> GetHospNumFromDiagnosticId(int diagId)
        {
            return await _context.Diagnostics
                .Where(d => d.DiagId == diagId)
                .Select(d => d.HospNum)
                .FirstOrDefaultAsync() ?? string.Empty;
        }

        // GET: Diagnostics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diagnostics = await _context.Diagnostics
                .Include(d => d.Treatments)
                .ThenInclude(t => t.Antibiotic)
                .FirstOrDefaultAsync(m => m.DiagId == id);

            if (diagnostics == null)
            {
                return NotFound();
            }

            ViewBag.SelectedAntibiotics = diagnostics.Treatments.Select(t => t.AntibioticId).ToArray();

            // Get patient name
            string patientName = string.Empty;

            try
            {
                // Use raw SQL to get patient information
                using (var command = _patientContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
                    SELECT LastName, FirstName 
                    FROM tbmaster 
                    WHERE HospNum = @HospNum";

                    // Add parameter
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@HospNum";
                    parameter.Value = diagnostics.HospNum;
                    command.Parameters.Add(parameter);

                    // Ensure connection is open
                    if (command.Connection.State != System.Data.ConnectionState.Open)
                    {
                        await command.Connection.OpenAsync();
                    }

                    // Execute query
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var lastName = reader["LastName"]?.ToString() ?? string.Empty;
                            var firstName = reader["FirstName"]?.ToString() ?? string.Empty;
                            patientName = $"{firstName} {lastName}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting patient info for {HospNum}", diagnostics.HospNum);

                // Try to get from PatientMasters as fallback
                var patient = await _patientContext.PatientMasters.FirstOrDefaultAsync(p => p.HospNum == diagnostics.HospNum);
                if (patient != null)
                {
                    patientName = $"{patient.FirstName} {patient.LastName}";
                }
                else
                {
                    patientName = "Unknown Patient";
                }
            }

            ViewBag.PatientName = patientName;
            ViewBag.HospNum = diagnostics.HospNum;

            return View(diagnostics);
        }

        // POST: Diagnostics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiagId,DateCollection,SourceSite,IsolateFindingsResult,HospNum")] Diagnostics diagnostics)
        {
            if (id != diagnostics.DiagId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update the diagnostic
                    _context.Update(diagnostics);
                    await _context.SaveChangesAsync();

                    // Redirect to add antibiotics for editing
                    return RedirectToAction(nameof(AddAntibiotics), new { id = diagnostics.DiagId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiagnosticsExists(diagnostics.DiagId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating diagnostic {DiagId}", diagnostics.DiagId);

                    ModelState.AddModelError("", $"Unable to save changes: {ex.Message}");

                    // If inner exception exists, also log it
                    if (ex.InnerException != null)
                    {
                        _logger.LogError(ex.InnerException, "Inner exception in Edit");
                        ModelState.AddModelError("", $"Inner exception: {ex.InnerException.Message}");
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            // Get patient name
            string patientName = string.Empty;

            try
            {
                // Use raw SQL to get patient information
                using (var command = _patientContext.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = @"
                    SELECT LastName, FirstName 
                    FROM tbmaster 
                    WHERE HospNum = @HospNum";

                    // Add parameter
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "@HospNum";
                    parameter.Value = diagnostics.HospNum;
                    command.Parameters.Add(parameter);

                    // Ensure connection is open
                    if (command.Connection.State != System.Data.ConnectionState.Open)
                    {
                        await command.Connection.OpenAsync();
                    }

                    // Execute query
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var lastName = reader["LastName"]?.ToString() ?? string.Empty;
                            var firstName = reader["FirstName"]?.ToString() ?? string.Empty;
                            patientName = $"{firstName} {lastName}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting patient info for {HospNum}", diagnostics.HospNum);

                // Try to get from PatientMasters as fallback
                var patient = await _patientContext.PatientMasters.FirstOrDefaultAsync(p => p.HospNum == diagnostics.HospNum);
                if (patient != null)
                {
                    patientName = $"{patient.FirstName} {patient.LastName}";
                }
                else
                {
                    patientName = "Unknown Patient";
                }
            }

            ViewBag.PatientName = patientName;
            ViewBag.HospNum = diagnostics.HospNum;

            return View(diagnostics);
        }

        // GET: Diagnostics/Delete/5
        public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var diagnostics = await _context.Diagnostics
                    .Include(d => d.Treatments)
                    .ThenInclude(t => t.Antibiotic)
                    .FirstOrDefaultAsync(m => m.DiagId == id);

                if (diagnostics == null)
                {
                    return NotFound();
                }

                // Get patient name
                string patientName = string.Empty;

                try
                {
                    // Use raw SQL to get patient information
                    using (var command = _patientContext.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = @"
                    SELECT LastName, FirstName 
                    FROM tbmaster 
                    WHERE HospNum = @HospNum";

                        // Add parameter
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = "@HospNum";
                        parameter.Value = diagnostics.HospNum;
                        command.Parameters.Add(parameter);

                        // Ensure connection is open
                        if (command.Connection.State != System.Data.ConnectionState.Open)
                        {
                            await command.Connection.OpenAsync();
                        }

                        // Execute query
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                var lastName = reader["LastName"]?.ToString() ?? string.Empty;
                                var firstName = reader["FirstName"]?.ToString() ?? string.Empty;
                                patientName = $"{firstName} {lastName}";
                            }
                        }
                    }

                    ViewBag.PatientName = patientName;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error getting patient info for {HospNum}", diagnostics.HospNum);
                }

                return View(diagnostics);
            }

            // POST: Diagnostics/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                try
                {
                    var diagnostics = await _context.Diagnostics.FindAsync(id);
                    string hospNum = null;

                    if (diagnostics != null)
                    {
                        hospNum = diagnostics.HospNum;

                        // Remove associated treatments first
                        var treatments = await _context.DiagnosticsTreatments
                            .Where(t => t.DiagId == id)
                            .ToListAsync();

                        _context.DiagnosticsTreatments.RemoveRange(treatments);

                        // Then remove the diagnostic
                        _context.Diagnostics.Remove(diagnostics);
                        await _context.SaveChangesAsync();

                        // Add success message
                        TempData["SuccessMessage"] = "Diagnostic record deleted successfully";
                    }

                    return RedirectToAction(nameof(Index), new { hospNum });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error deleting diagnostic {DiagId}", id);
                    TempData["ErrorMessage"] = "An error occurred while deleting the diagnostic record";

                    // Get the hospital number to redirect back to index
                    var diagnostics = await _context.Diagnostics.FindAsync(id);
                    string hospNum = diagnostics?.HospNum;

                    return RedirectToAction(nameof(Index), new { hospNum });
                }
            }

            private bool DiagnosticsExists(int id)
            {
                return _context.Diagnostics.Any(e => e.DiagId == id);
            }

            // API endpoint for autocomplete
            [HttpGet]
            public async Task<IActionResult> GetAntibiotics(string term)
            {
                var termLower = term?.ToLower() ?? "";
                var antibiotics = await _context.Antibiotics
                    .Where(a => string.IsNullOrEmpty(termLower) || a.Name.ToLower().Contains(termLower))
                    .OrderBy(a => a.Name)
                    .Select(a => new { id = a.AntibioticId, text = a.Name })
                    .ToListAsync();

                return Json(antibiotics);
            }

            // API endpoint for adding new antibiotics via AJAX
            [HttpPost]
        public async Task<IActionResult> AddAntibiotic(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("Antibiotic name is required");
            }

            var sanitizedName = name.Trim();

            // Check if it already exists
            var existingAntibiotic = await _context.Antibiotics
                .FirstOrDefaultAsync(a => a.Name.ToLower() == sanitizedName.ToLower());

            if (existingAntibiotic != null)
            {
                return Json(new { id = existingAntibiotic.AntibioticId, name = existingAntibiotic.Name, exists = true });
            }

            // Create new antibiotic
            var antibiotic = new Antibiotic
            {
                Name = sanitizedName,
                DateAdded = DateTime.Now
            };

            _context.Antibiotics.Add(antibiotic);
            await _context.SaveChangesAsync();

            return Json(new { id = antibiotic.AntibioticId, name = antibiotic.Name, exists = false });
        }


        // GET: Diagnostics/AddAntibioticsToLatest/
        public async Task<IActionResult> AddAntibioticsToLatest(string hospNum)
        {
            if (string.IsNullOrEmpty(hospNum))
            {
                return RedirectToAction("Index", "Home");
            }

            var patient = await _patientContext.PatientMasters.FirstOrDefaultAsync(p => p.HospNum == hospNum);
            if (patient == null)
            {
                return NotFound();
            }

            // Find the latest diagnostic for this patient
            var latestDiagnostic = await _context.Diagnostics
                .Include(d => d.Treatments)
                .ThenInclude(t => t.Antibiotic)
                .Where(d => d.HospNum == hospNum)
                .OrderByDescending(d => d.DateCollection)
                .FirstOrDefaultAsync();

            if (latestDiagnostic == null)
            {
                // No diagnostics found, inform the user
                TempData["ErrorMessage"] = "No diagnostic records found for this patient. Please create a diagnostic record first.";
                return RedirectToAction("Details", "Patient", new { hospNum });
            }

            // Get selected antibiotics IDs
            var selectedAntibiotics = latestDiagnostic.Treatments?.Select(t => t.AntibioticId).ToArray() ?? new int[0];
            ViewBag.SelectedAntibiotics = selectedAntibiotics;

            // Set patient name for the view
            ViewBag.PatientName = $"{patient.FirstName} {patient.LastName}";

            return View("AddAntibiotics", latestDiagnostic);
        }
    }
}
