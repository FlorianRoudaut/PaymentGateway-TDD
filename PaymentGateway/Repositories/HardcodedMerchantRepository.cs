using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentGateway.Domain;

namespace PaymentGateway.Repositories
{
    public class HardcodedMerchantRepository : IMerchantRepository
    {
        public List<Merchant> LoadAll()
        {
            var list = new List<Merchant>(); 
            list.Add(new Merchant("Amazon", "PiggyBank"));
            list.Add(new Merchant("Netflix", "PiggyBank"));
            list.Add(new Merchant("Apple", "OtherAcquirer"));
            return list;
        }
    }
}
