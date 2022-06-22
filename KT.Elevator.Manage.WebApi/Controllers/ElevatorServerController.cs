using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpApi;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using Microsoft.AspNetCore.Mvc;
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
    public class ElevatorServerController : ControllerBase
    {
        private readonly ILogger<ElevatorServerController> _logger;
        private IElevatorServerService _cardDeviceService;

        public ElevatorServerController(ILogger<ElevatorServerController> logger,
            IElevatorServerService cardDeviceService)
        {
            _logger = logger;
            _cardDeviceService = cardDeviceService;
        }

        /// <summary>
        /// 获取所有电梯服务
        /// </summary>
        /// <returns>电梯服务信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<ElevatorServerModel>>> GetAllAsync()
        {
            var result = await _cardDeviceService.GetAllAsync();
            return DataResponse<List<ElevatorServerModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有电梯服务
        /// </summary>
        /// <returns>电梯服务信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<ElevatorServerModel>> GetByIdAsync(string id)
        {
            var result = await _cardDeviceService.GetByIdAsync(id);
            return DataResponse<ElevatorServerModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改电梯服务
        /// </summary>
        /// <returns>电梯服务信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<ElevatorServerModel>> AddOrEditAsync(ElevatorServerModel model)
        {
            var result = await _cardDeviceService.AddOrEditAsync(model);
            return DataResponse<ElevatorServerModel>.Ok(result);
        }

        /// <summary>
        /// 删除电梯服务
        /// </summary>
        /// <returns>电梯服务信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(ElevatorServerModel model)
        {
            await _cardDeviceService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }
    }
}
