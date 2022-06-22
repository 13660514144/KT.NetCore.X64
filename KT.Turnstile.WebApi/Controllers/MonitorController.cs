using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KT.Common.Core.Utils;
using KT.Turnstile.Model.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KT.Turnstile.Manage.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MonitorController : ControllerBase
    {
        private readonly ILogger<MonitorController> _logger;

        public MonitorController(ILogger<MonitorController> logger)
        {
            _logger = logger;
        }


    }
}
