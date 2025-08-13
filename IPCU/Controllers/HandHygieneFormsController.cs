using IPCU.Data;
using IPCU.Models;
using IPCU.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Microsoft.Data.SqlClient;


namespace IPCU.Controllers
{
    public class HandHygieneFormsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public HandHygieneFormsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // GET: HandHygieneForms

        // GET: HandHygieneForms
        public async Task<IActionResult> Index()
        {
            var forms = await _context.HandHygieneForms
                .Include(f => f.Activities)  // Load related HHActivity records
                .ToListAsync();

            return View(forms);
        }

        // GET: HandHygieneForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var handHygieneForm = await _context.HandHygieneForms
                .Include(h => h.Activities)  // Load related HHActivity records
                .FirstOrDefaultAsync(m => m.HHId == id);

            if (handHygieneForm == null) return NotFound();

            return View(handHygieneForm);
        }

        //// View Models for Monthly Summary
        //public class ComplianceSummary
        //{
        //    public int TotalCompliantActions { get; set; }
        //    public int TotalObservedOpportunities { get; set; }
        //    public decimal ComplianceRate { get; set; } = 0;

        //    // Format compliance rate as percentage
        //    public string ComplianceRateFormatted => ComplianceRate.ToString("P2");
        //}

        //public class AreaComplianceSummary : ComplianceSummary
        //{
        //    public string Area { get; set; }
        //}

        //public class ProfessionComplianceSummary : ComplianceSummary
        //{
        //    public string Profession { get; set; }
        //}

        //public class ObserverComplianceSummary : ComplianceSummary
        //{
        //    public string Observer { get; set; }
        //}

        //public class HHMonthlySummaryViewModel
        //{
        //    public string Month { get; set; }
        //    public ComplianceSummary OverallSummary { get; set; }
        //    public List<AreaComplianceSummary> AreaSummaries { get; set; }
        //    public List<AreaComplianceSummary> NurseAreaSummaries { get; set; }
        //    public List<ProfessionComplianceSummary> ProfessionSummaries { get; set; }
        //    public List<ObserverComplianceSummary> ObserverSummaries { get; set; }
        //}

        // populate station dropdown
        private List<SelectListItem> GetStations()
        {
            var stations = new List<SelectListItem>();
            string connectionString = _configuration.GetConnectionString("BuildFileConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT Station FROM tbCoStation WHERE Station IS NOT NULL";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string station = reader["Station"].ToString();
                            stations.Add(new SelectListItem
                            {
                                Text = station,
                                Value = station
                            });
                        }
                    }
                }
            }

            return stations;
        }

        // populate observer dropdown
        private List<SelectListItem> GetObserver()
        {
            var observerList = new List<SelectListItem>();
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT DISTINCT Observer FROM HHAObserver WHERE Observer IS NOT NULL";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string observerName = reader["Observer"].ToString();
                            observerList.Add(new SelectListItem
                            {
                                Text = observerName,
                                Value = observerName
                            });
                        }
                    }
                }
            }

            return observerList;
        }


        public IActionResult Create()
        {
            ViewBag.StationList = GetStations(); 
            ViewBag.ObserverList = GetObserver();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HandHygieneForm handHygieneForm)
        {
            ModelState.Remove("HHId");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.HandHygieneForms.Add(handHygieneForm);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("AddActivities", new { id = handHygieneForm.HHId });
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Database error: {ex.Message}");
                }
            }

            ViewBag.StationList = GetStations();
            ViewBag.ObserverList = GetObserver();
            return View(handHygieneForm);
        }

        public IActionResult AddActivities(int id)
        {
            var handHygieneForm = _context.HandHygieneForms.Find(id);
            if (handHygieneForm == null)
            {
                return NotFound();
            }

            ViewBag.HHId = id; 
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddActivities(int HHId, string Activity, string BeforeHandRub, string BeforeHandWash,
                                                      string AfterHandRub, string AfterHandWash, string Gloves)
        {
            // Check if the required field is populated
            if (string.IsNullOrEmpty(Activity))
            {
                ModelState.AddModelError("Activity", "Activity information is required.");
                ViewBag.HHId = HHId;
                return View();
            }

            try
            {
                // Create a new activity object manually from the form fields
                var activity = new HHActivity
                {
                    HHId = HHId,
                    Activity = Activity,
                    BeforeHandRub = BeforeHandRub,
                    BeforeHandWash = BeforeHandWash,
                    AfterHandRub = AfterHandRub,
                    AfterHandWash = AfterHandWash,
                    Gloves = Gloves ?? "False"  // Handle checkbox (if not checked, it won't be in the request)
                };

                // Add the activity to the database
                _context.HHActivities.Add(activity);
                await _context.SaveChangesAsync();

                // Get the form with its current activities to display in the view
                var handHygieneForm = await _context.HandHygieneForms
                    .Include(f => f.Activities)
                    .FirstOrDefaultAsync(f => f.HHId == HHId);

                // Clear the ModelState so the form resets for the next activity
                ModelState.Clear();

                // Set success message
                TempData["SuccessMessage"] = "Activity added successfully. You can add another activity or finish.";

                // Return to the same page with the form ID
                ViewBag.HHId = HHId;
                ViewBag.Activities = handHygieneForm?.Activities;

                return View();
            }
            catch (Exception ex)
            {
                // Log the exception and show error
                ModelState.AddModelError("", $"Error saving activity: {ex.Message}");
                ViewBag.HHId = HHId;
                return View();
            }
        }

        // Add a new action method to finish adding activities
        public async Task<IActionResult> FinishActivities(int id)
        {
            var handHygieneForm = await _context.HandHygieneForms
                .Include(f => f.Activities)
                .FirstOrDefaultAsync(f => f.HHId == id);

            if (handHygieneForm == null) return NotFound();

            // Calculate and update compliance data
            UpdateComplianceData(handHygieneForm);

            // Save changes to database
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = id });
        }

        // Helper method to update compliance data
        private void UpdateComplianceData(HandHygieneForm form)
        {
            // Calculate total opportunities and compliant actions
            form.TotalObservedOpportunities = CalculateTotalOpportunities(form);
            form.TotalCompliantActions = CalculateCompliantActions(form);

            // Calculate compliance rate (handle division by zero)
            if (form.TotalObservedOpportunities > 0)
            {
                // Store as a decimal (not multiplied by 100)
                // This will be displayed as percentage with ToString("P2") in the view
                form.ComplianceRate = (decimal)form.TotalCompliantActions / form.TotalObservedOpportunities;
            }
            else
            {
                form.ComplianceRate = 0;
            }
        }



        // Helper methods for compliance calculation
        private int CalculateTotalOpportunities(HandHygieneForm form)
        {
            int count = 0;
            foreach (var activity in form.Activities)
            {
                // Count all non-empty entries as opportunities
                if (!string.IsNullOrEmpty(activity.BeforeHandRub)) count += CountEntries(activity.BeforeHandRub);
                if (!string.IsNullOrEmpty(activity.BeforeHandWash)) count += CountEntries(activity.BeforeHandWash);
                if (!string.IsNullOrEmpty(activity.AfterHandRub)) count += CountEntries(activity.AfterHandRub);
                if (!string.IsNullOrEmpty(activity.AfterHandWash)) count += CountEntries(activity.AfterHandWash);
            }
            return count;
        }

        private int CalculateCompliantActions(HandHygieneForm form)
        {
            int count = 0;
            foreach (var activity in form.Activities)
            {
                // Count entries marked with ✓ as compliant
                count += CountCompliantEntries(activity.BeforeHandRub);
                count += CountCompliantEntries(activity.BeforeHandWash);
                count += CountCompliantEntries(activity.AfterHandRub);
                count += CountCompliantEntries(activity.AfterHandWash);
            }
            return count;
        }

        private int CountEntries(string data)
        {
            if (string.IsNullOrEmpty(data)) return 0;
            return data.Split(';').Length;
        }

        private int CountCompliantEntries(string data)
        {
            if (string.IsNullOrEmpty(data)) return 0;
            int compliantCount = 0;
            var entries = data.Split(';');
            foreach (var entry in entries)
            {
                if (string.IsNullOrEmpty(entry)) continue;
                var parts = entry.Split(',');
                if (parts.Length == 2 && parts[1] == "1") 
                {
                    compliantCount++;
                }
            }
            return compliantCount;
        }




        // GET: HandHygieneForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var handHygieneForm = await _context.HandHygieneForms
                .Include(f => f.Activities)  // Ensure activities are loaded
                .FirstOrDefaultAsync(f => f.HHId == id);

            if (handHygieneForm == null) return NotFound();

            return View(handHygieneForm);
        }


        // POST: HandHygieneForms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HandHygieneForm handHygieneForm)
        {
            if (id != handHygieneForm.HHId) return NotFound();

            // Log received data for debugging
            System.Diagnostics.Debug.WriteLine($"Received form edit for HHId: {id} with {handHygieneForm.Activities?.Count ?? 0} activities");

            try
            {
                // First retrieve the existing form without activities
                var existingForm = await _context.HandHygieneForms
                    .FirstOrDefaultAsync(f => f.HHId == id);

                if (existingForm == null) return NotFound();

                // Update basic form properties
                _context.Entry(existingForm).CurrentValues.SetValues(handHygieneForm);

                // Handle activities separately
                // First, get existing activities
                var existingActivities = await _context.HHActivities
                    .Where(a => a.HHId == id)
                    .ToListAsync();

                // Process activities from the form
                if (handHygieneForm.Activities != null)
                {
                    var activityIdsToKeep = new List<int>();

                    foreach (var activity in handHygieneForm.Activities)
                    {
                        if (activity.ActId > 0) // Existing activity
                        {
                            var existingActivity = existingActivities.FirstOrDefault(a => a.ActId == activity.ActId);
                            if (existingActivity != null)
                            {
                                // Update existing activity
                                _context.Entry(existingActivity).CurrentValues.SetValues(activity);
                                activityIdsToKeep.Add(activity.ActId);
                            }
                        }
                        else // New activity
                        {
                            activity.HHId = id;
                            activity.Gloves = activity.Gloves ?? "False";
                            _context.HHActivities.Add(activity);
                        }
                    }

                    // Remove activities not in the updated list
                    foreach (var activity in existingActivities)
                    {
                        if (!activityIdsToKeep.Contains(activity.ActId))
                        {
                            _context.HHActivities.Remove(activity);
                        }
                    }
                }
                else
                {
                    // If no activities were submitted, keep existing ones
                    handHygieneForm.Activities = existingActivities;
                }

                // Reload all activities to recalculate compliance
                var updatedForm = await _context.HandHygieneForms
                    .Include(f => f.Activities)
                    .FirstOrDefaultAsync(f => f.HHId == id);

                // Calculate and update compliance data
                UpdateComplianceData(updatedForm);

                // Save all changes
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Log the exception
                System.Diagnostics.Debug.WriteLine($"Error saving form: {ex.Message}");
                ModelState.AddModelError("", $"Error saving form: {ex.Message}");
            }

            // If we got this far, something failed - reload activities and redisplay
            handHygieneForm.Activities = await _context.HHActivities
                .Where(a => a.HHId == id)
                .ToListAsync();

            return View(handHygieneForm);
        }



        // GET: HandHygieneForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var handHygieneForm = await _context.HandHygieneForms
                .FirstOrDefaultAsync(m => m.HHId == id);

            if (handHygieneForm == null) return NotFound();

            return View(handHygieneForm);
        }

        // POST: HandHygieneForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var handHygieneForm = await _context.HandHygieneForms.FindAsync(id);
            if (handHygieneForm != null)
            {
                _context.HandHygieneForms.Remove(handHygieneForm);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool HandHygieneFormExists(int id)
        {
            return _context.HandHygieneForms.Any(e => e.HHId == id);
        }

        // GET: HandHygieneForms/GeneratePdf/5
        public async Task<IActionResult> GeneratePdf(int? id)
        {
            if (id == null) return NotFound();

            var handHygieneForm = await _context.HandHygieneForms
                .Include(f => f.Activities)  // Load related HHActivity records
                .FirstOrDefaultAsync(m => m.HHId == id);

            if (handHygieneForm == null) return NotFound();

            // Create the PDF service
            var pdfService = new HandHygienePdfService();

            // Generate the PDF
            var pdfBytes = pdfService.GeneratePdf(handHygieneForm);

            // Return the PDF for inline viewing in the browser instead of downloading
            return File(
                pdfBytes,
                "application/pdf",
                $"HandHygieneForm_{id}.pdf",
                false  // false means "inline" (preview) instead of "attachment" (download)
            );
        }

        // GET: HandHygieneForms/MonthlySummary
        public async Task<IActionResult> MonthlySummary(DateTime? date, bool regenerate = false, string generationDate = null, string remarks = null)
        {
            // Default to current month if no date provided
            var targetDate = date ?? DateTime.Now;
            var startDate = new DateTime(targetDate.Year, targetDate.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            // Get all summaries for the selected month
            var allMonthlySummaries = await _context.HandHygieneComplianceSummary
                .Where(s => s.Month.Year == startDate.Year && s.Month.Month == startDate.Month)
                .OrderByDescending(s => s.GeneratedDate)
                .ToListAsync();

            // Group summaries by generation date to get unique timestamps
            var generationDates = allMonthlySummaries
                .Select(s => s.GeneratedDate)
                .Distinct()
                .OrderByDescending(d => d)
                .ToList();

            ViewBag.GenerationDates = generationDates;
            ViewBag.SelectedMonth = targetDate;

            // If we have summaries and aren't regenerating, display the requested or most recent generation
            if (allMonthlySummaries.Count > 0 && !regenerate)
            {
                DateTime? targetGenerationDate = null;

                // Parse the generation date if provided
                if (!string.IsNullOrEmpty(generationDate))
                {
                    if (DateTime.TryParse(generationDate, out var parsedDate))
                    {
                        targetGenerationDate = parsedDate;
                    }
                }

                // Use specified generation date or default to most recent
                DateTime actualGenDate = targetGenerationDate ?? generationDates.FirstOrDefault();

                // Filter summaries for this specific generation - use a fuzzy match for DateTime
                var summariesForGeneration = allMonthlySummaries
                    .Where(s => Math.Abs((s.GeneratedDate - actualGenDate).TotalSeconds) < 1)
                    .ToList();

                ViewBag.AreaSummaries = ConvertToDictionary(summariesForGeneration, "Area") ??
                    new Dictionary<string, (int compliant, int total, decimal rate)>();
                ViewBag.NurseAreaSummaries = ConvertToDictionary(summariesForGeneration, "NurseArea") ??
                    new Dictionary<string, (int compliant, int total, decimal rate)>();
                ViewBag.ProfessionSummaries = ConvertToDictionary(summariesForGeneration, "Profession") ??
                    new Dictionary<string, (int compliant, int total, decimal rate)>();
                ViewBag.ObserverSummaries = ConvertToDictionary(summariesForGeneration, "Observer") ??
                    new Dictionary<string, (int compliant, int total, decimal rate)>();
                ViewBag.CurrentRemarks = summariesForGeneration.FirstOrDefault()?.Remarks;

                ViewBag.LastGenerated = actualGenDate;
                ViewBag.Regenerate = false;

                return View();
            }

            // If we're regenerating but no remarks were provided, redirect back with a warning
            if (regenerate && string.IsNullOrEmpty(remarks))
            {
                TempData["ErrorMessage"] = "Remarks are required when regenerating data.";
                return RedirectToAction("MonthlySummary", new { date = targetDate.ToString("yyyy-MM-dd") });
            }

            // Get all forms for the selected month
            var allForms = await _context.HandHygieneForms.Include(f => f.Activities).ToListAsync();

            // Filter forms within the selected date range
            var forms = new List<HandHygieneForm>();
            foreach (var form in allForms)
            {
                if (form.Date >= startDate && form.Date <= endDate)
                {
                    forms.Add(form);
                }
            }

            // Generate summaries without using lambdas
            ViewBag.AreaSummaries = GenerateSummary(forms, "Area");

            var nurseForms = new List<HandHygieneForm>();
            foreach (var form in forms)
            {
                if (form.HCWType == "Nurse")
                {
                    nurseForms.Add(form);
                }
            }

            ViewBag.NurseAreaSummaries = GenerateSummary(nurseForms, "Area");
            ViewBag.ProfessionSummaries = GenerateSummary(forms, "HCWType");
            ViewBag.ObserverSummaries = GenerateSummary(forms, "Observer");

            ViewBag.Regenerate = true;

            // Save the data if regenerate is true
            if (regenerate)
            {
                await SaveSummaries(startDate,
                    ViewBag.AreaSummaries,
                    ViewBag.NurseAreaSummaries,
                    ViewBag.ProfessionSummaries,
                    ViewBag.ObserverSummaries,
                    remarks);

                // Get the latest generation date after saving
                var latestGeneration = await _context.HandHygieneComplianceSummary
                    .Where(s => s.Month.Year == startDate.Year && s.Month.Month == startDate.Month)
                    .OrderByDescending(s => s.GeneratedDate)
                    .Select(s => s.GeneratedDate)
                    .FirstOrDefaultAsync();

                ViewBag.LastGenerated = latestGeneration;
            }

            return View();
        }

        // POST: HandHygieneForms/SaveSummaries
        [HttpPost]
        public async Task<IActionResult> SaveSummaries(DateTime date)
        {
            var startDate = new DateTime(date.Year, date.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            // Get all forms for the selected month
            var allForms = await _context.HandHygieneForms.Include(f => f.Activities).ToListAsync();

            // Filter forms within the selected date range
            var forms = new List<HandHygieneForm>();
            foreach (var form in allForms)
            {
                if (form.Date >= startDate && form.Date <= endDate)
                {
                    forms.Add(form);
                }
            }

            // Generate summaries
            var areaSummaries = GenerateSummary(forms, "Area");

            var nurseForms = new List<HandHygieneForm>();
            foreach (var form in forms)
            {
                if (form.HCWType == "Nurse")
                {
                    nurseForms.Add(form);
                }
            }

            var nurseAreaSummaries = GenerateSummary(nurseForms, "Area");
            var professionSummaries = GenerateSummary(forms, "HCWType");
            var observerSummaries = GenerateSummary(forms, "Observer");

            // Save the summaries
            await SaveSummaries(startDate, areaSummaries, nurseAreaSummaries, professionSummaries, observerSummaries);

            return Json(new { success = true });
        }

        // Save summaries to the database
        // Save summaries to the database while preserving history
        private async Task SaveSummaries(
            DateTime date,
            Dictionary<string, (int compliant, int total, decimal rate)> areaSummaries,
            Dictionary<string, (int compliant, int total, decimal rate)> nurseAreaSummaries,
            Dictionary<string, (int compliant, int total, decimal rate)> professionSummaries,
            Dictionary<string, (int compliant, int total, decimal rate)> observerSummaries,
            string remarks = null)
        {
            // Don't remove existing summaries - we'll keep history
            DateTime generationTimestamp = DateTime.Now;

            // Helper function to add summaries to the database
            void AddSummaries(Dictionary<string, (int compliant, int total, decimal rate)> summaries, string summaryType)
            {
                foreach (var kvp in summaries)
                {
                    var newSummary = new HandHygieneComplianceSummary
                    {
                        Month = date,
                        SummaryType = summaryType,
                        Category = kvp.Key,
                        TotalCompliantActions = kvp.Value.compliant,
                        TotalObservedOpportunities = kvp.Value.total,
                        ComplianceRate = kvp.Value.rate,
                        GeneratedDate = generationTimestamp,
                        Remarks = remarks
                    };

                    _context.HandHygieneComplianceSummary.Add(newSummary);
                }
            }

            // Save each type of summary
            AddSummaries(areaSummaries, "Area");
            AddSummaries(nurseAreaSummaries, "NurseArea");
            AddSummaries(professionSummaries, "Profession");
            AddSummaries(observerSummaries, "Observer");

            await _context.SaveChangesAsync();
        }


        // Helper function to convert summaries into a dictionary without using lambdas
        private Dictionary<string, (int compliant, int total, decimal rate)> ConvertToDictionary(
            List<HandHygieneComplianceSummary> summaries, string summaryType)
        {
            var dictionary = new Dictionary<string, (int compliant, int total, decimal rate)>();

            foreach (var summary in summaries)
            {
                if (summary.SummaryType == summaryType)
                {
                    dictionary[summary.Category] = (
                        summary.TotalCompliantActions,
                        summary.TotalObservedOpportunities,
                        summary.ComplianceRate);
                }
            }

            return dictionary;
        }

        // Helper function to generate summary dictionaries without using lambdas
        private Dictionary<string, (int compliant, int total, decimal rate)> GenerateSummary(
            List<HandHygieneForm> forms, string groupingField)
        {
            var summaryDictionary = new Dictionary<string, (int compliant, int total, decimal rate)>();

            foreach (var form in forms)
            {
                string key = groupingField == "Area" ? form.Area :
                             groupingField == "HCWType" ? form.HCWType :
                             groupingField == "Observer" ? form.Observer : "";

                if (!string.IsNullOrEmpty(key))
                {
                    if (!summaryDictionary.ContainsKey(key))
                    {
                        summaryDictionary[key] = (0, 0, 0);
                    }

                    var existing = summaryDictionary[key];
                    int newCompliant = existing.compliant + form.TotalCompliantActions;
                    int newTotal = existing.total + form.TotalObservedOpportunities;
                    decimal newRate = newTotal > 0 ? (decimal)newCompliant / newTotal : 0;

                    summaryDictionary[key] = (newCompliant, newTotal, newRate);
                }
            }

            return summaryDictionary;
        }

        public async Task<IActionResult> ExportPDF(DateTime? date, DateTime? generationDate)
        {
            // Default to current month if no date provided
            var targetDate = date ?? DateTime.Now;
            var startDate = new DateTime(targetDate.Year, targetDate.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            // Get all summaries for the selected month
            var allSummaries = await _context.HandHygieneComplianceSummary
                .Where(s => s.Month.Year == startDate.Year && s.Month.Month == startDate.Month)
                .OrderByDescending(s => s.GeneratedDate)
                .ToListAsync();

            // Use selected generation date if provided, otherwise get the latest
            var selectedGenerationDate = generationDate;

            // If no generation date is specified, use the latest
            if (selectedGenerationDate == null)
            {
                selectedGenerationDate = allSummaries
                    .Select(s => s.GeneratedDate)
                    .FirstOrDefault();
            }

            // Filter summaries for the selected generation (using fuzzy match for DateTime)
            var selectedSummaries = allSummaries
                .Where(s => selectedGenerationDate != default && Math.Abs((s.GeneratedDate - selectedGenerationDate.Value).TotalSeconds) < 1)
                .ToList();

            // If no summaries exist, generate them
            if (selectedSummaries.Count == 0)
            {
                // Get all forms for the selected month
                var allForms = await _context.HandHygieneForms.Include(f => f.Activities).ToListAsync();

                // Filter forms within the selected date range
                var forms = new List<HandHygieneForm>();
                foreach (var form in allForms)
                {
                    if (form.Date >= startDate && form.Date <= endDate)
                    {
                        forms.Add(form);
                    }
                }

                // Generate summaries
                var areaSummaries = GenerateSummary(forms, "Area");

                var nurseForms = new List<HandHygieneForm>();
                foreach (var form in forms)
                {
                    if (form.HCWType == "Nurse")
                    {
                        nurseForms.Add(form);
                    }
                }

                var nurseAreaSummaries = GenerateSummary(nurseForms, "Area");
                var professionSummaries = GenerateSummary(forms, "HCWType");
                var observerSummaries = GenerateSummary(forms, "Observer");

                // Create PDF service
                var pdfService = new MonthlySummaryPdfService();
                var pdfBytes = pdfService.GeneratePdf(
                    startDate,
                    areaSummaries,
                    nurseAreaSummaries,
                    professionSummaries,
                    observerSummaries
                );

                return File(
                    pdfBytes,
                    "application/pdf",
                    $"HandHygieneSummary_{startDate:yyyy-MM}.pdf",
                    false  // Inline viewing
                );
            }
            else
            {
                // Convert existing summaries to dictionaries
                var areaSummaries = ConvertToDictionary(selectedSummaries, "Area") ?? new Dictionary<string, (int compliant, int total, decimal rate)>();
                var nurseAreaSummaries = ConvertToDictionary(selectedSummaries, "NurseArea") ?? new Dictionary<string, (int compliant, int total, decimal rate)>();
                var professionSummaries = ConvertToDictionary(selectedSummaries, "Profession") ?? new Dictionary<string, (int compliant, int total, decimal rate)>();
                var observerSummaries = ConvertToDictionary(selectedSummaries, "Observer") ?? new Dictionary<string, (int compliant, int total, decimal rate)>();

                // Create PDF service
                var pdfService = new MonthlySummaryPdfService();
                var pdfBytes = pdfService.GeneratePdf(
                    startDate,
                    areaSummaries,
                    nurseAreaSummaries,
                    professionSummaries,
                    observerSummaries
                );

                // Add generation date to filename
                string generationDateStr = selectedGenerationDate.HasValue
                    ? $"_Gen{selectedGenerationDate.Value:yyyyMMdd_HHmmss}"
                    : "";

                return File(
                    pdfBytes,
                    "application/pdf",
                    $"HandHygieneSummary_{startDate:yyyy-MM}{generationDateStr}.pdf",
                    false  // Inline viewing
                );
            }
        }

        // GET: HandHygieneForms/ExportExcel
        public async Task<IActionResult> ExportExcel(DateTime? date, DateTime? generationDate)
        {
            // Default to current month if no date provided
            var targetDate = date ?? DateTime.Now;
            var startDate = new DateTime(targetDate.Year, targetDate.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            // Get all summaries for the selected month
            var allSummaries = await _context.HandHygieneComplianceSummary
                .Where(s => s.Month.Year == startDate.Year && s.Month.Month == startDate.Month)
                .OrderByDescending(s => s.GeneratedDate)
                .ToListAsync();

            // Use selected generation date if provided, otherwise get the latest
            var selectedGenerationDate = generationDate;

            // If no generation date is specified, use the latest
            if (selectedGenerationDate == null)
            {
                selectedGenerationDate = allSummaries
                    .Select(s => s.GeneratedDate)
                    .FirstOrDefault();
            }

            // Filter summaries for the selected generation (using fuzzy match for DateTime)
            var selectedSummaries = allSummaries
                .Where(s => selectedGenerationDate != default && Math.Abs((s.GeneratedDate - selectedGenerationDate.Value).TotalSeconds) < 1)
                .ToList();

            // If no summaries exist, generate them
            Dictionary<string, (int compliant, int total, decimal rate)> areaSummaries;
            Dictionary<string, (int compliant, int total, decimal rate)> nurseAreaSummaries;
            Dictionary<string, (int compliant, int total, decimal rate)> professionSummaries;
            Dictionary<string, (int compliant, int total, decimal rate)> observerSummaries;

            if (selectedSummaries.Count == 0)
            {
                // Get all forms for the selected month
                var allForms = await _context.HandHygieneForms.Include(f => f.Activities).ToListAsync();

                // Filter forms within the selected date range
                var forms = new List<HandHygieneForm>();
                foreach (var form in allForms)
                {
                    if (form.Date >= startDate && form.Date <= endDate)
                    {
                        forms.Add(form);
                    }
                }

                // Generate summaries
                areaSummaries = GenerateSummary(forms, "Area");

                var nurseForms = new List<HandHygieneForm>();
                foreach (var form in forms)
                {
                    if (form.HCWType == "Nurse")
                    {
                        nurseForms.Add(form);
                    }
                }

                nurseAreaSummaries = GenerateSummary(nurseForms, "Area");
                professionSummaries = GenerateSummary(forms, "HCWType");
                observerSummaries = GenerateSummary(forms, "Observer");
            }
            else
            {
                // Convert existing summaries to dictionaries
                areaSummaries = ConvertToDictionary(selectedSummaries, "Area") ?? new Dictionary<string, (int compliant, int total, decimal rate)>();
                nurseAreaSummaries = ConvertToDictionary(selectedSummaries, "NurseArea") ?? new Dictionary<string, (int compliant, int total, decimal rate)>();
                professionSummaries = ConvertToDictionary(selectedSummaries, "Profession") ?? new Dictionary<string, (int compliant, int total, decimal rate)>();
                observerSummaries = ConvertToDictionary(selectedSummaries, "Observer") ?? new Dictionary<string, (int compliant, int total, decimal rate)>();
            }

            // Add generation date to Excel filename
            string generationDateStr = selectedGenerationDate.HasValue
                ? $"_Gen{selectedGenerationDate.Value:yyyyMMdd_HHmmss}"
                : "";

            // Generate Excel file
            using (var package = new ExcelPackage())
            {
                // Calculate overall compliance
                int totalCompliant = 0;
                int totalOpportunities = 0;
                decimal overallCompliance = 0;

                foreach (var area in areaSummaries)
                {
                    totalCompliant += area.Value.compliant;
                    totalOpportunities += area.Value.total;
                }

                overallCompliance = totalOpportunities > 0 ? (decimal)totalCompliant / totalOpportunities : 0;

                // Create a single worksheet
                var worksheet = package.Workbook.Worksheets.Add("Hand Hygiene Compliance");

                // Title and summary data
                worksheet.Cells[1, 1].Value = $"Hand Hygiene Compliance Summary - {startDate:MMMM yyyy}";
                worksheet.Cells[1, 1, 1, 5].Merge = true;
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1].Style.Font.Size = 14;
                worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Add generation date information
                if (selectedGenerationDate.HasValue)
                {
                    worksheet.Cells[2, 1].Value = $"Generation Date: {selectedGenerationDate.Value:yyyy-MM-dd HH:mm:ss}";
                    worksheet.Cells[2, 1, 2, 5].Merge = true;
                    worksheet.Cells[2, 1].Style.Font.Bold = true;
                    worksheet.Cells[2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                worksheet.Cells[3, 1].Value = "Overall Compliance:";
                worksheet.Cells[3, 2].Value = overallCompliance;
                worksheet.Cells[3, 2].Style.Numberformat.Format = "0.0%";

                worksheet.Cells[4, 1].Value = "Compliant Actions:";
                worksheet.Cells[4, 2].Value = totalCompliant;

                worksheet.Cells[5, 1].Value = "Observed Opportunities:";
                worksheet.Cells[5, 2].Value = totalOpportunities;

                worksheet.Cells[6, 1].Value = "Report Generated:";
                worksheet.Cells[6, 2].Value = DateTime.Now;
                worksheet.Cells[6, 2].Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";

                int currentRow = 8;

                // Add Areas (All Staff) section
                currentRow = AddDataSection(worksheet, currentRow, "Areas (All Staff)", areaSummaries);

                // Add Areas (Nurses Only) section
                currentRow = AddDataSection(worksheet, currentRow + 2, "Areas (Nurses Only)", nurseAreaSummaries);

                // Add Healthcare Workers section
                currentRow = AddDataSection(worksheet, currentRow + 2, "Healthcare Workers", professionSummaries);

                // Add Observers section
                currentRow = AddDataSection(worksheet, currentRow + 2, "Observers", observerSummaries);

                // Auto-fit columns
                worksheet.Cells.AutoFitColumns();

                // Return the Excel file
                var excelData = package.GetAsByteArray();
                return File(
                    excelData,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"HandHygieneSummary_{startDate:yyyy-MM}{generationDateStr}.xlsx"
                );
            }
        }

        // Helper method to add a data section to the worksheet
        private int AddDataSection(ExcelWorksheet worksheet, int startRow, string sectionTitle, Dictionary<string, (int compliant, int total, decimal rate)> data)
        {
            // Add section title
            worksheet.Cells[startRow, 1].Value = sectionTitle;
            worksheet.Cells[startRow, 1, startRow, 4].Merge = true;
            worksheet.Cells[startRow, 1].Style.Font.Bold = true;
            worksheet.Cells[startRow, 1].Style.Font.Size = 12;
            worksheet.Cells[startRow, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
            worksheet.Cells[startRow, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(220, 220, 220));

            // Add headers
            int headerRow = startRow + 1;
            worksheet.Cells[headerRow, 1].Value = "Category";
            worksheet.Cells[headerRow, 2].Value = "Compliant Actions";
            worksheet.Cells[headerRow, 3].Value = "Observed Opportunities";
            worksheet.Cells[headerRow, 4].Value = "Compliance Rate";

            // Style headers
            using (var range = worksheet.Cells[headerRow, 1, headerRow, 4])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(200, 200, 200));
                range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
            }

            // Add data
            int dataRow = headerRow + 1;
            foreach (var item in data)
            {
                worksheet.Cells[dataRow, 1].Value = item.Key;
                worksheet.Cells[dataRow, 2].Value = item.Value.compliant;
                worksheet.Cells[dataRow, 3].Value = item.Value.total;
                worksheet.Cells[dataRow, 4].Value = item.Value.rate;
                worksheet.Cells[dataRow, 4].Style.Numberformat.Format = "0.00%";

                // Apply conditional formatting based on compliance rate
                if (item.Value.rate >= 0.8m)
                {
                    worksheet.Cells[dataRow, 4].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[dataRow, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(198, 239, 206)); // Light green
                }
                else if (item.Value.rate >= 0.6m)
                {
                    worksheet.Cells[dataRow, 4].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[dataRow, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 235, 156)); // Light yellow
                }
                else
                {
                    worksheet.Cells[dataRow, 4].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    worksheet.Cells[dataRow, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 199, 206)); // Light red
                }

                dataRow++;
            }

            // Return the last row number used
            return dataRow;
        }

    }
}
