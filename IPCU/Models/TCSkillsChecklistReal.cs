using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class TCSkillsChecklistReal
    {
        [Key]
        public int Id { get; set; }
        public string? Fname { get; set; } // First Name
        public string? Lname { get; set; } // Last Name
        public string? Mname { get; set; } // Middle Name
        public string? EmployeeId { get; set; } // New field

        public string Area { get; set; }
        public string ObserverName { get; set; }
        public DateTime Date { get; set; }            // 2024-04-05  // 10:45

        // Section 1: PREPARATION AND SETUP
        [Display(Name = "Prepare equipment and cleaning cart with everything needed")]
        public bool IsEquipmentAndCartPrepared { get; set; }

        [Display(Name = "Prepare properly diluted cleaning solution (1:100)")]
        public bool IsCleaningSolutionPrepared { get; set; }

        [Display(Name = "Don appropriate attire and Personal and Protective Equipment (PPE)")]
        public bool IsProperAttireAndPPEWorn { get; set; }

        // Section 2: BASIC PROCEDURES
        [Display(Name = "Perform hand hygiene and don gloves before entering the room")]
        public bool IsHandHygieneAndGlovesDone { get; set; }

        [Display(Name = "Be aware of signage that indicates special precaution")]
        public bool IsSignageChecked { get; set; }

        // STEP 3: PATIENT ROOM CLEANING PROCEDURES
        [Display(Name = "If with spills, soak spill using 1:10 dilution of hypochlorite solution and leave in contact for 2–5 minutes")]
        public bool IsSpillSoakedWithSolution { get; set; }

        [Display(Name = "Clean walls using disposable cloths")]
        public bool IsWallsCleaned { get; set; }

        [Display(Name = "Wipe door frame")]
        public bool IsDoorFrameWiped { get; set; }

        [Display(Name = "Wipe window sill and spot clean windows")]
        public bool IsWindowSillAndWindowCleaned { get; set; }

        [Display(Name = "Wipe handles, light switches, and other high-touch areas")]
        public bool IsHighTouchAreasWiped { get; set; }

        [Display(Name = "Damp wipe all vertical surfaces, counters, ledges and sills")]
        public bool IsVerticalSurfacesWiped { get; set; }

        [Display(Name = "Pick up loose debris")]
        public bool IsLooseDebrisPickedUp { get; set; }

        [Display(Name = "Damp mop room floor starting from the far corner using correct color-coded mop")]
        public bool IsRoomFloorMopped { get; set; }

        [Display(Name = "Dispose of used cloths in facility-approved container")]
        public bool IsUsedClothsDisposed { get; set; }

        [Display(Name = "Empty and line waste containers")]
        public bool IsWasteContainersEmptied { get; set; }

        [Display(Name = "Remove infectious waste then proceed to Step 4")]
        public bool IsInfectiousWasteRemoved { get; set; }

        // STEP 4: BATHROOM CLEANING
        [Display(Name = "Clean mirror")]
        public bool IsMirrorCleaned { get; set; }

        [Display(Name = "Clean sink area")]
        public bool IsSinkAreaCleaned { get; set; }

        [Display(Name = "Clean faucet and handles")]
        public bool IsFaucetAndHandlesCleaned { get; set; }

        [Display(Name = "Clean toilet and flush handles")]
        public bool IsToiletAndFlushHandlesCleaned { get; set; }

        [Display(Name = "Clean other surfaces of the bathroom")]
        public bool IsOtherBathroomSurfacesCleaned { get; set; }

        [Display(Name = "Scrub bathroom floor")]
        public bool IsBathroomFloorScrubbed { get; set; }

        [Display(Name = "Empty and line color-coded waste containers")]
        public bool IsColorCodedWasteEmptied { get; set; }

        [Display(Name = "Remove PPE before leaving the room")]
        public bool IsPPERemoved { get; set; }

        [Display(Name = "Perform hand hygiene")]
        public bool IsHandHygieneAfterPPE { get; set; }

        [Display(Name = "Remove gloves and perform hand hygiene")]
        public bool IsGlovesRemovedAndHandHygieneDone { get; set; }

        [Display(Name = "Pre-cleaning areas/items")]
        [DataType(DataType.MultilineText)]
        public string PreCleaningItems { get; set; }

        [Display(Name = "Post-cleaning")]
        [DataType(DataType.MultilineText)]
        public string PostCleaningItems { get; set; }

        [Display(Name = "Recommendations / Actions Taken")]
        [DataType(DataType.MultilineText)]
        public string RecommendationsOrActions { get; set; }

        // Additional fields from image:
        [Display(Name = "Unit/Area Staff (Name and Signature)")]
        public string UnitAreaStaffSignature { get; set; }

        [Display(Name = "Date of Observation")]
        [DataType(DataType.Date)]
        public DateTime DateOfObservation { get; set; }
    }
}
