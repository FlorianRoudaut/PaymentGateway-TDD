using PaymentGateway.Domain;
using PaymentGateway.Domain.PaymentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public class ProcessPaymentService : IProcessPaymentService
    {
        private IPaymentValidator _paymentValidator;

        public ProcessPaymentService(IPaymentValidator validator)
        {
            _paymentValidator = validator;
        }

        public async Task<PaymentResult> ProcessPayment(PaymentRequest paymentRequest)
        {
            var result = new PaymentResult();

            var errors = new List<string>();
            var time = DateTime.Now;
            var isValid = _paymentValidator.IsPaymentRequestValid(paymentRequest, time, errors);

            if (!isValid) result.GatewayError = string.Join('|', errors);

            return result;
        }
    }
}
