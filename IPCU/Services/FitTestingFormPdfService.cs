using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using IPCU.Models;
public class FitTestingFormPdfService
{
    // Reduced font sizes
    private static readonly string DarkColor = Colors.Grey.Darken3;
    private static readonly string AccentColor = Colors.Indigo.Darken4;
    private static readonly float HeaderSize = 12;  // Reduced from 14
    private static readonly float BodySize = 10;    // Reduced from 12
    private byte[] LoadLogo()
    {
        // Replace "path/to/your/logo.png" with the actual path to your logo file
        return File.ReadAllBytes("wwwroot/images/NKTI.png");
    }

    public byte[] GeneratePdf(FitTestingForm fitTestingForm)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        return Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(15);  // Reduced from 25

                // Header Section with tighter spacing
                // Header Section with logo and text
                page.Header().Row(header =>
                {
                    // Logo column (left side)
                    header.ConstantColumn(50).Image(LoadLogo())
                        .FitArea();
                        //.MarginRight(10);

                    // Text column (center)
                    header.RelativeColumn().Column(col =>
                    {
                        col.Spacing(5);
                        col.Item().AlignCenter().Text("NATIONAL KIDNEY AND TRANSPLANT INSTITUTE")
                            .FontSize(14)
                            .Bold()
                            .FontColor(AccentColor);

                        col.Item().AlignCenter().Text("East Avenue, Quezon City")
                            .FontSize(9)
                            .Bold()
                            .FontColor(DarkColor);

                        col.Item().PaddingVertical(5)
                            .LineHorizontal(1).LineColor(Colors.Grey.Darken3);

                        col.Item().AlignCenter().Text("INFECTION PREVENTION AND CONTROL UNIT")
                            .FontSize(9)
                            .Bold()
                            .FontColor(DarkColor);

                        col.Item().AlignCenter().Text("QUALITATIVE RESPIRATOR FIT TESTING FORM")
                            .FontSize(9)
                            .Bold()
                            .FontColor(DarkColor);
                    });
                });


                page.Content().Column(content =>
                {
                    content.Spacing(8);  // Reduced from 15

                    // Healthcare Worker Information
                    content.Item().Border(1).BorderColor(Colors.Grey.Darken2).Column(col =>
                    {
                        col.Item().Padding(5).Table(table =>  // Reduced padding
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            AddTableRow(table, "HCW Name:", fitTestingForm.HCW_Name);
                            AddTableRow(table, "Department/Unit/Office:", fitTestingForm.DUO);
                            AddTableRow(table, "Limitations:", string.Join(", ", fitTestingForm.Limitation));
                        });
                    });

                    // Fit Test Details
                    content.Item().Border(1).BorderColor(Colors.Grey.Darken2).Column(col =>
                    {
                        col.Item().Padding(5).Table(table =>  // Reduced padding
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            AddTableRow(table, "Fit Test Solution:", fitTestingForm.Fit_Test_Solution);
                            AddTableRow(table, "Sensitivity Test:", fitTestingForm.Sensitivity_Test);
                            AddTableRow(table, "Respirator Type:", fitTestingForm.Respiratory_Type);
                            AddTableRow(table, "Model/Size:", $"{fitTestingForm.Model} - {fitTestingForm.Size}");
                        });
                    });

                    // Fit Test Activities
                    content.Item().Border(1).BorderColor(Colors.Grey.Darken2).Column(col =>
                    {
                        col.Item().Padding(5).Background(Colors.Grey.Lighten3)  // Reduced padding
                            .Text("FIT TEST ACTIVITIES")
                            .FontSize(HeaderSize)
                            .Bold()
                            .FontColor(DarkColor);

                        col.Item().Padding(5).Table(table =>  // Reduced padding
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.ConstantColumn(40);  // Narrower columns
                                columns.ConstantColumn(40);
                            });

                            // Header with tighter padding
                            table.Header(header =>
                            {
                                header.Cell().BorderBottom(1).PaddingVertical(3)  // Reduced padding
                                    .Text("Activity").Bold().FontColor(AccentColor);
                                header.Cell().BorderBottom(1).PaddingVertical(3)
                                    .Text("Pass").Bold().FontColor(AccentColor);
                                header.Cell().BorderBottom(1).PaddingVertical(3)
                                    .Text("Fail").Bold().FontColor(AccentColor);
                            });

                            // Modified rows with tighter spacing
                            void AddCompactActivityRow(string activity, bool? result)
                            {
                                table.Cell().BorderBottom(1).PaddingVertical(3)  // Reduced padding
                                    .Text(activity).FontSize(BodySize);
                                table.Cell().BorderBottom(1).PaddingVertical(3)
                                    .AlignCenter().Text(result == true ? "✔" : "").FontColor(Colors.Green.Darken3);
                                table.Cell().BorderBottom(1).PaddingVertical(3)
                                    .AlignCenter().Text(result == false ? "✘" : "").FontColor(Colors.Red.Darken3);
                            }

                            AddCompactActivityRow("1. Normal Breathing", fitTestingForm.Normal_Breathing);
                            AddCompactActivityRow("2. Deep Breathing", fitTestingForm.Deep_Breathing);
                            AddCompactActivityRow("3. Head Side", fitTestingForm.Turn_head_side_to_side);
                            AddCompactActivityRow("4. Head Up/Down", fitTestingForm.Move_head_up_and_down);
                            AddCompactActivityRow("5. Reading", fitTestingForm.Reading);
                            AddCompactActivityRow("6. Bending/Jogging", fitTestingForm.Bending_Jogging);
                            AddCompactActivityRow("7. Normal Repeat", fitTestingForm.Normal_Breathing_2);

                            table.Cell().BorderBottom(1).PaddingVertical(3)  // Reduced padding
                                .Text("TEST RESULT (Pass/Fail):").FontSize(BodySize).FontColor(DarkColor);
                            table.Cell().ColumnSpan(2).BorderBottom(1).PaddingVertical(3)  // Span across Pass/Fail columns
                                .Text(fitTestingForm.Test_Results).FontSize(BodySize).FontColor(
                                    fitTestingForm.Test_Results == "Passed" ? Colors.Green.Darken3 : Colors.Red.Darken3);
                        });
                    });

                    // Certification Section
                    content.Item().Border(1).BorderColor(Colors.Grey.Darken2).Column(col =>
                    {
                        col.Item().Padding(5).Table(table =>  // Reduced padding
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            AddTableRow(table, "Fit Tester:", fitTestingForm.Name_of_Fit_Tester);
                            AddTableRow(table, "Date:", fitTestingForm.SubmittedAt.ToString("MMM dd, yyyy"));
                            AddTableRow(table, "Date:", fitTestingForm.ExpiringAt.ToString("MMM dd, yyyy"));

                        });
                    });

                    // Compact Disclaimer
                    content.Item().Border(1).BorderColor(Colors.Grey.Darken2).Column(col =>
                    {
                        col.Item().Padding(5).Background(Colors.Grey.Lighten3)
                            .Text("DISCLAIMER")
                            .FontSize(HeaderSize)
                            .Bold()
                            .FontColor(DarkColor);

                        col.Item().Padding(5).Text(text =>  // Reduced padding
                        {
                            text.Span("These procedures are in accordance with accepted standards " +
                                "of fit testing. The above respirator fit test was performed " +
                                "on\r\nthe HCW named, by the test administrator named. " +
                                "The results indicate the performance of the listed respiratory\r\nprotective " +
                                "device under controlled conditions. The fit test measured the ability of the " +
                                "respiratory protective device to\r\nprovide protection to the individual tested. " +
                                "Improper use, maintenance, or application of this or any other respiratory\r\nprotective " +
                                "device will reduce or eliminate respiratory protection.")
                                .FontSize(BodySize - 1)  // Smaller than body text
                                .FontColor(Colors.Grey.Darken2)
                                .LineHeight(1);  // Tighter line spacing
                        });
                    });
                });

                // Compact Footer
                page.Footer().AlignCenter().Text(t =>
                {
                    t.Span("Page ").FontSize(BodySize).FontColor(Colors.Grey.Medium);
                    t.CurrentPageNumber().FontSize(BodySize).FontColor(AccentColor);
                    t.Span(" of ").FontSize(BodySize).FontColor(Colors.Grey.Medium);
                    t.TotalPages().FontSize(BodySize).FontColor(AccentColor);
                });
            });
        }).GeneratePdf();
    }

    private void AddTableRow(TableDescriptor table, string label, string value)
    {
        table.Cell().BorderBottom(1).PaddingVertical(3)  // Reduced padding
            .Text(label).FontSize(BodySize).Bold().FontColor(DarkColor);
        table.Cell().BorderBottom(1).PaddingVertical(3)
            .Text(value).FontSize(BodySize).FontColor(Colors.Grey.Darken2);
    }
}