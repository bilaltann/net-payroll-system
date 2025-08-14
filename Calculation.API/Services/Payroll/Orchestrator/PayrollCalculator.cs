using Calculation.API.Services.Interfaces;
using Calculation.API.Services.Parameters.Personel;
using Calculation.API.Services.Parameters;
using Calculation.API.Models.Payroll;

namespace Calculation.API.Services.Payroll.Orchestrator
{
    public sealed class PayrollCalculator(IPersonelReadService personel,
        IParameterSnapshotProvider snapshotProvider,
        IEnumerable<ICalculationStep> steps) : IPayrollCalculator
    {
        public async Task<PayrollResult> HesaplaAsync(PayrollRequest input)
        {
            // 1) Personel verileri
            var emp = await personel.GetEmployeeAsync(input.EmployeeId);
            Console.WriteLine("Employee DTO geldi mi? " + (emp != null));
            if (emp != null)
                Console.WriteLine($"  ➡ {emp.FullName} - Maaş: {emp.MonthlyGrossSalary}");

            var monthly = await personel.GetMonthlyByKeyAsync(input.EmployeeId, input.Year, input.Month)
                     ?? throw new InvalidOperationException("Aylık kayıt bulunamadı.");


            var snap = await snapshotProvider.GetAsync(new DateTime(input.Year, input.Month, 1));

            var context = new PayrollContext
            {
                EmployeeId = input.EmployeeId,
                Year = input.Year,
                Month = input.Month,
                Employee = emp,
                Monthly = monthly,
                TaxParameter = snap
            };
            // 4) Adımları sırayla çalıştır
            foreach (var step in steps.OrderBy(s => s.Order))
                await step.ExecuteAsync(context);

            // 5) Sonucu döndür
            return new PayrollResult
            {
                EmployeeId = input.EmployeeId,
                Year = input.Year,
                Month = input.Month,
                BrutGelir = context.BrutGelir,
                SgkMatrah = context.SgkMatrah,
                SgkIsciPrimi = context.SgkIsciPrimi,
                IssizlikIsciPrimi = context.IssizlikIsciPrimi,
                GelirVergisiMatrah = context.GelirVergisiMatrah,
                GelirVergisi = context.GelirVergisi,
                DamgaVergisi = context.DamgaVergisi,
                NetUcret = context.NetUcret
            };
        }
    }
}
