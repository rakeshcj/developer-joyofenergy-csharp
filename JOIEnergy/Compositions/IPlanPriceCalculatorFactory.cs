using JOIEnergy.Enums;

namespace JOIEnergy.Compositions
{
    public interface IPlanPriceCalculatorFactory
    {
        IPlanPriceCalculator GetPlanPriceCalculatorFor(PlanPriceCalculatorType calculatorType);
    }
}
