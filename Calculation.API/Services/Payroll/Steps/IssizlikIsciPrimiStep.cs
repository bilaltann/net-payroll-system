using Calculation.API.Common;
using Calculation.API.Models.Payroll;
using Calculation.API.Services.Interfaces;

namespace Calculation.API.Services.Payroll.Steps
{
    public sealed class IssizlikIsciPrimiStep : ICalculationStep
    {
        public int Order => 3;

        public Task ExecuteAsync(PayrollContext _context, CancellationToken ct = default)
        {
            // SGK matrahı yoksa prim 0
            if (_context.SgkMatrah <= 0m)
            {
                _context.IssizlikIsciPrimi = 0m;
                return Task.CompletedTask;
            }

            _context.IssizlikIsciPrimi = (_context.SgkMatrah * _context.TaxParameter.IssizlikIsciOrani).R2();
            return Task.CompletedTask;
        }
    }
}
