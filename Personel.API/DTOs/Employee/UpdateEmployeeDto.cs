using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Personel.API.DTOs.Employee
{
    public class UpdateEmployeeDto
    {
        public Guid Id { get; set; }

        [Required, StringLength(150)]
        public string FullName { get; set; } = default!;

        [Required]
        public decimal MonthlyGrossSalary { get; set; }

        [Required]
        public decimal OvertimeHourlyRate { get; set; }

        [Required]
        public decimal DailyMealAmount { get; set; }

        public bool IsActive { get; set; } = true;

    }
}
