using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using IPCU.Models;

namespace IPCU.Services
{
    public class HAIDischargeExcelService
    {
        // Use same license context as other services
        public HAIDischargeExcelService()
        {
            // Set EPPlus license context if needed
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public byte[] GenerateHAIDischargeExcel(Dictionary<string, List<HAIDischargeData>> areaMonthlyData, int year)
        {
            using (var package = new ExcelPackage())
            {
                // Add a worksheet for monthly data
                var monthlySheet = package.Workbook.Worksheets.Add("HAI Discharge Report");

                // Set column headers with month names
                string[] months = new string[] { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };
                int colIndex = 2;

                // Create header row for unit/area and months
                monthlySheet.Cells[1, 1].Value = "UNIT/AREA";
                monthlySheet.Cells[1, 1].Style.Font.Bold = true;

                // Create the month columns (each month has 3 columns - Case, Discharge, Rate)
                foreach (var month in months)
                {
                    // Create a merged cell for the month
                    monthlySheet.Cells[1, colIndex, 1, colIndex + 2].Merge = true;
                    monthlySheet.Cells[1, colIndex].Value = month;
                    monthlySheet.Cells[1, colIndex].Style.Font.Bold = true;
                    monthlySheet.Cells[1, colIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    // Set sub-headers for each month
                    monthlySheet.Cells[2, colIndex].Value = "Case";
                    monthlySheet.Cells[2, colIndex + 1].Value = "Discharges";
                    monthlySheet.Cells[2, colIndex + 2].Value = "Rate";

                    // Style the sub-headers
                    for (int i = 0; i <= 2; i++)
                    {
                        monthlySheet.Cells[2, colIndex + i].Style.Font.Bold = true;
                        monthlySheet.Cells[2, colIndex + i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    colIndex += 3;
                }

                // Add column for yearly totals
                monthlySheet.Cells[1, colIndex, 1, colIndex + 2].Merge = true;
                monthlySheet.Cells[1, colIndex].Value = "YEARLY TOTAL";
                monthlySheet.Cells[1, colIndex].Style.Font.Bold = true;
                monthlySheet.Cells[1, colIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                monthlySheet.Cells[2, colIndex].Value = "Cases";
                monthlySheet.Cells[2, colIndex + 1].Value = "Discharges";
                monthlySheet.Cells[2, colIndex + 2].Value = "Rate";
                for (int i = 0; i <= 2; i++)
                {
                    monthlySheet.Cells[2, colIndex + i].Style.Font.Bold = true;
                    monthlySheet.Cells[2, colIndex + i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Start populating data from row 3
                int rowIndex = 3;

                // Sort areas alphabetically
                var sortedAreas = areaMonthlyData.Keys.OrderBy(a => a).ToList();

                // Track monthly totals for the TOTAL row
                int[] totalCases = new int[12];
                int[] totalDischarges = new int[12];

                // Populate data for each area
                foreach (var area in sortedAreas)
                {
                    monthlySheet.Cells[rowIndex, 1].Value = area;

                    var areaData = areaMonthlyData[area];

                    // Initialize yearly totals for this area
                    int yearlyAreaCases = 0;
                    int yearlyAreaDischarges = 0;

                    // Populate month data
                    colIndex = 2;
                    for (int monthIndex = 0; monthIndex < 12; monthIndex++)
                    {
                        // Find data for current month
                        var monthData = areaData.FirstOrDefault(d => d.Month == monthIndex + 1);

                        int cases = monthData?.Cases ?? 0;
                        int discharges = monthData?.Discharges ?? 0;

                        // Add to yearly area totals
                        yearlyAreaCases += cases;
                        yearlyAreaDischarges += discharges;

                        // Add to monthly totals for TOTAL row
                        totalCases[monthIndex] += cases;
                        totalDischarges[monthIndex] += discharges;

                        // Calculate rate
                        string rate = "0.00%";
                        if (discharges > 0)
                        {
                            rate = $"{(double)cases / discharges * 100:0.00}%";
                        }
                        else if (cases > 0)
                        {
                            rate = "#####";
                        }

                        // Populate cells
                        monthlySheet.Cells[rowIndex, colIndex].Value = cases;
                        monthlySheet.Cells[rowIndex, colIndex + 1].Value = discharges;
                        monthlySheet.Cells[rowIndex, colIndex + 2].Value = rate;

                        // Format cells
                        monthlySheet.Cells[rowIndex, colIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        monthlySheet.Cells[rowIndex, colIndex + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        monthlySheet.Cells[rowIndex, colIndex + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        colIndex += 3;
                    }

                    // Add yearly totals for this area
                    string yearlyRate = "0.00%";
                    if (yearlyAreaDischarges > 0)
                    {
                        yearlyRate = $"{(double)yearlyAreaCases / yearlyAreaDischarges * 100:0.00}%";
                    }
                    else if (yearlyAreaCases > 0)
                    {
                        yearlyRate = "#####";
                    }

                    monthlySheet.Cells[rowIndex, colIndex].Value = yearlyAreaCases;
                    monthlySheet.Cells[rowIndex, colIndex + 1].Value = yearlyAreaDischarges;
                    monthlySheet.Cells[rowIndex, colIndex + 2].Value = yearlyRate;

                    // Format yearly cells
                    monthlySheet.Cells[rowIndex, colIndex].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    monthlySheet.Cells[rowIndex, colIndex + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    monthlySheet.Cells[rowIndex, colIndex + 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    rowIndex++;
                }

                // Add TOTAL row
                monthlySheet.Cells[rowIndex, 1].Value = "TOTAL";
                monthlySheet.Cells[rowIndex, 1].Style.Font.Bold = true;

                // Add a new worksheet for half-year and annual summary
                var summarySheet = package.Workbook.Worksheets.Add("Half-Year Summary");

                // Set up the summary sheet headers
                summarySheet.Cells[1, 1, 1, 10].Merge = true;
                summarySheet.Cells[1, 1].Value = $"HAI DISCHARGE SUMMARY REPORT {year}";
                summarySheet.Cells[1, 1].Style.Font.Bold = true;
                summarySheet.Cells[1, 1].Style.Font.Size = 16;
                summarySheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Create header row
                summarySheet.Cells[3, 1].Value = "UNIT/AREA";
                summarySheet.Cells[3, 1].Style.Font.Bold = true;

                // Set column headers for half-year periods
                summarySheet.Cells[3, 2, 3, 4].Merge = true;
                summarySheet.Cells[3, 2].Value = "JAN-JUN";
                summarySheet.Cells[3, 2].Style.Font.Bold = true;
                summarySheet.Cells[3, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                summarySheet.Cells[3, 5, 3, 7].Merge = true;
                summarySheet.Cells[3, 5].Value = "JUL-DEC";
                summarySheet.Cells[3, 5].Style.Font.Bold = true;
                summarySheet.Cells[3, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                summarySheet.Cells[3, 8, 3, 10].Merge = true;
                summarySheet.Cells[3, 8].Value = "ANNUAL";
                summarySheet.Cells[3, 8].Style.Font.Bold = true;
                summarySheet.Cells[3, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Set sub-headers
                string[] subHeaders = { "Case", "Discharges", "Rate" };

                for (int i = 0; i < 3; i++)
                {
                    // First half year
                    summarySheet.Cells[4, 2 + i].Value = subHeaders[i];
                    summarySheet.Cells[4, 2 + i].Style.Font.Bold = true;
                    summarySheet.Cells[4, 2 + i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    // Second half year
                    summarySheet.Cells[4, 5 + i].Value = subHeaders[i];
                    summarySheet.Cells[4, 5 + i].Style.Font.Bold = true;
                    summarySheet.Cells[4, 5 + i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    // Annual
                    summarySheet.Cells[4, 8 + i].Value = subHeaders[i];
                    summarySheet.Cells[4, 8 + i].Style.Font.Bold = true;
                    summarySheet.Cells[4, 8 + i].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Track half-year and annual totals
                int totalFirstHalfCases = 0;
                int totalFirstHalfDischarges = 0;
                int totalSecondHalfCases = 0;
                int totalSecondHalfDischarges = 0;

                // Populate data for each area
                foreach (var area in sortedAreas)
                {
                    var areaData = areaMonthlyData[area];

                    // Add area name
                    summarySheet.Cells[rowIndex, 1].Value = area;

                    // Calculate half-year totals for this area
                    int firstHalfCases = 0;
                    int firstHalfDischarges = 0;
                    int secondHalfCases = 0;
                    int secondHalfDischarges = 0;

                    // First half (Jan-Jun)
                    for (int month = 1; month <= 6; month++)
                    {
                        var monthData = areaData.FirstOrDefault(d => d.Month == month);
                        if (monthData != null)
                        {
                            firstHalfCases += monthData.Cases;
                            firstHalfDischarges += monthData.Discharges;
                        }
                    }

                    // Second half (Jul-Dec)
                    for (int month = 7; month <= 12; month++)
                    {
                        var monthData = areaData.FirstOrDefault(d => d.Month == month);
                        if (monthData != null)
                        {
                            secondHalfCases += monthData.Cases;
                            secondHalfDischarges += monthData.Discharges;
                        }
                    }

                    // Update running totals
                    totalFirstHalfCases += firstHalfCases;
                    totalFirstHalfDischarges += firstHalfDischarges;
                    totalSecondHalfCases += secondHalfCases;
                    totalSecondHalfDischarges += secondHalfDischarges;

                    // Calculate annual totals
                    int annualCases = firstHalfCases + secondHalfCases;
                    int annualDischarges = firstHalfDischarges + secondHalfDischarges;

                    // Calculate rates
                    string firstHalfRate = "0.00%";
                    if (firstHalfDischarges > 0)
                    {
                        firstHalfRate = $"{(double)firstHalfCases / firstHalfDischarges * 100:0.00}%";
                    }
                    else if (firstHalfCases > 0)
                    {
                        firstHalfRate = "#####";
                    }

                    string secondHalfRate = "0.00%";
                    if (secondHalfDischarges > 0)
                    {
                        secondHalfRate = $"{(double)secondHalfCases / secondHalfDischarges * 100:0.00}%";
                    }
                    else if (secondHalfCases > 0)
                    {
                        secondHalfRate = "#####";
                    }

                    string annualRate = "0.00%";
                    if (annualDischarges > 0)
                    {
                        annualRate = $"{(double)annualCases / annualDischarges * 100:0.00}%";
                    }
                    else if (annualCases > 0)
                    {
                        annualRate = "#####";
                    }

                    // Populate first half-year data
                    summarySheet.Cells[rowIndex, 2].Value = firstHalfCases;
                    summarySheet.Cells[rowIndex, 3].Value = firstHalfDischarges;
                    summarySheet.Cells[rowIndex, 4].Value = firstHalfRate;

                    // Populate second half-year data
                    summarySheet.Cells[rowIndex, 5].Value = secondHalfCases;
                    summarySheet.Cells[rowIndex, 6].Value = secondHalfDischarges;
                    summarySheet.Cells[rowIndex, 7].Value = secondHalfRate;

                    // Populate annual data
                    summarySheet.Cells[rowIndex, 8].Value = annualCases;
                    summarySheet.Cells[rowIndex, 9].Value = annualDischarges;
                    summarySheet.Cells[rowIndex, 10].Value = annualRate;

                    // Center align all data cells
                    for (int col = 2; col <= 10; col++)
                    {
                        summarySheet.Cells[rowIndex, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    rowIndex++;
                }

                // Add TOTAL row
                summarySheet.Cells[rowIndex, 1].Value = "TOTAL";
                summarySheet.Cells[rowIndex, 1].Style.Font.Bold = true;

                // Calculate total rates
                int totalAnnualCases = totalFirstHalfCases + totalSecondHalfCases;
                int totalAnnualDischarges = totalFirstHalfDischarges + totalSecondHalfDischarges;

                string totalFirstHalfRate = "0.00%";
                if (totalFirstHalfDischarges > 0)
                {
                    totalFirstHalfRate = $"{(double)totalFirstHalfCases / totalFirstHalfDischarges * 100:0.00}%";
                }
                else if (totalFirstHalfCases > 0)
                {
                    totalFirstHalfRate = "#####";
                }

                string totalSecondHalfRate = "0.00%";
                if (totalSecondHalfDischarges > 0)
                {
                    totalSecondHalfRate = $"{(double)totalSecondHalfCases / totalSecondHalfDischarges * 100:0.00}%";
                }
                else if (totalSecondHalfCases > 0)
                {
                    totalSecondHalfRate = "#####";
                }

                string totalAnnualRate = "0.00%";
                if (totalAnnualDischarges > 0)
                {
                    totalAnnualRate = $"{(double)totalAnnualCases / totalAnnualDischarges * 100:0.00}%";
                }
                else if (totalAnnualCases > 0)
                {
                    totalAnnualRate = "#####";
                }

                // Populate totals
                summarySheet.Cells[rowIndex, 2].Value = totalFirstHalfCases;
                summarySheet.Cells[rowIndex, 3].Value = totalFirstHalfDischarges;
                summarySheet.Cells[rowIndex, 4].Value = totalFirstHalfRate;

                summarySheet.Cells[rowIndex, 5].Value = totalSecondHalfCases;
                summarySheet.Cells[rowIndex, 6].Value = totalSecondHalfDischarges;
                summarySheet.Cells[rowIndex, 7].Value = totalSecondHalfRate;

                summarySheet.Cells[rowIndex, 8].Value = totalAnnualCases;
                summarySheet.Cells[rowIndex, 9].Value = totalAnnualDischarges;
                summarySheet.Cells[rowIndex, 10].Value = totalAnnualRate;

                // Format total row
                for (int col = 2; col <= 10; col++)
                {
                    summarySheet.Cells[rowIndex, col].Style.Font.Bold = true;
                    summarySheet.Cells[rowIndex, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                // Apply border to all data cells
                var dataRange = summarySheet.Cells[3, 1, rowIndex, 10];
                dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                // Add conditional formatting for rate cells
                var rateColumns = new[] { 4, 7, 10 };
                foreach (var col in rateColumns)
                {
                    var rateRange = summarySheet.Cells[5, col, rowIndex, col];
                    var rateRule = rateRange.ConditionalFormatting.AddGreaterThan();
                    rateRule.Formula = "5%"; // Adjust threshold as needed
                    rateRule.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rateRule.Style.Fill.BackgroundColor.Color = Color.LightPink;
                }

                // Auto-fit columns
                for (int i = 1; i <= 10; i++)
                {
                    summarySheet.Column(i).AutoFit();
                }

                // Save the package to a MemoryStream
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;
                return stream.ToArray();
            }
        }
    }

    // Model for HAI discharge data
    public class HAIDischargeData
    {
        public string Area { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int Cases { get; set; }
        public int Discharges { get; set; }

        // Calculate rate on demand
        public double Rate => Discharges > 0 ? (double)Cases / Discharges * 100 : 0;
    }
}