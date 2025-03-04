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
    public class FitTestingFormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FitTestingFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FitTestingForms
        public async Task<IActionResult> Index()
        {
            return View(await _context.FitTestingForm.ToListAsync());
        }

        // GET: FitTestingForms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitTestingForm = await _context.FitTestingForm
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fitTestingForm == null)
            {
                return NotFound();
            }

            return View(fitTestingForm);
        }

        // GET: FitTestingForms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FitTestingForms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HCW_Name,DUO,Limitation,Fit_Test_Solution,Sensitivity_Test,Respiratory_Type,Model,Size,Normal_Breathing,Deep_Breathing,Turn_head_side_to_side,Move_head_up_and_down,Reading,Bending_Jogging,Normal_Breathing_2,Name_of_Fit_Tester,DUO_Tester,SubmittedAt")] FitTestingForm fitTestingForm)
        {
            if (ModelState.IsValid)
            {
                fitTestingForm.SubmittedAt = DateTime.Now;

                _context.Add(fitTestingForm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fitTestingForm);
        }

        // GET: FitTestingForms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitTestingForm = await _context.FitTestingForm.FindAsync(id);
            if (fitTestingForm == null)
            {
                return NotFound();
            }
            return View(fitTestingForm);
        }

        // POST: FitTestingForms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HCW_Name,DUO,Limitation,Fit_Test_Solution,Sensitivity_Test,Respiratory_Type,Model,Size,Normal_Breathing,Deep_Breathing,Turn_head_side_to_side,Move_head_up_and_down,Reading,Bending_Jogging,Normal_Breathing_2,Name_of_Fit_Tester,DUO_Tester,SubmittedAt")] FitTestingForm fitTestingForm)
        {
            if (id != fitTestingForm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fitTestingForm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FitTestingFormExists(fitTestingForm.Id))
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
            return View(fitTestingForm);
        }

        // GET: FitTestingForms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fitTestingForm = await _context.FitTestingForm
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fitTestingForm == null)
            {
                return NotFound();
            }

            return View(fitTestingForm);
        }

        // POST: FitTestingForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fitTestingForm = await _context.FitTestingForm.FindAsync(id);
            if (fitTestingForm != null)
            {
                _context.FitTestingForm.Remove(fitTestingForm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FitTestingFormExists(int id)
        {
            return _context.FitTestingForm.Any(e => e.Id == id);
        }

        public IActionResult PrintPdf(int id)
        {
            var fitTestingForm = _context.FitTestingForm.FirstOrDefault(f => f.Id == id);
            if (fitTestingForm == null)
            {
                return NotFound();
            }

            var pdfService = new FitTestingFormPdfService();
            var fileName = $"{fitTestingForm.HCW_Name}_FitTestingForm.pdf";
            var filePath = Path.Combine(Path.GetTempPath(), fileName);

            pdfService.GeneratePdf(fitTestingForm, filePath);

            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            Response.Headers.Add("Content-Disposition", "inline; filename=" + fileName);
            return File(fileStream, "application/pdf");
        }


    }
}
