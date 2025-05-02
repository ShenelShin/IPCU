using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPCU.Models
{
    [Table("tbpatient")]
    public class Patient
    {
        [StringLength(8)]
        public string HospNum { get; set; }

        [Key]
        [StringLength(10)]
        public string IdNum { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? AccountNum { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string? HospPlan { get; set; }

        [StringLength(3)]
        [Column(TypeName = "varchar(3)")]
        public string? MedicareType { get; set; }

        [StringLength(3)]
        [Column(TypeName = "varchar(3)")]
        public string? AdmType { get; set; }

        public DateTime? AdmDate { get; set; }

        public DateTime? DcrDate { get; set; }

        [StringLength(3)]
        [Column(TypeName = "varchar(3)")]
        public string? ServiceID { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string? ResultID { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string? DispositionID { get; set; }

        [StringLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string? AttendingDr1 { get; set; }

        [StringLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string? AttendingDr2 { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? AttendingDr3 { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? AttendingDr4 { get; set; }

        [StringLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string? AdmittingDr { get; set; }

        [StringLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string? ReferringDr { get; set; }

        [StringLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string? AdmittingClerk { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? ReferredFrom { get; set; }

        [StringLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string? DischargeClerk { get; set; }

        [StringLength(3)]
        [Column(TypeName = "varchar(3)")]
        public string? Age { get; set; }

        public DateTime? BillingDate { get; set; }

        [StringLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string? RoomID { get; set; }

        [StringLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string? BillingClerk { get; set; }

        [Column(TypeName = "float")]
        public float? RoomRate { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string? RecordStatus { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? OPDIdNum { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string? MedicoLegal { get; set; }

        [Column(TypeName = "float")]
        public float? Claim1 { get; set; }

        public DateTime? ArDate { get; set; }

        [Column(TypeName = "bit")]
        public bool WithWatcher { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string AdditionalBed { get; set; }

        [Column(TypeName = "float")]
        public float? WatcherRate { get; set; }

        [Column(TypeName = "bit")]
        public bool PharmaSeniorDiscounted { get; set; }

        public DateTime? LastUpdatedAR { get; set; }

        public DateTime? ZeroDet { get; set; }

        [StringLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string? ResidentDr1 { get; set; }

        [StringLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string? ResidentDr2 { get; set; }

        [StringLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string? ResidentDr3 { get; set; }

        [StringLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string? ResidentDr4 { get; set; }

        [StringLength(60)]
        [Column(TypeName = "varchar(60)")]
        public string? ReferringHospital { get; set; }

        public DateTime? ArrivalTime { get; set; }

        [StringLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string? ClassificationCode { get; set; }

        [Column(TypeName = "float")]
        public float? LOA { get; set; }

        public int? Markup { get; set; }

        [Column(TypeName = "bit")]
        public bool IsDonor { get; set; }

        [Column(TypeName = "bit")]
        public bool IsRoomSharing { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string Transplant { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string Dialysis { get; set; }

        public DateTime? RBRDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? UserId { get; set; }

        [Column(TypeName = "bit")]
        public bool? IsPHIC { get; set; }

        public DateTime? FormDate { get; set; }

        [Column(TypeName = "bit")]
        public bool? IsHemoDialysis { get; set; }

        [Column(TypeName = "bit")]
        public bool? IsChemo { get; set; }

        [Column(TypeName = "bit")]
        public bool? isRadiotherapy { get; set; }

        [Column(TypeName = "bit")]
        public bool? isMultiple { get; set; }

        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string? InvoiceNumber { get; set; }

        [Column(TypeName = "bit")]
        public bool? isAdjust { get; set; }

        [StringLength(3)]
        [Column(TypeName = "varchar(3)")]
        public string? PackageID { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string? LateCharges { get; set; }

        [Column(TypeName = "bit")]
        public bool? OkeyMRI { get; set; }

        [Column(TypeName = "bit")]
        public bool isWellBaby { get; set; }

        [StringLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string? AttendingDr5 { get; set; }

        [StringLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string? AttendingDr6 { get; set; }

        [StringLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string? AttendingDr7 { get; set; }

        [StringLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string? AttendingDr8 { get; set; }

        [StringLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string? AttendingDr9 { get; set; }

        [StringLength(4)]
        [Column(TypeName = "varchar(4)")]
        public string? AttendingDr10 { get; set; }

        public DateTime? TransferredDate { get; set; }

        [Column(TypeName = "bit")]
        public bool? RoomIn { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string HostName { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? RevokeBy { get; set; }

        public DateTime? RevokeDate { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string BadDebtAccount { get; set; }

        public DateTime? EmbarkationDate { get; set; }

        public DateTime? DisembarkationDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? Category { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? CaseNum { get; set; }

        [StringLength(8)]
        [Column(TypeName = "varchar(8)")]
        public string? VesID { get; set; }

        [StringLength(5)]
        [Column(TypeName = "varchar(5)")]
        public string? DeathType { get; set; }

        [Column(TypeName = "bit")]
        public bool? FullyPaid { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? RFCode { get; set; }

        [Column(TypeName = "bit")]
        public bool? isObstetricalCase { get; set; }

        [Column(TypeName = "bit")]
        public bool? WithConsultation { get; set; }

        [StringLength(3)]
        [Column(TypeName = "varchar(3)")]
        public string? HospID { get; set; }

        public DateTime? TransferDate { get; set; }

        public DateTime? ClearingDate { get; set; }

        public DateTime? HPBSPrintDate { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? PrintedBy { get; set; }

        public DateTime? HBPSPrintDate { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? HBPSPrintedBy { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? WatcherID { get; set; }

        [Column(TypeName = "bit")]
        public bool? WithAdmissionKit { get; set; }

        [Column(TypeName = "bit")]
        public bool? isOutborn { get; set; }

        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string? MotherIDNum { get; set; }

        [Column(TypeName = "bit")]
        public bool? WithTransferFee { get; set; }

        [Column(TypeName = "bit")]
        public bool? isCaesarianDelivery { get; set; }

        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string? NewBornID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? SMSnotification { get; set; }

        [StringLength(30)]
        [Column(TypeName = "varchar(30)")]
        public string? EmailAddress { get; set; }

        [Column(TypeName = "bit")]
        public bool? BadDebt { get; set; }

        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string? AccountNumII { get; set; }

        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string? COEmployee { get; set; }

        [StringLength(15)]
        [Column(TypeName = "varchar(15)")]
        public string? JoneltaID { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? Cardnum { get; set; }

        public DateTime? DeathDate { get; set; }

        public int? SeamanDeptID { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? SeamanDeptOther { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? ShipType { get; set; }

        public int? TradeID { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? PHClass { get; set; }

        public int? ActionSlipNum { get; set; }

        [Column(TypeName = "text")]
        public string? ChaplainsRemark { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? HMONum { get; set; }

        [StringLength(2)]
        [Column(TypeName = "varchar(2)")]
        public string? StayTypeID { get; set; }

        public DateTime? DcrClearanceDate { get; set; }

        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string? SOAUserName { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? AccountNum1 { get; set; }

        public DateTime? BillTimeStart { get; set; }

        public DateTime? BillTimeEnd { get; set; }

        [StringLength(1)]
        [Column(TypeName = "varchar(1)")]
        public string? AdmLocation { get; set; }

        [StringLength(5)]
        [Column(TypeName = "varchar(5)")]
        public string? AttendingDR11 { get; set; }

        [StringLength(5)]
        [Column(TypeName = "varchar(5)")]
        public string? AttendingDR12 { get; set; }

        [StringLength(5)]
        [Column(TypeName = "varchar(5)")]
        public string? AttendingDR13 { get; set; }

        [StringLength(5)]
        [Column(TypeName = "varchar(5)")]
        public string? AttendingDR14 { get; set; }

        [StringLength(5)]
        [Column(TypeName = "varchar(5)")]
        public string? AttendingDR15 { get; set; }

        [StringLength(5)]
        [Column(TypeName = "varchar(5)")]
        public string? AttendingDR16 { get; set; }

        [StringLength(5)]
        [Column(TypeName = "varchar(5)")]
        public string? AttendingDR17 { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? Version { get; set; }

        public DateTime? DateCreated { get; set; }

        [Column(TypeName = "bit")]
        public bool isRenal { get; set; }

        [Column(TypeName = "bit")]
        public bool isPEME { get; set; }

        [StringLength(6)]
        [Column(TypeName = "varchar(6)")]
        public string? ReferringDr2 { get; set; }

        [StringLength(6)]
        [Column(TypeName = "varchar(6)")]
        public string? ReferringDr3 { get; set; }

        [StringLength(6)]
        [Column(TypeName = "varchar(6)")]
        public string? ReferringDr4 { get; set; }

        [StringLength(6)]
        [Column(TypeName = "varchar(6)")]
        public string? ReferringDr5 { get; set; }

        [StringLength(500)]
        [Column(TypeName = "varchar(500)")]
        public string? ReasonReferredBy { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? OLD_HOSPNUM { get; set; }

        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string? OLD_ACCOUNTNUM { get; set; }

        public DateTime? AcknowledgeDateTime { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? AcknowledgedBy { get; set; }

        public DateTime? OriginalAdmDateTime { get; set; }

        [StringLength(500)]
        [Column(TypeName = "varchar(500)")]
        public string? ReasonReferredTo { get; set; }

        [StringLength(500)]
        [Column(TypeName = "varchar(500)")]
        public string? ReferredTo { get; set; }

        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string? Used_PHICMemberNumber { get; set; }

        [Column(TypeName = "bit")]
        public bool isNoBalanceBilling { get; set; }

        // Navigation property to PatientMaster
        [ForeignKey("HospNum")]
        public virtual PatientMaster PatientMaster { get; set; }
    }

}