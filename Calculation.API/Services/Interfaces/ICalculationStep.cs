using Calculation.API.Models.Payroll;

namespace Calculation.API.Services.Interfaces
{
        public interface ICalculationStep
        {
        // Her step ortak context'i günceller.

            int Order { get; } // Sıralama için
            Task ExecuteAsync(PayrollContext _context, CancellationToken ct = default);
        }
    
}
