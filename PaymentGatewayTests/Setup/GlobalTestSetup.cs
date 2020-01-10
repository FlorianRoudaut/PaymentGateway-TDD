using PaymentGateway.Controllers;
using PaymentGateway.Domain;
using PaymentGateway.Domain.PaymentValidation;
using PaymentGateway.Repositories;
using PaymentGateway.Services;
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

        public static PaymentValidator GetPaymentValidator()
        {
            var currencyRepository = new HarcodedCurrencyRepository();
            return new PaymentValidator(currencyRepository);
        }

        public static ProcessPaymentService GetPaymentService()
        {
            var paymentValidator = GetPaymentValidator();
            var merchantRepository = new HardcodedMerchantRepository();
            return new ProcessPaymentService(paymentValidator, merchantRepository);
        }

        public static ProcessPaymentController GetProcessPaymentController()
        {
            var paymentService = GetPaymentService();
            return new ProcessPaymentController(paymentService);
        }
    }
}
