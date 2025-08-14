namespace Calculation.API.Models.Payroll
{
    public class PayrollResult
    {
        public Guid EmployeeId { get; init; }
        public int Year { get; init; }
        public int Month { get; init; }

        public decimal BrutGelir { get; init; }
        public decimal SgkMatrah { get; init; }
        public decimal SgkIsciPrimi { get; init; }
        public decimal IssizlikIsciPrimi { get; init; }
        public decimal GelirVergisiMatrah { get; init; }
        public decimal GelirVergisi { get; init; }
        public decimal DamgaVergisi { get; init; }
        public decimal NetUcret { get; init; }
    }
}
