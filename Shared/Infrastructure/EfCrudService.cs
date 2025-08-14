using Microsoft.EntityFrameworkCore;
using Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure
{
    public class EfCrudService<TContext, TEntity, TId, TGetDto, TCreateDto, TUpdateDto> :
        ICrudService<TEntity, TId, TGetDto, TCreateDto, TUpdateDto>
        where TContext : DbContext
        where TEntity : class, IHasId<TId>, new()
    {
        protected readonly TContext _context;
        protected readonly DbSet<TEntity> _set;
        protected readonly ICrudMapper<TEntity, TGetDto, TCreateDto, TUpdateDto> _mapper;

        public EfCrudService(
            TContext context,
            ICrudMapper<TEntity, TGetDto, TCreateDto, TUpdateDto> mapper)
        {
            _context = context;
            _set = _context.Set<TEntity>();
            _mapper = mapper;
        }

        public async Task<TGetDto?> GetByIdAsync(TId id, CancellationToken ct = default)
        {
            var entity = await _set.FindAsync(new object[] { (object)id! }, ct);
            return entity is null ? default : _mapper.ToGetDto(entity);
        }

        public async Task<List<TGetDto>> GetAllAsync(CancellationToken ct = default)
        {
            var list = await _set.AsNoTracking().ToListAsync(ct);
            return list.Select(_mapper.ToGetDto).ToList();
        }

        public async Task<TGetDto> CreateAsync(TCreateDto dto, CancellationToken ct = default)
        {
            var entity = _mapper.ToEntity(dto);
            _set.Add(entity);
            await _context.SaveChangesAsync(ct);
            return _mapper.ToGetDto(entity);
        }

        public async Task<TGetDto?> UpdateAsync(TId id, TUpdateDto dto, CancellationToken ct = default)
        {
            var entity = await _set.FindAsync(new object[] { (object)id! }, ct);
            if (entity is null) return default;

            _mapper.UpdateEntity(entity, dto);
            await _context.SaveChangesAsync(ct);
            return _mapper.ToGetDto(entity);
        }

        public async Task<bool> DeleteAsync(TId id, CancellationToken ct = default)
        {
            var entity = await _set.FindAsync(new object[] { (object)id! }, ct);
            if (entity is null) return false;

            _set.Remove(entity);
            await _context.SaveChangesAsync(ct);
            return true;
        }
    }


}
