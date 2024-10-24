using System;

namespace JOIEnergy.Domain
{
    public class CalculateCostOnFilter
    {
        public string SmartMeterId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

}
