using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IPCU.Data
{
    public class BuildFileDbContext : DbContext
    {
        public BuildFileDbContext(DbContextOptions<BuildFileDbContext> options)
            : base(options)
        {
        }

        // Define your DbSet for the tbCoStation table
        public virtual DbSet<CoStation> CoStations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the tbCoStation entity
            modelBuilder.Entity<CoStation>(entity =>
            {
                entity.ToTable("tbCoStation", "dbo");

                entity.HasKey(e => e.StationID);

                entity.Property(e => e.StationID)
                    .HasColumnName("StationID");

                entity.Property(e => e.Station)
                    .HasColumnName("Station")
                    .HasMaxLength(100);

                entity.Property(e => e.Status)
                    .HasColumnName("Status")
                    .HasMaxLength(50);

                // Add mappings for other properties as needed
                // based on your table structure
            });
        }
    }

    // Entity class for tbCoStation table
    public class CoStation
    {
        public int StationID { get; set; }
        public string Station { get; set; }
        public string Status { get; set; }
        public string DepartmentCode { get; set; }
        public string PwdCode { get; set; }
        public bool IsNurseStation { get; set; }
        public int? ReportOrder { get; set; }
        public string Sufix { get; set; }
        public int? UnitID { get; set; }
        public bool? withDiet { get; set; }
        public bool? IsUnitDose { get; set; }
        public string RevenueCode { get; set; }
        public string SapCode { get; set; }
        public string SapSectionID { get; set; }
        public string SapDepartmentID { get; set; }
    }
}