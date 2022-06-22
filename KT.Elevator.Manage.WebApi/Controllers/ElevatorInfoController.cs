using KT.Common.WebApi.HttpApi;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.WebApi.Controllers
{
    /// <summary>
    /// 人员信息表
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ElevatorInfoController : ControllerBase
    {
        private readonly ILogger<ElevatorInfoController> _logger;
        private IElevatorInfoService _elevatorInfoService;

        public ElevatorInfoController(ILogger<ElevatorInfoController> logger,
            IElevatorInfoService elevatorInfoService)
        {
            _logger = logger;
            _elevatorInfoService = elevatorInfoService;
        }
        /// <summary>
        /// 获取所有电梯信息
        /// </summary>
        /// <returns>电梯信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<ElevatorInfoModel>>> GetAllAsync()
        {
            var result = await _elevatorInfoService.GetAllAsync();
            return DataResponse<List<ElevatorInfoModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有电梯信息
        /// </summary>
        /// <returns>电梯信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<ElevatorInfoModel>> GetByIdAsync(string id)
        {
            var result = await _elevatorInfoService.GetByIdAsync(id);
            return DataResponse<ElevatorInfoModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改电梯信息
        /// </summary>
        /// <returns>电梯信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<ElevatorInfoModel>> AddOrEditAsync(ElevatorInfoModel model)
        {
            var result = await _elevatorInfoService.AddOrEditAsync(model);
            return DataResponse<ElevatorInfoModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改电梯信息
        /// </summary>
        /// <returns>电梯信息</returns>
        [HttpPost("addOrEditByElevatorGroupId")]
        public async Task<DataResponse<ElevatorGroupModel>> AddOrEditByElevatorGroupId(ElevatorGroupModel model)
        {
            var result = await _elevatorInfoService.AddOrEditByElevatorGroupId(model);
            return DataResponse<ElevatorGroupModel>.Ok(result);
        }
        /// <summary>
        /// 删除电梯信息
        /// </summary>
        /// <returns>电梯信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(ElevatorInfoModel model)
        {
            await _elevatorInfoService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }

        /// <summary>
        /// 删除电梯信息
        /// </summary>
        /// <returns>电梯信息</returns>
        [HttpPost("deleteByElevatorGroupId")]
        public async Task<VoidResponse> DeleteByElevatorGroupId(ElevatorGroupModel model)
        {
            await _elevatorInfoService.DeleteByElevatorGroupId(model.Id);
            return VoidResponse.Ok();
        }
    }
}
