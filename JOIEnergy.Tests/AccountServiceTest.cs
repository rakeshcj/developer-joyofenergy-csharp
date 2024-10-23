using System;
using System.Collections.Generic;
using JOIEnergy.Enums;
using JOIEnergy.Services;
using Xunit;

namespace JOIEnergy.Tests
{
    public class AccountServiceTest
    {
        private const string PRICE_PLAN_ID = "price-plan-id";
        private const string SMART_METER_ID = "smart-meter-id";

        private readonly AccountService _accountService;

        public AccountServiceTest()
        {
            var smartMeterToPricePlanAccounts = new Dictionary<string, string>();
            smartMeterToPricePlanAccounts.Add(SMART_METER_ID, PRICE_PLAN_ID);

            _accountService = new AccountService(smartMeterToPricePlanAccounts);
        }

        [Fact]
        public void GivenTheSmartMeterIdReturnsThePricePlanId()
        {
            var result = _accountService.GetPricePlanIdForSmartMeterId(SMART_METER_ID);
            Assert.Equal(PRICE_PLAN_ID, result);
        }
        
        [Fact]
        public void GivenAnUnknownSmartMeterIdReturnsNull()
        {
            var result = _accountService.GetPricePlanIdForSmartMeterId("non-existent");
            Assert.Null(result);
        }

    }
}
