using MassTransit;
using Microsoft.EntityFrameworkCore;
using Parameter.API.Models;
using Parameter.API.Models.Entities;
using Shared.Contracts;

namespace Parameter.API.Consumers
{
    public class GetTaxParameterConsumer(ParameterDbContext _context) : IConsumer<GetTaxParameterRequest>
    {
        public async Task Consume(ConsumeContext<GetTaxParameterRequest> context)
        {
            var year = context.Message.Year;

            var taxParameters = await _context.TaxParameters.Where(p => p.StartDate.Year <= year)
                .OrderByDescending(p => p.StartDate)
                .GroupBy(p => p.Key)
                .Select(g => g.First())
                .ToListAsync();

            var dictionary= taxParameters.
                ToDictionary(p => p.Key, p => p.Value);

            var response = new GetTaxParameterResponse
            {
                TaxParameters = dictionary
            };

            await context.RespondAsync(response);


        }


    }
}
