using IPCU.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;

public class EvaluationReportDocument : IDocument
{
    private readonly List<EvaluationSummaryViewModel> _evaluations;

    public EvaluationReportDocument(List<EvaluationSummaryViewModel> evaluations)
    {
        _evaluations = evaluations;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(30);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontSize(12).FontColor(Colors.Black));

            // Header
            page.Header().Text("Evaluation Summary Report")
                .FontSize(20).Bold().FontColor(Colors.Blue.Medium).AlignCenter();

            // Content
            page.Content().PaddingVertical(10).Element(ComposeContent);

            // Footer
            page.Footer().AlignRight().Text(text =>
            {
                text.Span("Generated on: ").FontSize(10);
                text.Span($"{System.DateTime.Now:MMMM d, yyyy}").FontSize(10).Bold();
            });
        });
    }

    private void ComposeContent(IContainer container)
    {
        foreach (var eval in _evaluations)
        {
            container.PaddingBottom(20).Column(column =>
            {
                // Training Details
                column.Item().Text($"Training Date: {eval.TrainingDate:MMMM d, yyyy}")
                    .FontSize(14).Bold();
                column.Item().Text($"Venue: {eval.Venue ?? "N/A"}")
                    .FontSize(12);
                column.Item().Text($"SME Lecturer: {eval.SMELecturer ?? "N/A"}")
                    .FontSize(12);

                column.Spacing(10);

                // Participant Statistics
                column.Item().Row(row =>
                {
                    row.RelativeColumn(1).Text($"Total Participants: {eval.TotalParticipants}")
                        .FontSize(12).Bold();
                    row.RelativeColumn(1).Text($"Male: {eval.MaleCount}")
                        .FontSize(12).Bold();
                    row.RelativeColumn(1).Text($"Female: {eval.FemaleCount}")
                        .FontSize(12).Bold();
                });

                column.Spacing(10);

                // Averages Table
                column.Item().Text("Evaluation Averages").FontSize(14).Bold().Underline();
                column.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(2); // Criteria
                        columns.RelativeColumn(1); // Average Score
                    });

                    // Table Header
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("Criteria").Bold();
                        header.Cell().Element(CellStyle).Text("Average Score").Bold();
                    });

                    // Table Rows for Averages
                    table.Cell().Element(CellStyle).Text("Flow Followed");
                    table.Cell().Element(CellStyle).Text(eval.AverageFlowFollowed.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Rules Established");
                    table.Cell().Element(CellStyle).Text(eval.AverageRulesEstablished.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Initiate Discussion");
                    table.Cell().Element(CellStyle).Text(eval.AverageInitiateDiscussion.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Technical Capability");
                    table.Cell().Element(CellStyle).Text(eval.AverageTechnicalCapability.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Content Organization");
                    table.Cell().Element(CellStyle).Text(eval.AverageContentOrganization.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Objective Stated");
                    table.Cell().Element(CellStyle).Text(eval.AverageObjectiveStated.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Content Quality");
                    table.Cell().Element(CellStyle).Text(eval.AverageContentQuality.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Flow of Topic");
                    table.Cell().Element(CellStyle).Text(eval.AverageFlowOfTopic.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Relevance of Topic");
                    table.Cell().Element(CellStyle).Text(eval.AverageRelevanceOfTopic.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Practice Application");
                    table.Cell().Element(CellStyle).Text(eval.AveragePracticeApplication.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Learning Activities");
                    table.Cell().Element(CellStyle).Text(eval.AverageLearningActivities.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Visual Aids");
                    table.Cell().Element(CellStyle).Text(eval.AverageVisualAids.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Present Knowledge");
                    table.Cell().Element(CellStyle).Text(eval.AveragePresentKnowledge.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Balance of Principles");
                    table.Cell().Element(CellStyle).Text(eval.AverageBalancePrinciples.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Address Clarifications");
                    table.Cell().Element(CellStyle).Text(eval.AverageAddressClarifications.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Teaching Personality");
                    table.Cell().Element(CellStyle).Text(eval.AverageTeachingPersonality.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Preparedness");
                    table.Cell().Element(CellStyle).Text(eval.AveragePreparedness.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Establish Rapport");
                    table.Cell().Element(CellStyle).Text(eval.AverageEstablishRapport.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Respect for Participants");
                    table.Cell().Element(CellStyle).Text(eval.AverageRespectForParticipants.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Voice/Personality");
                    table.Cell().Element(CellStyle).Text(eval.AverageVoicePersonality.ToString("F2"));

                    table.Cell().Element(CellStyle).Text("Time Management");
                    table.Cell().Element(CellStyle).Text(eval.AverageTimeManagement.ToString("F2"));
                });

                column.Spacing(10);

                // Post-Evaluation Average
                column.Item().Text("Post-Evaluation Average").FontSize(14).Bold().Underline();
                column.Item().Text($"Post-Test Evaluation Grade: {eval.PostTestEvaluationGrade:F2}%")
                    .FontSize(12).Bold().FontColor(Colors.Green.Medium);


                column.Spacing(10);

                // Combined Suggestions
                column.Item().Text("Combined Suggestions").FontSize(14).Bold().Underline();
                column.Item().Text(eval.CombinedSuggestions ?? "N/A")
                    .FontSize(12).LineHeight(2);

                column.Spacing(10);

                // Combined Say to Speaker
                column.Item().Text("Combined Say to Speaker").FontSize(14).Bold().Underline();
                column.Item().Text(eval.CombinedSayToSpeaker ?? "N/A")
                    .FontSize(12).LineHeight(2);

                // Final Rating
                column.Item().Text($"Final Rating: {eval.FinalRating:F2}")
                    .FontSize(16).Bold().FontColor(Colors.Green.Medium);
            });
        }
    }

    private static IContainer CellStyle(IContainer container)
    {
        return container.Border(1).BorderColor(Colors.Grey.Lighten2).Padding(5).AlignCenter();
    }
}