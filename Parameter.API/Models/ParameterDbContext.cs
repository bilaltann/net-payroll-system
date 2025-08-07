using Microsoft.EntityFrameworkCore;
using Parameter.API.Models.Entities;

namespace Parameter.API.Models
{
    public class ParameterDbContext : DbContext
    {
        public ParameterDbContext(DbContextOptions<ParameterDbContext> options)
           : base(options)
        {
        }

        public DbSet<IncomeTaxBracket> IncomeTaxBrackets { get; set; }
        public DbSet<TaxParameter> TaxParameters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IncomeTaxBracket>().ToTable("GelirVergisiDilimleri");
            modelBuilder.Entity<TaxParameter>().ToTable("VergiParametreleri");
        }
    }
}
