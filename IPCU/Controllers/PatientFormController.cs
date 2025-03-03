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
    public class PatientFormController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PatientFormPdfService _pdfService;

        public PatientFormController(ApplicationDbContext context, PatientFormPdfService pdfService)
        {
            _context = context;
            _pdfService = pdfService;
        }

        // Generate PDF for all patients
        // New method to generate PDF for a single patient
        public async Task<IActionResult> PrintPdf(int id)
        {
            var patient = await _context.PatientForms.FirstOrDefaultAsync(m => m.Id == id);

            if (patient == null)
            {
                return NotFound("Patient data not found.");
            }

            var pdfData = _pdfService.GeneratePdf(patient);

            return File(pdfData, "application/pdf", $"Patient_{patient.Id}_Report.pdf");
        }

        // GET: PatientForms
        public async Task<IActionResult> Index()
        {
            return View(await _context.PatientForms.ToListAsync());
        }

        // GET: PatientForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var patientForm = await _context.PatientForms.FirstOrDefaultAsync(m => m.Id == id);
            if (patientForm == null) return NotFound();

            return View(patientForm);
        }

        // GET: PatientForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PatientForms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,MiddleName,LastName,Suffix,Disease,Status,Room,Building,NurseFirstName,NurseMiddleName,NurseLastName,NurseSuffix,Age,Sex")] PatientForm patientForm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patientForm);
        }

        // GET: PatientForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var patientForm = await _context.PatientForms.FindAsync(id);
            if (patientForm == null) return NotFound();

            return View(patientForm);
        }

        // POST: PatientForms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,MiddleName,LastName,Suffix,Disease,Status,Room,Building,NurseFirstName,NurseMiddleName,NurseLastName,NurseSuffix,Age,Sex")] PatientForm patientForm)
        {
            if (id != patientForm.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientFormExists(patientForm.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patientForm);
        }

        // GET: PatientForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var patientForm = await _context.PatientForms.FirstOrDefaultAsync(m => m.Id == id);
            if (patientForm == null) return NotFound();

            return View(patientForm);
        }

        // POST: PatientForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patientForm = await _context.PatientForms.FindAsync(id);
            if (patientForm != null)
            {
                _context.PatientForms.Remove(patientForm);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PatientFormExists(int id)
        {
            return _context.PatientForms.Any(e => e.Id == id);
        }
    }
}
