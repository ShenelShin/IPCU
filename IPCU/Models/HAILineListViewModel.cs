using System;
using System.ComponentModel.DataAnnotations;

namespace IPCU.Models
{
    public class HAILineListViewModel
    {
        [Display(Name = "Hospital Number")]
        public string HospNum { get; set; }

        [Display(Name = "Patient Name")]
        public string PatientName { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Age")]
        public int? Age { get; set; }

        [Display(Name = "Unit")]
        public string Unit { get; set; }

        [Display(Name = "Room")]
        public string Room { get; set; }

        [Display(Name = "Admission Date")]
        [DataType(DataType.Date)]
        public DateTime? AdmissionDate { get; set; }

        [Display(Name = "Patient Classification")]
        public string Classification { get; set; }

        [Display(Name = "Main Service")]
        public string MainService { get; set; }

        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        public DateTime? EventDate { get; set; }

        [Display(Name = "HAI Status")]
        public bool HaiStatus { get; set; }

        [Display(Name = "HAI Type")]
        public string HaiType { get; set; }

        [Display(Name = "Specific HAI Classification")]
        public string SpecificHaiClassification { get; set; }

        [Display(Name = "Central Line Access")]
        public string CLAccess { get; set; }

        [Display(Name = "MDRO")]
        public bool IsMDRO { get; set; }

        [Display(Name = "MDRO Organism")]
        public string MDROOrganism { get; set; }

        [Display(Name = "Outcome")]
        public string Outcome { get; set; }

        [Display(Name = "Discharge Date")]
        [DataType(DataType.Date)]
        public DateTime? DischargeDate { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.DateTime)]
        public DateTime DateCreated { get; set; }
    }
}