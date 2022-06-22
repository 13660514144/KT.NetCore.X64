using KT.Common.WebApi.HttpApi;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Quanta.WebApi.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Elevator
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
        private ApiSendServer _ApiSendServer;
        public ElevatorInfoController(ILogger<ElevatorInfoController> logger,
            IElevatorInfoService elevatorInfoService
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _elevatorInfoService = elevatorInfoService;
            _ApiSendServer = _apiSendServer;
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
            //进入API队列
            _ApiSendServer._Elevator_ElevatorInfoController.WorkModel = "AddOrEditAsync";
            _ApiSendServer._Elevator_ElevatorInfoController._ElevatorInfoModel = model;
            _ApiSendServer.List_Elevator_ElevatorInfo.Add(_ApiSendServer._Elevator_ElevatorInfoController);
            return DataResponse<ElevatorInfoModel>.Ok(null);
            /*
            var result = await _elevatorInfoService.AddOrEditAsync(model);
            return DataResponse<ElevatorInfoModel>.Ok(result);
            */
        }

        /// <summary>
        /// 新增或修改电梯信息
        /// </summary>
        /// <returns>电梯信息</returns>
        [HttpPost("addOrEditByElevatorGroupId")]
        public async Task<DataResponse<ElevatorGroupModel>> AddOrEditByElevatorGroupId(ElevatorGroupModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_ElevatorInfoController.WorkModel = "AddOrEditByElevatorGroupId";
            _ApiSendServer._Elevator_ElevatorInfoController._ElevatorGroupModel = model;
            _ApiSendServer.List_Elevator_ElevatorInfo.Add(_ApiSendServer._Elevator_ElevatorInfoController);
            return DataResponse<ElevatorGroupModel>.Ok(null);
            /*
            var result = await _elevatorInfoService.AddOrEditByElevatorGroupId(model);
            return DataResponse<ElevatorGroupModel>.Ok(result);
            */
        }
        /// <summary>
        /// 删除电梯信息
        /// </summary>
        /// <returns>电梯信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(ElevatorInfoModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_ElevatorInfoController.WorkModel = "DeleteAsync";
            _ApiSendServer._Elevator_ElevatorInfoController._ElevatorInfoModel = model;
            _ApiSendServer.List_Elevator_ElevatorInfo.Add(_ApiSendServer._Elevator_ElevatorInfoController);
            return VoidResponse.Ok();
            /*
            await _elevatorInfoService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
            */
        }

        /// <summary>
        /// 删除电梯信息
        /// </summary>
        /// <returns>电梯信息</returns>
        [HttpPost("deleteByElevatorGroupId")]
        public async Task<VoidResponse> DeleteByElevatorGroupId(ElevatorGroupModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_ElevatorInfoController.WorkModel = "DeleteByElevatorGroupId";
            _ApiSendServer._Elevator_ElevatorInfoController._ElevatorGroupModel = model;
            _ApiSendServer.List_Elevator_ElevatorInfo.Add(_ApiSendServer._Elevator_ElevatorInfoController);
            return VoidResponse.Ok();
            /*
            await _elevatorInfoService.DeleteByElevatorGroupId(model.Id);
            return VoidResponse.Ok();
            */
        }
    }
}
