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
<<<<<<< HEAD
        public DbSet<IPCU.Models.TrainingEvaluation> TrainingEvaluation { get; set; } = default!;
=======
        public DbSet<IPCU.Models.FitTestingForm> FitTestingForm { get; set; } = default!;
>>>>>>> d8fff3f8df733883b4b74bb3d7808447988b3394
    }
}
