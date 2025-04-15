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
        public async Task<IActionResult> EngineeringSign(int id)
        {
            var icra = await _context.ICRA.FindAsync(id);
            if (icra == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync(icra, "",
                x => x.EngineeringSign,
                x => x.RiskGroup_Below, x => x.RiskGroup_Above, x => x.RiskGroup_Lateral, x => x.RiskGroup_Behind, x => x.RiskGroup_Front,
                x => x.LocalNumber_Below, x => x.LocalNumber_Above, x => x.LocalNumber_Lateral, x => x.LocalNumber_Behind, x => x.LocalNumber_Front,
                x => x.Below_Noise, x => x.Below_Vibration, x => x.Below_Dust, x => x.Below_Ventilation, x => x.Below_Pressuraztion, x => x.Below_Data,
                x => x.Below_Mechanical, x => x.Below_MedicalGas, x => x.Below_HotColdWater, x => x.Below_Other,
                x => x.Above_Noise, x => x.Above_Vibration, x => x.Above_Dust, x => x.Above_Ventilation, x => x.Above_Pressuraztion, x => x.Above_Data,
                x => x.Above_Mechanical, x => x.Above_MedicalGas, x => x.Above_HotColdWater, x => x.Above_Other,
                x => x.Lateral_Noise, x => x.Lateral_Vibration, x => x.Lateral_Dust, x => x.Lateral_Ventilation, x => x.Lateral_Pressuraztion, x => x.Lateral_Data,
                x => x.Lateral_Mechanical, x => x.Lateral_MedicalGas, x => x.Lateral_HotColdWater, x => x.Lateral_Other,
                x => x.Behind_Noise, x => x.Behind_Vibration, x => x.Behind_Dust, x => x.Behind_Ventilation, x => x.Behind_Pressuraztion, x => x.Behind_Data,
                x => x.Behind_Mechanical, x => x.Behind_MedicalGas, x => x.Behind_HotColdWater, x => x.Behind_Other,
                x => x.Front_Noise, x => x.Front_Vibration, x => x.Front_Dust, x => x.Front_Ventilation, x => x.Front_Pressuraztion, x => x.Front_Data,
                x => x.Front_Mechanical, x => x.Front_MedicalGas, x => x.Front_HotColdWater, x => x.Front_Other,
                x => x.RiskofWater, x => x.Remarks_RiskofWater, x => x.ShouldWork, x => x.Remarks_ShouldWork,
                x => x.CanSupplyAir, x => x.Remarks_CanSupplyAir, x => x.HaveTraffic, x => x.Remarks_HaveTraffic,
                x => x.CanPatientCare, x => x.Remarks_CanPatientCare, x => x.AreMeasures, x => x.Remarks_AreMeasures,
                x => x.AdditionalComments))
            {
                try
                {
                    await _context.SaveChangesAsync();

                    // Notify Admin and ICN after successful save
                    await _emailService.NotifyRolesAboutICRA(icra, new[] { "Admin", "ICN" });

                    TempData["SuccessMessage"] = "Engineering signature and ICRA data updated successfully.";
                    return RedirectToAction(nameof(Engineering));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ICRAExists(id))
                        return NotFound();
                    else
                        throw;
                }
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
        public async Task<IActionResult> AdminICNSign(int id)
        {
            var icra = await _context.ICRA.FindAsync(id);
            if (icra == null)
            {
                return NotFound();
            }

            // Try to update only the allowed fields from the posted form data
            if (await TryUpdateModelAsync(icra, "",
                x => x.RiskGroup_Below, x => x.RiskGroup_Above, x => x.RiskGroup_Lateral, x => x.RiskGroup_Behind, x => x.RiskGroup_Front,
                x => x.LocalNumber_Below, x => x.LocalNumber_Above, x => x.LocalNumber_Lateral, x => x.LocalNumber_Behind, x => x.LocalNumber_Front,
                x => x.Below_Noise, x => x.Below_Vibration, x => x.Below_Dust, x => x.Below_Ventilation, x => x.Below_Pressuraztion, x => x.Below_Data,
                x => x.Below_Mechanical, x => x.Below_MedicalGas, x => x.Below_HotColdWater, x => x.Below_Other,
                x => x.Above_Noise, x => x.Above_Vibration, x => x.Above_Dust, x => x.Above_Ventilation, x => x.Above_Pressuraztion, x => x.Above_Data,
                x => x.Above_Mechanical, x => x.Above_MedicalGas, x => x.Above_HotColdWater, x => x.Above_Other,
                x => x.Lateral_Noise, x => x.Lateral_Vibration, x => x.Lateral_Dust, x => x.Lateral_Ventilation, x => x.Lateral_Pressuraztion, x => x.Lateral_Data,
                x => x.Lateral_Mechanical, x => x.Lateral_MedicalGas, x => x.Lateral_HotColdWater, x => x.Lateral_Other,
                x => x.Behind_Noise, x => x.Behind_Vibration, x => x.Behind_Dust, x => x.Behind_Ventilation, x => x.Behind_Pressuraztion, x => x.Behind_Data,
                x => x.Behind_Mechanical, x => x.Behind_MedicalGas, x => x.Behind_HotColdWater, x => x.Behind_Other,
                x => x.Front_Noise, x => x.Front_Vibration, x => x.Front_Dust, x => x.Front_Ventilation, x => x.Front_Pressuraztion, x => x.Front_Data,
                x => x.Front_Mechanical, x => x.Front_MedicalGas, x => x.Front_HotColdWater, x => x.Front_Other,
                x => x.RiskofWater, x => x.Remarks_RiskofWater, x => x.ShouldWork, x => x.Remarks_ShouldWork,
                x => x.CanSupplyAir, x => x.Remarks_CanSupplyAir, x => x.HaveTraffic, x => x.Remarks_HaveTraffic,
                x => x.CanPatientCare, x => x.Remarks_CanPatientCare, x => x.AreMeasures, x => x.Remarks_AreMeasures,
                x => x.AdditionalComments, x => x.ICPSign, x => x.UnitAreaRep))
            {
                try
                {
                    await _context.SaveChangesAsync();

                    if (string.IsNullOrEmpty(icra.EngineeringSign))
                    {
                        await _emailService.NotifyRolesAboutICRA(icra, new[] { "Engineering" });
                        TempData["SuccessMessage"] = "Signatures added successfully and notification sent to Engineering.";
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Signatures added successfully.";
                    }

                    return RedirectToAction(nameof(AdminICN));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ICRAExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
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
        public async Task<IActionResult> ReviewLowRiskICRA(int id)
        {
            var icra = await _context.ICRA.FindAsync(id);
            if (icra == null)
            {
                return NotFound();
            }

            // Try to update only the allowed fields from the posted form data
            if (await TryUpdateModelAsync(icra, "",
                x => x.RiskGroup_Below, x => x.RiskGroup_Above, x => x.RiskGroup_Lateral, x => x.RiskGroup_Behind, x => x.RiskGroup_Front,
                x => x.LocalNumber_Below, x => x.LocalNumber_Above, x => x.LocalNumber_Lateral, x => x.LocalNumber_Behind, x => x.LocalNumber_Front,
                x => x.Below_Noise, x => x.Below_Vibration, x => x.Below_Dust, x => x.Below_Ventilation, x => x.Below_Pressuraztion, x => x.Below_Data,
                x => x.Below_Mechanical, x => x.Below_MedicalGas, x => x.Below_HotColdWater, x => x.Below_Other,
                x => x.Above_Noise, x => x.Above_Vibration, x => x.Above_Dust, x => x.Above_Ventilation, x => x.Above_Pressuraztion, x => x.Above_Data,
                x => x.Above_Mechanical, x => x.Above_MedicalGas, x => x.Above_HotColdWater, x => x.Above_Other,
                x => x.Lateral_Noise, x => x.Lateral_Vibration, x => x.Lateral_Dust, x => x.Lateral_Ventilation, x => x.Lateral_Pressuraztion, x => x.Lateral_Data,
                x => x.Lateral_Mechanical, x => x.Lateral_MedicalGas, x => x.Lateral_HotColdWater, x => x.Lateral_Other,
                x => x.Behind_Noise, x => x.Behind_Vibration, x => x.Behind_Dust, x => x.Behind_Ventilation, x => x.Behind_Pressuraztion, x => x.Behind_Data,
                x => x.Behind_Mechanical, x => x.Behind_MedicalGas, x => x.Behind_HotColdWater, x => x.Behind_Other,
                x => x.Front_Noise, x => x.Front_Vibration, x => x.Front_Dust, x => x.Front_Ventilation, x => x.Front_Pressuraztion, x => x.Front_Data,
                x => x.Front_Mechanical, x => x.Front_MedicalGas, x => x.Front_HotColdWater, x => x.Front_Other,
                x => x.RiskofWater, x => x.Remarks_RiskofWater, x => x.ShouldWork, x => x.Remarks_ShouldWork,
                x => x.CanSupplyAir, x => x.Remarks_CanSupplyAir, x => x.HaveTraffic, x => x.Remarks_HaveTraffic,
                x => x.CanPatientCare, x => x.Remarks_CanPatientCare, x => x.AreMeasures, x => x.Remarks_AreMeasures,
                x => x.AdditionalComments, x => x.ICPSign))
            {
                try
                {
                    await _context.SaveChangesAsync();

                    if (string.IsNullOrEmpty(icra.EngineeringSign))
                    {
                        await _emailService.NotifyRolesAboutICRA(icra, new[] { "Engineering" });
                        TempData["SuccessMessage"] = "Signatures added successfully and notification sent to Engineering.";
                    }
                    else
                    {
                        TempData["SuccessMessage"] = "Signatures added successfully.";
                    }

                    return RedirectToAction(nameof(AdminICN));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ICRAExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(icra);
        }

        private bool ICRAExists(int id)
        {
            return _context.ICRA.Any(e => e.Id == id);
        }
    }
}