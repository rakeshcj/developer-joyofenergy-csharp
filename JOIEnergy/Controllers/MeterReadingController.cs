using JOIEnergy.Compositions;
using JOIEnergy.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JOIEnergy.Controllers
{
    [Route("readings")]
    public class MeterReadingController : Controller
    {
        private readonly IMeterReadingService _meterReadingService;

        public MeterReadingController(IMeterReadingService meterReadingService)
        {
            _meterReadingService = meterReadingService;
        }
        // POST api/values
        [HttpPost ("store")]
        public ObjectResult Post([FromBody]MeterReadings meterReadings)
        {
            if(!ModelState.IsValid){
                return new BadRequestObjectResult("Request Model Validation Failed");
            }
            if(!IsMeterReadingsValid(meterReadings)) {
                return new BadRequestObjectResult("Internal Server Error");
            }
            _meterReadingService.StoreReadings(meterReadings.SmartMeterId,meterReadings.ElectricityReadings);
            return new OkObjectResult("{}");
        }

        private bool IsMeterReadingsValid(MeterReadings meterReadings)
        {
            string smartMeterId = meterReadings.SmartMeterId;
            List<ElectricityReading> electricityReadings = meterReadings.ElectricityReadings;
            return smartMeterId != null && smartMeterId.Any()
                    && electricityReadings != null && electricityReadings.Any();
        }

        [HttpGet("read/{smartMeterId}")]
        public ObjectResult GetReading(string smartMeterId) {
            if (!ModelState.IsValid){
                return new BadRequestObjectResult("Request Model Validation Failed");
            }
            return new OkObjectResult(_meterReadingService.GetReadings(smartMeterId));
        }
    }
}
