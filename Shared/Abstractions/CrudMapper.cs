using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Abstractions
{
    public class CrudMapper<TEntity, TGetDto, TCreateDto, TUpdateDto>
    : ICrudMapper<TEntity, TGetDto, TCreateDto, TUpdateDto>
    where TEntity : class, new()
    {
        private readonly IMapper _mapper;

        public CrudMapper(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TEntity ToEntity(TCreateDto dto)
            => _mapper.Map<TEntity>(dto);

        public void UpdateEntity(TEntity entity, TUpdateDto dto)
            => _mapper.Map(dto, entity); // mevcut entity üzerinde update

        public TGetDto ToGetDto(TEntity entity)
            => _mapper.Map<TGetDto>(entity);
    }


}
