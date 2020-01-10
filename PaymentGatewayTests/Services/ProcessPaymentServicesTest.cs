using NUnit.Framework;
using PaymentGateway.Services;
using PaymentGatewayTests.Setup;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PaymentGatewayTests.Services
{
    public class ProcessPaymentServicesTests
    {
        ProcessPaymentService PaymentService = GlobalTestSetup.GetPaymentService();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task MerchantShouldNotBeEmpty()
        {
            var request = GlobalTestSetup.InitValidRequest();
            request.MerchantName = "";
            var response = await PaymentService.ProcessPayment(request);

            Assert.AreEqual(true, response.HasGatewayError);
            Assert.AreEqual("Empty-Merchant", response.GatewayErrorMessage);
        }

        [Test]
        public async Task MerchantShouldBeInList()
        {
            var request = GlobalTestSetup.InitValidRequest();
            request.MerchantName = "DoNoExists";
            var response = await PaymentService.ProcessPayment(request);

            Assert.AreEqual(true, response.HasGatewayError);
            Assert.AreEqual("Invalid-Merchant", response.GatewayErrorMessage);
        }

        [Test]
        public async Task PaymentShouldBeSentToPiggyBank()
        {
            var request = GlobalTestSetup.InitValidRequest();
            var response = await PaymentService.ProcessPayment(request);

            Assert.AreEqual("PiggyBank", response.AcquiringBank);
            Assert.IsNotNull(response.AcquiringBankStatus);
            Assert.IsNotEmpty(response.AcquiringBankStatus);
        }

        [Test]
        public async Task OtherAcquirerShouldNotBeFound()
        {
            var request = GlobalTestSetup.InitValidRequest();
            request.MerchantName = "Apple";
            var response = await PaymentService.ProcessPayment(request);

            Assert.AreEqual("OtherAcquirer", response.AcquiringBank);
            Assert.AreEqual(true, response.HasGatewayError);
            Assert.AreEqual("Invalid-AcquiringBank", response.GatewayErrorMessage);
        }
    }
}
