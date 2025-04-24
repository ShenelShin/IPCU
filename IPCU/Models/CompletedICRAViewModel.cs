namespace IPCU.Models
{
    public class CompletedICRAViewModel
    {
        public ICRA ICRA { get; set; }
        public bool HasPostConstruction { get; set; }
        public int? PostConstructionId { get; set; }
        public PostConstruction PostConstruction { get; set; } // Add this to hold the full post-construction data
    }
}
