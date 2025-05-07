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
using System.Configuration;

namespace IPCU.Controllers
{
    public class UsisController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly PatientDbContext _patientContext;

        public UsisController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration, PatientDbContext patientContext)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _patientContext = patientContext;
        }

        // GET: Usis
        public async Task<IActionResult> Index(string hospNum)
        {
            var model = new Usi();

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

                    // null kasi bdate ko fuck goddamit
                    if (patientInfo.Patients.AdmDate.HasValue)
                    {
                        model.DateOfBirth = patientInfo.Patients.AdmDate.Value;
                    }
                    else
                    {
                        // null shit
                        model.DateOfAdmission = DateTime.MinValue;
                    }
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
                return NotFound();
            }

            // Query all USI records where HospitalNumber matches
            var usiRecords = await _context.Usi
                .Where(u => u.HospitalNumber == hospNum) // Assuming HospitalNumber is a string
                .ToListAsync();


            if (!usiRecords.Any())
            {
                return NotFound("No USI records found for this hospital number.");
            }

            // Create the ViewModel
            var model = new PatientUsiViewModel
            {
                FullName = $"{usiRecords.First().Fname} {usiRecords.First().Mname} {usiRecords.First().Lname}".Trim(),
                HospitalNumber = usiRecords.First().HospitalNumber.ToString(),
                DateOfBirth = usiRecords.First().DateOfBirth,
                Age = usiRecords.First().Age,
                UnitWardArea = usiRecords.First().UnitWardArea,
                Investigator = usiRecords.First().Investigator,
                DateOfAdmission = usiRecords.First().DateOfAdmission,
                Gender = usiRecords.First().Gender,
                UsiRecords = usiRecords
            };

            return View("Index", model);
        }




        // GET: Usis/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var usi = await _context.Usi.FindAsync(id);

            if (usi == null)
            {
                return NotFound($"No record found for ID {id}");
            }

            return View(usi);
        }



        // GET: Usis/Create
        public async Task<IActionResult> Create(string hospNum)
        {
            var model = new Usi();
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


        // POST: Usis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fname,Lname,Mname,HospitalNumber,DateOfBirth,Age,UnitWardArea,MainService,DateOfEvent,Investigator,DateOfAdmission,Disposition,DispositionDate,DispositionTransfer,Gender,Classification,MDRO,TypeClass,PatientOrganism,PatientAbscess,Fever1,LocalizedPain,PurulentDrainage,Organism,PatienLessthan1year,Fever2,Hypothermia,Apnea,Bradycardia,Lethargy,Vomiting,PurulentDrainage2,Organism2,CultureDate,CultureResults,MDROOrganism")] Usi usi)
        {
            if (ModelState.IsValid)
            {
                usi.TypeClass = "USI";
                usi.DateCreated = DateTime.Now;

                _context.Add(usi);
                await _context.SaveChangesAsync();

                // Retrieve the latest IdNum
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
                        cmd.Parameters.AddWithValue("@HospNum", usi.HospitalNumber);
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
                    TempData["Success"] = "USI record created successfully!";
                    return RedirectToAction("Details", "ICNPatient", new { id = idNum });
                }
                else
                {
                    TempData["Error"] = "Patient not found.";
                    return RedirectToAction("Index");
                }
            }

            return View(usi);
        }





        // GET: Usis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usi = await _context.Usi.FindAsync(id);
            if (usi == null)
            {
                return NotFound();
            }
            return View(usi);
        }

        // POST: Usis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fname,Lname,Mname,HospitalNumber,DateOfBirth,Age,UnitWardArea,MainService,DateOfEvent,Investigator,DateOfAdmission,Disposition,DispositionDate,DispositionTransfer,Gender,Classification,MDRO,TypeClass,PatientOrganism,PatientAbscess,Fever1,LocalizedPain,PurulentDrainage,Organism,PatienLessthan1year,Fever2,Hypothermia,Apnea,Bradycardia,Lethargy,Vomiting,PurulentDrainage2,Organism2,CultureDate,CultureResults")] Usi usi)
        {
            if (id != usi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(usi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsiExists(usi.Id))
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
            return View(usi);
        }

        // GET: Usis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usi = await _context.Usi
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usi == null)
            {
                return NotFound();
            }

            return View(usi);
        }

        // POST: Usis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usi = await _context.Usi.FindAsync(id);
            if (usi != null)
            {
                _context.Usi.Remove(usi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsiExists(int id)
        {
            return _context.Usi.Any(e => e.Id == id);
        }
    }
}
