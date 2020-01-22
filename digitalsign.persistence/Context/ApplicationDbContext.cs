using System.Reflection;
using digitalsign.domain.Domain;
using Microsoft.EntityFrameworkCore;

namespace digitalsign.persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        public DbSet<DailyReminder> DailyReminders { get; set; }

        public DbSet<WeeklyReminder> WeeklyReminders { get; set; }

        public DbSet<MonthlyReminder> MonthlyReminders { get; set; }
        public DbSet<YearlyReminder> YearlyReminders { get; set; }

        public DbSet<User> Users { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
