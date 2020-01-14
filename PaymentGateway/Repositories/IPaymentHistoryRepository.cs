using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Repositories
{
    public interface IPaymentHistoryRepository
    {
        Task Save(PaymentHistory history);

        Task<List<PaymentHistory>> LoadAll(string merchantName);

        Task<PaymentHistory> GetById(Guid paymentId);

    }
}
