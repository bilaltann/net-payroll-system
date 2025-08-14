using System.ComponentModel.DataAnnotations;

namespace Personel.API.DTOs.Employee
{
    public class CreateEmployeeDto
    {
       
            [Required, StringLength(150)]
            public string FullName { get; set; } = default!;

            [Required]
            public decimal MonthlyGrossSalary { get; set; }

            [Required]
            public decimal OvertimeHourlyRate { get; set; }

            [Required]
            public decimal DailyMealAmount { get; set; }
        
    }
}
