using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;

namespace IPCU.Services
{
    public class MonthlySummaryPdfService
    {
        public byte[] GeneratePdf(
            DateTime month,
            Dictionary<string, (int compliant, int total, decimal rate)> areaSummaries,
            Dictionary<string, (int compliant, int total, decimal rate)> nurseAreaSummaries,
            Dictionary<string, (int compliant, int total, decimal rate)> professionSummaries,
            Dictionary<string, (int compliant, int total, decimal rate)> observerSummaries)
        {
            // Calculate overall compliance
            int totalCompliant = 0;
            int totalOpportunities = 0;
            decimal overallCompliance = 0;

            foreach (var area in areaSummaries)
            {
                totalCompliant += area.Value.compliant;
                totalOpportunities += area.Value.total;
            }

            overallCompliance = totalOpportunities > 0 ? (decimal)totalCompliant / totalOpportunities : 0;

            // Helper function for compliance color
            string GetComplianceColor(decimal compliance) =>
                compliance >= 0.8m ? Colors.Green.Medium :
                compliance >= 0.6m ? Colors.Orange.Medium :
                Colors.Red.Medium;

            // Helper function for compliance message
            string GetComplianceMessage(decimal compliance) =>
                compliance >= 0.8m ? "Great compliance level" :
                compliance >= 0.6m ? "Compliance needs improvement" :
                "Critical compliance level";

            // Generate PDF - using a minimal approach but with all data
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(20);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    // Header
                    page.Header().Row(row =>
                    {
                        row.RelativeItem().Column(col =>
                        {
                            col.Item().Text("Hand Hygiene Compliance Summary").FontSize(16).Bold();
                            col.Item().Text($"Month: {month:MMMM yyyy}").FontSize(12);
                            col.Item().Text($"Generated: {DateTime.Now:g}").FontSize(9);
                        });
                    });

                    // Content
                    page.Content().Padding(10).Column(column =>
                    {
                        column.Spacing(15);

                        // Overall compliance section
                        column.Item().Row(row =>
                        {
                            // Summary box
                            row.RelativeItem().Border(1)
                                .BorderColor(Colors.Grey.Lighten5)
                                .Padding(10)
                                .Column(col =>
                                {
                                    col.Item().Text("Overall Compliance").SemiBold();
                                    col.Item().PaddingVertical(5)
                                         .Text($"{overallCompliance:P1}")
                                         .FontSize(20)
                                         .FontColor(GetComplianceColor(overallCompliance))
                                         .Bold();
                                    col.Item().Text($"{totalCompliant} of {totalOpportunities} opportunities")
                                         .FontSize(9)
                                         .FontColor(Colors.Grey.Medium);
                                });

                            // Status box
                            row.RelativeItem()
                                .Background(GetComplianceColor(overallCompliance))
                                .Padding(10)
                                .Text(GetComplianceMessage(overallCompliance))
                                .Bold()
                                .FontColor(Colors.White);
                        });

                        // Section: Area summaries (All staff)
                        column.Item().Text("Compliance by Area (All Professions)")
                            .Bold().FontSize(12);
                        column.Item().CreateSummaryTable(areaSummaries, "Area", GetComplianceColor);

                        // Section: Nurse area summaries
                        column.Item().Text("Compliance by Area (Nurses Only)")
                            .Bold().FontSize(12);
                        column.Item().CreateSummaryTable(nurseAreaSummaries, "Area", GetComplianceColor);

                        // Section: Profession summaries
                        column.Item().Text("Compliance by Profession")
                            .Bold().FontSize(12);
                        column.Item().CreateSummaryTable(professionSummaries, "Profession", GetComplianceColor);

                        // Section: Observer summaries
                        column.Item().Text("Compliance by Observer")
                            .Bold().FontSize(12);
                        column.Item().CreateSummaryTable(observerSummaries, "Observer", GetComplianceColor);
                    });

                    // Footer
                    page.Footer().AlignCenter().Text(text =>
                    {
                        text.Span("Page ").FontSize(9);
                        text.CurrentPageNumber().FontSize(9);
                        text.Span(" of ").FontSize(9);
                        text.TotalPages().FontSize(9);
                    });
                });
            });

            return document.GeneratePdf();
        }
    }
}

// Extension method to create summary tables
public static class DocumentExtensions
{
    public static void CreateSummaryTable(
        this IContainer container,
        Dictionary<string, (int compliant, int total, decimal rate)> data,
        string categoryName,
        Func<decimal, string> getColorFunc)
    {
        container.Table(table =>
        {
            // Define columns
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(3);
                columns.RelativeColumn(2);
                columns.RelativeColumn(2);
                columns.RelativeColumn(2);
            });

            // Add header
            table.Header(header =>
            {
                header.Cell().Background(Colors.Grey.Lighten2)
                      .Padding(2).Text(categoryName).Bold();
                header.Cell().Background(Colors.Grey.Lighten2)
                      .Padding(2).Text("Compliant Actions").Bold();
                header.Cell().Background(Colors.Grey.Lighten2)
                      .Padding(2).Text("Observed Opportunities").Bold();
                header.Cell().Background(Colors.Grey.Lighten2)
                      .Padding(2).Text("Compliance Rate").Bold();
            });

            // Add data rows
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3)
                         .Padding(2).Text(item.Key);
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3)
                         .Padding(2).Text(item.Value.compliant.ToString());
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3)
                         .Padding(2).Text(item.Value.total.ToString());
                    table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten3)
                         .Padding(2).Text(item.Value.rate.ToString("P2"))
                         .FontColor(getColorFunc(item.Value.rate));
                }
            }
            else
            {
                table.Cell().ColumnSpan(4).Padding(5)
                     .Text("No data available").FontColor(Colors.Grey.Medium);
            }
        });
    }
}