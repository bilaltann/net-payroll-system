using Calculation.API.Models.Payroll;
using Calculation.API.Services.Interfaces;

namespace Calculation.API.Services.Payroll.Orchestrator
{
    public interface IPayrollCalculator : ICalculator<PayrollRequest, PayrollResult>
    {
    }
}
