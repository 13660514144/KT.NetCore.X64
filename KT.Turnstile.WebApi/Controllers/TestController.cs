using KT.Turnstile.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace KT.Turnstile.Manage.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("get")]
        public CardDeviceRightGroupModel GetTest()
        {
            var model = new CardDeviceRightGroupModel();
            model.CardDeviceIds = new List<string>();
            model.CardDeviceIds.Add("1121212");
            model.CardDeviceIds.Add("3454");
            model.CardDeviceIds.Add("57668");
            model.CardDeviceIds.Add("23233");

            return model;
        }


    }
}
