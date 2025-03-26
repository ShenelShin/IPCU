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
    public class PneumoniasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PneumoniasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pneumonias
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pneumonias.ToListAsync());
        }

        // GET: Pneumonias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pneumonia = await _context.Pneumonias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pneumonia == null)
            {
                return NotFound();
            }

            return View(pneumonia);
        }

        // GET: Pneumonias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pneumonias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Fname, Lname, Mname, HospitalNumber, DateOfBirth, Age, UnitWardArea, MainService, DateOfEvent, Investigator, DateOfAdmission, Disposition, DispositionDate, DispositionTransfer, Gender, Classification, MDRO, TypeClass, PNEU_Subclass, PersistentorProgressive1_1, Fever1_1, Leukopenia1_1, Leukocytosis1_1, Adults70old1_1, NewPurulentSputum1_1, WorseningCoughOrDyspnea1_1, RalesOrBronchialBreathSounds1_1, WorseningGasExchange1_1, PersistentorProgressive1_2, WorseningGasExchange1_2, TemperatureInstability1_2, LeukopeniaOrLeukocytosis1_2, NewPurulentSputum1_2, ApneaOrTachypneaOrNasalFlaring1_2, WheezingRalesOrRhonchi1_2, Cough1_2, BradycardiaOrTachycardia1_2, PersistentorProgressive1_3, Fever1_3, Leukopenia1_3, NewPurulentSputum1_3, NewWorseningCough1_3, RalesorBronchial1_3, WorseningGas1_3, PersistentorProgressive2_1, Fever2_1, Leukopenia12_1, Leukocytosis2_1, Adults70old2_1, NewPurulentSputum2_1, WorseningCoughOrDyspnea2_1, RalesOrBronchialBreathSounds2_1, WorseningGasExchange2_1, OrganismIdentifiedFromBlood_2_1, PositiveQuantitativeCultureLRT_2_1, BALCellsContainIntracellularBacteria_2_1, PositiveQuantitativeCultureLungTissue_2_1, HistopathologicExam2_1, AbscessFormationOrFoci_2_1, EvidenceOfFungalInvasion_2_1, CultureDate2_1, CultureResults2_1, PersistentorProgressive2_2, Fever2_2, Leukopenia12_2, Leukocytosis2_2, Adults70old2_2, NewPurulentSputum2_2, WorseningCoughOrDyspnea2_2, RalesOrBronchialBreathSounds2_2, WorseningGasExchange2_2, VirusBordetellaLegionella2_2, FourfoldrisePaired2_2, FourfoldriseLegionella2_2, DetectionofLegionella2_2, CultureDate2_2, CultureResults2_2, PersistentorProgressive3, Fever3, Adults70old3, NewPurulentSputum3, WorseningCoughOrDyspnea3, RalesOrBronchialBreathSounds3, WorseningGasExchange3, Hemoptysis3, PleuriticChestPain3, IdentificationCandida3, EvidenceofFungi3, DirectMicroscopicExam3, PositiveCultureFungi3, NonCultureDiagnostic3")] Pneumonia pneumonia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pneumonia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pneumonia);
        }

        // GET: Pneumonias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pneumonia = await _context.Pneumonias.FindAsync(id);
            if (pneumonia == null)
            {
                return NotFound();
            }
            return View(pneumonia);
        }

        // POST: Pneumonias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fname,Lname,Mname,HospitalNumber,DateOfBirth,Age,UnitWardArea,MainService,DateOfEvent,Investigator,DateOfAdmission,Disposition,DispositionDate,DispositionTransfer,Gender,Classification,MDRO,MDROOrganism,TypeClass,PNEU_Subclass,PersistentorProgressive,Fever1_1,Leukopenia1_1,Leukocytosis1_1,Adults70old1_1,NewPurulentSputum1_1,WorseningCoughOrDyspnea1_1,RalesOrBronchialBreathSounds1_1,WorseningGasExchange1_1,WorseningGasExchange1_2,TemperatureInstability1_2,LeukopeniaOrLeukocytosis1_2,NewPurulentSputum1_2,ApneaOrTachypneaOrNasalFlaring1_2,WheezingRalesOrRhonchi1_2,Cough1_2,BradycardiaOrTachycardia1_2,Fever1_3,Leukopenia1_3,NewPurulentSputum1_3,NewWorseningCough1_3,RalesorBronchial1_3,WorseningGas1_3,Fever2_1,Leukopenia12_1,Leukocytosis2_1,Adults70old2_1,NewPurulentSputum2_1,WorseningCoughOrDyspnea2_1,RalesOrBronchialBreathSounds2_1,WorseningGasExchange2_1,OrganismIdentifiedFromBlood_2_1,PositiveQuantitativeCultureLRT_2_1,BALCellsContainIntracellularBacteria_2_1,PositiveQuantitativeCultureLungTissue_2_1,HistopathologicExam2_1,AbscessFormationOrFoci_2_1,EvidenceOfFungalInvasion_2_1,CultureDate2_1,CultureResults2_1,Fever2_2,Leukopenia12_2,Leukocytosis2_2,Adults70old2_2,NewPurulentSputum2_2,WorseningCoughOrDyspnea2_2,RalesOrBronchialBreathSounds2_2,WorseningGasExchange2_2,VirusBordetellaLegionella2_2,FourfoldrisePaired2_2,FourfoldriseLegionella2_2,DetectionofLegionella2_2,CultureDate2_2,CultureResults2_2,Fever3,Adults70old3,NewPurulentSputum3,WorseningCoughOrDyspnea3,RalesOrBronchialBreathSounds3,WorseningGasExchange3,Hemoptysis3,PleuriticChestPain3,IdentificationCandida3,EvidenceofFungi3,DirectMicroscopicExam3,PositiveCultureFungi3,NonCultureDiagnostic3")] Pneumonia pneumonia)
        {
            if (id != pneumonia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pneumonia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PneumoniaExists(pneumonia.Id))
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
            return View(pneumonia);
        }

        // GET: Pneumonias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pneumonia = await _context.Pneumonias
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pneumonia == null)
            {
                return NotFound();
            }

            return View(pneumonia);
        }

        // POST: Pneumonias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pneumonia = await _context.Pneumonias.FindAsync(id);
            if (pneumonia != null)
            {
                _context.Pneumonias.Remove(pneumonia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PneumoniaExists(int id)
        {
            return _context.Pneumonias.Any(e => e.Id == id);
        }
    }
}
