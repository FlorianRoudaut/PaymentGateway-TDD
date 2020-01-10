using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Domain.PaymentValidation
{
    public interface IPaymentValidator
    {
        public bool IsPaymentRequestValid(PaymentRequest request, DateTime currentDate,
            List<string> errors);
    }
}
