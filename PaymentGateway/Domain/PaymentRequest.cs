using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Domain
{
    public class PaymentRequest
    {
        public string MerchantName { get; set; }
        public string CardNumber { get; set; }
        public string Cvv { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string CardHolderName { get; set; }
        public double Amount { get; set; }
        public string Currency { get; set; }
    }
}
