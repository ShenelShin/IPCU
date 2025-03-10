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
        private static readonly string DarkColor = Colors.Black;
        private static readonly string AccentColor = Colors.Indigo.Darken2;
        private static readonly string LightBgColor = Colors.Grey.Lighten4;
        private static readonly string BorderColor = Colors.Grey.Medium;
        private static readonly string HeaderBgColor = Colors.Indigo.Darken2;
        private static readonly float HeaderSize = 8;
        private static readonly float BodySize = 7;
        private static readonly float SmallSize = 7;

        public byte[] GeneratePdf(HandHygieneForm form)
        {
            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(15);                       
                    page.PageColor(Colors.White);

                    // Header 
                    page.Header().Element(ComposeHeader);

                    // Content
                    page.Content().PaddingVertical(5).Column(content =>  // Reduced padding from 10 to 5
                    {
                        content.Spacing(8);                 // Reduced spacing from 15 to 8

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

                    // Footer - more compact
                    page.Footer().Column(footer =>
                    {
                        footer.Item().AlignRight().Text(text =>
                        {
                            text.Span("Page ").FontSize(7);  // Reduced from SmallSize
                            text.CurrentPageNumber().FontSize(7);
                            text.Span(" of ").FontSize(7);
                            text.TotalPages().FontSize(7);
                        });

                        footer.Item().BorderTop(1).BorderColor(BorderColor).PaddingTop(3)  // Reduced padding
                            .Text("Hand Hygiene Observation Form - Generated on " + DateTime.Now.ToString("MM/dd/yyyy"))
                            .FontSize(7).FontColor(Colors.Grey.Medium);                     // Reduced from 8
                    });
                });
            }).GeneratePdf();
        }

        private void ComposeHeader(IContainer container)
        {
            container.Column(header =>
            {
                // Logo and title - more compact
                header.Item().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().AlignCenter().Text("NATIONAL KIDNEY AND TRANSPLANT INSTITUTE")
                            .FontColor(DarkColor).Bold().FontSize(12);  // Reduced from 14

                        col.Item().AlignCenter().Text("East Avenue, Quezon City")
                            .FontColor(DarkColor).FontSize(9);          // Reduced from 10

                        col.Item().AlignCenter().Text("INFECTION PREVENTION AND CONTROL UNIT")
                            .FontColor(DarkColor).Bold().FontSize(10);  // Reduced from 11
                    });
                });

                // Form title
                header.Item().Background(HeaderBgColor).Padding(3).AlignCenter()  // Reduced padding from 5 to 3
                    .Text("HAND HYGIENE (HH) DETAILED OBSERVATION AND MONITORING FORM")
                    .FontColor(Colors.White).Bold().FontSize(11);               // Reduced from 12

                header.Item().PaddingTop(2).Row(row =>                       // Reduced padding
                {
                    row.RelativeItem();
                    row.ConstantItem(100).Text("IPC-WIF-005_ver1")
                        .FontColor(DarkColor).FontSize(7);                   // Reduced from 8
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
                        .Padding(3).Text("AREA").Bold().FontSize(SmallSize);  // Reduced padding from 5 to 3

                    table.Cell().Border(1).BorderColor(BorderColor).Background(LightBgColor)
                        .Padding(3).Text("DATE").Bold().FontSize(SmallSize);

                    // Values Row 1
                    table.Cell().Border(1).BorderColor(BorderColor)
                        .Padding(3).Text(form.Area ?? "").FontSize(SmallSize); 

                    table.Cell().Border(1).BorderColor(BorderColor)
                        .Padding(3).Text(form.Date.ToString("MM/dd/yyyy")).FontSize(SmallSize);

                    // Header Row 2
                    table.Cell().Border(1).BorderColor(BorderColor).Background(LightBgColor)
                        .Padding(3).Text("OBSERVER").Bold().FontSize(SmallSize);

                    table.Cell().Border(1).BorderColor(BorderColor).Background(LightBgColor)
                        .Padding(3).Text("TIME").Bold().FontSize(SmallSize);

                    // Values Row 2
                    table.Cell().Border(1).BorderColor(BorderColor)
                        .Padding(3).Text(form.Observer ?? "").FontSize(SmallSize);

                    // Time formatting
                    var formattedTime = FormatTime(form.Time);
                    table.Cell().Border(1).BorderColor(BorderColor)
                        .Padding(3).Text(formattedTime).FontSize(SmallSize);
                });

                // Instructions section - more compact
                mainColumn.Item().PaddingTop(3).Border(1).BorderColor(BorderColor).Padding(3)  // Reduced padding
                    .Background(LightBgColor).Column(instructionsCol =>
                    {
                        instructionsCol.Item().Text("INSTRUCTIONS:").Bold().FontSize(SmallSize);
                        instructionsCol.Item().Text("• Observe only one HCW at a time from the time they enter a patient's environment to the time they exit.")
                            .FontSize(7);  // Even smaller for instructions
                        instructionsCol.Item().Text("• You do not need to write their name but document their designation.")
                            .FontSize(7);
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
                    .Padding(3).Text("HEALTHCARE WORKER INFORMATION")  // Reduced padding from 5 to 3
                    .FontColor(Colors.White).FontSize(HeaderSize).Bold();

                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn(3);
                    });

                    // HCW Name
                    table.Cell().Border(1).BorderColor(BorderColor).Padding(3).Column(nameCol =>
                    {
                        nameCol.Item().Text("NAME (Optional)").Bold().FontSize(BodySize);
                        nameCol.Item().PaddingTop(2).Text(form.Name ?? "N/A").FontSize(BodySize);
                    });

                    // HCW Type
                    table.Cell().Border(1).BorderColor(BorderColor).Padding(3).Column(typeCol =>
                    {
                        typeCol.Item().Text("HEALTHCARE WORKER TYPE").Bold().FontSize(BodySize);
                        typeCol.Item().PaddingTop(2).Table(innerTable =>  // Reduced padding
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
                                        row.ConstantItem(12).Text(isSelected ? "☑" : "☐").FontSize(BodySize);  // Reduced from 15
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
                                    row.ConstantItem(12).Text(isOther ? "☑" : "☐").FontSize(BodySize);  // Reduced from 15
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
                    .Padding(3).Text("ROOM AND ISOLATION INFORMATION")  // Reduced padding from 5 to 3
                    .FontColor(Colors.White).FontSize(HeaderSize).Bold();

                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn();
                        columns.RelativeColumn();
                    });

                    // Room Type
                    table.Cell().Border(1).BorderColor(BorderColor).Padding(3).Column(roomCol =>
                    {
                        roomCol.Item().Text("ROOM TYPE").Bold().FontSize(BodySize);
                        roomCol.Item().Text("(For in-patients only)").FontSize(7).Italic();

                        string[] roomTypes = { "ICE/IMCU", "AIIR", "Ward" };
                        foreach (var type in roomTypes)
                        {
                            roomCol.Item().PaddingTop(1).Row(row =>  // Reduced padding from 2 to 1
                            {
                                bool isSelected = form.RoomType == type;
                                row.ConstantItem(12).Text(isSelected ? "☑" : "☐").FontSize(BodySize);  // Reduced from 15
                                row.RelativeItem().Text(type).FontSize(BodySize);
                            });
                        }

                        // Other option for Room Type
                        roomCol.Item().PaddingTop(1).Row(row =>  // Reduced padding
                        {
                            bool isOther = form.RoomType != null && !roomTypes.Contains(form.RoomType);
                            row.ConstantItem(12).Text(isOther ? "☑" : "☐").FontSize(BodySize);  // Reduced from 15
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
                    table.Cell().Border(1).BorderColor(BorderColor).Padding(3).Column(isoCol =>
                    {
                        isoCol.Item().Text("ISOLATION PRECAUTIONS").Bold().FontSize(BodySize);

                        isoCol.Item().PaddingTop(1).Row(row =>  // Reduced padding
                        {
                            row.ConstantItem(12).Text(!form.Isolation ? "☑" : "☐").FontSize(BodySize);  // Reduced from 15
                            row.RelativeItem().Text("None").FontSize(BodySize);
                        });

                        isoCol.Item().PaddingTop(1).Row(row =>  // Reduced padding
                        {
                            row.ConstantItem(12).Text(form.Isolation ? "☑" : "☐").FontSize(BodySize);  // Reduced from 15
                            row.RelativeItem().Text("Yes (check type below)").FontSize(BodySize);
                        });

                        if (form.Isolation)
                        {
                            isoCol.Item().PaddingLeft(15).PaddingTop(2).Table(innerTable =>  // Reduced padding
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
                                            r.ConstantItem(12).Text(isSelected ? "☑" : "☐").FontSize(BodySize);  // Reduced from 15
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
                    .Padding(3).Text("OBSERVATION DETAILS")  // Reduced padding from 5 to 3
                    .FontColor(Colors.White).FontSize(HeaderSize).Bold();

                col.Item().Border(1).BorderColor(BorderColor).Padding(3).Column(obsCol =>  // Reduced padding
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
                        obsCol.Item().PaddingTop(1).Row(row =>  // Reduced padding from 3 to 1
                        {
                            bool isSelected = form.ObsvPatientCare == option;
                            row.ConstantItem(12).Text(isSelected ? "☑" : "☐").FontSize(BodySize);  // Reduced from 15
                            row.RelativeItem().Text(option).FontSize(BodySize);
                        });
                    }

                    // Other option
                    obsCol.Item().PaddingTop(1).Row(row =>  // Reduced padding
                    {
                        bool isOther = form.ObsvPatientCare != null && !observationOptions.Contains(form.ObsvPatientCare);
                        row.ConstantItem(12).Text(isOther ? "☑" : "☐").FontSize(BodySize);  // Reduced from 15
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
                    table.Cell().Border(1).BorderColor(BorderColor).Padding(3).Column(resourceCol =>  // Reduced padding
                    {
                        resourceCol.Item().Text("HAND HYGIENE RESOURCES AVAILABLE").Bold().FontSize(BodySize);

                        // Yes option
                        resourceCol.Item().PaddingTop(1).Row(row =>  // Reduced padding
                        {
                            bool hasEnvironment = form.ObsvPatientEnvironment?.Equals("on", StringComparison.OrdinalIgnoreCase) ?? false;
                            row.ConstantItem(12).Text(hasEnvironment ? "☑" : "☐").FontSize(BodySize);  // Reduced from 15
                            row.RelativeItem().Text("Yes").FontSize(BodySize);
                        });

                        // Show resources only if "Yes" is selected
                        if (form.ObsvPatientEnvironment?.Equals("on", StringComparison.OrdinalIgnoreCase) ?? false)
                        {
                            resourceCol.Item().PaddingLeft(15).PaddingTop(1).Row(row =>  // Reduced padding
                            {
                                bool hasHandSanitizer = form.EnvironmentResource?.Equals("Hand sanitizer", StringComparison.OrdinalIgnoreCase) ?? false;
                                row.ConstantItem(12).Text(hasHandSanitizer ? "☑" : "☐").FontSize(BodySize);  // Reduced from 15
                                row.RelativeItem().Text("Hand sanitizer").FontSize(BodySize);
                            });

                            resourceCol.Item().PaddingLeft(15).PaddingTop(1).Row(row =>  // Reduced padding
                            {
                                bool hasSoapWater = form.EnvironmentResource?.Equals("Soap and water", StringComparison.OrdinalIgnoreCase) ?? false;
                                row.ConstantItem(12).Text(hasSoapWater ? "☑" : "☐").FontSize(BodySize);  // Reduced from 15
                                row.RelativeItem().Text("Soap and water").FontSize(BodySize);
                            });
                        }

                        // No option
                        resourceCol.Item().PaddingTop(1).Row(row =>  // Reduced padding
                        {
                            bool noEnvironment = form.ObsvPatientEnvironment?.Equals("off", StringComparison.OrdinalIgnoreCase) ?? false;
                            row.ConstantItem(12).Text(noEnvironment ? "☑" : "☐").FontSize(BodySize);  // Reduced from 15
                            row.RelativeItem().Text("No").FontSize(BodySize);
                        });
                    });

                    // Contact with objects Cell
                    table.Cell().Border(1).BorderColor(BorderColor).Padding(3).Column(contactCol =>  // Reduced padding
                    {
                        contactCol.Item().Text("OBJECTS HCW HAD CONTACT WITH").Bold().FontSize(BodySize);
                        contactCol.Item().Text("(Before touching the patient)").FontSize(SmallSize).Italic();
                        contactCol.Item().PaddingTop(2).Text(form.ObsvPatientContact ?? "None observed").FontSize(BodySize);  // Reduced padding
                    });
                });
            });
        }

        private void CreateActivitiesTable(IContainer container, HandHygieneForm form)
        {
            container.Column(col =>
            {
                col.Item().BorderBottom(0).Border(1).BorderColor(BorderColor).Background(AccentColor)
                    .Padding(5).Text("HAND HYGIENE OBSERVED ACTIVITIES")
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

                            // Before Hand Rub - Format and display numbers with symbols
                            table.Cell().Border(1).BorderColor(BorderColor).AlignCenter().AlignMiddle()
                                .Element(e => FormatHygieneCell(e, beforeHandRub));

                            // Before Hand Wash - Format and display numbers with symbols
                            table.Cell().Border(1).BorderColor(BorderColor).AlignCenter().AlignMiddle()
                                .Element(e => FormatHygieneCell(e, beforeHandWash));

                            // After Hand Rub - Format and display numbers with symbols
                            table.Cell().Border(1).BorderColor(BorderColor).AlignCenter().AlignMiddle()
                                .Element(e => FormatHygieneCell(e, afterHandRub));

                            // After Hand Wash - Format and display numbers with symbols
                            table.Cell().Border(1).BorderColor(BorderColor).AlignCenter().AlignMiddle()
                                .Element(e => FormatHygieneCell(e, afterHandWash));

                            // Gloves
                            table.Cell().Border(1).BorderColor(BorderColor).AlignCenter().AlignMiddle()
                                .Text(!string.IsNullOrEmpty(activityDesc) ? (usedGloves ? "✓" : "✗") : "").FontSize(SmallSize);
                        }
                    });
                });

                // Side-by-side container for Moments and Compliance Summary
                col.Item().Element(container =>
                {
                    container.Row(row =>
                    {
                        // Left side - Hand Hygiene Moments
                        row.RelativeItem(3).Column(momentCol =>
                        {
                            momentCol.Item().BorderBottom(0).Border(1).BorderColor(BorderColor).Background(AccentColor)
                                .Padding(5).Text("RUBRICS OF HAND HYGIENE")
                                .FontColor(Colors.White).FontSize(HeaderSize).Bold();

                            momentCol.Item().Element(container =>
                            {
                                container.Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.ConstantColumn(30);      // Moment number
                                        columns.RelativeColumn();        // Moment description
                                    });

                                    // Moments rows
                                    string[] moments = new[]
                                    {
                                "Before touching a patient",
                                "Before clean/aseptic procedure",
                                "After body fluid exposure risk",
                                "After touching a patient",
                                "After touching patient surroundings"
                            };

                                    for (int i = 0; i < moments.Length; i++)
                                    {
                                        table.Cell().Border(1).BorderColor(BorderColor).Background(LightBgColor)
                                            .AlignCenter().AlignMiddle()
                                            .Text((i + 1).ToString()).FontSize(SmallSize).Bold();

                                        table.Cell().Border(1).BorderColor(BorderColor)
                                            .Padding(3)
                                            .Text(moments[i]).FontSize(SmallSize);
                                    }
                                });
                            });
                        });

                        // Right side - Compliance Summary
                        row.RelativeItem(2).Column(complianceCol =>
                        {
                            complianceCol.Item().BorderBottom(0).Border(1).BorderColor(BorderColor).Background(AccentColor)
                                .Padding(5).Text("COMPLIANCE SUMMARY")
                                .FontColor(Colors.White).FontSize(HeaderSize).Bold();

                            complianceCol.Item().Element(container =>
                            {
                                container.Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(2);      // Description
                                        columns.RelativeColumn();       // Count/Value
                                    });

                                    // Calculate compliance
                                    int totalOpportunities = CalculateTotalOpportunities(form);
                                    int compliantActions = CalculateCompliantActions(form);
                                    double complianceRate = totalOpportunities > 0
                                        ? (double)compliantActions / totalOpportunities * 100
                                        : 0;

                                    // Data rows
                                    table.Cell().Border(1).BorderColor(BorderColor)
                                        .Padding(3)
                                        .Text("Number of Compliant HH Actions:").FontSize(SmallSize);

                                    table.Cell().Border(1).BorderColor(BorderColor)
                                        .Padding(3).AlignCenter()
                                        .Text(compliantActions.ToString()).FontSize(SmallSize);

                                    table.Cell().Border(1).BorderColor(BorderColor)
                                        .Padding(3)
                                        .Text("Total Observed Opportunities:").FontSize(SmallSize);

                                    table.Cell().Border(1).BorderColor(BorderColor)
                                        .Padding(3).AlignCenter()
                                        .Text(totalOpportunities.ToString()).FontSize(SmallSize);

                                    table.Cell().Border(1).BorderColor(BorderColor)
                                        .Padding(3)
                                        .Text("Compliance Rate (%):").FontSize(SmallSize).Bold();

                                    table.Cell().Border(1).BorderColor(BorderColor)
                                        .Padding(3).AlignCenter()
                                        .Text($"{complianceRate:F1}%").FontSize(SmallSize).Bold();
                                });
                            });
                        });
                    });
                });
            });
        }

        // Helper methods for compliance calculation
        private int CalculateTotalOpportunities(HandHygieneForm form)
        {
            int count = 0;
            foreach (var activity in form.Activities)
            {
                // Count all non-empty entries as opportunities
                if (!string.IsNullOrEmpty(activity.BeforeHandRub)) count += CountEntries(activity.BeforeHandRub);
                if (!string.IsNullOrEmpty(activity.BeforeHandWash)) count += CountEntries(activity.BeforeHandWash);
                if (!string.IsNullOrEmpty(activity.AfterHandRub)) count += CountEntries(activity.AfterHandRub);
                if (!string.IsNullOrEmpty(activity.AfterHandWash)) count += CountEntries(activity.AfterHandWash);
            }
            return count;
        }

        private int CalculateCompliantActions(HandHygieneForm form)
        {
            int count = 0;
            foreach (var activity in form.Activities)
            {
                // Count entries marked with ✓ as compliant
                count += CountCompliantEntries(activity.BeforeHandRub);
                count += CountCompliantEntries(activity.BeforeHandWash);
                count += CountCompliantEntries(activity.AfterHandRub);
                count += CountCompliantEntries(activity.AfterHandWash);
            }
            return count;
        }

        private int CountEntries(string data)
        {
            if (string.IsNullOrEmpty(data)) return 0;
            return data.Split(';').Length;
        }

        private int CountCompliantEntries(string data)
        {
            if (string.IsNullOrEmpty(data)) return 0;

            int compliantCount = 0;
            var entries = data.Split(';');

            foreach (var entry in entries)
            {
                if (string.IsNullOrEmpty(entry)) continue;

                var parts = entry.Split(',');
                if (parts.Length == 2 && parts[1] == "✓")
                {
                    compliantCount++;
                }
            }

            return compliantCount;
        }

        // Helper method to format hygiene cells with numbers and symbols
        private void FormatHygieneCell(IContainer container, string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return;
            }

            // Split multiple entries (format: "1,✓;2,X;3,✓")
            var entries = data.Split(';');

            container.Text(text =>
            {
                bool isFirst = true;
                foreach (var entry in entries)
                {
                    if (string.IsNullOrEmpty(entry)) continue;

                    var parts = entry.Split(',');
                    if (parts.Length == 2)
                    {
                        string number = parts[0];
                        string symbol = parts[1];

                        // Add a space between entries (except for the first one)
                        if (!isFirst)
                        {
                            text.Span(" ").FontSize(SmallSize);
                        }

                        text.Span($"{number}{symbol}").FontSize(SmallSize);
                        isFirst = false;
                    }
                }
            });
        }

        private void CellStyle(IContainer container)
        {
            container.DefaultTextStyle(x => x.Bold());
        }
    }
}