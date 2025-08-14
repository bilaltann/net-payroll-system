using Microsoft.EntityFrameworkCore;
using Personel.API.Models.Entities;
using System;

namespace Personel.API.Models
{
    public class PersonelDbContext(DbContextOptions<PersonelDbContext> options) : DbContext(options)
    {
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<MonthlyWorkRecord> MonthlyWorkRecords => Set<MonthlyWorkRecord>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(e =>
            {
                e.Property(p => p.FullName).HasMaxLength(150).IsRequired();
                e.Property(p => p.MonthlyGrossSalary).HasPrecision(18, 2);
                e.Property(p => p.OvertimeHourlyRate).HasPrecision(18, 2);
                e.Property(p => p.DailyMealAmount).HasPrecision(18, 2);
            });

            modelBuilder.Entity<MonthlyWorkRecord>(m =>
            {
                m.Property(p => p.OvertimeHours).HasPrecision(10, 2); //decimal(10,2) demek: toplam 10 basamak, bunun 2’si ondalık. Yani 8 basamak tam kısım 
                // Aynı çalışan için aynı yıl/ay tek kayıt olsun
                m.HasIndex(x => new { x.EmployeeId, x.Year, x.Month }).IsUnique();
            });
        }

    }
}
