using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.IO;
using IPCU.Models;
using Microsoft.AspNetCore.Mvc;

namespace IPCU.Services
{
    public class MechanicalVentilatorReportService
    {
        // This method generates the Mechanical Ventilator Monitoring Form PDF
        public byte[] GenerateMechanicalVentilatorReport()
        {
            // Configure QuestPDF for commercial license if needed
            // QuestPDF.Settings.License = LicenseType.Community;

            // Generate the PDF document
            var pdf = Document.Create(document =>
            {
                document.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(20);
                    page.DefaultTextStyle(x => x.FontSize(9));

                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("Page ").FontSize(8);
                        text.CurrentPageNumber().FontSize(8);
                        text.Span(" of ").FontSize(8);
                        text.TotalPages().FontSize(8);
                    });
                });
            });

            // Return the PDF as a byte array
            using (var stream = new MemoryStream())
            {
                pdf.GeneratePdf(stream);
                return stream.ToArray();
            }
        }

        // Header section of the report
        void ComposeHeader(IContainer container)
        {
            container.Column(column =>
            {
                // Section for institution name and department
                column.Item().AlignCenter().Text("NATIONAL KIDNEY AND TRANSPLANT INSTITUTE").Bold().FontSize(12);
                column.Item().AlignCenter().Text("East Avenue, Quezon City").FontSize(10);
                column.Item().AlignCenter().Text("INFECTION PREVENTION AND CONTROL UNIT").Bold().FontSize(11);
                column.Item().AlignCenter().Text("HEALTHCARE ASSOCIATED INFECTION (HAI) MONITORING SHEET").Bold().FontSize(11);
                column.Item().AlignCenter().PaddingTop(5).Text("MECHANICAL VENTILATOR MONITORING FORM").Bold().FontSize(14);
                column.Item().AlignRight().Text("IPC-WIF-001c_ver2").FontSize(8);
                column.Item().PaddingTop(10);
            });
        }

        // Main content of the report
        void ComposeContent(IContainer container)
        {
            container.Column(column =>
            {
                // Patient Information Section
                column.Item().Element(ComposePatientInfoSection);

                // Monitoring Table Section
                column.Item().PaddingTop(20).Element(ComposeMonitoringTable);
            });
        }

        // Patient information section
        void ComposePatientInfoSection(IContainer container)
        {
            container.Border(1).Padding(5).Column(column =>
            {
                column.Item().Text("PATIENT INFORMATION").Bold().FontSize(10);
                column.Item().PaddingTop(5);

                // Create a grid for patient information fields
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn(2);
                        columns.RelativeColumn();
                        columns.RelativeColumn(2);
                    });

                    // Row 1
                    table.Cell().Text("HOSPITAL NO.").Bold();
                    table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).PaddingLeft(5).Text("_________________");
                    table.Cell().Text("AGE/SEX").Bold();
                    table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).PaddingLeft(5).Text("_________________");

                    // Row 2
                    table.Cell().Text("PATIENT NAME").Bold();
                    table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).PaddingLeft(5).Text("_________________");
                    table.Cell().Text("DISCHARGE/STATUS").Bold();
                    table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).PaddingLeft(5).Text("_________________");

                    // Row 3
                    table.Cell().Text("AREA/ROOM").Bold();
                    table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).PaddingLeft(5).Text("_________________");
                    table.Cell().Text("HAI STATUS").Bold();
                    table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).PaddingLeft(5).Text("_________________");

                    // Row 4
                    table.Cell().Text("ADMISSION").Bold();
                    table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).PaddingLeft(5).Text("_________________");
                    table.Cell().Text("Type").Bold();
                    table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).PaddingLeft(5).Text("_________________");

                    // Row 5 - Initial Diagnosis
                    table.Cell().Text("INITIAL DIAGNOSIS").Bold();
                    table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).PaddingLeft(5).Text("_________________");

                    // Row 6 - Final Diagnosis
                    table.Cell().Text("FINAL DIAGNOSIS").Bold();
                    table.Cell().Border(1).BorderColor(Colors.Grey.Lighten1).PaddingLeft(5).Text("_________________");
                });
            });
        }

        // Monitoring table section
        void ComposeMonitoringTable(IContainer container)
        {
            container.Table(table =>
            {
                // Define columns
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(1); // HOSP DAY
                    columns.RelativeColumn(1); // DATE
                    columns.RelativeColumn(2); // DIAGNOSTICS
                    columns.RelativeColumn(2); // SIGNS AND SYMPTOMS
                    columns.RelativeColumn(2); // TREATMENT
                });

                // Table header
                table.Header(header =>
                {
                    header.Cell().Border(1).Background(Colors.Grey.Lighten3).AlignCenter().AlignMiddle().Text("HOSP DAY").Bold();
                    header.Cell().Border(1).Background(Colors.Grey.Lighten3).AlignCenter().AlignMiddle().Text("DATE").Bold();
                    header.Cell().Border(1).Background(Colors.Grey.Lighten3).AlignCenter().AlignMiddle().Text("DIAGNOSTICS").Bold();
                    header.Cell().Border(1).Background(Colors.Grey.Lighten3).AlignCenter().AlignMiddle().Text("SIGNS AND SYMPTOMS").Bold();
                    header.Cell().Border(1).Background(Colors.Grey.Lighten3).AlignCenter().AlignMiddle().Text("TREATMENT").Bold();
                });

                // Create empty rows for data entry
                for (int i = 0; i < 15; i++)
                {
                    table.Cell().Border(1).Height(25).Text("");
                    table.Cell().Border(1).Height(25).Text("");
                    table.Cell().Border(1).Height(25).Text("");
                    table.Cell().Border(1).Height(25).Text("");
                    table.Cell().Border(1).Height(25).Text("");
                }
            });
        }
    }

    // Extension method for MVC controllers to generate and return the PDF
    public static class PDFGeneratorExtensions
    {
        public static FileContentResult GenerateMechanicalVentilatorMonitoringPDF(this Microsoft.AspNetCore.Mvc.Controller controller)
        {
            var service = new MechanicalVentilatorReportService();
            var pdfData = service.GenerateMechanicalVentilatorReport();

            return controller.File(
                pdfData,
                "application/pdf",
                $"MechanicalVentilatorMonitoring_{DateTime.Now:yyyyMMdd}.pdf"
            );
        }
    }
}