using IPCU.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IPCU.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<PatientForm> PatientForms { get; set; }
        public DbSet<IPCU.Models.TrainingEvaluation> TrainingEvaluation { get; set; } = default!;
        public DbSet<IPCU.Models.FitTestingForm> FitTestingForm { get; set; } = default!;
        public DbSet<HandHygieneForm> HandHygieneForms { get; set; }
        public DbSet<HHActivity> HHActivities { get; set; }
        public DbSet<FitTestingFormHistory> FitTestingFormHistory { get; set; }
        public DbSet<Trainee> Trainees { get; set; }

        public DbSet<HandHygieneComplianceSummary> HandHygieneComplianceSummary { get; set; }

        public DbSet<PreTestNonClinical> PreTestNonClinicals { get; set; }
        public DbSet<PreTestClinical> PreTestClinicals { get; set; }
        public DbSet<PostTestNonClinical> PostTestNonCLinicals { get; set; }
        public DbSet<PostTestClinical> PostTestClinicals { get; set; }
        public DbSet<IPCU.Models.EvaluationViewModel> EvaluationViewModel { get; set; } = default!;

        public DbSet<TrainingSummary> TrainingSummaries { get; set; }
        public DbSet<EvaluationViewModel> Evaluations { get; set; }
        public DbSet<TrainingEvaluation> TrainingEvaluations { get; set; } // ✅ Ensure this is present

        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientMaster> PatientMasters { get; set; }
        public DbSet<VitalSigns> VitalSigns { get; set; }
        public DbSet<DeviceConnected> DeviceConnected { get; set; }

        public DbSet<SSTInfectionModel> SSTInfectionModels { get; set; }
        public DbSet<Diagnostics> Diagnostics { get; set; }
        public DbSet<Antibiotic> Antibiotics { get; set; }
        public DbSet<DiagnosticsTreatment> DiagnosticsTreatments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Diagnostics entity
            modelBuilder.Entity<Diagnostics>()
                .HasOne(d => d.Patient)
                .WithMany()
                .HasForeignKey(d => d.HospNum)
                .OnDelete(DeleteBehavior.Restrict);

            // Only configure Patient navigation property
            modelBuilder.Entity<Diagnostics>()
                .Navigation(d => d.Patient).IsRequired(false);

            // Do NOT add this line - it's causing the error
            // modelBuilder.Entity<Diagnostics>()
            //    .Navigation(d => d.Treatments).IsRequired(true);

            // Configure the relationship between Diagnostics and DiagnosticsTreatment
            modelBuilder.Entity<DiagnosticsTreatment>()
                .HasOne(dt => dt.Diagnostic)
                .WithMany(d => d.Treatments)
                .HasForeignKey(dt => dt.DiagId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DiagnosticsTreatment>()
                .HasOne(dt => dt.Antibiotic)
                .WithMany()
                .HasForeignKey(dt => dt.AntibioticId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }

}