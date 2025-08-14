using Calculation.API.Models.Parameters;

namespace Calculation.API.Services.Parameters
{
    public interface IParameterSnapshotProvider
    {
        Task<ParameterSnapshot> GetAsync(DateTime onDate);
    }
}
