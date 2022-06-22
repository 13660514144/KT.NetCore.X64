using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpApi;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Quanta.WebApi.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Elevator
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
        private ApiSendServer _ApiSendServer;
        public ElevatorServerController(ILogger<ElevatorServerController> logger,
            IElevatorServerService cardDeviceService
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _cardDeviceService = cardDeviceService;
            _ApiSendServer = _apiSendServer;
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
            //进入API队列
            _ApiSendServer._Elevator_ElevatorServerController.WorkModel = "AddOrEditAsync";
            _ApiSendServer._Elevator_ElevatorServerController._ElevatorServerModel = model;
            _ApiSendServer.List_Elevator_ElevatorServer.Add(_ApiSendServer._Elevator_ElevatorServerController);
            return DataResponse<ElevatorServerModel>.Ok(null);
            /*
            var result = await _cardDeviceService.AddOrEditAsync(model);
            return DataResponse<ElevatorServerModel>.Ok(result);
            */
        }

        /// <summary>
        /// 删除电梯服务
        /// </summary>
        /// <returns>电梯服务信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(ElevatorServerModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_ElevatorServerController.WorkModel = "DeleteAsync";
            _ApiSendServer._Elevator_ElevatorServerController._ElevatorServerModel = model;
            _ApiSendServer.List_Elevator_ElevatorServer.Add(_ApiSendServer._Elevator_ElevatorServerController);
            return VoidResponse.Ok();
            /*
            await _cardDeviceService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
            */
        }
    }
}
