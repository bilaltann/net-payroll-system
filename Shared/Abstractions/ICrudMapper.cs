using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Abstractions
{

    //// Mapper: Id tipinden bağımsız olsun
    public interface ICrudMapper<TEntity, TGetDto, TCreateDto, TUpdateDto>
         where TEntity : class, new()
    {
        TEntity ToEntity(TCreateDto dto);                  // Create: DTO -> Entity
        void UpdateEntity(TEntity entity, TUpdateDto dto); // Update: mevcut entity'yi DTO ile güncelle
        TGetDto ToGetDto(TEntity entity);                 // Read: Entity -> DTO


    }
    // neden update de void kullandık? Dolayısıyla methodun geriye dönmesi gereken yeni bir nesne yok;
    // zaten entity referansı değişmedi, sadece içi güncellendi.
}
