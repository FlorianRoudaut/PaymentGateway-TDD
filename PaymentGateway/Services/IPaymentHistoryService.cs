using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public interface IPaymentHistoryService
    {
        Task SaveHistory(PaymentRequest request, PaymentResult result);

        Task<List<PaymentHistory>> GetHistories(string merchantName);
    }
}
