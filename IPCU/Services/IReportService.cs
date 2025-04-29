using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Drawing;
using IPCU.Models;

namespace IPCU.Services
{
    public interface IReportService
    {
        byte[] GenerateICRAReport(ICRA icra);
        //byte[] GenerateTCSkillsChecklistReport(TCSkillsChecklist checklist);
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
                worksheet.Cells["A3"].Value = "Project Reference:";
                worksheet.Cells["B3"].Value = checklist.ProjectReferenceNumber;
                worksheet.Cells["A4"].Value = "Project Name:";
                worksheet.Cells["B4"].Value = checklist.ProjectNameAndDescription;
                worksheet.Cells["A5"].Value = "Location:";
                worksheet.Cells["B5"].Value = checklist.SpecificSiteOfActivity;
                worksheet.Cells["A6"].Value = "Contractor:";
                worksheet.Cells["B6"].Value = checklist.ContractorRepresentativeName;
                worksheet.Cells["A7"].Value = "Contact:";
                worksheet.Cells["B7"].Value = checklist.TelephoneOrMobileNumber;
                worksheet.Cells["A8"].Value = "Start Date:";
                worksheet.Cells["B8"].Value = checklist.ProjectStartDate?.ToString("yyyy-MM-dd");
                worksheet.Cells["A9"].Value = "Duration:";
                worksheet.Cells["B9"].Value = checklist.EstimatedDuration;

                // Sections
                // Barrier Section
                AddChecklistSection(worksheet, 11, "1. BARRIER ASSESSMENT", new List<string>
        {
            "ICRA Barrier",
            "Doors Sealed",
            "Floor Clean",
            "Walk Off Mats",
            "Tape Adhering"
        }, new List<string>
        {
            checklist.BarrierICRA,
            checklist.BarrierDoorsSealedSelection,
            checklist.BarrierFloorCleanSelection,
            checklist.BarrierWalkOffMatsSelection,
            checklist.BarrierTapeAdheringSelection
        }, new List<string>
        {
            checklist.BarrierICRAComments,
            checklist.BarrierDoorsSealedComments,
            checklist.BarrierFloorCleanComments,
            checklist.BarrierWalkOffMatsComments,
            checklist.BarrierTapeAdheringComments
        });

                // Air Handling Section
                AddChecklistSection(worksheet, 18, "2. AIR HANDLING ASSESSMENT", new List<string>
        {
            "Windows Closed Behind Barrier",
            "Negative Air Monitored",
            "Air Handling Unit Running",
            "Maintenance Label Visible",
            "Air Exhausted To Appropriate Area"
        }, new List<string>
        {
            checklist.AirhandlingWindowsClosedBehindBarrierSelection,
            checklist.AirhandlingNegativeAirMonitoredSelection,
            checklist.AirHandlingUnitRunningSelection,
            checklist.AirhandlingMaintenanceLabelVisibleSelection,
            checklist.AirhandlingAirExhaustedToAppropriateAreaSelection
        }, new List<string>
        {
            checklist.AirhandlingWindowsClosedBehindBarrierComments,
            checklist.AirhandlingNegativeAirMonitoredComments,
            checklist.AirHandlingUnitRunningComments,
            checklist.AirhandlingMaintenanceLabelVisibleComments,
            checklist.AirhandlingAirExhaustedToAppropriateAreaComments
        });

                // Project Area Section
                AddChecklistSection(worksheet, 25, "3. PROJECT AREA ASSESSMENT", new List<string>
        {
            "HEPA Filtered Vacuum On Jobsite",
            "Debris Removed In Covered Container Daily",
            "Designated Construction Route Or Map Posted",
            "Trash In Appropriate Container",
            "Routine Cleaning Done On Site",
            "Air Vents Sealed Or Ductwork Capped"
        }, new List<string>
        {
            checklist.ProjectareaHEPAFilteredVacuumOnJobsiteSelection,
            checklist.ProjectareaDebrisRemovedInCoveredContainerDailySelection,
            checklist.ProjectareaDesignatedConstructionRouteOrMapPostedSelection,
            checklist.ProjectareaTrashInAppropriateContainerSelection,
            checklist.ProjectareaRoutineCleaningDoneOnSiteSelection,
            checklist.ProjectareaAirVentsSealedOrDuctworkCappedSelection
        }, new List<string>
        {
            checklist.ProjectareaHEPAFilteredVacuumOnJobsiteComments,
            checklist.ProjectareaDebrisRemovedInCoveredContainerDailyComments,
            checklist.ProjectareaDesignatedConstructionRouteOrMapPostedComments,
            checklist.ProjectareaTrashInAppropriateContainerComments,
            checklist.ProjectareaRoutineCleaningDoneOnSiteComments,
            checklist.ProjectareaAirVentsSealedOrDuctworkCappedComments
        });

                // Traffic Control Section
                AddChecklistSection(worksheet, 33, "4. TRAFFIC CONTROL ASSESSMENT", new List<string>
        {
            "Restricted To Construction Workers",
            "Doors And Exits Free Of Debris"
        }, new List<string>
        {
            checklist.TrafficcontrolRestrictedToConstructionWorkersSelection,
            checklist.TrafficcontrolDoorsAndExitsFreeOfDebrisSelection
        }, new List<string>
        {
            checklist.TrafficcontrolRestrictedToConstructionWorkersComments,
            checklist.TrafficcontrolDoorsAndExitsFreeOfDebrisComments
        });

                // Dress Code Section
                AddChecklistSection(worksheet, 37, "5. DRESS CODE ASSESSMENT", new List<string>
        {
            "Protective Clothing Worn",
            "Workers Clothing Clean Upon Exiting"
        }, new List<string>
        {
            checklist.DresscodeProtectiveClothingWornSelection,
            checklist.DresscodeWorkersClothingCleanUponExitingSelection
        }, new List<string>
        {
            checklist.DresscodeProtectiveClothingWornComments,
            checklist.DresscodeWorkersClothingCleanUponExitingComments
        });

                // Signatures
                int sigRow = 41;
                worksheet.Cells[$"A{sigRow}"].Value = "Signatures";
                worksheet.Cells[$"A{sigRow}:D{sigRow}"].Merge = true;
                worksheet.Cells[$"A{sigRow}"].Style.Font.Bold = true;
                worksheet.Cells[$"A{sigRow}"].Style.Font.Size = 14;
                worksheet.Cells[$"A{sigRow}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                worksheet.Cells[$"A{sigRow}"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                sigRow++;

                worksheet.Cells[$"A{sigRow}"].Value = "Contractor:";
                worksheet.Cells[$"B{sigRow}"].Value = checklist.ContractorSign;
                sigRow++;
                worksheet.Cells[$"A{sigRow}"].Value = "Engineering:";
                worksheet.Cells[$"B{sigRow}"].Value = checklist.EngineeringSign;
                sigRow++;
                worksheet.Cells[$"A{sigRow}"].Value = "ICP:";
                worksheet.Cells[$"B{sigRow}"].Value = checklist.ICPSign;
                sigRow++;
                worksheet.Cells[$"A{sigRow}"].Value = "Unit/Area Rep:";
                worksheet.Cells[$"B{sigRow}"].Value = checklist.UnitAreaRep;

                // Auto-fit columns
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                return package.GetAsByteArray();
            }
        }

        // Updated method to handle string selections and comments
        private void AddChecklistSection(ExcelWorksheet worksheet, int startRow, string sectionTitle, List<string> items, List<string> selections, List<string> comments)
        {
            worksheet.Cells[$"A{startRow}"].Value = sectionTitle;
            worksheet.Cells[$"A{startRow}:D{startRow}"].Merge = true;
            worksheet.Cells[$"A{startRow}"].Style.Font.Bold = true;
            worksheet.Cells[$"A{startRow}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[$"A{startRow}"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);

            // Add headers
            worksheet.Cells[$"A{startRow + 1}"].Value = "Item";
            worksheet.Cells[$"B{startRow + 1}"].Value = "Status";
            worksheet.Cells[$"C{startRow + 1}:D{startRow + 1}"].Merge = true;
            worksheet.Cells[$"C{startRow + 1}"].Value = "Comments";

            worksheet.Cells[$"A{startRow + 1}:D{startRow + 1}"].Style.Font.Bold = true;
            worksheet.Cells[$"A{startRow + 1}:D{startRow + 1}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[$"A{startRow + 1}:D{startRow + 1}"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);

            for (int i = 0; i < items.Count; i++)
            {
                worksheet.Cells[$"A{startRow + i + 2}"].Value = items[i];
                worksheet.Cells[$"B{startRow + i + 2}"].Value = selections[i];
                worksheet.Cells[$"C{startRow + i + 2}:D{startRow + i + 2}"].Merge = true;
                worksheet.Cells[$"C{startRow + i + 2}"].Value = comments[i];

                worksheet.Cells[$"B{startRow + i + 2}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
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
            // Set basic styling
            worksheet.Cells.Style.Font.Name = "Calibri";
            worksheet.Cells.Style.Font.Size = 11;

            // Header
            worksheet.Cells["A1"].Value = "Infection Control Risk Assessment (ICRA) Report";
            worksheet.Cells["A1:F1"].Merge = true;
            worksheet.Cells["A1"].Style.Font.Bold = true;
            worksheet.Cells["A1"].Style.Font.Size = 16;
            worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Basic Information Section
            worksheet.Cells["A3"].Value = "Project Reference Number:";
            worksheet.Cells["B3"].Value = icra.ProjectReferenceNumber;
            worksheet.Cells["D3"].Value = "Start Date:";
            worksheet.Cells["E3"].Value = icra.ProjectStartDate?.ToString("yyyy-MM-dd");

            worksheet.Cells["A4"].Value = "Project Name:";
            worksheet.Cells["B4"].Value = icra.ProjectNameAndDescription;
            worksheet.Cells["D4"].Value = "Duration:";
            worksheet.Cells["E4"].Value = icra.EstimatedDuration;

            worksheet.Cells["A5"].Value = "Location:";
            worksheet.Cells["B5"].Value = icra.SpecificSiteOfActivity;
            worksheet.Cells["D5"].Value = "Email:";
            worksheet.Cells["E5"].Value = icra.ConstructionEmail;

            worksheet.Cells["A6"].Value = "Contractor:";
            worksheet.Cells["B6"].Value = icra.ContractorRepresentativeName;

            worksheet.Cells["A7"].Value = "Contact:";
            worksheet.Cells["B7"].Value = icra.TelephoneOrMobileNumber;

            // Add border and background to basic info
            var infoRange = worksheet.Cells["A3:E7"];
            infoRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            infoRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            infoRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            infoRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Risk Assessment Section
            worksheet.Cells["A9"].Value = "Risk Assessment";
            worksheet.Cells["A9:F9"].Merge = true;
            worksheet.Cells["A9"].Style.Font.Bold = true;
            worksheet.Cells["A9"].Style.Font.Size = 14;
            worksheet.Cells["A9"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells["A9"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            worksheet.Cells["A9"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            worksheet.Cells["A10"].Value = "Patient Risk Group:";
            worksheet.Cells["B10"].Value = icra.PatientRiskGroup;
            worksheet.Cells["A11"].Value = "Preventive Measures:";
            worksheet.Cells["B11"].Value = icra.PreventiveMeasures;
            worksheet.Cells["A12"].Value = "Construction Type:";
            worksheet.Cells["B12"].Value = icra.ConstructionType;
            worksheet.Cells["A13"].Value = "Scope of Work:";
            worksheet.Cells["B13"].Value = icra.ScopeOfWork;

            var riskRange = worksheet.Cells["A10:F13"];
            riskRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            riskRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            riskRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            riskRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Adjacent Areas Risk Assessment Section
            worksheet.Cells["A15"].Value = "Adjacent Areas Risk Assessment";
            worksheet.Cells["A15:F15"].Merge = true;
            worksheet.Cells["A15"].Style.Font.Bold = true;
            worksheet.Cells["A15"].Style.Font.Size = 14;
            worksheet.Cells["A15"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells["A15"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            worksheet.Cells["A15"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Headers for adjacent areas table
            worksheet.Cells["A16"].Value = "Location";
            worksheet.Cells["B16"].Value = "Risk Group";
            worksheet.Cells["C16"].Value = "Local Number";
            worksheet.Cells["D16"].Value = "Noise";
            worksheet.Cells["E16"].Value = "Vibration";
            worksheet.Cells["F16"].Value = "Dust";

            // Style the header row
            var headerRange = worksheet.Cells["A16:F16"];
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            headerRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            headerRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            headerRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            headerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            int row = 17;
            // Below
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

            // Above
            if (!string.IsNullOrEmpty(icra.RiskGroup_Above))
            {
                worksheet.Cells[$"A{row}"].Value = "Above";
                worksheet.Cells[$"B{row}"].Value = icra.RiskGroup_Above;
                worksheet.Cells[$"C{row}"].Value = icra.LocalNumber_Above;
                worksheet.Cells[$"D{row}"].Value = icra.Above_Noise ? "Yes" : "No";
                worksheet.Cells[$"E{row}"].Value = icra.Above_Vibration ? "Yes" : "No";
                worksheet.Cells[$"F{row}"].Value = icra.Above_Dust ? "Yes" : "No";
                row++;
            }

            // Lateral
            if (!string.IsNullOrEmpty(icra.RiskGroup_Lateral))
            {
                worksheet.Cells[$"A{row}"].Value = "Lateral";
                worksheet.Cells[$"B{row}"].Value = icra.RiskGroup_Lateral;
                worksheet.Cells[$"C{row}"].Value = icra.LocalNumber_Lateral;
                worksheet.Cells[$"D{row}"].Value = icra.Lateral_Noise ? "Yes" : "No";
                worksheet.Cells[$"E{row}"].Value = icra.Lateral_Vibration ? "Yes" : "No";
                worksheet.Cells[$"F{row}"].Value = icra.Lateral_Dust ? "Yes" : "No";
                row++;
            }

            // Behind
            if (!string.IsNullOrEmpty(icra.RiskGroup_Behind))
            {
                worksheet.Cells[$"A{row}"].Value = "Behind";
                worksheet.Cells[$"B{row}"].Value = icra.RiskGroup_Behind;
                worksheet.Cells[$"C{row}"].Value = icra.LocalNumber_Behind;
                worksheet.Cells[$"D{row}"].Value = icra.Behind_Noise ? "Yes" : "No";
                worksheet.Cells[$"E{row}"].Value = icra.Behind_Vibration ? "Yes" : "No";
                worksheet.Cells[$"F{row}"].Value = icra.Behind_Dust ? "Yes" : "No";
                row++;
            }

            // Front
            if (!string.IsNullOrEmpty(icra.RiskGroup_Front))
            {
                worksheet.Cells[$"A{row}"].Value = "Front";
                worksheet.Cells[$"B{row}"].Value = icra.RiskGroup_Front;
                worksheet.Cells[$"C{row}"].Value = icra.LocalNumber_Front;
                worksheet.Cells[$"D{row}"].Value = icra.Front_Noise ? "Yes" : "No";
                worksheet.Cells[$"E{row}"].Value = icra.Front_Vibration ? "Yes" : "No";
                worksheet.Cells[$"F{row}"].Value = icra.Front_Dust ? "Yes" : "No";
                row++;
            }

            // Add borders to the adjacent areas data
            var adjacentRange = worksheet.Cells[$"A17:F{row - 1}"];
            adjacentRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            adjacentRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            adjacentRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            adjacentRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            adjacentRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Signatures Section
            row += 1; // Add space
            worksheet.Cells[$"A{row}"].Value = "Signatures";
            worksheet.Cells[$"A{row}:F{row}"].Merge = true;
            worksheet.Cells[$"A{row}"].Style.Font.Bold = true;
            worksheet.Cells[$"A{row}"].Style.Font.Size = 14;
            worksheet.Cells[$"A{row}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[$"A{row}"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            worksheet.Cells[$"A{row}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            row++;

            // Add signature fields
            worksheet.Cells[$"A{row}"].Value = "Contractor:";
            worksheet.Cells[$"B{row}"].Value = icra.ContractorSign;
            worksheet.Cells[$"D{row}"].Value = "Date:";
            worksheet.Cells[$"E{row}"].Value = DateTime.Now.ToString("yyyy-MM-dd");
            row++;

            worksheet.Cells[$"A{row}"].Value = "Engineering:";
            worksheet.Cells[$"B{row}"].Value = icra.EngineeringSign;
            worksheet.Cells[$"D{row}"].Value = "Date:";
            worksheet.Cells[$"E{row}"].Value = DateTime.Now.ToString("yyyy-MM-dd");
            row++;

            worksheet.Cells[$"A{row}"].Value = "ICP:";
            worksheet.Cells[$"B{row}"].Value = icra.ICPSign;
            worksheet.Cells[$"D{row}"].Value = "Date:";
            worksheet.Cells[$"E{row}"].Value = DateTime.Now.ToString("yyyy-MM-dd");
            row++;

            worksheet.Cells[$"A{row}"].Value = "Unit/Area Rep:";
            worksheet.Cells[$"B{row}"].Value = icra.UnitAreaRep;
            worksheet.Cells[$"D{row}"].Value = "Date:";
            worksheet.Cells[$"E{row}"].Value = DateTime.Now.ToString("yyyy-MM-dd");

            // Add borders to signature fields
            var sigRange = worksheet.Cells[$"A{row - 3}:E{row}"];
            sigRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sigRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sigRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sigRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Auto-fit columns
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            // Set print options
            worksheet.PrinterSettings.FitToPage = true;
            worksheet.PrinterSettings.FitToWidth = 1;
            worksheet.PrinterSettings.FitToHeight = 0;
            worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
        }

        private void GenerateChecklistWorksheet(ExcelWorksheet worksheet, TCSkillsChecklist checklist)
        {
            // Set basic styling
            worksheet.Cells.Style.Font.Name = "Calibri";
            worksheet.Cells.Style.Font.Size = 11;

            // Header
            worksheet.Cells["A1"].Value = "Terminal Cleaning Skills Checklist";
            worksheet.Cells["A1:F1"].Merge = true;
            worksheet.Cells["A1"].Style.Font.Bold = true;
            worksheet.Cells["A1"].Style.Font.Size = 16;
            worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Date and Checklist Number
            worksheet.Cells["A2"].Value = $"Date: {DateTime.Now.ToString("yyyy-MM-dd")}";
            worksheet.Cells["A2:C2"].Merge = true;
            worksheet.Cells["E2"].Value = $"Checklist #: {checklist.Id}";
            worksheet.Cells["E2:F2"].Merge = true;

            // Basic Information
            worksheet.Cells["A4"].Value = "Project Reference:";
            worksheet.Cells["B4"].Value = checklist.ProjectReferenceNumber;
            worksheet.Cells["D4"].Value = "Start Date:";
            worksheet.Cells["E4"].Value = checklist.ProjectStartDate?.ToString("yyyy-MM-dd");

            worksheet.Cells["A5"].Value = "Project Name:";
            worksheet.Cells["B5"].Value = checklist.ProjectNameAndDescription;
            worksheet.Cells["D5"].Value = "Duration:";
            worksheet.Cells["E5"].Value = checklist.EstimatedDuration;

            worksheet.Cells["A6"].Value = "Location:";
            worksheet.Cells["B6"].Value = checklist.SpecificSiteOfActivity;

            worksheet.Cells["A7"].Value = "Contractor:";
            worksheet.Cells["B7"].Value = checklist.ContractorRepresentativeName;

            worksheet.Cells["A8"].Value = "Contact:";
            worksheet.Cells["B8"].Value = checklist.TelephoneOrMobileNumber;

            // Add border and background to basic info
            var infoRange = worksheet.Cells["A4:E8"];
            infoRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            infoRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            infoRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            infoRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Sections
            int currentRow = 10;

            // 1. Barrier Assessment
            currentRow = AddDetailedChecklistSection(worksheet, currentRow, "1. BARRIER ASSESSMENT", new List<string>
    {
        "ICRA Barrier",
        "Doors Sealed",
        "Floor Clean",
        "Walk Off Mats",
        "Tape Adhering"
    }, new List<string>
    {
        checklist.BarrierICRA,
        checklist.BarrierDoorsSealedSelection,
        checklist.BarrierFloorCleanSelection,
        checklist.BarrierWalkOffMatsSelection,
        checklist.BarrierTapeAdheringSelection
    }, new List<string>
    {
        checklist.BarrierICRAComments,
        checklist.BarrierDoorsSealedComments,
        checklist.BarrierFloorCleanComments,
        checklist.BarrierWalkOffMatsComments,
        checklist.BarrierTapeAdheringComments
    });

            // Add spacing between sections
            currentRow += 1;

            // 2. Air Handling Assessment
            currentRow = AddDetailedChecklistSection(worksheet, currentRow, "2. AIR HANDLING ASSESSMENT", new List<string>
    {
        "Windows Closed Behind Barrier",
        "Negative Air Monitored",
        "Air Handling Unit Running",
        "Maintenance Label Visible",
        "Air Exhausted To Appropriate Area"
    }, new List<string>
    {
        checklist.AirhandlingWindowsClosedBehindBarrierSelection,
        checklist.AirhandlingNegativeAirMonitoredSelection,
        checklist.AirHandlingUnitRunningSelection,
        checklist.AirhandlingMaintenanceLabelVisibleSelection,
        checklist.AirhandlingAirExhaustedToAppropriateAreaSelection
    }, new List<string>
    {
        checklist.AirhandlingWindowsClosedBehindBarrierComments,
        checklist.AirhandlingNegativeAirMonitoredComments,
        checklist.AirHandlingUnitRunningComments,
        checklist.AirhandlingMaintenanceLabelVisibleComments,
        checklist.AirhandlingAirExhaustedToAppropriateAreaComments
    });

            // Add spacing between sections
            currentRow += 1;

            // 3. Project Area Assessment
            currentRow = AddDetailedChecklistSection(worksheet, currentRow, "3. PROJECT AREA ASSESSMENT", new List<string>
    {
        "HEPA Filtered Vacuum On Jobsite",
        "Debris Removed In Covered Container Daily",
        "Designated Construction Route Or Map Posted",
        "Trash In Appropriate Container",
        "Routine Cleaning Done On Site",
        "Air Vents Sealed Or Ductwork Capped"
    }, new List<string>
    {
        checklist.ProjectareaHEPAFilteredVacuumOnJobsiteSelection,
        checklist.ProjectareaDebrisRemovedInCoveredContainerDailySelection,
        checklist.ProjectareaDesignatedConstructionRouteOrMapPostedSelection,
        checklist.ProjectareaTrashInAppropriateContainerSelection,
        checklist.ProjectareaRoutineCleaningDoneOnSiteSelection,
        checklist.ProjectareaAirVentsSealedOrDuctworkCappedSelection
    }, new List<string>
    {
        checklist.ProjectareaHEPAFilteredVacuumOnJobsiteComments,
        checklist.ProjectareaDebrisRemovedInCoveredContainerDailyComments,
        checklist.ProjectareaDesignatedConstructionRouteOrMapPostedComments,
        checklist.ProjectareaTrashInAppropriateContainerComments,
        checklist.ProjectareaRoutineCleaningDoneOnSiteComments,
        checklist.ProjectareaAirVentsSealedOrDuctworkCappedComments
    });

            // Add spacing between sections
            currentRow += 1;

            // 4. Traffic Control Assessment
            currentRow = AddDetailedChecklistSection(worksheet, currentRow, "4. TRAFFIC CONTROL ASSESSMENT", new List<string>
    {
        "Restricted To Construction Workers",
        "Doors And Exits Free Of Debris"
    }, new List<string>
    {
        checklist.TrafficcontrolRestrictedToConstructionWorkersSelection,
        checklist.TrafficcontrolDoorsAndExitsFreeOfDebrisSelection
    }, new List<string>
    {
        checklist.TrafficcontrolRestrictedToConstructionWorkersComments,
        checklist.TrafficcontrolDoorsAndExitsFreeOfDebrisComments
    });

            // Add spacing between sections
            currentRow += 1;

            // 5. Dress Code Assessment
            currentRow = AddDetailedChecklistSection(worksheet, currentRow, "5. DRESS CODE ASSESSMENT", new List<string>
    {
        "Protective Clothing Worn",
        "Workers Clothing Clean Upon Exiting"
    }, new List<string>
    {
        checklist.DresscodeProtectiveClothingWornSelection,
        checklist.DresscodeWorkersClothingCleanUponExitingSelection
    }, new List<string>
    {
        checklist.DresscodeProtectiveClothingWornComments,
        checklist.DresscodeWorkersClothingCleanUponExitingComments
    });

            // Add spacing before signatures
            currentRow += 2;

            // Signatures Section
            worksheet.Cells[$"A{currentRow}"].Value = "Signatures";
            worksheet.Cells[$"A{currentRow}:F{currentRow}"].Merge = true;
            worksheet.Cells[$"A{currentRow}"].Style.Font.Bold = true;
            worksheet.Cells[$"A{currentRow}"].Style.Font.Size = 14;
            worksheet.Cells[$"A{currentRow}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[$"A{currentRow}"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            worksheet.Cells[$"A{currentRow}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            currentRow++;

            // Add signature fields
            worksheet.Cells[$"A{currentRow}"].Value = "Contractor:";
            worksheet.Cells[$"B{currentRow}"].Value = checklist.ContractorSign;
            worksheet.Cells[$"D{currentRow}"].Value = "Date:";
            worksheet.Cells[$"E{currentRow}"].Value = DateTime.Now.ToString("yyyy-MM-dd");
            currentRow++;

            worksheet.Cells[$"A{currentRow}"].Value = "Engineering:";
            worksheet.Cells[$"B{currentRow}"].Value = checklist.EngineeringSign;
            worksheet.Cells[$"D{currentRow}"].Value = "Date:";
            worksheet.Cells[$"E{currentRow}"].Value = DateTime.Now.ToString("yyyy-MM-dd");
            currentRow++;

            worksheet.Cells[$"A{currentRow}"].Value = "ICP:";
            worksheet.Cells[$"B{currentRow}"].Value = checklist.ICPSign;
            worksheet.Cells[$"D{currentRow}"].Value = "Date:";
            worksheet.Cells[$"E{currentRow}"].Value = DateTime.Now.ToString("yyyy-MM-dd");
            currentRow++;

            worksheet.Cells[$"A{currentRow}"].Value = "Unit/Area Rep:";
            worksheet.Cells[$"B{currentRow}"].Value = checklist.UnitAreaRep;
            worksheet.Cells[$"D{currentRow}"].Value = "Date:";
            worksheet.Cells[$"E{currentRow}"].Value = DateTime.Now.ToString("yyyy-MM-dd");

            // Add borders to signature fields
            var sigRange = worksheet.Cells[$"A{currentRow - 3}:E{currentRow}"];
            sigRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sigRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sigRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sigRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Auto-fit columns
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            // Set print options
            worksheet.PrinterSettings.FitToPage = true;
            worksheet.PrinterSettings.FitToWidth = 1;
            worksheet.PrinterSettings.FitToHeight = 0;
            worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
        }

        private int AddDetailedChecklistSection(ExcelWorksheet worksheet, int startRow, string sectionTitle, List<string> items, List<string> selections, List<string> comments)
        {
            // Section title
            worksheet.Cells[$"A{startRow}"].Value = sectionTitle;
            worksheet.Cells[$"A{startRow}:F{startRow}"].Merge = true;
            worksheet.Cells[$"A{startRow}"].Style.Font.Bold = true;
            worksheet.Cells[$"A{startRow}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[$"A{startRow}"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            worksheet.Cells[$"A{startRow}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            startRow++;

            // Add headers
            worksheet.Cells[$"A{startRow}"].Value = "Item";
            worksheet.Cells[$"B{startRow}"].Value = "Status";
            worksheet.Cells[$"C{startRow}:F{startRow}"].Merge = true;
            worksheet.Cells[$"C{startRow}"].Value = "Comments";

            // Style the header row
            var headerRange = worksheet.Cells[$"A{startRow}:F{startRow}"];
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            headerRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            headerRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            headerRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            headerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            startRow++;

            // Populate items
            for (int i = 0; i < items.Count; i++)
            {
                worksheet.Cells[$"A{startRow + i}"].Value = items[i];
                worksheet.Cells[$"B{startRow + i}"].Value = selections[i];
                worksheet.Cells[$"C{startRow + i}:F{startRow + i}"].Merge = true;
                worksheet.Cells[$"C{startRow + i}"].Value = comments[i];

                // Add visual status indicator
                if (!string.IsNullOrEmpty(selections[i]))
                {
                    // Color-coding based on status
                    if (selections[i].Equals("Yes", StringComparison.OrdinalIgnoreCase) ||
                        selections[i].Equals("Complete", StringComparison.OrdinalIgnoreCase) ||
                        selections[i].Equals("Satisfactory", StringComparison.OrdinalIgnoreCase))
                    {
                        worksheet.Cells[$"B{startRow + i}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[$"B{startRow + i}"].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                    }
                    else if (selections[i].Equals("No", StringComparison.OrdinalIgnoreCase) ||
                            selections[i].Equals("Incomplete", StringComparison.OrdinalIgnoreCase) ||
                            selections[i].Equals("Unsatisfactory", StringComparison.OrdinalIgnoreCase))
                    {
                        worksheet.Cells[$"B{startRow + i}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[$"B{startRow + i}"].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);
                    }
                    else if (selections[i].Equals("N/A", StringComparison.OrdinalIgnoreCase))
                    {
                        worksheet.Cells[$"B{startRow + i}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[$"B{startRow + i}"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    }
                }

                worksheet.Cells[$"B{startRow + i}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            }

            // Add borders to data cells
            var dataRange = worksheet.Cells[$"A{startRow}:F{startRow + items.Count - 1}"];
            dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            return startRow + items.Count;
        }

        private void GeneratePostConstructionWorksheet(ExcelWorksheet worksheet, PostConstruction postConstruction)
        {
            // Set basic styling
            worksheet.Cells.Style.Font.Name = "Calibri";
            worksheet.Cells.Style.Font.Size = 11;

            // Header
            worksheet.Cells["A1"].Value = "Post Construction Report";
            worksheet.Cells["A1:F1"].Merge = true;
            worksheet.Cells["A1"].Style.Font.Bold = true;
            worksheet.Cells["A1"].Style.Font.Size = 16;
            worksheet.Cells["A1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            // Basic Information
            worksheet.Cells["A3"].Value = "Project Reference Number:";
            worksheet.Cells["B3"].Value = postConstruction.ProjectReferenceNumber;
            worksheet.Cells["D3"].Value = "Start Date:";
            worksheet.Cells["E3"].Value = postConstruction.ProjectStartDate?.ToString("yyyy-MM-dd");

            worksheet.Cells["A4"].Value = "Project Name:";
            worksheet.Cells["B4"].Value = postConstruction.ProjectNameAndDescription;
            worksheet.Cells["D4"].Value = "Estimated Duration:";
            worksheet.Cells["E4"].Value = postConstruction.EstimatedDuration;

            worksheet.Cells["A5"].Value = "Location:";
            worksheet.Cells["B5"].Value = postConstruction.SpecificSiteOfActivity;
            worksheet.Cells["D5"].Value = "Date Completed:";
            worksheet.Cells["E5"].Value = postConstruction.DateCompleted?.ToString("yyyy-MM-dd");

            // Add border and background to basic info
            var infoRange = worksheet.Cells["A3:E5"];
            infoRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            infoRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            infoRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            infoRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Post Construction Cleaning Section
            int currentRow = 7;
            currentRow = AddPostConstructionSection(worksheet, currentRow, "POST CONSTRUCTION CLEANING", new List<string>
            {
                "Before Hoarding",
                "Facility Based",
                "After Hoarding",
                "Final Cleaning",
                "Construction Waste Removed",
                "Air Vents Cleaned",
                "HEPA Vacuumed",
                "Walls Cleaned",
                "Ceiling Cleaned",
                "Floors Cleaned",
                "Windows Cleaned",
                "All Surfaces Dusted"
            }, postConstruction);

            // Finishes Section
            currentRow += 2;
            currentRow = AddPostConstructionSection(worksheet, currentRow, "FINISHES", new List<string>
            {
                "Area Is",
                "Walls",
                "Ceilings",
                "Floors",
                "Windows",
                "Doors",
                "Fixtures",
                "Equipment"
            }, postConstruction);

            // Infrastructure Section
            currentRow += 2;
            currentRow = AddPostConstructionSection(worksheet, currentRow, "INFRASTRUCTURE", new List<string>
            {
                "If Plumbing has been Affected",
                "If Electrical has been Affected",
                "If HVAC has been Affected",
                "If Medical Gases have been Affected",
                "If Fire Safety has been Affected"
            }, postConstruction);

            // Signatures Section
            currentRow += 2;
            worksheet.Cells[$"A{currentRow}"].Value = "Signatures";
            worksheet.Cells[$"A{currentRow}:F{currentRow}"].Merge = true;
            worksheet.Cells[$"A{currentRow}"].Style.Font.Bold = true;
            worksheet.Cells[$"A{currentRow}"].Style.Font.Size = 14;
            worksheet.Cells[$"A{currentRow}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[$"A{currentRow}"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            worksheet.Cells[$"A{currentRow}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            currentRow++;

            // Add signature fields
            worksheet.Cells[$"A{currentRow}"].Value = "Contractor:";
            worksheet.Cells[$"B{currentRow}"].Value = postConstruction.ContractorSign;
            worksheet.Cells[$"D{currentRow}"].Value = "Date:";
            worksheet.Cells[$"E{currentRow}"].Value = DateTime.Now.ToString("yyyy-MM-dd");
            currentRow++;

            worksheet.Cells[$"A{currentRow}"].Value = "Engineering:";
            worksheet.Cells[$"B{currentRow}"].Value = postConstruction.EngineeringSign;
            worksheet.Cells[$"D{currentRow}"].Value = "Date:";
            worksheet.Cells[$"E{currentRow}"].Value = DateTime.Now.ToString("yyyy-MM-dd");
            currentRow++;

            worksheet.Cells[$"A{currentRow}"].Value = "ICP:";
            worksheet.Cells[$"B{currentRow}"].Value = postConstruction.ICPSign;
            worksheet.Cells[$"D{currentRow}"].Value = "Date:";
            worksheet.Cells[$"E{currentRow}"].Value = DateTime.Now.ToString("yyyy-MM-dd");
            currentRow++;

            worksheet.Cells[$"A{currentRow}"].Value = "Unit/Area Rep:";
            worksheet.Cells[$"B{currentRow}"].Value = postConstruction.UnitAreaRep;
            worksheet.Cells[$"D{currentRow}"].Value = "Date:";
            worksheet.Cells[$"E{currentRow}"].Value = DateTime.Now.ToString("yyyy-MM-dd");

            // Add borders to signature fields
            var sigRange = worksheet.Cells[$"A{currentRow - 3}:E{currentRow}"];
            sigRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            sigRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            sigRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            sigRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

            // Auto-fit columns
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            // Set print options
            worksheet.PrinterSettings.FitToPage = true;
            worksheet.PrinterSettings.FitToWidth = 1;
            worksheet.PrinterSettings.FitToHeight = 0;
            worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
        }

        private int AddPostConstructionSection(ExcelWorksheet worksheet, int startRow, string sectionTitle, List<string> items, PostConstruction postConstruction)
        {
            // Section title
            worksheet.Cells[$"A{startRow}"].Value = sectionTitle;
            worksheet.Cells[$"A{startRow}:F{startRow}"].Merge = true;
            worksheet.Cells[$"A{startRow}"].Style.Font.Bold = true;
            worksheet.Cells[$"A{startRow}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            worksheet.Cells[$"A{startRow}"].Style.Fill.BackgroundColor.SetColor(Color.LightBlue);
            worksheet.Cells[$"A{startRow}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            startRow++;

            // Add headers
            worksheet.Cells[$"A{startRow}"].Value = "Item";
            worksheet.Cells[$"B{startRow}"].Value = "Status";
            worksheet.Cells[$"C{startRow}"].Value = "Date Completed";
            worksheet.Cells[$"D{startRow}:F{startRow}"].Merge = true;
            worksheet.Cells[$"D{startRow}"].Value = "Comments";

            // Style the header row
            var headerRange = worksheet.Cells[$"A{startRow}:F{startRow}"];
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            headerRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            headerRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            headerRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            headerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            startRow++;

            // Return early if there are no items
            if (items == null || items.Count == 0)
            {
                return startRow;
            }

            // Populate items
            foreach (var item in items)
            {
                var propName = item.Replace(" ", "");
                var statusProp = postConstruction.GetType().GetProperty(propName);
                var dateProp = postConstruction.GetType().GetProperty(propName + "DC");

                if (statusProp != null && dateProp != null)
                {
                    var statusValue = statusProp.GetValue(postConstruction)?.ToString();
                    var dateValue = dateProp.GetValue(postConstruction)?.ToString();

                    worksheet.Cells[$"A{startRow}"].Value = item;
                    worksheet.Cells[$"B{startRow}"].Value = statusValue;
                    worksheet.Cells[$"C{startRow}"].Value = dateValue;
                    worksheet.Cells[$"D{startRow}:F{startRow}"].Merge = true;

                    // Color coding for status
                    if (!string.IsNullOrEmpty(statusValue))
                    {
                        if (statusValue.Equals("Complete", StringComparison.OrdinalIgnoreCase) ||
                            statusValue.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                        {
                            worksheet.Cells[$"B{startRow}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[$"B{startRow}"].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                        }
                        else if (statusValue.Equals("Incomplete", StringComparison.OrdinalIgnoreCase) ||
                                statusValue.Equals("No", StringComparison.OrdinalIgnoreCase))
                        {
                            worksheet.Cells[$"B{startRow}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[$"B{startRow}"].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);
                        }
                    }

                    worksheet.Cells[$"B{startRow}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    startRow++;
                }
            }

            // Add borders to data cells, only if items exist
            // Validate items
            if (items == null || items.Count == 0)
            {
                return startRow; // Return early if there are no items
            }

            // Populate items
            foreach (var item in items)
            {
                var propName = item.Replace(" ", "");
                var statusProp = postConstruction.GetType().GetProperty(propName);
                var dateProp = postConstruction.GetType().GetProperty(propName + "DC");

                if (statusProp != null && dateProp != null)
                {
                    var statusValue = statusProp.GetValue(postConstruction)?.ToString();
                    var dateValue = dateProp.GetValue(postConstruction)?.ToString();

                    worksheet.Cells[$"A{startRow}"].Value = item;
                    worksheet.Cells[$"B{startRow}"].Value = statusValue;
                    worksheet.Cells[$"C{startRow}"].Value = dateValue;
                    worksheet.Cells[$"D{startRow}:F{startRow}"].Merge = true;

                    // Color coding for status
                    if (!string.IsNullOrEmpty(statusValue))
                    {
                        if (statusValue.Equals("Complete", StringComparison.OrdinalIgnoreCase) ||
                            statusValue.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                        {
                            worksheet.Cells[$"B{startRow}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[$"B{startRow}"].Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                        }
                        else if (statusValue.Equals("Incomplete", StringComparison.OrdinalIgnoreCase) ||
                                statusValue.Equals("No", StringComparison.OrdinalIgnoreCase))
                        {
                            worksheet.Cells[$"B{startRow}"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[$"B{startRow}"].Style.Fill.BackgroundColor.SetColor(Color.LightSalmon);
                        }
                    }

                    worksheet.Cells[$"B{startRow}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    startRow++;
                }
            }

            // Add borders to data cells only if there are items
            if (items.Count > 0)
            {
                var dataRange = worksheet.Cells[$"A{startRow - items.Count}:F{startRow - 1}"];
                dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            }

            return startRow;
        }
    }
}