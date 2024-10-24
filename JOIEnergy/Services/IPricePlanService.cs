using JOIEnergy.Domain;
using System;
using System.Collections.Generic;

namespace JOIEnergy.Services
{
    public interface IPricePlanService
    {
        Dictionary<string, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(string smartMeterId);
        decimal GetConsumptionCostOfElectricityReadingsBasedOnFilterForAPlan(string smartMeterId,string pricePlanId, DateTime startDateTime, DateTime endDateTime);
    }
}