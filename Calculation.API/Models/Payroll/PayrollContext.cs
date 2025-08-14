using Calculation.API.Models.Parameters;
using Shared.Contracts.Personel;

namespace Calculation.API.Models.Payroll
{
    public class PayrollContext
    {
        //Giriş
        public required Guid EmployeeId { get; init; }
        public required int Year { get; init; }
        public required int Month { get; init; }
        public DateTime CalcDate => new DateTime(Year, Month, 1);

        // Dış veriler
        public GetEmployeeDto? Employee { get; set; }
        public GetMonthlyWorkRecordDto? Monthly { get; set; }

        // Parametreler (tek seferde doldurulacak)
        public required ParameterSnapshot TaxParameter { get; init; }

        // Ara değerler
        public decimal BrutGelir { get; set; }
        public decimal SgkMatrah { get; set; }
        public decimal SgkIsciPrimi { get; set; }
        public decimal IssizlikIsciPrimi { get; set; }
        public decimal GelirVergisiMatrah { get; set; }
        public decimal GelirVergisi { get; set; }
        public decimal DamgaVergisi { get; set; }

        public decimal NetUcret =>
            BrutGelir - SgkIsciPrimi - IssizlikIsciPrimi - GelirVergisi - DamgaVergisi;
    }
}

