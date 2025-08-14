using Calculation.API.Models.Parameters;
using Calculation.API.Services.Parameters.TaxParameters;
using Shared.Enums;

namespace Calculation.API.Services.Parameters
{
    public sealed class ParameterSnapshotProvider(IParameterValueService _parameterService) : IParameterSnapshotProvider
    {
        const decimal SCALE = 100000m;
        public async Task<ParameterSnapshot> GetAsync(DateTime onDate)
        {
            var year = onDate.Year;

            var sgkIsci = await _parameterService.GetAsync(TaxParameterName.SgkIsciOrani, year);
            var issizlik = await _parameterService.GetAsync(TaxParameterName.IssizlikOrani, year);
            var damga = await _parameterService.GetAsync(TaxParameterName.DamgaVergisiOrani, year);
            var yemekMuaf = await _parameterService.GetAsync(TaxParameterName.GunlukYemekMuafTutari, year);
            Console.WriteLine($"[SNAP-IN] raw sgk={sgkIsci}, issiz={issizlik}, damga={damga}, yemek={yemekMuaf}");

            var snap= new ParameterSnapshot
            {
                SgkIsciOrani = (sgkIsci ?? 0m) / SCALE,
                IssizlikIsciOrani = (issizlik ?? 0m) / SCALE,
                DamgaVergisiOrani = (damga ?? 0m) / SCALE,
                GunlukYemekMuafTutar = (yemekMuaf ?? 0m) / SCALE

            };
            Console.WriteLine($"[SNAP-OUT] sgk={snap.SgkIsciOrani}, issiz={snap.IssizlikIsciOrani}, damga={snap.DamgaVergisiOrani}, yemek={snap.GunlukYemekMuafTutar}");
            return snap;

        }
    }
}
