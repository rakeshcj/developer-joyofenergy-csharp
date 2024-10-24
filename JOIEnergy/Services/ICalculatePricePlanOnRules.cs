using JOIEnergy.Domain;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace JOIEnergy.Services
{
    public interface ICalculatePricePlanOnRules
    {
        decimal CalculateCost(List<ElectricityReading> electricityReadings, PricePlan pricePlan, DateTime startDateTime, DateTime endDateTime);
    }
}
