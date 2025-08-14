using MassTransit;
using Microsoft.EntityFrameworkCore;
using Parameter.API.Mapping;
using Parameter.API.Models;
using Parameter.API.Models.Entities;
using Parameter.API.Services.Interfaces;
using Parameter.API.Services.Mappers;
using Shared.Abstractions;
using Shared.Infrastructure;
using Parameter.API.DTOs.TaxParameter;
using Parameter.API.DTOs.IncomeTaxBracket;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// DbContext
builder.Services.AddDbContext<ParameterDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer"));
});

// AutoMapper profile
builder.Services.AddAutoMapper(typeof(MappingProfile));

// MassTransit + RabbitMQ
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(builder.Configuration["RabbitMQ"]);
        configurator.ConfigureEndpoints(context);
    });
});

// Key Mapper
builder.Services.AddScoped<ITaxParameterKeyMapper, TaxParameterKeyMapper>();


builder.Services.AddScoped<
    ICrudMapper<TaxParameter, GetTaxParameterDto, CreateTaxParameterDto, UpdateTaxParameterDto>,
    CrudMapper<TaxParameter, GetTaxParameterDto, CreateTaxParameterDto, UpdateTaxParameterDto>>();

builder.Services.AddScoped<
    ICrudMapper<IncomeTaxBracket, GetIncomeTaxBracketDto, CreateIncomeTaxBracketDto, UpdateIncomeTaxBracketDto>,
    CrudMapper<IncomeTaxBracket, GetIncomeTaxBracketDto, CreateIncomeTaxBracketDto, UpdateIncomeTaxBracketDto>>();



// TaxParameter CRUD service
builder.Services.AddScoped<
    ICrudService<TaxParameter, Guid, GetTaxParameterDto, CreateTaxParameterDto, UpdateTaxParameterDto>,
    EfCrudService<ParameterDbContext, TaxParameter, Guid, GetTaxParameterDto, CreateTaxParameterDto, UpdateTaxParameterDto>>();

// IncomeTaxBracket CRUD service
builder.Services.AddScoped<
    ICrudService<IncomeTaxBracket, Guid, GetIncomeTaxBracketDto, CreateIncomeTaxBracketDto, UpdateIncomeTaxBracketDto>,
    EfCrudService<ParameterDbContext, IncomeTaxBracket, Guid, GetIncomeTaxBracketDto, CreateIncomeTaxBracketDto, UpdateIncomeTaxBracketDto>>();


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
