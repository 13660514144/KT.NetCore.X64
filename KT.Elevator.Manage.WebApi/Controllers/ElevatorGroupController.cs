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
    /// 电梯组
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ElevatorGroupController : ControllerBase
    {
        private readonly ILogger<ElevatorGroupController> _logger;
        private IElevatorGroupService _cardDeviceService;

        public ElevatorGroupController(ILogger<ElevatorGroupController> logger,
            IElevatorGroupService cardDeviceService)
        {
            _logger = logger;
            _cardDeviceService = cardDeviceService;
        }

        /// <summary>
        /// 获取所有电梯组
        /// </summary>
        /// <returns>电梯组信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<ElevatorGroupModel>>> GetAllAsync()
        {
            var result = await _cardDeviceService.GetAllAsync();
            return DataResponse<List<ElevatorGroupModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有电梯组
        /// </summary>
        /// <returns>电梯组信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<ElevatorGroupModel>> GetByIdAsync(string id)
        {
            var result = await _cardDeviceService.GetByIdAsync(id);
            return DataResponse<ElevatorGroupModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改电梯组
        /// </summary>
        /// <returns>电梯组信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<ElevatorGroupModel>> AddOrEditAsync(ElevatorGroupModel model)
        {
            var result = await _cardDeviceService.AddOrEditAsync(model);
            return DataResponse<ElevatorGroupModel>.Ok(result);
        }

        /// <summary>
        /// 删除电梯组
        /// </summary>
        /// <returns>电梯组信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(ElevatorGroupModel model)
        {
            await _cardDeviceService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }
    }
}
