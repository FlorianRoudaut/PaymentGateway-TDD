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

            var result = await Controller.Process(request);

            Assert.IsNotNull(result);
            Assert.IsNull(result.GatewayError);
        }

        [Test]
        public async Task ProcessPaymentControllerShouldReturnResultWithGatewayError()
        {
            var request = new PaymentRequest();

            var result = await Controller.Process(request);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.GatewayError);
            Assert.IsNotEmpty(result.GatewayError);
        }
    }
}