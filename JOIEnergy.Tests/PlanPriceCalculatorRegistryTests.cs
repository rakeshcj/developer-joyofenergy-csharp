using JOIEnergy.Compositions;
using JOIEnergy.Registers;
using JOIEnergy.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace JOIEnergy.Tests
{
    public class PlanPriceCalculatorRegistryTests
    {

        [Fact]
        public void GivenAUnregisteredCalculatorObjectShoudlReturnObject()
        {
            PlanPriceCalculatorRegistry registry = new PlanPriceCalculatorRegistry();
            IPlanPriceCalculator calculator = new AveragePlanPriceCalculator();
            registry.AddPriceCalculatorToRegistry(Enums.PlanPriceCalculatorType.AverageUnits, calculator);

            Assert.NotNull(registry.GetPricCalculatorForType(Enums.PlanPriceCalculatorType.AverageUnits));
        }

        [Fact]
        public void GivenARegisteredCalculatorObjectShouldReturnNull()
        {
            PlanPriceCalculatorRegistry registry = new PlanPriceCalculatorRegistry();
            Assert.Null(registry.GetPricCalculatorForType(Enums.PlanPriceCalculatorType.AverageUnits));
        }


    }
}
