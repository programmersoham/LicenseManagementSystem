using Microsoft.EntityFrameworkCore;
namespace API.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<ApplicationModel> Applications { get; set; }
        public DbSet<LicenseModel> Licenses { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationModel>().HasKey(a => a.Id);
            modelBuilder.Entity<UserModel>().HasKey(u => u.Id);
            modelBuilder.Entity<LicenseModel>().HasKey(l => l.Id);
            
            base.OnModelCreating(modelBuilder);
        }

    }
}
