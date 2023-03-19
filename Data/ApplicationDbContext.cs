using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlimBio.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AlimBio.Models.Salarie> Salaries { get; set; }
        public DbSet<AlimBio.Models.Service> Services { get; set; }
        public DbSet<AlimBio.Models.Site> Sites { get; set; }
        public DbSet<AlimBio.Models.Entreprise> Entreprises { get; set; }
        public DbSet<AlimBio.Models.Ville> Villes { get; set; }
        public DbSet<AlimBio.Models.Message> Messages { get; set; }

    }
}