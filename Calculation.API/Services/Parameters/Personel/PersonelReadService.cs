using Shared.Contracts.Personel;
using static System.Net.WebRequestMethods;

namespace Calculation.API.Services.Parameters.Personel
{
    public class PersonelReadService(HttpClient _httpClient) : IPersonelReadService
    {
        public Task<GetEmployeeDto?> GetEmployeeAsync(Guid id)
        {
            if (_httpClient.BaseAddress is null)
                throw new InvalidOperationException("BaseAddress NULL! Services:PersonelApi okunamadı.");

            return _httpClient.GetFromJsonAsync<GetEmployeeDto>($"api/Employees/{id}");
       
        }

        public Task<GetMonthlyWorkRecordDto?> GetMonthlyByKeyAsync(Guid employeeId, int year, int month)
            => _httpClient.GetFromJsonAsync<GetMonthlyWorkRecordDto>($"api/MonthlyWorkRecords/by-key?employeeId={employeeId}&year={year}&month={month}");
        
    }
}
