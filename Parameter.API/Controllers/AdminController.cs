using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Parameter.API.Models;
using Shared.Contracts;
using Shared.Events;

namespace Parameter.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController(ParameterDbContext _context , IPublishEndpoint _publishEndpoint) : ControllerBase
{
  

    [HttpPost("sync-tax-parameters")]
    public async Task<IActionResult> SyncAllTaxParameters()
    {
        var parameters = _context.TaxParameters.ToList();

        foreach (var param in parameters)
        {
            var eventMessage = new TaxParameterUpdatedEvent
            {
                Key = param.Key,
                Value = param.Value,
                Year = param.StartDate.Year
            };

            await _publishEndpoint.Publish(eventMessage);
        }

        return Ok(new { message = "Tüm veriler publish edildi." });
    }
}
