using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using IPCU.Models;

public class FitTestingFormPdfService
{
    public void GeneratePdf(FitTestingForm fitTestingForm, string filePath)
    {
        QuestPDF.Settings.License = LicenseType.Community; // Set license for QuestPDF

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                // Header Section
                page.Header()
                    .AlignCenter()
                    .Column(column =>
                    {
                        column.Spacing(5);
                        column.Item().Text("NATIONAL KIDNEY AND TRANSPLANT INSTITUTE").Bold().FontSize(18);
                        column.Item().Text("East Avenue, Quezon City").FontSize(8).AlignCenter();
                        column.Item().Text("INFECTION PREVENTION AND CONTROL UNIT (IPCU)").FontSize(10).AlignCenter();
                        column.Item().Text("QUALITATIVE RESPIRATOR FIT TESTING FORM").Bold().FontSize(14).AlignCenter();
                    });

                // Content Section
                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(column =>
                    {
                        column.Spacing(10);

                        // Healthcare Worker Information
                        column.Item().Text("Healthcare Worker Information").Bold().FontSize(14);
                        column.Item().Row(row =>
                        {
                            row.RelativeColumn().Text($"HCW Name: {fitTestingForm.HCW_Name}");
                            row.RelativeColumn().AlignRight().Text($"Department/Unit/Office: {fitTestingForm.DUO}");
                        });
                        column.Item().Row(row =>
                        {
                            row.RelativeColumn().Text($"Limitations: {fitTestingForm.Limitation}");
                        });


                        // Fit Test Details
                        column.Item().Text("Fit Test Details").Bold().FontSize(14);
                        column.Item().Row(row =>
                        {
                            row.RelativeColumn().Text($"Fit Test Solution: {fitTestingForm.Fit_Test_Solution}");
                            row.RelativeColumn().AlignRight().Text($"Sensitivity Test: {fitTestingForm.Sensitivity_Test}");

                        });
                        column.Item().Row(row =>
                        {
                            row.RelativeColumn().AlignLeft().Text($"Respirator Type: {fitTestingForm.Respiratory_Type}");
                        });

                        column.Item().Row(row =>
                        {
                            row.RelativeColumn().Text($"Model: {fitTestingForm.Model}");
                            row.RelativeColumn().AlignRight().Text($"Size: {fitTestingForm.Size}");
                        });

                        // Fit Test Activities
                        column.Item().Text("Fit Test Activities").Bold().FontSize(14);
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.ConstantColumn(50); // Pass column
                                columns.ConstantColumn(50); // Fail column
                            });

                            table.Header(header =>
                            {
                                header.Cell().Text("Activity").Bold();
                                header.Cell().Text("Pass").Bold();
                                header.Cell().Text("Fail").Bold();
                            });

                            table.Cell().Text("1. Normal Breathing");
                            table.Cell().Text(fitTestingForm.Normal_Breathing ? "✔" : "");
                            table.Cell().Text(!fitTestingForm.Normal_Breathing ? "✔" : "");

                            table.Cell().Text("2. Deep Breathing");
                            table.Cell().Text(fitTestingForm.Deep_Breathing ? "✔" : "");
                            table.Cell().Text(!fitTestingForm.Deep_Breathing ? "✔" : "");

                            table.Cell().Text("3. Turn Head Side to Side");
                            table.Cell().Text(fitTestingForm.Turn_head_side_to_side ? "✔" : "");
                            table.Cell().Text(!fitTestingForm.Turn_head_side_to_side ? "✔" : "");

                            table.Cell().Text("4. Move Head Up and Down");
                            table.Cell().Text(fitTestingForm.Move_head_up_and_down ? "✔" : "");
                            table.Cell().Text(!fitTestingForm.Move_head_up_and_down ? "✔" : "");

                            table.Cell().Text("5. Reading");
                            table.Cell().Text(fitTestingForm.Reading ? "✔" : "");
                            table.Cell().Text(!fitTestingForm.Reading ? "✔" : "");

                            table.Cell().Text("6. Bending/Jogging");
                            table.Cell().Text(fitTestingForm.Bending_Jogging ? "✔" : "");
                            table.Cell().Text(!fitTestingForm.Bending_Jogging ? "✔" : "");

                            table.Cell().Text("7. Normal Breathing (Repeat)");
                            table.Cell().Text(fitTestingForm.Normal_Breathing_2 ? "✔" : "");
                            table.Cell().Text(!fitTestingForm.Normal_Breathing_2 ? "✔" : "");
                        });

                        // Test Result
                        //column.Item().Text($"Test Result: {fitTestingForm.Test_Result}").Bold().FontSize(14);
                        column.Item().Text($"Test Result: {"SECRET LANG "}").Bold().FontSize(14);

                        // Certification Section
                        column.Item().Text("Certification").Bold().FontSize(14);
                        column.Item().Row(row =>
                        {
                            row.RelativeColumn().Text($"Name of Fit Tester: {fitTestingForm.Name_of_Fit_Tester}");
                            row.RelativeColumn().AlignRight().Text($"Date: {fitTestingForm.SubmittedAt}");
                        });

                        // Disclaimer
                        // Disclaimer Section
                        column.Item().Text("Disclaimer").Bold().FontSize(14).Underline();
                        column.Item().Border(1).Padding(10).Background(Colors.Grey.Lighten3).Text(
                            "These procedures are in accordance with accepted standards of fit testing. The above respirator fit test was performed on the HCW named, by the test administrator named. The results indicate the performance of the listed respiratory protective device under controlled conditions. Improper use, maintenance, or application of this or any other respiratory protective device will reduce or eliminate respiratory protection."
                        ).FontSize(10).LineHeight(1);
                    });

                // Footer Section
                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span("Generated on: ");
                        text.CurrentPageNumber();
                    });
            });
        })
        .GeneratePdf(filePath);
    }
}
