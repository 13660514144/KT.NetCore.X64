using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpApi;
using KT.Elevator.Manage.Service.Hubs;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.WebApi.Controllers
{
    /// <summary>
    /// 电梯服务
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ElevatorController : ControllerBase
    {
        private readonly ILogger<ElevatorController> _logger;
        private IHandleElevatorDeviceService _handleElevatorDeviceService;
        private IHubContext<DistributeHub> _distributeHub;

        public ElevatorController(ILogger<ElevatorController> logger,
            IHandleElevatorDeviceService handleElevatorDeviceService,
            IHubContext<DistributeHub> distributeHub)
        {
            _logger = logger;
            _handleElevatorDeviceService = handleElevatorDeviceService;
            _distributeHub = distributeHub;
        }

        /// <summary>
        /// 新增或修改电梯服务
        /// </summary>
        /// <returns>电梯服务信息</returns>
        [HttpPost("handle")]
        public async Task<VoidResponse> HandleAsync(RightHandleElevatorModel model)
        {
            await _handleElevatorDeviceService.RightHandleElevator(model);
            return VoidResponse.Ok();
        }

    }
}
