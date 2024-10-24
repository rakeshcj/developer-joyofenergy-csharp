using JOIEnergy.Domain;
using JOIEnergy.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System;
using System.Collections.Generic;

namespace JOIEnergy.Controllers
{
    [Route("calculate-price-plan")]
    public class CalculatePricePlanController : Controller {
        private readonly IPricePlanService _pricePlanService;
        private readonly IAccountService _accountService;

        public CalculatePricePlanController(IPricePlanService pricePlanService, IAccountService accountService)
        {
            _pricePlanService = pricePlanService;
            _accountService = accountService;
        }

        [HttpGet("calculateCostForPricePlan")]
        public ObjectResult CalculatedCostForEachPricePlan([FromBody] CalculateCostOnFilter costOnFilter)
        {
            string pricePlanId = _accountService.GetPricePlanIdForSmartMeterId(costOnFilter.SmartMeterId);
            var result = _pricePlanService.GetConsumptionCostOfElectricityReadingsBasedOnFilterForAPlan(costOnFilter.SmartMeterId, pricePlanId, costOnFilter.StartDate, costOnFilter.EndDate);

            if (result != -1)
            {
                return new NotFoundObjectResult(string.Format("Plan for Meter ID ({0}) not found", costOnFilter.SmartMeterId));
            }
            else
            {
                return new ObjectResult(new Dictionary<string, object>()
                {
                    { costOnFilter.SmartMeterId, result },
                });
            }
            
        }
    }
}
