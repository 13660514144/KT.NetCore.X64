using KT.Quanta.Service.Devices.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KT.Quanta.WebApi.Controllers.GuestMothed
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly ILogger<EquipmentController> _logger;
        private IServiceProvider _serviceProvider;
        private RemoteDeviceList _remoteDeviceList;
        private readonly ICommunicateDeviceFactory _communicateDeviceFactory;
        public EquipmentController(RemoteDeviceList remoteDeviceList, ILogger<EquipmentController> logger)
        {
            _remoteDeviceList = remoteDeviceList;
            _logger = logger;
        }
        [HttpGet("all")]
        public async Task<string> GetAllAsync(string id)
        {
           
            return string.Empty;
        }
    }
}
