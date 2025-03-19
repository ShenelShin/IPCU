using IPCU.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class VitalSigns
{
    [Key]
    public int VitalId { get; set; }

    [Required]
    public string VitalSign { get; set; }

    [Required]
    public string VitalSignValue { get; set; }

    [Required]
    public DateTime VitalSignDate { get; set; }

    [Required]
    [StringLength(8)] // Match the length with Patient.HospNum and PatientMaster.HospNum
    public string HospNum { get; set; }

    [ForeignKey("HospNum")]
    public virtual PatientMaster PatientMaster { get; set; }

    // Remove this redundant foreign key since we're already linking to PatientMaster
    // [ForeignKey("HospNum")]
    // public virtual Patient Patient { get; set; }
}