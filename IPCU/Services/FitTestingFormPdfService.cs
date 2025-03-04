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

                page.Header()
                    .AlignCenter()
                    .Text("Fit Testing Form")
                    .Bold()
                    .FontSize(20);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(column =>
                    {
                        column.Spacing(10);

                        column.Item().Text($"HCW Name: {fitTestingForm.HCW_Name}");
                        column.Item().Text($"DUO: {fitTestingForm.DUO}");
                        column.Item().Text($"Limitation: {fitTestingForm.Limitation}");
                        column.Item().Text($"Fit Test Solution: {fitTestingForm.Fit_Test_Solution}");
                        column.Item().Text($"Sensitivity Test: {fitTestingForm.Sensitivity_Test}");
                        column.Item().Text($"Respiratory Type: {fitTestingForm.Respiratory_Type}");
                        column.Item().Text($"Model: {fitTestingForm.Model}");
                        column.Item().Text($"Size: {fitTestingForm.Size}");
                        column.Item().Text($"Normal Breathing: {fitTestingForm.Normal_Breathing}");
                        column.Item().Text($"Deep Breathing: {fitTestingForm.Deep_Breathing}");
                        column.Item().Text($"Turn Head Side to Side: {fitTestingForm.Turn_head_side_to_side}");
                        column.Item().Text($"Move Head Up and Down: {fitTestingForm.Move_head_up_and_down}");
                        column.Item().Text($"Reading: {fitTestingForm.Reading}");
                        column.Item().Text($"Bending/Jogging: {fitTestingForm.Bending_Jogging}");
                        column.Item().Text($"Normal Breathing 2: {fitTestingForm.Normal_Breathing_2}");
                        column.Item().Text($"Name of Fit Tester: {fitTestingForm.Name_of_Fit_Tester}");
                        column.Item().Text($"DUO Tester: {fitTestingForm.DUO_Tester}");
                        column.Item().Text($"Submitted At: {fitTestingForm.SubmittedAt}");
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Generated on: ");
                        x.CurrentPageNumber();
                    });
            });
        })
        .GeneratePdf(filePath);
    }
}
