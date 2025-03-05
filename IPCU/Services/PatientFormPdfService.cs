using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using IPCU.Models;
using System;

namespace IPCU.Services
{
    public class PatientFormPdfService
    {
        private static readonly string DarkColor = Colors.Grey.Darken3;
        private static readonly string AccentColor = Colors.Indigo.Darken4;
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
                            .LineHorizontal(2)
                            .LineColor(Colors.Grey.Darken3);

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
                                AddTableRow(table, "Patient ID:", patient.Id.ToString() ?? "N/A");
                            });
                        });

                        // Medical Information Section - Updated
                        content.Item().Border(1).BorderColor(Colors.Grey.Lighten2).Column(col =>
                        {
                            col.Item().Padding(10).Background(Colors.Grey.Lighten3).Text("CLINICAL INFORMATION")
                                .FontColor(DarkColor)
                                .Bold()
                                .FontSize(HeaderSize);

                            col.Item().Padding(10).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });
                                

                                // Primary Diagnosis
                                table.Cell().Column(column =>
                                {
                                    column.Item().Text("PRIMARY DIAGNOSIS").FontSize(BodySize).Bold();
                                    column.Item().Text(patient.Disease).FontSize(BodySize);
                                });

                                // Current Status
                                table.Cell().Column(column =>
                                {
                                    column.Item().Text("CURRENT STATUS").FontSize(BodySize).Bold();
                                    column.Item().Text(patient.Status).FontSize(BodySize);
                                });
                            });
                        });

                        // Care Team Section - Updated
                        content.Item().Border(1).BorderColor(Colors.Grey.Lighten2).Column(col =>
                        {
                            col.Item().Padding(10).Background(Colors.Grey.Lighten3).Text("CARE TEAM")
                                .FontColor(DarkColor)
                                .Bold()
                                .FontSize(HeaderSize);

                            col.Item().Padding(10).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });
                                

                                // Attending Physician
                                table.Cell().Column(column =>
                                {
                                    column.Item().Text("ATTENDING PHYSICIAN").FontSize(BodySize).Bold();
                                    column.Item().Text("Dr. Maria Santos, MD").FontSize(BodySize);
                                });

                                // Primary Nurse
                                table.Cell().Column(column =>
                                {
                                    column.Item().Text("PRIMARY NURSE").FontSize(BodySize).Bold();
                                    column.Item().Text($"{patient.NurseFirstName} {patient.NurseLastName}").FontSize(BodySize);
                                });
                            });
                        });

                        // Admission Details - Updated
                        content.Item().Border(1).BorderColor(Colors.Grey.Lighten2).Column(col =>
                        {
                            col.Item().Padding(10).Background(Colors.Grey.Lighten3).Text("ADMISSION DETAILS")
                                .FontColor(DarkColor)
                                .Bold()
                                .FontSize(HeaderSize);

                            col.Item().Padding(10).Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });
                                

                                // Admission Date
                                table.Cell().Column(column =>
                                {
                                    column.Item().Text("ADMISSION DATE").FontSize(BodySize).Bold();
                                    column.Item().Text(DateTime.Now.ToString("MMM dd, yyyy")).FontSize(BodySize);
                                });

                                // Location
                                table.Cell().Column(column =>
                                {
                                    column.Item().Text("LOCATION").FontSize(BodySize).Bold();
                                    column.Item().Text($"Building {patient.Building} - Room {patient.Room}").FontSize(BodySize);
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

                            col.Item().Padding(10).Text(text =>
                            {
                                text.Span("The patient presents with significant weight-related health concerns, characterized by Class I obesity (BMI: 32.1 kg/m²) with central adiposity evidenced by a waist circumference of 112 cm. This metabolic profile is complicated by comorbid hypertension, with recent readings averaging 145/92 mmHg, and impaired glucose tolerance demonstrated by an elevated HbA1c of 6.1%. ")
                                    .FontColor(Colors.Grey.Darken2)
                                    .FontSize(BodySize)
                                    .LineHeight(1);

                                text.EmptyLine();

                                text.Span("Lifestyle assessment reveals a predominantly sedentary pattern with less than 30 minutes of weekly physical activity and nutritional intake patterns consistent with excessive caloric consumption. The patient reports emerging symptoms of sleep-disordered breathing, including daytime somnolence and witnessed apneic episodes, suggesting probable obstructive sleep apnea. ")
                                    .FontColor(Colors.Grey.Darken2)
                                    .FontSize(BodySize)
                                    .LineHeight(1);

                                text.EmptyLine();

                                text.Span("Management strategy focuses on multimodal intervention: Initiation of a Mediterranean-style dietary protocol under clinical nutrition supervision, implementation of a progressive aerobic exercise regimen targeting 150 minutes weekly, and referral for polysomnography to evaluate CPAP necessity. Concurrent pharmacotherapy with GLP-1 receptor agonists is being considered pending endocrine consultation. Long-term monitoring will emphasize cardiovascular risk reduction through sustained weight loss of 5-10% body mass over six months.")
                                    .FontColor(Colors.Grey.Darken2)
                                    .FontSize(BodySize)
                                    .LineHeight(1);
                            });
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