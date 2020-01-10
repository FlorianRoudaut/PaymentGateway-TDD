using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Domain
{
    public class PaymentResult
    {
        public Guid PaymentId { get; set; }

        public bool HasGatewayError { get; set; }

        public string GatewayErrorMessage { get; set; }

        public string AcquiringBank { get; set; }

        public string AcquiringBankStatus { get; set; }
        public string AcquiringBankPaymentId { get; set; }
        public DateTime ProcessedTime { get; set; }
    }
}
