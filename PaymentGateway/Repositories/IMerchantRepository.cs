using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Repositories
{
    public interface IMerchantRepository
    {
        public List<Merchant> LoadAll();
    }
}
