using System.Configuration;
using System.Data.SqlClient;
using IPCU.Data;
using IPCU.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IPCU.Controllers
{
    public class SurgicalSiteInfectionController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly PatientDbContext _patientContext;
        public SurgicalSiteInfectionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration, PatientDbContext patientContext)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _patientContext = patientContext;
        }
        
        public async Task<IActionResult> Index(string hospNum)
        {
            var model = new SurgicalSiteInfectionChecklist();

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

            var patients = await _context.SurgicalSiteInfectionChecklist
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

            var patient = await _context.SurgicalSiteInfectionChecklist
                                        .FirstOrDefaultAsync(p => p.Id == id);

            // Ensure patient.Subclass is properly initialized
            if (string.IsNullOrEmpty(patient.Subclass))
            {
                patient.Subclass = "";  // Prevent null reference issues
            }
            var Subclass = patient.Subclass;
            return View(patient);
        }
        [HttpPost]
        public IActionResult Submit(SurgicalSiteInfectionChecklist model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Concatenate multi-select values
                    model.TypeClass = string.Join(",", Request.Form["TypeClass"]);
                    model.Subclass = string.Join(",", Request.Form["Subclass"]);
                    model.DateCreated = DateTime.Now;

                    _context.SurgicalSiteInfectionChecklist.Add(model);

                    // Get latest IdNum from tbpatient using HospitalNumber
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
                            conn.Open();

                            var result = cmd.ExecuteScalar();
                            if (result != null && result != DBNull.Value)
                            {
                                idNum = result.ToString();
                            }
                        }
                    }

                    _context.SaveChanges();
                    Console.WriteLine("Data saved successfully.");

                    if (!string.IsNullOrEmpty(idNum))
                    {
                        TempData["Success"] = "Checklist submitted successfully!";
                        return RedirectToAction("Details", "ICNPatient", new { id = idNum });
                    }
                    else
                    {
                        TempData["Error"] = "Patient not found.";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving data: {ex.Message}");
                }
            }

            // If ModelState is invalid, log errors
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine($"Validation Error: {error.ErrorMessage}");
            }

            return View("Index", model);
        }


    }
}
