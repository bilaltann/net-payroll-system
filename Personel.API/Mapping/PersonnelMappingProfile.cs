using AutoMapper;
using Personel.API.DTOs.Employee;
using Personel.API.DTOs.MonthlyWorkRecord;
using Personel.API.Models.Entities;
using Shared.Contracts.Personel;

namespace Personel.API.Mapping
{
    public class PersonnelMappingProfile:Profile
    {
        public PersonnelMappingProfile()
        {
            // Employee
            CreateMap<Employee, GetEmployeeDto>().ReverseMap();
            CreateMap<Employee, CreateEmployeeDto>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeDto>().ReverseMap();

            // MonthlyWorkRecord
            CreateMap<MonthlyWorkRecord, GetMonthlyWorkRecordDto>().ReverseMap();

            // Create DTO’n yoksa Upsert’ı create olarak kullanacağız:
            CreateMap<MonthlyWorkRecord, UpsertMonthlyWorkRecordDto>().ReverseMap();
            CreateMap<MonthlyWorkRecord, UpdateMonthlyWorkRecordDto>().ReverseMap();
        }

    }
}
