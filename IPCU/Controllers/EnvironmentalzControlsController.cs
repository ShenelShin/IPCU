using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IPCU.Data;
using IPCU.Models;
using DocumentFormat.OpenXml.Bibliography;

namespace IPCU.Controllers
{
    public class EnvironmentalzControlsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnvironmentalzControlsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EnvironmentalzControls
        public async Task<IActionResult> Index(string station)
        {
            ViewBag.Station = station;

            return View(await _context.EnvironmentalzControl.ToListAsync());
        }

        // GET: EnvironmentalzControls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentalzControl = await _context.EnvironmentalzControl
                .FirstOrDefaultAsync(m => m.Id == id);
            if (environmentalzControl == null)
            {
                return NotFound();
            }

            return View(environmentalzControl);
        }

        // GET: EnvironmentalzControls/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EnvironmentalzControls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AreaOrUnit,DateAndTimeOfMonitoring,AssessedBy,PatientExamRoom_FloorsWallsClean_Finding,PatientExamRoom_FloorsWallsClean_ActionDone,PatientExamRoom_FloorsWallsClean_Remarks,PatientExamRoom_FloorsWallsClean_FuDate,PatientExamRoom_FloorsWallsClean_FuRemarks,PatientExamRoom_CurtainsClean_Finding,PatientExamRoom_CurtainsClean_ActionDone,PatientExamRoom_CurtainsClean_Remarks,PatientExamRoom_CurtainsClean_FuDate,PatientExamRoom_CurtainsClean_FuRemarks,PatientExamRoom_SinkClean_Finding,PatientExamRoom_SinkClean_ActionDone,PatientExamRoom_SinkClean_Remarks,PatientExamRoom_SinkClean_FuDate,PatientExamRoom_SinkClean_FuRemarks,PatientExamRoom_SoapDispenser_Finding,PatientExamRoom_SoapDispenser_ActionDone,PatientExamRoom_SoapDispenser_Remarks,PatientExamRoom_SoapDispenser_FuDate,PatientExamRoom_SoapDispenser_FuRemarks,PatientExamRoom_AlcoholSanitizers_Finding,PatientExamRoom_AlcoholSanitizers_ActionDone,PatientExamRoom_AlcoholSanitizers_Remarks,PatientExamRoom_AlcoholSanitizers_FuDate,PatientExamRoom_AlcoholSanitizers_FuRemarks,PatientExamRoom_PPEAvailable_Finding,PatientExamRoom_PPEAvailable_ActionDone,PatientExamRoom_PPEAvailable_Remarks,PatientExamRoom_PPEAvailable_FuDate,PatientExamRoom_PPEAvailable_FuRemarks,PatientExamRoom_NoSuppliesUnderSinks_Finding,PatientExamRoom_NoSuppliesUnderSinks_ActionDone,PatientExamRoom_NoSuppliesUnderSinks_Remarks,PatientExamRoom_NoSuppliesUnderSinks_FuDate,PatientExamRoom_NoSuppliesUnderSinks_FuRemarks,PatientExamRoom_NoFoodInCareAreas_Finding,PatientExamRoom_NoFoodInCareAreas_ActionDone,PatientExamRoom_NoFoodInCareAreas_Remarks,PatientExamRoom_NoFoodInCareAreas_FuDate,PatientExamRoom_NoFoodInCareAreas_FuRemarks,WorkArea_ChartsStored_Finding,WorkArea_ChartsStored_ActionDone,WorkArea_ChartsStored_Remarks,WorkArea_ChartsStored_FuDate,WorkArea_ChartsStored_FuRemarks,WorkArea_NoFoodOrDrinks_Finding,WorkArea_NoFoodOrDrinks_ActionDone,WorkArea_NoFoodOrDrinks_Remarks,WorkArea_NoFoodOrDrinks_FuDate,WorkArea_NoFoodOrDrinks_FuRemarks,WorkArea_FansClean_Finding,WorkArea_FansClean_ActionDone,WorkArea_FansClean_Remarks,WorkArea_FansClean_FuDate,WorkArea_FansClean_FuRemarks,WorkArea_ACClean_Finding,WorkArea_ACClean_ActionDone,WorkArea_ACClean_Remarks,WorkArea_ACClean_FuDate,WorkArea_ACClean_FuRemarks,WorkArea_ExhaustFansClean_Finding,WorkArea_ExhaustFansClean_ActionDone,WorkArea_ExhaustFansClean_Remarks,WorkArea_ExhaustFansClean_FuDate,WorkArea_ExhaustFansClean_FuRemarks,WorkArea_CeilingClean_Finding,WorkArea_CeilingClean_ActionDone,WorkArea_CeilingClean_Remarks,WorkArea_CeilingClean_FuDate,WorkArea_CeilingClean_FuRemarks,WorkArea_WallsClean_Finding,WorkArea_WallsClean_ActionDone,WorkArea_WallsClean_Remarks,WorkArea_WallsClean_FuDate,WorkArea_WallsClean_FuRemarks,WorkArea_CountersClean_Finding,WorkArea_CountersClean_ActionDone,WorkArea_CountersClean_Remarks,WorkArea_CountersClean_FuDate,WorkArea_CountersClean_FuRemarks,WorkArea_FloorsClean_Finding,WorkArea_FloorsClean_ActionDone,WorkArea_FloorsClean_Remarks,WorkArea_FloorsClean_FuDate,WorkArea_FloorsClean_FuRemarks,WorkArea_DoorsWindowsClean_Finding,WorkArea_DoorsWindowsClean_ActionDone,WorkArea_DoorsWindowsClean_Remarks,WorkArea_DoorsWindowsClean_FuDate,WorkArea_DoorsWindowsClean_FuRemarks,WorkArea_CleanBathroom_Finding,WorkArea_CleanBathroom_ActionDone,WorkArea_CleanBathroom_Remarks,WorkArea_CleanBathroom_FuDate,WorkArea_CleanBathroom_FuRemarks,Hallway_FloorsWallsClean_Finding,Hallway_FloorsWallsClean_ActionDone,Hallway_FloorsWallsClean_Remarks,Hallway_FloorsWallsClean_FuDate,Hallway_FloorsWallsClean_FuRemarks,Hallway_NoObstruction_Finding,Hallway_NoObstruction_ActionDone,Hallway_NoObstruction_Remarks,Hallway_NoObstruction_FuDate,Hallway_NoObstruction_FuRemarks,WaitingArea_FurnitureClean_Finding,WaitingArea_FurnitureClean_ActionDone,WaitingArea_FurnitureClean_Remarks,WaitingArea_FurnitureClean_FuDate,WaitingArea_FurnitureClean_FuRemarks,WaitingArea_TrashDisposed_Finding,WaitingArea_TrashDisposed_ActionDone,WaitingArea_TrashDisposed_Remarks,WaitingArea_TrashDisposed_FuDate,WaitingArea_TrashDisposed_FuRemarks,WaitingArea_FoodInDesignatedArea_Finding,WaitingArea_FoodInDesignatedArea_ActionDone,WaitingArea_FoodInDesignatedArea_Remarks,WaitingArea_FoodInDesignatedArea_FuDate,WaitingArea_FoodInDesignatedArea_FuRemarks,Equipment_StorageClean_Finding,Equipment_StorageClean_ActionDone,Equipment_StorageClean_Remarks,Equipment_StorageClean_FuDate,Equipment_StorageClean_FuRemarks,UtilityRoom_CleanSoiledSeparation_Finding,UtilityRoom_CleanSoiledSeparation_ActionDone,UtilityRoom_CleanSoiledSeparation_Remarks,UtilityRoom_CleanSoiledSeparation_FuDate,UtilityRoom_CleanSoiledSeparation_FuRemarks,UtilityRoom_FloorsWallsClean_Finding,UtilityRoom_FloorsWallsClean_ActionDone,UtilityRoom_FloorsWallsClean_Remarks,UtilityRoom_FloorsWallsClean_FuDate,UtilityRoom_FloorsWallsClean_FuRemarks,UtilityRoom_NoSuppliesOnFloor_Finding,UtilityRoom_NoSuppliesOnFloor_ActionDone,UtilityRoom_NoSuppliesOnFloor_Remarks,UtilityRoom_NoSuppliesOnFloor_FuDate,UtilityRoom_NoSuppliesOnFloor_FuRemarks,UtilityRoom_SuppliesFromCeiling_Finding,UtilityRoom_SuppliesFromCeiling_ActionDone,UtilityRoom_SuppliesFromCeiling_Remarks,UtilityRoom_SuppliesFromCeiling_FuDate,UtilityRoom_SuppliesFromCeiling_FuRemarks,UtilityRoom_NoSuppliesUnderSink_Finding,UtilityRoom_NoSuppliesUnderSink_ActionDone,UtilityRoom_NoSuppliesUnderSink_Remarks,UtilityRoom_NoSuppliesUnderSink_FuDate,UtilityRoom_NoSuppliesUnderSink_FuRemarks,UtilityRoom_NoSuppliesInBathrooms_Finding,UtilityRoom_NoSuppliesInBathrooms_ActionDone,UtilityRoom_NoSuppliesInBathrooms_Remarks,UtilityRoom_NoSuppliesInBathrooms_FuDate,UtilityRoom_NoSuppliesInBathrooms_FuRemarks,UtilityRoom_FIFOStocks_Finding,UtilityRoom_FIFOStocks_ActionDone,UtilityRoom_FIFOStocks_Remarks,UtilityRoom_FIFOStocks_FuDate,UtilityRoom_FIFOStocks_FuRemarks,UtilityRoom_SuppliesNotExpired_Finding,UtilityRoom_SuppliesNotExpired_ActionDone,UtilityRoom_SuppliesNotExpired_Remarks,UtilityRoom_SuppliesNotExpired_FuDate,UtilityRoom_SuppliesNotExpired_FuRemarks,UtilityRoom_SterileTraysClean_Finding,UtilityRoom_SterileTraysClean_ActionDone,UtilityRoom_SterileTraysClean_Remarks,UtilityRoom_SterileTraysClean_FuDate,UtilityRoom_SterileTraysClean_FuRemarks,SoiledRoom_FloorsWallsClean_Finding,SoiledRoom_FloorsWallsClean_ActionDone,SoiledRoom_FloorsWallsClean_Remarks,SoiledRoom_FloorsWallsClean_FuDate,SoiledRoom_FloorsWallsClean_FuRemarks,SoiledRoom_NoPatientSupplies_Finding,SoiledRoom_NoPatientSupplies_ActionDone,SoiledRoom_NoPatientSupplies_Remarks,SoiledRoom_NoPatientSupplies_FuDate,SoiledRoom_NoPatientSupplies_FuRemarks,SoiledRoom_LinenBagged_Finding,SoiledRoom_LinenBagged_ActionDone,SoiledRoom_LinenBagged_Remarks,SoiledRoom_LinenBagged_FuDate,SoiledRoom_LinenBagged_FuRemarks,WasteMgmt_ColorBinsAvailable_Finding,WasteMgmt_ColorBinsAvailable_ActionDone,WasteMgmt_ColorBinsAvailable_Remarks,WasteMgmt_ColorBinsAvailable_FuDate,WasteMgmt_ColorBinsAvailable_FuRemarks,WasteMgmt_ProperDisposal_Finding,WasteMgmt_ProperDisposal_ActionDone,WasteMgmt_ProperDisposal_Remarks,WasteMgmt_ProperDisposal_FuDate,WasteMgmt_ProperDisposal_FuRemarks,WasteMgmt_BinsNotOverfilled_Finding,WasteMgmt_BinsNotOverfilled_ActionDone,WasteMgmt_BinsNotOverfilled_Remarks,WasteMgmt_BinsNotOverfilled_FuDate,WasteMgmt_BinsNotOverfilled_FuRemarks,WasteMgmt_BinsClean_Finding,WasteMgmt_BinsClean_ActionDone,WasteMgmt_BinsClean_Remarks,WasteMgmt_BinsClean_FuDate,WasteMgmt_BinsClean_FuRemarks,WasteMgmt_SharpsLabeled_Finding,WasteMgmt_SharpsLabeled_ActionDone,WasteMgmt_SharpsLabeled_Remarks,WasteMgmt_SharpsLabeled_FuDate,WasteMgmt_SharpsLabeled_FuRemarks,WasteMgmt_SharpsNotOverfilled_Finding,WasteMgmt_SharpsNotOverfilled_ActionDone,WasteMgmt_SharpsNotOverfilled_Remarks,WasteMgmt_SharpsNotOverfilled_FuDate,WasteMgmt_SharpsNotOverfilled_FuRemarks,Refrigerator_TempChecked_Finding,Refrigerator_TempChecked_ActionDone,Refrigerator_TempChecked_Remarks,Refrigerator_TempChecked_FuDate,Refrigerator_TempChecked_FuRemarks,Refrigerator_Dedicated_Finding,Refrigerator_Dedicated_ActionDone,Refrigerator_Dedicated_Remarks,Refrigerator_Dedicated_FuDate,Refrigerator_Dedicated_FuRemarks,Medications_NoExpired_Finding,Medications_NoExpired_ActionDone,Medications_NoExpired_Remarks,Medications_NoExpired_FuDate,Medications_NoExpired_FuRemarks,Medications_VialsDated_Finding,Medications_VialsDated_ActionDone,Medications_VialsDated_Remarks,Medications_VialsDated_FuDate,Medications_VialsDated_FuRemarks,Medications_VaccinesStored_Finding,Medications_VaccinesStored_ActionDone,Medications_VaccinesStored_Remarks,Medications_VaccinesStored_FuDate,Medications_VaccinesStored_FuRemarks,Medications_NoLooseNeedles_Finding,Medications_NoLooseNeedles_ActionDone,Medications_NoLooseNeedles_Remarks,Medications_NoLooseNeedles_FuDate,Medications_NoLooseNeedles_FuRemarks,Misc_SpecimensLabeled_Finding,Misc_SpecimensLabeled_ActionDone,Misc_SpecimensLabeled_Remarks,Misc_SpecimensLabeled_FuDate,Misc_SpecimensLabeled_FuRemarks,Misc_StaffHygienePPE_Finding,Misc_StaffHygienePPE_ActionDone,Misc_StaffHygienePPE_Remarks,Misc_StaffHygienePPE_FuDate,Misc_StaffHygienePPE_FuRemarks,UnitAreaStaffSignature,FollowUpDate")] EnvironmentalzControl environmentalzControl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(environmentalzControl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(environmentalzControl);
        }

        // GET: EnvironmentalzControls/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentalzControl = await _context.EnvironmentalzControl.FindAsync(id);
            if (environmentalzControl == null)
            {
                return NotFound();
            }
            return View(environmentalzControl);
        }

        // POST: EnvironmentalzControls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AreaOrUnit,DateAndTimeOfMonitoring,AssessedBy,PatientExamRoom_FloorsWallsClean_Finding,PatientExamRoom_FloorsWallsClean_ActionDone,PatientExamRoom_FloorsWallsClean_Remarks,PatientExamRoom_FloorsWallsClean_FuDate,PatientExamRoom_FloorsWallsClean_FuRemarks,PatientExamRoom_CurtainsClean_Finding,PatientExamRoom_CurtainsClean_ActionDone,PatientExamRoom_CurtainsClean_Remarks,PatientExamRoom_CurtainsClean_FuDate,PatientExamRoom_CurtainsClean_FuRemarks,PatientExamRoom_SinkClean_Finding,PatientExamRoom_SinkClean_ActionDone,PatientExamRoom_SinkClean_Remarks,PatientExamRoom_SinkClean_FuDate,PatientExamRoom_SinkClean_FuRemarks,PatientExamRoom_SoapDispenser_Finding,PatientExamRoom_SoapDispenser_ActionDone,PatientExamRoom_SoapDispenser_Remarks,PatientExamRoom_SoapDispenser_FuDate,PatientExamRoom_SoapDispenser_FuRemarks,PatientExamRoom_AlcoholSanitizers_Finding,PatientExamRoom_AlcoholSanitizers_ActionDone,PatientExamRoom_AlcoholSanitizers_Remarks,PatientExamRoom_AlcoholSanitizers_FuDate,PatientExamRoom_AlcoholSanitizers_FuRemarks,PatientExamRoom_PPEAvailable_Finding,PatientExamRoom_PPEAvailable_ActionDone,PatientExamRoom_PPEAvailable_Remarks,PatientExamRoom_PPEAvailable_FuDate,PatientExamRoom_PPEAvailable_FuRemarks,PatientExamRoom_NoSuppliesUnderSinks_Finding,PatientExamRoom_NoSuppliesUnderSinks_ActionDone,PatientExamRoom_NoSuppliesUnderSinks_Remarks,PatientExamRoom_NoSuppliesUnderSinks_FuDate,PatientExamRoom_NoSuppliesUnderSinks_FuRemarks,PatientExamRoom_NoFoodInCareAreas_Finding,PatientExamRoom_NoFoodInCareAreas_ActionDone,PatientExamRoom_NoFoodInCareAreas_Remarks,PatientExamRoom_NoFoodInCareAreas_FuDate,PatientExamRoom_NoFoodInCareAreas_FuRemarks,WorkArea_ChartsStored_Finding,WorkArea_ChartsStored_ActionDone,WorkArea_ChartsStored_Remarks,WorkArea_ChartsStored_FuDate,WorkArea_ChartsStored_FuRemarks,WorkArea_NoFoodOrDrinks_Finding,WorkArea_NoFoodOrDrinks_ActionDone,WorkArea_NoFoodOrDrinks_Remarks,WorkArea_NoFoodOrDrinks_FuDate,WorkArea_NoFoodOrDrinks_FuRemarks,WorkArea_FansClean_Finding,WorkArea_FansClean_ActionDone,WorkArea_FansClean_Remarks,WorkArea_FansClean_FuDate,WorkArea_FansClean_FuRemarks,WorkArea_ACClean_Finding,WorkArea_ACClean_ActionDone,WorkArea_ACClean_Remarks,WorkArea_ACClean_FuDate,WorkArea_ACClean_FuRemarks,WorkArea_ExhaustFansClean_Finding,WorkArea_ExhaustFansClean_ActionDone,WorkArea_ExhaustFansClean_Remarks,WorkArea_ExhaustFansClean_FuDate,WorkArea_ExhaustFansClean_FuRemarks,WorkArea_CeilingClean_Finding,WorkArea_CeilingClean_ActionDone,WorkArea_CeilingClean_Remarks,WorkArea_CeilingClean_FuDate,WorkArea_CeilingClean_FuRemarks,WorkArea_WallsClean_Finding,WorkArea_WallsClean_ActionDone,WorkArea_WallsClean_Remarks,WorkArea_WallsClean_FuDate,WorkArea_WallsClean_FuRemarks,WorkArea_CountersClean_Finding,WorkArea_CountersClean_ActionDone,WorkArea_CountersClean_Remarks,WorkArea_CountersClean_FuDate,WorkArea_CountersClean_FuRemarks,WorkArea_FloorsClean_Finding,WorkArea_FloorsClean_ActionDone,WorkArea_FloorsClean_Remarks,WorkArea_FloorsClean_FuDate,WorkArea_FloorsClean_FuRemarks,WorkArea_DoorsWindowsClean_Finding,WorkArea_DoorsWindowsClean_ActionDone,WorkArea_DoorsWindowsClean_Remarks,WorkArea_DoorsWindowsClean_FuDate,WorkArea_DoorsWindowsClean_FuRemarks,WorkArea_CleanBathroom_Finding,WorkArea_CleanBathroom_ActionDone,WorkArea_CleanBathroom_Remarks,WorkArea_CleanBathroom_FuDate,WorkArea_CleanBathroom_FuRemarks,Hallway_FloorsWallsClean_Finding,Hallway_FloorsWallsClean_ActionDone,Hallway_FloorsWallsClean_Remarks,Hallway_FloorsWallsClean_FuDate,Hallway_FloorsWallsClean_FuRemarks,Hallway_NoObstruction_Finding,Hallway_NoObstruction_ActionDone,Hallway_NoObstruction_Remarks,Hallway_NoObstruction_FuDate,Hallway_NoObstruction_FuRemarks,WaitingArea_FurnitureClean_Finding,WaitingArea_FurnitureClean_ActionDone,WaitingArea_FurnitureClean_Remarks,WaitingArea_FurnitureClean_FuDate,WaitingArea_FurnitureClean_FuRemarks,WaitingArea_TrashDisposed_Finding,WaitingArea_TrashDisposed_ActionDone,WaitingArea_TrashDisposed_Remarks,WaitingArea_TrashDisposed_FuDate,WaitingArea_TrashDisposed_FuRemarks,WaitingArea_FoodInDesignatedArea_Finding,WaitingArea_FoodInDesignatedArea_ActionDone,WaitingArea_FoodInDesignatedArea_Remarks,WaitingArea_FoodInDesignatedArea_FuDate,WaitingArea_FoodInDesignatedArea_FuRemarks,Equipment_StorageClean_Finding,Equipment_StorageClean_ActionDone,Equipment_StorageClean_Remarks,Equipment_StorageClean_FuDate,Equipment_StorageClean_FuRemarks,UtilityRoom_CleanSoiledSeparation_Finding,UtilityRoom_CleanSoiledSeparation_ActionDone,UtilityRoom_CleanSoiledSeparation_Remarks,UtilityRoom_CleanSoiledSeparation_FuDate,UtilityRoom_CleanSoiledSeparation_FuRemarks,UtilityRoom_FloorsWallsClean_Finding,UtilityRoom_FloorsWallsClean_ActionDone,UtilityRoom_FloorsWallsClean_Remarks,UtilityRoom_FloorsWallsClean_FuDate,UtilityRoom_FloorsWallsClean_FuRemarks,UtilityRoom_NoSuppliesOnFloor_Finding,UtilityRoom_NoSuppliesOnFloor_ActionDone,UtilityRoom_NoSuppliesOnFloor_Remarks,UtilityRoom_NoSuppliesOnFloor_FuDate,UtilityRoom_NoSuppliesOnFloor_FuRemarks,UtilityRoom_SuppliesFromCeiling_Finding,UtilityRoom_SuppliesFromCeiling_ActionDone,UtilityRoom_SuppliesFromCeiling_Remarks,UtilityRoom_SuppliesFromCeiling_FuDate,UtilityRoom_SuppliesFromCeiling_FuRemarks,UtilityRoom_NoSuppliesUnderSink_Finding,UtilityRoom_NoSuppliesUnderSink_ActionDone,UtilityRoom_NoSuppliesUnderSink_Remarks,UtilityRoom_NoSuppliesUnderSink_FuDate,UtilityRoom_NoSuppliesUnderSink_FuRemarks,UtilityRoom_NoSuppliesInBathrooms_Finding,UtilityRoom_NoSuppliesInBathrooms_ActionDone,UtilityRoom_NoSuppliesInBathrooms_Remarks,UtilityRoom_NoSuppliesInBathrooms_FuDate,UtilityRoom_NoSuppliesInBathrooms_FuRemarks,UtilityRoom_FIFOStocks_Finding,UtilityRoom_FIFOStocks_ActionDone,UtilityRoom_FIFOStocks_Remarks,UtilityRoom_FIFOStocks_FuDate,UtilityRoom_FIFOStocks_FuRemarks,UtilityRoom_SuppliesNotExpired_Finding,UtilityRoom_SuppliesNotExpired_ActionDone,UtilityRoom_SuppliesNotExpired_Remarks,UtilityRoom_SuppliesNotExpired_FuDate,UtilityRoom_SuppliesNotExpired_FuRemarks,UtilityRoom_SterileTraysClean_Finding,UtilityRoom_SterileTraysClean_ActionDone,UtilityRoom_SterileTraysClean_Remarks,UtilityRoom_SterileTraysClean_FuDate,UtilityRoom_SterileTraysClean_FuRemarks,SoiledRoom_FloorsWallsClean_Finding,SoiledRoom_FloorsWallsClean_ActionDone,SoiledRoom_FloorsWallsClean_Remarks,SoiledRoom_FloorsWallsClean_FuDate,SoiledRoom_FloorsWallsClean_FuRemarks,SoiledRoom_NoPatientSupplies_Finding,SoiledRoom_NoPatientSupplies_ActionDone,SoiledRoom_NoPatientSupplies_Remarks,SoiledRoom_NoPatientSupplies_FuDate,SoiledRoom_NoPatientSupplies_FuRemarks,SoiledRoom_LinenBagged_Finding,SoiledRoom_LinenBagged_ActionDone,SoiledRoom_LinenBagged_Remarks,SoiledRoom_LinenBagged_FuDate,SoiledRoom_LinenBagged_FuRemarks,WasteMgmt_ColorBinsAvailable_Finding,WasteMgmt_ColorBinsAvailable_ActionDone,WasteMgmt_ColorBinsAvailable_Remarks,WasteMgmt_ColorBinsAvailable_FuDate,WasteMgmt_ColorBinsAvailable_FuRemarks,WasteMgmt_ProperDisposal_Finding,WasteMgmt_ProperDisposal_ActionDone,WasteMgmt_ProperDisposal_Remarks,WasteMgmt_ProperDisposal_FuDate,WasteMgmt_ProperDisposal_FuRemarks,WasteMgmt_BinsNotOverfilled_Finding,WasteMgmt_BinsNotOverfilled_ActionDone,WasteMgmt_BinsNotOverfilled_Remarks,WasteMgmt_BinsNotOverfilled_FuDate,WasteMgmt_BinsNotOverfilled_FuRemarks,WasteMgmt_BinsClean_Finding,WasteMgmt_BinsClean_ActionDone,WasteMgmt_BinsClean_Remarks,WasteMgmt_BinsClean_FuDate,WasteMgmt_BinsClean_FuRemarks,WasteMgmt_SharpsLabeled_Finding,WasteMgmt_SharpsLabeled_ActionDone,WasteMgmt_SharpsLabeled_Remarks,WasteMgmt_SharpsLabeled_FuDate,WasteMgmt_SharpsLabeled_FuRemarks,WasteMgmt_SharpsNotOverfilled_Finding,WasteMgmt_SharpsNotOverfilled_ActionDone,WasteMgmt_SharpsNotOverfilled_Remarks,WasteMgmt_SharpsNotOverfilled_FuDate,WasteMgmt_SharpsNotOverfilled_FuRemarks,Refrigerator_TempChecked_Finding,Refrigerator_TempChecked_ActionDone,Refrigerator_TempChecked_Remarks,Refrigerator_TempChecked_FuDate,Refrigerator_TempChecked_FuRemarks,Refrigerator_Dedicated_Finding,Refrigerator_Dedicated_ActionDone,Refrigerator_Dedicated_Remarks,Refrigerator_Dedicated_FuDate,Refrigerator_Dedicated_FuRemarks,Medications_NoExpired_Finding,Medications_NoExpired_ActionDone,Medications_NoExpired_Remarks,Medications_NoExpired_FuDate,Medications_NoExpired_FuRemarks,Medications_VialsDated_Finding,Medications_VialsDated_ActionDone,Medications_VialsDated_Remarks,Medications_VialsDated_FuDate,Medications_VialsDated_FuRemarks,Medications_VaccinesStored_Finding,Medications_VaccinesStored_ActionDone,Medications_VaccinesStored_Remarks,Medications_VaccinesStored_FuDate,Medications_VaccinesStored_FuRemarks,Medications_NoLooseNeedles_Finding,Medications_NoLooseNeedles_ActionDone,Medications_NoLooseNeedles_Remarks,Medications_NoLooseNeedles_FuDate,Medications_NoLooseNeedles_FuRemarks,Misc_SpecimensLabeled_Finding,Misc_SpecimensLabeled_ActionDone,Misc_SpecimensLabeled_Remarks,Misc_SpecimensLabeled_FuDate,Misc_SpecimensLabeled_FuRemarks,Misc_StaffHygienePPE_Finding,Misc_StaffHygienePPE_ActionDone,Misc_StaffHygienePPE_Remarks,Misc_StaffHygienePPE_FuDate,Misc_StaffHygienePPE_FuRemarks,UnitAreaStaffSignature,FollowUpDate")] EnvironmentalzControl environmentalzControl)
        {
            if (id != environmentalzControl.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(environmentalzControl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnvironmentalzControlExists(environmentalzControl.Id))
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
            return View(environmentalzControl);
        }

        // GET: EnvironmentalzControls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environmentalzControl = await _context.EnvironmentalzControl
                .FirstOrDefaultAsync(m => m.Id == id);
            if (environmentalzControl == null)
            {
                return NotFound();
            }

            return View(environmentalzControl);
        }

        // POST: EnvironmentalzControls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var environmentalzControl = await _context.EnvironmentalzControl.FindAsync(id);
            if (environmentalzControl != null)
            {
                _context.EnvironmentalzControl.Remove(environmentalzControl);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnvironmentalzControlExists(int id)
        {
            return _context.EnvironmentalzControl.Any(e => e.Id == id);
        }
    }
}
