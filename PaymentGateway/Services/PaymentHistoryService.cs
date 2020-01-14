using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentGateway.Domain;
using PaymentGateway.Repositories;

namespace PaymentGateway.Services
{
    public class PaymentHistoryService : IPaymentHistoryService
    {
        private IPaymentHistoryRepository _historyRepository;

        public PaymentHistoryService(IPaymentHistoryRepository historyRepository)
        {
            _historyRepository = historyRepository;
        }

        public async Task<List<PaymentHistory>> GetHistories(string merchantName)
        {
            return await _historyRepository.LoadAll(merchantName);
        }

        public async Task<PaymentHistory> GetPaymentHistory(Guid paymentId)
        {
            return await _historyRepository.GetById(paymentId);
        }

        public async Task SaveHistory(PaymentRequest request, PaymentResult result)
        {
            var history = BuildHistory(request, result);
            await _historyRepository.Save(history);
        }

        private PaymentHistory BuildHistory(PaymentRequest request, PaymentResult result)
        {
            var history = new PaymentHistory();
            history.GatewayPaymentId = result.GatewayPaymentId;
            history.CreatedAt = DateTime.UtcNow;
            history.MerchantName = request.MerchantName;
            history.CardNumber = MaskCardNumber(request.CardNumber);
            history.Cvv = request.Cvv;
            history.ExpiryMonth = request.ExpiryMonth;
            history.ExpiryYear = request.ExpiryYear;
            history.CardHolderName = request.CardHolderName;
            history.Amount = request.Amount;
            history.Currency = request.Currency;

            history.HasGatewayError = result.HasGatewayError;
            history.GatewayErrorMessage = result.GatewayErrorMessage;
            history.AcquiringBank = result.AcquiringBank;
            history.AcquiringBankStatus = result.AcquiringBankStatus;
            history.AcquiringBankPaymentId = result.AcquiringBankPaymentId;
            history.ProcessedTime = result.ProcessedTime;

            return history;
        }

        private string MaskCardNumber(string cardNumber)
        {
            if (string.IsNullOrEmpty(cardNumber)) return "XXXX-XXXX-XXXX-XXXX";

            var length = cardNumber.Length;
            if (length > 4)
            {
                return "XXXX-XXXX-XXXX-"+cardNumber.Substring(length-4,4);
            }
            else
            {
                return "XXXX-XXXX-XXXX-XXXX";
            }
        }
    }
}
