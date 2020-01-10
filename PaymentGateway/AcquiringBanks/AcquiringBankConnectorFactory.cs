using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.AcquiringBanks
{
    public class AcquiringBankConnectorFactory
    {
        public static IAcquiringBankConnector GetConnector(string bankName)
        {
            if (string.IsNullOrEmpty(bankName)) return null;
            if (bankName.Equals("PiggyBank")) return new PiggyBankConnector();

            return null;
        }
    }
}
