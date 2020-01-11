using PiggyBankApi.Model;
using System;
using System.Threading.Tasks;

namespace PiggyBankApi.Functions
{
    public static class ApiFunctions
    {
        public async static Task<PiggyPaymentStatus> ProcessPaymentRequestAsync(PiggyPaymentRequest payment)
        {
            var task = Task.Run(() => ProcessPaymentRequest(payment));
            return await task;
        }

        public static PiggyPaymentStatus ProcessPaymentRequest(PiggyPaymentRequest payment)
        {
            var firstDigit = payment.CardNumber[0];
            var failed = false;
            if (firstDigit == '0') failed = true;

            if (failed)
            {
                var status = new PiggyPaymentStatus();
                status.Status = PiggyStatus.Failed;
                return status;
            }
            else
            {
                var status = new PiggyPaymentStatus();
                status.PaymentId = Guid.NewGuid().ToString();
                status.Status = PiggyStatus.Authorised;
                return status;
            }
        }
    }
}
