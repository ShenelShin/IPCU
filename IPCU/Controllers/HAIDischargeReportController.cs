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
    public class HAIDischargeReportController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly HAIDischargeReportService _reportService;
        private readonly HAIDischargeExcelService _excelService;

        public HAIDischargeReportController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            HAIDischargeReportService reportService,
            HAIDischargeExcelService excelService)
        {
            _context = context;
            _userManager = userManager;
            _reportService = reportService;
            _excelService = excelService;
        }

        // GET: Display form to select report options
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            // Default to current year
            var viewModel = new HAIDischargeReportViewModel
            {
                ReportYear = DateTime.Now.Year
            };

            return View(viewModel);
        }

        // POST: Generate the report based on selected parameters
        [HttpPost]
        public async Task<IActionResult> GenerateReport(HAIDischargeReportViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index", model);
                }

                // Get data for the selected year
                var reportData = await _reportService.GetHAIDischargeDataForYearAsync(model.ReportYear);

                if (!reportData.Any())
                {
                    ModelState.AddModelError("", "No data available for the selected year.");
                    return View("Index", model);
                }

                // Generate Excel report
                var excelData = _excelService.GenerateHAIDischargeExcel(reportData, model.ReportYear);

                // Return the Excel file
                return File(
                    fileContents: excelData,
                    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileDownloadName: $"HAI-Discharge-Report-{model.ReportYear}.xlsx"
                );
            }
            catch (Exception ex)
            {
                // Log the exception
                ModelState.AddModelError("", "An error occurred while generating the report: " + ex.Message);
                return View("Index", model);
            }
        }

        // GET: Generate report for a specific year
        [HttpGet]
        public async Task<IActionResult> GenerateYearlyReport(int year)
        {
            try
            {
                // Get data for the specified year
                var reportData = await _reportService.GetHAIDischargeDataForYearAsync(year);

                if (!reportData.Any())
                {
                    return NotFound("No data available for the selected year.");
                }

                // Generate Excel report
                var excelData = _excelService.GenerateHAIDischargeExcel(reportData, year);

                // Return the Excel file
                return File(
                    fileContents: excelData,
                    contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    fileDownloadName: $"HAI-Discharge-Report-{year}.xlsx"
                );
            }
            catch (Exception ex)
            {
                return Content($"Error generating report: {ex.Message}");
            }
        }
    }

    // View model for the report selection form
    public class HAIDischargeReportViewModel
    {
        [Required(ErrorMessage = "Please select a year")]
        [Display(Name = "Year")]
        [Range(2020, 2030, ErrorMessage = "Year must be between 2020 and 2030")]
        public int ReportYear { get; set; }
    }
}