using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IPCU.Data;
using IPCU.Models;
using IPCU.Services;

namespace IPCU.Controllers
{
    public class ICRADashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public ICRADashboardController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _emailService = emailService;
        }

        // GET: ICRADashboard/Engineering
        // GET: ICRADashboard/Engineering
        [Authorize(Roles = "Engineering")]
        public async Task<IActionResult> Engineering()
        {
            // High risk ICRAs needing Engineering signature
            var highRiskICRAs = await _context.ICRA
                .Where(i => (i.PreventiveMeasures.Contains("3") ||
                            i.PreventiveMeasures.Contains("4") ||
                            i.PreventiveMeasures.Contains("5") ||
                            i.PreventiveMeasures.ToLower().Contains("class iii") ||
                            i.PreventiveMeasures.ToLower().Contains("class iv") ||
                            i.PreventiveMeasures.ToLower().Contains("class v")) &&
                            string.IsNullOrEmpty(i.EngineeringSign))
                .ToListAsync();

            // Low risk ICRAs needing Engineering signature
            var lowRiskICRAs = await _context.ICRA
                .Where(i => (i.PreventiveMeasures.Contains("1") ||
                            i.PreventiveMeasures.Contains("2") ||
                            i.PreventiveMeasures.ToLower().Contains("class i") ||
                            i.PreventiveMeasures.ToLower().Contains("class ii")) &&
                            string.IsNullOrEmpty(i.EngineeringSign))
                .ToListAsync();

            // All ICRAs with Engineering signature
            var completedICRAs = await _context.ICRA
                .Where(i => !string.IsNullOrEmpty(i.EngineeringSign))
                .ToListAsync();

            ViewBag.PendingCount = highRiskICRAs.Count;
            ViewBag.LowRiskCount = lowRiskICRAs.Count;
            ViewBag.CompletedCount = completedICRAs.Count;
            ViewBag.HighRiskICRAs = highRiskICRAs;
            ViewBag.LowRiskICRAs = lowRiskICRAs;
            ViewBag.CompletedICRAs = completedICRAs;

            // Combine high and low risk ICRAs for the model (backward compatibility)
            var allPendingICRAs = highRiskICRAs.Concat(lowRiskICRAs).ToList();

            return View(allPendingICRAs);
        }

        // GET: ICRADashboard/AdminICN
        // GET: ICRADashboard/AdminICN
        [Authorize(Roles = "Admin,ICN")]
        public async Task<IActionResult> AdminICN()
        {
            // High risk ICRAs needing approval
            var highRiskICRAs = await _context.ICRA
                .Where(i => (i.PreventiveMeasures.Contains("3") ||
                            i.PreventiveMeasures.Contains("4") ||
                            i.PreventiveMeasures.Contains("5") ||
                            i.PreventiveMeasures.ToLower().Contains("class iii") ||
                            i.PreventiveMeasures.ToLower().Contains("class iv") ||
                            i.PreventiveMeasures.ToLower().Contains("class v")) &&
                            (string.IsNullOrEmpty(i.ICPSign) || string.IsNullOrEmpty(i.UnitAreaRep)))
                .ToListAsync();

            // Low risk ICRAs (Class I-II only, excluding any with high risk classifications)
            var lowRiskICRAs = await _context.ICRA
                .Where(i => (i.PreventiveMeasures.Contains("1") ||
                            i.PreventiveMeasures.Contains("2") ||
                            i.PreventiveMeasures.ToLower().Contains("class i") ||
                            i.PreventiveMeasures.ToLower().Contains("class ii")) &&
                            !(i.PreventiveMeasures.Contains("3") ||
                            i.PreventiveMeasures.Contains("4") ||
                            i.PreventiveMeasures.Contains("5") ||
                            i.PreventiveMeasures.ToLower().Contains("class iii") ||
                            i.PreventiveMeasures.ToLower().Contains("class iv") ||
                            i.PreventiveMeasures.ToLower().Contains("class v")))
                .ToListAsync();

            // All completed ICRAs (both high and low risk)
            var completedICRAs = await _context.ICRA
                .Where(i => ((i.PreventiveMeasures.Contains("3") ||
                            i.PreventiveMeasures.Contains("4") ||
                            i.PreventiveMeasures.Contains("5") ||
                            i.PreventiveMeasures.ToLower().Contains("class iii") ||
                            i.PreventiveMeasures.ToLower().Contains("class iv") ||
                            i.PreventiveMeasures.ToLower().Contains("class v")) &&
                            !string.IsNullOrEmpty(i.ICPSign) &&
                            !string.IsNullOrEmpty(i.UnitAreaRep) &&
                            !string.IsNullOrEmpty(i.EngineeringSign)) ||
                            ((i.PreventiveMeasures.Contains("1") ||
                            i.PreventiveMeasures.Contains("2") ||
                            i.PreventiveMeasures.ToLower().Contains("class i") ||
                            i.PreventiveMeasures.ToLower().Contains("class ii")) &&
                            !string.IsNullOrEmpty(i.ICPSign)))
                .ToListAsync();

            ViewBag.PendingCount = highRiskICRAs.Count;
            ViewBag.LowRiskCount = lowRiskICRAs.Count;
            ViewBag.CompletedCount = completedICRAs.Count;
            ViewBag.CompletedICRAs = completedICRAs;

            // Send separate lists to the view
            ViewBag.HighRiskICRAs = highRiskICRAs;
            ViewBag.LowRiskICRAs = lowRiskICRAs;

            // Combine all ICRAs for the view for backward compatibility
            var allICRAs = highRiskICRAs.Concat(lowRiskICRAs).ToList();

            return View(allICRAs);
        }

        // GET: ICRADashboard/EngineeringSign/5
        [Authorize(Roles = "Engineering")]
        public async Task<IActionResult> EngineeringSign(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icra = await _context.ICRA.FindAsync(id);
            if (icra == null)
            {
                return NotFound();
            }

            return View(icra);
        }

        // POST: ICRADashboard/EngineeringSign/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Engineering")]
        public async Task<IActionResult> EngineeringSign(int id, [Bind("Id,EngineeringSign")] ICRA signModel)
        {
            if (id != signModel.Id)
            {
                return NotFound();
            }

            // Clear ModelState errors for properties we aren't binding
            foreach (var key in ModelState.Keys.ToList())
            {
                if (key != "Id" && key != "EngineeringSign")
                {
                    ModelState.Remove(key);
                }
            }

            var icra = await _context.ICRA.FindAsync(id);
            if (icra == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update only the Engineering signature
                    icra.EngineeringSign = signModel.EngineeringSign;
                    _context.Update(icra);
                    await _context.SaveChangesAsync();

                    // Notify Admin and ICN roles
                    await _emailService.NotifyRolesAboutICRA(icra, new[] { "Admin", "ICN" });

                    TempData["SuccessMessage"] = "Engineering signature added successfully and notification sent to Admin/ICN.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ICRAExists(signModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Engineering));
            }
            return View(icra);
        }

        // GET: ICRADashboard/AdminICNSign/5
        [Authorize(Roles = "Admin,ICN")]
        public async Task<IActionResult> AdminICNSign(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icra = await _context.ICRA.FindAsync(id);
            if (icra == null)
            {
                return NotFound();
            }

            return View(icra);
        }

        // POST: ICRADashboard/AdminICNSign/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ICN")]
        public async Task<IActionResult> AdminICNSign(int id, [Bind("Id,ICPSign,UnitAreaRep")] ICRA signModel)
        {
            if (id != signModel.Id)
            {
                return NotFound();
            }

            // Clear ModelState errors for properties we aren't binding
            foreach (var key in ModelState.Keys.ToList())
            {
                if (key != "Id" && key != "ICPSign" && key != "UnitAreaRep")
                {
                    ModelState.Remove(key);
                }
            }

            var icra = await _context.ICRA.FindAsync(id);
            if (icra == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update only the ICP and UnitArea signatures
                    icra.ICPSign = signModel.ICPSign;
                    icra.UnitAreaRep = signModel.UnitAreaRep;
                    _context.Update(icra);
                    await _context.SaveChangesAsync();

                    // If Engineering sign is missing, notify them
                    if (string.IsNullOrEmpty(icra.EngineeringSign))
                    {
                        await _emailService.NotifyRolesAboutICRA(icra, new[] { "Engineering" });
                        TempData["SuccessMessage"] = "Signatures added successfully and notification sent to Engineering.";
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Signatures added successfully.";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ICRAExists(signModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AdminICN));
            }
            return View(icra);
        }

        // GET: ICRADashboard/ReviewLowRiskICRA/5
        [Authorize(Roles = "Admin,ICN")]
        public async Task<IActionResult> ReviewLowRiskICRA(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var icra = await _context.ICRA.FindAsync(id);
            if (icra == null)
            {
                return NotFound();
            }

            // Verify this is indeed a low-risk ICRA
            if (!(icra.PreventiveMeasures.Contains("1") ||
                icra.PreventiveMeasures.Contains("2") ||
                icra.PreventiveMeasures.ToLower().Contains("class i") ||
                icra.PreventiveMeasures.ToLower().Contains("class ii")))
            {
                return BadRequest("This is not a low-risk ICRA.");
            }

            return View(icra);
        }

        // POST: ICRADashboard/ReviewLowRiskICRA/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,ICN")]
        public async Task<IActionResult> ReviewLowRiskICRA(int id, [Bind("Id,ICPSign")] ICRA signModel)
        {
            if (id != signModel.Id)
            {
                return NotFound();
            }

            // Clear ModelState errors for properties we aren't binding
            foreach (var key in ModelState.Keys.ToList())
            {
                if (key != "Id" && key != "ICPSign")
                {
                    ModelState.Remove(key);
                }
            }

            var icra = await _context.ICRA.FindAsync(id);
            if (icra == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update only the ICP signature for low-risk ICRAs
                    icra.ICPSign = signModel.ICPSign;
                    _context.Update(icra);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Low-risk ICRA reviewed and acknowledged successfully.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ICRAExists(signModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AdminICN));
            }
            return View(icra);
        }

        private bool ICRAExists(int id)
        {
            return _context.ICRA.Any(e => e.Id == id);
        }
    }
}