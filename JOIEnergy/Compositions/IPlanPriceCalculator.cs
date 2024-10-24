using JOIEnergy.Domain;
using System.Collections.Generic;

namespace JOIEnergy.Compositions
{
    public interface IPlanPriceCalculator
    {
        decimal CalculateCost(List<ElectricityReading> electricityReadings, PricePlan pricePlan);
    }
}
