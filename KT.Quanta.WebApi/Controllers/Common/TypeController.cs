using KT.Common.WebApi.HttpApi;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Common
{
    /// <summary>
    /// 类型信息获取
    /// </summary>
    [ApiController]
    [Route("device")]
    public class TypeController : ControllerBase
    {
        private readonly RemoteDeviceList _remoteDeviceList;
        private readonly ILogger<TypeController> _logger;

        public TypeController(ILogger<TypeController> logger,
            RemoteDeviceList remoteDeviceList)
        {
            _logger = logger;
            _remoteDeviceList = remoteDeviceList;
        }
 
        /// <summary>
        /// 获取所有继电器设备类型
        /// </summary>
        /// <returns>继电器设备类型列表</returns>
        [HttpGet("relayDeviceTypes")]
        public DataResponse<List<DeviceTypeEnum>> GetRelayDeviceTypes()
        {
            var result = DeviceTypeEnum.Items;
            return DataResponse<List<DeviceTypeEnum>>.Ok(result);
        }
    }
}
