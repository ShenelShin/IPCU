using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using IPCU.Models;

namespace IPCU.Services
{
    public interface IReportService
    {
        byte[] GenerateICRAReport(ICRA icra);
        byte[] GenerateTCSkillsChecklistReport(TCSkillsChecklist checklist);
        byte[] GeneratePostConstructionReport(PostConstruction postConstruction);
        byte[] GenerateCombinedReport(ICRA icra, List<TCSkillsChecklist> checklists, PostConstruction postConstruction);
    }

    public class ReportService : IReportService
    {
        public byte[] GenerateICRAReport(ICRA icra)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("ICRA Report");

                // Set basic styling
                worksheet.Cells.Style.Font.Name = "Calibri";
                worksheet.Cells.Style.Font.Size = 11;

                // Header
                worksheet.Cells["A1"].Value = "Infection Control Risk Assessment (ICRA) Report";
                worksheet.Cells["A1:D1"].Merge = true;
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["A1"].Style.Font.Size = 16;
                worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Basic Information
                worksheet.Cells["A3"].Value = "Project Reference Number:";
                worksheet.Cells["B3"].Value = icra.ProjectReferenceNumber;
                worksheet.Cells["A4"].Value = "Project Name:";
                worksheet.Cells["B4"].Value = icra.ProjectNameAndDescription;
                worksheet.Cells["A5"].Value = "Location:";
                worksheet.Cells["B5"].Value = icra.SpecificSiteOfActivity;
                worksheet.Cells["A6"].Value = "Contractor:";
                worksheet.Cells["B6"].Value = icra.ContractorRepresentativeName;
                worksheet.Cells["A7"].Value = "Contact:";
                worksheet.Cells["B7"].Value = icra.TelephoneOrMobileNumber;
                worksheet.Cells["A8"].Value = "Email:";
                worksheet.Cells["B8"].Value = icra.ConstructionEmail;

                // Risk Assessment
                worksheet.Cells["A10"].Value = "Risk Assessment";
                worksheet.Cells["A10:B10"].Merge = true;
                worksheet.Cells["A10"].Style.Font.Bold = true;
                worksheet.Cells["A10"].Style.Font.Size = 14;
                worksheet.Cells["A10"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A10"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                worksheet.Cells["A11"].Value = "Patient Risk Group:";
                worksheet.Cells["B11"].Value = icra.PatientRiskGroup;
                worksheet.Cells["A12"].Value = "Preventive Measures:";
                worksheet.Cells["B12"].Value = icra.PreventiveMeasures;
                worksheet.Cells["A13"].Value = "Construction Type:";
                worksheet.Cells["B13"].Value = icra.ConstructionType;

                // Adjacent Areas
                worksheet.Cells["A15"].Value = "Adjacent Areas Risk Assessment";
                worksheet.Cells["A15:F15"].Merge = true;
                worksheet.Cells["A15"].Style.Font.Bold = true;
                worksheet.Cells["A15"].Style.Font.Size = 14;
                worksheet.Cells["A15"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A15"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                // Create a table for adjacent areas
                worksheet.Cells["A16"].Value = "Location";
                worksheet.Cells["B16"].Value = "Risk Group";
                worksheet.Cells["C16"].Value = "Local Number";
                worksheet.Cells["D16"].Value = "Noise";
                worksheet.Cells["E16"].Value = "Vibration";
                worksheet.Cells["F16"].Value = "Dust";
                // Add more columns as needed

                int row = 17;
                if (!string.IsNullOrEmpty(icra.RiskGroup_Below))
                {
                    worksheet.Cells[$"A{row}"].Value = "Below";
                    worksheet.Cells[$"B{row}"].Value = icra.RiskGroup_Below;
                    worksheet.Cells[$"C{row}"].Value = icra.LocalNumber_Below;
                    worksheet.Cells[$"D{row}"].Value = icra.Below_Noise ? "Yes" : "No";
                    worksheet.Cells[$"E{row}"].Value = icra.Below_Vibration ? "Yes" : "No";
                    worksheet.Cells[$"F{row}"].Value = icra.Below_Dust ? "Yes" : "No";
                    row++;
                }

                // Repeat for Above, Lateral, Behind, Front...

                // Signatures
                worksheet.Cells[$"A{row}"].Value = "Signatures";
                worksheet.Cells[$"A{row}:F{row}"].Merge = true;
                worksheet.Cells[$"A{row}"].Style.Font.Bold = true;
                worksheet.Cells[$"A{row}"].Style.Font.Size = 14;
                worksheet.Cells[$"A{row}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[$"A{row}"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                row++;

                worksheet.Cells[$"A{row}"].Value = "Contractor:";
                worksheet.Cells[$"B{row}"].Value = icra.ContractorSign;
                row++;
                worksheet.Cells[$"A{row}"].Value = "Engineering:";
                worksheet.Cells[$"B{row}"].Value = icra.EngineeringSign;
                row++;
                worksheet.Cells[$"A{row}"].Value = "ICP:";
                worksheet.Cells[$"B{row}"].Value = icra.ICPSign;
                row++;
                worksheet.Cells[$"A{row}"].Value = "Unit/Area Rep:";
                worksheet.Cells[$"B{row}"].Value = icra.UnitAreaRep;

                // Auto-fit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                return package.GetAsByteArray();
            }
        }

        public byte[] GenerateTCSkillsChecklistReport(TCSkillsChecklist checklist)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("TC Skills Checklist");

                // Set basic styling
                worksheet.Cells.Style.Font.Name = "Calibri";
                worksheet.Cells.Style.Font.Size = 11;

                // Header
                worksheet.Cells["A1"].Value = "Terminal Cleaning Skills Checklist";
                worksheet.Cells["A1:D1"].Merge = true;
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["A1"].Style.Font.Size = 16;
                worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Basic Information
                worksheet.Cells["A3"].Value = "Area:";
                worksheet.Cells["B3"].Value = checklist.Area;
                worksheet.Cells["A4"].Value = "Observer Name:";
                worksheet.Cells["B4"].Value = checklist.ObserverName;
                worksheet.Cells["A5"].Value = "Date:";
                worksheet.Cells["B5"].Value = checklist.Date.ToString("yyyy-MM-dd");
                worksheet.Cells["A6"].Value = "Date of Observation:";
                worksheet.Cells["B6"].Value = checklist.DateOfObservation.ToString("yyyy-MM-dd");

                // Sections
                AddChecklistSection(worksheet, 8, "1. PREPARATION AND SETUP", new List<string>
                {
                    "Prepare equipment and cleaning cart with everything needed",
                    "Prepare properly diluted cleaning solution (1:100)",
                    "Don appropriate attire and Personal and Protective Equipment (PPE)"
                }, new List<bool>
                {
                    checklist.IsEquipmentAndCartPrepared,
                    checklist.IsCleaningSolutionPrepared,
                    checklist.IsProperAttireAndPPEWorn
                });

                AddChecklistSection(worksheet, 13, "2. BASIC PROCEDURES", new List<string>
                {
                    "Perform hand hygiene and don gloves before entering the room",
                    "Be aware of signage that indicates special precaution"
                }, new List<bool>
                {
                    checklist.IsHandHygieneAndGlovesDone,
                    checklist.IsSignageChecked
                });

                AddChecklistSection(worksheet, 17, "3. PATIENT ROOM CLEANING PROCEDURES", new List<string>
                {
                    "If with spills, soak spill using 1:10 dilution of hypochlorite solution and leave in contact for 2–5 minutes",
                    "Clean walls using disposable cloths",
                    "Wipe door frame",
                    // Add all other items from section 3
                }, new List<bool>
                {
                    checklist.IsSpillSoakedWithSolution,
                    checklist.IsWallsCleaned,
                    checklist.IsDoorFrameWiped,
                    // Add all other bool values from section 3
                });

                // Add other sections similarly...

                // Notes
                int notesRow = 40; // Adjust based on your content
                worksheet.Cells[$"A{notesRow}"].Value = "Pre-cleaning areas/items:";
                worksheet.Cells[$"B{notesRow}"].Value = checklist.PreCleaningItems;
                notesRow++;
                worksheet.Cells[$"A{notesRow}"].Value = "Post-cleaning:";
                worksheet.Cells[$"B{notesRow}"].Value = checklist.PostCleaningItems;
                notesRow++;
                worksheet.Cells[$"A{notesRow}"].Value = "Recommendations/Actions Taken:";
                worksheet.Cells[$"B{notesRow}"].Value = checklist.RecommendationsOrActions;
                notesRow++;

                // Signature
                worksheet.Cells[$"A{notesRow}"].Value = "Unit/Area Staff (Name and Signature):";
                worksheet.Cells[$"B{notesRow}"].Value = checklist.UnitAreaStaffSignature;

                // Auto-fit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                return package.GetAsByteArray();
            }
        }

        public byte[] GeneratePostConstructionReport(PostConstruction postConstruction)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Post Construction");

                // Set basic styling
                worksheet.Cells.Style.Font.Name = "Calibri";
                worksheet.Cells.Style.Font.Size = 11;

                // Header
                worksheet.Cells["A1"].Value = "Post Construction Report";
                worksheet.Cells["A1:D1"].Merge = true;
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["A1"].Style.Font.Size = 16;
                worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                // Basic Information
                worksheet.Cells["A3"].Value = "Project Reference Number:";
                worksheet.Cells["B3"].Value = postConstruction.ProjectReferenceNumber;
                worksheet.Cells["A4"].Value = "Project Name:";
                worksheet.Cells["B4"].Value = postConstruction.ProjectNameAndDescription;
                worksheet.Cells["A5"].Value = "Location:";
                worksheet.Cells["B5"].Value = postConstruction.SpecificSiteOfActivity;
                worksheet.Cells["A6"].Value = "Start Date:";
                worksheet.Cells["B6"].Value = postConstruction.ProjectStartDate?.ToString("yyyy-MM-dd");
                worksheet.Cells["A7"].Value = "Estimated Duration:";
                worksheet.Cells["B7"].Value = postConstruction.EstimatedDuration;
                worksheet.Cells["A8"].Value = "Date Completed:";
                worksheet.Cells["B8"].Value = postConstruction.DateCompleted?.ToString("yyyy-MM-dd");

                // Post Construction Cleaning
                worksheet.Cells["A10"].Value = "Post Construction Cleaning";
                worksheet.Cells["A10:B10"].Merge = true;
                worksheet.Cells["A10"].Style.Font.Bold = true;
                worksheet.Cells["A10"].Style.Font.Size = 14;
                worksheet.Cells["A10"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A10"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                worksheet.Cells["A11"].Value = "Before Hoarding:";
                worksheet.Cells["B11"].Value = postConstruction.BeforeHoarding;
                worksheet.Cells["C11"].Value = postConstruction.BeforeHoardingDC;
                worksheet.Cells["A12"].Value = "Facility Based:";
                worksheet.Cells["B12"].Value = postConstruction.FacilityBased;
                worksheet.Cells["C12"].Value = postConstruction.FacilityBasedDC;
                // Add all other cleaning items...

                // Finishes
                worksheet.Cells["A20"].Value = "Finishes";
                worksheet.Cells["A20:B20"].Merge = true;
                worksheet.Cells["A20"].Style.Font.Bold = true;
                worksheet.Cells["A20"].Style.Font.Size = 14;
                worksheet.Cells["A20"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A20"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                worksheet.Cells["A21"].Value = "Area Is:";
                worksheet.Cells["B21"].Value = postConstruction.AreaIs;
                worksheet.Cells["C21"].Value = postConstruction.AreaIsDC;
                // Add all other finishes items...

                // Infrastructure
                worksheet.Cells["A30"].Value = "Infrastructure";
                worksheet.Cells["A30:B30"].Merge = true;
                worksheet.Cells["A30"].Style.Font.Bold = true;
                worksheet.Cells["A30"].Style.Font.Size = 14;
                worksheet.Cells["A30"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells["A30"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

                worksheet.Cells["A31"].Value = "If Plumbing has been Affected:";
                worksheet.Cells["B31"].Value = postConstruction.IfPlumbinghasbeenAffected;
                worksheet.Cells["C31"].Value = postConstruction.IfPlumbinghasbeenAffectedDC;
                // Add all other infrastructure items...

                // Signatures
                int sigRow = 50; // Adjust based on your content
                worksheet.Cells[$"A{sigRow}"].Value = "Signatures";
                worksheet.Cells[$"A{sigRow}:B{sigRow}"].Merge = true;
                worksheet.Cells[$"A{sigRow}"].Style.Font.Bold = true;
                worksheet.Cells[$"A{sigRow}"].Style.Font.Size = 14;
                worksheet.Cells[$"A{sigRow}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[$"A{sigRow}"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                sigRow++;

                worksheet.Cells[$"A{sigRow}"].Value = "Contractor:";
                worksheet.Cells[$"B{sigRow}"].Value = postConstruction.ContractorSign;
                sigRow++;
                worksheet.Cells[$"A{sigRow}"].Value = "Engineering:";
                worksheet.Cells[$"B{sigRow}"].Value = postConstruction.EngineeringSign;
                sigRow++;
                worksheet.Cells[$"A{sigRow}"].Value = "ICP:";
                worksheet.Cells[$"B{sigRow}"].Value = postConstruction.ICPSign;
                sigRow++;
                worksheet.Cells[$"A{sigRow}"].Value = "Unit/Area Rep:";
                worksheet.Cells[$"B{sigRow}"].Value = postConstruction.UnitAreaRep;

                // Auto-fit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                return package.GetAsByteArray();
            }
        }

        public byte[] GenerateCombinedReport(ICRA icra, List<TCSkillsChecklist> checklists, PostConstruction postConstruction)
        {
            using (var package = new ExcelPackage())
            {
                // Generate ICRA report as first sheet
                var icraWorksheet = package.Workbook.Worksheets.Add("ICRA");
                GenerateICRAWorksheet(icraWorksheet, icra);

                // Generate TCSkillsChecklist reports as subsequent sheets
                for (int i = 0; i < checklists.Count; i++)
                {
                    var checklistWorksheet = package.Workbook.Worksheets.Add($"Cleaning Checklist {i + 1}");
                    GenerateChecklistWorksheet(checklistWorksheet, checklists[i]);
                }

                // Generate PostConstruction report if exists
                if (postConstruction != null)
                {
                    var postConstructionWorksheet = package.Workbook.Worksheets.Add("Post Construction");
                    GeneratePostConstructionWorksheet(postConstructionWorksheet, postConstruction);
                }

                return package.GetAsByteArray();
            }
        }

        private void GenerateICRAWorksheet(ExcelWorksheet worksheet, ICRA icra)
        {
            // Same content as GenerateICRAReport but without creating a new package
            // Copy the implementation from GenerateICRAReport method
        }

        private void GenerateChecklistWorksheet(ExcelWorksheet worksheet, TCSkillsChecklist checklist)
        {
            // Same content as GenerateTCSkillsChecklistReport but without creating a new package
            // Copy the implementation from GenerateTCSkillsChecklistReport method
        }

        private void GeneratePostConstructionWorksheet(ExcelWorksheet worksheet, PostConstruction postConstruction)
        {
            // Same content as GeneratePostConstructionReport but without creating a new package
            // Copy the implementation from GeneratePostConstructionReport method
        }

        private void AddChecklistSection(ExcelWorksheet worksheet, int startRow, string sectionTitle, List<string> items, List<bool> checks)
        {
            worksheet.Cells[$"A{startRow}"].Value = sectionTitle;
            worksheet.Cells[$"A{startRow}:B{startRow}"].Merge = true;
            worksheet.Cells[$"A{startRow}"].Style.Font.Bold = true;
            worksheet.Cells[$"A{startRow}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[$"A{startRow}"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

            for (int i = 0; i < items.Count; i++)
            {
                worksheet.Cells[$"A{startRow + i + 1}"].Value = items[i];
                worksheet.Cells[$"B{startRow + i + 1}"].Value = checks[i] ? "✓" : "✗";
                worksheet.Cells[$"B{startRow + i + 1}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }
        }
    }
}