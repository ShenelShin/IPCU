using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPCU.Data;
using IPCU.Models;
using System.Configuration;
using System.Data.SqlClient;
using DocumentFormat.OpenXml.Bibliography;

namespace IPCU.Controllers
{
    public class TCSkillsChecklistRealsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public TCSkillsChecklistRealsController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        // GET: TCSkillsChecklistReals
        public async Task<IActionResult> Index(string roomId, string stationId, string station)
        {
            ViewData["RoomId"] = roomId;
            ViewBag.StationID = stationId;
            ViewBag.Station = station;
            var filteredData = await _context.TCSkillsChecklistReal
                                              .Where(x => x.Area == roomId)
                                              .ToListAsync();
            return View(filteredData);
        }



        // GET: TCSkillsChecklistReals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCSkillsChecklistReal = await _context.TCSkillsChecklistReal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tCSkillsChecklistReal == null)
            {
                return NotFound();
            }

            return View(tCSkillsChecklistReal);
        }

        // GET: TCSkillsChecklistReals/Create
        public IActionResult Create(string roomId)
        {
            var model = new TCSkillsChecklistReal
            {
                Area = roomId // Pre-fill Area with roomId
            };
            return View(model);
        }

        // POST: TCSkillsChecklistReals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fname,Mname,Lname,EmployeeId,Area,ObserverName,Date,IsEquipmentAndCartPrepared,IsCleaningSolutionPrepared,IsProperAttireAndPPEWorn,IsHandHygieneAndGlovesDone,IsSignageChecked,IsSpillSoakedWithSolution,IsWallsCleaned,IsDoorFrameWiped,IsWindowSillAndWindowCleaned,IsHighTouchAreasWiped,IsVerticalSurfacesWiped,IsLooseDebrisPickedUp,IsRoomFloorMopped,IsUsedClothsDisposed,IsWasteContainersEmptied,IsInfectiousWasteRemoved,IsMirrorCleaned,IsSinkAreaCleaned,IsFaucetAndHandlesCleaned,IsToiletAndFlushHandlesCleaned,IsOtherBathroomSurfacesCleaned,IsBathroomFloorScrubbed,IsColorCodedWasteEmptied,IsPPERemoved,IsHandHygieneAfterPPE,IsGlovesRemovedAndHandHygieneDone,PreCleaningItems,PostCleaningItems,RecommendationsOrActions,UnitAreaStaffSignature,DateOfObservation")] TCSkillsChecklistReal tCSkillsChecklistReal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tCSkillsChecklistReal);
                await _context.SaveChangesAsync();

                // 👇 Redirect with roomId as query string
                return RedirectToAction(nameof(Index), new { roomId = tCSkillsChecklistReal.Area });
            }
            return View(tCSkillsChecklistReal);
        }


        // GET: TCSkillsChecklistReals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCSkillsChecklistReal = await _context.TCSkillsChecklistReal.FindAsync(id);
            if (tCSkillsChecklistReal == null)
            {
                return NotFound();
            }
            return View(tCSkillsChecklistReal);
        }

        // POST: TCSkillsChecklistReals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fname,Mname,Lname,EmployeeId,Area,ObserverName,Date,IsEquipmentAndCartPrepared,IsCleaningSolutionPrepared,IsProperAttireAndPPEWorn,IsHandHygieneAndGlovesDone,IsSignageChecked,IsSpillSoakedWithSolution,IsWallsCleaned,IsDoorFrameWiped,IsWindowSillAndWindowCleaned,IsHighTouchAreasWiped,IsVerticalSurfacesWiped,IsLooseDebrisPickedUp,IsRoomFloorMopped,IsUsedClothsDisposed,IsWasteContainersEmptied,IsInfectiousWasteRemoved,IsMirrorCleaned,IsSinkAreaCleaned,IsFaucetAndHandlesCleaned,IsToiletAndFlushHandlesCleaned,IsOtherBathroomSurfacesCleaned,IsBathroomFloorScrubbed,IsColorCodedWasteEmptied,IsPPERemoved,IsHandHygieneAfterPPE,IsGlovesRemovedAndHandHygieneDone,PreCleaningItems,PostCleaningItems,RecommendationsOrActions,UnitAreaStaffSignature,DateOfObservation")] TCSkillsChecklistReal tCSkillsChecklistReal)
        {
            if (id != tCSkillsChecklistReal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tCSkillsChecklistReal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TCSkillsChecklistRealExists(tCSkillsChecklistReal.Id))
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
            return View(tCSkillsChecklistReal);
        }

        // GET: TCSkillsChecklistReals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCSkillsChecklistReal = await _context.TCSkillsChecklistReal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tCSkillsChecklistReal == null)
            {
                return NotFound();
            }

            return View(tCSkillsChecklistReal);
        }

        // POST: TCSkillsChecklistReals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tCSkillsChecklistReal = await _context.TCSkillsChecklistReal.FindAsync(id);
            if (tCSkillsChecklistReal != null)
            {
                _context.TCSkillsChecklistReal.Remove(tCSkillsChecklistReal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TCSkillsChecklistRealExists(int id)
        {
            return _context.TCSkillsChecklistReal.Any(e => e.Id == id);
        }
        public IActionResult EnvironmentalDashboard(string employeeId)
        {
            if (string.IsNullOrEmpty(employeeId))
            {
                return View("~/Views/Environmental/Index.cshtml");
            }

            var checklist = _context.TCSkillsChecklistReal
                .FirstOrDefault(e => e.EmployeeId == employeeId);

            if (checklist == null)
            {
                ViewBag.ErrorMessage = "Employee not found.";
                return View("~/Views/Environmental/Index.cshtml");
            }

            return View("~/Views/Environmental/Index.cshtml", checklist);
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeeDetails(string employeeId)
        {
            var connectionString = _configuration.GetConnectionString("EmployeeConnection"); // name it as you want in appsettings.json
            var result = new { fname = "", mname = "", lname = "" };

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = @"
            SELECT TOP 1 
                FirstName, MiddleName, LastName 
            FROM UNIFIEDSVR.payroll.dbo.vwSPMS_User 
            WHERE EmpNum = @EmpNum";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@EmpNum", employeeId);
                    await conn.OpenAsync();

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            result = new
                            {
                                fname = reader["FirstName"].ToString(),
                                mname = reader["MiddleName"].ToString(),
                                lname = reader["LastName"].ToString()
                            };

                            return Json(result);
                        }
                    }
                }
            }

            return NotFound(); // return 404 if no match
        }

    }
}
