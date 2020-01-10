using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.AcquiringBanks
{
    public interface IAcquiringBankConnector
    {
        public Task<AcquiringBankResult> SendToAcquiringBank(PaymentRequest request);
    }
}
