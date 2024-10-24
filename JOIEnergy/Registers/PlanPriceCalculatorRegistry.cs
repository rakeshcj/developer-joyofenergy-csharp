using JOIEnergy.Compositions;
using JOIEnergy.Enums;
using System.Collections.Generic;

namespace JOIEnergy.Registers
{
    public class PlanPriceCalculatorRegistry
    {
        private Dictionary<PlanPriceCalculatorType, IPlanPriceCalculator> _priceCalculatorRegister;


        public PlanPriceCalculatorRegistry()
        {
            _priceCalculatorRegister = new();
        }

        public PlanPriceCalculatorRegistry AddPriceCalculatorToRegistry(PlanPriceCalculatorType calculatorType, IPlanPriceCalculator calculator)
        {
            if (_priceCalculatorRegister.ContainsKey(calculatorType))
            {
                //Log that register already exists
                return this;
            }

            _priceCalculatorRegister.Add(calculatorType, calculator);
            return this;
        }

        public PlanPriceCalculatorRegistry RemovePriceCalculatorFromRegistry(PlanPriceCalculatorType calculatorType)
        {
            if (_priceCalculatorRegister.ContainsKey(calculatorType))
            {
                _priceCalculatorRegister.Remove(calculatorType);
                return this;
            }

            //log that type doesnt exist
            return this;
        }

        public IPlanPriceCalculator GetPricCalculatorForType(PlanPriceCalculatorType calculatorType)
        {
            if (_priceCalculatorRegister.ContainsKey(calculatorType))
            {
                return _priceCalculatorRegister[calculatorType];
            }

            //Log that No Calculator Exist for that type
            return null;
        }
    }
}
