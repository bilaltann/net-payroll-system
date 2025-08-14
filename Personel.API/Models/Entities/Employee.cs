using Shared.Abstractions;

namespace Personel.API.Models.Entities
{
    public class Employee :IHasId<Guid>
    {
        public Guid Id { get; set; }
        // İsim
        public string FullName { get; set; } = default!;

        // Sabit: Aylık brüt maaş
        public decimal MonthlyGrossSalary { get; set; }

        // Sabit: Fazla mesai saatlik ücreti
        public decimal OvertimeHourlyRate { get; set; }

        // Sabit: Günlük yemek ücreti
        public decimal DailyMealAmount { get; set; }

        public bool IsActive { get; set; } = true;

        // Nav
        public ICollection<MonthlyWorkRecord> MonthlyWorkRecords { get; set; } = new List<MonthlyWorkRecord>();
    
    }
}
