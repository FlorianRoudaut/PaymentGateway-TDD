using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PaymentGateway.Domain;
using PaymentGateway.Domain.PaymentValidation;
using PaymentGatewayTests.Setup;

namespace PaymentGatewayTests.Domain
{
    public class PaymentValidationTests
    {
        private DateTime TestDate = new DateTime(2020, 06, 15);

        [SetUp]
        public void Setup()
        {
        }



        [Test]
        public void EmptyPaymentShouldNotBeValid()
        {
            var request = new PaymentRequest();

            var errors = new List<string>();
            var isValid = PaymentValidator.IsPaymentRequestValid(request, TestDate, errors);

            Assert.AreEqual(false, isValid);
            Assert.Contains("Empty-Merchant", errors);
            Assert.Contains("Empty-CardNumber", errors);
            Assert.Contains("Empty-Cvv", errors);
            Assert.Contains("Empty-ExpiryMonth", errors);
            Assert.Contains("Empty-ExpiryYear", errors);
            Assert.Contains("Empty-CardHolderName", errors);
            Assert.Contains("Empty-Amount", errors);
            Assert.Contains("Empty-Currency", errors);
        }



        [Test]
        public void FullPaymentRequestShouldBeValid()
        {
            var request = GlobalTestSetup.InitValidRequest();

            var errors = new List<string>();
            var isValid = PaymentValidator.IsPaymentRequestValid(request, TestDate, errors);

            Assert.AreEqual(true, isValid);
            Assert.AreEqual(0, errors.Count);
        }

        [Test]
        public void ExpiredCardShouldNotBeValid()
        {
            var request = GlobalTestSetup.InitValidRequest();
            request.ExpiryMonth = 01;
            request.ExpiryYear = 20;
            var errors = new List<string>();
            var isValid = PaymentValidator.IsPaymentRequestValid(request, TestDate, errors);

            Assert.AreEqual(false, isValid);
            Assert.Contains("Expired-Card", errors);

            request = GlobalTestSetup.InitValidRequest();
            request.ExpiryMonth = 12;
            request.ExpiryYear = 19;
            errors = new List<string>();
            isValid = PaymentValidator.IsPaymentRequestValid(request, TestDate, errors);

            Assert.AreEqual(false, isValid);
            Assert.Contains("Expired-Card", errors);
        }
    }
}
