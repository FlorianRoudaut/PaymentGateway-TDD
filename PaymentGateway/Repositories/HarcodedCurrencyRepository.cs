using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentGateway.Domain;

namespace PaymentGateway.Repositories
{
    public class HarcodedCurrencyRepository : ICurrencyRepository
    {
        public List<Currency> LoadAll()
        {
            var list = new List<Currency>();
            list.Add(new Currency("EUR", "Euro"));
            list.Add(new Currency("GBP", "British Pound"));
            list.Add(new Currency("USD", "US Dollar"));

            return list;
        }
    }
}
