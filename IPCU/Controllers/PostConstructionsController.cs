using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using IPCU.Data;
using IPCU.Models;
using IPCU.Services;

namespace IPCU.Controllers
{
    public class PostConstructionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public PostConstructionsController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: PostConstructions
        public async Task<IActionResult> Index(int? icraId)
        {
            // If an ICRA ID is provided, filter post constructions by that ICRA
            if (icraId.HasValue)
            {
                var icra = await _context.ICRA.FindAsync(icraId.Value);
                if (icra != null)
                {
                    ViewBag.ICRA = icra;
                    return View(await _context.PostConstruction.Where(p => p.ICRAId == icraId.Value).ToListAsync());
                }
            }

            // Otherwise, return all post constructions
            return View(await _context.PostConstruction.ToListAsync());
        }

        // GET: PostConstructions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postConstruction = await _context.PostConstruction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postConstruction == null)
            {
                return NotFound();
            }

            return View(postConstruction);
        }

        // GET: PostConstructions/Create
        [Authorize(Roles = "Admin,ICN,Engineering")]
        public async Task<IActionResult> Create(int? icraId)
        {
            // If an ICRA ID is provided, use it to pre-populate the form
            if (icraId.HasValue)
            {
                var icra = await _context.ICRA.FindAsync(icraId.Value);
                if (icra == null)
                {
                    return NotFound();
                }

                // Check if the ICRA has at least one checklist
                var hasChecklists = await _context.TCSkillsChecklist
                    .AnyAsync(c => c.ICRAId == icraId.Value);

                if (!hasChecklists)
                {
                    TempData["ErrorMessage"] = "Cannot create a post-construction form for an ICRA without any checklists. Please create at least one checklist first.";
                    return RedirectToAction("ICRAChecklists", "ICRADashboard", new { id = icraId });
                }

                // Check if a post-construction form already exists for this ICRA
                var existingForm = await _context.PostConstruction
                    .FirstOrDefaultAsync(p => p.ICRAId == icraId.Value);

                if (existingForm != null)
                {
                    return RedirectToAction(nameof(Edit), new { id = existingForm.Id });
                }

                // Create a new post-construction form pre-populated with ICRA data
                var postConstruction = new PostConstruction
                {
                    ICRAId = icra.Id,
                    ProjectReferenceNumber = icra.ProjectReferenceNumber,
                    ProjectNameAndDescription = icra.ProjectNameAndDescription,
                    SpecificSiteOfActivity = icra.SpecificSiteOfActivity,
                    ProjectStartDate = icra.ProjectStartDate,
                    EstimatedDuration = icra.EstimatedDuration
                };

                return View(postConstruction);
            }

            // If no ICRA ID provided, return a blank form
            // Provide a dropdown to select from ICRAs with at least one checklist
            var icrasWithChecklists = await _context.ICRA
                .Where(i => _context.TCSkillsChecklist.Any(c => c.ICRAId == i.Id))
                .ToListAsync();

            ViewBag.ICRAs = new SelectList(icrasWithChecklists, "Id", "ProjectReferenceNumber");

            return View(new PostConstruction());
        }
        // POST: PostConstructions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ICN,Engineering")]
        public async Task<IActionResult> Create([Bind("Id,ICRAId,ProjectReferenceNumber,ProjectNameAndDescription," +
    "SpecificSiteOfActivity,ProjectStartDate,EstimatedDuration," +
    "BeforeHoarding,BeforeHoardingDC,FacilityBased,FacilityBasedDC,AfterRemoval,AfterRemovalDC,WhereRequired,WhereRequiredlDC," +
    "AreaIs,AreaIsDC,IntegrityofWalls,IntegrityofWallsDC,SurfaceinPatient,SurfaceinPatientDC,AreaSurfaces,AreaSurfacesDC," +
    "IfPlumbinghasbeenAffected,IfPlumbinghasbeenAffectedDC,PlumbingifAffected,PlumbingifAffectedDC," +
    "CorrectHandWashing,CorrectHandWashingDC,FaucetAerators,FaucetAeratorsDC," +
    "CeilingTiles,CeilingTilesDC,HVACSystems,HVACSystemsDC,CorrectRoomPressurization,CorrectRoomPressurizationDC," +
    "AllMechanicalSpaces,AllMechanicalSpacesDC,DateCompleted,ContractorSign,EngineeringSign,ICPSign,UnitAreaRep")] PostConstruction postConstruction)
        {
            // Debug information - check what values are missing
            var missingRequiredFields = new List<string>();

            // Check required radio button selections
            if (string.IsNullOrEmpty(postConstruction.BeforeHoarding)) missingRequiredFields.Add("Before Hoarding Removal");
            if (string.IsNullOrEmpty(postConstruction.FacilityBased)) missingRequiredFields.Add("Facility Based Cleaning");
            if (string.IsNullOrEmpty(postConstruction.AfterRemoval)) missingRequiredFields.Add("After Removal of Hoarding");
            if (string.IsNullOrEmpty(postConstruction.WhereRequired)) missingRequiredFields.Add("HVAC Duct Work Cleaning");
            if (string.IsNullOrEmpty(postConstruction.AreaIs)) missingRequiredFields.Add("Area Is Dust Free");
            if (string.IsNullOrEmpty(postConstruction.IntegrityofWalls)) missingRequiredFields.Add("Integrity of Walls");
            if (string.IsNullOrEmpty(postConstruction.SurfaceinPatient)) missingRequiredFields.Add("Surfaces in Patient Care");
            if (string.IsNullOrEmpty(postConstruction.AreaSurfaces)) missingRequiredFields.Add("Area Surfaces");
            if (string.IsNullOrEmpty(postConstruction.IfPlumbinghasbeenAffected)) missingRequiredFields.Add("If Plumbing Has Been Affected");
            if (string.IsNullOrEmpty(postConstruction.PlumbingifAffected)) missingRequiredFields.Add("Plumbing If Affected");
            if (string.IsNullOrEmpty(postConstruction.CorrectHandWashing)) missingRequiredFields.Add("Correct Hand Washing");
            if (string.IsNullOrEmpty(postConstruction.FaucetAerators)) missingRequiredFields.Add("Faucet Aerators");
            if (string.IsNullOrEmpty(postConstruction.CeilingTiles)) missingRequiredFields.Add("Ceiling Tiles");
            if (string.IsNullOrEmpty(postConstruction.HVACSystems)) missingRequiredFields.Add("HVAC Systems");
            if (string.IsNullOrEmpty(postConstruction.CorrectRoomPressurization)) missingRequiredFields.Add("Correct Room Pressurization");
            if (string.IsNullOrEmpty(postConstruction.AllMechanicalSpaces)) missingRequiredFields.Add("All Mechanical Spaces");

            // Check other required fields
            if (string.IsNullOrEmpty(postConstruction.ProjectReferenceNumber)) missingRequiredFields.Add("Project Reference Number");
            if (string.IsNullOrEmpty(postConstruction.ProjectNameAndDescription)) missingRequiredFields.Add("Project Name and Description");
            if (string.IsNullOrEmpty(postConstruction.SpecificSiteOfActivity)) missingRequiredFields.Add("Specific Site of Activity");
            if (postConstruction.ProjectStartDate == null) missingRequiredFields.Add("Project Start Date");
            if (string.IsNullOrEmpty(postConstruction.EstimatedDuration)) missingRequiredFields.Add("Estimated Duration");

            // If any required fields are missing, add them to ModelState
            if (missingRequiredFields.Any())
            {
                ModelState.AddModelError("", $"The following required fields are missing: {string.Join(", ", missingRequiredFields)}");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Log the state of the object being saved
                    Console.WriteLine($"Saving PostConstruction: ICRAId={postConstruction.ICRAId}, " +
                                       $"ProjectRef={postConstruction.ProjectReferenceNumber}, " +
                                       $"BeforeHoarding={postConstruction.BeforeHoarding}");

                    // Find the ICRA and attach it to the context
                    if (postConstruction.ICRAId > 0)
                    {
                        // Set the ICRA to null to avoid validation issues
                        postConstruction.ICRA = null;
                    }

                    _context.Add(postConstruction);
                    await _context.SaveChangesAsync();

                    // Get the associated ICRA to notify relevant parties
                    var icra = await _context.ICRA.FindAsync(postConstruction.ICRAId);

                    if (icra != null)
                    {
                        // Notify appropriate roles about the post-construction form
                        await _emailService.NotifyRolesAboutICRA(icra, new[] { "Admin", "ICN", "Engineering" });
                    }

                    TempData["SuccessMessage"] = "Post-construction form created successfully.";
                    return RedirectToAction("ICRAChecklists", "ICRADashboard", new { id = postConstruction.ICRAId });
                }
                catch (Exception ex)
                {
                    // Log the exception details
                    Console.WriteLine($"Exception when saving PostConstruction: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    }

                    // Add detailed error message to ModelState
                    ModelState.AddModelError("", $"Unable to save changes: {ex.Message}");

                    // If there's an inner exception, add it too
                    if (ex.InnerException != null)
                    {
                        ModelState.AddModelError("", $"Details: {ex.InnerException.Message}");
                    }
                }
            }
            else
            {
                // Add all validation errors to ModelState for debugging
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { Key = x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage).ToList() })
                    .ToList();

                // Format and display errors in a clear way
                foreach (var error in errors)
                {
                    // If the key is empty, it's a model-level error
                    if (string.IsNullOrEmpty(error.Key))
                    {
                        foreach (var message in error.Errors)
                        {
                            ModelState.AddModelError("", message);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Field: {error.Key}, Errors: {string.Join(", ", error.Errors)}");
                    }
                }
            }

            // If we get here, something went wrong - repopulate ICRA data for the view
            if (postConstruction.ICRAId > 0)
            {
                var icra = _context.ICRA.Find(postConstruction.ICRAId);
                if (icra != null)
                {
                    ViewBag.ICRANumber = icra.ProjectReferenceNumber;
                }
            }

            // Pass missing fields to the view for better user feedback
            ViewBag.MissingFields = missingRequiredFields.Any() ? string.Join(", ", missingRequiredFields) : null;

            return View(postConstruction);
        }

        // GET: PostConstructions/Edit/5
        [Authorize(Roles = "Admin,ICN,Engineering")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postConstruction = await _context.PostConstruction.FindAsync(id);
            if (postConstruction == null)
            {
                return NotFound();
            }

            // Get the associated ICRA for display
            if (postConstruction.ICRAId > 0)
            {
                var icra = await _context.ICRA.FindAsync(postConstruction.ICRAId);
                if (icra != null)
                {
                    ViewBag.ICRANumber = icra.ProjectReferenceNumber;
                }
            }

            return View(postConstruction);
        }

        // POST: PostConstructions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ICN,Engineering")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ICRAId,ProjectReferenceNumber,ProjectNameAndDescription," +
            "SpecificSiteOfActivity,ProjectStartDate,EstimatedDuration," +
            "BeforeHoarding,BeforeHoardingDC,FacilityBased,FacilityBasedDC,AfterRemoval,AfterRemovalDC,WhereRequired,WhereRequiredlDC," +
            "AreaIs,AreaIsDC,IntegrityofWalls,IntegrityofWallsDC,SurfaceinPatient,SurfaceinPatientDC,AreaSurfaces,AreaSurfacesDC," +
            "IfPlumbinghasbeenAffected,IfPlumbinghasbeenAffectedDC,PlumbingifAffected,PlumbingifAffectedDC," +
            "CorrectHandWashing,CorrectHandWashingDC,FaucetAerators,FaucetAeratorsDC," +
            "CeilingTiles,CeilingTilesDC,HVACSystems,HVACSystemsDC,CorrectRoomPressurization,CorrectRoomPressurizationDC," +
            "AllMechanicalSpaces,AllMechanicalSpacesDC,DateCompleted,ContractorSign,EngineeringSign,ICPSign,UnitAreaRep")] PostConstruction postConstruction)
        {
            if (id != postConstruction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postConstruction);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Post-construction form updated successfully.";
                    return RedirectToAction("ICRAChecklists", "ICRADashboard", new { id = postConstruction.ICRAId });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostConstructionExists(postConstruction.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // If we get here, something went wrong - repopulate ICRA data for the view
            if (postConstruction.ICRAId > 0)
            {
                var icra = _context.ICRA.Find(postConstruction.ICRAId);
                if (icra != null)
                {
                    ViewBag.ICRANumber = icra.ProjectReferenceNumber;
                }
            }

            return View(postConstruction);
        }

        // GET: PostConstructions/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postConstruction = await _context.PostConstruction
                .FirstOrDefaultAsync(m => m.Id == id);
            if (postConstruction == null)
            {
                return NotFound();
            }

            return View(postConstruction);
        }

        // POST: PostConstructions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var postConstruction = await _context.PostConstruction.FindAsync(id);
            if (postConstruction == null)
            {
                return NotFound();
            }

            var icraId = postConstruction.ICRAId;
            _context.PostConstruction.Remove(postConstruction);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Post-construction form deleted successfully.";
            return RedirectToAction("ICRAChecklists", "ICRADashboard", new { id = icraId });
        }

        // GET: PostConstructions/Sign/5
        [Authorize(Roles = "Admin,ICN,Engineering")]
        public async Task<IActionResult> Sign(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var postConstruction = await _context.PostConstruction.FindAsync(id);
            if (postConstruction == null)
            {
                return NotFound();
            }

            return View(postConstruction);
        }

        // POST: PostConstructions/Sign/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ICN,Engineering")]
        public async Task<IActionResult> Sign(int id)
        {
            var postConstruction = await _context.PostConstruction.FindAsync(id);
            if (postConstruction == null)
            {
                return NotFound();
            }

            // Update only the signature fields based on user role
            if (User.IsInRole("Engineering"))
            {
                await TryUpdateModelAsync(postConstruction, "", p => p.EngineeringSign);
            }
            else if (User.IsInRole("Admin") || User.IsInRole("ICN"))
            {
                await TryUpdateModelAsync(postConstruction, "", p => p.ICPSign, p => p.UnitAreaRep);
            }

            try
            {
                postConstruction.DateCompleted = DateTime.Now;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Signature added successfully.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostConstructionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction("ICRAChecklists", "ICRADashboard", new { id = postConstruction.ICRAId });
        }

        private bool PostConstructionExists(int id)
        {
            return _context.PostConstruction.Any(e => e.Id == id);
        }
    }
}