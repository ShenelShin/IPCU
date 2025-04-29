namespace IPCU.Models
{

    using System;
    using System.ComponentModel.DataAnnotations;

    namespace InfectionControl.Models
    {
        public class InfectionControlMonitoringForm
        {
            [Key]
            public int Id { get; set; }
            public string Fname { get; set; } // First Name
            public string Lname { get; set; } // Last Name
            public string? Mname { get; set; } // Middle Name
            public string EmployeeId { get; set; } // New field
            public string AreaOrUnit { get; set; }
            public DateTime DateAndTimeOfMonitoring { get; set; }
            public string AssessedBy { get; set; }

            // Patient Exam Room
            public string PatientExamRoom_FloorsWallsClean_Finding { get; set; }
            public string PatientExamRoom_FloorsWallsClean_ActionDone { get; set; }
            public string? PatientExamRoom_FloorsWallsClean_Remarks { get; set; }
            public DateTime? PatientExamRoom_FloorsWallsClean_FuDate { get; set; }
            public string? PatientExamRoom_FloorsWallsClean_FuRemarks { get; set; }

            public string PatientExamRoom_CurtainsClean_Finding { get; set; }
            public string PatientExamRoom_CurtainsClean_ActionDone { get; set; }
            public string? PatientExamRoom_CurtainsClean_Remarks { get; set; }
            public DateTime? PatientExamRoom_CurtainsClean_FuDate { get; set; }
            public string? PatientExamRoom_CurtainsClean_FuRemarks { get; set; }

            public string PatientExamRoom_SinkClean_Finding { get; set; }
            public string PatientExamRoom_SinkClean_ActionDone { get; set; }
            public string? PatientExamRoom_SinkClean_Remarks { get; set; }
            public DateTime? PatientExamRoom_SinkClean_FuDate { get; set; }
            public string? PatientExamRoom_SinkClean_FuRemarks { get; set; }

            public string PatientExamRoom_SoapDispenser_Finding { get; set; }
            public string PatientExamRoom_SoapDispenser_ActionDone { get; set; }
            public string? PatientExamRoom_SoapDispenser_Remarks { get; set; }
            public DateTime? PatientExamRoom_SoapDispenser_FuDate { get; set; }
            public string? PatientExamRoom_SoapDispenser_FuRemarks { get; set; }

            public string PatientExamRoom_AlcoholSanitizers_Finding { get; set; }
            public string PatientExamRoom_AlcoholSanitizers_ActionDone { get; set; }
            public string? PatientExamRoom_AlcoholSanitizers_Remarks { get; set; }
            public DateTime? PatientExamRoom_AlcoholSanitizers_FuDate { get; set; }
            public string? PatientExamRoom_AlcoholSanitizers_FuRemarks { get; set; }

            public string PatientExamRoom_PPEAvailable_Finding { get; set; }
            public string PatientExamRoom_PPEAvailable_ActionDone { get; set; }
            public string? PatientExamRoom_PPEAvailable_Remarks { get; set; }
            public DateTime? PatientExamRoom_PPEAvailable_FuDate { get; set; }
            public string? PatientExamRoom_PPEAvailable_FuRemarks { get; set; }

            public string PatientExamRoom_NoSuppliesUnderSinks_Finding { get; set; }
            public string PatientExamRoom_NoSuppliesUnderSinks_ActionDone { get; set; }
            public string? PatientExamRoom_NoSuppliesUnderSinks_Remarks { get; set; }
            public DateTime? PatientExamRoom_NoSuppliesUnderSinks_FuDate { get; set; }
            public string? PatientExamRoom_NoSuppliesUnderSinks_FuRemarks { get; set; }


            public string PatientExamRoom_NoFoodInCareAreas_Finding { get; set; }
            public string PatientExamRoom_NoFoodInCareAreas_ActionDone { get; set; }
            public string? PatientExamRoom_NoFoodInCareAreas_Remarks { get; set; }
            public DateTime? PatientExamRoom_NoFoodInCareAreas_FuDate { get; set; }
            public string? PatientExamRoom_NoFoodInCareAreas_FuRemarks { get; set; }

            // Work Area
            public string WorkArea_ChartsStored_Finding { get; set; }
            public string WorkArea_ChartsStored_ActionDone { get; set; }
            public string? WorkArea_ChartsStored_Remarks { get; set; }
            public DateTime? WorkArea_ChartsStored_FuDate { get; set; }
            public string? WorkArea_ChartsStored_FuRemarks { get; set; }

            public string WorkArea_NoFoodOrDrinks_Finding { get; set; }
            public string WorkArea_NoFoodOrDrinks_ActionDone { get; set; }
            public string? WorkArea_NoFoodOrDrinks_Remarks { get; set; }
            public DateTime? WorkArea_NoFoodOrDrinks_FuDate { get; set; }
            public string? WorkArea_NoFoodOrDrinks_FuRemarks { get; set; }

            public string WorkArea_FansClean_Finding { get; set; }
            public string WorkArea_FansClean_ActionDone { get; set; }
            public string? WorkArea_FansClean_Remarks { get; set; }
            public DateTime? WorkArea_FansClean_FuDate { get; set; }
            public string? WorkArea_FansClean_FuRemarks { get; set; }

            public string WorkArea_ACClean_Finding { get; set; }
            public string WorkArea_ACClean_ActionDone { get; set; }
            public string? WorkArea_ACClean_Remarks { get; set; }
            public DateTime? WorkArea_ACClean_FuDate { get; set; }
            public string? WorkArea_ACClean_FuRemarks { get; set; }

            public string WorkArea_ExhaustFansClean_Finding { get; set; }
            public string WorkArea_ExhaustFansClean_ActionDone { get; set; }
            public string? WorkArea_ExhaustFansClean_Remarks { get; set; }
            public DateTime? WorkArea_ExhaustFansClean_FuDate { get; set; }
            public string? WorkArea_ExhaustFansClean_FuRemarks { get; set; }

            public string WorkArea_CeilingClean_Finding { get; set; }
            public string WorkArea_CeilingClean_ActionDone { get; set; }
            public string? WorkArea_CeilingClean_Remarks { get; set; }
            public DateTime? WorkArea_CeilingClean_FuDate { get; set; }
            public string? WorkArea_CeilingClean_FuRemarks { get; set; }

            public string WorkArea_WallsClean_Finding { get; set; }
            public string WorkArea_WallsClean_ActionDone { get; set; }
            public string? WorkArea_WallsClean_Remarks { get; set; }
            public DateTime? WorkArea_WallsClean_FuDate { get; set; }
            public string? WorkArea_WallsClean_FuRemarks { get; set; }

            public string WorkArea_CountersClean_Finding { get; set; }
            public string WorkArea_CountersClean_ActionDone { get; set; }
            public string? WorkArea_CountersClean_Remarks { get; set; }
            public DateTime? WorkArea_CountersClean_FuDate { get; set; }
            public string? WorkArea_CountersClean_FuRemarks { get; set; }

            public string WorkArea_FloorsClean_Finding { get; set; }
            public string WorkArea_FloorsClean_ActionDone { get; set; }
            public string? WorkArea_FloorsClean_Remarks { get; set; }
            public DateTime? WorkArea_FloorsClean_FuDate { get; set; }
            public string? WorkArea_FloorsClean_FuRemarks { get; set; }

            public string WorkArea_DoorsWindowsClean_Finding { get; set; }
            public string WorkArea_DoorsWindowsClean_ActionDone { get; set; }
            public string? WorkArea_DoorsWindowsClean_Remarks { get; set; }
            public DateTime? WorkArea_DoorsWindowsClean_FuDate { get; set; }
            public string? WorkArea_DoorsWindowsClean_FuRemarks { get; set; }

            public string WorkArea_CleanBathroom_Finding { get; set; }
            public string WorkArea_CleanBathroom_ActionDone { get; set; }
            public string? WorkArea_CleanBathroom_Remarks { get; set; }
            public DateTime? WorkArea_CleanBathroom_FuDate { get; set; }
            public string? WorkArea_CleanBathroom_FuRemarks { get; set; }


            // Hallways
            public string Hallway_FloorsWallsClean_Finding { get; set; }
            public string Hallway_FloorsWallsClean_ActionDone { get; set; }
            public string? Hallway_FloorsWallsClean_Remarks { get; set; }
            public DateTime? Hallway_FloorsWallsClean_FuDate { get; set; }
            public string? Hallway_FloorsWallsClean_FuRemarks { get; set; }

            public string Hallway_NoObstruction_Finding { get; set; }
            public string Hallway_NoObstruction_ActionDone { get; set; }
            public string? Hallway_NoObstruction_Remarks { get; set; }
            public DateTime? Hallway_NoObstruction_FuDate { get; set; }
            public string? Hallway_NoObstruction_FuRemarks { get; set; }
            // Waiting Area
            public string WaitingArea_FurnitureClean_Finding { get; set; }
            public string WaitingArea_FurnitureClean_ActionDone { get; set; }
            public string? WaitingArea_FurnitureClean_Remarks { get; set; }
            public DateTime? WaitingArea_FurnitureClean_FuDate { get; set; }
            public string? WaitingArea_FurnitureClean_FuRemarks { get; set; }

            public string WaitingArea_TrashDisposed_Finding { get; set; }
            public string WaitingArea_TrashDisposed_ActionDone { get; set; }
            public string? WaitingArea_TrashDisposed_Remarks { get; set; }
            public DateTime? WaitingArea_TrashDisposed_FuDate { get; set; }
            public string? WaitingArea_TrashDisposed_FuRemarks { get; set; }

            public string WaitingArea_FoodInDesignatedArea_Finding { get; set; }
            public string WaitingArea_FoodInDesignatedArea_ActionDone { get; set; }
            public string? WaitingArea_FoodInDesignatedArea_Remarks { get; set; }
            public DateTime? WaitingArea_FoodInDesignatedArea_FuDate { get; set; }
            public string? WaitingArea_FoodInDesignatedArea_FuRemarks { get; set; }

            // Equipment
            public string Equipment_StorageClean_Finding { get; set; }
            public string Equipment_StorageClean_ActionDone { get; set; }
            public string? Equipment_StorageClean_Remarks { get; set; }
            public DateTime? Equipment_StorageClean_FuDate { get; set; }
            public string? Equipment_StorageClean_FuRemarks { get; set; }

            // Utility Room
            public string UtilityRoom_CleanSoiledSeparation_Finding { get; set; }
            public string UtilityRoom_CleanSoiledSeparation_ActionDone { get; set; }
            public string? UtilityRoom_CleanSoiledSeparation_Remarks { get; set; }
            public DateTime? UtilityRoom_CleanSoiledSeparation_FuDate { get; set; }
            public string? UtilityRoom_CleanSoiledSeparation_FuRemarks { get; set; }

            public string UtilityRoom_FloorsWallsClean_Finding { get; set; }
            public string UtilityRoom_FloorsWallsClean_ActionDone { get; set; }
            public string? UtilityRoom_FloorsWallsClean_Remarks { get; set; }
            public DateTime? UtilityRoom_FloorsWallsClean_FuDate { get; set; }
            public string? UtilityRoom_FloorsWallsClean_FuRemarks { get; set; }

            public string UtilityRoom_NoSuppliesOnFloor_Finding { get; set; }
            public string UtilityRoom_NoSuppliesOnFloor_ActionDone { get; set; }
            public string? UtilityRoom_NoSuppliesOnFloor_Remarks { get; set; }
            public DateTime? UtilityRoom_NoSuppliesOnFloor_FuDate { get; set; }
            public string? UtilityRoom_NoSuppliesOnFloor_FuRemarks { get; set; }

            public string UtilityRoom_SuppliesFromCeiling_Finding { get; set; }
            public string UtilityRoom_SuppliesFromCeiling_ActionDone { get; set; }
            public string? UtilityRoom_SuppliesFromCeiling_Remarks { get; set; }
            public DateTime? UtilityRoom_SuppliesFromCeiling_FuDate { get; set; }
            public string? UtilityRoom_SuppliesFromCeiling_FuRemarks { get; set; }

            public string UtilityRoom_NoSuppliesUnderSink_Finding { get; set; }
            public string UtilityRoom_NoSuppliesUnderSink_ActionDone { get; set; }
            public string? UtilityRoom_NoSuppliesUnderSink_Remarks { get; set; }
            public DateTime? UtilityRoom_NoSuppliesUnderSink_FuDate { get; set; }
            public string? UtilityRoom_NoSuppliesUnderSink_FuRemarks { get; set; }

            public string UtilityRoom_NoSuppliesInBathrooms_Finding { get; set; }
            public string UtilityRoom_NoSuppliesInBathrooms_ActionDone { get; set; }
            public string? UtilityRoom_NoSuppliesInBathrooms_Remarks { get; set; }
            public DateTime? UtilityRoom_NoSuppliesInBathrooms_FuDate { get; set; }
            public string? UtilityRoom_NoSuppliesInBathrooms_FuRemarks { get; set; }

            public string UtilityRoom_FIFOStocks_Finding { get; set; }
            public string UtilityRoom_FIFOStocks_ActionDone { get; set; }
            public string? UtilityRoom_FIFOStocks_Remarks { get; set; }
            public DateTime? UtilityRoom_FIFOStocks_FuDate { get; set; }
            public string? UtilityRoom_FIFOStocks_FuRemarks { get; set; }

            public string UtilityRoom_SuppliesNotExpired_Finding { get; set; }
            public string UtilityRoom_SuppliesNotExpired_ActionDone { get; set; }
            public string? UtilityRoom_SuppliesNotExpired_Remarks { get; set; }
            public DateTime? UtilityRoom_SuppliesNotExpired_FuDate { get; set; }
            public string? UtilityRoom_SuppliesNotExpired_FuRemarks { get; set; }

            public string UtilityRoom_SterileTraysClean_Finding { get; set; }
            public string UtilityRoom_SterileTraysClean_ActionDone { get; set; }
            public string? UtilityRoom_SterileTraysClean_Remarks { get; set; }
            public DateTime? UtilityRoom_SterileTraysClean_FuDate { get; set; }
            public string? UtilityRoom_SterileTraysClean_FuRemarks { get; set; }

            // Soiled Utility Rooms
            public string SoiledRoom_FloorsWallsClean_Finding { get; set; }
            public string SoiledRoom_FloorsWallsClean_ActionDone { get; set; }
            public string? SoiledRoom_FloorsWallsClean_Remarks { get; set; }
            public DateTime? SoiledRoom_FloorsWallsClean_FuDate { get; set; }
            public string? SoiledRoom_FloorsWallsClean_FuRemarks { get; set; }

            public string SoiledRoom_NoPatientSupplies_Finding { get; set; }
            public string SoiledRoom_NoPatientSupplies_ActionDone { get; set; }
            public string? SoiledRoom_NoPatientSupplies_Remarks { get; set; }
            public DateTime? SoiledRoom_NoPatientSupplies_FuDate { get; set; }
            public string? SoiledRoom_NoPatientSupplies_FuRemarks { get; set; }

            public string SoiledRoom_LinenBagged_Finding { get; set; }
            public string SoiledRoom_LinenBagged_ActionDone { get; set; }
            public string? SoiledRoom_LinenBagged_Remarks { get; set; }
            public DateTime? SoiledRoom_LinenBagged_FuDate { get; set; }
            public string? SoiledRoom_LinenBagged_FuRemarks { get; set; }


            // Waste Management
            public string WasteMgmt_ColorBinsAvailable_Finding { get; set; }
            public string WasteMgmt_ColorBinsAvailable_ActionDone { get; set; }
            public string? WasteMgmt_ColorBinsAvailable_Remarks { get; set; }
            public DateTime? WasteMgmt_ColorBinsAvailable_FuDate { get; set; }
            public string? WasteMgmt_ColorBinsAvailable_FuRemarks { get; set; }

            public string WasteMgmt_ProperDisposal_Finding { get; set; }
            public string WasteMgmt_ProperDisposal_ActionDone { get; set; }
            public string? WasteMgmt_ProperDisposal_Remarks { get; set; }
            public DateTime? WasteMgmt_ProperDisposal_FuDate { get; set; }
            public string? WasteMgmt_ProperDisposal_FuRemarks { get; set; }

            public string WasteMgmt_BinsNotOverfilled_Finding { get; set; }
            public string WasteMgmt_BinsNotOverfilled_ActionDone { get; set; }
            public string? WasteMgmt_BinsNotOverfilled_Remarks { get; set; }
            public DateTime? WasteMgmt_BinsNotOverfilled_FuDate { get; set; }
            public string? WasteMgmt_BinsNotOverfilled_FuRemarks { get; set; }

            public string WasteMgmt_BinsClean_Finding { get; set; }
            public string WasteMgmt_BinsClean_ActionDone { get; set; }
            public string? WasteMgmt_BinsClean_Remarks { get; set; }
            public DateTime? WasteMgmt_BinsClean_FuDate { get; set; }
            public string? WasteMgmt_BinsClean_FuRemarks { get; set; }

            public string WasteMgmt_SharpsLabeled_Finding { get; set; }
            public string WasteMgmt_SharpsLabeled_ActionDone { get; set; }
            public string? WasteMgmt_SharpsLabeled_Remarks { get; set; }
            public DateTime? WasteMgmt_SharpsLabeled_FuDate { get; set; }
            public string? WasteMgmt_SharpsLabeled_FuRemarks { get; set; }

            public string WasteMgmt_SharpsNotOverfilled_Finding { get; set; }
            public string WasteMgmt_SharpsNotOverfilled_ActionDone { get; set; }
            public string? WasteMgmt_SharpsNotOverfilled_Remarks { get; set; }
            public DateTime? WasteMgmt_SharpsNotOverfilled_FuDate { get; set; }
            public string? WasteMgmt_SharpsNotOverfilled_FuRemarks { get; set; }


            // Refrigerators or Freezers
            public string Refrigerator_TempChecked_Finding { get; set; }
            public string Refrigerator_TempChecked_ActionDone { get; set; }
            public string? Refrigerator_TempChecked_Remarks { get; set; }
            public DateTime? Refrigerator_TempChecked_FuDate { get; set; }
            public string? Refrigerator_TempChecked_FuRemarks { get; set; }

            public string Refrigerator_Dedicated_Finding { get; set; }
            public string Refrigerator_Dedicated_ActionDone { get; set; }
            public string? Refrigerator_Dedicated_Remarks { get; set; }
            public DateTime? Refrigerator_Dedicated_FuDate { get; set; }
            public string? Refrigerator_Dedicated_FuRemarks { get; set; }

            // Medications
            public string Medications_NoExpired_Finding { get; set; }
            public string Medications_NoExpired_ActionDone { get; set; }
            public string? Medications_NoExpired_Remarks { get; set; }
            public DateTime? Medications_NoExpired_FuDate { get; set; }
            public string? Medications_NoExpired_FuRemarks { get; set; }

            public string Medications_VialsDated_Finding { get; set; }
            public string Medications_VialsDated_ActionDone { get; set; }
            public string? Medications_VialsDated_Remarks { get; set; }
            public DateTime? Medications_VialsDated_FuDate { get; set; }
            public string? Medications_VialsDated_FuRemarks { get; set; }

            public string Medications_VaccinesStored_Finding { get; set; }
            public string Medications_VaccinesStored_ActionDone { get; set; }
            public string? Medications_VaccinesStored_Remarks { get; set; }
            public DateTime? Medications_VaccinesStored_FuDate { get; set; }
            public string? Medications_VaccinesStored_FuRemarks { get; set; }

            public string Medications_NoLooseNeedles_Finding { get; set; }
            public string Medications_NoLooseNeedles_ActionDone { get; set; }
            public string? Medications_NoLooseNeedles_Remarks { get; set; }
            public DateTime? Medications_NoLooseNeedles_FuDate { get; set; }
            public string? Medications_NoLooseNeedles_FuRemarks { get; set; }

            // Miscellaneous
            public string Misc_SpecimensLabeled_Finding { get; set; }
            public string Misc_SpecimensLabeled_ActionDone { get; set; }
            public string? Misc_SpecimensLabeled_Remarks { get; set; }
            public DateTime? Misc_SpecimensLabeled_FuDate { get; set; }
            public string? Misc_SpecimensLabeled_FuRemarks { get; set; }

            public string Misc_StaffHygienePPE_Finding { get; set; }
            public string Misc_StaffHygienePPE_ActionDone { get; set; }
            public string? Misc_StaffHygienePPE_Remarks { get; set; }
            public DateTime? Misc_StaffHygienePPE_FuDate { get; set; }
            public string? Misc_StaffHygienePPE_FuRemarks { get; set; }

            public string UnitAreaStaffSignature { get; set; }
            public DateTime? FollowUpDate { get; set; }


        }
    }
}

