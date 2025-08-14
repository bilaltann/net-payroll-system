using System.ComponentModel.DataAnnotations;

namespace Personel.API.DTOs.MonthlyWorkRecord
{
    // Sadece güncelleme (id route’tan gelir)

    public class UpdateMonthlyWorkRecordDto
    {
        public Guid Id { get; set; }
        [Range(0, 31)]
        public int WorkDays { get; set; }

        [Range(0, 1000)]
        public decimal OvertimeHours { get; set; }
    }
}
