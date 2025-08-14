// Services/Parameters/IncomeTaxBracketService.cs
using System.Collections.Concurrent;
using System.Net.Http.Json;
using Shared.Contracts.IncomeTaxBracket;

namespace Calculation.API.Services.Parameters.IncomeTaxBracket
{
    // NOT: Mevcut ParameterValueService’e hiç dokunmuyoruz.
    public class IncomeTaxBracketService(HttpClient _httpClient) : IIncomeTaxBracketService
    {
        // key: "brackets-20250101"  -> o güne ait liste cache
        private static readonly ConcurrentDictionary<string, List<GetIncomeTaxBracketDto>> _cache = new();

        private static string Key(DateTime d) => $"brackets-{d:yyyyMMdd}";

        public async Task<IReadOnlyList<GetIncomeTaxBracketDto>> GetActiveAsync(DateTime onDate, CancellationToken ct = default)
        {
            var k = Key(onDate.Date);
            if (_cache.TryGetValue(k, out var list))
                return list;

            // Parameter.API: GET /api/IncomeTaxBrackets/active?onDate=...
            var url = $"/api/IncomeTaxBrackets/active?onDate={onDate:O}";
            var fetched = await _httpClient.GetFromJsonAsync<List<GetIncomeTaxBracketDto>>(url, ct) ?? new();
            fetched = fetched.OrderBy(b => b.LowerLimit).ToList();
            Console.WriteLine($"[DEBUG] Calling /active with onDate={onDate:yyyy-MM-dd}");
            _cache[k] = fetched;
            return fetched;
        }

        public async Task<GetIncomeTaxBracketDto?> GetBracketAsync(decimal income, DateTime onDate, CancellationToken ct = default)
        {
            var list = await GetActiveAsync(onDate, ct);

            // DEBUG LOG
            Console.WriteLine($"[DEBUG] Matrah={income} - Tarih={onDate:yyyy-MM-dd} - Gelen {list.Count} dilim:");
            foreach (var b in list)
            {
                Console.WriteLine($"    Lower={b.LowerLimit}, Upper={(b.UpperLimit?.ToString() ?? "NULL")}, Rate={b.Rate}");
            }

            return list.FirstOrDefault(b =>
                income >= b.LowerLimit &&
                (b.UpperLimit is null || income <= b.UpperLimit));
        }

        public void Invalidate(DateTime? onDate = null)
        {
            if (onDate is not null)
            {
                _cache.TryRemove(Key(onDate.Value.Date), out _);
                return;
            }

            // Genel temizlik (bugün ±3 gün)
            foreach (var i in Enumerable.Range(-3, 7))
                _cache.TryRemove(Key(DateTime.Today.AddDays(i)), out _);
        }
    }
}
