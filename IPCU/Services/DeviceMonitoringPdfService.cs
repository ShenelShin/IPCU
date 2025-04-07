using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using IPCU.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;

namespace IPCU.Services
{
    public class DeviceMonitoringPdfService
    {
        // Define custom colors for a professional look
        private static readonly string DarkColor = Colors.Black;
        private static readonly string PrimaryColor = Colors.Indigo.Darken2;  // Similar to your Deep Blue
        private static readonly string SecondaryColor = Colors.Grey.Lighten4; // Similar to your Light Gray
        private static readonly string AccentColor = Colors.Orange.Medium;    // Similar to your Orange
        private static readonly string SuccessColor = Colors.Green.Darken1;   // Similar to your Green
        private static readonly string TextDarkColor = Colors.Grey.Darken4;   // Similar to your Near Black
        private static readonly string TextLightColor = Colors.Grey.Lighten5; // Similar to your Off White
        private static readonly string GridLineColor = Colors.Grey.Lighten2;  // Similar to your Light Gray

        public DeviceMonitoringPdfService()
        {
            // Ensure QuestPDF is properly registered
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] GeneratePdf(DeviceMonitoringReportData reportData)
        {
            // Set default font
            QuestPDF.Settings.DocumentLayoutExceptionThreshold = 10000;

            // Generate the PDF document
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    // Page setup - landscape orientation for better table visibility
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(30);
                    page.DefaultTextStyle(x => x.FontSize(10).FontColor(DarkColor));
                    page.Background().Border(1).BorderColor(GridLineColor);

                    // Header section with logo placeholder and title
                    page.Header().Padding(5).Element(header =>
                    {
                        header.Row(row =>
                        {
                            // Logo placeholder
                            row.ConstantItem(50).Image(@"wwwroot\images\nktilogo.png").FitWidth();

                            // Report title and information
                            row.RelativeItem(2).Padding(10).Column(col =>
                            {
                                col.Item().Text($"DEVICE MONITORING REPORT")
                                    .FontSize(18).Bold().FontColor(PrimaryColor);

                                col.Item().PaddingTop(5).Text(text =>
                                {
                                    text.Span("Area: ").SemiBold();
                                    text.Span($"{reportData.Area}").FontColor(AccentColor);
                                });

                                col.Item().Text(text =>
                                {
                                    text.Span("Period: ").SemiBold();
                                    text.Span($"{new DateTime(reportData.Year, reportData.Month, 1):MMMM yyyy}")
                                        .FontColor(AccentColor);
                                });
                            });

                            // Summary stats
                            row.RelativeItem().Border(1).BorderColor(GridLineColor)
                                .Background(SecondaryColor).Padding(5)
                                .Column(col =>
                                {
                                    col.Item().AlignCenter().Text("MONTHLY SUMMARY").SemiBold()
                                        .FontColor(PrimaryColor);

                                    // Calculate some statistics
                                    var totalDevices = reportData.DailyData.Sum(d =>
                                        d.IUCNonKTCount + d.IUCKTCount + d.CLNonHDCount +
                                        d.CLHDCount + d.MVCount);

                                    var avgDevicesPerDay = totalDevices /
                                        (reportData.DailyData.Count > 0 ? reportData.DailyData.Count : 1);

                                    var totalAdmissions = reportData.DailyData.Sum(d => d.AdmissionCount);
                                    var totalTransfers = reportData.DailyData.Sum(d => d.TransferInCount);

                                    col.Item().Text($"Total Devices: {totalDevices}");
                                    col.Item().Text($"Avg. Daily: {avgDevicesPerDay}");
                                    col.Item().Text($"Admissions: {totalAdmissions}");
                                });
                        });
                    });

                    // Content section with data tables
                    page.Content().PaddingVertical(10).Element(content =>
                    {
                        content.Column(col =>
                        {
                            // Table title and description
                            col.Item().PaddingBottom(5).Element(element =>
                            {
                                element.Row(row =>
                                {
                                    row.RelativeItem().Text(text =>
                                    {
                                        text.Span("Daily Device Monitoring Data").FontSize(14).Bold()
                                            .FontColor(PrimaryColor);
                                    });

                                    row.RelativeItem().AlignRight().Text(text =>
                                    {
                                        text.Span($"Generated: {DateTime.Now:MM/dd/yyyy HH:mm}")
                                            .FontColor(DarkColor).FontSize(9);
                                    });
                                });
                            });

                            // Main data table
                            col.Item().Table(table =>
                            {
                                // Define columns with proportional widths for better layout
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(40);              // Day
                                    columns.RelativeColumn(1);             // IUC Non-KT
                                    columns.RelativeColumn(1);             // IUC KT
                                    columns.RelativeColumn(1);             // CL Non-HD
                                    columns.RelativeColumn(1);             // CL HD
                                    columns.RelativeColumn(1);               // MV
                                    columns.RelativeColumn(1);               // Admission
                                    columns.RelativeColumn(1);               // Transfer In
                                    columns.RelativeColumn(1);               // Sent Home
                                    columns.RelativeColumn(1);               // Mortality
                                    columns.RelativeColumn(1);               // Transfer Out
                                });

                                // Table header with category grouping
                                table.Header(header =>
                                {
                                    // Create a header row for categories
                                    header.Cell().RowSpan(2).Element(CellHeaderStyle)
                                        .Text("Day").FontColor(TextLightColor);

                                    header.Cell().ColumnSpan(5).Element(CellHeaderStyle)
                                        .Text("DEVICE UTILIZATION").FontColor(TextLightColor);

                                    header.Cell().ColumnSpan(5).Element(CellHeaderStyle)
                                        .Text("PATIENT MOVEMENT").FontColor(TextLightColor);

                                    static IContainer CellHeaderStyle(IContainer container)
                                    {
                                        return container.DefaultTextStyle(x => x.SemiBold())
                                            .PaddingVertical(5)
                                            .PaddingHorizontal(5)
                                            .Border(1)
                                            .BorderColor(PrimaryColor)
                                            .Background(PrimaryColor)
                                            .AlignCenter()
                                            .AlignMiddle();
                                    }

                                    // Create subheader row
                                    header.Cell().Element(CellSubHeaderStyle)
                                        .Text("IUC\nNon-KT").FontColor(TextLightColor);
                                    header.Cell().Element(CellSubHeaderStyle)
                                        .Text("IUC\nKT").FontColor(TextLightColor);
                                    header.Cell().Element(CellSubHeaderStyle)
                                        .Text("CL\nNon-HD").FontColor(TextLightColor);
                                    header.Cell().Element(CellSubHeaderStyle)
                                        .Text("CL\nHD").FontColor(TextLightColor);
                                    header.Cell().Element(CellSubHeaderStyle)
                                        .Text("MV").FontColor(TextLightColor);

                                    header.Cell().Element(CellSubHeaderStyle)
                                        .Text("Admit").FontColor(TextLightColor);
                                    header.Cell().Element(CellSubHeaderStyle)
                                        .Text("Trans.\nIn").FontColor(TextLightColor);
                                    header.Cell().Element(CellSubHeaderStyle)
                                        .Text("Sent\nHome").FontColor(TextLightColor);
                                    header.Cell().Element(CellSubHeaderStyle)
                                        .Text("Mort.").FontColor(TextLightColor);
                                    header.Cell().Element(CellSubHeaderStyle)
                                        .Text("Trans.\nOut").FontColor(TextLightColor);

                                    static IContainer CellSubHeaderStyle(IContainer container)
                                    {
                                        return container.DefaultTextStyle(x => x.SemiBold())
                                            .PaddingVertical(5)
                                            .PaddingHorizontal(2)
                                            .Border(1)
                                            .BorderColor(PrimaryColor)
                                            .Background(PrimaryColor)
                                            .AlignCenter()
                                            .AlignMiddle();
                                    }
                                });

                                // Table data with alternate row styling
                                bool isEvenRow = false;
                                foreach (var daily in reportData.DailyData)
                                {
                                    isEvenRow = !isEvenRow;

                                    // Date column with special formatting
                                    table.Cell().Element(DateCellStyle).AlignCenter().AlignMiddle()
                                        .Text($"{daily.Date.Day}").FontSize(11).Bold();

                                    // Device data - using inline lambda to handle the conditional styling
                                    table.Cell().Element(container => isEvenRow ? DataRowEvenStyle(container) : DataRowOddStyle(container))
                                        .AlignCenter().AlignMiddle().Text($"{daily.IUCNonKTCount}");
                                    table.Cell().Element(container => isEvenRow ? DataRowEvenStyle(container) : DataRowOddStyle(container))
                                        .AlignCenter().AlignMiddle().Text($"{daily.IUCKTCount}");
                                    table.Cell().Element(container => isEvenRow ? DataRowEvenStyle(container) : DataRowOddStyle(container))
                                        .AlignCenter().AlignMiddle().Text($"{daily.CLNonHDCount}");
                                    table.Cell().Element(container => isEvenRow ? DataRowEvenStyle(container) : DataRowOddStyle(container))
                                        .AlignCenter().AlignMiddle().Text($"{daily.CLHDCount}");
                                    table.Cell().Element(container => isEvenRow ? DataRowEvenStyle(container) : DataRowOddStyle(container))
                                        .AlignCenter().AlignMiddle().Text($"{daily.MVCount}");

                                    // Patient movement data
                                    table.Cell().Element(container => isEvenRow ? DataRowEvenStyle(container) : DataRowOddStyle(container))
                                        .AlignCenter().AlignMiddle().Text($"{daily.AdmissionCount}");
                                    table.Cell().Element(container => isEvenRow ? DataRowEvenStyle(container) : DataRowOddStyle(container))
                                        .AlignCenter().AlignMiddle().Text($"{daily.TransferInCount}");
                                    table.Cell().Element(container => isEvenRow ? DataRowEvenStyle(container) : DataRowOddStyle(container))
                                        .AlignCenter().AlignMiddle().Text($"{daily.SentHomeCount}");
                                    table.Cell().Element(container => isEvenRow ? DataRowEvenStyle(container) : DataRowOddStyle(container))
                                        .AlignCenter().AlignMiddle().Text($"{daily.MortalityCount}");
                                    table.Cell().Element(container => isEvenRow ? DataRowEvenStyle(container) : DataRowOddStyle(container))
                                        .AlignCenter().AlignMiddle().Text($"{daily.TransferOutCount}");
                                }

                                // Calculate totals
                                var totalIUCNonKT = reportData.DailyData.Sum(d => d.IUCNonKTCount);
                                var totalIUCKT = reportData.DailyData.Sum(d => d.IUCKTCount);
                                var totalCLNonHD = reportData.DailyData.Sum(d => d.CLNonHDCount);
                                var totalCLHD = reportData.DailyData.Sum(d => d.CLHDCount);
                                var totalMV = reportData.DailyData.Sum(d => d.MVCount);
                                var totalAdmission = reportData.DailyData.Sum(d => d.AdmissionCount);
                                var totalTransferIn = reportData.DailyData.Sum(d => d.TransferInCount);
                                var totalSentHome = reportData.DailyData.Sum(d => d.SentHomeCount);
                                var totalMortality = reportData.DailyData.Sum(d => d.MortalityCount);
                                var totalTransferOut = reportData.DailyData.Sum(d => d.TransferOutCount);

                                // Add a totals row
                                table.Cell().Element(TotalRowStyle).AlignCenter().AlignMiddle()
                                    .Text("Total").FontSize(11);
                                table.Cell().Element(TotalRowStyle).AlignCenter().AlignMiddle()
                                    .Text($"{totalIUCNonKT}");
                                table.Cell().Element(TotalRowStyle).AlignCenter().AlignMiddle()
                                    .Text($"{totalIUCKT}");
                                table.Cell().Element(TotalRowStyle).AlignCenter().AlignMiddle()
                                    .Text($"{totalCLNonHD}");
                                table.Cell().Element(TotalRowStyle).AlignCenter().AlignMiddle()
                                    .Text($"{totalCLHD}");
                                table.Cell().Element(TotalRowStyle).AlignCenter().AlignMiddle()
                                    .Text($"{totalMV}");
                                table.Cell().Element(TotalRowStyle).AlignCenter().AlignMiddle()
                                    .Text($"{totalAdmission}");
                                table.Cell().Element(TotalRowStyle).AlignCenter().AlignMiddle()
                                    .Text($"{totalTransferIn}");
                                table.Cell().Element(TotalRowStyle).AlignCenter().AlignMiddle()
                                    .Text($"{totalSentHome}");
                                table.Cell().Element(TotalRowStyle).AlignCenter().AlignMiddle()
                                    .Text($"{totalMortality}");
                                table.Cell().Element(TotalRowStyle).AlignCenter().AlignMiddle()
                                    .Text($"{totalTransferOut}");
                            });

                            // Legend and helpful information
                            col.Item().PaddingTop(10).Element(legend =>
                            {
                                legend.Border(1).BorderColor(GridLineColor)
                                    .Background(SecondaryColor)
                                    .Padding(10)
                                    .Column(c =>
                                    {
                                        c.Item().Text("ABBREVIATIONS & INFORMATION").Bold()
                                            .FontColor(PrimaryColor);

                                        c.Item().PaddingTop(5).Table(t =>
                                        {
                                            t.ColumnsDefinition(cols =>
                                            {
                                                cols.RelativeColumn();
                                                cols.RelativeColumn();
                                                cols.RelativeColumn();
                                                cols.RelativeColumn();
                                            });

                                            t.Cell().Text("IUC: Indwelling Urinary Catheter");
                                            t.Cell().Text("KT: Kidney Transplant");
                                            t.Cell().Text("CL: Central Line");
                                            t.Cell().Text("HD: Hemodialysis");

                                            t.Cell().Text("MV: Mechanical Ventilation");
                                            t.Cell().Text("Trans: Transfer");
                                            t.Cell().Text("Mort: Mortality");
                                            t.Cell().Text("Admit: Admission");
                                        });
                                    });
                            });
                        });
                    });

                    // Footer with page numbers and disclaimer
                    page.Footer().AlignCenter().PaddingTop(10).Element(footer =>
                    {
                        footer.Column(col =>
                        {
                            col.Item().BorderTop(1).BorderColor(PrimaryColor).PaddingTop(5)
                                .Text(text =>
                                {
                                    text.Span("Page ").FontSize(9);
                                    text.CurrentPageNumber().FontSize(9);
                                    text.Span(" of ").FontSize(9);
                                    text.TotalPages().FontSize(9);
                                });

                            col.Item().Text("CONFIDENTIAL - FOR INTERNAL USE ONLY")
                                .FontSize(8).FontColor(PrimaryColor);
                        });
                    });

                    // Cell styling functions
                    static IContainer DateCellStyle(IContainer container)
                    {
                        return container.Border(1)
                            .BorderColor(GridLineColor)
                            .Background(PrimaryColor)
                            .PaddingVertical(5)
                            .PaddingHorizontal(3);
                    }

                    static IContainer DataRowEvenStyle(IContainer container)
                    {
                        return container.Border(1)
                            .BorderColor(GridLineColor)
                            .Background(Colors.White)
                            .PaddingVertical(5)
                            .PaddingHorizontal(3);
                    }

                    static IContainer DataRowOddStyle(IContainer container)
                    {
                        return container.Border(1)
                            .BorderColor(GridLineColor)
                            .Background(SecondaryColor)
                            .PaddingVertical(5)
                            .PaddingHorizontal(3);
                    }

                    static IContainer TotalRowStyle(IContainer container)
                    {
                        return container.Border(1)
                            .BorderColor(PrimaryColor)
                            .Background(PrimaryColor)
                            .DefaultTextStyle(x => x.Bold())
                            .PaddingVertical(5)
                            .PaddingHorizontal(3);
                    }
                });
            });

            // Generate and return the PDF bytes
            return document.GeneratePdf();
        }
    }
}