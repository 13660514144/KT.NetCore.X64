using KT.Common.Event;
using KT.Common.WebApi.HttpApi;
using KT.Device.Quanta.Events;
using KT.Quanta.Common.Enums;
using KT.Quanta.Common.Models;
using KT.Quanta.Service.Devices.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Common
{
    /// <summary>
    /// 显示
    /// </summary>
    [ApiController]
    [Route("display")]
    public class DisplayController : ControllerBase
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly ILogger<DisplayController> _logger;
        private readonly RemoteDeviceList _remoteDeviceList;

        public DisplayController(ILogger<DisplayController> logger,
            IEventAggregator eventAggregator,
            RemoteDeviceList remoteDeviceList)
        {
            _logger = logger;
            _eventAggregator = eventAggregator;
            _remoteDeviceList = remoteDeviceList;
        }

        /// <summary>
        /// 通行显示
        /// </summary> 
        [HttpPost("pass")]
        public async Task<VoidResponse> PassAsync(PassDisplayModel model)
        {
            var handleElevatorDevice = await _remoteDeviceList.GetByIdAsync(model.HandleElevatorDeviceId);
            if (handleElevatorDevice == null)
            {
                throw new Exception($"找不到派梯设备！");
            }

            model.DisplayDeivceId = handleElevatorDevice.RemoteDeviceInfo.ExtensionId;
            if (string.IsNullOrEmpty(model.DisplayDeivceId))
            {
                throw new Exception($"找不到派梯设备下的显示设备Id！");
            }

            _eventAggregator.GetEvent<PassDisplayEvent>().Publish(model);

            return VoidResponse.Ok();
        }

        /// <summary>
        /// 通行显示
        /// </summary> 
        [HttpPost("handleElevator")]
        public VoidResponse HandleElevatorAsync(HandleElevatorDisplayModel model)
        {
            _eventAggregator.GetEvent<HandleElevatorDisplayEvent>().Publish(model);

            return VoidResponse.Ok();
        }
    }
}
