using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Abstractions
{
    /// <summary>
    /// Generic CRUD sözleşmesi: TEntity için TDto/TCreate/TUpdate ile çalışır.
    /// EF bağımlılığı yoktur; implementasyonu servis tarafında yapılır.
    /// </summary>
    public interface ICrudService<TEntity ,TId,TGetDto,TCreateDto,TUpdateDto> where TEntity: 
        class , IHasId<TId>, new()
    {
        Task<TGetDto?> GetByIdAsync(TId id, CancellationToken ct = default);
        Task<List<TGetDto>> GetAllAsync(CancellationToken ct = default);

        Task<TGetDto> CreateAsync(TCreateDto dto , CancellationToken ct=default);
        Task<TGetDto?> UpdateAsync(TId id , TUpdateDto dto , CancellationToken ct=default);

        Task<bool> DeleteAsync(TId id, CancellationToken ct = default);
    }

    ///CancellationToken ct = default Amacı: Uzun süren async işlemleri (DB sorgusu, API isteği vb.) yarıda kesebilmek.
    /// Örneğin, HTTP isteği iptal edilirse (HttpContext.RequestAborted) bu token tetiklenir ve EF Core işlemi de iptal olur.


}
