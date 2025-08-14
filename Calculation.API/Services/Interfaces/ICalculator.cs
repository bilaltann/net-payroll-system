namespace Calculation.API.Services.Interfaces
{
    public interface ICalculator<TInput, TResult>
    {
        Task<TResult> HesaplaAsync(TInput input);
    }
}
