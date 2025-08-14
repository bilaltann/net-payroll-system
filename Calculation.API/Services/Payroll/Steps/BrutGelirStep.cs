using Calculation.API.Common;
using Calculation.API.Models.Payroll;
using Calculation.API.Services.Interfaces;

namespace Calculation.API.Services.Payroll.Steps
{
    public sealed class BrutGelirStep : ICalculationStep
    {
        public int Order => 1 ;

        public Task ExecuteAsync(PayrollContext _context, CancellationToken ct = default)
        {
            var monthlyWorkRecord = _context.Monthly ?? throw new InvalidOperationException("MonthlyWorkRecord bulunamadı.");
            var employee = _context.Employee ?? throw new InvalidOperationException("Employee bulunamadı.");

            decimal aylikMaas = employee.MonthlyGrossSalary;

            decimal fazlaMesai = monthlyWorkRecord.OvertimeHours * employee.OvertimeHourlyRate;

          
            decimal yemekToplam = monthlyWorkRecord.WorkDays * employee.DailyMealAmount;

            _context.BrutGelir = (aylikMaas + fazlaMesai + yemekToplam).R2();

            return Task.CompletedTask;

              
        }
    }
}
