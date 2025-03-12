namespace IPCU.Services
{
    using QuestPDF.Fluent;
    using QuestPDF.Helpers;
    using QuestPDF.Infrastructure;
    using IPCU.Models;
    using System;
    using static QuestPDF.Helpers.Colors;

    public class TrainingEvaluationPdfService
    {
        private static byte[] LoadLogo()
        {
            return File.ReadAllBytes("wwwroot/images/nktilogo.png");
        }

        public static byte[] GeneratePdf(TrainingEvaluation training)


        {

            return Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Size(PageSizes.A4);
                  
                    page.Header().Element(header =>
                    {
                        header.Border(1) // Adds border
                              .Padding(10) // Padding inside the box
                              .Background(White) 
                              .Row(row =>
                              {
                                  // Load and add the logo
                                  byte[] logoBytes = LoadLogo();
                                  if (logoBytes != null && logoBytes.Length > 0)
                                  {
                                      row.ConstantItem(70).Element(container =>
                                      {
                                          container.Image(logoBytes)
                                                  .FitWidth(); // ✅ Ensures it scales properly within the width
                                      });
                                  }

                                  // Organization Details
                                  row.RelativeItem().Column(col =>
                                  {
                                      col.Spacing(0);

                                      col.Item()
                                         .AlignCenter()
                                         .Text("NATIONAL KIDNEY AND TRANSPLANT INSTITUTE")
                                         .Bold()
                                         .FontSize(12)
                                         .FontColor(Colors.Blue.Darken4);

                                      col.Item().AlignCenter().Text("East Avenue, Quezon City").FontSize(10);

                                      col.Item()
                                         .PaddingTop(10)
                                         .AlignCenter()
                                         .Text("INFECTION PREVENTION AND CONTROL UNIT")
                                         .Bold()
                                         .FontSize(12)
                                         .FontColor(Colors.Blue.Darken4);

                                      col.Item().Element(container =>
                                          container.PaddingVertical(2)
                                                   .AlignCenter()
                                                   .Width(400)
                                      ).LineHorizontal(0.5f);

                                      col.Item()
                                          .AlignCenter()
                                          .Text("TRAINING EVALUATION SUMMARY")
                                          .Bold()
                                          .FontSize(12)
                                          .FontColor(Colors.Blue.Darken4);
                                  
                    

                                                      });
                              });
                    });





                    // ===== Training Details & Post-Test Evaluation Grade + Final Rating =====
                    page.Content().Column(col =>
                    {
                        col.Spacing(5);

                        // Add margin before Training Details
                        col.Item().PaddingTop(15) // ✅ Adds spacing between header and training details
                            .Row(row =>
                            {
                                row.RelativeItem().Border(1).Padding(3).Background(Grey.Lighten4).Column(column =>
                                {
                                    // Section Title
                                    column.Item().Background(Grey.Lighten3).Padding(5).AlignCenter()
                                        .Text("TRAINING DETAILS").Bold().FontSize(10);

                              
                                    column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(1); // Label Column
                                        columns.RelativeColumn(2); // Value Column
                                    });

                                    void AddRow(string label, string value, bool addUnderline = true)
                                    {
                                        table.Cell().PaddingVertical(0).Text(label).Bold();
                                        table.Cell().PaddingVertical(0).Text(value);

                                        // Only add underline if addUnderline is true
                                        if (addUnderline)
                                        {
                                            table.Cell().ColumnSpan(2).Element(e =>
                                                e.BorderBottom(1).PaddingBottom(0) // Thin bottom border as underline
                                            );
                                        }
                                    }

                                    // Adding rows with training details
                                    AddRow("Title:", training.Title);
                                    AddRow("Date:", training.DateOfTraining.ToString("dd MMM yyyy"));
                                    AddRow("Venue:", training.Venue);
                                    AddRow("Methodology:", training.TrainingMethodology); // No underline for the last row
                                    AddRow("Professional Category:", training.ProfessionalCategory);
                                    AddRow("SME/Lecturer:", training.SMELecturer, false); // NEWLY ADDED LINE
                                });
                            });


                            // Post-Test Evaluation Grade and Final Rating Section
                            row.RelativeItem(0.4f).Border(1).Padding(3).Background(Grey.Lighten3).Column(innerCol =>
                            {
                                innerCol.Item().PaddingTop(7).Text("Post Test Evaluation Grade").Bold().AlignCenter();
                                innerCol.Item().Text(training.PostTestEvaluationGrade.ToString()).FontSize(10).AlignCenter();

                                innerCol.Item().PaddingVertical(5).LineHorizontal(0.5f); // Adds a horizontal line

                                innerCol.Item().Text("Final Rating").Bold().AlignCenter();
                                innerCol.Item().Text(training.FinalRating.ToString()).FontSize(10).AlignCenter();
                            });
                        });

                        // ===== Total Participants =====
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Border(1).Padding(3).Column(column =>
                            {
                                column.Item().Background(Grey.Lighten3).Padding(2).AlignCenter().Text("PARTICIPANTS").Bold().FontSize(10);
                                AddRow(column, "Male Participants:", training.TotalParticipantsMale);
                                AddRow(column, "Female Participants:", training.TotalParticipantsFemale);
                                column.Item().LineHorizontal(0.5f);
                                AddRow(column, "Total Participants:", training.TotalParticipantsMale + training.TotalParticipantsFemale, true);
                            });
                        });

                        // ===== Program Facilitation & Trainer Characteristics =====
                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Border(1).Padding(3).Column(column =>
                            {
                                AddSectionTitle(column, "PROGRAM FACILITATION");
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(); // Title Column
                                        columns.ConstantColumn(50); // Rating Column
                                    });

                                    AddRow(table, "Flow followed:", training.FlowFollowed);
                                    AddRow(table, "Rules established:", training.RulesEstablished);
                                    AddRow(table, "Initiate discussion:", training.InitiateDiscussion);
                                    AddRow(table, "Technical capability:", training.TechnicalCapability);
                                });
                            });

                            row.RelativeItem().Border(1).Padding(3).Column(column =>
                            {
                                AddSectionTitle(column, "TRAINER CHARACTERISTICS");
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(); // Title Column
                                        columns.ConstantColumn(50); // Rating Column
                                    });

                                    AddRow(table, "Preparedness:", training.Preparedness);
                                    AddRow(table, "Teaching Personality:", training.TeachingPersonality);
                                    AddRow(table, "Establish Rapport:", training.EstablishRapport);
                                    AddRow(table, "Respect for Participants:", training.RespectForParticipants);
                                    AddRow(table, "Voice Personality:", training.VoicePersonality);
                                    AddRow(table, "Time Management:", training.TimeManagement);
                                });
                            });
                        });

                        col.Item().Row(row =>
                        {
                            row.RelativeItem().Border(1).Padding(3).Column(column =>
                            {
                                column.Spacing(0); // Remove extra spacing
                                AddSectionTitle(column, "MODULE EVALUATION");
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn();
                                        columns.ConstantColumn(50);
                                    });

                                    // Modify existing function call to remove padding
                                    AddRowWithVerticalLine(table, "Content Organization:", training.ContentOrganization, removePadding: true);
                                    AddRowWithVerticalLine(table, "Objective Stated:", training.ObjectiveStated, removePadding: true);
                                    AddRowWithVerticalLine(table, "Content Quality:", training.ContentQuality, removePadding: true);
                                    AddRowWithVerticalLine(table, "Flow of Topic:", training.FlowOfTopic, removePadding: true);
                                    AddRowWithVerticalLine(table, "Relevance of Topic:", training.RelevanceOfTopic, removePadding: true);
                                    AddRowWithVerticalLine(table, "Learning Activities:", training.LearningActivities, removePadding: true);
                                    AddRowWithVerticalLine(table, "Visual Aids:", training.VisualAids, removePadding: true);
                                });
                            });

                            row.RelativeItem().Border(1).Padding(3).Column(column =>
                            {
                                column.Spacing(0); // Remove extra spacing
                                AddSectionTitle(column, "MASTERY OF SUBJECT MATTER");
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn();
                                        columns.ConstantColumn(50);
                                    });

                                    // Modify existing function call to remove padding
                                    AddRowWithVerticalLine(table, "Present Knowledge:", training.PresentKnowledge, removePadding: true);
                                    AddRowWithVerticalLine(table, "Balance Principles:", training.BalancePrinciples, removePadding: true);
                                    AddRowWithVerticalLine(table, "Address Clarifications:", training.AddressClarifications, removePadding: true);
                                });
                            });
                        });



                        col.Item().PaddingTop(10).Text("Please suggest ways in which the speaker can improve and be more effective in the delivery of the topics:").Bold().FontSize(10); // NEWLY ADDED LINE
                        col.Item().Border(1).Padding(3).Text(training.SayToSpeaker ?? "N/A"); // NEWLY ADDED LINE
                        // ===== Suggestions for Improvement =====
                        col.Item().Text("What is the best thing you can say about the speaker?").Bold().FontSize(10);
                        col.Item().Border(1).Padding(3).Text(training.SuggestionsForImprovement ?? "N/A");
                    });

                    page.Footer().AlignCenter().Text($"Generated on {DateTime.Now:dd MMM yyyy}").FontSize(8).Italic();
                });
            }).GeneratePdf();
        }

        // === Utility Methods ===

        // Method for adding a row in tables
        private static void AddRow(TableDescriptor table, string label, object value)
        {
        
            table.Cell().Element(e => e.PaddingVertical(1)).Text(label);

 
            table.Cell().BorderLeft(1).Element(e => e.AlignCenter().PaddingRight(10))
                .Text(value.ToString()).Bold();
        }
        private static void AddRowWithVerticalLine(TableDescriptor table, string label, object value, bool removePadding = false)
        {
            var paddingValue = removePadding ? 0 : 2;

         
            table.Cell().Element(e => e.PaddingVertical(paddingValue)).Text(label);

           
            table.Cell().BorderLeft(1).Element(e => e.AlignCenter().PaddingVertical(paddingValue))
                .Text(value.ToString()).Bold();

        }



        // Overloaded method for adding a row in columns
        private static void AddRow(ColumnDescriptor column, string label, object value, bool bold = false)
        {
            column.Item().Row(row =>
            {
                row.RelativeItem().Text(label);
                var textElement = row.AutoItem().PaddingLeft(5).PaddingRight(50).Text(value.ToString());
                if (bold) textElement.Bold();
            });
        }

        // Method to add section titles with "Rating" label
        private static void AddSectionTitle(ColumnDescriptor column, string title)
        {
            column.Item().Element(e => e.Background(Grey.Lighten3).Padding(2))
                .Row(r =>
                {
                    r.RelativeItem().Text(title).Bold().FontSize(10);
                    r.ConstantItem(50).Background(Colors.Teal.Lighten4).Padding(5).Text("RATING").Bold().FontSize(10);
                });
        }
    }
}
