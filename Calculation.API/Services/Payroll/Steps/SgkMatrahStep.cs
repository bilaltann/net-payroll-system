using Calculation.API.Common;
using Calculation.API.Models.Payroll;
using Calculation.API.Services.Interfaces;

namespace Calculation.API.Services.Payroll.Steps
{
    public sealed class SgkMatrahStep : ICalculationStep
    {
        public int Order => 2;

        public Task ExecuteAsync(PayrollContext _context, CancellationToken ct = default)
        {
            var monthlyWorkRecord= _context.Monthly ?? throw new InvalidOperationException("MonthlyWorkRecord bulunamadı.");
            var employee = _context.Employee ?? throw new InvalidOperationException("MonthlyWorkRecord bulunamadı.");

            decimal gunlukYemekMuaf = _context.TaxParameter.GunlukYemekMuafTutar;
            decimal gunlukYemek = employee.DailyMealAmount; // fiilen ödenen
            int gunSayisi = monthlyWorkRecord.WorkDays;

            decimal muafToplam = Math.Min(gunlukYemek, gunlukYemekMuaf) * gunSayisi;

            var sgkMatrah = _context.BrutGelir - muafToplam;
            _context.SgkMatrah = (sgkMatrah < 0 ? 0 : sgkMatrah).R2();

            return Task.CompletedTask;

            //SGK Matrahı: brütten sadece muaf kısım kadar yemek düşülür
            //(ödenen > muaf ise fazlası matraha dahil kalır).
        }
    }
}
