using Shared.Abstractions;

namespace Personel.API.Models.Entities
{
    public class MonthlyWorkRecord:IHasId<Guid>
    {
        public Guid Id { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee Employee { get; set; } = default!;

        public int Year { get; set; }
        public int Month { get; set; } // 1..12

        // O ay fiili çalışma günü
        public int WorkDays { get; set; } = 30;

        // O ay toplam fazla mesai saati
        public decimal OvertimeHours { get; set; } = 0m;
    }
}
