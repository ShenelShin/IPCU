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

            using (var package = new ExcelPackage())
            {
                // Create the worksheet
                var worksheet = package.Workbook.Worksheets.Add($"{halfYearLabel} Report");

                // Set column widths
                worksheet.DefaultColWidth = 12;
                worksheet.Column(1).Width = 15; // Unit/Ward column

                // Create the header row
                int row = 1;
                int col = 1;

                // Add title
                worksheet.Cells[row, col].Value = "Unit/Ward";
                worksheet.Cells[row, col].Style.Font.Bold = true;
                worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[row, col].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                // Add month headers - only for the selected half of the year
                string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
                string[] selectedMonths = months.Skip(startMonth - 1).Take(6).ToArray();

                foreach (var month in selectedMonths)
                {
                    col++;
                    worksheet.Cells[row, col].Value = month;
                    worksheet.Cells[row, col].Style.Font.Bold = true;
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col, row, col + 16].Merge = true;
                    col += 16;
                }

                // Row 2 - Sub headers
                row++;
                col = 1;

                // Empty cell above unit/ward
                worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                col++;

                // Create sub headers for each month
                for (int month = startMonth; month <= endMonth; month++)
                {
                    // First column group - First Day/Month
                    worksheet.Cells[row, col].Value = "First Day";
                    worksheet.Cells[row, col, row, col + 1].Merge = true;
                    worksheet.Cells[row, col, row, col + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col, row, col + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[row, col, row, col + 1].Style.Font.Bold = true;
                    col += 2;

                    // Second column group - New Arrival
                    worksheet.Cells[row, col].Value = "New Arrival";
                    worksheet.Cells[row, col, row, col + 1].Merge = true;
                    worksheet.Cells[row, col, row, col + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col, row, col + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[row, col, row, col + 1].Style.Font.Bold = true;
                    col += 2;

                    // Third column group - # of DC
                    worksheet.Cells[row, col].Value = "# of DC";
                    worksheet.Cells[row, col, row, col + 2].Merge = true;
                    worksheet.Cells[row, col, row, col + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col, row, col + 2].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[row, col, row, col + 2].Style.Font.Bold = true;
                    col += 3;

                    // Fourth column - Total
                    worksheet.Cells[row, col].Value = "Total";
                    worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[row, col].Style.Font.Bold = true;
                    col += 1;

                    // Fifth column group - Px w/ IUC
                    worksheet.Cells[row, col].Value = "Px w/ IUC";
                    worksheet.Cells[row, col, row, col + 1].Merge = true;
                    worksheet.Cells[row, col, row, col + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col, row, col + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[row, col, row, col + 1].Style.Font.Bold = true;
                    col += 2;

                    // Sixth column group - Px w/ CL
                    worksheet.Cells[row, col].Value = "Px w/ CL";
                    worksheet.Cells[row, col, row, col + 1].Merge = true;
                    worksheet.Cells[row, col, row, col + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col, row, col + 1].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[row, col, row, col + 1].Style.Font.Bold = true;
                    col += 2;

                    // Seventh column - Px w/ MV
                    worksheet.Cells[row, col].Value = "Px w/ MV";
                    worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[row, col].Style.Font.Bold = true;
                    col += 1;
                }

                // Row 3 - Sub headers (continued)
                row++;
                col = 1;

                // Empty cell above unit/ward
                worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                col++;

                // Create detailed sub headers for each month
                for (int month = startMonth; month <= endMonth; month++)
                {
                    // First column group - First Day/Month
                    worksheet.Cells[row, col].Value = "of the Month";
                    worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    col++;

                    worksheet.Cells[row, col].Value = "Next Month";
                    worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    col++;

                    // Second column group - New Arrival
                    worksheet.Cells[row, col].Value = "Adm";
                    worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    col++;

                    worksheet.Cells[row, col].Value = "T-in";
                    worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    col++;

                    // Third column group - # of DC
                    worksheet.Cells[row, col].Value = "DC";
                    worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    col++;

                    worksheet.Cells[row, col].Value = "Mor";
                    worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    col++;

                    worksheet.Cells[row, col].Value = "T-out";
                    worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    col++;

                    // Fourth column - Total is already filled
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    col++;

                    // Fifth column group - Px w/ IUC
                    worksheet.Cells[row, col].Value = "Non-KT";
                    worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    col++;

                    worksheet.Cells[row, col].Value = "KT";
                    worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    col++;

                    // Sixth column group - Px w/ CL
                    worksheet.Cells[row, col].Value = "Non-HD";
                    worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    col++;

                    worksheet.Cells[row, col].Value = "HD";
                    worksheet.Cells[row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    col++;

                    // Seventh column - Px w/ MV is already filled
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    col++;
                }

                // Process data for each area
                var areas = GetDefinedAreas(); // Use the predefined list of areas

                foreach (var area in areas)
                {
                    row++;
                    col = 1;

                    // Write area name
                    worksheet.Cells[row, col].Value = area;
                    worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[row, col].Style.Font.Bold = true;
                    col++;

                    // Process each month for this area (only for the selected half-year)
                    for (int monthNumber = startMonth; monthNumber <= endMonth; monthNumber++)
                    {
                        var monthReport = sortedReports.FirstOrDefault(r => r.Month == monthNumber && r.Area == area);

                        if (monthReport != null && monthReport.DailyData != null && monthReport.DailyData.Any())
                        {
                            // Calculate monthly summary data
                            var monthData = CalculateMonthlyData(monthReport.DailyData, monthNumber, monthReport.Year);

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
                            // No data for this month, fill with zeros or empty cells
                            for (int i = 0; i < 13; i++)
                            {
                                worksheet.Cells[row, col++].Value = 0;
                            }
                        }

                        // Apply borders to all cells in this row
                        for (int i = col - 13; i < col; i++)
                        {
                            worksheet.Cells[row, i].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                            worksheet.Cells[row, i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }
                    }
                }

                // Add a "Total" row at the bottom
                row++;
                col = 1;
                worksheet.Cells[row, col].Value = "Total";
                worksheet.Cells[row, col].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                worksheet.Cells[row, col].Style.Font.Bold = true;
                col++;

                // Calculate and insert totals for each column
                int totalColumns = 6 * 13; // 6 months, 13 columns per month
                int startCol = 2;
                int endCol = startCol + totalColumns - 1;

                for (int c = startCol; c <= endCol; c++)
                {
                    // Get the column letter
                    string colLetter = GetExcelColumnName(c);

                    // Create a formula to sum the column (excluding header rows)
                    int dataStartRow = 4; // First row with actual data
                    int dataEndRow = row - 1; // Last row with actual data

                    string formula = $"=SUM({colLetter}{dataStartRow}:{colLetter}{dataEndRow})";
                    worksheet.Cells[row, c].Formula = formula;
                    worksheet.Cells[row, c].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                    worksheet.Cells[row, c].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[row, c].Style.Font.Bold = true;
                }

                // Apply header styling
                worksheet.Cells[1, 1, 3, col - 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[1, 1, 3, col - 1].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                // Apply alternating row colors for better readability
                for (int r = 4; r <= row; r++)
                {
                    if (r % 2 == 0)
                    {
                        worksheet.Cells[r, 1, r, col - 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[r, 1, r, col - 1].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(240, 240, 240));
                    }
                }

                // Save the Excel package to a byte array
                return package.GetAsByteArray();
            }
        }

        // Keep the original method for backward compatibility
        public byte[] GenerateAreaYearlyExcel(List<DeviceMonitoringReportData> monthlyReports)
        {
            // Generate full year report by combining both half-years
            return GenerateAreaHalfYearlyExcel(monthlyReports, 1); // Default to first half if using the old method
        }

        // Helper method to get the predefined list of areas
        private List<string> GetDefinedAreas()
        {
            // Use the predefined list of areas you provided
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
            // This fixes the problem where it was summing all values across all days
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