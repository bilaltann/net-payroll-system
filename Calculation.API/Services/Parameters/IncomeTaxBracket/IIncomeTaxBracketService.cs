using MassTransit.Futures.Contracts;
using Shared.Contracts.IncomeTaxBracket;

namespace Calculation.API.Services.Parameters.IncomeTaxBracket
{
    public interface IIncomeTaxBracketService
    {
        Task<IReadOnlyList<GetIncomeTaxBracketDto>> GetActiveAsync(DateTime onDate, CancellationToken ct = default);
        Task<GetIncomeTaxBracketDto?> GetBracketAsync(decimal income, DateTime onDate, CancellationToken ct = default);
        void Invalidate(DateTime? onDate = null);
    }
}
