using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPCU.Models
{
    [Table("tbPatientMovement")]
    public class PatientMovement
    {
        [Key]
        public int MovementId { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime MovementDate { get; set; }

        [Required]
        [StringLength(50)]
        public string Area { get; set; }

        // Fields for new arrivals
        public int AdmissionCount { get; set; } = 0;
        public int TransferInCount { get; set; } = 0;

        // Fields for discharges
        public int SentHomeCount { get; set; } = 0;
        public int MortalityCount { get; set; } = 0;
        public int TransferOutCount { get; set; } = 0;

        // Optional notes
        [StringLength(500)]
        public string Notes { get; set; }

        [Required]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public DateTime? DateModified { get; set; }

        [StringLength(50)]
        [Required]
        public string CreatedBy { get; set; }

        [StringLength(50)]
        [Required]
        public string ModifiedBy { get; set; }
    }

    // This class can be used to track individual patient movements if needed
    [Table("tbPatientMovementDetail")]
    public class PatientMovementDetail
    {
        [Key]
        public int MovementDetailId { get; set; }

        [Required]
        public int MovementId { get; set; }

        [ForeignKey("MovementId")]
        public PatientMovement PatientMovement { get; set; }

        [Required]
        [StringLength(8)]
        public string HospNum { get; set; }

        [ForeignKey("HospNum")]
        public PatientMaster Patient { get; set; }

        [StringLength(10)]
        public string IdNum { get; set; }

        [Required]
        [StringLength(20)]
        public string MovementType { get; set; } // Admission, TransferIn, SentHome, Mortality, TransferOut

        [StringLength(50)]
        public string SourceArea { get; set; } // Applicable for transfers

        [StringLength(50)]
        public string DestinationArea { get; set; } // Applicable for transfers

        [Required]
        public DateTime MovementDateTime { get; set; }

        [StringLength(500)]
        public string Remarks { get; set; }
    }

    // View model for displaying movement summary
    public class PatientMovementViewModel
    {
        public int MovementId { get; set; }
        public DateTime MovementDate { get; set; }
        public string Area { get; set; }

        // New arrivals
        public int TotalArrivals { get; set; }
        public int AdmissionCount { get; set; }
        public int TransferInCount { get; set; }

        // Discharges
        public int TotalDischarges { get; set; }
        public int SentHomeCount { get; set; }
        public int MortalityCount { get; set; }
        public int TransferOutCount { get; set; }

        public int NetChange { get; set; } // TotalArrivals - TotalDischarges

        public string Notes { get; set; }
    }
}