using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IPCU.Data;
using IPCU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using IPCU.Services;

namespace IPCU.Controllers
{
    public class DeviceMonitoringReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PatientDbContext _patientContext;
        private readonly BuildFileDbContext _buildFileContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DeviceMonitoringReportService _reportService;
        private readonly DeviceMonitoringPdfService _pdfService;
        private readonly DeviceMonitoringExcelService _excelService;
        private readonly DeviceMonitoringYearlyExcelService _yearlyExcelService;
        private readonly DeviceMonitoringAreaYearlyExcelService _areaYearlyExcelService;
        private readonly StationService _stationService;

        public DeviceMonitoringReportController(
            ApplicationDbContext context,
            PatientDbContext patientContext,
            BuildFileDbContext buildFileContext,
            UserManager<ApplicationUser> userManager,
            DeviceMonitoringReportService reportService,
            DeviceMonitoringPdfService pdfService,
            DeviceMonitoringExcelService excelService,
            DeviceMonitoringYearlyExcelService yearlyExcelService,
            DeviceMonitoringAreaYearlyExcelService areaYearlyExcelService,
            StationService stationService)
        {
            _context = context;
            _patientContext = patientContext;
            _buildFileContext = buildFileContext;
            _userManager = userManager;
            _reportService = reportService;
            _pdfService = pdfService;
            _excelService = excelService;
            _yearlyExcelService = yearlyExcelService;
            _areaYearlyExcelService = areaYearlyExcelService;
            _stationService = stationService;
        }

        // Update the Index method in your DeviceMonitoringReportController.cs

        public async Task<IActionResult> Index()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                // Get all available areas from StationService
                var allAreas = await _stationService.GetAllStationAreas();

                if (allAreas == null || !allAreas.Any())
                {
                    // Log warning and use fallback data explicitly
                    System.Diagnostics.Debug.WriteLine("Warning: No station areas found, using fallback data");
                    allAreas = _stationService.GetFallbackStationList();
                }

                // Get the user's assigned areas for the dropdown
                var assignedAreas = user.AssignedArea?.Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(a => a.Trim())
                    .ToList() ?? new List<string>();

                // Filter areas based on user permissions
                List<string> availableAreas;
                if (assignedAreas.Any() && !User.IsInRole("Administrator"))
                {
                    availableAreas = allAreas
                        .Where(a => assignedAreas.Contains(a))
                        .OrderBy(a => a)
                        .ToList();
                }
                else
                {
                    availableAreas = allAreas
                        .OrderBy(a => a)
                        .ToList();
                }

                // If still no available areas, use fallback in any case
                if (!availableAreas.Any())
                {
                    availableAreas = _stationService.GetFallbackStationList();
                    System.Diagnostics.Debug.WriteLine($"Using {availableAreas.Count} fallback areas as last resort");
                }

                // Create view model with areas and defaults
                var viewModel = new DeviceMonitoringReportViewModel
                {
                    Areas = availableAreas,
                    SelectedArea = availableAreas.FirstOrDefault() ?? "Emergency Department",
                    ReportMonth = DateTime.Now.Month,
                    ReportYear = DateTime.Now.Year,
                    ExportFormat = "PDF"
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                // Log the error
                System.Diagnostics.Debug.WriteLine($"Error in Index: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");

                // Use the public method to get fallback data
                var fallbackAreas = _stationService.GetFallbackStationList();
                return View(new DeviceMonitoringReportViewModel
                {
                    Areas = fallbackAreas,
                    SelectedArea = fallbackAreas.FirstOrDefault() ?? "Emergency Department",
                    ReportMonth = DateTime.Now.Month,
                    ReportYear = DateTime.Now.Year,
                    ExportFormat = "PDF"
                });
            }
        }

        [HttpGet]
        public IActionResult TestPdf()
        {
            var reportData = new DeviceMonitoringReportData
            {
                Area = "Test",
                Month = 1,
                Year = 2025,
                DailyData = new List<DailyDeviceData>
                {
                    new DailyDeviceData { Date = DateTime.Now }
                }
            };

            var pdfData = _pdfService.GeneratePdf(reportData);
            return File(pdfData, "application/pdf", "test.pdf", false);
        }

        [HttpGet]
        public async Task<IActionResult> GenerateAreaHalfYearlyReport(int year, int halfYear)
        {
            try
            {
                // Validate halfYear parameter
                if (halfYear != 1 && halfYear != 2)
                {
                    return BadRequest("HalfYear parameter must be either 1 (Jan-Jun) or 2 (Jul-Dec)");
                }

                // Validate user access
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return NotFound("User not found.");
                }

                var userAreas = currentUser.AssignedArea?.Split(',').Select(a => a.Trim()).ToList() ?? new List<string>();

                if (!userAreas.Any())
                {
                    return Forbid("No areas assigned to this user.");
                }

                // Determine which months to include
                int startMonth = halfYear == 1 ? 1 : 7;
                int endMonth = halfYear == 1 ? 6 : 12;
                string halfYearLabel = halfYear == 1 ? "First-Half" : "Second-Half";

                // Get data for all areas and all months in the selected half year
                var allReports = new List<DeviceMonitoringReportData>();

                foreach (var area in userAreas)
                {
                    for (int month = startMonth; month <= endMonth; month++)
                    {
                        try
                        {
                            var reportData = await _reportService.GenerateReportData(area, year, month);
                            if (reportData != null && reportData.DailyData != null && reportData.DailyData.Any())
                            {
                                allReports.Add(reportData);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Log the error but continue with other months and areas
                            System.Diagnostics.Debug.WriteLine($"Error generating data for {area} - {month}/{year}: {ex.Message}");
                        }
                    }
                }

                if (!allReports.Any())
                {
                    return NotFound($"No data available for any area or month in the {halfYearLabel} of {year}.");
                }

                // Generate the Excel file using the modified service
                var excelData = _areaYearlyExcelService.GenerateAreaHalfYearlyExcel(allReports, halfYear);

                // Return the Excel file
                return File(
                    fileContents: excelData,
                    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileDownloadName: $"AreaDeviceMonitoring-{halfYearLabel}-{year}.xlsx"
                );
            }
            catch (Exception ex)
            {
                return Content($"Error generating half-yearly report: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GenerateAreaYearlyReport(int year)
        {
            try
            {
                // Validate user access
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return NotFound("User not found.");
                }

                var userAreas = currentUser.AssignedArea?.Split(',').Select(a => a.Trim()).ToList() ?? new List<string>();

                if (!userAreas.Any())
                {
                    return Forbid("No areas assigned to this user.");
                }

                // Get data for all areas and all months in the year
                var allReports = new List<DeviceMonitoringReportData>();

                foreach (var area in userAreas)
                {
                    for (int month = 1; month <= 12; month++)
                    {
                        try
                        {
                            var reportData = await _reportService.GenerateReportData(area, year, month);
                            if (reportData != null && reportData.DailyData != null && reportData.DailyData.Any())
                            {
                                allReports.Add(reportData);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Log the error but continue with other months and areas
                            System.Diagnostics.Debug.WriteLine($"Error generating data for {area} - {month}/{year}: {ex.Message}");
                        }
                    }
                }

                if (!allReports.Any())
                {
                    return NotFound("No data available for any area or month in the selected year.");
                }

                // Generate the Excel file using the new service
                var excelData = _areaYearlyExcelService.GenerateAreaYearlyExcel(allReports);

                // Return the Excel file
                return RedirectToAction("GenerateAreaHalfYearlyReport", new { year, halfYear = 1 });
            }
            catch (Exception ex)
            {
                return Content($"Error generating area yearly report: {ex.Message}");
            }
        }

        //[HttpGet]
        //public IActionResult TestExcel()
        //{
        //    var reportData = new DeviceMonitoringReportData
        //    {
        //        Area = "Test",
        //        Month = 1,
        //        Year = 2025,
        //        DailyData = new List<DailyDeviceData>
        //{
        //    new DailyDeviceData {
        //        Date = DateTime.Now,
        //        IUCNonKTCount = 10,
        //        IUCKTCount = 5,
        //        CLNonHDCount = 15,
        //        CLHDCount = 8,
        //        MVCount = 12,
        //        AdmissionCount = 4,
        //        TransferInCount = 2,
        //        SentHomeCount = 3,
        //        MortalityCount = 1,
        //        TransferOutCount = 2
        //    }
        //}
        //    };

        //    var excelData = _excelService.GenerateExcel(reportData);

        //    return File(
        //        excelData,
        //        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //        "test.xlsx"
        //    );
        //}
        [HttpGet]
        public async Task<IActionResult> GenerateYearlyReport(string area, int year)
        {
            try
            {
                // Validate user has access to the area
                var currentUser = await _userManager.GetUserAsync(User);
                var userAreas = currentUser.AssignedArea?.Split(',').Select(a => a.Trim()).ToList() ?? new List<string>();

                if (!userAreas.Contains(area))
                {
                    return Forbid();
                }

                // Generate reports for all months in the year
                var monthlyReports = new List<DeviceMonitoringReportData>();

                for (int month = 1; month <= 12; month++)
                {
                    try
                    {
                        var reportData = await _reportService.GenerateReportData(area, year, month);
                        if (reportData != null && reportData.DailyData != null && reportData.DailyData.Any())
                        {
                            monthlyReports.Add(reportData);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log the error but continue with other months
                        System.Diagnostics.Debug.WriteLine($"Error generating data for {month}/{year}: {ex.Message}");
                    }
                }

                if (!monthlyReports.Any())
                {
                    return NotFound("No data available for any month in the selected year.");
                }

                // Generate the Excel file
                var excelData = _yearlyExcelService.GenerateYearlyExcel(monthlyReports);

                // Return the Excel file
                return File(
                    fileContents: excelData,
                    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileDownloadName: $"YearlyDeviceMonitoring-{area}-{year}.xlsx"
                );
            }
            catch (Exception ex)
            {
                return Content($"Error generating yearly report: {ex.Message}");
            }
        }



        // POST: Generate the report based on selected parameters
        // POST: Generate the report based on selected parameters
        [HttpPost]
        public async Task<IActionResult> GenerateReport(DeviceMonitoringReportViewModel model)
        {
            try
            {
                // Debug the model state
                if (!ModelState.IsValid)
                {
                    foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                    {
                        // Log or debug the errors
                        System.Diagnostics.Debug.WriteLine($"Model Error: {error.ErrorMessage}");
                    }

                    // Get all available areas
                    var allAreas = await _stationService.GetAllStationAreas();

                    // Get the user's assigned areas
                    var user = await _userManager.GetUserAsync(User);
                    var assignedAreas = user.AssignedArea?.Split(',').Select(a => a.Trim()).ToList() ?? new List<string>();

                    // Filter areas based on user permissions
                    List<string> availableAreas;
                    if (assignedAreas.Any() && !User.IsInRole("Administrator"))
                    {
                        availableAreas = allAreas.Where(a => assignedAreas.Contains(a)).ToList();
                    }
                    else
                    {
                        availableAreas = allAreas;
                    }

                    model.Areas = availableAreas;
                    return View("Index", model);
                }

                // Debug the area validation
                var currentUser = await _userManager.GetUserAsync(User);
                var userAreas = currentUser.AssignedArea?.Split(',').Select(a => a.Trim()).ToList() ?? new List<string>();

                // If user has assigned areas and is not an admin, check permissions
                if (userAreas.Any() && !User.IsInRole("Administrator") && !userAreas.Contains(model.SelectedArea))
                {
                    ModelState.AddModelError("SelectedArea", "You do not have access to the selected area.");

                    // Get all available areas
                    var allAreas = await _stationService.GetAllStationAreas();
                    var availableAreas = allAreas.Where(a => userAreas.Contains(a)).ToList();
                    model.Areas = availableAreas;

                    return View("Index", model);
                }

                // Generate report data using the service
                var reportData = await _reportService.GenerateReportData(model.SelectedArea, model.ReportYear, model.ReportMonth);

                // Check if we have any data
                if (reportData == null || reportData.DailyData == null || !reportData.DailyData.Any())
                {
                    ModelState.AddModelError("", "No data available for the selected criteria.");

                    // Get all available areas for re-displaying the form
                    var allAreas = await _stationService.GetAllStationAreas();
                    var availableAreas = userAreas.Any() && !User.IsInRole("Administrator")
                        ? allAreas.Where(a => userAreas.Contains(a)).ToList()
                        : allAreas;

                    model.Areas = availableAreas;
                    return View("Index", model);
                }

                // Based on the selected format, return the appropriate action
                switch (model.ExportFormat.ToUpper())
                {
                    case "PDF":
                        return RedirectToAction("ViewReport", new
                        {
                            area = model.SelectedArea,
                            year = model.ReportYear,
                            month = model.ReportMonth
                        });

                    case "EXCEL":
                        return RedirectToAction("DownloadExcel", new
                        {
                            area = model.SelectedArea,
                            year = model.ReportYear,
                            month = model.ReportMonth
                        });

                    default:
                        // If format is not recognized, default to PDF
                        return RedirectToAction("ViewReport", new
                        {
                            area = model.SelectedArea,
                            year = model.ReportYear,
                            month = model.ReportMonth
                        });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                System.Diagnostics.Debug.WriteLine($"Report Generation Error: {ex.Message}");

                // Check if this is an AJAX request
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    Response.StatusCode = 500;
                    return Json(new { error = "Error generating report: " + ex.Message });
                }

                // Add error to ModelState for non-AJAX requests
                ModelState.AddModelError("", "An error occurred while generating the report: " + ex.Message);

                // Re-populate model areas
                var allAreas = await _stationService.GetAllStationAreas();
                var currentUser = await _userManager.GetUserAsync(User);
                var userAreas = currentUser.AssignedArea?.Split(',').Select(a => a.Trim()).ToList() ?? new List<string>();

                // Filter areas based on user permissions
                List<string> availableAreas;
                if (userAreas.Any() && !User.IsInRole("Administrator"))
                {
                    availableAreas = allAreas.Where(a => userAreas.Contains(a)).ToList();
                }
                else
                {
                    availableAreas = allAreas;
                }

                model.Areas = availableAreas;

                return View("Index", model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ViewReport(string area, int year, int month)
        {
            try
            {
                var reportData = await _reportService.GenerateReportData(area, year, month);
                var pdfData = _pdfService.GeneratePdf(reportData);

                // Return PDF for inline viewing
                return File(
                    pdfData,
                    "application/pdf",
                    null // No filename needed for inline viewing
                );
            }
            catch (Exception ex)
            {
                return Content($"Error generating PDF: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DownloadExcel(string area, int year, int month)
        {
            try
            {
                var reportData = await _reportService.GenerateReportData(area, year, month);
                var excelData = _excelService.GenerateExcel(reportData);

                // Return Excel file for downloading
                return File(
                    excelData,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"DeviceMonitoring-{area}-{year}-{month}.xlsx"
                );
            }
            catch (Exception ex)
            {
                return Content($"Error generating Excel: {ex.Message}");
            }
        }

    }

    // View model for the report selection form
    public class DeviceMonitoringReportViewModel
    {
        [Required(ErrorMessage = "Please select an area")]
        [Display(Name = "Area")]
        public string SelectedArea { get; set; }

        [Required(ErrorMessage = "Please select a month")]
        [Display(Name = "Month")]
        [Range(1, 12, ErrorMessage = "Month must be between 1 and 12")]
        public int ReportMonth { get; set; }

        [Required(ErrorMessage = "Please select a year")]
        [Display(Name = "Year")]
        [Range(2020, 2030, ErrorMessage = "Year must be between 2020 and 2030")]
        public int ReportYear { get; set; }

        [Required(ErrorMessage = "Please select an export format")]
        [Display(Name = "Export Format")]
        public string ExportFormat { get; set; }

        // This will be populated from the controller to display the dropdown of areas
        public List<string> Areas { get; set; }
    }
    // Add a new view model for half-yearly reports
    public class HalfYearlyReportViewModel
    {
        [Required(ErrorMessage = "Please select a year")]
        [Display(Name = "Year")]
        [Range(2020, 2030, ErrorMessage = "Year must be between 2020 and 2030")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please select which half of the year")]
        [Display(Name = "Half Year")]
        [Range(1, 2, ErrorMessage = "Half year must be either 1 (Jan-Jun) or 2 (Jul-Dec)")]
        public int HalfYear { get; set; }
    }


}