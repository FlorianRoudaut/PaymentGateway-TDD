using PaymentGateway.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Repositories
{
    public class HardcodedPaymentHistoryRepository : IPaymentHistoryRepository
    {
        private static List<PaymentHistory> InMemoryList = new List<PaymentHistory>();

        public HardcodedPaymentHistoryRepository()
        {
        }

        public async Task<List<PaymentHistory>> LoadAll(string merchantName)
        {
            var newList = new List<PaymentHistory>();
            if (string.IsNullOrEmpty(merchantName)) return newList;

            foreach(var history in InMemoryList)
            {
                if(merchantName.Equals(history.MerchantName))
                {
                    newList.Add(history.ShallowCopy());
                }
            }
            return newList;
        }

        public async Task<PaymentHistory> GetById(Guid paymentId)
        {
            if (paymentId == null) return null;

            foreach (var history in InMemoryList)
            {
                if (paymentId.Equals(history.GatewayPaymentId))
                {
                    return history;
                }
            }
            return null;
        }

        public async Task Save(PaymentHistory history)
        {
            InMemoryList.Add(history);
        }
    }
}
