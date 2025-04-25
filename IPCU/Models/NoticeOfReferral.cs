using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class NoticeOfReferral
    {
        [Key]
        public int Id { get; set; }
        public string PatientLastName { get; set; }
        public string PatientFirstName { get; set; }
        public string? PatientMiddleName { get; set; }
        public string AreaUnit { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public DateTime DOA { get; set; }
        public DateTime DOD { get; set; }
        public string InitialDiagnosis { get; set; }
        public bool HAAT { get; set; }
        public bool Communicable { get; set; }
        public bool Fever { get; set; }
        public bool PostOp { get; set; }
        public bool ReAdmitted { get; set; }
        public bool Laboratory { get; set; }
        public bool Radiology { get; set; }
        public string? Referredby { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReferredbyDnT { get; set; }

        public string? Receivedby { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ReceivedbyDnT { get; set; }
    }
}
