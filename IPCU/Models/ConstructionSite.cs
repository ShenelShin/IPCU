using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class ConstructionSite
    {
        [Key]
        public int CSID { get; set; }

        public string ProjectReferenceNumber { get; set; }
        public string ProjectNameAndDescription { get; set; }

        public string ContractorRepresentativeName { get; set; }
        public string TelephoneOrMobileNumber { get; set; }

        public string SpecificSiteOfActivity { get; set; }

        public string ScopeOfWork { get; set; }

        public DateTime? ProjectStartDate { get; set; }
        public string EstimatedDuration { get; set; }




        // barrier
        public string BarrierICRA { get; set; }
        public string? BarrierICRAComments { get; set; }

        public string BarrierDoorsSealedSelection { get; set; }
        public string? BarrierDoorsSealedComments { get; set; }

        public string BarrierFloorCleanSelection { get; set; }
        public string? BarrierFloorCleanComments { get; set; }

        public string BarrierWalkOffMatsSelection { get; set; }
        public string? BarrierWalkOffMatsComments { get; set; }

        public string BarrierTapeAdheringSelection { get; set; }
        public string? BarrierTapeAdheringComments { get; set; }




        // Airhandling

        public string AirhandlingWindowsClosedBehindBarrierSelection { get; set; }
        public string? AirhandlingWindowsClosedBehindBarrierComments { get; set; }

        public string AirhandlingNegativeAirMonitoredSelection { get; set; }
        public string? AirhandlingNegativeAirMonitoredComments { get; set; }

        public string AirHandlingUnitRunningSelection { get; set; }
        public string? AirHandlingUnitRunningComments { get; set; }

        public string AirhandlingMaintenanceLabelVisibleSelection { get; set; }
        public string? AirhandlingMaintenanceLabelVisibleComments { get; set; }

        public string AirhandlingAirExhaustedToAppropriateAreaSelection { get; set; }
        public string? AirhandlingAirExhaustedToAppropriateAreaComments { get; set; }





        // Projectarea

        public string ProjectareaHEPAFilteredVacuumOnJobsiteSelection { get; set; }
        public string? ProjectareaHEPAFilteredVacuumOnJobsiteComments { get; set; }

        public string ProjectareaDebrisRemovedInCoveredContainerDailySelection { get; set; }
        public string? ProjectareaDebrisRemovedInCoveredContainerDailyComments { get; set; }

        public string ProjectareaDesignatedConstructionRouteOrMapPostedSelection { get; set; }
        public string? ProjectareaDesignatedConstructionRouteOrMapPostedComments { get; set; }

        public string ProjectareaTrashInAppropriateContainerSelection { get; set; }
        public string? ProjectareaTrashInAppropriateContainerComments { get; set; }

        public string ProjectareaRoutineCleaningDoneOnSiteSelection { get; set; }
        public string? ProjectareaRoutineCleaningDoneOnSiteComments { get; set; }

        public string ProjectareaAirVentsSealedOrDuctworkCappedSelection { get; set; }
        public string? ProjectareaAirVentsSealedOrDuctworkCappedComments { get; set; }










        // Trafficcontrol

        public string TrafficcontrolRestrictedToConstructionWorkersSelection { get; set; }
        public string? TrafficcontrolRestrictedToConstructionWorkersComments { get; set; }

        public string TrafficcontrolDoorsAndExitsFreeOfDebrisSelection { get; set; }
        public string? TrafficcontrolDoorsAndExitsFreeOfDebrisComments { get; set; }




        //Dresscode

        public string? DresscodeProtectiveClothingWornSelection { get; set; }
        public string? DresscodeProtectiveClothingWornComments { get; set; }

        public string? DresscodeWorkersClothingCleanUponExitingSelection { get; set; }
        public string? DresscodeWorkersClothingCleanUponExitingComments { get; set; }





        public string? ContractorSign { get; set; }
        public string? EngineeringSign { get; set; }
        public string? ICPSign { get; set; }
        public string? UnitAreaRep { get; set; }





    }
}
