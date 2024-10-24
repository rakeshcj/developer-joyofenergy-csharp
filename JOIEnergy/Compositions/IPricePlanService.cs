using System.Collections.Generic;

namespace JOIEnergy.Compositions
{
    public interface IPricePlanService
    {
        Dictionary<string, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(string smartMeterId);
    }
}