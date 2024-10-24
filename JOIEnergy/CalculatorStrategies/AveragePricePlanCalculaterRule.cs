using JOIEnergy.Domain;
using JOIEnergy.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JOIEnergy.CalculatorStrategies
{
    public class AveragePricePlanCalculaterRule : ICalculatePricePlanOnRules
    {
        private decimal calculateAverageReading(List<ElectricityReading> electricityReadings)
        {
            var newSummedReadings = electricityReadings.Select(readings => readings.Reading).Aggregate((reading, accumulator) => reading + accumulator);

            return newSummedReadings / electricityReadings.Count();
        }

        private decimal calculateTimeElapsed(List<ElectricityReading> electricityReadings)
        {
            var first = electricityReadings.Min(reading => reading.Time);
            var last = electricityReadings.Max(reading => reading.Time);

            return (decimal)(last - first).TotalHours;
        }
        private decimal calculateCost(List<ElectricityReading> electricityReadings, PricePlan pricePlan)
        {
            var average = calculateAverageReading(electricityReadings);
            var timeElapsed = calculateTimeElapsed(electricityReadings);
            var averagedCost = average / timeElapsed;
            return Math.Round(averagedCost * pricePlan.UnitRate, 3);
        }

        private List<ElectricityReading> FilterElectricalReadings(List<ElectricityReading> electricityReadings, DateTime startDateTime, DateTime endDateTime)
        {
            return electricityReadings.Where(reading => reading.Time >= startDateTime && reading.Time <= endDateTime).ToList();
        }

        public decimal CalculateCost(List<ElectricityReading> electricityReadings, PricePlan pricePlan, DateTime startDateTime, DateTime endDateTime)
        {
            List<ElectricityReading> filteredReadings = FilterElectricalReadings(electricityReadings, startDateTime, endDateTime);
            if(filteredReadings.Count == 0)
            {
                return -1;
            }
            return calculateCost(filteredReadings, pricePlan);
        }
    }
}
