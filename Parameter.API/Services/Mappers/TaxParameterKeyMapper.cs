using Parameter.API.Services.Interfaces;
using Shared.Enums;

namespace Parameter.API.Services.Mappers
{
    public class TaxParameterKeyMapper : ITaxParameterKeyMapper
    {
        private static readonly Dictionary<TaxParameterName, string> _map = new()
        {
            { TaxParameterName.DamgaVergisiOrani, "damga_vergisi_orani" },
            { TaxParameterName.SgkIsciOrani, "sgk_isci_orani" },
            { TaxParameterName.IssizlikOrani, "issizlik_orani" },
            { TaxParameterName.GunlukYemekMuafTutari, "gunluk_yemek_muaf_tutar" }
        };

        public string GetKey(TaxParameterName name) => _map[name];

    }
}
