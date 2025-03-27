using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPCU.Pages.ICNPatient
{
    public class AllFormsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public AllFormsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public string PatientName { get; set; }
        public string HospNum { get; set; }
        public InfectionFormsInfo InfectionForms { get; set; }

        public async Task<IActionResult> OnGetAsync(string hospNum)
        {
            if (string.IsNullOrEmpty(hospNum))
            {
                return NotFound();
            }

            var patient = await (from pm in _context.PatientMasters
                                 join p in _context.Patients on pm.HospNum equals p.HospNum
                                 where pm.HospNum == hospNum
                                 select new
                                 {
                                     pm.FirstName,
                                     pm.MiddleName,
                                     pm.LastName,
                                     pm.HospNum,
                                     InfectionForms = new InfectionFormsInfo
                                     {
                                         CardiovascularForm = _context.CardiovascularSystemInfection
                                             .FirstOrDefault(f => f.HospitalNumber == pm.HospNum),
                                         SSTForm = _context.SSTInfectionModels
                                             .FirstOrDefault(f => f.HospitalNumber == pm.HospNum),
                                         LCBIForm = _context.LaboratoryConfirmedBSI
                                             .FirstOrDefault(f => f.HospitalNumber == pm.HospNum),
                                         PVAEForm = _context.PediatricVAEChecklist
                                             .FirstOrDefault(f => f.HospitalNumber == pm.HospNum),
                                         UTIForm = _context.UTIModels
                                             .FirstOrDefault(f => f.HospitalNumber == pm.HospNum),
                                         PneumoniaForm = _context.Pneumonias
                                             .FirstOrDefault(f => f.HospitalNumber == pm.HospNum),
                                         USIForm = _context.Usi
                                             .FirstOrDefault(f => f.HospitalNumber == pm.HospNum),
                                         VAEForm = _context.VentilatorEventChecklists
                                             .FirstOrDefault(f => f.HospNum == pm.HospNum)
                                     }
                                 }).FirstOrDefaultAsync();

            if (patient == null)
            {
                return NotFound();
            }

            PatientName = $"{patient.LastName}, {patient.FirstName} {patient.MiddleName}";
            HospNum = patient.HospNum;
            InfectionForms = patient.InfectionForms;

            return Page();
        }
    }
}
