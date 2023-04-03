using Microsoft.EntityFrameworkCore;

namespace ElmarakbyTest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Employer> Employers { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }

        
    }
}
