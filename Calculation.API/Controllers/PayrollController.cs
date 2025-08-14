using Calculation.API.Models.Parameters;
using Calculation.API.Models.Payroll;
using Calculation.API.Services.Parameters;
using Calculation.API.Services.Payroll.Orchestrator;
using Microsoft.AspNetCore.Mvc;

namespace Calculation.API.Controllers
{
    public class PayrollController(IPayrollCalculator _calculator , IParameterSnapshotProvider _provider) : ControllerBase
    {
        [HttpPost("calculate")]
        public async Task <ActionResult<PayrollResult>> Calculate([FromBody] PayrollRequest req)
        {
            var result = await _calculator.HesaplaAsync(req);
            return Ok(result);
        }

        [HttpGet("parameters")]
        public async Task<ActionResult<ParameterSnapshot>> GetParameters([FromQuery] int year, [FromQuery] int month)
        {
            var snap = await _provider.GetAsync(new DateTime(year, month, 1));
            return Ok(snap);
        }

    }
}
