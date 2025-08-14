using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Abstractions
{
    public interface IHasId<TId>
    {
        TId Id { get; set; }

    }

    //Amaç: “Bu entity’nin bir kimliği (Id) var” sözleşmesi.
    // Neden lazım? Generic CRUD servis yazarken her entity’nin Id alanına güvenebilmek isteriz.
    // Bu arayüzü uygulatınca, generic servis şunu yapabilir FindAsync(id) ile bul, Remove(entity) ile sil,

}
