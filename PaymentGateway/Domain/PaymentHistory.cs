using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Domain
{
    public class PaymentHistory
    {

        public bool HasGatewayError { get; set; }
        public string GatewayErrorMessage { get; set; }
        public string AcquiringBank { get; set; }
        public string AcquiringBankStatus { get; set; }
        public string AcquiringBankPaymentId { get; set; }
        public DateTime ProcessedTime { get; set; }
        public Guid GatewayPaymentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string MerchantName { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CardHolderName { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }

        public PaymentHistory ShallowCopy()
        {
            return MemberwiseClone() as PaymentHistory;
        }
    }
}
