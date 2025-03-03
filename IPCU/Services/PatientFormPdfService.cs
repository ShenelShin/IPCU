using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using IPCU.Models;
using System;

namespace IPCU.Services
{
    public class PatientFormPdfService
    {
        public byte[] GeneratePdf(PatientForm patient)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(30);

                    page.Header()
                        .PaddingBottom(20)
                        .BorderBottom(1).BorderColor(Colors.Grey.Lighten1)
                        .Text($"PATIENT MEDICAL RECORD")
                        .Bold()
                        .FontSize(24)
                        .FontColor(Colors.Blue.Darken3)
                        .AlignCenter();

                    page.Content().PaddingVertical(15).Column(column =>
                    {
                        column.Spacing(15);

                        // Patient Information Section
                        column.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(section =>
                        {
                            section.Spacing(5);
                            section.Item().Text("PATIENT INFORMATION").Bold().FontSize(16).FontColor(Colors.Blue.Darken2);
                            section.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).PaddingBottom(5);

                            section.Item().Grid(grid =>
                            {
                                grid.Columns(2);
                                grid.Spacing(5);

                                grid.Item().Text("Full Name:").Bold().FontColor(Colors.Grey.Darken3);
                                grid.Item().Text($"{patient.FirstName} {patient.MiddleName ?? ""} {patient.LastName} {patient.Suffix ?? ""}");

                                grid.Item().Text("Sex:").Bold();
                                grid.Item().Text(patient.Sex);

                                grid.Item().Text("Age:").Bold();
                                grid.Item().Text(patient.Age);
                            });
                        });

                        // Medical Details Section
                        column.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(section =>
                        {
                            section.Spacing(5);
                            section.Item().Text("MEDICAL DETAILS").Bold().FontSize(16).FontColor(Colors.Blue.Darken2);
                            section.Item().BorderBottom(1).BorderColor(Colors.Grey.Lighten1).PaddingBottom(5);

                            section.Item().Grid(grid =>
                            {
                                grid.Columns(2);
                                grid.Spacing(5);

                                grid.Item().Text("Primary Diagnosis:").Bold();
                                grid.Item().Text(patient.Disease);

                                grid.Item().Text("Patient Status:").Bold();
                                grid.Item().Text(patient.Status);

                            });
                        });

                        // Location and Nurse Information
                        column.Item().Grid(grid =>
                        {
                            grid.Columns(2);
                            grid.Spacing(10);

                            // Location Section
                            grid.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(col =>
                            {
                                col.Item().Text("ASSIGNED LOCATION").Bold().FontSize(14).FontColor(Colors.Blue.Darken2);
                                col.Item().PaddingVertical(5);
                                col.Item().Text($"Building: {patient.Building}").FontSize(14);
                                col.Item().Text($"Room: {patient.Room}").FontSize(14);
                            });

                            // Nurse Section
                            grid.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(col =>
                            {
                                col.Item().Text("ASSIGNED NURSE").Bold().FontSize(14).FontColor(Colors.Blue.Darken2);
                                col.Item().PaddingVertical(5);
                                col.Item().Text($"{patient.NurseFirstName} {patient.NurseMiddleName ?? ""} {patient.NurseLastName}");
                            });
                        });

                        // Additional Notes Section
                        column.Item().Background(Colors.Grey.Lighten3).Padding(10).Column(col =>
                        {
                            col.Item().Text("OBSERVATIONS").Bold().FontSize(14).FontColor(Colors.Blue.Darken2);
                            col.Item().PaddingTop(5);
                            col.Item().Text("Patient is responding well to treatment. Vital signs stable.")
                                .FontSize(12)
                                .FontColor(Colors.Grey.Darken1);
                        });
                    });

                    page.Footer()
                        .BorderTop(1).BorderColor(Colors.Grey.Lighten1)
                        .PaddingTop(5)
                        .AlignCenter()
                        .Text(text =>
                        {
                            text.Span("Generated on ").FontColor(Colors.Grey.Medium);
                            text.Span($"{DateTime.Now:MMMM dd, yyyy 'at' HH:mm}").Bold();
                        });
                });
            });

            return document.GeneratePdf();
        }
    }
}