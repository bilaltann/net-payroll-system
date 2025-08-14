// Services/Parameters/ParameterValueService.cs
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Enums;

namespace Calculation.API.Services.Parameters.TaxParameters
{
    public class ParameterValueService(HttpClient _httpClient) : IParameterValueService
    {

        // key: "damga_vergisi_orani-2025" gibi
        private static readonly ConcurrentDictionary<string, decimal> _cache = new();
        //Amaç: Hafızada (RAM) parametre değerlerini saklamak.
        // Anahtar(key) : "ParametreAdı-Yıl" formatında tutulur(ör. "DamgaVergisiOrani-2025").
        // <Değer(value) : Parametrenin sayısal değeri(decimal).

        public async Task<decimal?> GetAsync(TaxParameterName name, int? year = null)
        //Parameter.API’den parametre değeri getirir (veya cache’ten okur).
        {


            // var cacheKey Amaç: İstenen değer zaten cache’te varsa HTTP isteği yapmadan döndürmek.

            var cacheKey = year.HasValue ? $"{name}-{year}" : $"{name}"; //Yıl verilmişse "Ad-Yıl", verilmemişse "Ad".
            if (_cache.TryGetValue(cacheKey, out var cached))
                return cached;
            //Varsa cached değişkenine değeri atar ve direkt return yapar.
            // Yoksa HTTP’den çekme aşamasına geçer.



            // Parameter.API: GET /api/taxparameters/by-name/{name}?year=YYYY
            //Amaç: Parameter.API’ye doğru endpoint ile istek atmak. yıl varsa ona göre yoksa ona göre

            var url = year is null
                ? $"/api/taxparameters/by-name/{name}"
                : $"/api/taxparameters/by-name/{name}?year={year}";

            //urlye istek atılır
            using var resp = await _httpClient.GetAsync(url);
            if (!resp.IsSuccessStatusCode) return null;


            //Amaç: API’den gelen cevabı decimal tipine dönüştürmek.
            var s = await resp.Content.ReadAsStringAsync();
            if (!decimal.TryParse(s, out var value)) return null;

            _cache[cacheKey] = value;
            return value;
            //Çekilen değer hem cache’e yazılır hem de çağıran metoda döndürülür.


        }

        public void SetCache(TaxParameterName name, int year, decimal value)
        {
            //Amaç: Event geldiğinde (ör. Parameter.API’de veri güncellendiğinde)
            // HTTP isteği atmadan cache’i güncellemek.
            // Event Consumer bu metodu çağırır → Cache anında tazelenir.
            _cache[$"{name}-{year}"] = value;
        }
    }
}
