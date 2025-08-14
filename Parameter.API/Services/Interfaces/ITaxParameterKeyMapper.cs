using Shared.Enums;

namespace Parameter.API.Services.Interfaces
{
    public interface ITaxParameterKeyMapper
    {
        string GetKey(TaxParameterName name);
    }
}
