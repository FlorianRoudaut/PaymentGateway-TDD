using PaymentGateway.AcquiringBanks;
using PaymentGateway.Domain;
using PaymentGateway.Domain.PaymentValidation;
using PaymentGateway.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Services
{
    public class ProcessPaymentService : IProcessPaymentService
    {
        private IPaymentValidator _paymentValidator;
        private IMerchantRepository _merchantRepository;

        public ProcessPaymentService(IPaymentValidator validator, IMerchantRepository merchantRepository)
        {
            _paymentValidator = validator;
            _merchantRepository = merchantRepository;
        }

        public async Task<PaymentResult> ProcessPayment(PaymentRequest paymentRequest)
        {
            var result = new PaymentResult();
            result.GatewayPaymentId = Guid.NewGuid();

            var errors = new List<string>();
            var time = DateTime.UtcNow;
            var isValid = _paymentValidator.IsPaymentRequestValid(paymentRequest, time, errors);
            if (!isValid)
            {
                result.HasGatewayError = true;
                result.GatewayErrorMessage = string.Join('|', errors);
                return result;
            }

            var merchantName = paymentRequest.MerchantName;
            if (string.IsNullOrEmpty(merchantName))
            {
                result.HasGatewayError = true;
                result.GatewayErrorMessage = "Empty-Merchant";
                return result;
            }

            var merchants = _merchantRepository.LoadAll();
            Merchant merchant = null;
            foreach (var savedMerchant in merchants)
            {
                if (merchantName.Equals(savedMerchant.Name))
                {
                    merchant = savedMerchant;
                    break;
                }
            }

            if (merchant == null)
            {
                result.HasGatewayError = true;
                result.GatewayErrorMessage = "Invalid-Merchant";
                return result;
            }

            result.AcquiringBank = merchant.AcquiringBank;
            var connector = AcquiringBankConnectorFactory.GetConnector(merchant.AcquiringBank);
            if(connector==null)
            {
                result.HasGatewayError = true;
                result.GatewayErrorMessage = "Invalid-AcquiringBank";
                return result;
            }

            var acquiringBankResult = await connector.SendToAcquiringBank(paymentRequest);
            result.AcquiringBankStatus = acquiringBankResult.Status;
            result.AcquiringBankPaymentId = acquiringBankResult.PaymentId;
            result.ProcessedTime = DateTime.UtcNow;

            return result;
        }
    }
}
