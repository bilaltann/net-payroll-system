using Shared.Enums;

namespace Calculation.API.Services.Parameters.TaxParameters
{
    public interface IParameterValueService
    {
        Task<decimal?> GetAsync(TaxParameterName name, int? year = null);
        void SetCache(TaxParameterName name, int year, decimal value); // event için
    }
}
