using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using PaymentGateway.Domain;
using PaymentGateway.Domain.PaymentValidation;
using PaymentGateway.Repositories;
using PaymentGatewayTests.Setup;

namespace PaymentGatewayTests.Domain
{
    public class PaymentValidationTests
    {
        private DateTime TestDate = new DateTime(2020, 06, 15);

        PaymentValidator PaymentValidator = GlobalTestSetup.GetPaymentValidator();

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

        [Test]
        public void CardNumberShouldHaveSixteenDigits()
        {
            var request = GlobalTestSetup.InitValidRequest();
            request.CardNumber = "1234 567 8910 1";
            var errors = new List<string>();
            var isValid = PaymentValidator.IsPaymentRequestValid(request, TestDate, errors);

            Assert.AreEqual(false, isValid);
            Assert.Contains("Invalid-CardNumber", errors);

            request = GlobalTestSetup.InitValidRequest();
            request.CardNumber = "abcd 5679 8910 1234";
            errors = new List<string>();
            isValid = PaymentValidator.IsPaymentRequestValid(request, TestDate, errors);

            Assert.AreEqual(false, isValid);
            Assert.Contains("Invalid-CardNumber", errors);
        }

        [Test]
        public void CvvShouldHaveThreeOrFourDigits()
        {
            //https://en.wikipedia.org/wiki/Card_security_code
            var request = GlobalTestSetup.InitValidRequest();
            request.Cvv = "1234";
            var errors = new List<string>();
            var isValid = PaymentValidator.IsPaymentRequestValid(request, TestDate, errors);
            Assert.AreEqual(true, isValid);

            request = GlobalTestSetup.InitValidRequest();
            request.Cvv = "12345";
            errors = new List<string>();
            isValid = PaymentValidator.IsPaymentRequestValid(request, TestDate, errors);
            Assert.AreEqual(false, isValid);
            Assert.Contains("Invalid-Cvv", errors);

            request = GlobalTestSetup.InitValidRequest();
            request.Cvv = "12";
            errors = new List<string>();
            isValid = PaymentValidator.IsPaymentRequestValid(request, TestDate, errors);
            Assert.AreEqual(false, isValid);
            Assert.Contains("Invalid-Cvv", errors);

            request = GlobalTestSetup.InitValidRequest();
            request.Cvv = "abc";
            errors = new List<string>();
            isValid = PaymentValidator.IsPaymentRequestValid(request, TestDate, errors);
            Assert.AreEqual(false, isValid);
            Assert.Contains("Invalid-Cvv", errors);
        }

        [Test]
        public void CurrencyShouldBeInList()
        {
            var request = GlobalTestSetup.InitValidRequest();
            request.Currency = "CHF";
            var errors = new List<string>();
            var isValid = PaymentValidator.IsPaymentRequestValid(request, TestDate, errors);
            Assert.AreEqual(false, isValid);
            Assert.Contains("Invalid-Currency", errors);
        }

        [Test]
        public void AmountShouldBeStrictlyPositive()
        {
            var request = GlobalTestSetup.InitValidRequest();
            request.Amount = -2.0;
            var errors = new List<string>();
            var isValid = PaymentValidator.IsPaymentRequestValid(request, TestDate, errors);
            Assert.AreEqual(false, isValid);
            Assert.Contains("Invalid-Amount", errors);
        }
    }
}
