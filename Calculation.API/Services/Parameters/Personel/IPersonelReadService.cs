using Shared.Contracts.Personel;

namespace Calculation.API.Services.Parameters.Personel
{
    public interface IPersonelReadService
    {
        Task<GetEmployeeDto?> GetEmployeeAsync(Guid id);

        // Ayın bordrosu için tek kayıt:
        Task<GetMonthlyWorkRecordDto?> GetMonthlyByKeyAsync(Guid employeeId, int year, int month);
    }
}
