using KT.Common.Core.Utils;
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
    /// 电梯组
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ElevatorGroupController : ControllerBase
    {
        private readonly ILogger<ElevatorGroupController> _logger;
        private IElevatorGroupService _cardDeviceService;
        private ApiSendServer _ApiSendServer;
        public ElevatorGroupController(ILogger<ElevatorGroupController> logger,
            IElevatorGroupService cardDeviceService
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _cardDeviceService = cardDeviceService;
            _ApiSendServer = _apiSendServer;
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
        [HttpGet("allWithFloors")]
        public async Task<DataResponse<List<ElevatorGroupModel>>> GetAllWithFloorsAsync()
        {
            var result = await _cardDeviceService.GetAllWithFloorsAsync();
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
            //进入API队列
            _ApiSendServer._Elevator_ElevatorGroupController.WorkModel = "AddOrEditAsync";
            _ApiSendServer._Elevator_ElevatorGroupController._ElevatorGroupModel = model;
            _ApiSendServer.List_Elevator_ElevatorGroup.Add(_ApiSendServer._Elevator_ElevatorGroupController);
            return DataResponse<ElevatorGroupModel>.Ok(null);
            /*
            var result = await _cardDeviceService.AddOrEditAsync(model);
            return DataResponse<ElevatorGroupModel>.Ok(result);
            */
        }

        /// <summary>
        /// 删除电梯组
        /// </summary>
        /// <returns>电梯组信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(ElevatorGroupModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_ElevatorGroupController.WorkModel = "DeleteAsync";
            _ApiSendServer._Elevator_ElevatorGroupController._ElevatorGroupModel = model;
            _ApiSendServer.List_Elevator_ElevatorGroup.Add(_ApiSendServer._Elevator_ElevatorGroupController);
            return VoidResponse.Ok();
            /*
            await _cardDeviceService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
            */
        }

    }
}
