namespace Calculation.API.Models.Payroll
{
    public sealed class PayrollRequest
    {
        public Guid EmployeeId { get; init; }
        public int Year { get; init; }
        public int Month { get; init; }
    }

    //API’de /api/payroll/calculate gibi bir endpoint açtığında,
    //dışarıdan JSON olarak bu veriler gelecek:

    //Hesaplayıcı (ICalculator) bu nesneyi alacak, içeride EmployeeId, Year, Month bilgilerini
    //kullanarak Personel.API ve Parameter.API’den verileri çekecek.
}
