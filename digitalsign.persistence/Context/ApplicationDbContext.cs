using digitalsign.domain.Domain;
using digitalsign.persistence.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace digitalsign.persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Message> Messages { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
                .HasKey(m => m.Guid);
            
            modelBuilder.Entity<ApplicationUser>()
                .HasOne(u => u.RefreshToken)
                .WithOne(r => r.User);

            modelBuilder.Entity<ApplicationUser>()
                .ToTable("Users");

            modelBuilder.Entity<RefreshToken>()
                .HasKey(r => r.Token);

            modelBuilder.Entity<RefreshToken>()
                .ToTable("RefreshToken");    
        }
    }
}
