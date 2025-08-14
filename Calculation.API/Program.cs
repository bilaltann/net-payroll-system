using Calculation.API.Services.Parameters;                // <-- ParameterSnapshotProvider için
using Calculation.API.Services.Payroll.Orchestrator;      // <-- IPayrollCalculator için
using Calculation.API.Services.Payroll.Steps;
using Calculation.API.Services.Interfaces;
using MassTransit;
using Calculation.API.Consumers;
using Calculation.API.Services.Parameters.TaxParameters;
using Calculation.API.Services.Parameters.IncomeTaxBracket;
using Calculation.API.Services.Parameters.Personel;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();


var cfg = builder.Configuration;
Console.WriteLine("ParameterApi = " + builder.Configuration["Services:ParameterApi"]);
Console.WriteLine("PersonelApi  = " + builder.Configuration["Services:PersonelApi"]);
// ---- Parameter.API HttpClients ---
// Parameter.API
builder.Services.AddHttpClient<IParameterValueService, ParameterValueService>(c =>
{
    c.BaseAddress = new Uri(cfg["Services:ParameterApi"]!);
});

builder.Services.AddHttpClient<IIncomeTaxBracketService, IncomeTaxBracketService>(c =>
{
    c.BaseAddress = new Uri(cfg["Services:ParameterApi"]!);
});

builder.Services.AddHttpClient<
    Calculation.API.Services.Parameters.Personel.IPersonelReadService,
    Calculation.API.Services.Parameters.Personel.PersonelReadService
>(c =>
{
    c.BaseAddress = new Uri(cfg["Services:PersonelApi"]!);
});



// ---- Parametre snapshot sağlayıcı (tek seferde çekmek için) ----
builder.Services.AddScoped<IParameterSnapshotProvider, ParameterSnapshotProvider>();

// ---- Hesaplama pipeline (adımlar) ----
// Sıra: 1 Brüt, 2 SGK Matrah, 3 İşsizlik(İşçi), 4 SGK(İşçi), 5 GV Matrah, 6 GV, 7 Damga
builder.Services.AddScoped<ICalculationStep, BrutGelirStep>();
builder.Services.AddScoped<ICalculationStep, SgkMatrahStep>();
builder.Services.AddScoped<ICalculationStep, IssizlikIsciPrimiStep>();
builder.Services.AddScoped<ICalculationStep, SgkIsciPrimiStep>();
builder.Services.AddScoped<ICalculationStep, GelirVergisiMatrahStep>();
builder.Services.AddScoped<ICalculationStep, GelirVergisiStep>();
builder.Services.AddScoped<ICalculationStep, DamgaVergisiStep>();

// ---- Orkestratör ----
builder.Services.AddScoped<IPayrollCalculator, PayrollCalculator>();

// ---- MassTransit (senin kodun) ----
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<TaxParameterUpdatedEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQ"]);
        cfg.ReceiveEndpoint("tax-parameter-updated-event", e =>
        {
            e.ConfigureConsumer<TaxParameterUpdatedEventConsumer>(context);
        });
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
