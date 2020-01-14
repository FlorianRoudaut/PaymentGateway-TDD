using NUnit.Framework;
using PaymentGateway.Controllers;
using PaymentGateway.Domain;
using PaymentGatewayTests.Setup;
using System.Threading.Tasks;

namespace PaymentGatewayTests.Controllers
{
    public class ProcessPaymentControllerTests
    {
        ProcessPaymentController Controller = GlobalTestSetup.GetProcessPaymentController();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ProcessPaymentControllerShouldReturnResult()
        {
            
            var request = GlobalTestSetup.InitValidRequest();
            var histories = await Controller.GetHistory(request.MerchantName);
            var oldCount = histories.Count;

            var result = await Controller.Process(request);

            Assert.IsNotNull(result);
            Assert.AreEqual(false, result.HasGatewayError);
            Assert.IsNull(result.GatewayErrorMessage);

            Assert.AreEqual("PiggyBank", result.AcquiringBank);
            Assert.IsNotNull(result.AcquiringBankStatus);
            Assert.IsNotEmpty(result.AcquiringBankStatus);
            Assert.IsNotNull(result.AcquiringBankPaymentId);
            Assert.IsNotEmpty(result.AcquiringBankPaymentId);

            histories = await Controller.GetHistory(request.MerchantName);
            var newCount = histories.Count;
            Assert.AreEqual(oldCount+1, newCount);

            var insertedPayment = await Controller.GetPaymentHistory(result.GatewayPaymentId);
            Assert.IsNotNull(insertedPayment);

            Assert.AreNotEqual(request.CardNumber, insertedPayment.CardNumber);
            Assert.AreEqual(19, insertedPayment.CardNumber.Length);
        }


        [Test]
        public async Task ProcessPaymentControllerShouldReturnResultWithGatewayError()
        {
            var request = new PaymentRequest();

            var result = await Controller.Process(request);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.HasGatewayError);
            Assert.IsNotNull(result.GatewayErrorMessage);
            Assert.IsNotEmpty(result.GatewayErrorMessage);

            var insertedPayment = await Controller.GetPaymentHistory(result.GatewayPaymentId);
            Assert.IsNotNull(insertedPayment);
        }

        [Test]
        public async Task PaymentHistoryShouldBeNullIfPaymentIdIsNotValid()
        {
            var insertedPayment = await Controller.GetPaymentHistory(new System.Guid());
            Assert.IsNull(insertedPayment);
        }
    }
}