using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using IPCU.Models;
using System;
using System.Linq;
using System.Collections.Generic;

namespace IPCU.Services
{
    public class HandHygienePdfService
    {
        // Constants for consistent styling
        private static readonly string DarkColor = Colors.Black;
        private static readonly string AccentColor = Colors.Blue.Medium;
        private static readonly string LightBgColor = Colors.Grey.Lighten4;
        private static readonly string BorderColor = Colors.Grey.Medium;
        private static readonly string HeaderBgColor = Colors.Blue.Lighten3;
        private static readonly float HeaderSize = 12;
        private static readonly float BodySize = 10;
        private static readonly float SmallSize = 9;

        public byte[] GeneratePdf(HandHygieneForm form)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(20);
                    page.PageColor(Colors.White);

                    // Header
                    page.Header().Element(ComposeHeader);

                    // Content
                    page.Content().PaddingVertical(10).Column(content =>
                    {
                        content.Spacing(15);

                        // Basic information section
                        content.Item().Element(container => CreateInfoSection(container, form));

                        // HCW Details
                        content.Item().Element(container => CreateHcwSection(container, form));

                        // Room and Isolation information
                        content.Item().Element(container => CreateRoomAndIsolationSection(container, form));

                        // Observation section
                        content.Item().Element(container => CreateObservationSection(container, form));

                        // Entry and Environment Observation
                        content.Item().Element(container => CreateEnvironmentSection(container, form));

                        // Activities Table
                        content.Item().Element(container => CreateActivitiesTable(container, form));
                    });

                    // Footer
                    page.Footer().Column(footer =>
                    {
                        footer.Item().AlignRight().Text(text =>
                        {
                            text.Span("Page ").FontSize(SmallSize);
                            text.CurrentPageNumber().FontSize(SmallSize);
                            text.Span(" of ").FontSize(SmallSize);
                            text.TotalPages().FontSize(SmallSize);
                        });

                        footer.Item().BorderTop(1).BorderColor(BorderColor).PaddingTop(5)
                            .Text("Hand Hygiene Observation Form - Generated on " + DateTime.Now.ToString("MM/dd/yyyy"))
                            .FontSize(8).FontColor(Colors.Grey.Medium);
                    });
                });
            }).GeneratePdf();
        }

        private void ComposeHeader(IContainer container)
        {
            container.Column(header =>
            {
                // Logo and title
                header.Item().Row(row =>
                {
                    // You can add a logo here if available
                    // row.ConstantItem(80).Height(50).Placeholder();

                    row.RelativeItem().Column(col =>
                    {
                        col.Item().AlignCenter().Text("NATIONAL KIDNEY AND TRANSPLANT INSTITUTE")
                            .FontColor(DarkColor).Bold().FontSize(14);

                        col.Item().AlignCenter().Text("East Avenue, Quezon City")
                            .FontColor(DarkColor).FontSize(10);

                        col.Item().AlignCenter().Text("INFECTION PREVENTION AND CONTROL UNIT")
                            .FontColor(DarkColor).Bold().FontSize(11);
                    });
                });

                // Form title
                header.Item().Background(HeaderBgColor).Padding(5).AlignCenter()
                    .Text("HAND HYGIENE (HH) DETAILED OBSERVATION AND MONITORING FORM")
                    .FontColor(DarkColor).Bold().FontSize(12);

                header.Item().PaddingTop(3).Row(row =>
                {
                    row.RelativeItem();
                    row.ConstantItem(100).Text("IPC-WIF-005_ver1")
                        .FontColor(DarkColor).FontSize(8);
                });

                header.Item().BorderBottom(1).BorderColor(BorderColor);
            });
        }

        private void CreateInfoSection(IContainer container, HandHygieneForm form)
        {
            container.Column(mainColumn =>
            {
                // Table section
                mainColumn.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    // Header Row 1
                    table.Cell().Border(1).BorderColor(BorderColor).Background(LightBgColor)
                        .Padding(5).Text("AREA").Bold().FontSize(SmallSize);

                    table.Cell().Border(1).BorderColor(BorderColor).Background(LightBgColor)
                        .Padding(5).Text("DATE").Bold().FontSize(SmallSize);

                    // Values Row 1
                    table.Cell().Border(1).BorderColor(BorderColor)
                        .Padding(5).Text(form.Area ?? "");

                    table.Cell().Border(1).BorderColor(BorderColor)
                        .Padding(5).Text(form.Date.ToString("MM/dd/yyyy"));

                    // Header Row 2
                    table.Cell().Border(1).BorderColor(BorderColor).Background(LightBgColor)
                        .Padding(5).Text("OBSERVER").Bold().FontSize(SmallSize);

                    table.Cell().Border(1).BorderColor(BorderColor).Background(LightBgColor)
                        .Padding(5).Text("TIME").Bold().FontSize(SmallSize);

                    // Values Row 2
                    table.Cell().Border(1).BorderColor(BorderColor)
                        .Padding(5).Text(form.Observer ?? "");

                    // Time formatting
                    var formattedTime = FormatTime(form.Time);
                    table.Cell().Border(1).BorderColor(BorderColor)
                        .Padding(5).Text(formattedTime);
                });

                // Instructions section
                mainColumn.Item().PaddingTop(5).Border(1).BorderColor(BorderColor).Padding(5)
                    .Background(LightBgColor).Column(instructionsCol =>
                    {
                        instructionsCol.Item().Text("INSTRUCTIONS:").Bold().FontSize(SmallSize);
                        instructionsCol.Item().Text("• Observe only one HCW at a time from the time they enter a patient's environment to the time they exit.")
                            .FontSize(SmallSize);
                        instructionsCol.Item().Text("• You do not need to write their name but document their designation.")
                            .FontSize(SmallSize);
                    });
            });
        }

        private string FormatTime(TimeSpan time)
        {
            try
            {
                int hours = time.Hours;
                int minutes = time.Minutes;
                string period = hours < 12 ? "AM" : "PM";
                int displayHours = hours % 12;
                displayHours = displayHours == 0 ? 12 : displayHours;
                return $"{displayHours:D2}:{minutes:D2} {period}";
            }
            catch
            {
                return "N/A";
            }
        }


        private void CreateHcwSection(IContainer container, HandHygieneForm form)
        {
            container.Column(col =>
            {
                col.Item().BorderBottom(0).Border(1).BorderColor(BorderColor).Background(AccentColor)
                    .Padding(5).Text("HEALTHCARE WORKER INFORMATION")
                    .FontColor(Colors.White).FontSize(HeaderSize).Bold();

                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn(3);
                    });

                    // HCW Name
                    table.Cell().Border(1).BorderColor(BorderColor).Padding(5).Column(nameCol =>
                    {
                        nameCol.Item().Text("NAME (Optional)").Bold().FontSize(BodySize);
                        nameCol.Item().PaddingTop(3).Text(form.Name ?? "N/A").FontSize(BodySize);
                    });

                    // HCW Type
                    table.Cell().Border(1).BorderColor(BorderColor).Padding(5).Column(typeCol =>
                    {
                        typeCol.Item().Text("HEALTHCARE WORKER TYPE").Bold().FontSize(BodySize);
                        typeCol.Item().PaddingTop(3).Table(innerTable =>
                        {
                            innerTable.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            string[] hcwTypes = new[]
                            {
                                "Consultant", "Fellow", "Resident",
                                "Nurse", "Nursing Attendant", "Medical Technologist",
                                "Respiratory Therapist", "Radiology Department Staff", "Nursing Aide"
                            };

                            int row = 0;
                            int col = 0;

                            foreach (var type in hcwTypes)
                            {
                                innerTable.Cell().Element(cell =>
                                {
                                    bool isSelected = form.HCWType == type;
                                    cell.Row(row =>
                                    {
                                        row.ConstantItem(15).Text(isSelected ? "☑" : "☐").FontSize(BodySize);
                                        row.RelativeItem().Text(type).FontSize(BodySize);
                                    });
                                });

                                col++;
                                if (col >= 3)
                                {
                                    col = 0;
                                    row++;
                                }
                            }

                            // Other option (spans all columns)
                            innerTable.Cell().ColumnSpan(3).Element(cell =>
                            {
                                bool isOther = form.HCWType != null && !hcwTypes.Contains(form.HCWType);
                                cell.Row(row =>
                                {
                                    row.ConstantItem(15).Text(isOther ? "☑" : "☐").FontSize(BodySize);
                                    row.RelativeItem().Text(text =>
                                    {
                                        text.Span("Other: ").FontSize(BodySize);
                                        if (isOther)
                                        {
                                            text.Span(form.HCWType).FontSize(BodySize);
                                        }
                                    });
                                });
                            });
                        });
                    });
                });
            });
        }

        private void CreateRoomAndIsolationSection(IContainer container, HandHygieneForm form)
        {
            container.Column(col =>
            {
                col.Item().BorderBottom(0).Border(1).BorderColor(BorderColor).Background(AccentColor)
                    .Padding(5).Text("ROOM AND ISOLATION INFORMATION")
                    .FontColor(Colors.White).FontSize(HeaderSize).Bold();

                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    // Room Type
                    table.Cell().Border(1).BorderColor(BorderColor).Padding(5).Column(roomCol =>
                    {
                        roomCol.Item().Text("ROOM TYPE").Bold().FontSize(BodySize);
                        roomCol.Item().Text("(For in-patients only)").FontSize(7).Italic();

                        string[] roomTypes = { "ICE/IMCU", "AIIR", "Ward" };
                        foreach (var type in roomTypes)
                        {
                            roomCol.Item().PaddingTop(2).Row(row =>
                            {
                                bool isSelected = form.RoomType == type;
                                row.ConstantItem(15).Text(isSelected ? "☑" : "☐").FontSize(BodySize);
                                row.RelativeItem().Text(type).FontSize(BodySize);
                            });
                        }

                        // Other option for Room Type
                        roomCol.Item().PaddingTop(2).Row(row =>
                        {
                            bool isOther = form.RoomType != null && !roomTypes.Contains(form.RoomType);
                            row.ConstantItem(15).Text(isOther ? "☑" : "☐").FontSize(BodySize);
                            row.RelativeItem().Text(text =>
                            {
                                text.Span("Other: ").FontSize(BodySize);
                                if (isOther)
                                {
                                    text.Span(form.RoomType).FontSize(BodySize);
                                }
                            });
                        });
                    });

                    // Isolation Information
                    table.Cell().Border(1).BorderColor(BorderColor).Padding(5).Column(isoCol =>
                    {
                        isoCol.Item().Text("ISOLATION PRECAUTIONS").Bold().FontSize(BodySize);

                        isoCol.Item().PaddingTop(2).Row(row =>
                        {
                            row.ConstantItem(15).Text(!form.Isolation ? "☑" : "☐").FontSize(BodySize);
                            row.RelativeItem().Text("None").FontSize(BodySize);
                        });

                        isoCol.Item().PaddingTop(2).Row(row =>
                        {
                            row.ConstantItem(15).Text(form.Isolation ? "☑" : "☐").FontSize(BodySize);
                            row.RelativeItem().Text("Yes (check type below)").FontSize(BodySize);
                        });

                        if (form.Isolation)
                        {
                            isoCol.Item().PaddingLeft(20).PaddingTop(5).Table(innerTable =>
                            {
                                innerTable.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                string[] precautions = { "Airborne", "Droplet", "Contact", "Protective" };
                                int row = 0;
                                int col = 0;

                                foreach (var precaution in precautions)
                                {
                                    innerTable.Cell().Element(cell =>
                                    {
                                        bool isSelected = form.IsolationPrecaution == precaution;
                                        cell.Row(r =>
                                        {
                                            r.ConstantItem(15).Text(isSelected ? "☑" : "☐").FontSize(BodySize);
                                            r.RelativeItem().Text(precaution).FontSize(BodySize);
                                        });
                                    });

                                    col++;
                                    if (col >= 2)
                                    {
                                        col = 0;
                                        row++;
                                    }
                                }
                            });
                        }
                    });
                });
            });
        }

        private void CreateObservationSection(IContainer container, HandHygieneForm form)
        {
            container.Column(col =>
            {
                col.Item().BorderBottom(0).Border(1).BorderColor(BorderColor).Background(AccentColor)
                    .Padding(5).Text("OBSERVATION DETAILS")
                    .FontColor(Colors.White).FontSize(HeaderSize).Bold();

                col.Item().Border(1).BorderColor(BorderColor).Padding(5).Column(obsCol =>
                {
                    obsCol.Item().Text("DESCRIBE HOW YOU CAN OBSERVE THE HCW DURING PATIENT CARE:").Bold().FontSize(BodySize);

                    var observationOptions = new[]
                    {
                        "I can directly observe the patient without any obstruction",
                        "I can observe only if I glance through a room door",
                        "There is a large window or glass that allows me to view the activity",
                        "There is a curtain around the patient. I can see only when the curtain is parted"
                    };

                    foreach (var option in observationOptions)
                    {
                        obsCol.Item().PaddingTop(3).Row(row =>
                        {
                            bool isSelected = form.ObsvPatientCare == option;
                            row.ConstantItem(15).Text(isSelected ? "☑" : "☐").FontSize(BodySize);
                            row.RelativeItem().Text(option).FontSize(BodySize);
                        });
                    }

                    // Other option
                    obsCol.Item().PaddingTop(3).Row(row =>
                    {
                        bool isOther = form.ObsvPatientCare != null && !observationOptions.Contains(form.ObsvPatientCare);
                        row.ConstantItem(15).Text(isOther ? "☑" : "☐").FontSize(BodySize);
                        row.RelativeItem().Text(text =>
                        {
                            text.Span("Other: ").FontSize(BodySize);
                            if (isOther)
                            {
                                text.Span(form.ObsvPatientCare).FontSize(BodySize);
                            }
                        });
                    });
                });
            });
        }

        private void CreateEnvironmentSection(IContainer container, HandHygieneForm form)
        {
            container.Column(col =>
            {
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    // Hand Hygiene Resources Cell
                    table.Cell().Border(1).BorderColor(BorderColor).Padding(5).Column(resourceCol =>
                    {
                        resourceCol.Item().Text("HAND HYGIENE RESOURCES AVAILABLE").Bold().FontSize(BodySize);

                        // Yes option
                        resourceCol.Item().PaddingTop(3).Row(row =>
                        {
                            bool hasEnvironment = form.ObsvPatientEnvironment?.Equals("on", StringComparison.OrdinalIgnoreCase) ?? false;
                            row.ConstantItem(15).Text(hasEnvironment ? "☑" : "☐").FontSize(BodySize);
                            row.RelativeItem().Text("Yes").FontSize(BodySize);
                        });

                        // Show resources only if "Yes" is selected
                        if (form.ObsvPatientEnvironment?.Equals("on", StringComparison.OrdinalIgnoreCase) ?? false)
                        {
                            resourceCol.Item().PaddingLeft(20).PaddingTop(3).Row(row =>
                            {
                                bool hasHandSanitizer = form.EnvironmentResource?.Equals("Hand sanitizer", StringComparison.OrdinalIgnoreCase) ?? false;
                                row.ConstantItem(15).Text(hasHandSanitizer ? "☑" : "☐").FontSize(BodySize);
                                row.RelativeItem().Text("Hand sanitizer").FontSize(BodySize);
                            });

                            resourceCol.Item().PaddingLeft(20).PaddingTop(3).Row(row =>
                            {
                                bool hasSoapWater = form.EnvironmentResource?.Equals("Soap and water", StringComparison.OrdinalIgnoreCase) ?? false;
                                row.ConstantItem(15).Text(hasSoapWater ? "☑" : "☐").FontSize(BodySize);
                                row.RelativeItem().Text("Soap and water").FontSize(BodySize);
                            });
                        }

                        // No option
                        resourceCol.Item().PaddingTop(3).Row(row =>
                        {
                            bool noEnvironment = form.ObsvPatientEnvironment?.Equals("off", StringComparison.OrdinalIgnoreCase) ?? false;
                            row.ConstantItem(15).Text(noEnvironment ? "☑" : "☐").FontSize(BodySize);
                            row.RelativeItem().Text("No").FontSize(BodySize);
                        });
                    });

                    // Contact with objects Cell
                    table.Cell().Border(1).BorderColor(BorderColor).Padding(5).Column(contactCol =>
                    {
                        contactCol.Item().Text("OBJECTS HCW HAD CONTACT WITH").Bold().FontSize(BodySize);
                        contactCol.Item().Text("(Before touching the patient)").FontSize(SmallSize).Italic();
                        contactCol.Item().PaddingTop(5).Text(form.ObsvPatientContact ?? "None observed").FontSize(BodySize);
                    });
                });
            });
        }

        private void CreateActivitiesTable(IContainer container, HandHygieneForm form)
        {
            container.Column(col =>
            {
                col.Item().BorderBottom(0).Border(1).BorderColor(BorderColor).Background(AccentColor)
                    .Padding(5).Text("HAND HYGIENE ACTIVITIES OBSERVED")
                    .FontColor(Colors.White).FontSize(HeaderSize).Bold();

                col.Item().Element(container =>
                {
                    container.Table(table =>
                    {
                        // Column definitions
                        table.ColumnsDefinition(columns =>
                        {
                            columns.ConstantColumn(30);       // Activity number
                            columns.RelativeColumn(3);        // Activity description
                            columns.ConstantColumn(60);       // Before Hand Rub
                            columns.ConstantColumn(60);       // Before Hand Wash
                            columns.ConstantColumn(60);       // After Hand Rub
                            columns.ConstantColumn(60);       // After Hand Wash
                            columns.ConstantColumn(50);       // With gloves
                        });

                        // Header row
                        table.Cell().RowSpan(2).Border(1).BorderColor(BorderColor).Background(LightBgColor)
                            .AlignCenter().AlignMiddle()
                            .Text("#").FontSize(SmallSize).Bold();

                        table.Cell().RowSpan(2).Border(1).BorderColor(BorderColor).Background(LightBgColor)
                            .AlignCenter().AlignMiddle().Padding(2)
                            .Text("Activity Description").FontSize(SmallSize).Bold();

                        table.Cell().ColumnSpan(2).Border(1).BorderColor(BorderColor).Background(LightBgColor)
                            .AlignCenter().Padding(2)
                            .Text("Before Activity").FontSize(SmallSize).Bold();

                        table.Cell().ColumnSpan(2).Border(1).BorderColor(BorderColor).Background(LightBgColor)
                            .AlignCenter().Padding(2)
                            .Text("After Activity").FontSize(SmallSize).Bold();

                        table.Cell().RowSpan(2).Border(1).BorderColor(BorderColor).Background(LightBgColor)
                            .AlignCenter().AlignMiddle().Padding(2)
                            .Text("Used Gloves").FontSize(SmallSize).Bold();

                        // Second header row
                        table.Cell().Border(1).BorderColor(BorderColor).Background(LightBgColor)
                            .AlignCenter().Padding(2)
                            .Text("Hand Rub").FontSize(SmallSize);

                        table.Cell().Border(1).BorderColor(BorderColor).Background(LightBgColor)
                            .AlignCenter().Padding(2)
                            .Text("Hand Wash").FontSize(SmallSize);

                        table.Cell().Border(1).BorderColor(BorderColor).Background(LightBgColor)
                            .AlignCenter().Padding(2)
                            .Text("Hand Rub").FontSize(SmallSize);

                        table.Cell().Border(1).BorderColor(BorderColor).Background(LightBgColor)
                            .AlignCenter().Padding(2)
                            .Text("Hand Wash").FontSize(SmallSize);

                        // Activity rows
                        for (int i = 1; i <= 10; i++)
                        {
                            var activity = form.Activities.ElementAtOrDefault(i - 1);
                            string activityDesc = activity?.Activity ?? "";
                            string beforeHandRub = activity?.BeforeHandRub ?? "";
                            string beforeHandWash = activity?.BeforeHandWash ?? "";
                            string afterHandRub = activity?.AfterHandRub ?? "";
                            string afterHandWash = activity?.AfterHandWash ?? "";
                            bool usedGloves = activity?.Gloves == "True";

                            // Activity number
                            table.Cell().Border(1).BorderColor(BorderColor).AlignCenter().AlignMiddle()
                                .Text(i.ToString()).FontSize(SmallSize);

                            // Activity description
                            table.Cell().Border(1).BorderColor(BorderColor).Padding(3)
                                .Text(activityDesc).FontSize(SmallSize);

                            // Before Hand Rub
                            table.Cell().Border(1).BorderColor(BorderColor).AlignCenter().AlignMiddle()
                                .Text(!string.IsNullOrEmpty(beforeHandRub) ? "✓" : "").FontSize(SmallSize);

                            // Before Hand Wash
                            table.Cell().Border(1).BorderColor(BorderColor).AlignCenter().AlignMiddle()
                                .Text(!string.IsNullOrEmpty(beforeHandWash) ? "✓" : "").FontSize(SmallSize);

                            // After Hand Rub
                            table.Cell().Border(1).BorderColor(BorderColor).AlignCenter().AlignMiddle()
                                .Text(!string.IsNullOrEmpty(afterHandRub) ? "✓" : "").FontSize(SmallSize);

                            // After Hand Wash
                            table.Cell().Border(1).BorderColor(BorderColor).AlignCenter().AlignMiddle()
                                .Text(!string.IsNullOrEmpty(afterHandWash) ? "✓" : "").FontSize(SmallSize);

                            // Gloves
                            table.Cell().Border(1).BorderColor(BorderColor).AlignCenter().AlignMiddle()
                                .Text(usedGloves ? "✓" : "").FontSize(SmallSize);
                        }
                    });
                });

                // Hand Hygiene Moments explanation and summary
                col.Item().PaddingTop(10).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(2);
                        columns.RelativeColumn();
                    });

                    // Moments of Hand Hygiene
                    table.Cell().Border(1).BorderColor(BorderColor).Padding(5).Column(legendCol =>
                    {
                        legendCol.Item().Text("MOMENTS OF HAND HYGIENE").Bold().FontSize(BodySize);

                        legendCol.Item().PaddingTop(3).Text("1. Before touching a patient")
                            .FontSize(SmallSize);

                        legendCol.Item().PaddingTop(2).Text("2. Before clean/aseptic procedure")
                            .FontSize(SmallSize);

                        legendCol.Item().PaddingTop(2).Text("3. After body fluid exposure risk")
                            .FontSize(SmallSize);

                        legendCol.Item().PaddingTop(2).Text("4. After touching a patient")
                            .FontSize(SmallSize);

                        legendCol.Item().PaddingTop(2).Text("5. After touching patient surroundings")
                            .FontSize(SmallSize);
                    });

                    // Compliance Summary
                    table.Cell().Border(1).BorderColor(BorderColor).Padding(5).Column(summaryCol =>
                    {
                        summaryCol.Item().Text("COMPLIANCE SUMMARY").Bold().FontSize(BodySize);

                        summaryCol.Item().PaddingTop(5).Table(innerTable =>
                        {
                            innerTable.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(2);
                                columns.RelativeColumn();
                            });

                            // Compliant Actions
                            innerTable.Cell().Border(1).BorderColor(BorderColor).Background(LightBgColor)
                                .Padding(3).Text("Compliant HH Actions:").FontSize(SmallSize).Bold();

                            innerTable.Cell().Border(1).BorderColor(BorderColor)
                                .Padding(3).AlignRight().Text(form.TotalCompliantActions.ToString())
                                .FontSize(SmallSize).Bold();

                            // Total Opportunities
                            innerTable.Cell().Border(1).BorderColor(BorderColor).Background(LightBgColor)
                                .Padding(3).Text("Total Observed Opportunities:").FontSize(SmallSize).Bold();

                            innerTable.Cell().Border(1).BorderColor(BorderColor)
                                .Padding(3).AlignRight().Text(form.TotalObservedOpportunities.ToString())
                                .FontSize(SmallSize).Bold();

                            // Compliance Rate
                            innerTable.Cell().Border(1).BorderColor(BorderColor).Background(LightBgColor)
                                .Padding(3).Text("Compliance Rate (%):").FontSize(SmallSize).Bold();

                            innerTable.Cell().Border(1).BorderColor(BorderColor).Background(LightBgColor)
                                .Padding(3).AlignRight().Text($"{form.ComplianceRate:F1}%")
                                .FontSize(SmallSize).Bold();
                        });
                    });
                });
            });
        }

        private void CellStyle(IContainer container)
        {
            container.DefaultTextStyle(x => x.Bold());
        }
    }
}