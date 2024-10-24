using System.Collections.Generic;
using JOIEnergy.Compositions;

namespace JOIEnergy.Services
{
    public class AccountService : IAccountService
    { 
        private readonly Dictionary<string, string> _smartMeterToPricePlanAccounts;

        public AccountService(Dictionary<string, string> smartMeterToPricePlanAccounts) {
            _smartMeterToPricePlanAccounts = smartMeterToPricePlanAccounts;
        }

        public string GetPricePlanIdForSmartMeterId(string smartMeterId) {
            if (!_smartMeterToPricePlanAccounts.ContainsKey(smartMeterId))
            {
                return null;
            }
            return _smartMeterToPricePlanAccounts[smartMeterId];
        }
    }
}
