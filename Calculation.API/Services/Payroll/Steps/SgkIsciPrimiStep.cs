using Calculation.API.Common;
using Calculation.API.Models.Payroll;
using Calculation.API.Services.Interfaces;

namespace Calculation.API.Services.Payroll.Steps
{
    public sealed class SgkIsciPrimiStep:ICalculationStep
    {
        public int Order => 4;

        public Task ExecuteAsync(PayrollContext _context, CancellationToken ct = default)
        {
            if (_context.SgkMatrah <= 0m)
            {
                _context.SgkIsciPrimi = 0m;
                return Task.CompletedTask;
            }

            _context.SgkIsciPrimi = (_context.SgkMatrah * _context.TaxParameter.SgkIsciOrani).R2();
            return Task.CompletedTask;
        }
    }
}
