using Microsoft.AspNetCore.Mvc;
using IPCU.Models;
using IPCU.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Configuration;
namespace IPCU.Controllers
{
    public class InfectionChecklistController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly PatientDbContext _patientContext;


        // Inject DbContext via constructor
        public InfectionChecklistController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IConfiguration configuration, PatientDbContext patientContext)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _patientContext = patientContext;
        }

        public async Task<IActionResult> Index(string hospNum)
        {
            var model = new SSTInfectionModel();

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
            {
                model.Investigator = $"{currentUser.FirstName} {currentUser.Initial} {currentUser.LastName}".Trim();
            }

            if (!string.IsNullOrEmpty(hospNum))
            {
                string connectionStringPatient = _configuration.GetConnectionString("PatientConnection");

                // Fetch patient data from tbmaster and tbpatient
                using (SqlConnection connPatient = new SqlConnection(connectionStringPatient))
                {
                    await connPatient.OpenAsync();

                    string queryPatient = @"
                        SELECT TOP 1 tm.HospNum, tm.FirstName, tm.MiddleName, tm.LastName, tm.Sex,
                               tm.BirthDate, tm.MSSDiscountExpiry,  -- <-- added
                               tp.AdmLocation, tp.AdmDate, tp.Age, tp.RoomId, tp.DcrDate
                        FROM tbmaster tm
                        LEFT JOIN tbpatient tp ON tm.HospNum = tp.HospNum
                        WHERE tm.HospNum = @HospNum
                        ORDER BY tp.AdmDate DESC";


                    using (SqlCommand cmdPatient = new SqlCommand(queryPatient, connPatient))
                    {
                        cmdPatient.Parameters.AddWithValue("@HospNum", hospNum);

                        using (SqlDataReader reader = await cmdPatient.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                model.HospitalNumber = reader["HospNum"]?.ToString();
                                model.Fname = reader["FirstName"]?.ToString();
                                model.Mname = reader["MiddleName"]?.ToString();
                                model.Lname = reader["LastName"]?.ToString();
                                model.Gender = reader["Sex"]?.ToString() == "M" ? "Male" : "Female";
                                model.UnitWardArea = reader["AdmLocation"]?.ToString();

                                if (reader["AdmDate"] != DBNull.Value)
                                    model.DateOfAdmission = Convert.ToDateTime(reader["AdmDate"]);
                                else
                                    model.DateOfAdmission = DateTime.MinValue;

                                if (reader["BirthDate"] != DBNull.Value)
                                    model.DateOfBirth = Convert.ToDateTime(reader["BirthDate"]);
                                else
                                    model.DateOfBirth = DateTime.MinValue;

                                // Retrieve the RoomId from tbpatient
                                string roomId = reader["RoomId"]?.ToString();

                                model.Age = int.TryParse(reader["Age"]?.ToString(), out int age) ? age : 0;
                                // ✅ Classification logic based on MSDiscountExpiry
                                if (reader["MSSDiscountExpiry"] != DBNull.Value && DateTime.TryParse(reader["MSSDiscountExpiry"].ToString(), out DateTime expiry))
                                {
                                    model.Classification = expiry < DateTime.Now ? "Pay" : "Service";
                                }
                                else
                                {
                                    model.Classification = "Pay"; // Default to "Pay" if missing
                                }
                                // Check Discharge Date (DcrDate) to set Disposition
                                if (reader["DcrDate"] == DBNull.Value)
                                {
                                    model.Disposition = "Still Admitted";  // If DcrDate is null, set Disposition as "Still Admitted"
                                }
                                else
                                {
                                    model.Disposition = "Discharged";  // If DcrDate is not null, set Disposition as "Discharged"
                                }

                                // Now get the Station name based on RoomId
                                if (!string.IsNullOrEmpty(roomId))
                                {
                                    string connectionStringBuildFile = _configuration.GetConnectionString("Build_FileConnection");

                                    using (SqlConnection connBuildFile = new SqlConnection(connectionStringBuildFile))
                                    {
                                        await connBuildFile.OpenAsync();

                                        // Query to get StationID from tbCoRoom
                                        string queryRoom = @"
                                    SELECT cr.StationID
                                    FROM tbCoRoom cr
                                    WHERE cr.RoomId = @RoomId";

                                        string stationId = null;

                                        using (SqlCommand cmdRoom = new SqlCommand(queryRoom, connBuildFile))
                                        {
                                            cmdRoom.Parameters.AddWithValue("@RoomId", roomId);

                                            stationId = (string)await cmdRoom.ExecuteScalarAsync();
                                        }

                                        // If we have a StationID, get the Station name from tbCoStation
                                        if (!string.IsNullOrEmpty(stationId))
                                        {
                                            string queryStation = @"
                                        SELECT cs.Station
                                        FROM tbCoStation cs
                                        WHERE cs.StationID = @StationID";

                                            using (SqlCommand cmdStation = new SqlCommand(queryStation, connBuildFile))
                                            {
                                                cmdStation.Parameters.AddWithValue("@StationID", stationId);

                                                var station = (string)await cmdStation.ExecuteScalarAsync();

                                                // Concatenate Station and RoomId for UnitWardArea
                                                model.UnitWardArea = $"{station} / {roomId}";
                                            }
                                        }
                                        else
                                        {
                                            // If no Station found, just set RoomId
                                            model.UnitWardArea = roomId;
                                        }
                                    }
                                }
                                else
                                {
                                    // If no RoomId, just leave the UnitWardArea as it is (or assign default value)
                                    model.UnitWardArea = model.UnitWardArea ?? "Unknown Location";
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

            var patients = await _context.SSTInfectionModels
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

            var patient = await _context.SSTInfectionModels
                                        .FirstOrDefaultAsync(p => p.SSTID == id);

            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        [HttpPost]
        public IActionResult Submit(SSTInfectionModel model)
        {
            if (ModelState.IsValid)
            {
                // Concatenate checkbox values before saving
                model.InfectionType = string.Join(",", Request.Form["InfectionType"]);

                // Add the infection record to the database
                _context.SSTInfectionModels.Add(model);
                
                // Find the patient HAI record or create a new one if it doesn't exist
                var patientHAI = _context.PatientHAI
                    .FirstOrDefault(p => p.HospNum == model.HospitalNumber);

                // Get the latest IdNum using AdmDate from tbpatient
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

                if (patientHAI != null)
                {
                    // Increment the HAI count
                    patientHAI.HaiCount += 1;
                    patientHAI.HaiStatus = true;
                    patientHAI.LastUpdated = DateTime.Now;
                    // Update the patient HAI record
                    _context.PatientHAI.Update(patientHAI);
                }
                else
                {
                    // Create a new PatientHAI record if one doesn't exist
                    var newPatientHAI = new PatientHAI
                    {
                        HospNum = model.HospitalNumber,
                        HaiStatus = true,
                        HaiCount = 1,
                        LastUpdated = DateTime.Now
                    };
                    _context.PatientHAI.Add(newPatientHAI);
                }

                // Save all changes to the database
                _context.SaveChanges();


                // Redirect to Details with latest IdNum
                if (!string.IsNullOrEmpty(idNum))
                {
                    TempData["Success"] = "Checklist submitted successfully! HAI count has been updated.";
                    return RedirectToAction("Details", "ICNPatient", new { id = idNum });
                }
                else
                {
                    TempData["Error"] = "Patient not found.";
                    return RedirectToAction("Index");
                }
            }

            return View("Index", model);
        }


    }
}