using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentGateway.Domain;
using PiggyBankApi.Functions;
using PiggyBankApi.Model;

namespace PaymentGateway.AcquiringBanks
{
    public class PiggyBankConnector : IAcquiringBankConnector
    {
        public async Task<AcquiringBankResult> SendToAcquiringBank(PaymentRequest request)
        {
            var piggyRequest = BuildPiggyRequest(request);
            var piggyStatus = await ApiFunctions.ProcessPaymentRequestAsync(piggyRequest);
            return BuildPaymentProcessed(piggyStatus);
        }

        private static PiggyPaymentRequest BuildPiggyRequest(PaymentRequest processPayment)
        {
            var piggyRequest = new PiggyPaymentRequest();
            piggyRequest.CardNumber = processPayment.CardNumber;
            piggyRequest.Cvv = processPayment.Cvv;
            piggyRequest.ExpiryMonth = processPayment.ExpiryMonth;
            piggyRequest.ExpiryYear = processPayment.ExpiryYear;
            piggyRequest.From = processPayment.CardHolderName;
            piggyRequest.To = processPayment.MerchantName;
            piggyRequest.Amount = processPayment.Amount;
            piggyRequest.Currency = processPayment.Currency;
            return piggyRequest;
        }

        private static AcquiringBankResult BuildPaymentProcessed(PiggyPaymentStatus status)
        {
            var paymentProcessed = new AcquiringBankResult();
            paymentProcessed.Status = status.Status.ToString();
            paymentProcessed.PaymentId = status.PaymentId;
            return paymentProcessed;
        }
    }
}
