namespace IPCU.Models
{
    public class PatientVitalSignsViewModel
    {
        public PatientViewModel Patient { get; set; }
        public List<VitalSigns> VitalSigns { get; set; }
    }
}
