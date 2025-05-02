using Microsoft.EntityFrameworkCore;
using IPCU.Models;

namespace IPCU.Data
{
    public class PatientDbContext : DbContext
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options)
            : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientMaster> PatientMasters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Patient entity
            modelBuilder.Entity<Patient>()
                .ToTable("tbpatient");

            // Configure the PatientMaster entity
            modelBuilder.Entity<PatientMaster>()
                .ToTable("tbmaster");

            // Set up EF Core to ignore any columns in the database
            // that don't have corresponding properties in the model
            modelBuilder.Entity<Patient>().ToTable(tb => tb.ExcludeFromMigrations());
            modelBuilder.Entity<PatientMaster>().ToTable(tb => tb.ExcludeFromMigrations());
        }
    }
}