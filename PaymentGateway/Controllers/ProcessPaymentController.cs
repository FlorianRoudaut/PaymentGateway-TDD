using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentGateway.Domain;
using PaymentGateway.Services;

namespace PaymentGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProcessPaymentController : ControllerBase
    {
        public ProcessPaymentController()
        {
        }

        [HttpPost("")]
        public async Task<PaymentResult> Process([FromBody]PaymentRequest request)
        {
            var task = Task.Run(() => { });
            await task;

            return await ProcessPaymentService.ProcessPayment(request);
        }
    }
}
