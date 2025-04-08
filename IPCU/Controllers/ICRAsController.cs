using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPCU.Data;
using IPCU.Models;

namespace IPCU.Controllers
{
    public class ICRAsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ICRAsController(ApplicationDbContext context)
        {
            _context = context;
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProjectReferenceNumber,ProjectNameAndDescription,ContractorRepresentativeName,TelephoneOrMobileNumber,SpecificSiteOfActivity,ScopeOfWork,ProjectStartDate,EstimatedDuration,ConstructionType,PatientRiskGroup,PreventiveMeasures,RiskGroup,LocalNumber,Below_Noise,Below_Vibration,Below_Dust,Below_Ventilation,Below_Pressuraztion,Below_Data,Below_Mechanical,Below_MedicalGas,Below_HotColdWater,Below_Other,Above_Noise,Above_Vibration,Above_Dust,Above_Ventilation,Above_Pressuraztion,Above_Data,Above_Mechanical,Above_MedicalGas,Above_HotColdWater,Above_Other,Lateral_Noise,Lateral_Vibration,Lateral_Dust,Lateral_Ventilation,Lateral_Pressuraztion,Lateral_Data,Lateral_Mechanical,Lateral_MedicalGas,Lateral_HotColdWater,Lateral_Other,Behind_Noise,Behind_Vibration,Behind_Dust,Behind_Ventilation,Behind_Pressuraztion,Behind_Data,Behind_Mechanical,Behind_MedicalGas,Behind_HotColdWater,Behind_Other,Front_Noise,Front_Vibration,Front_Dust,Front_Ventilation,Front_Pressuraztion,Front_Data,Front_Mechanical,Front_MedicalGas,Front_HotColdWater,Front_Other,RiskofWater,Remarks_RiskofWater,ShouldWork,Remarks_ShouldWork,CanSupplyAir,Remarks_CanSupplyAir,HaveTraffic,Remarks_HaveTraffic,CanPatientCare,Remarks_CanPatientCare,AreMeasures,Remarks_AreMeasures,AdditionalComments,ContractorSign,EngineeringSign,ICPSign,UnitAreaRep")] ICRA iCRA)
        {
            if (ModelState.IsValid)
            {
                _context.Add(iCRA);
                await _context.SaveChangesAsync();
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                    _context.Update(iCRA);
                    await _context.SaveChangesAsync();
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
    }
}
