using System;
using System.Collections.Generic;
using System.Linq;
using JOIEnergy.Compositions;
using JOIEnergy.Domain;

namespace JOIEnergy.Services
{
    public class PricePlanService : IPricePlanService
    {
        private readonly List<PricePlan> _pricePlans;
        private readonly IMeterReadingService _meterReadingService;
        private readonly IPlanPriceCalculatorFactory _planPriceCalculatorFactory;

        public PricePlanService(List<PricePlan> pricePlan, IMeterReadingService meterReadingService, IPlanPriceCalculatorFactory planPriceCalculatorFactory)
        {
            _pricePlans = pricePlan;
            _meterReadingService = meterReadingService;
            _planPriceCalculatorFactory = planPriceCalculatorFactory;
        }

        public Dictionary<string, decimal> GetConsumptionCostOfElectricityReadingsForEachPricePlan(string smartMeterId)
        {
            List<ElectricityReading> electricityReadings = _meterReadingService.GetReadings(smartMeterId);

            if (!electricityReadings.Any())
            {
                return new Dictionary<string, decimal>();
            }
            return _pricePlans.ToDictionary(
                plan => plan.PlanName, 
                plan => _planPriceCalculatorFactory
                            .GetPlanPriceCalculatorFor(Enums.PlanPriceCalculatorType.AverageUnits)
                            .CalculateCost(electricityReadings, plan)
            );
        }
    }
}
