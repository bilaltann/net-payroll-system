using AutoMapper;
using Parameter.API.DTOs.IncomeTaxBracket;
using Parameter.API.DTOs.TaxParameter;
using Parameter.API.Models.Entities;

namespace Parameter.API.Mapping
{
    public class MappingProfile:Profile
    {
       public MappingProfile() 
        {
            // IncomeTaxBracket ↔ DTO eşlemeleri
            CreateMap<IncomeTaxBracket, CreateIncomeTaxBracketDto>().ReverseMap();
            CreateMap<IncomeTaxBracket, UpdateIncomeTaxBracketDto>().ReverseMap();
            CreateMap<IncomeTaxBracket, GetIncomeTaxBracketDto>().ReverseMap();

            // TaxParameter ↔ DTO eşlemeleri
            CreateMap<TaxParameter, CreateTaxParameterDto>().ReverseMap();
            CreateMap<TaxParameter, UpdateTaxParameterDto>().ReverseMap();
            CreateMap<TaxParameter, GetTaxParameterDto>().ReverseMap();
        }
    }
}

// ReverseMap() → İki yönlü dönüşüm sağlar (DTO → Entity, Entity → DTO)

