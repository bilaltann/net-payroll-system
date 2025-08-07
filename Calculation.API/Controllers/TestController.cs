using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Shared.Contracts;

namespace Calculation.API.Controllers
{
    [ApiController]
    [Route("api/test")]
    public class TestController : ControllerBase
    {
        private readonly IRequestClient<GetTaxParameterRequest> _client;

        public TestController(IRequestClient<GetTaxParameterRequest> client)
        {
            _client = client;
        }

        [HttpGet("{year}")]
        public async Task<IActionResult> Get(int year)
        {
            var response = await _client.GetResponse<GetTaxParameterResponse>(
                new GetTaxParameterRequest { Year = year });

            return Ok(response.Message.TaxParameters);
        }
    }
}
