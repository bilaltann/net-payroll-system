using Calculation.API.Services;
using Calculation.API.Services.Interfaces;
using Calculation.API.Services.Parameters.TaxParameters;
using MassTransit;
using Shared.Enums;
using Shared.Events;

namespace Calculation.API.Consumers
{
    public class TaxParameterUpdatedEventConsumer(IParameterValueService _parameterValueService) : IConsumer<TaxParameterUpdatedEvent>
    {
        public Task Consume(ConsumeContext<TaxParameterUpdatedEvent> context)
        {
            // Parameter.API eventi key string ile gönderiyor.
            // Enum’a map etmemiz gerek: key -> enum
            var name = context.Message.Key switch
            {
                "damga_vergisi_orani" => TaxParameterName.DamgaVergisiOrani,
                "sgk_isci_orani" => TaxParameterName.SgkIsciOrani,
                "issizlik_orani" => TaxParameterName.IssizlikOrani,
                "gunluk_yemek_muaf_tutar" => TaxParameterName.GunlukYemekMuafTutari,
                _ => (TaxParameterName?)null
            };

            if (name is not null)
                _parameterValueService.SetCache(name.Value, context.Message.Year, context.Message.Value);

            return Task.CompletedTask;
        }
    }
}
