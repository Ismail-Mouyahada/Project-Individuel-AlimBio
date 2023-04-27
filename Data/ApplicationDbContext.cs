using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AlimBio.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AlimBio.Models.Salarie>? Salaries { get; set; }
        public DbSet<AlimBio.Models.Service>? Services { get; set; }
        public DbSet<AlimBio.Models.Site>? Sites { get; set; }
        public DbSet<AlimBio.Models.Entreprise>? Entreprises { get; set; }
        public DbSet<AlimBio.Models.Ville>? Villes { get; set; }
        public DbSet<AlimBio.Models.Message>? Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the ApplicationUser entity
            modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "Users");

                entity.Property(e => e.Id).HasMaxLength(36).ValueGeneratedOnAdd();
                entity.Property(e => e.Email).HasMaxLength(256);
                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
                entity.Property(e => e.UserName).HasMaxLength(256);
                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            });

            // Seed the admin user
            modelBuilder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "94",
                    UserName = "admin@example.com",
                    NormalizedUserName = "ADMIN@EXAMPLE.COM",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Sois150916++@"),
                    SecurityStamp = string.Empty,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                });
        }
    }
}
