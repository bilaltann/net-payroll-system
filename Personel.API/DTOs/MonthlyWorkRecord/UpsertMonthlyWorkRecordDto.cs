using System.ComponentModel.DataAnnotations;

namespace Personel.API.DTOs.MonthlyWorkRecord
{
    public class UpsertMonthlyWorkRecordDto
    {
        // Upsert için: aynı (EmployeeId, Year, Month) varsa günceller, yoksa ekler
        [Required]
        public Guid EmployeeId { get; set; }

        [Range(1900, 2100)]
        public int Year { get; set; }

        [Range(1, 12)]
        public int Month { get; set; }

        [Range(0, 31)]
        public int WorkDays { get; set; }

        [Range(0, 1000)]
        public decimal OvertimeHours { get; set; }
    }
}
