using System;
using System.Collections.Generic;
using System.Linq;
using JOIEnergy.Domain;

namespace JOIEnergy.Services
{
    public class PricePlanService : IPricePlanService
    {
        public interface Debug { void Log(string s); };

        private readonly List<PricePlan> _pricePlans;
        private IMeterReadingService _meterReadingService;
        private ICalculatePricePlanOnRules _ruleBasedPricePlanCalculator;

        public PricePlanService(List<PricePlan> pricePlan, IMeterReadingService meterReadingService, ICalculatePricePlanOnRules ruleBasedPricePlanCalculator)
        {
            _pricePlans = pricePlan;
            _meterReadingService = meterReadingService;
            _ruleBasedPricePlanCalculator = ruleBasedPricePlanCalculator;
        }

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
            var averagedCost = average/timeElapsed;
            return Math.Round(averagedCost * pricePlan.UnitRate, 3);
        }

        public Dictionary<string, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(string smartMeterId)
        {
            List<ElectricityReading> electricityReadings = _meterReadingService.GetReadings(smartMeterId);

            if (!electricityReadings.Any())
            {
                return new Dictionary<string, decimal>();
            }
            return _pricePlans.ToDictionary(plan => plan.PlanName, plan => calculateCost(electricityReadings, plan));
        }

        public decimal GetConsumptionCostOfElectricityReadingsBasedOnFilterForAPlan(string smartMeterId, string pricePlanId, DateTime startDate, DateTime endDate)
        {
            List<ElectricityReading> electricityReadings = _meterReadingService.GetReadings(smartMeterId);
            var currentPlan = _pricePlans.FirstOrDefault(pricePlan => pricePlan.PlanName == pricePlanId);
            if(currentPlan != null)
            {
                return _ruleBasedPricePlanCalculator.CalculateCost(electricityReadings, currentPlan, startDate, endDate);
            }

            return -1;
        }
    }
}
