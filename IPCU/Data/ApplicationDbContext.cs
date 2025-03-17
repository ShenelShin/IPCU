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

    }

}