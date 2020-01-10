using System;
using System.Collections.Generic;
using System.Text;

namespace PiggyBankApi.Model
{
    public class PiggyPaymentStatus
    {
        public string PaymentId { get; set; }

        public PiggyStatus Status { get; set; }
    }
}
