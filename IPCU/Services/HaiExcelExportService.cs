using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using IPCU.Models;

namespace IPCU.Services
{
    public class HaiExcelExportService
    {
        public async Task<byte[]> ExportMonthlyReportAsync(MonthlyHaiReportData reportData)
        {
            using (var package = new ExcelPackage())
            {
                // Create the worksheets
                var overviewSheet = package.Workbook.Worksheets.Add("Overview");
                var deviceAssociatedSheet = package.Workbook.Worksheets.Add("Device-Associated");
                var ventilatorEventsSheet = package.Workbook.Worksheets.Add("Ventilator Events");
                var nonDeviceSheet = package.Workbook.Worksheets.Add("Non-Device");
                var siteSpecificSheet = package.Workbook.Worksheets.Add("Site-Specific");

                // Format the overview sheet
                await FormatOverviewSheetAsync(overviewSheet, reportData);

                // Format the device-associated infections sheet
                await FormatDeviceAssociatedSheetAsync(deviceAssociatedSheet, reportData);

                // Format the ventilator-associated events sheet
                await FormatVentilatorEventsSheetAsync(ventilatorEventsSheet, reportData);

                // Format the non-device associated infections sheet
                await FormatNonDeviceSheetAsync(nonDeviceSheet, reportData);

                // Format the site-specific infections sheet
                await FormatSiteSpecificSheetAsync(siteSpecificSheet, reportData);

                // Return the Excel package as a byte array
                return await package.GetAsByteArrayAsync();
            }
        }

        public async Task<byte[]> ExportQuarterlyReportAsync(QuarterlyHaiReportData reportData)
        {
            using (var package = new ExcelPackage())
            {
                // Create the worksheets
                var overviewSheet = package.Workbook.Worksheets.Add("Overview");
                var monthlyBreakdownSheet = package.Workbook.Worksheets.Add("Monthly Breakdown");
                var deviceSummarySheet = package.Workbook.Worksheets.Add("Device Summary");
                var detailsSheet = package.Workbook.Worksheets.Add("Detailed Data");

                // Format the overview sheet
                await FormatQuarterlyOverviewSheetAsync(overviewSheet, reportData);

                // Format the monthly breakdown sheet
                await FormatMonthlyBreakdownSheetAsync(monthlyBreakdownSheet, reportData);

                // Format the device summary sheet
                await FormatDeviceSummarySheetAsync(deviceSummarySheet, reportData);

                // Format the details sheet
                await FormatQuarterlyDetailsSheetAsync(detailsSheet, reportData);

                // Return the Excel package as a byte array
                return await package.GetAsByteArrayAsync();
            }
        }

        // Helper methods for formatting the Monthly Report sheets
        private async Task FormatOverviewSheetAsync(ExcelWorksheet sheet, MonthlyHaiReportData reportData)
        {
            // Set the title
            sheet.Cells[1, 1].Value = $"Monthly HAI Report - {reportData.MonthName} {reportData.Year}";
            sheet.Cells[1, 1, 1, 4].Merge = true;
            sheet.Cells[1, 1, 1, 4].Style.Font.Bold = true;
            sheet.Cells[1, 1, 1, 4].Style.Font.Size = 14;

            // Add the summary information
            sheet.Cells[3, 1].Value = "Overall HAI Summary";
            sheet.Cells[3, 1, 3, 4].Merge = true;
            sheet.Cells[3, 1, 3, 4].Style.Font.Bold = true;
            sheet.Cells[3, 1, 3, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[3, 1, 3, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

            // Header row
            sheet.Cells[4, 1].Value = "Infection Type";
            sheet.Cells[4, 2].Value = "Number of Cases";
            sheet.Cells[4, 3].Value = "Patient Days";
            sheet.Cells[4, 4].Value = "Rate";
            sheet.Cells[4, 1, 4, 4].Style.Font.Bold = true;
            sheet.Cells[4, 1, 4, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[4, 1, 4, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

            // Add the data rows
            int row = 5;
            AddDataRow(sheet, row++, reportData.DeviceAssociatedTotal);
            AddDataRow(sheet, row++, reportData.NonDeviceAssociatedTotal);
            AddDataRow(sheet, row++, reportData.SsiTotal);
            AddDataRow(sheet, row++, reportData.OverallHaiRate);
            AddDataRow(sheet, row++, reportData.DohRate);

            // Highlight the overall HAI rate row
            sheet.Cells[row - 2, 1, row - 2, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[row - 2, 1, row - 2, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

            // Format the DOH rate row
            sheet.Cells[row - 1, 1, row - 1, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[row - 1, 1, row - 1, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightPink);

            // Auto-fit the columns
            sheet.Cells.AutoFitColumns();

            await Task.CompletedTask;
        }

        private async Task FormatDeviceAssociatedSheetAsync(ExcelWorksheet sheet, MonthlyHaiReportData reportData)
        {
            // Set the title
            sheet.Cells[1, 1].Value = $"Device-Associated Infections - {reportData.MonthName} {reportData.Year}";
            sheet.Cells[1, 1, 1, 4].Merge = true;
            sheet.Cells[1, 1, 1, 4].Style.Font.Bold = true;
            sheet.Cells[1, 1, 1, 4].Style.Font.Size = 14;

            // Header row
            sheet.Cells[3, 1].Value = "Infection Type";
            sheet.Cells[3, 2].Value = "Number of Cases";
            sheet.Cells[3, 3].Value = "Device Days";
            sheet.Cells[3, 4].Value = "Rate";
            sheet.Cells[3, 1, 3, 4].Style.Font.Bold = true;
            sheet.Cells[3, 1, 3, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[3, 1, 3, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

            // Add the data rows
            int row = 4;
            foreach (var infection in reportData.DeviceAssociatedInfections)
            {
                AddDataRow(sheet, row++, infection);
            }

            // Add the total row
            sheet.Cells[row, 1].Value = "Total Device-Associated Infections";
            sheet.Cells[row, 2].Value = reportData.DeviceAssociatedInfections.Sum(i => i.CaseCount);
            sheet.Cells[row, 3].Value = "--";
            sheet.Cells[row, 4].Value = "--";
            sheet.Cells[row, 1, row, 4].Style.Font.Bold = true;
            sheet.Cells[row, 1, row, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[row, 1, row, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

            // Add notes
            sheet.Cells[row + 2, 1].Value = "Note: Device-associated rates are calculated per 1,000 device days";
            sheet.Cells[row + 2, 1, row + 2, 4].Merge = true;
            sheet.Cells[row + 2, 1, row + 2, 4].Style.Font.Italic = true;

            // Auto-fit the columns
            sheet.Cells.AutoFitColumns();

            await Task.CompletedTask;
        }

        private async Task FormatVentilatorEventsSheetAsync(ExcelWorksheet sheet, MonthlyHaiReportData reportData)
        {
            // Set the title
            sheet.Cells[1, 1].Value = $"Ventilator-Associated Events - {reportData.MonthName} {reportData.Year}";
            sheet.Cells[1, 1, 1, 5].Merge = true;
            sheet.Cells[1, 1, 1, 5].Style.Font.Bold = true;
            sheet.Cells[1, 1, 1, 5].Style.Font.Size = 14;

            // Add description
            sheet.Cells[2, 1].Value = "Ventilator-Associated Events (VAEs) are reported based on device-connected days";
            sheet.Cells[2, 1, 2, 5].Merge = true;
            sheet.Cells[2, 1, 2, 5].Style.Font.Italic = true;

            // Header row
            sheet.Cells[4, 1].Value = "Event Type";
            sheet.Cells[4, 2].Value = "Number of Cases";
            sheet.Cells[4, 3].Value = "Ventilator Days";
            sheet.Cells[4, 4].Value = "Rate per 1,000 Ventilator Days";
            sheet.Cells[4, 5].Value = "Device Count Name";
            sheet.Cells[4, 1, 4, 5].Style.Font.Bold = true;
            sheet.Cells[4, 1, 4, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[4, 1, 4, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

            // Add the data rows for specific VAE types
            int row = 5;

            // VAC row
            var vacEvent = reportData.VentilatorAssociatedEvents.FirstOrDefault(v => v.InfectionType == "Ventilator Associated Condition (VAC)") ??
                new HaiCaseData { InfectionType = "Ventilator Associated Condition (VAC)", CaseCount = 0, DeviceDays = reportData.VentilatorDays };
            AddVaeDataRow(sheet, row++, vacEvent, "MV");

            // IVAC row
            var ivacEvent = reportData.VentilatorAssociatedEvents.FirstOrDefault(v => v.InfectionType == "Infection-related Ventilator Associated Complication (IVAC)") ??
                new HaiCaseData { InfectionType = "Infection-related Ventilator Associated Complication (IVAC)", CaseCount = 0, DeviceDays = reportData.VentilatorDays };
            AddVaeDataRow(sheet, row++, ivacEvent, "MV");

            // PVAP row
            var pvapEvent = reportData.VentilatorAssociatedEvents.FirstOrDefault(v => v.InfectionType == "Possible Ventilator Associated Pneumonia (PVAP)") ??
                new HaiCaseData { InfectionType = "Possible Ventilator Associated Pneumonia (PVAP)", CaseCount = 0, DeviceDays = reportData.VentilatorDays };
            AddVaeDataRow(sheet, row++, pvapEvent, "MV");

            // Add remaining VAE data rows if there are any other types in the data
            foreach (var infection in reportData.VentilatorAssociatedEvents.Where(v =>
                v.InfectionType != "Ventilator Associated Condition (VAC)" &&
                v.InfectionType != "Infection-related Ventilator Associated Complication (IVAC)" &&
                v.InfectionType != "Possible Ventilator Associated Pneumonia (PVAP)"))
            {
                AddVaeDataRow(sheet, row++, infection, "MV");
            }

            // Add the total row
            sheet.Cells[row, 1].Value = "Total VAE";
            sheet.Cells[row, 2].Value = reportData.VentilatorAssociatedEvents.Sum(i => i.CaseCount);
            sheet.Cells[row, 3].Value = reportData.VentilatorDays;
            decimal totalRate = reportData.VentilatorDays > 0 ?
                (decimal)reportData.VentilatorAssociatedEvents.Sum(i => i.CaseCount) * 1000 / reportData.VentilatorDays : 0;
            sheet.Cells[row, 4].Value = totalRate.ToString("0.00");
            sheet.Cells[row, 5].Value = "MV";
            sheet.Cells[row, 1, row, 5].Style.Font.Bold = true;
            sheet.Cells[row, 1, row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[row, 1, row, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

            // Add notes
            sheet.Cells[row + 2, 1].Value = "Note: Ventilator-associated event rates are calculated per 1,000 ventilator days (MV)";
            sheet.Cells[row + 2, 1, row + 2, 5].Merge = true;
            sheet.Cells[row + 2, 1, row + 2, 5].Style.Font.Italic = true;

            // Auto-fit the columns
            sheet.Cells.AutoFitColumns();

            await Task.CompletedTask;
        }

        private void AddVaeDataRow(ExcelWorksheet sheet, int row, HaiCaseData data, string deviceCountName)
        {
            sheet.Cells[row, 1].Value = data.InfectionType;
            sheet.Cells[row, 2].Value = data.CaseCount;
            sheet.Cells[row, 3].Value = data.DeviceDays;
            sheet.Cells[row, 4].Value = data.FormattedRate;
            sheet.Cells[row, 5].Value = deviceCountName;
        }

        private async Task FormatNonDeviceSheetAsync(ExcelWorksheet sheet, MonthlyHaiReportData reportData)
        {
            // Set the title
            sheet.Cells[1, 1].Value = $"Non-Device-Associated Infections - {reportData.MonthName} {reportData.Year}";
            sheet.Cells[1, 1, 1, 4].Merge = true;
            sheet.Cells[1, 1, 1, 4].Style.Font.Bold = true;
            sheet.Cells[1, 1, 1, 4].Style.Font.Size = 14;

            // Header row
            sheet.Cells[3, 1].Value = "Infection Type";
            sheet.Cells[3, 2].Value = "Number of Cases";
            sheet.Cells[3, 3].Value = "Patient Days";
            sheet.Cells[3, 4].Value = "Rate";
            sheet.Cells[3, 1, 3, 4].Style.Font.Bold = true;
            sheet.Cells[3, 1, 3, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[3, 1, 3, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

            // Add the data rows
            int row = 4;
            foreach (var infection in reportData.NonDeviceAssociatedInfections)
            {
                AddDataRow(sheet, row++, infection);
            }

            // Add the total row
            sheet.Cells[row, 1].Value = "Total Non-Device-Associated Infections";
            sheet.Cells[row, 2].Value = reportData.NonDeviceAssociatedInfections.Sum(i => i.CaseCount);
            sheet.Cells[row, 3].Value = reportData.NonDeviceAssociatedTotal.DeviceDays;
            sheet.Cells[row, 4].Value = reportData.NonDeviceAssociatedTotal.FormattedRate;
            sheet.Cells[row, 1, row, 4].Style.Font.Bold = true;
            sheet.Cells[row, 1, row, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[row, 1, row, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

            // Add notes
            sheet.Cells[row + 2, 1].Value = "Note: Non-device rates are calculated per 100 patient days";
            sheet.Cells[row + 2, 1, row + 2, 4].Merge = true;
            sheet.Cells[row + 2, 1, row + 2, 4].Style.Font.Italic = true;

            // Auto-fit the columns
            sheet.Cells.AutoFitColumns();

            await Task.CompletedTask;
        }

        private async Task FormatSiteSpecificSheetAsync(ExcelWorksheet sheet, MonthlyHaiReportData reportData)
        {
            // Set the title
            sheet.Cells[1, 1].Value = $"Site-Specific Infections - {reportData.MonthName} {reportData.Year}";
            sheet.Cells[1, 1, 1, 4].Merge = true;
            sheet.Cells[1, 1, 1, 4].Style.Font.Bold = true;
            sheet.Cells[1, 1, 1, 4].Style.Font.Size = 14;

            // Header row
            sheet.Cells[3, 1].Value = "Infection Type";
            sheet.Cells[3, 2].Value = "Number of Cases";
            sheet.Cells[3, 3].Value = "Patient Days";
            sheet.Cells[3, 4].Value = "Rate";
            sheet.Cells[3, 1, 3, 4].Style.Font.Bold = true;
            sheet.Cells[3, 1, 3, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[3, 1, 3, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

            // Add the data rows
            int row = 4;
            foreach (var infection in reportData.SiteSpecificInfections)
            {
                AddDataRow(sheet, row++, infection);
            }

            // Add the total row
            sheet.Cells[row, 1].Value = "Total Site-Specific Infections";
            sheet.Cells[row, 2].Value = reportData.SiteSpecificInfections.Sum(i => i.CaseCount);
            sheet.Cells[row, 3].Value = reportData.SiteSpecificInfections.FirstOrDefault()?.DeviceDays ?? 0;
            sheet.Cells[row, 4].Value = reportData.SiteSpecificInfections.Any() && reportData.SiteSpecificInfections.First().DeviceDays > 0 ?
                 ((decimal)reportData.SiteSpecificInfections.Sum(i => i.CaseCount) * 100 / reportData.SiteSpecificInfections.First().DeviceDays).ToString("0.00") + "%" : "0.00%";
            sheet.Cells[row, 1, row, 4].Style.Font.Bold = true;
            sheet.Cells[row, 1, row, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[row, 1, row, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

            // Add notes
            sheet.Cells[row + 2, 1].Value = "Note: Site-specific rates are calculated per 100 patient days";
            sheet.Cells[row + 2, 1, row + 2, 4].Merge = true;
            sheet.Cells[row + 2, 1, row + 2, 4].Style.Font.Italic = true;

            // Auto-fit the columns
            sheet.Cells.AutoFitColumns();

            await Task.CompletedTask;
        }

        // Helper methods for formatting the Quarterly Report sheets
        private async Task FormatQuarterlyOverviewSheetAsync(ExcelWorksheet sheet, QuarterlyHaiReportData reportData)
        {
            // Set the title
            sheet.Cells[1, 1].Value = $"Quarterly HAI Report - Q{reportData.Quarter} {reportData.Year} ({reportData.QuarterRange})";
            sheet.Cells[1, 1, 1, 4].Merge = true;
            sheet.Cells[1, 1, 1, 4].Style.Font.Bold = true;
            sheet.Cells[1, 1, 1, 4].Style.Font.Size = 14;

            // Add the summary information
            sheet.Cells[3, 1].Value = "Quarterly Summary";
            sheet.Cells[3, 1, 3, 4].Merge = true;
            sheet.Cells[3, 1, 3, 4].Style.Font.Bold = true;
            sheet.Cells[3, 1, 3, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[3, 1, 3, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

            // Summary data
            sheet.Cells[4, 1].Value = "Total Patient Days:";
            sheet.Cells[4, 2].Value = reportData.TotalPatientDays;
            sheet.Cells[4, 1, 4, 2].Style.Font.Bold = true;

            sheet.Cells[5, 1].Value = "Total HAI Cases:";
            sheet.Cells[5, 2].Value = reportData.MonthlyReports.Sum(r => r.OverallHaiRate.CaseCount);
            sheet.Cells[5, 1, 5, 2].Style.Font.Bold = true;

            sheet.Cells[6, 1].Value = "Quarterly HAI Rate:";
            sheet.Cells[6, 2].Value = reportData.QuarterlyRate.ToString("0.00") + "%";
            sheet.Cells[6, 1, 6, 2].Style.Font.Bold = true;

            // Highlight the rate
            sheet.Cells[6, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[6, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);

            // Auto-fit the columns
            sheet.Cells.AutoFitColumns();

            await Task.CompletedTask;
        }

        private async Task FormatMonthlyBreakdownSheetAsync(ExcelWorksheet sheet, QuarterlyHaiReportData reportData)
        {
            // Set the title
            sheet.Cells[1, 1].Value = $"Monthly Breakdown - Q{reportData.Quarter} {reportData.Year}";
            sheet.Cells[1, 1, 1, 5].Merge = true;
            sheet.Cells[1, 1, 1, 5].Style.Font.Bold = true;
            sheet.Cells[1, 1, 1, 5].Style.Font.Size = 14;

            // Header row
            sheet.Cells[3, 1].Value = "Month";
            sheet.Cells[3, 2].Value = "Total HAI Cases";
            sheet.Cells[3, 3].Value = "Patient Days";
            sheet.Cells[3, 4].Value = "HAI Rate";
            sheet.Cells[3, 5].Value = "Trend";
            sheet.Cells[3, 1, 3, 5].Style.Font.Bold = true;
            sheet.Cells[3, 1, 3, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[3, 1, 3, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

            // Add the data rows
            int row = 4;
            decimal? previousRate = null;
            foreach (var report in reportData.MonthlyReports)
            {
                // Basic data
                sheet.Cells[row, 1].Value = report.MonthName;
                sheet.Cells[row, 2].Value = report.OverallHaiRate.CaseCount;
                sheet.Cells[row, 3].Value = report.OverallHaiRate.DeviceDays;
                sheet.Cells[row, 4].Value = report.OverallHaiRate.FormattedRate;

                // Add trend indicator
                if (previousRate.HasValue)
                {
                    if (report.OverallHaiRate.Rate > previousRate.Value)
                    {
                        sheet.Cells[row, 5].Value = "↑";
                        sheet.Cells[row, 5].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                    }
                    else if (report.OverallHaiRate.Rate < previousRate.Value)
                    {
                        sheet.Cells[row, 5].Value = "↓";
                        sheet.Cells[row, 5].Style.Font.Color.SetColor(System.Drawing.Color.Green);
                    }
                    else
                    {
                        sheet.Cells[row, 5].Value = "→";
                    }
                }
                else
                {
                    sheet.Cells[row, 5].Value = "--";
                }

                previousRate = report.OverallHaiRate.Rate;
                row++;
            }

            // Add quarterly totals
            sheet.Cells[row, 1].Value = "Quarter Total";
            sheet.Cells[row, 2].Value = reportData.MonthlyReports.Sum(r => r.OverallHaiRate.CaseCount);
            sheet.Cells[row, 3].Value = reportData.TotalPatientDays;
            sheet.Cells[row, 4].Value = reportData.QuarterlyRate.ToString("0.00") + "%";
            sheet.Cells[row, 1, row, 5].Style.Font.Bold = true;
            sheet.Cells[row, 1, row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[row, 1, row, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

            // Auto-fit the columns
            sheet.Cells.AutoFitColumns();

            await Task.CompletedTask;
        }

        private async Task FormatDeviceSummarySheetAsync(ExcelWorksheet sheet, QuarterlyHaiReportData reportData)
        {
            // Set the title
            sheet.Cells[1, 1].Value = $"Device-Associated Infections Summary - Q{reportData.Quarter} {reportData.Year}";
            sheet.Cells[1, 1, 1, 5].Merge = true;
            sheet.Cells[1, 1, 1, 5].Style.Font.Bold = true;
            sheet.Cells[1, 1, 1, 5].Style.Font.Size = 14;

            // Get unique infection types
            var deviceInfections = reportData.MonthlyReports.First().DeviceAssociatedInfections.Select(i => i.InfectionType).ToList();

            // Header row
            sheet.Cells[3, 1].Value = "Infection Type";
            int col = 2;
            foreach (var report in reportData.MonthlyReports)
            {
                sheet.Cells[3, col++].Value = report.MonthName;
            }
            sheet.Cells[3, col].Value = "Quarter Total";
            sheet.Cells[3, 1, 3, col].Style.Font.Bold = true;
            sheet.Cells[3, 1, 3, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[3, 1, 3, col].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);

            // Add the data rows
            int row = 4;
            foreach (var infectionType in deviceInfections)
            {
                sheet.Cells[row, 1].Value = infectionType;
                col = 2;
                foreach (var report in reportData.MonthlyReports)
                {
                    var infection = report.DeviceAssociatedInfections.FirstOrDefault(i => i.InfectionType == infectionType);
                    sheet.Cells[row, col++].Value = infection?.CaseCount ?? 0;
                }
                sheet.Cells[row, col].Value = reportData.MonthlyReports.Sum(r =>
                    r.DeviceAssociatedInfections.FirstOrDefault(i => i.InfectionType == infectionType)?.CaseCount ?? 0);
                sheet.Cells[row, col].Style.Font.Bold = true;
                row++;
            }

            // Add row for totals
            sheet.Cells[row, 1].Value = "Total";
            sheet.Cells[row, 1].Style.Font.Bold = true;
            col = 2;
            foreach (var report in reportData.MonthlyReports)
            {
                sheet.Cells[row, col++].Value = report.DeviceAssociatedInfections.Sum(i => i.CaseCount);
            }
            sheet.Cells[row, col].Value = reportData.MonthlyReports.Sum(r => r.DeviceAssociatedInfections.Sum(i => i.CaseCount));
            sheet.Cells[row, 1, row, col].Style.Font.Bold = true;
            sheet.Cells[row, 1, row, col].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[row, 1, row, col].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

            // Auto-fit the columns
            sheet.Cells.AutoFitColumns();

            await Task.CompletedTask;
        }

        private async Task FormatQuarterlyDetailsSheetAsync(ExcelWorksheet sheet, QuarterlyHaiReportData reportData)
        {
            // Set the title
            sheet.Cells[1, 1].Value = $"Detailed HAI Data - Q{reportData.Quarter} {reportData.Year}";
            sheet.Cells[1, 1, 1, 5].Merge = true;
            sheet.Cells[1, 1, 1, 5].Style.Font.Bold = true;
            sheet.Cells[1, 1, 1, 5].Style.Font.Size = 14;

            int row = 3;

            // Process each month's data separately
            foreach (var monthlyReport in reportData.MonthlyReports)
            {
                // Month header
                sheet.Cells[row, 1].Value = $"{monthlyReport.MonthName} {reportData.Year} Data";
                sheet.Cells[row, 1, row, 5].Merge = true;
                sheet.Cells[row, 1, row, 5].Style.Font.Bold = true;
                sheet.Cells[row, 1, row, 5].Style.Font.Size = 12;
                sheet.Cells[row, 1, row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells[row, 1, row, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSteelBlue);
                row++;

                // Device-Associated Infections for this month
                sheet.Cells[row, 1].Value = "Device-Associated Infections";
                sheet.Cells[row, 1, row, 5].Merge = true;
                sheet.Cells[row, 1, row, 5].Style.Font.Bold = true;
                sheet.Cells[row, 1, row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells[row, 1, row, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                // Header row for Device-Associated
                row++;
                sheet.Cells[row, 1].Value = "Type";
                sheet.Cells[row, 2].Value = "Cases";
                sheet.Cells[row, 3].Value = "Device Days";
                sheet.Cells[row, 4].Value = "Rate (per 1000 device days)";
                sheet.Cells[row, 5].Value = "Benchmark";
                sheet.Cells[row, 1, row, 5].Style.Font.Bold = true;

                // Add device-associated data for this month
                foreach (var infection in monthlyReport.DeviceAssociatedInfections)
                {
                    row++;
                    sheet.Cells[row, 1].Value = infection.InfectionType;
                    sheet.Cells[row, 2].Value = infection.CaseCount;
                    sheet.Cells[row, 3].Value = infection.DeviceDays;
                    sheet.Cells[row, 4].Value = infection.FormattedRate;
                    sheet.Cells[row, 5].Value = ""; // Benchmark would be provided if available
                }

                // Ventilator-Associated Events for this month
                row += 2;
                sheet.Cells[row, 1].Value = "Ventilator-Associated Events";
                sheet.Cells[row, 1, row, 5].Merge = true;
                sheet.Cells[row, 1, row, 5].Style.Font.Bold = true;
                sheet.Cells[row, 1, row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells[row, 1, row, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightCyan);

                // Header row for Ventilator-Associated Events
                row++;
                sheet.Cells[row, 1].Value = "Type";
                sheet.Cells[row, 2].Value = "Cases";
                sheet.Cells[row, 3].Value = "Ventilator Days";
                sheet.Cells[row, 4].Value = "Rate (per 1000 ventilator days)";
                sheet.Cells[row, 5].Value = "Device Count Name";
                sheet.Cells[row, 1, row, 5].Style.Font.Bold = true;

                // Add ventilator-associated event data for this month
                foreach (var infection in monthlyReport.VentilatorAssociatedEvents)
                {
                    row++;
                    sheet.Cells[row, 1].Value = infection.InfectionType;
                    sheet.Cells[row, 2].Value = infection.CaseCount;
                    sheet.Cells[row, 3].Value = infection.DeviceDays;
                    sheet.Cells[row, 4].Value = infection.FormattedRate;
                    sheet.Cells[row, 5].Value = "MV";
                }

                // Non-Device-Associated Infections for this month
                row += 2;
                sheet.Cells[row, 1].Value = "Non-Device-Associated Infections";
                sheet.Cells[row, 1, row, 5].Merge = true;
                sheet.Cells[row, 1, row, 5].Style.Font.Bold = true;
                sheet.Cells[row, 1, row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells[row, 1, row, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);

                // Header row for Non-Device-Associated
                row++;
                sheet.Cells[row, 1].Value = "Type";
                sheet.Cells[row, 2].Value = "Cases";
                sheet.Cells[row, 3].Value = "Patient Days";
                sheet.Cells[row, 4].Value = "Rate (per 100 patient days)";
                sheet.Cells[row, 5].Value = "Benchmark";
                sheet.Cells[row, 1, row, 5].Style.Font.Bold = true;

                // Add non-device-associated data for this month
                foreach (var infection in monthlyReport.NonDeviceAssociatedInfections)
                {
                    row++;
                    sheet.Cells[row, 1].Value = infection.InfectionType;
                    sheet.Cells[row, 2].Value = infection.CaseCount;
                    sheet.Cells[row, 3].Value = infection.DeviceDays;
                    sheet.Cells[row, 4].Value = infection.FormattedRate;
                    sheet.Cells[row, 5].Value = ""; // Benchmark would be provided if available
                }

                // Site-Specific Infections for this month
                row += 2;
                sheet.Cells[row, 1].Value = "Site-Specific Infections";
                sheet.Cells[row, 1, row, 5].Merge = true;
                sheet.Cells[row, 1, row, 5].Style.Font.Bold = true;
                sheet.Cells[row, 1, row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells[row, 1, row, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightPink);

                // Header row for Site-Specific
                row++;
                sheet.Cells[row, 1].Value = "Type";
                sheet.Cells[row, 2].Value = "Cases";
                sheet.Cells[row, 3].Value = "Patient Days";
                sheet.Cells[row, 4].Value = "Rate (per 100 patient days)";
                sheet.Cells[row, 5].Value = "Benchmark";
                sheet.Cells[row, 1, row, 5].Style.Font.Bold = true;

                // Add site-specific data for this month
                foreach (var infection in monthlyReport.SiteSpecificInfections)
                {
                    row++;
                    sheet.Cells[row, 1].Value = infection.InfectionType;
                    sheet.Cells[row, 2].Value = infection.CaseCount;
                    sheet.Cells[row, 3].Value = infection.DeviceDays;
                    sheet.Cells[row, 4].Value = infection.FormattedRate;
                    sheet.Cells[row, 5].Value = ""; // Benchmark would be provided if available
                }

                // Monthly summary
                row += 2;
                sheet.Cells[row, 1].Value = $"{monthlyReport.MonthName} Summary";
                sheet.Cells[row, 1, row, 5].Merge = true;
                sheet.Cells[row, 1, row, 5].Style.Font.Bold = true;
                sheet.Cells[row, 1, row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells[row, 1, row, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGoldenrodYellow);

                row++;
                sheet.Cells[row, 1].Value = "Total HAI Cases:";
                sheet.Cells[row, 2].Value = monthlyReport.OverallHaiRate.CaseCount;
                sheet.Cells[row, 1, row, 2].Style.Font.Bold = true;

                row++;
                sheet.Cells[row, 1].Value = "Total Patient Days:";
                sheet.Cells[row, 2].Value = monthlyReport.OverallHaiRate.DeviceDays;
                sheet.Cells[row, 1, row, 2].Style.Font.Bold = true;

                row++;
                sheet.Cells[row, 1].Value = "Monthly HAI Rate:";
                sheet.Cells[row, 2].Value = monthlyReport.OverallHaiRate.FormattedRate;
                sheet.Cells[row, 1, row, 2].Style.Font.Bold = true;
                sheet.Cells[row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                sheet.Cells[row, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);

                // Add a separator between months
                row += 2;
            }

            // Add quarterly summary section at the end
            row += 1;
            sheet.Cells[row, 1].Value = "Quarterly Summary";
            sheet.Cells[row, 1, row, 5].Merge = true;
            sheet.Cells[row, 1, row, 5].Style.Font.Bold = true;
            sheet.Cells[row, 1, row, 5].Style.Font.Size = 12;
            sheet.Cells[row, 1, row, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[row, 1, row, 5].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.MediumAquamarine);

            row++;
            sheet.Cells[row, 1].Value = "Total HAI Cases:";
            sheet.Cells[row, 2].Value = reportData.MonthlyReports.Sum(r => r.OverallHaiRate.CaseCount);
            sheet.Cells[row, 1, row, 2].Style.Font.Bold = true;

            row++;
            sheet.Cells[row, 1].Value = "Total Patient Days:";
            sheet.Cells[row, 2].Value = reportData.TotalPatientDays;
            sheet.Cells[row, 1, row, 2].Style.Font.Bold = true;

            row++;
            sheet.Cells[row, 1].Value = "Quarterly HAI Rate:";
            sheet.Cells[row, 2].Value = reportData.QuarterlyRate.ToString("0.00") + "%";
            sheet.Cells[row, 1, row, 2].Style.Font.Bold = true;
            sheet.Cells[row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[row, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);

            // Add notes
            row += 2;
            sheet.Cells[row, 1].Value = "Notes:";
            sheet.Cells[row, 1].Style.Font.Bold = true;

            row++;
            sheet.Cells[row, 1].Value = "1. Device-associated rates are calculated per 1,000 device days";
            sheet.Cells[row, 1, row, 5].Merge = true;

            row++;
            sheet.Cells[row, 1].Value = "2. Non-device and site-specific rates are calculated per 100 patient days";
            sheet.Cells[row, 1, row, 5].Merge = true;

            row++;
            sheet.Cells[row, 1].Value = "3. Report generated on " + DateTime.Now.ToString("MM/dd/yyyy");
            sheet.Cells[row, 1, row, 5].Merge = true;

            // Auto-fit the columns
            sheet.Cells.AutoFitColumns();

            await Task.CompletedTask;
        }

        private void AddDataRow(ExcelWorksheet sheet, int row, HaiCaseData data)
        {
            sheet.Cells[row, 1].Value = data.InfectionType;
            sheet.Cells[row, 2].Value = data.CaseCount;
            sheet.Cells[row, 3].Value = data.DeviceDays;
            sheet.Cells[row, 4].Value = data.FormattedRate;
        }

        public async Task<byte[]> ExportDashboardSummaryAsync(List<MonthlyHaiReportData> monthlyReports, int year)
        {
            using (var package = new ExcelPackage())
            {
                // Create the dashboard worksheet
                var dashboardSheet = package.Workbook.Worksheets.Add("HAI Dashboard");

                // Format the dashboard sheet
                await FormatDashboardSheetAsync(dashboardSheet, monthlyReports, year);

                // Return the Excel package as a byte array
                return await package.GetAsByteArrayAsync();
            }
        }

        private async Task FormatDashboardSheetAsync(ExcelWorksheet sheet, List<MonthlyHaiReportData> monthlyReports, int year)
        {
            // Set the title
            sheet.Cells[1, 1].Value = $"Healthcare-Associated Infections Dashboard - {year}";
            sheet.Cells[1, 1, 1, 12].Merge = true;
            sheet.Cells[1, 1, 1, 12].Style.Font.Bold = true;
            sheet.Cells[1, 1, 1, 12].Style.Font.Size = 16;
            sheet.Cells[1, 1, 1, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Add current date
            sheet.Cells[2, 1].Value = $"Generated on: {DateTime.Now.ToString("MMMM dd, yyyy")}";
            sheet.Cells[2, 1, 2, 12].Merge = true;
            sheet.Cells[2, 1, 2, 12].Style.Font.Italic = true;
            sheet.Cells[2, 1, 2, 12].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Overall summary section
            int row = 4;
            sheet.Cells[row, 1].Value = "Annual Summary";
            sheet.Cells[row, 1, row, 12].Merge = true;
            sheet.Cells[row, 1, row, 12].Style.Font.Bold = true;
            sheet.Cells[row, 1, row, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[row, 1, row, 12].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

            // Calculate annual totals
            int totalCases = monthlyReports.Sum(r => r.OverallHaiRate.CaseCount);
            int totalPatientDays = monthlyReports.Sum(r => r.OverallHaiRate.DeviceDays);
            decimal annualRate = totalPatientDays > 0 ? (decimal)totalCases * 100 / totalPatientDays : 0;

            // Add annual summary data
            row++;
            sheet.Cells[row, 1].Value = "Total HAI Cases:";
            sheet.Cells[row, 2].Value = totalCases;
            sheet.Cells[row, 1, row, 2].Style.Font.Bold = true;

            row++;
            sheet.Cells[row, 1].Value = "Total Patient Days:";
            sheet.Cells[row, 2].Value = totalPatientDays;
            sheet.Cells[row, 1, row, 2].Style.Font.Bold = true;

            row++;
            sheet.Cells[row, 1].Value = "Annual HAI Rate:";
            sheet.Cells[row, 2].Value = annualRate.ToString("0.00") + "%";
            sheet.Cells[row, 1, row, 2].Style.Font.Bold = true;
            sheet.Cells[row, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[row, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightYellow);

            // Monthly trend section
            row += 2;
            sheet.Cells[row, 1].Value = "Monthly HAI Trends";
            sheet.Cells[row, 1, row, 12].Merge = true;
            sheet.Cells[row, 1, row, 12].Style.Font.Bold = true;
            sheet.Cells[row, 1, row, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[row, 1, row, 12].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);

            // Header row for monthly trends
            row++;
            sheet.Cells[row, 1].Value = "Month";
            sheet.Cells[row, 2].Value = "HAI Cases";
            sheet.Cells[row, 3].Value = "Patient Days";
            sheet.Cells[row, 4].Value = "HAI Rate";
            sheet.Cells[row, 5].Value = "Device-Associated";
            sheet.Cells[row, 6].Value = "Non-Device";
            sheet.Cells[row, 7].Value = "SSI";
            sheet.Cells[row, 1, row, 7].Style.Font.Bold = true;

            // Add monthly data
            foreach (var report in monthlyReports.OrderBy(r => r.Month))
            {
                row++;
                sheet.Cells[row, 1].Value = report.MonthName;
                sheet.Cells[row, 2].Value = report.OverallHaiRate.CaseCount;
                sheet.Cells[row, 3].Value = report.OverallHaiRate.DeviceDays;
                sheet.Cells[row, 4].Value = report.OverallHaiRate.FormattedRate;
                sheet.Cells[row, 5].Value = report.DeviceAssociatedTotal.CaseCount;
                sheet.Cells[row, 6].Value = report.NonDeviceAssociatedTotal.CaseCount;
                sheet.Cells[row, 7].Value = report.SsiTotal.CaseCount;

                // Highlight high rates
                if (report.OverallHaiRate.Rate > annualRate * 1.1m) // 10% above annual rate
                {
                    sheet.Cells[row, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    sheet.Cells[row, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightPink);
                }
            }

            // Infection type breakdown section
            row += 2;
            sheet.Cells[row, 1].Value = "Top 5 Infection Types";
            sheet.Cells[row, 1, row, 7].Merge = true;
            sheet.Cells[row, 1, row, 7].Style.Font.Bold = true;
            sheet.Cells[row, 1, row, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
            sheet.Cells[row, 1, row, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSalmon);

            // Header for infection types
            row++;
            sheet.Cells[row, 1].Value = "Infection Type";
            sheet.Cells[row, 2].Value = "Total Cases";
            sheet.Cells[row, 3].Value = "% of All Infections";
            sheet.Cells[row, 1, row, 3].Style.Font.Bold = true;

            // Collect all infection types and their total cases
            var allInfections = new Dictionary<string, int>();

            foreach (var report in monthlyReports)
            {
                // Device-associated
                foreach (var infection in report.DeviceAssociatedInfections)
                {
                    if (!allInfections.ContainsKey(infection.InfectionType))
                        allInfections[infection.InfectionType] = 0;
                    allInfections[infection.InfectionType] += infection.CaseCount;
                }

                // Non-device
                foreach (var infection in report.NonDeviceAssociatedInfections)
                {
                    if (!allInfections.ContainsKey(infection.InfectionType))
                        allInfections[infection.InfectionType] = 0;
                    allInfections[infection.InfectionType] += infection.CaseCount;
                }

                // Site-specific
                foreach (var infection in report.SiteSpecificInfections)
                {
                    if (!allInfections.ContainsKey(infection.InfectionType))
                        allInfections[infection.InfectionType] = 0;
                    allInfections[infection.InfectionType] += infection.CaseCount;
                }
            }

            // Get top 5 infection types
            var topInfections = allInfections.OrderByDescending(i => i.Value).Take(5);

            // Add top infection types
            foreach (var infection in topInfections)
            {
                row++;
                decimal percentage = totalCases > 0 ? (decimal)infection.Value * 100 / totalCases : 0;

                sheet.Cells[row, 1].Value = infection.Key;
                sheet.Cells[row, 2].Value = infection.Value;
                sheet.Cells[row, 3].Value = percentage.ToString("0.0") + "%";
            }

            // Auto-fit the columns
            sheet.Cells.AutoFitColumns();

            await Task.CompletedTask;
        }
    }
}