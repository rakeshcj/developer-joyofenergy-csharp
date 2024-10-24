using JOIEnergy.Enums;
using JOIEnergy.Factories;
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
    public class PlanPriceCalculatorFactoryTests
    {
        private readonly PlanPriceCalculatorFactory _planPriceCalcFactory;

        public PlanPriceCalculatorFactoryTests()
        {
            PlanPriceCalculatorRegistry planPriceCalculatorRegistry = new PlanPriceCalculatorRegistry()
                .AddPriceCalculatorToRegistry(
                    PlanPriceCalculatorType.AverageUnits, new AveragePlanPriceCalculator()
                );

            _planPriceCalcFactory = new PlanPriceCalculatorFactory(planPriceCalculatorRegistry);
        }

        [Fact]
        public void GivenCalculatorTypeThatIsRegisteredShouldReturnAverageCalculator()
        {
            Assert.NotNull(_planPriceCalcFactory.GetPlanPriceCalculatorFor(PlanPriceCalculatorType.AverageUnits));
        }
    }
}
