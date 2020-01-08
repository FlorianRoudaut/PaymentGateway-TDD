using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Domain.PaymentValidation
{
    public static class PaymentValidator
    {
        public static bool IsPaymentRequestValid(PaymentRequest request, DateTime currentDate,
            List<string> errors)
        {
            var isValid = true;
            if (string.IsNullOrEmpty(request.MerchantName))
            {
                isValid = false;
                errors.Add("Empty-Merchant");
            }

            if (string.IsNullOrEmpty(request.CardNumber))
            {
                isValid = false;
                errors.Add("Empty-CardNumber");
            }

            if (string.IsNullOrEmpty(request.Cvv))
            {
                isValid = false;
                errors.Add("Empty-Cvv");
            }

            if (request.ExpiryMonth == 0)
            {
                isValid = false;
                errors.Add("Empty-ExpiryMonth");
            }

            if (request.ExpiryYear == 0)
            {
                isValid = false;
                errors.Add("Empty-ExpiryYear");
            }

            if (string.IsNullOrEmpty(request.CardHolderName))
            {
                isValid = false;
                errors.Add("Empty-CardHolderName");
            }

            if (Math.Abs(request.Amount) < double.Epsilon)
            {
                isValid = false;
                errors.Add("Empty-Amount");
            }

            if (string.IsNullOrEmpty(request.Currency))
            {
                isValid = false;
                errors.Add("Empty-Currency");
            }

            isValid &= IsCardNotExpired(request, currentDate, errors);


            return isValid;
        }

        private static bool IsCardNotExpired(PaymentRequest request, DateTime currentDate,
            List<string> errors)
        {
            var isValid = true;

            if (request.ExpiryYear < currentDate.Year)
            {
                isValid = false;
                errors.Add("Expired-Card");
            }
            else if (request.ExpiryYear == currentDate.Year)
            {
                if (request.ExpiryMonth < currentDate.Month)
                {
                    isValid = false;
                    errors.Add("Expired-Card");
                }
            }
            return isValid;
        }
    }
}
