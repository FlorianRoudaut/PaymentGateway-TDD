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

        public async Task Save(PaymentHistory history)
        {
            InMemoryList.Add(history);
        }
    }
}
