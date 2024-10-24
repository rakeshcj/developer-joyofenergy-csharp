using JOIEnergy.Enums;

namespace JOIEnergy.Compositions
{
    public interface IAccountService
    {
        string GetPricePlanIdForSmartMeterId(string smartMeterId);
    }
}