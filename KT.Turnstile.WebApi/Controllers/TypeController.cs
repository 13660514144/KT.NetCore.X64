using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KT.Common.WebApi.HttpApi;
using KT.Common.Data.Models;
using KT.Turnstile.Model.Models;
using KT.Turnstile.Common.Enums;
using KT.Turnstile.Manage.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KT.Turnstile.Manage.WebApi.Controllers
{
    /// <summary>
    /// 类型信息获取
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TypeController : ControllerBase
    {
        private readonly ILogger<TypeController> _logger;

        public TypeController(ILogger<TypeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取所有读卡器设备类型
        /// </summary>
        /// <returns>读卡器设备类型列表</returns>
        [HttpGet("cardDeviceTypes")]
        public DataResponse<List<RelayDeviceTypeEnum>> GetCardDeviceTypes()
        {
            var result = RelayDeviceTypeEnum.Items;
            return DataResponse<List<RelayDeviceTypeEnum>>.Ok(result);
        }

        /// <summary>
        /// 获取所有继电器设备类型
        /// </summary>
        /// <returns>继电器设备类型列表</returns>
        [HttpGet("relayDeviceTypes")]
        public DataResponse<List<CardDeviceTypeEnum>> GetRelayDeviceTypes()
        {
            var result = CardDeviceTypeEnum.Items;
            return DataResponse<List<CardDeviceTypeEnum>>.Ok(result);
        }

        /// <summary>
        /// 获取所有继电器设备通信类型
        /// </summary>
        /// <returns>继电器设备通信类型列表</returns>
        [HttpGet("relayCommunicateTypes")]
        public DataResponse<List<RelayCommunicateTypeEnum>> GetRelayCommunicateTypes()
        {
            var result = RelayCommunicateTypeEnum.Items;
            return DataResponse<List<RelayCommunicateTypeEnum>>.Ok(result);
        }
    }
}
