using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data.SqlClient;

namespace IPCU.Controllers
{
    public class GIInfectionChecklistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly PatientDbContext _patientContext;
        public GIInfectionChecklistController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration, PatientDbContext patientContext)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _patientContext = patientContext;
        }

        public async Task<IActionResult> Index(string hospNum)
        {
            var model = new GIInfectionChecklist();

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                model.Investigator = $"{currentUser.FirstName} {currentUser.Initial} {currentUser.LastName}".Trim();
            }

            if (!string.IsNullOrEmpty(hospNum))
            {
                string connectionStringPatient = _configuration.GetConnectionString("PatientConnection");

                using (SqlConnection conn = new SqlConnection(connectionStringPatient))
                {
                    await conn.OpenAsync();

                    string query = @"
                            SELECT TOP 1 tm.HospNum, tm.FirstName, tm.MiddleName, tm.LastName, tm.Sex,
                                   tm.BirthDate, tm.MSSDiscountExpiry,  -- Include MSSDiscountExpiry
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

                                // Check Disposition
                                model.Disposition = reader["DcrDate"] == DBNull.Value ? "Still Admitted" : "Discharged";
                                // Classification logic based on MSSDiscountExpiry
                                if (reader["MSSDiscountExpiry"] != DBNull.Value && DateTime.TryParse(reader["MSSDiscountExpiry"].ToString(), out DateTime expiry))
                                {
                                    model.Classification = expiry < DateTime.Now ? "Pay" : "Service";
                                }
                                else
                                {
                                    model.Classification = "Pay"; // Default to Pay if MSSDiscountExpiry is null or invalid
                                }

                                // Resolve Room → Station
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

        public async Task<IActionResult> PatientIndex(string hospNum)
        {
            if (string.IsNullOrEmpty(hospNum))
            {
                return NotFound("Hospital Number is required.");
            }

            var patients = await _context.GIInfectionChecklists
                                         .Where(p => p.HospitalNumber == hospNum)
                                         .ToListAsync();

            return View(patients);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.GIInfectionChecklists
                                        .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(GIInfectionChecklist model, string[] TypeClass)
        {
            // Combine selected classifications into a single comma-separated string
            if (TypeClass != null && TypeClass.Length > 0)
            {
                model.TypeClass = string.Join(", ", TypeClass);
            }

            // Check if the model is valid
            if (!ModelState.IsValid)
            {
                // Log validation errors to console
                foreach (var error in ModelState)
                {
                    foreach (var subError in error.Value.Errors)
                    {
                        Console.WriteLine($"Field: {error.Key}, Error: {subError.ErrorMessage}");
                    }
                }

                Console.WriteLine("Model state is invalid!");
                return View("Index", model);
            }

            // Save the checklist to the database
            _context.GIInfectionChecklists.Add(model);
            await _context.SaveChangesAsync();
            Console.WriteLine("Saved successfully!");

            // Retrieve IdNum using HospitalNumber
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
                    cmd.Parameters.AddWithValue("@HospNum", model.HospitalNumber);
                    await conn.OpenAsync();

                    var result = await cmd.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        idNum = result.ToString();
                    }
                }
            }

            // Redirect based on patient ID result
            if (!string.IsNullOrEmpty(idNum))
            {
                TempData["Success"] = "GI Infection Checklist submitted successfully!";
                return RedirectToAction("Details", "ICNPatient", new { id = idNum });
            }
            else
            {
                TempData["Error"] = "Patient not found.";
                return RedirectToAction("Index");
            }
        }


    }
}
