using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using IPCU.Data;
using IPCU.Models;

namespace IPCU.Controllers
{
    [Authorize(Roles = "Admin,ICN")]
    public class ReportsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportsController(ApplicationDbContext context)
        {
            _context = context;
            // Set EPPlus license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        // GET: Reports
        public IActionResult Index()
        {
            return View();
        }

        // GET: Reports/ICRAReport
        public async Task<IActionResult> ICRAReport()
        {
            // Get all ICRAs with related data
            var icras = await _context.ICRA
                .ToListAsync();

            return View(icras);
        }

        // POST: Reports/GenerateExcel
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GenerateExcel(List<int> selectedICRAs)
        {
            if (selectedICRAs == null || !selectedICRAs.Any())
            {
                TempData["ErrorMessage"] = "Please select at least one ICRA to generate a report.";
                return RedirectToAction(nameof(ICRAReport));
            }

            // Fetch selected ICRAs with all related data
            var icras = await _context.ICRA
                .Where(i => selectedICRAs.Contains(i.Id))
                .ToListAsync();

            // Fetch all related TCSkillsChecklists for selected ICRAs
            var checklists = await _context.TCSkillsChecklist
                .Where(c => selectedICRAs.Contains(c.ICRAId))
                .ToListAsync();

            // Fetch all related PostConstruction records for selected ICRAs
            var postConstructions = await _context.PostConstruction
                .Where(p => selectedICRAs.Contains(p.ICRAId))
                .ToListAsync();

            // Create Excel package
            using (var package = new ExcelPackage())
            {
                // Add ICRA Summary worksheet
                var icraWorksheet = package.Workbook.Worksheets.Add("ICRA Summary");

                // Create header row for ICRA summary
                string[] headers = new string[] {
                    "ID", "Project Reference", "Project Name", "Site Location", "Start Date",
                    "Duration", "Contractor", "Contact", "Risk Level", "Engineering Sign",
                    "ICP Sign", "Unit Area Rep", "Has Checklists", "Has Post-Construction"
                };

                for (int i = 0; i < headers.Length; i++)
                {
                    icraWorksheet.Cells[1, i + 1].Value = headers[i];
                    icraWorksheet.Cells[1, i + 1].Style.Font.Bold = true;
                }

                // Populate data rows for ICRA summary
                int row = 2;
                foreach (var icra in icras)
                {
                    icraWorksheet.Cells[row, 1].Value = icra.Id;
                    icraWorksheet.Cells[row, 2].Value = icra.ProjectReferenceNumber;
                    icraWorksheet.Cells[row, 3].Value = icra.ProjectNameAndDescription;
                    icraWorksheet.Cells[row, 4].Value = icra.SpecificSiteOfActivity;
                    icraWorksheet.Cells[row, 5].Value = icra.ProjectStartDate;
                    icraWorksheet.Cells[row, 6].Value = icra.EstimatedDuration;
                    icraWorksheet.Cells[row, 7].Value = icra.ContractorRepresentativeName;
                    icraWorksheet.Cells[row, 8].Value = icra.TelephoneOrMobileNumber;
                    icraWorksheet.Cells[row, 9].Value = GetRiskLevelFromPreventiveMeasures(icra.PreventiveMeasures);
                    icraWorksheet.Cells[row, 10].Value = !string.IsNullOrEmpty(icra.EngineeringSign) ? "Yes" : "No";
                    icraWorksheet.Cells[row, 11].Value = !string.IsNullOrEmpty(icra.ICPSign) ? "Yes" : "No";
                    icraWorksheet.Cells[row, 12].Value = !string.IsNullOrEmpty(icra.UnitAreaRep) ? "Yes" : "No";
                    icraWorksheet.Cells[row, 13].Value = checklists.Any(c => c.ICRAId == icra.Id) ? "Yes" : "No";
                    icraWorksheet.Cells[row, 14].Value = postConstructions.Any(p => p.ICRAId == icra.Id) ? "Yes" : "No";

                    row++;
                }

                // Format ICRA Summary worksheet
                icraWorksheet.Cells.AutoFitColumns();
                var headerRange = icraWorksheet.Cells[1, 1, 1, headers.Length];
                headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                headerRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                headerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;

                // Add detailed worksheet for each ICRA
                foreach (var icra in icras)
                {
                    // Create ICRA Detail worksheet
                    var detailWorksheet = package.Workbook.Worksheets.Add($"ICRA-{icra.Id}");

                    // ICRA Header
                    detailWorksheet.Cells[1, 1].Value = "ICRA Details";
                    detailWorksheet.Cells[1, 1].Style.Font.Bold = true;
                    detailWorksheet.Cells[1, 1].Style.Font.Size = 14;
                    detailWorksheet.Cells[1, 1, 1, 4].Merge = true;

                    // ICRA Main Info
                    var infoHeaders = new string[] {
                        "Project Reference", "Project Name", "Site Location", "Start Date", "Duration",
                        "Contractor", "Contact", "Risk Level", "Construction Type"
                    };

                    for (int i = 0; i < infoHeaders.Length; i++)
                    {
                        detailWorksheet.Cells[i + 3, 1].Value = infoHeaders[i];
                        detailWorksheet.Cells[i + 3, 1].Style.Font.Bold = true;
                    }

                    detailWorksheet.Cells[3, 2].Value = icra.ProjectReferenceNumber;
                    detailWorksheet.Cells[4, 2].Value = icra.ProjectNameAndDescription;
                    detailWorksheet.Cells[5, 2].Value = icra.SpecificSiteOfActivity;
                    detailWorksheet.Cells[6, 2].Value = icra.ProjectStartDate?.ToShortDateString();
                    detailWorksheet.Cells[7, 2].Value = icra.EstimatedDuration;
                    detailWorksheet.Cells[8, 2].Value = icra.ContractorRepresentativeName;
                    detailWorksheet.Cells[9, 2].Value = icra.TelephoneOrMobileNumber;
                    detailWorksheet.Cells[10, 2].Value = GetRiskLevelFromPreventiveMeasures(icra.PreventiveMeasures);
                    detailWorksheet.Cells[11, 2].Value = icra.ConstructionType;

                    // Signatures section
                    detailWorksheet.Cells[13, 1].Value = "Signatures";
                    detailWorksheet.Cells[13, 1].Style.Font.Bold = true;

                    detailWorksheet.Cells[14, 1].Value = "Contractor";
                    detailWorksheet.Cells[14, 2].Value = !string.IsNullOrEmpty(icra.ContractorSign) ? "Signed" : "Not Signed";

                    detailWorksheet.Cells[15, 1].Value = "Engineering";
                    detailWorksheet.Cells[15, 2].Value = !string.IsNullOrEmpty(icra.EngineeringSign) ? "Signed" : "Not Signed";

                    detailWorksheet.Cells[16, 1].Value = "ICP";
                    detailWorksheet.Cells[16, 2].Value = !string.IsNullOrEmpty(icra.ICPSign) ? "Signed" : "Not Signed";

                    detailWorksheet.Cells[17, 1].Value = "Unit Area Rep";
                    detailWorksheet.Cells[17, 2].Value = !string.IsNullOrEmpty(icra.UnitAreaRep) ? "Signed" : "Not Signed";

                    // Risk Assessment Section
                    detailWorksheet.Cells[19, 1].Value = "Risk Assessment";
                    detailWorksheet.Cells[19, 1].Style.Font.Bold = true;

                    // Adjacent Areas
                    detailWorksheet.Cells[20, 1].Value = "Adjacent Areas";
                    detailWorksheet.Cells[20, 1].Style.Font.Bold = true;

                    var directions = new string[] { "Below", "Above", "Lateral", "Behind", "Front" };

                    // Headers for adjacent areas
                    detailWorksheet.Cells[21, 2].Value = "Risk Group";
                    detailWorksheet.Cells[21, 3].Value = "Local Number";

                    for (int i = 0; i < directions.Length; i++)
                    {
                        detailWorksheet.Cells[i + 22, 1].Value = directions[i];

                        var riskGroup = GetPropertyValue(icra, $"RiskGroup_{directions[i]}");
                        detailWorksheet.Cells[i + 22, 2].Value = riskGroup;

                        var localNumber = GetPropertyValue(icra, $"LocalNumber_{directions[i]}");
                        detailWorksheet.Cells[i + 22, 3].Value = localNumber;
                    }

                    // Impact Assessment section
                    detailWorksheet.Cells[28, 1].Value = "Impact Assessment";
                    detailWorksheet.Cells[28, 1].Style.Font.Bold = true;

                    // Headers for impact types
                    var impactTypes = new string[] {
                        "Noise", "Vibration", "Dust", "Ventilation", "Pressurization",
                        "Data", "Mechanical", "MedicalGas", "HotColdWater", "Other"
                    };

                    for (int i = 0; i < directions.Length; i++)
                    {
                        detailWorksheet.Cells[29, i + 2].Value = directions[i];
                        detailWorksheet.Cells[29, i + 2].Style.Font.Bold = true;
                    }

                    for (int i = 0; i < impactTypes.Length; i++)
                    {
                        detailWorksheet.Cells[i + 30, 1].Value = impactTypes[i];

                        for (int j = 0; j < directions.Length; j++)
                        {
                            var impactValue = GetPropertyValue(icra, $"{directions[j]}_{impactTypes[i]}");
                            detailWorksheet.Cells[i + 30, j + 2].Value = impactValue == "True" ? "Yes" : "No";
                        }
                    }

                    // TCSkillsChecklist section (if any)
                    var icraChecklists = checklists.Where(c => c.ICRAId == icra.Id).ToList();
                    if (icraChecklists.Any())
                    {
                        detailWorksheet.Cells[41, 1].Value = "Skills Checklists";
                        detailWorksheet.Cells[41, 1].Style.Font.Bold = true;
                        detailWorksheet.Cells[41, 1].Style.Font.Size = 12;

                        row = 42;
                        foreach (var checklist in icraChecklists)
                        {
                            detailWorksheet.Cells[row, 1].Value = $"Checklist #{checklist.Id}";
                            detailWorksheet.Cells[row, 1].Style.Font.Bold = true;

                            detailWorksheet.Cells[row + 1, 1].Value = "Date";
                            detailWorksheet.Cells[row + 1, 2].Value = checklist.ProjectStartDate;

                            detailWorksheet.Cells[row + 2, 1].Value = "Project Name And Description";
                            detailWorksheet.Cells[row + 2, 2].Value = checklist.ProjectNameAndDescription;

                            detailWorksheet.Cells[row + 3, 1].Value = "Contractor Representative Name";
                            detailWorksheet.Cells[row + 3, 2].Value = checklist.ContractorRepresentativeName;

                            row += 5;
                        }
                    }

                    // PostConstruction section (if any)
                    var postConstruction = postConstructions.FirstOrDefault(p => p.ICRAId == icra.Id);
                    if (postConstruction != null)
                    {
                        // Set the start row based on where the previous sections ended
                        int postConstructionStartRow = row + 2;

                        detailWorksheet.Cells[postConstructionStartRow, 1].Value = "Post-Construction";
                        detailWorksheet.Cells[postConstructionStartRow, 1].Style.Font.Bold = true;
                        detailWorksheet.Cells[postConstructionStartRow, 1].Style.Font.Size = 12;

                        // Post-Construction Cleaning section
                        detailWorksheet.Cells[postConstructionStartRow + 1, 1].Value = "Post-Construction Cleaning";
                        detailWorksheet.Cells[postConstructionStartRow + 1, 1].Style.Font.Bold = true;

                        // Replace the section in GenerateExcel method that gets property values with this:
                        var cleaningItems = new string[] {
                            "Before Hoarding", "Facility Based", "After Removal", "Where Required"
                        };
                                                var cleaningProps = new string[] {
                            "BeforeHoarding", "FacilityBased", "AfterRemoval", "WhereRequired"
                        };

                        for (int i = 0; i < cleaningItems.Length; i++)
                        {
                            detailWorksheet.Cells[postConstructionStartRow + 2 + i, 1].Value = cleaningItems[i];

                            // Add null checks
                            var propValue = GetPropertyValue(postConstruction, cleaningProps[i]);
                            detailWorksheet.Cells[postConstructionStartRow + 2 + i, 2].Value = propValue;

                            var dcPropValue = GetPropertyValue(postConstruction, $"{cleaningProps[i]}DC");
                            detailWorksheet.Cells[postConstructionStartRow + 2 + i, 3].Value = dcPropValue;
                        }

                        // Finishes section
                        int finishesStartRow = postConstructionStartRow + cleaningItems.Length + 3;

                        detailWorksheet.Cells[finishesStartRow, 1].Value = "Finishes";
                        detailWorksheet.Cells[finishesStartRow, 1].Style.Font.Bold = true;

                        var finishesItems = new string[] {
                            "Area Is", "Integrity of Walls", "Surface in Patient", "Area Surfaces"
                        };
                        var finishesProps = new string[] {
                            "AreaIs", "IntegrityofWalls", "SurfaceinPatient", "AreaSurfaces"
                        };

                        for (int i = 0; i < finishesItems.Length; i++)
                        {
                            detailWorksheet.Cells[finishesStartRow + 1 + i, 1].Value = finishesItems[i];
                            detailWorksheet.Cells[finishesStartRow + 1 + i, 2].Value = GetPropertyValue(postConstruction, finishesProps[i]);
                            detailWorksheet.Cells[finishesStartRow + 1 + i, 3].Value = GetPropertyValue(postConstruction, $"{finishesProps[i]}DC");
                        }

                        // Infrastructure section
                        int infraStartRow = finishesStartRow + finishesItems.Length + 3;

                        detailWorksheet.Cells[infraStartRow, 1].Value = "Infrastructure";
                        detailWorksheet.Cells[infraStartRow, 1].Style.Font.Bold = true;

                        var infraItems = new string[] {
                            "If Plumbing has been Affected", "Plumbing if Affected", "Correct Hand Washing",
                            "Faucet Aerators", "Ceiling Tiles", "HVAC Systems",
                            "Correct Room Pressurization", "All Mechanical Spaces"
                        };
                        var infraProps = new string[] {
                            "IfPlumbinghasbeenAffected", "PlumbingifAffected", "CorrectHandWashing",
                            "FaucetAerators", "CeilingTiles", "HVACSystems",
                            "CorrectRoomPressurization", "AllMechanicalSpaces"
                        };

                        for (int i = 0; i < infraItems.Length; i++)
                        {
                            detailWorksheet.Cells[infraStartRow + 1 + i, 1].Value = infraItems[i];
                            detailWorksheet.Cells[infraStartRow + 1 + i, 2].Value = GetPropertyValue(postConstruction, infraProps[i]);
                            detailWorksheet.Cells[infraStartRow + 1 + i, 3].Value = GetPropertyValue(postConstruction, $"{infraProps[i]}DC");
                        }

                        // Post-Construction Signatures
                        int signaturesRow = infraStartRow + infraItems.Length + 3;

                        detailWorksheet.Cells[signaturesRow, 1].Value = "Post-Construction Signatures";
                        detailWorksheet.Cells[signaturesRow, 1].Style.Font.Bold = true;

                        detailWorksheet.Cells[signaturesRow + 1, 1].Value = "Contractor";
                        detailWorksheet.Cells[signaturesRow + 1, 2].Value = !string.IsNullOrEmpty(postConstruction.ContractorSign) ? "Signed" : "Not Signed";

                        detailWorksheet.Cells[signaturesRow + 2, 1].Value = "Engineering";
                        detailWorksheet.Cells[signaturesRow + 2, 2].Value = !string.IsNullOrEmpty(postConstruction.EngineeringSign) ? "Signed" : "Not Signed";

                        detailWorksheet.Cells[signaturesRow + 3, 1].Value = "ICP";
                        detailWorksheet.Cells[signaturesRow + 3, 2].Value = !string.IsNullOrEmpty(postConstruction.ICPSign) ? "Signed" : "Not Signed";

                        detailWorksheet.Cells[signaturesRow + 4, 1].Value = "Unit Area Rep";
                        detailWorksheet.Cells[signaturesRow + 4, 2].Value = !string.IsNullOrEmpty(postConstruction.UnitAreaRep) ? "Signed" : "Not Signed";

                        detailWorksheet.Cells[signaturesRow + 5, 1].Value = "Date Completed";
                        detailWorksheet.Cells[signaturesRow + 5, 2].Value = postConstruction.DateCompleted?.ToShortDateString() ?? "";
                    }

                    // Format the detailed worksheet
                    detailWorksheet.Cells.AutoFitColumns();
                }

                // Add a TCSkillsChecklist Summary worksheet
                if (checklists.Any())
                {
                    var checklistWorksheet = package.Workbook.Worksheets.Add("Checklists Summary");

                    // Headers
                    string[] checklistHeaders = new string[] {
                        "ID", "ICRA ID", "Area", "Observer", "Date", "Pre-Cleaning Items",
                        "Post-Cleaning Items", "Recommendations/Actions"
                    };

                    for (int i = 0; i < checklistHeaders.Length; i++)
                    {
                        checklistWorksheet.Cells[1, i + 1].Value = checklistHeaders[i];
                        checklistWorksheet.Cells[1, i + 1].Style.Font.Bold = true;
                    }

                    // Data
                    row = 2;
                    foreach (var checklist in checklists)
                    {
                        checklistWorksheet.Cells[row, 1].Value = checklist.Id;
                        checklistWorksheet.Cells[row, 2].Value = checklist.ICRAId;
                        checklistWorksheet.Cells[row, 3].Value = checklist.ContractorRepresentativeName;
                        checklistWorksheet.Cells[row, 4].Value = checklist.ContractorRepresentativeName;
                        checklistWorksheet.Cells[row, 5].Value = checklist.ContractorRepresentativeName;
                        checklistWorksheet.Cells[row, 6].Value = checklist.ContractorRepresentativeName;
                        checklistWorksheet.Cells[row, 7].Value = checklist.ContractorRepresentativeName;
                        checklistWorksheet.Cells[row, 8].Value = checklist.ContractorRepresentativeName;

                        row++;
                    }

                    // Format
                    checklistWorksheet.Cells.AutoFitColumns();
                    var checklistHeaderRange = checklistWorksheet.Cells[1, 1, 1, checklistHeaders.Length];
                    checklistHeaderRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    checklistHeaderRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    checklistHeaderRange.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                }

                // Add PostConstruction Summary worksheet
                if (postConstructions.Any())
                {
                    var postConstructionWorksheet = package.Workbook.Worksheets.Add("PostConstruction Summary");

                    // Headers
                    string[] pcHeaders = new string[] {
                        "ID", "ICRA ID", "Project Reference", "Project Name",
                        "Site Location", "Date Completed",
                        "Contractor", "Engineering", "ICP", "Unit Area Rep"
                    };

                    for (int i = 0; i < pcHeaders.Length; i++)
                    {
                        postConstructionWorksheet.Cells[1, i + 1].Value = pcHeaders[i];
                        postConstructionWorksheet.Cells[1, i + 1].Style.Font.Bold = true;
                    }

                    // Data
                    row = 2;
                    foreach (var pc in postConstructions)
                    {
                        postConstructionWorksheet.Cells[row, 1].Value = pc.Id;
                        postConstructionWorksheet.Cells[row, 2].Value = pc.ICRAId;
                        postConstructionWorksheet.Cells[row, 3].Value = pc.ProjectReferenceNumber;
                        postConstructionWorksheet.Cells[row, 4].Value = pc.ProjectNameAndDescription;
                        postConstructionWorksheet.Cells[row, 5].Value = pc.SpecificSiteOfActivity;
                        postConstructionWorksheet.Cells[row, 6].Value = pc.DateCompleted;
                        postConstructionWorksheet.Cells[row, 7].Value = !string.IsNullOrEmpty(pc.ContractorSign) ? "Signed" : "Not Signed";
                        postConstructionWorksheet.Cells[row, 8].Value = !string.IsNullOrEmpty(pc.EngineeringSign) ? "Signed" : "Not Signed";
                        postConstructionWorksheet.Cells[row, 9].Value = !string.IsNullOrEmpty(pc.ICPSign) ? "Signed" : "Not Signed";
                        postConstructionWorksheet.Cells[row, 10].Value = !string.IsNullOrEmpty(pc.UnitAreaRep) ? "Signed" : "Not Signed";

                        row++;
                    }

                    // Format
                    postConstructionWorksheet.Cells.AutoFitColumns();
                    var pcHeaderRange = postConstructionWorksheet.Cells[1, 1, 1, pcHeaders.Length];
                    pcHeaderRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    pcHeaderRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    pcHeaderRange.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                }

                // Save the Excel file to a memory stream
                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;

                // Return the Excel file
                string excelName = $"ICRA_Report_{DateTime.Now:yyyyMMdd}.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
            }
        }

        // Helper method to get risk level from preventive measures
        private string GetRiskLevelFromPreventiveMeasures(string preventiveMeasures)
        {
            if (string.IsNullOrEmpty(preventiveMeasures))
                return "Unknown";

            var pm = preventiveMeasures.ToLower();

            if (pm.Contains("5") || pm.Contains("class v"))
                return "Class V (Highest)";
            else if (pm.Contains("4") || pm.Contains("class iv"))
                return "Class IV";
            else if (pm.Contains("3") || pm.Contains("class iii"))
                return "Class III";
            else if (pm.Contains("2") || pm.Contains("class ii"))
                return "Class II";
            else if (pm.Contains("1") || pm.Contains("class i"))
                return "Class I (Lowest)";
            else
                return "Unknown";
        }

        // Enhanced GetPropertyValue method
        private string GetPropertyValue(object obj, string propertyName)
        {
            if (obj == null)
                return "";

            var prop = obj.GetType().GetProperty(propertyName);
            if (prop == null)
                return "";

            try
            {
                var value = prop.GetValue(obj);
                return value?.ToString() ?? "";
            }
            catch
            {
                return "";
            }
        }
    }
}