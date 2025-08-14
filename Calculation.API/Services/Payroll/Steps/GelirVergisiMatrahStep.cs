using Calculation.API.Common;
using Calculation.API.Models.Payroll;
using Calculation.API.Services.Interfaces;

namespace Calculation.API.Services.Payroll.Steps
{
    public sealed class GelirVergisiMatrahStep : ICalculationStep
    {
        public int Order => 5;

        public Task ExecuteAsync(PayrollContext _context, CancellationToken ct = default)
        {
           // Formül(senin notlarına göre):
          // GV Matrahı = Brüt Gelir - (SGK İşçi Primi + İşsizlik İşçi Primi)
           var matrah = _context.BrutGelir - _context.SgkIsciPrimi - _context.IssizlikIsciPrimi;

            // Negatif olmasın
            _context.GelirVergisiMatrah = (matrah < 0m ? 0m : matrah).R2();

            return Task.CompletedTask;
        }
    }
}
