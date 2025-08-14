using Calculation.API.Common;
using Calculation.API.Models.Payroll;
using Calculation.API.Services.Interfaces;

namespace Calculation.API.Services.Payroll.Steps
{
    public sealed class DamgaVergisiStep : ICalculationStep
    {
        public int Order => 7;

        public Task ExecuteAsync(PayrollContext _context, CancellationToken ct = default)
        {
            _context.DamgaVergisi = (_context.BrutGelir * _context.TaxParameter.DamgaVergisiOrani).R2();
            return Task.CompletedTask; 
        }
    }
}
