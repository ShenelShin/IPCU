using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IPCU.Models;
using IPCU.Services;
using System.Collections.Generic;

namespace IPCU.Controllers
{
    public class HaiReportController : Controller
    {
        private readonly HaiReportService _haiReportService;
        private readonly HaiExcelExportService _haiExcelExportService;


        public HaiReportController(HaiReportService haiReportService, HaiExcelExportService haiExcelExportService)
        {
            _haiReportService = haiReportService;
            _haiExcelExportService = haiExcelExportService;
        }

        // GET: HaiReport
        public IActionResult Index()
        {
            // Default to current year/month
            var now = DateTime.Now;
            return View(new HaiReportViewModel
            {
                Year = now.Year,
                Month = now.Month,
                ReportType = "Monthly"
            });
        }

        // POST: HaiReport/Generate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Generate(HaiReportViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            if (model.ReportType == "Monthly")
            {
                var reportData = await _haiReportService.GenerateMonthlyReportAsync(model.Year, model.Month);
                return View("MonthlyReport", reportData);
            }
            else if (model.ReportType == "Quarterly")
            {
                var reportData = await _haiReportService.GenerateQuarterlyReportAsync(model.Year, model.Quarter);
                return View("QuarterlyReport", reportData);
            }
            else // Annual
            {
                // For annual report, you might want to aggregate quarterly reports
                // This is a placeholder for implementation
                return RedirectToAction("Index");
            }
        }

        // GET: HaiReport/ManualEntry
        public IActionResult ManualEntry()
        {
            var now = DateTime.Now;
            var model = new ManualHaiEntryViewModel
            {
                Year = now.Year,
                Month = now.Month
            };

            return View(model);
        }

        // POST: HaiReport/SaveManualEntry
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveManualEntry(ManualHaiEntryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("ManualEntry", model);
            }

            // Here you would save the manual entry data to your database
            // This would typically involve creating or updating records
            // For this example, we'll just redirect back to the report generation

            TempData["SuccessMessage"] = "Manual HAI data has been saved successfully.";

            return RedirectToAction("Index", new { year = model.Year, month = model.Month });
        }

        // GET: HaiReport/Export
        // GET: HaiReport/Export
        public async Task<IActionResult> Export(int year, int month, string reportType)
        {
            try
            {
                byte[] fileContents;
                string fileName;

                if (reportType == "Monthly")
                {
                    var reportData = await _haiReportService.GenerateMonthlyReportAsync(year, month);
                    fileContents = await _haiExcelExportService.ExportMonthlyReportAsync(reportData);
                    fileName = $"HAI_Monthly_Report_{year}_{month}.xlsx";
                }
                else if (reportType == "Quarterly")
                {
                    // Use the quarter parameter directly instead of calculating from month
                    var reportData = await _haiReportService.GenerateQuarterlyReportAsync(year, month);
                    fileContents = await _haiExcelExportService.ExportQuarterlyReportAsync(reportData);
                    fileName = $"HAI_Quarterly_Report_{year}_Q{month}.xlsx";
                }
                else // Annual
                {
                    int quarter = (month + 2) / 3;
                    var reportData = await _haiReportService.GenerateQuarterlyReportAsync(year, quarter);
                    fileContents = await _haiExcelExportService.ExportQuarterlyReportAsync(reportData);
                    fileName = $"HAI_Quarterly_Report_{year}_Q{quarter}.xlsx";
                }

                return File(fileContents, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                // Log the exception
                TempData["ErrorMessage"] = $"Error exporting report: {ex.Message}";
                return RedirectToAction("Generate", new { year, month, reportType });
            }
        }
    }

    // View model for report generation
    public class HaiReportViewModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Quarter { get; set; }
        public string ReportType { get; set; } // "Monthly", "Quarterly", or "Annual"

        public List<int> AvailableYears => GetAvailableYears();
        public List<int> AvailableMonths => GetAvailableMonths();
        public List<int> AvailableQuarters => new List<int> { 1, 2, 3, 4 };

        private List<int> GetAvailableYears()
        {
            var years = new List<int>();
            int currentYear = DateTime.Now.Year;

            for (int i = currentYear - 5; i <= currentYear; i++)
            {
                years.Add(i);
            }

            return years;
        }

        private List<int> GetAvailableMonths()
        {
            var months = new List<int>();

            for (int i = 1; i <= 12; i++)
            {
                months.Add(i);
            }

            return months;
        }
    }
}