using Calculation.API.Common;
using Calculation.API.Models.Payroll;
using Calculation.API.Services.Interfaces;
using Calculation.API.Services.Parameters.IncomeTaxBracket;

namespace Calculation.API.Services.Payroll.Steps
{
    public sealed class GelirVergisiStep(IIncomeTaxBracketService brackets) : ICalculationStep
    {
        public int Order => 6;

        public async Task ExecuteAsync(PayrollContext ctx, CancellationToken ct = default)
        {
            if (ctx is null) throw new ArgumentNullException(nameof(ctx));

            // Matrah sıfır/negatifse vergi yok
            if (ctx.GelirVergisiMatrah <= 0m) { ctx.GelirVergisi = 0m; return; }

            // Dilimi çek
            var b = await brackets.GetBracketAsync(ctx.GelirVergisiMatrah, ctx.CalcDate, ct);

            // Tanı koyabilmek için logla
            Console.WriteLine($"[BRACKET] income={ctx.GelirVergisiMatrah} date={ctx.CalcDate:yyyy-MM-dd}");
            Console.WriteLine(b is null
                ? "    -> bracket = NULL"
                : $"    -> lower={b.LowerLimit} upper={b.UpperLimit} rate={b.Rate}");

            if (b is null)
                throw new InvalidOperationException("Gelir vergisi dilimi bulunamadı (null döndü). Veri aralığını/tarihi kontrol et.");

            // Rate nullable ise kontrol et
            if (b.Rate == 0)
                throw new InvalidOperationException("Gelir vergisi diliminin Rate değeri NULL.");

            // Eğer Rate ölçekli geliyorsa burada normalize et (örn. 0.15 yerine 15000 gelirse)
            decimal rate = b.Rate;
            if (rate > 1m) // kaba ama etkili kontrol
            {
                rate = rate / 100000m; // senin parametrelerde 1e5 ölçek vardı
                Console.WriteLine($"    -> rate normalized to {rate}");
            }

            ctx.GelirVergisi = (ctx.GelirVergisiMatrah * rate).R2();
        }
    }
}
