using System;
using System.Collections.Generic;

namespace IPCU.Models
{
    public class PatientUsiViewModel
    {
        public string FullName { get; set; }
        public string HospitalNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string UnitWardArea { get; set; }
        public string Investigator { get; set; }
        public DateTime? DateOfAdmission { get; set; }
        public string Gender { get; set; }
        public List<Usi> UsiRecords { get; set; } = new List<Usi>();
    }
}
