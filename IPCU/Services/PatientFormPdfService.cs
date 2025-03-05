using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using IPCU.Models;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IPCU.Services
{
    public class PatientFormPdfService
    {
        private static readonly string DarkColor = Colors.Grey.Darken3;
        private static readonly string AccentColor = Colors.Teal.Darken1;
        private static readonly float HeaderSize = 14;
        private static readonly float BodySize = 12;

        public byte[] GeneratePdf(PatientForm patient)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(25);

                    // Header Section
                    page.Header().Column(header =>
                    {
                        header.Item().AlignCenter().Text("NATIONAL KIDNEY AND TRANSPLANT INSTITUTE")
                            .FontColor(AccentColor)
                            .Bold()
                            .FontSize(16);

                        header.Item().AlignCenter().Text(" EastAvenue,QuezonCity")
                            .FontColor(DarkColor)
                        .FontSize(10)
                            .Bold();

                        header.Item()
                            .PaddingVertical(10)
                            .LineHorizontal(2);

                        header.Item().AlignCenter().Text(" INFECTION PREVENTION AND CONTROL UNIT")
                            .FontColor(DarkColor)
                            .FontSize(10)
                            .Bold();

                        header.Item().AlignCenter().Text(" HANDHY GIENE(HH) DETAILED")
                            .FontColor(DarkColor)
                            .FontSize(10)
                            .Bold();

                        header.Item().AlignCenter().Text(" OBSERVATION AND MONITORING FORM")
                            .FontColor(DarkColor)
                            .FontSize(10)
                            .Bold();

                        header.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten1);
                    });

                    page.Content().Column(content =>
                    {
                        content.Spacing(15);

                        // Patient Information
                        content.Item().Column(col =>
                        {
                            col.Item().Text("PATIENT RECORD")
                                .FontColor(AccentColor)
                                .Bold()
                                .FontSize(HeaderSize);

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(3);
                                });

                                AddTableRow(table, "Full Name:", $"{patient.LastName}, {patient.FirstName} {patient.MiddleName} {patient.Suffix}");
                                AddTableRow(table, "Date of Birth:", $"{patient.Age} years old");
                                AddTableRow(table, "Sex:", patient.Sex);
                                AddTableRow(table, "Patient ID:", patient.Id.ToString());
                            });
                        });

                        // Medical Information Section
                        content.Item().Border(1).BorderColor(Colors.Grey.Lighten2).Column(col =>
                        {
                            col.Item().Padding(10).Background(Colors.Grey.Lighten3).Text("CLINICAL INFORMATION")
                                .FontColor(DarkColor)
                                .Bold()
                                .FontSize(HeaderSize);

                            col.Item().Padding(10).Grid(grid =>
                            {
                                grid.Columns(2);
                                grid.Spacing(10);

                                grid.Item().Column(c =>
                                {
                                    c.Item().Text("PRIMARY DIAGNOSIS").FontSize(BodySize).Bold();
                                    c.Item().Text(patient.Disease).FontSize(BodySize);
                                });

                                grid.Item().Column(c =>
                                {
                                    c.Item().Text("CURRENT STATUS").FontSize(BodySize).Bold();
                                    c.Item().Text(patient.Status).FontSize(BodySize);
                                });
                            });
                        });

                        // Care Team Section
                        content.Item().Border(1).BorderColor(Colors.Grey.Lighten2).Column(col =>
                        {
                            col.Item().Padding(10).Background(Colors.Grey.Lighten3).Text("CARE TEAM")
                                .FontColor(DarkColor)
                                .Bold()
                                .FontSize(HeaderSize);

                            col.Item().Padding(10).Grid(grid =>
                            {
                                grid.Columns(2);
                                grid.Spacing(15);

                                grid.Item().Column(c =>
                                {
                                    c.Item().Text("ATTENDING PHYSICIAN").FontSize(BodySize).Bold();
                                    c.Item().Text("Dr. Maria Santos, MD").FontSize(BodySize);
                                });

                                grid.Item().Column(c =>
                                {
                                    c.Item().Text("PRIMARY NURSE").FontSize(BodySize).Bold();
                                    c.Item().Text($"{patient.NurseFirstName} {patient.NurseLastName}").FontSize(BodySize);
                                });
                            });
                        });

                        // Location Information
                        content.Item().Border(1).BorderColor(Colors.Grey.Lighten2).Column(col =>
                        {
                            col.Item().Padding(10).Background(Colors.Grey.Lighten3).Text("ADMISSION DETAILS")
                                .FontColor(DarkColor)
                                .Bold()
                                .FontSize(HeaderSize);

                            col.Item().Padding(10).Grid(grid =>
                            {
                                grid.Columns(2);
                                grid.Spacing(15);

                                grid.Item().Column(c =>
                                {
                                    c.Item().Text("ADMISSION DATE").FontSize(BodySize).Bold();
                                    c.Item().Text(DateTime.Now.ToString("MMM dd, yyyy")).FontSize(BodySize);
                                });

                                grid.Item().Column(c =>
                                {
                                    c.Item().Text("LOCATION").FontSize(BodySize).Bold();
                                    c.Item().Text($"Building {patient.Building} - Room {patient.Room}").FontSize(BodySize);
                                });
                            });
                        });

                        // Observations
                        content.Item().Border(1).BorderColor(Colors.Grey.Lighten2).Column(col =>
                        {
                            col.Item().Padding(10).Background(Colors.Grey.Lighten3).Text("CLINICAL NOTES")
                                .FontColor(DarkColor)
                                .Bold()
                                .FontSize(HeaderSize);

                            col.Item().Padding(10).Text("No significant observations recorded")
                                .FontSize(BodySize)
                                .FontColor(Colors.Grey.Darken1);
                        });
                    });

                    // Footer
                    page.Footer().AlignCenter().Text(t =>
                    {
                        t.Span("Page ").FontColor(Colors.Grey.Medium);
                        t.CurrentPageNumber().FontColor(AccentColor);
                        t.Span(" of ").FontColor(Colors.Grey.Medium);
                        t.TotalPages().FontColor(AccentColor);
                    });
                });
            });

            return document.GeneratePdf();
        }

        private void AddTableRow(TableDescriptor table, string label, string value)
        {
            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5).Text(label)
                .FontColor(DarkColor).Bold().FontSize(BodySize);
            table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5).Text(value)
                .FontColor(Colors.Grey.Darken2).FontSize(BodySize);
        }
    }
}