using PaymentGateway.Domain;
using PaymentGateway.Domain.PaymentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public class ProcessPaymentService
    {
        public static async Task<PaymentResult> ProcessPayment(PaymentRequest paymentRequest)
        {
            var result = new PaymentResult();

            var errors = new List<string>();
            var time = DateTime.Now;
            var isValid = PaymentValidator.IsPaymentRequestValid(paymentRequest, time, errors);

            if (!isValid) result.GatewayError = string.Join('|', errors);

            return result;
        }
    }
}
