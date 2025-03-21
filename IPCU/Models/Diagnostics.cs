using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IPCU.Models
{
    [Table("tbdiagnostics")]
    public class Diagnostics
    {
        public Diagnostics()
        {
            // Initialize the collection in the constructor
            Treatments = new List<DiagnosticsTreatment>();
        }

        [Key]
        public int DiagId { get; set; }

        [Column(TypeName = "date")]
        [Required(ErrorMessage = "Date of collection is required")]
        public DateTime? DateCollection { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Source site is required")]
        public string SourceSite { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "Isolate findings result is required")]
        public string IsolateFindingsResult { get; set; }

        [StringLength(8)]
        [Required(ErrorMessage = "Hospital number is required")]
        public string HospNum { get; set; }

        [ForeignKey("HospNum")]
        public virtual PatientMaster Patient { get; set; }

        // Navigation property for treatments
        public virtual ICollection<DiagnosticsTreatment> Treatments { get; set; }
    }

    // Junction table to link Diagnostics with multiple Antibiotics
    [Table("tbdiagnosticstreatments")]
    public class DiagnosticsTreatment
    {
        [Key]
        public int Id { get; set; }

        public int DiagId { get; set; }

        public int AntibioticId { get; set; }

        [ForeignKey("DiagId")]
        public virtual Diagnostics Diagnostic { get; set; }

        [ForeignKey("AntibioticId")]
        public virtual Antibiotic Antibiotic { get; set; }
    }
}