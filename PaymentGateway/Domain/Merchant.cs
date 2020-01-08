using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentGateway.Domain
{
    //The seller of the product. For example, Apple or Amazon.
    public class Merchant
    {
        public Merchant() { }

        public Merchant(string name, string acquiringBank)
        {
            Name = name;
            AcquiringBank = acquiringBank;
        }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string AcquiringBank { get; set; }
    }
}
