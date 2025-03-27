namespace IPCU.Models
{
    public class InfectionFormsInfo
    {
        public CardiovascularSystemInfection? CardiovascularForm { get; set; }
        public SSTInfectionModel? SSTForm { get; set; }
        public LaboratoryConfirmedBSI? LCBIForm { get; set; }
        public PediatricVAEChecklist? PVAEForm { get; set; }
        public UTIFormModel? UTIForm { get; set; }
        public Pneumonia? PneumoniaForm { get; set; }
        public Usi? USIForm { get; set; }
        public VentilatorEventChecklist? VAEForm { get; set; }
    }
}
