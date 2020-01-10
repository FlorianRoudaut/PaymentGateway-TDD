using PaymentGateway.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PaymentGateway.Domain.PaymentValidation
{
    public class PaymentValidator : IPaymentValidator
    {
        private ICurrencyRepository _currencyRepository { get; set; }

        public PaymentValidator(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public bool IsPaymentRequestValid(PaymentRequest request, DateTime currentDate,
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
            else
            {
                isValid &= CheckCardDigits(request, errors);
            }

            if (string.IsNullOrEmpty(request.Cvv))
            {
                isValid = false;
                errors.Add("Empty-Cvv");
            }
            else
            {
                isValid &= CheckCvvDigits(request, errors);
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
            isValid &= IsCardNotExpired(request, currentDate, errors);

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
            else
            {
                isValid &= CheckAmount(request, errors);
            }

            if (string.IsNullOrEmpty(request.Currency))
            {
                isValid = false;
                errors.Add("Empty-Currency");
            }
            else
            {
                isValid &= CheckCurrency(request, errors);
            }

            return isValid;
        }

        private bool IsCardNotExpired(PaymentRequest request, DateTime currentDate,
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


        private bool CheckCardDigits(PaymentRequest request,
            List<string> errors)
        {
            //Card number should be exactly 16 digits
            //Spaces are allowed but not lettters
            var cardNumber = request.CardNumber;
            cardNumber = cardNumber.Replace(" ", "");
            if (cardNumber.Length != 16)
            {
                errors.Add("Invalid-CardNumber");
                return false;
            }

            var match = Regex.Match(cardNumber, @"^[0-9-]*$");
            if (!match.Success)
            {
                errors.Add("Invalid-CardNumber");
                return false;
            }

            return true;
        }

        private bool CheckCvvDigits(PaymentRequest request, List<string> errors)
        {
            //Card number should be exactly 16 digits
            //Spaces are allowed but not lettters
            var cvv = request.Cvv;
            cvv = cvv.Replace(" ", "");
            var length = cvv.Length;
            if (length != 3 && length != 4)
            {
                errors.Add("Invalid-Cvv");
                return false;
            }

            var match = Regex.Match(cvv, @"^[0-9-]*$");
            if (!match.Success)
            {
                errors.Add("Invalid-Cvv");
                return false;
            }

            return true;
        }

        private bool CheckCurrency(PaymentRequest request, List<string> errors)
        {
            //Currency should be in a list saved in the system
            var currency = request.Currency;
            var allowedCurrencies = _currencyRepository.LoadAll();
            if (!allowedCurrencies.Any(t => currency.Equals(t.Code)))
            {
                errors.Add("Invalid-Currency");
                return false;
            }
            return true;
        }

        private bool CheckAmount(PaymentRequest request, List<string> errors)
        {
            //Currency should be in a list saved in the system
            var amount = request.Amount;
            if (amount < 0)
            {
                errors.Add("Invalid-Amount");
                return false;
            }
            return true;
        }
    }
}
