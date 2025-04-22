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
    public class NoticeOfReferralsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NoticeOfReferralsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: NoticeOfReferrals
        public async Task<IActionResult> Index()
        {
            return View(await _context.NoticeOfReferral.ToListAsync());
        }

        // GET: NoticeOfReferrals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticeOfReferral = await _context.NoticeOfReferral
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noticeOfReferral == null)
            {
                return NotFound();
            }

            return View(noticeOfReferral);
        }

        // GET: NoticeOfReferrals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NoticeOfReferrals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientLastName,PatientFirstName,PatientMiddleName,AreaUnit,Age,Sex,DOA,DOD,InitialDiagnosis,HAAT,Communicable,Fever,PostOp,ReAdmitted,Laboratory,Radiology,Referredby,ReferredbyDnT,Receivedby,ReceivedbyDnT")] NoticeOfReferral noticeOfReferral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(noticeOfReferral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(noticeOfReferral);
        }

        // GET: NoticeOfReferrals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticeOfReferral = await _context.NoticeOfReferral.FindAsync(id);
            if (noticeOfReferral == null)
            {
                return NotFound();
            }
            return View(noticeOfReferral);
        }

        // POST: NoticeOfReferrals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatientLastName,PatientFirstName,PatientMiddleName,AreaUnit,Age,Sex,DOA,DOD,InitialDiagnosis,HAAT,Communicable,Fever,PostOp,ReAdmitted,Laboratory,Radiology,Referredby,ReferredbyDnT,Receivedby,ReceivedbyDnT")] NoticeOfReferral noticeOfReferral)
        {
            if (id != noticeOfReferral.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(noticeOfReferral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoticeOfReferralExists(noticeOfReferral.Id))
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
            return View(noticeOfReferral);
        }

        // GET: NoticeOfReferrals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var noticeOfReferral = await _context.NoticeOfReferral
                .FirstOrDefaultAsync(m => m.Id == id);
            if (noticeOfReferral == null)
            {
                return NotFound();
            }

            return View(noticeOfReferral);
        }

        // POST: NoticeOfReferrals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var noticeOfReferral = await _context.NoticeOfReferral.FindAsync(id);
            if (noticeOfReferral != null)
            {
                _context.NoticeOfReferral.Remove(noticeOfReferral);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoticeOfReferralExists(int id)
        {
            return _context.NoticeOfReferral.Any(e => e.Id == id);
        }
    }
}
