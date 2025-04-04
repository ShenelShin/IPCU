using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using IPCU.Models;

namespace IPCU.Services
{
    public class DeviceMonitoringExcelService
    {
        public byte[] GenerateExcel(DeviceMonitoringReportData reportData)
        {
            // Set the license context for non-commercial use (or commercial if you have a license)
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                // Add a worksheet to the Excel package
                var worksheet = package.Workbook.Worksheets.Add("Device Monitoring Report");

                // Add headers and report information
                worksheet.Cells[1, 1].Value = "Device Monitoring Report";
                worksheet.Cells[1, 1, 1, 11].Merge = true;
                worksheet.Cells[1, 1].Style.Font.Size = 16;
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                worksheet.Cells[2, 1].Value = $"Area: {reportData.Area}";
                worksheet.Cells[3, 1].Value = $"Month/Year: {new DateTime(reportData.Year, reportData.Month, 1).ToString("MMMM yyyy")}";
                worksheet.Cells[2, 1, 3, 11].Style.Font.Bold = true;

                // Device Count Section
                int headerRow = 5;
                worksheet.Cells[headerRow, 1].Value = "Date";

                // Device Counts header
                worksheet.Cells[headerRow - 1, 2].Value = "Device Counts";
                worksheet.Cells[headerRow - 1, 2, headerRow - 1, 6].Merge = true;
                worksheet.Cells[headerRow - 1, 2].Style.Font.Bold = true;
                worksheet.Cells[headerRow - 1, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[headerRow - 1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[headerRow - 1, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                worksheet.Cells[headerRow, 2].Value = "IUC Non-KT";
                worksheet.Cells[headerRow, 3].Value = "IUC KT";
                worksheet.Cells[headerRow, 4].Value = "CL Non-HD";
                worksheet.Cells[headerRow, 5].Value = "CL HD";
                worksheet.Cells[headerRow, 6].Value = "MV";

                // Patient Movement header
                worksheet.Cells[headerRow - 1, 7].Value = "Patient Movement";
                worksheet.Cells[headerRow - 1, 7, headerRow - 1, 11].Merge = true;
                worksheet.Cells[headerRow - 1, 7].Style.Font.Bold = true;
                worksheet.Cells[headerRow - 1, 7].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                worksheet.Cells[headerRow - 1, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[headerRow - 1, 7].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);

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

                // Add data rows
                int currentRow = headerRow + 1;
                foreach (var daily in reportData.DailyData.OrderBy(d => d.Date))
                {
                    worksheet.Cells[currentRow, 1].Value = daily.Date.ToString("yyyy-MM-dd");

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

                // Add totals row
                worksheet.Cells[currentRow, 1].Value = "TOTALS";
                worksheet.Cells[currentRow, 1].Style.Font.Bold = true;

                // Sum device counts
                worksheet.Cells[currentRow, 2].Formula = $"SUM(B{headerRow + 1}:B{currentRow - 1})";
                worksheet.Cells[currentRow, 3].Formula = $"SUM(C{headerRow + 1}:C{currentRow - 1})";
                worksheet.Cells[currentRow, 4].Formula = $"SUM(D{headerRow + 1}:D{currentRow - 1})";
                worksheet.Cells[currentRow, 5].Formula = $"SUM(E{headerRow + 1}:E{currentRow - 1})";
                worksheet.Cells[currentRow, 6].Formula = $"SUM(F{headerRow + 1}:F{currentRow - 1})";

                // Sum patient movement
                worksheet.Cells[currentRow, 7].Formula = $"SUM(G{headerRow + 1}:G{currentRow - 1})";
                worksheet.Cells[currentRow, 8].Formula = $"SUM(H{headerRow + 1}:H{currentRow - 1})";
                worksheet.Cells[currentRow, 9].Formula = $"SUM(I{headerRow + 1}:I{currentRow - 1})";
                worksheet.Cells[currentRow, 10].Formula = $"SUM(J{headerRow + 1}:J{currentRow - 1})";
                worksheet.Cells[currentRow, 11].Formula = $"SUM(K{headerRow + 1}:K{currentRow - 1})";

                // Format the totals row
                using (var range = worksheet.Cells[currentRow, 1, currentRow, 11])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Double;
                }

                // Add summary section
                currentRow += 2;
                worksheet.Cells[currentRow, 1].Value = "Summary";
                worksheet.Cells[currentRow, 1].Style.Font.Bold = true;
                worksheet.Cells[currentRow, 1].Style.Font.Size = 14;

                currentRow += 1;
                // Total patient flow summary
                worksheet.Cells[currentRow, 1].Value = "Total Patient Flow";
                worksheet.Cells[currentRow, 1].Style.Font.Bold = true;
                currentRow += 1;

                worksheet.Cells[currentRow, 1].Value = "Total Arrivals:";
                worksheet.Cells[currentRow, 2].Formula = $"G{currentRow - 2}+H{currentRow - 2}";

                currentRow += 1;
                worksheet.Cells[currentRow, 1].Value = "Total Discharges:";
                worksheet.Cells[currentRow, 2].Formula = $"I{currentRow - 3}+J{currentRow - 3}+K{currentRow - 3}";

                currentRow += 1;
                worksheet.Cells[currentRow, 1].Value = "Net Change:";
                worksheet.Cells[currentRow, 2].Formula = $"B{currentRow - 2}-B{currentRow - 1}";
                worksheet.Cells[currentRow, 2].Style.Font.Bold = true;

                // Format summary header
                using (var range = worksheet.Cells[currentRow - 4, 1, currentRow, 2])
                {
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }

                // Add device averages
                currentRow += 2;
                worksheet.Cells[currentRow, 1].Value = "Device Averages";
                worksheet.Cells[currentRow, 1].Style.Font.Bold = true;
                currentRow += 1;

                worksheet.Cells[currentRow, 1].Value = "Average IUC Non-KT:";
                worksheet.Cells[currentRow, 2].Formula = $"AVERAGE(B{headerRow + 1}:B{currentRow - 5})";
                worksheet.Cells[currentRow, 2].Style.Numberformat.Format = "0.00";

                currentRow += 1;
                worksheet.Cells[currentRow, 1].Value = "Average IUC KT:";
                worksheet.Cells[currentRow, 2].Formula = $"AVERAGE(C{headerRow + 1}:C{currentRow - 6})";
                worksheet.Cells[currentRow, 2].Style.Numberformat.Format = "0.00";

                currentRow += 1;
                worksheet.Cells[currentRow, 1].Value = "Average CL Non-HD:";
                worksheet.Cells[currentRow, 2].Formula = $"AVERAGE(D{headerRow + 1}:D{currentRow - 7})";
                worksheet.Cells[currentRow, 2].Style.Numberformat.Format = "0.00";

                currentRow += 1;
                worksheet.Cells[currentRow, 1].Value = "Average CL HD:";
                worksheet.Cells[currentRow, 2].Formula = $"AVERAGE(E{headerRow + 1}:E{currentRow - 8})";
                worksheet.Cells[currentRow, 2].Style.Numberformat.Format = "0.00";

                currentRow += 1;
                worksheet.Cells[currentRow, 1].Value = "Average MV:";
                worksheet.Cells[currentRow, 2].Formula = $"AVERAGE(F{headerRow + 1}:F{currentRow - 9})";
                worksheet.Cells[currentRow, 2].Style.Numberformat.Format = "0.00";

                // Format device averages section
                using (var range = worksheet.Cells[currentRow - 5, 1, currentRow, 2])
                {
                    range.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    range.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }

                // Create a second worksheet for charts
                var chartSheet = package.Workbook.Worksheets.Add("Charts");

                // Auto-fit columns in main worksheet
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Return the Excel package as a byte array
                return package.GetAsByteArray();
            }
        }
    }
}