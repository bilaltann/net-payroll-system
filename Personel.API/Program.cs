using Microsoft.EntityFrameworkCore;
using Personel.API.DTOs.Employee;
using Personel.API.DTOs.MonthlyWorkRecord;
using Personel.API.Mapping;
using Personel.API.Models;
using Personel.API.Models.Entities;
using Shared.Abstractions;
using Shared.Contracts.Personel;
using Shared.Infrastructure;
using System;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAll", p => p
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod());
});

builder.Services.AddDbContext<PersonelDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));

builder.Services.AddAutoMapper(typeof(PersonnelMappingProfile));

// AutoMapper tabanlý generic mapper
builder.Services.AddScoped<
    ICrudMapper<Employee, GetEmployeeDto, CreateEmployeeDto, UpdateEmployeeDto>,
    CrudMapper<Employee, GetEmployeeDto, CreateEmployeeDto, UpdateEmployeeDto>>();


builder.Services.AddScoped<
    ICrudMapper<MonthlyWorkRecord, GetMonthlyWorkRecordDto, UpsertMonthlyWorkRecordDto, UpdateMonthlyWorkRecordDto>,
    CrudMapper<MonthlyWorkRecord, GetMonthlyWorkRecordDto, UpsertMonthlyWorkRecordDto, UpdateMonthlyWorkRecordDto>>();

// Generic EF CRUD servisleri
builder.Services.AddScoped<
    ICrudService<Employee, Guid, GetEmployeeDto, CreateEmployeeDto, UpdateEmployeeDto>,
    EfCrudService<PersonelDbContext, Employee, Guid, GetEmployeeDto, CreateEmployeeDto, UpdateEmployeeDto>>();

builder.Services.AddScoped<
    ICrudService<MonthlyWorkRecord, Guid, GetMonthlyWorkRecordDto, UpsertMonthlyWorkRecordDto, UpdateMonthlyWorkRecordDto>,
    EfCrudService<PersonelDbContext, MonthlyWorkRecord, Guid, GetMonthlyWorkRecordDto, UpsertMonthlyWorkRecordDto, UpdateMonthlyWorkRecordDto>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowAll");   // <-- eklendi

app.MapControllers();

app.Run();
