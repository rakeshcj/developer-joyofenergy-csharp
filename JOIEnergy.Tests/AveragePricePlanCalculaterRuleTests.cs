using JOIEnergy.CalculatorStrategies;
using JOIEnergy.Domain;
using JOIEnergy.Services;
using System.Collections.Generic;
using System;
using Xunit;
using System.Linq;

namespace JOIEnergy.Tests
{
    public class AveragePricePlanCalculaterRuleTests
    {
        private ICalculatePricePlanOnRules _calculatePricePlanOnRules { get; set; }
        private static string SMART_METER_ID = "smart-meter-id";
        private MeterReadingService _meterReadingService;
        //private AccountService _accountService;

        private static string PRICE_PLAN_1_ID = "test-supplier";
        private static string PRICE_PLAN_2_ID = "best-supplier";
        private static string PRICE_PLAN_3_ID = "second-best-supplier";

        public AveragePricePlanCalculaterRuleTests()
        {
            _calculatePricePlanOnRules = new AveragePricePlanCalculaterRule();
            var readings = new Dictionary<string, List<Domain.ElectricityReading>>();
            _meterReadingService = new MeterReadingService(readings);
            //var pricePlans = new List<PricePlan>() {
            //    new PricePlan() { PlanName = PRICE_PLAN_1_ID, UnitRate = 10, PeakTimeMultiplier = [] },
            //    new PricePlan() { PlanName = PRICE_PLAN_2_ID, UnitRate = 1, PeakTimeMultiplier = [] },
            //    new PricePlan() { PlanName = PRICE_PLAN_3_ID, UnitRate = 2, PeakTimeMultiplier = [] }
            //};
            //var pricePlanService = new PricePlanService(pricePlans, _meterReadingService);
            //var smartMeterToPricePlanAccounts = new Dictionary<string, string>
            //{
            //    { SMART_METER_ID, PRICE_PLAN_1_ID }
            //};
            //_accountService = new AccountService(smartMeterToPricePlanAccounts);


        }

        [Fact]
        public void ShouldCalculateAverageCostForRangeIfPresent()
        {
            var electricityReading = new ElectricityReading() { Time = DateTime.Now.AddHours(-1), Reading = 15.0m };
            var otherReading = new ElectricityReading() { Time = DateTime.Now, Reading = 5.0m };
            var pricePlan = new PricePlan() { PlanName = PRICE_PLAN_1_ID, UnitRate = 10, PeakTimeMultiplier = [] };
            _meterReadingService.StoreReadings(SMART_METER_ID, new List<ElectricityReading>() { electricityReading, otherReading });

            var readings = _meterReadingService.GetReadings(SMART_METER_ID);
            var price = _calculatePricePlanOnRules.CalculateCost(readings, pricePlan, readings.Min(m => m.Time), readings.Max(m => m.Time));

            Assert.True(price != 0);
        }


        [Fact]
        public void ShouldCalculateAverageCostForSpecifiedRange()
        {
            var electricityReading = new ElectricityReading() { Time = DateTime.Now.AddHours(-1), Reading = 15.0m };
            var otherReading = new ElectricityReading() { Time = DateTime.Now, Reading = 5.0m };
            var extraReading = new ElectricityReading() { Time = DateTime.Now.AddHours(1), Reading = 5.0m };
            var pricePlan = new PricePlan() { PlanName = PRICE_PLAN_1_ID, UnitRate = 10, PeakTimeMultiplier = [] };
            _meterReadingService.StoreReadings(SMART_METER_ID, new List<ElectricityReading>() { electricityReading, otherReading, extraReading });

            var readings = _meterReadingService.GetReadings(SMART_METER_ID);
            var price = _calculatePricePlanOnRules.CalculateCost(readings, pricePlan, readings.Min(m => m.Time), DateTime.Now);

            Assert.True(price == 100);
        }


    }
}
