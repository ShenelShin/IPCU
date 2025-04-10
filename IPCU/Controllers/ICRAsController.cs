using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPCU.Data;
using IPCU.Models;
using IPCU.Services;

namespace IPCU.Controllers
{
    public class ICRAsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public ICRAsController(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: ICRAs
        public async Task<IActionResult> Index()
        {
            return View(await _context.ICRA.ToListAsync());
        }

        // GET: ICRAs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iCRA = await _context.ICRA
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iCRA == null)
            {
                return NotFound();
            }

            return View(iCRA);
        }

        // GET: ICRAs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ICRAs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectReferenceNumber,ConstructionEmail,ProjectNameAndDescription,ContractorRepresentativeName,TelephoneOrMobileNumber,SpecificSiteOfActivity,ScopeOfWork,ProjectStartDate,EstimatedDuration,ConstructionType,PatientRiskGroup,PreventiveMeasures,RiskGroup,LocalNumber,Below_Noise,Below_Vibration,Below_Dust,Below_Ventilation,Below_Pressuraztion,Below_Data,Below_Mechanical,Below_MedicalGas,Below_HotColdWater,Below_Other,Above_Noise,Above_Vibration,Above_Dust,Above_Ventilation,Above_Pressuraztion,Above_Data,Above_Mechanical,Above_MedicalGas,Above_HotColdWater,Above_Other,Lateral_Noise,Lateral_Vibration,Lateral_Dust,Lateral_Ventilation,Lateral_Pressuraztion,Lateral_Data,Lateral_Mechanical,Lateral_MedicalGas,Lateral_HotColdWater,Lateral_Other,Behind_Noise,Behind_Vibration,Behind_Dust,Behind_Ventilation,Behind_Pressuraztion,Behind_Data,Behind_Mechanical,Behind_MedicalGas,Behind_HotColdWater,Behind_Other,Front_Noise,Front_Vibration,Front_Dust,Front_Ventilation,Front_Pressuraztion,Front_Data,Front_Mechanical,Front_MedicalGas,Front_HotColdWater,Front_Other,RiskofWater,Remarks_RiskofWater,ShouldWork,Remarks_ShouldWork,CanSupplyAir,Remarks_CanSupplyAir,HaveTraffic,Remarks_HaveTraffic,CanPatientCare,Remarks_CanPatientCare,AreMeasures,Remarks_AreMeasures,AdditionalComments,ContractorSign,EngineeringSign,ICPSign,UnitAreaRep")] ICRA iCRA)
        {
            if (ModelState.IsValid)
            {
                _context.Add(iCRA);
                await _context.SaveChangesAsync();

                // Check if preventive measures require notification
                await CheckAndNotifyHighRiskICRA(iCRA);

                return RedirectToAction(nameof(Index));
            }
            return View(iCRA);
        }

        // GET: ICRAs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iCRA = await _context.ICRA.FindAsync(id);
            if (iCRA == null)
            {
                return NotFound();
            }
            return View(iCRA);
        }

        // POST: ICRAs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProjectReferenceNumber,ProjectNameAndDescription,ContractorRepresentativeName,TelephoneOrMobileNumber,SpecificSiteOfActivity,ScopeOfWork,ProjectStartDate,EstimatedDuration,ConstructionType,PatientRiskGroup,PreventiveMeasures,RiskGroup,LocalNumber,Below_Noise,Below_Vibration,Below_Dust,Below_Ventilation,Below_Pressuraztion,Below_Data,Below_Mechanical,Below_MedicalGas,Below_HotColdWater,Below_Other,Above_Noise,Above_Vibration,Above_Dust,Above_Ventilation,Above_Pressuraztion,Above_Data,Above_Mechanical,Above_MedicalGas,Above_HotColdWater,Above_Other,Lateral_Noise,Lateral_Vibration,Lateral_Dust,Lateral_Ventilation,Lateral_Pressuraztion,Lateral_Data,Lateral_Mechanical,Lateral_MedicalGas,Lateral_HotColdWater,Lateral_Other,Behind_Noise,Behind_Vibration,Behind_Dust,Behind_Ventilation,Behind_Pressuraztion,Behind_Data,Behind_Mechanical,Behind_MedicalGas,Behind_HotColdWater,Behind_Other,Front_Noise,Front_Vibration,Front_Dust,Front_Ventilation,Front_Pressuraztion,Front_Data,Front_Mechanical,Front_MedicalGas,Front_HotColdWater,Front_Other,RiskofWater,Remarks_RiskofWater,ShouldWork,Remarks_ShouldWork,CanSupplyAir,Remarks_CanSupplyAir,HaveTraffic,Remarks_HaveTraffic,CanPatientCare,Remarks_CanPatientCare,AreMeasures,Remarks_AreMeasures,AdditionalComments,ContractorSign,EngineeringSign,ICPSign,UnitAreaRep")] ICRA iCRA)
        {
            if (id != iCRA.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Get original ICRA to compare
                    var originalICRA = await _context.ICRA.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

                    _context.Update(iCRA);
                    await _context.SaveChangesAsync();

                    // Check if preventive measures were changed to Class 3 or higher
                    if (originalICRA != null &&
                        !IsHighRiskClass(originalICRA.PreventiveMeasures) &&
                        IsHighRiskClass(iCRA.PreventiveMeasures))
                    {
                        await CheckAndNotifyHighRiskICRA(iCRA);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ICRAExists(iCRA.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(iCRA);
        }

        // GET: ICRAs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var iCRA = await _context.ICRA
                .FirstOrDefaultAsync(m => m.Id == id);
            if (iCRA == null)
            {
                return NotFound();
            }

            return View(iCRA);
        }

        // POST: ICRAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var iCRA = await _context.ICRA.FindAsync(id);
            if (iCRA != null)
            {
                _context.ICRA.Remove(iCRA);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ICRAExists(int id)
        {
            return _context.ICRA.Any(e => e.Id == id);
        }

        // Helper method to check if preventive measures class is 3 or above
        private bool IsHighRiskClass(string preventiveMeasures)
        {
            if (!string.IsNullOrEmpty(preventiveMeasures))
            {
                // Check if the string contains a number 3 or higher
                if (preventiveMeasures.Contains("3") ||
                    preventiveMeasures.Contains("4") ||
                    preventiveMeasures.Contains("5") ||
                    preventiveMeasures.ToLower().Contains("class iii") ||
                    preventiveMeasures.ToLower().Contains("class iv") ||
                    preventiveMeasures.ToLower().Contains("class v"))
                {
                    return true;
                }
            }
            return false;
        }

        // Helper method to send notifications for high risk ICRAs
        private async Task CheckAndNotifyHighRiskICRA(ICRA icra)
        {
            if (IsHighRiskClass(icra.PreventiveMeasures))
            {
                try
                {
                    // Notify Engineering and Admin roles
                    await _emailService.NotifyRolesAboutICRA(icra, new[] { "Engineering", "Admin" });

                    // Optional: Log that notification was sent
                    // _logger.LogInformation($"High risk ICRA notification sent for Project {icra.ProjectReferenceNumber}");
                }
                catch (Exception ex)
                {
                    // Handle or log the exception
                    // _logger.LogError($"Failed to send ICRA notification: {ex.Message}");
                }
            }
        }
    }
}