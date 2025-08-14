using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Contracts.Personel
{
    public sealed record GetEmployeeDto
    (
        Guid Id,
        string FullName,
        decimal MonthlyGrossSalary,
        decimal OvertimeHourlyRate,
        decimal DailyMealAmount,
        bool IsActive
    );
}
