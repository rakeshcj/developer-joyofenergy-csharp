using JOIEnergy.Compositions;
using JOIEnergy.Enums;
using JOIEnergy.Registers;

namespace JOIEnergy.Factories
{
    /// <summary>
    /// Reason for having a plan price calcualtor factory is to have future extensibility. 
    /// There might be a different service thats going to calculate price based on multiple PlanPriceCalculators that factory only holds responsiblity for returning objects either single or multiple.
    /// For now only single is implemented. 
    /// </summary>
    public class PlanPriceCalculatorFactory : IPlanPriceCalculatorFactory
    {
        private readonly PlanPriceCalculatorRegistry _registry;
        public PlanPriceCalculatorFactory(PlanPriceCalculatorRegistry register) 
        {
            _registry = register;
        }

        public IPlanPriceCalculator GetPlanPriceCalculatorFor(PlanPriceCalculatorType calculatorType)
        {
            return _registry.GetPricCalculatorForType(calculatorType);
        }
    }
}
