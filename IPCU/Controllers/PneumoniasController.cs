using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;

namespace IPCU.Controllers
{
    public class PneumoniasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly PatientDbContext _patientContext;

        public PneumoniasController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration, PatientDbContext patientContext)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _patientContext = patientContext;
        }

        // GET: Pneumonias
        public async Task<IActionResult> Index(string hospNum)
        {
            var model = new Pneumonia();

            // Get current user's name for investigator
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                model.Investigator = $"{currentUser.FirstName} {currentUser.Initial} {currentUser.LastName}".Trim();
            }

            if (!string.IsNullOrEmpty(hospNum))
            {
                // Get patient info where HospNum matches
                var patientInfo = await (from pm in _context.PatientMasters
                                         join p in _context.Patients
                                         on pm.HospNum equals p.HospNum
                                         where pm.HospNum == hospNum
                                         select new
                                         {
                                             PatientMaster = pm,
                                             Patients = p
                                         }).FirstOrDefaultAsync();

                if (patientInfo != null)
                  {
                      model.HospitalNumber = patientInfo.PatientMaster.HospNum;
                      model.Gender = patientInfo.PatientMaster.Sex == "M" ? "Male" : "Female";
                      model.Fname = patientInfo.PatientMaster.FirstName;
                      model.Mname = patientInfo.PatientMaster.MiddleName;
                      model.Lname = patientInfo.PatientMaster.LastName;
                    //model.DateOfBirth = patientInfo.PatientMaster.BirthDate;

                      model.UnitWardArea = patientInfo.Patients.AdmLocation;
                    model.DateOfAdmission = patientInfo.Patients.AdmDate.Value;
                      model.Age = int.Parse(patientInfo.Patients.Age);


                      // Add other fields you want to auto-fill
                  }
            }

            return View("Create", model);
        }

        public async Task<IActionResult> PatientIndex(string hospNum)
        {
            if (string.IsNullOrEmpty(hospNum))
            {
                return NotFound("Hospital Number is required.");
            }

            var patients = await _context.Pneumonias
                                         .Where(p => p.HospitalNumber == hospNum)
                                         .ToListAsync();

            return View(patients);
        }

        // GET: Pneumonias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Pneumonias
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Pneumonias/Create
        public async Task<IActionResult> Create(string hospNum)
        {
            var model = new Pneumonia();
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                model.Investigator = $"{currentUser.FirstName} {currentUser.Initial} {currentUser.LastName}".Trim();
            }
            // Set default TypeClass
            model.TypeClass = "USI";

            if (!string.IsNullOrEmpty(hospNum))
            {
                string connectionStringPatient = _configuration.GetConnectionString("PatientConnection");

                using (SqlConnection conn = new SqlConnection(connectionStringPatient))
                {
                    await conn.OpenAsync();

                    string query = @"
                        SELECT TOP 1 tm.HospNum, tm.FirstName, tm.MiddleName, tm.LastName, tm.Sex,
                               tm.BirthDate, tm.MSSDiscountExpiry,
                               tp.AdmLocation, tp.AdmDate, tp.Age, tp.RoomId, tp.DcrDate
                        FROM tbmaster tm
                        LEFT JOIN tbpatient tp ON tm.HospNum = tp.HospNum
                        WHERE tm.HospNum = @HospNum
                        ORDER BY tp.AdmDate DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@HospNum", hospNum);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                model.HospitalNumber = reader["HospNum"]?.ToString();
                                model.Fname = reader["FirstName"]?.ToString();
                                model.Mname = reader["MiddleName"]?.ToString();
                                model.Lname = reader["LastName"]?.ToString();
                                model.Gender = reader["Sex"]?.ToString() == "M" ? "Male" : "Female";

                                if (reader["BirthDate"] != DBNull.Value)
                                    model.DateOfBirth = Convert.ToDateTime(reader["BirthDate"]);

                                if (reader["AdmDate"] != DBNull.Value)
                                    model.DateOfAdmission = Convert.ToDateTime(reader["AdmDate"]);

                                model.Age = int.TryParse(reader["Age"]?.ToString(), out int age) ? age : 0;

                                string roomId = reader["RoomId"]?.ToString();

                                // Disposition
                                model.Disposition = reader["DcrDate"] == DBNull.Value ? "Still Admitted" : "Discharged";

                                // Classification
                                if (reader["MSSDiscountExpiry"] != DBNull.Value &&
                                    DateTime.TryParse(reader["MSSDiscountExpiry"].ToString(), out DateTime expiry))
                                {
                                    model.Classification = expiry < DateTime.Now ? "Pay" : "Service";
                                }
                                else
                                {
                                    model.Classification = "Pay";
                                }

                                // Resolve UnitWardArea
                                if (!string.IsNullOrEmpty(roomId))
                                {
                                    string connectionStringBuild = _configuration.GetConnectionString("Build_FileConnection");

                                    using (SqlConnection connBuild = new SqlConnection(connectionStringBuild))
                                    {
                                        await connBuild.OpenAsync();

                                        string queryRoom = "SELECT StationID FROM tbCoRoom WHERE RoomId = @RoomId";
                                        string stationId = null;

                                        using (SqlCommand cmdRoom = new SqlCommand(queryRoom, connBuild))
                                        {
                                            cmdRoom.Parameters.AddWithValue("@RoomId", roomId);
                                            stationId = (string)await cmdRoom.ExecuteScalarAsync();
                                        }

                                        if (!string.IsNullOrEmpty(stationId))
                                        {
                                            string queryStation = "SELECT Station FROM tbCoStation WHERE StationID = @StationID";

                                            using (SqlCommand cmdStation = new SqlCommand(queryStation, connBuild))
                                            {
                                                cmdStation.Parameters.AddWithValue("@StationID", stationId);
                                                var station = (string)await cmdStation.ExecuteScalarAsync();
                                                model.UnitWardArea = $"{station} / {roomId}";
                                            }
                                        }
                                        else
                                        {
                                            model.UnitWardArea = roomId;
                                        }
                                    }
                                }
                                else
                                {
                                    model.UnitWardArea = "Unknown Location";
                                }
                            }
                        }
                    }
                }
            }

            return View(model);
        }

        // POST: Pneumonias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Fname, Lname, Mname, HospitalNumber, DateOfBirth, Age, UnitWardArea, MainService, DateOfEvent, Investigator, DateOfAdmission, Disposition, DispositionDate, DispositionTransfer, Gender, Classification, MDRO,MDROOrganism, TypeClass, PNEU_Subclass, PersistentorProgressive1_1, Fever1_1, Leukopenia1_1, Leukocytosis1_1, Adults70old1_1, NewPurulentSputum1_1, WorseningCoughOrDyspnea1_1, RalesOrBronchialBreathSounds1_1, WorseningGasExchange1_1, PersistentorProgressive1_2, WorseningGasExchange1_2, TemperatureInstability1_2, LeukopeniaOrLeukocytosis1_2, NewPurulentSputum1_2, ApneaOrTachypneaOrNasalFlaring1_2, WheezingRalesOrRhonchi1_2, Cough1_2, BradycardiaOrTachycardia1_2, PersistentorProgressive1_3, Fever1_3, Leukopenia1_3, NewPurulentSputum1_3, NewWorseningCough1_3, RalesorBronchial1_3, WorseningGas1_3, PersistentorProgressive2_1, Fever2_1, Leukopenia12_1, Leukocytosis2_1, Adults70old2_1, NewPurulentSputum2_1, WorseningCoughOrDyspnea2_1, RalesOrBronchialBreathSounds2_1, WorseningGasExchange2_1, OrganismIdentifiedFromBlood_2_1, PositiveQuantitativeCultureLRT_2_1, BALCellsContainIntracellularBacteria_2_1, PositiveQuantitativeCultureLungTissue_2_1, HistopathologicExam2_1, AbscessFormationOrFoci_2_1, EvidenceOfFungalInvasion_2_1, CultureDate2_1, CultureResults2_1, PersistentorProgressive2_2, Fever2_2, Leukopenia12_2, Leukocytosis2_2, Adults70old2_2, NewPurulentSputum2_2, WorseningCoughOrDyspnea2_2, RalesOrBronchialBreathSounds2_2, WorseningGasExchange2_2, VirusBordetellaLegionella2_2, FourfoldrisePaired2_2, FourfoldriseLegionella2_2, DetectionofLegionella2_2, CultureDate2_2, CultureResults2_2, PersistentorProgressive3, Fever3, Adults70old3, NewPurulentSputum3, WorseningCoughOrDyspnea3, RalesOrBronchialBreathSounds3, WorseningGasExchange3, Hemoptysis3, PleuriticChestPain3, IdentificationCandida3, EvidenceofFungi3, DirectMicroscopicExam3, PositiveCultureFungi3, NonCultureDiagnostic3")] Pneumonia pneumonia)
        {
            if (ModelState.IsValid)
            {
                pneumonia.DateCreated = DateTime.Now;
                _context.Add(pneumonia);
                await _context.SaveChangesAsync();

                // Get latest IdNum using HospitalNumber
                string idNum = null;
                string connectionString = _configuration.GetConnectionString("PatientConnection");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT TOP 1 tp.IdNum
                FROM tbmaster tm
                LEFT JOIN tbpatient tp ON tm.HospNum = tp.HospNum
                WHERE tm.HospNum = @HospNum
                ORDER BY tp.AdmDate DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@HospNum", pneumonia.HospitalNumber);
                        conn.Open();

                        var result = await cmd.ExecuteScalarAsync();
                        if (result != null && result != DBNull.Value)
                        {
                            idNum = result.ToString();
                        }
                    }
                }

                if (!string.IsNullOrEmpty(idNum))
                {
                    TempData["Success"] = "Pneumonia record created successfully!";
                    return RedirectToAction("Details", "ICNPatient", new { id = idNum });
                }
                else
                {
                    TempData["Error"] = "Patient not found.";
                    return RedirectToAction("Index");
                }
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
