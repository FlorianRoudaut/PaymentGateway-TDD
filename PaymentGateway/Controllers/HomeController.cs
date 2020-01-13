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
    [Route("")]
    public class HomeController : ControllerBase
    {

        public HomeController()
        {
        }

        [HttpGet("Home")]
        public string GetHistory(string merchantName)
        {
            return "Hello this is home";
        }
    }
}