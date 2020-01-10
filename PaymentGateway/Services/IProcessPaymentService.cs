using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public interface IProcessPaymentService
    {
        Task<PaymentResult> ProcessPayment(PaymentRequest paymentRequest);
    }
}
