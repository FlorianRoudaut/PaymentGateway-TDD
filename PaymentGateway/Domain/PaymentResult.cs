using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Domain
{
    public class PaymentResult
    {
        public Guid PaymentId { get; set; }

        public string GatewayError { get; set; }
    }
}
