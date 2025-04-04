using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Drawing.Chart;
using IPCU.Models;
using OfficeOpenXml.Drawing;

namespace IPCU.Services
{
    public class DeviceMonitoringYearlyExcelService
    {
        public byte[] GenerateYearlyExcel(List<DeviceMonitoringReportData> monthlyReports)
        {
            // Set the license context for non-commercial use
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                // Create a single worksheet for all monthly data
                var worksheet = package.Workbook.Worksheets.Add("Monthly Device Data");

                // Assuming all monthly reports are for the same area and year
                var area = monthlyReports.FirstOrDefault()?.Area ?? "All Areas";
                var year = monthlyReports.FirstOrDefault()?.Year ?? DateTime.Now.Year;

                // Add header information
                worksheet.Cells[1, 1].Value = "Monthly Device Monitoring Report";
                worksheet.Cells[1, 1, 1, 11].Merge = true;
                worksheet.Cells[1, 1].Style.Font.Size = 16;
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells[2, 1].Value = $"Area: {area}";
                worksheet.Cells[3, 1].Value = $"Year: {year}";
                worksheet.Cells[2, 1].Style.Font.Bold = true;
                worksheet.Cells[3, 1].Style.Font.Bold = true;

                // Sort monthly reports by month
                var sortedReports = monthlyReports.OrderBy(r => r.Month).ToList();

                int currentRow = 5; // Starting row for the data
                var monthNames = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

                // Create headers and display data for each month
                foreach (var monthReport in sortedReports)
                {
                    if (monthReport.DailyData == null || !monthReport.DailyData.Any())
                        continue;

                    string monthName = monthNames[monthReport.Month - 1];

                    // Month header - fix by NOT merging A5:K5
                    worksheet.Cells[currentRow, 1].Value = monthName.ToUpper();
                    // Use a different range that doesn't conflict with A5:K5
                    if (currentRow != 5)
                    {
                        worksheet.Cells[currentRow, 1, currentRow, 11].Merge = true;
                    }
                    else
                    {
                        // For row 5, don't merge cells, just style the first cell
                        worksheet.Cells[currentRow, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[currentRow, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                        worksheet.Cells[currentRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    worksheet.Cells[currentRow, 1].Style.Font.Bold = true;
                    worksheet.Cells[currentRow, 1].Style.Font.Size = 14;
                    worksheet.Cells[currentRow, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[currentRow, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    worksheet.Cells[currentRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    currentRow += 1;

                    // Subheaders - avoid any merge conflicts
                    int headerRow = currentRow;

                    // Device Counts header section
                    worksheet.Cells[headerRow, 1].Value = "Date";

                    // Device Counts header - avoid merged ranges to fix conflict
                    worksheet.Cells[headerRow, 2].Value = "IUC Non-KT";
                    worksheet.Cells[headerRow, 3].Value = "IUC KT";
                    worksheet.Cells[headerRow, 4].Value = "CL Non-HD";
                    worksheet.Cells[headerRow, 5].Value = "CL HD";
                    worksheet.Cells[headerRow, 6].Value = "MV";

                    // Patient Movement columns
                    worksheet.Cells[headerRow, 7].Value = "Admissions";
                    worksheet.Cells[headerRow, 8].Value = "Transfers In";
                    worksheet.Cells[headerRow, 9].Value = "Sent Home";
                    worksheet.Cells[headerRow, 10].Value = "Mortality";
                    worksheet.Cells[headerRow, 11].Value = "Transfers Out";

                    // Format header row
                    using (var range = worksheet.Cells[headerRow, 1, headerRow, 11])
                    {
                        range.Style.Font.Bold = true;
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                        range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    }

                    // Add color to section headers after formatting
                    // Device section headers
                    using (var deviceRange = worksheet.Cells[headerRow, 2, headerRow, 6])
                    {
                        deviceRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        deviceRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                    }

                    // Patient movement section headers
                    using (var movementRange = worksheet.Cells[headerRow, 7, headerRow, 11])
                    {
                        movementRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        movementRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
                    }

                    currentRow += 1;

                    // Add daily data rows
                    var dailyDataStartRow = currentRow;
                    foreach (var daily in monthReport.DailyData.OrderBy(d => d.Date))
                    {
                        worksheet.Cells[currentRow, 1].Value = daily.Date.ToString("yyyy-MM-dd");
                        worksheet.Cells[currentRow, 1].Style.Numberformat.Format = "yyyy-mm-dd";

                        // Device counts
                        worksheet.Cells[currentRow, 2].Value = daily.IUCNonKTCount;
                        worksheet.Cells[currentRow, 3].Value = daily.IUCKTCount;
                        worksheet.Cells[currentRow, 4].Value = daily.CLNonHDCount;
                        worksheet.Cells[currentRow, 5].Value = daily.CLHDCount;
                        worksheet.Cells[currentRow, 6].Value = daily.MVCount;

                        // Patient movement
                        worksheet.Cells[currentRow, 7].Value = daily.AdmissionCount;
                        worksheet.Cells[currentRow, 8].Value = daily.TransferInCount;
                        worksheet.Cells[currentRow, 9].Value = daily.SentHomeCount;
                        worksheet.Cells[currentRow, 10].Value = daily.MortalityCount;
                        worksheet.Cells[currentRow, 11].Value = daily.TransferOutCount;

                        currentRow++;
                    }

                    // Add monthly totals/averages row
                    worksheet.Cells[currentRow, 1].Value = "MONTHLY TOTAL/AVG";
                    worksheet.Cells[currentRow, 1].Style.Font.Bold = true;

                    // Average device counts
                    for (int col = 2; col <= 6; col++)
                    {
                        worksheet.Cells[currentRow, col].Formula = $"AVERAGE({GetExcelColumnLetter(col)}{dailyDataStartRow}:{GetExcelColumnLetter(col)}{currentRow - 1})";
                        worksheet.Cells[currentRow, col].Style.Numberformat.Format = "0.00";
                        worksheet.Cells[currentRow, col].Style.Font.Bold = true;
                    }

                    // Sum patient movement
                    for (int col = 7; col <= 11; col++)
                    {
                        worksheet.Cells[currentRow, col].Formula = $"SUM({GetExcelColumnLetter(col)}{dailyDataStartRow}:{GetExcelColumnLetter(col)}{currentRow - 1})";
                        worksheet.Cells[currentRow, col].Style.Font.Bold = true;
                    }

                    // Format the totals row
                    using (var range = worksheet.Cells[currentRow, 1, currentRow, 11])
                    {
                        range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                    }

                    // Add a summary section for this month
                    currentRow += 2;
                    worksheet.Cells[currentRow, 1].Value = "Monthly Summary:";
                    worksheet.Cells[currentRow, 1].Style.Font.Bold = true;

                    currentRow += 1;
                    int totalRow = currentRow - 3; // Reference to the row with monthly totals

                    // Calculate total arrivals and departures
                    worksheet.Cells[currentRow, 1].Value = "Total Patient Arrivals:";
                    worksheet.Cells[currentRow, 2].Formula = $"{GetExcelColumnLetter(7)}{totalRow}+{GetExcelColumnLetter(8)}{totalRow}";
                    worksheet.Cells[currentRow, 2].Style.Font.Bold = true;

                    currentRow += 1;
                    worksheet.Cells[currentRow, 1].Value = "Total Patient Discharges:";
                    worksheet.Cells[currentRow, 2].Formula = $"{GetExcelColumnLetter(9)}{totalRow - 1}+{GetExcelColumnLetter(10)}{totalRow - 1}+{GetExcelColumnLetter(11)}{totalRow - 1}";
                    worksheet.Cells[currentRow, 2].Style.Font.Bold = true;

                    currentRow += 1;
                    worksheet.Cells[currentRow, 1].Value = "Net Patient Flow:";
                    worksheet.Cells[currentRow, 2].Formula = $"{GetExcelColumnLetter(2)}{currentRow - 2}-{GetExcelColumnLetter(2)}{currentRow - 1}";
                    worksheet.Cells[currentRow, 2].Style.Font.Bold = true;

                    // Format the summary section
                    using (var range = worksheet.Cells[currentRow - 2, 1, currentRow, 2])
                    {
                        range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    }

                    // Add space between months
                    currentRow += 2;
                }

                // Add a yearly summary at the end
                currentRow += 1;
                worksheet.Cells[currentRow, 1].Value = "YEARLY SUMMARY";
                worksheet.Cells[currentRow, 1, currentRow, 11].Merge = true;
                worksheet.Cells[currentRow, 1].Style.Font.Bold = true;
                worksheet.Cells[currentRow, 1].Style.Font.Size = 14;
                worksheet.Cells[currentRow, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[currentRow, 1].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.DarkGray);
                worksheet.Cells[currentRow, 1].Style.Font.Color.SetColor(System.Drawing.Color.White);
                worksheet.Cells[currentRow, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                currentRow += 1;

                // Calculate yearly device usage averages
                worksheet.Cells[currentRow, 1].Value = "Average Daily Device Usage:";
                worksheet.Cells[currentRow, 1].Style.Font.Bold = true;
                currentRow += 1;

                int row = currentRow;
                worksheet.Cells[row, 1].Value = "IUC Non-KT (Avg):";
                worksheet.Cells[row, 2].Value = monthlyReports.SelectMany(m => m.DailyData).Average(d => d.IUCNonKTCount);
                worksheet.Cells[row, 2].Style.Numberformat.Format = "0.00";
                row++;

                worksheet.Cells[row, 1].Value = "IUC KT (Avg):";
                worksheet.Cells[row, 2].Value = monthlyReports.SelectMany(m => m.DailyData).Average(d => d.IUCKTCount);
                worksheet.Cells[row, 2].Style.Numberformat.Format = "0.00";
                row++;

                worksheet.Cells[row, 1].Value = "CL Non-HD (Avg):";
                worksheet.Cells[row, 2].Value = monthlyReports.SelectMany(m => m.DailyData).Average(d => d.CLNonHDCount);
                worksheet.Cells[row, 2].Style.Numberformat.Format = "0.00";
                row++;

                worksheet.Cells[row, 1].Value = "CL HD (Avg):";
                worksheet.Cells[row, 2].Value = monthlyReports.SelectMany(m => m.DailyData).Average(d => d.CLHDCount);
                worksheet.Cells[row, 2].Style.Numberformat.Format = "0.00";
                row++;

                worksheet.Cells[row, 1].Value = "MV (Avg):";
                worksheet.Cells[row, 2].Value = monthlyReports.SelectMany(m => m.DailyData).Average(d => d.MVCount);
                worksheet.Cells[row, 2].Style.Numberformat.Format = "0.00";
                row++;

                // Format the averages section
                using (var range = worksheet.Cells[currentRow, 1, row - 1, 2])
                {
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }

                currentRow = row + 1;

                // Calculate yearly patient movement totals
                worksheet.Cells[currentRow, 1].Value = "Total Patient Movement:";
                worksheet.Cells[currentRow, 1].Style.Font.Bold = true;
                currentRow += 1;

                row = currentRow;
                worksheet.Cells[row, 1].Value = "Total Admissions:";
                worksheet.Cells[row, 2].Value = monthlyReports.SelectMany(m => m.DailyData).Sum(d => d.AdmissionCount);
                row++;

                worksheet.Cells[row, 1].Value = "Total Transfers In:";
                worksheet.Cells[row, 2].Value = monthlyReports.SelectMany(m => m.DailyData).Sum(d => d.TransferInCount);
                row++;

                worksheet.Cells[row, 1].Value = "Total Sent Home:";
                worksheet.Cells[row, 2].Value = monthlyReports.SelectMany(m => m.DailyData).Sum(d => d.SentHomeCount);
                row++;

                worksheet.Cells[row, 1].Value = "Total Mortality:";
                worksheet.Cells[row, 2].Value = monthlyReports.SelectMany(m => m.DailyData).Sum(d => d.MortalityCount);
                row++;

                worksheet.Cells[row, 1].Value = "Total Transfers Out:";
                worksheet.Cells[row, 2].Value = monthlyReports.SelectMany(m => m.DailyData).Sum(d => d.TransferOutCount);
                row++;

                // Calculate net change for the year
                int totalArrivals = monthlyReports.SelectMany(m => m.DailyData).Sum(d => d.AdmissionCount + d.TransferInCount);
                int totalDischarges = monthlyReports.SelectMany(m => m.DailyData).Sum(d => d.SentHomeCount + d.MortalityCount + d.TransferOutCount);
                int netChange = totalArrivals - totalDischarges;

                worksheet.Cells[row, 1].Value = "Yearly Net Change:";
                worksheet.Cells[row, 2].Value = netChange;
                worksheet.Cells[row, 2].Style.Font.Bold = true;
                row++;

                // Format the patient movement section
                using (var range = worksheet.Cells[currentRow, 1, row - 1, 2])
                {
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }

                // Add a chart worksheet
                var chartSheet = package.Workbook.Worksheets.Add("Charts");

                // Create charts based on the monthly data
                CreateYearlyCharts(chartSheet, sortedReports);

                // Auto-fit columns in main worksheet
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Return the Excel package as a byte array
                return package.GetAsByteArray();
            }
        }

        private void CreateYearlyCharts(ExcelWorksheet chartSheet, List<DeviceMonitoringReportData> monthlyReports)
        {
            var monthNames = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

            // Prepare data for charts
            // Monthly device averages
            var iucNonKTAvg = new double[12];
            var iucKTAvg = new double[12];
            var clNonHDAvg = new double[12];
            var clHDAvg = new double[12];
            var mvAvg = new double[12];

            // Monthly patient movement
            var admissions = new int[12];
            var transfersIn = new int[12];
            var sentHome = new int[12];
            var mortality = new int[12];
            var transfersOut = new int[12];

            // Fill in data from monthly reports
            foreach (var report in monthlyReports)
            {
                if (report.Month < 1 || report.Month > 12)
                    continue;

                int monthIndex = report.Month - 1;

                if (report.DailyData != null && report.DailyData.Any())
                {
                    // Device averages
                    iucNonKTAvg[monthIndex] = report.DailyData.Average(d => d.IUCNonKTCount);
                    iucKTAvg[monthIndex] = report.DailyData.Average(d => d.IUCKTCount);
                    clNonHDAvg[monthIndex] = report.DailyData.Average(d => d.CLNonHDCount);
                    clHDAvg[monthIndex] = report.DailyData.Average(d => d.CLHDCount);
                    mvAvg[monthIndex] = report.DailyData.Average(d => d.MVCount);

                    // Patient movement
                    admissions[monthIndex] = report.DailyData.Sum(d => d.AdmissionCount);
                    transfersIn[monthIndex] = report.DailyData.Sum(d => d.TransferInCount);
                    sentHome[monthIndex] = report.DailyData.Sum(d => d.SentHomeCount);
                    mortality[monthIndex] = report.DailyData.Sum(d => d.MortalityCount);
                    transfersOut[monthIndex] = report.DailyData.Sum(d => d.TransferOutCount);
                }
            }

            // Create a data table for the charts
            int row = 1;
            chartSheet.Cells[row, 1].Value = "Month";

            // Device column headers
            chartSheet.Cells[row, 2].Value = "IUC Non-KT";
            chartSheet.Cells[row, 3].Value = "IUC KT";
            chartSheet.Cells[row, 4].Value = "CL Non-HD";
            chartSheet.Cells[row, 5].Value = "CL HD";
            chartSheet.Cells[row, 6].Value = "MV";

            // Patient movement column headers
            chartSheet.Cells[row, 7].Value = "Admissions";
            chartSheet.Cells[row, 8].Value = "Transfers In";
            chartSheet.Cells[row, 9].Value = "Sent Home";
            chartSheet.Cells[row, 10].Value = "Mortality";
            chartSheet.Cells[row, 11].Value = "Transfers Out";

            // Occupancy column header
            chartSheet.Cells[row, 12].Value = "Occupancy %";

            // Format header row
            using (var range = chartSheet.Cells[row, 1, row, 12])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            }

            // Fill in data
            for (int i = 0; i < 12; i++)
            {
                row++;
                chartSheet.Cells[row, 1].Value = monthNames[i];

                // Device averages
                chartSheet.Cells[row, 2].Value = iucNonKTAvg[i];
                chartSheet.Cells[row, 3].Value = iucKTAvg[i];
                chartSheet.Cells[row, 4].Value = clNonHDAvg[i];
                chartSheet.Cells[row, 5].Value = clHDAvg[i];
                chartSheet.Cells[row, 6].Value = mvAvg[i];

                // Patient movement
                chartSheet.Cells[row, 7].Value = admissions[i];
                chartSheet.Cells[row, 8].Value = transfersIn[i];
                chartSheet.Cells[row, 9].Value = sentHome[i];
                chartSheet.Cells[row, 10].Value = mortality[i];
                chartSheet.Cells[row, 11].Value = transfersOut[i];

                // Calculate occupancy rate (total devices as percentage of capacity)
                double totalDevices = iucNonKTAvg[i] + iucKTAvg[i] + clNonHDAvg[i] + clHDAvg[i] + mvAvg[i];
                double occupancyRate = (totalDevices / 30) * 100; // Assuming 30 bed capacity
                chartSheet.Cells[row, 12].Value = occupancyRate;
            }

            // Create device usage trend chart
            var deviceChart = chartSheet.Drawings.AddChart("DeviceUsageChart", eChartType.Line);
            deviceChart.Title.Text = "Device Usage Trends (Monthly Average)";
            deviceChart.SetPosition(1, 0, 15, 0);
            deviceChart.SetSize(800, 400);

            // Add data series for each device type
            for (int col = 2; col <= 6; col++)
            {
                var series = deviceChart.Series.Add(
                    chartSheet.Cells[2, col, 13, col],
                    chartSheet.Cells[2, 1, 13, 1]
                );
                series.Header = chartSheet.Cells[1, col].Value.ToString();
            }

            // Create patient movement chart
            var patientChart = chartSheet.Drawings.AddChart("PatientMovementChart", eChartType.ColumnClustered);
            patientChart.Title.Text = "Patient Movement by Month";
            patientChart.SetPosition(25, 0, 15, 0);
            patientChart.SetSize(800, 400);

            // Add data series for patient movement
            for (int col = 7; col <= 11; col++)
            {
                var series = patientChart.Series.Add(
                    chartSheet.Cells[2, col, 13, col],
                    chartSheet.Cells[2, 1, 13, 1]
                );
                series.Header = chartSheet.Cells[1, col].Value.ToString();
            }

            // Create occupancy rate chart
            var occupancyChart = chartSheet.Drawings.AddChart("OccupancyChart", eChartType.Line);
            occupancyChart.Title.Text = "Monthly Occupancy Rate (%)";
            occupancyChart.SetPosition(50, 0, 15, 0);
            occupancyChart.SetSize(800, 400);

            // Add data series for occupancy
            var occupancySeries = occupancyChart.Series.Add(
                chartSheet.Cells[2, 12, 13, 12],
                chartSheet.Cells[2, 1, 13, 1]
            );
            occupancySeries.Header = "Occupancy Rate";

            // Add a target line at 80% occupancy
            int targetRow = 15;
            chartSheet.Cells[targetRow, 1].Value = "Target";

            // Add target value for each month
            for (int i = 0; i < 12; i++)
            {
                chartSheet.Cells[targetRow, i + 2].Value = 80; // 80% target
            }

            var targetSeries = occupancyChart.Series.Add(
                chartSheet.Cells[targetRow, 2, targetRow, 13],
                chartSheet.Cells[2, 1, 13, 1]
            );
            targetSeries.Header = "80% Target";

            // Format the charts
            deviceChart.Legend.Position = eLegendPosition.Bottom;
            patientChart.Legend.Position = eLegendPosition.Bottom;
            occupancyChart.Legend.Position = eLegendPosition.Bottom;
        }

        // Helper method to convert column number to Excel column letter
        private string GetExcelColumnLetter(int columnNumber)
        {
            string columnName = "";
            while (columnNumber > 0)
            {
                int modulo = (columnNumber - 1) % 26;
                columnName = Convert.ToChar('A' + modulo) + columnName;
                columnNumber = (columnNumber - modulo) / 26;
            }
            return columnName;
        }
    }
}