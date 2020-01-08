using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentGatewayTests.Setup
{
    public static class GlobalTestSetup
    {
        public static PaymentRequest InitValidRequest()
        {
            var request = new PaymentRequest();
            request.MerchantName = "Amazon";
            request.CardNumber = "1234 5678 9120 3012";
            request.Cvv = "123";
            request.ExpiryMonth = 12;
            request.ExpiryYear = 2021;
            request.CardHolderName = "M. Santa Claus";
            request.Amount = 100;
            request.Currency = "USD";
            return request;
        }
    }
}
