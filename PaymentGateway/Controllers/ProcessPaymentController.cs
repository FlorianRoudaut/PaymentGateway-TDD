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
        private IProcessPaymentService _paymentService;
        private IPaymentHistoryService _historyService;

        public ProcessPaymentController(IProcessPaymentService paymentService, 
            IPaymentHistoryService historyService)
        {
            _paymentService = paymentService;
            _historyService = historyService;
        }

        [HttpGet("GetHistory")]
        public async Task<List<PaymentHistory>> GetHistory(string merchantName)
        {
            var histories = await _historyService.GetHistories(merchantName);
            return histories;
        }

        [HttpGet("GetPayment")]
        public async Task<PaymentHistory> GetPaymentHistory(Guid paymentId)
        {
            var history = await _historyService.GetPaymentHistory(paymentId);
            return history;
        }

        [HttpPost("")]
        public async Task<PaymentResult> Process([FromBody]PaymentRequest request)
        {
            var result = await _paymentService.ProcessPayment(request);

            await _historyService.SaveHistory(request, result);

            return result;
        }
    }
}
