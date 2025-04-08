using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using IPCU.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace IPCU.Services
{
    public class DeviceMonitoringAreaYearlyExcelService
    {
        public byte[] GenerateAreaHalfYearlyExcel(List<DeviceMonitoringReportData> monthlyReports, int halfYear)
        {
            // Sort reports by month
            var sortedReports = monthlyReports.OrderBy(r => r.Area).ThenBy(r => r.Month).ToList();

            // Determine which months to include based on the halfYear parameter
            int startMonth = halfYear == 1 ? 1 : 7;
            int endMonth = halfYear == 1 ? 6 : 12;
            string halfYearLabel = halfYear == 1 ? "First Half" : "Second Half";
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

            using (var package = new ExcelPackage())
            {
                // Create a summary sheet first
                var summarySheet = package.Workbook.Worksheets.Add("Summary");
                CreateSummarySheet(summarySheet, sortedReports, startMonth, endMonth, halfYearLabel);

                // Create separate sheets for each month
                for (int month = startMonth; month <= endMonth; month++)
                {
                    // Get reports for this month
                    var monthReports = sortedReports.Where(r => r.Month == month).ToList();
                    if (monthReports.Any())
                    {
                        // Create the worksheet for this month
                        var worksheet = package.Workbook.Worksheets.Add(months[month - 1]);
                        CreateMonthSheet(worksheet, monthReports, month);
                    }
                }

                // Save the Excel package to a byte array
                return package.GetAsByteArray();
            }
        }

        // Keep the original method for backward compatibility
        public byte[] GenerateAreaYearlyExcel(List<DeviceMonitoringReportData> monthlyReports)
        {
            // Generate full year report by splitting into month sheets
            return GenerateAreaHalfYearlyExcel(monthlyReports, 1); // Default to first half if using the old method
        }

        // Helper method to create the summary sheet
        private void CreateSummarySheet(ExcelWorksheet worksheet, List<DeviceMonitoringReportData> reports, int startMonth, int endMonth, string halfYearLabel)
        {
            var areas = GetDefinedAreas();
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            string[] selectedMonths = months.Skip(startMonth - 1).Take(endMonth - startMonth + 1).ToArray();

            // Set column widths
            worksheet.DefaultColWidth = 12;
            worksheet.Column(1).Width = 20; // Unit/Ward column wider for area names

            // Add title
            worksheet.Cells[1, 1].Value = $"{halfYearLabel} Summary Report";
            worksheet.Cells[1, 1, 1, selectedMonths.Length + 1].Merge = true;
            worksheet.Cells[1, 1, 1, selectedMonths.Length + 1].Style.Font.Bold = true;
            worksheet.Cells[1, 1, 1, selectedMonths.Length + 1].Style.Font.Size = 14;
            worksheet.Cells[1, 1, 1, selectedMonths.Length + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Create header row
            int row = 3;
            worksheet.Cells[row, 1].Value = "Unit/Ward";
            worksheet.Cells[row, 1].Style.Font.Bold = true;

            // Add month columns
            for (int i = 0; i < selectedMonths.Length; i++)
            {
                worksheet.Cells[row, i + 2].Value = selectedMonths[i];
                worksheet.Cells[row, i + 2].Style.Font.Bold = true;
            }

            // Style header row
            worksheet.Cells[row, 1, row, selectedMonths.Length + 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[row, 1, row, selectedMonths.Length + 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            worksheet.Cells[row, 1, row, selectedMonths.Length + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);

            // Populate data
            row++;
            foreach (var area in areas)
            {
                worksheet.Cells[row, 1].Value = area;
                worksheet.Cells[row, 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);

                // For each month, show total patient count (from end of month)
                for (int month = startMonth; month <= endMonth; month++)
                {
                    int col = month - startMonth + 2;
                    var monthReport = reports.FirstOrDefault(r => r.Month == month && r.Area == area);

                    if (monthReport != null && monthReport.DailyData != null && monthReport.DailyData.Any())
                    {
                        var monthData = CalculateMonthlyData(monthReport.DailyData, month, monthReport.Year);
                        worksheet.Cells[row, col].Value = monthData.TotalCount;
                    }
                    else
                    {
                        worksheet.Cells[row, col].Value = 0;
                    }

                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                row++;
            }

            // Add navigation instructions
            row += 2;
            worksheet.Cells[row, 1].Value = "Click on the tabs below to view detailed data for each month";
            worksheet.Cells[row, 1, row, selectedMonths.Length + 1].Merge = true;
            worksheet.Cells[row, 1].Style.Font.Italic = true;

            // Auto-fit columns
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
        }

        // Helper method to create individual month sheets
        private void CreateMonthSheet(ExcelWorksheet worksheet, List<DeviceMonitoringReportData> monthReports, int month)
        {
            string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            string monthName = months[month - 1];

            // Set column widths
            worksheet.DefaultColWidth = 12;
            worksheet.Column(1).Width = 20; // Unit/Ward column wider for area names

            // Create the header row
            int row = 1;
            int col = 1;

            // Add title
            worksheet.Cells[row, col].Value = $"{monthName} Report";
            worksheet.Cells[row, col, row, 14].Merge = true;
            worksheet.Cells[row, col, row, 14].Style.Font.Bold = true;
            worksheet.Cells[row, col, row, 14].Style.Font.Size = 14;
            worksheet.Cells[row, col, row, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            row += 2;

            // Add column headers
            worksheet.Cells[row, col++].Value = "Unit/Ward";
            worksheet.Cells[row, col++].Value = "First Day";
            worksheet.Cells[row, col++].Value = "Next Month";
            worksheet.Cells[row, col++].Value = "Adm";
            worksheet.Cells[row, col++].Value = "T-in";
            worksheet.Cells[row, col++].Value = "DC";
            worksheet.Cells[row, col++].Value = "Mor";
            worksheet.Cells[row, col++].Value = "T-out";
            worksheet.Cells[row, col++].Value = "Total";
            worksheet.Cells[row, col++].Value = "IUC Non-KT";
            worksheet.Cells[row, col++].Value = "IUC KT";
            worksheet.Cells[row, col++].Value = "CL Non-HD";
            worksheet.Cells[row, col++].Value = "CL HD";
            worksheet.Cells[row, col++].Value = "MV";

            // Style header row
            worksheet.Cells[3, 1, 3, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[3, 1, 3, 14].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            worksheet.Cells[3, 1, 3, 14].Style.Font.Bold = true;
            worksheet.Cells[3, 1, 3, 14].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            worksheet.Cells[3, 1, 3, 14].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Process data for each area
            var areas = GetDefinedAreas();
            row = 4;

            foreach (var area in areas)
            {
                col = 1;
                var report = monthReports.FirstOrDefault(r => r.Area == area);

                // Write area name
                worksheet.Cells[row, col++].Value = area;

                if (report != null && report.DailyData != null && report.DailyData.Any())
                {
                    // Calculate monthly summary data
                    var monthData = CalculateMonthlyData(report.DailyData, month, report.Year);

                    // Fill in the data for this month
                    worksheet.Cells[row, col++].Value = monthData.FirstDayCount;
                    worksheet.Cells[row, col++].Value = monthData.NextMonthCount;
                    worksheet.Cells[row, col++].Value = monthData.AdmissionCount;
                    worksheet.Cells[row, col++].Value = monthData.TransferInCount;
                    worksheet.Cells[row, col++].Value = monthData.SentHomeCount;
                    worksheet.Cells[row, col++].Value = monthData.MortalityCount;
                    worksheet.Cells[row, col++].Value = monthData.TransferOutCount;
                    worksheet.Cells[row, col++].Value = monthData.TotalCount;
                    worksheet.Cells[row, col++].Value = monthData.IUCNonKTCount;
                    worksheet.Cells[row, col++].Value = monthData.IUCKTCount;
                    worksheet.Cells[row, col++].Value = monthData.CLNonHDCount;
                    worksheet.Cells[row, col++].Value = monthData.CLHDCount;
                    worksheet.Cells[row, col++].Value = monthData.MVCount;
                }
                else
                {
                    // No data for this month, fill with zeros
                    for (int i = 0; i < 13; i++)
                    {
                        worksheet.Cells[row, col++].Value = 0;
                    }
                }

                row++;
            }

            // Add a "Total" row at the bottom
            col = 1;
            worksheet.Cells[row, col++].Value = "Total";
            worksheet.Cells[row, 1].Style.Font.Bold = true;

            // Calculate and insert totals for each column
            for (int c = 2; c <= 14; c++)
            {
                // Get the column letter
                string colLetter = GetExcelColumnName(c);

                // Create a formula to sum the column (excluding header rows)
                int dataStartRow = 4; // First row with actual data
                int dataEndRow = row - 1; // Last row with actual data

                string formula = $"=SUM({colLetter}{dataStartRow}:{colLetter}{dataEndRow})";
                worksheet.Cells[row, c].Formula = formula;
                worksheet.Cells[row, c].Style.Font.Bold = true;
            }

            // Apply borders to all data cells
            worksheet.Cells[3, 1, row, 14].Style.Border.BorderAround(ExcelBorderStyle.Thin);

            // Apply alternating row colors for better readability
            for (int r = 4; r < row; r++)
            {
                if (r % 2 == 0)
                {
                    worksheet.Cells[r, 1, r, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[r, 1, r, 14].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 240, 240));
                }
            }

            // Format the total row
            worksheet.Cells[row, 1, row, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[row, 1, row, 14].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            worksheet.Cells[row, 1, row, 14].Style.Border.BorderAround(ExcelBorderStyle.Thin);

            // Auto-fit columns for better readability
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
        }

        // Helper method to get the predefined list of areas
        private List<string> GetDefinedAreas()
        {
            // Use the predefined list of areas
            return new List<string>
            {
                "Unit 2A", "Unit 2B", "Unit 2C", "Unit 2D", "Unit 2E", "Unit 2F", "Unit 2G", "Unit 2H",
                "2B Extension", "2D Extension", "2E Extension", "Unit 3A", "Unit 3B", "Unit 3C", "Unit 3D",
                "Unit 3E", "Unit 3F", "CTU", "PDU", "CCRU", "Cardiology", "Laboratory", "Radiology",
                "Rehab", "OR-RR", "OPS", "AITU", "IVASC", "AUEC", "ICU/IMCU", "ER", "PULMO",
                "HD Annex", "HD Main"
            };
        }

        // Helper method to calculate monthly summary data
        private MonthlyDataSummary CalculateMonthlyData(List<DailyDeviceData> dailyData, int month, int year)
        {
            var summary = new MonthlyDataSummary();

            if (dailyData.Count == 0)
                return summary;

            // Find first day of the month data
            var firstDay = new DateTime(year, month, 1);
            var firstDayData = dailyData.FirstOrDefault(d => d.Date.Date == firstDay.Date);

            if (firstDayData != null)
            {
                // Calculate patient count on first day
                summary.FirstDayCount = firstDayData.IUCNonKTCount + firstDayData.IUCKTCount +
                                        firstDayData.CLNonHDCount + firstDayData.CLHDCount +
                                        firstDayData.MVCount;
            }
            else if (dailyData.Any())
            {
                // If first day not found, use the earliest available day
                var earliestData = dailyData.OrderBy(d => d.Date).First();
                summary.FirstDayCount = earliestData.IUCNonKTCount + earliestData.IUCKTCount +
                                       earliestData.CLNonHDCount + earliestData.CLHDCount +
                                       earliestData.MVCount;
            }

            // Find last day of the month data
            var lastDay = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            var lastDayData = dailyData.FirstOrDefault(d => d.Date.Date == lastDay.Date);

            if (lastDayData != null)
            {
                // Calculate patient count on last day
                summary.NextMonthCount = lastDayData.IUCNonKTCount + lastDayData.IUCKTCount +
                                         lastDayData.CLNonHDCount + lastDayData.CLHDCount +
                                         lastDayData.MVCount;
            }
            else if (dailyData.Any())
            {
                // If last day not found, use the latest available day
                var latestData = dailyData.OrderByDescending(d => d.Date).First();
                summary.NextMonthCount = latestData.IUCNonKTCount + latestData.IUCKTCount +
                                        latestData.CLNonHDCount + latestData.CLHDCount +
                                        latestData.MVCount;
            }

            // Aggregate movement data for the month
            foreach (var dayData in dailyData)
            {
                summary.AdmissionCount += dayData.AdmissionCount;
                summary.TransferInCount += dayData.TransferInCount;
                summary.SentHomeCount += dayData.SentHomeCount;
                summary.MortalityCount += dayData.MortalityCount;
                summary.TransferOutCount += dayData.TransferOutCount;
            }

            // For device counts, use the LAST DAY's data instead of summing
            if (dailyData.Any())
            {
                var latestData = dailyData.OrderByDescending(d => d.Date).First();
                summary.IUCNonKTCount = latestData.IUCNonKTCount;
                summary.IUCKTCount = latestData.IUCKTCount;
                summary.CLNonHDCount = latestData.CLNonHDCount;
                summary.CLHDCount = latestData.CLHDCount;
                summary.MVCount = latestData.MVCount;
            }

            // Calculate total count
            summary.TotalCount = summary.IUCNonKTCount + summary.IUCKTCount +
                                 summary.CLNonHDCount + summary.CLHDCount +
                                 summary.MVCount;

            return summary;
        }

        // Helper method to convert column number to Excel column letter
        private string GetExcelColumnName(int columnNumber)
        {
            string columnName = "";

            while (columnNumber > 0)
            {
                int remainder = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + remainder) + columnName;
                columnNumber = (columnNumber - remainder) / 26;
            }

            return columnName;
        }

        // Class to hold monthly summary data
        private class MonthlyDataSummary
        {
            public int FirstDayCount { get; set; }
            public int NextMonthCount { get; set; }
            public int AdmissionCount { get; set; }
            public int TransferInCount { get; set; }
            public int SentHomeCount { get; set; }
            public int MortalityCount { get; set; }
            public int TransferOutCount { get; set; }
            public int TotalCount { get; set; }
            public int IUCNonKTCount { get; set; }
            public int IUCKTCount { get; set; }
            public int CLNonHDCount { get; set; }
            public int CLHDCount { get; set; }
            public int MVCount { get; set; }
        }
    }
}