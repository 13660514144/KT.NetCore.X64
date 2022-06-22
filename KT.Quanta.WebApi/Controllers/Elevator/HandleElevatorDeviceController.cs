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
    /// 派梯设备
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HandleElevatorDeviceController : ControllerBase
    {
        private readonly ILogger<HandleElevatorDeviceController> _logger;
        private IHandleElevatorDeviceService _service;
        private IHandleElevatorInputDeviceService _handleElevatorInputDeviceService;
        private ApiSendServer _ApiSendServer;
        public HandleElevatorDeviceController(ILogger<HandleElevatorDeviceController> logger,
            IHandleElevatorDeviceService service,
            IHandleElevatorInputDeviceService handleElevatorInputDeviceService
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _service = service;
            _handleElevatorInputDeviceService = handleElevatorInputDeviceService;
            _ApiSendServer = _apiSendServer;
        }

        /// <summary>
        /// 获取所有派梯设备
        /// </summary>
        /// <returns>派梯设备信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<HandleElevatorDeviceModel>>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return DataResponse<List<HandleElevatorDeviceModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有派梯设备
        /// </summary>
        /// <returns>派梯设备信息</returns>
        [HttpGet("allByElevatorGroupId")]
        public async Task<DataResponse<List<HandleElevatorDeviceModel>>> GetByElevatorGroupIdAsync(string elevatorGroupId)
        {
            var result = await _service.GetByElevatorGroupIdAsync(elevatorGroupId);
            return DataResponse<List<HandleElevatorDeviceModel>>.Ok(result);
        }
        /// <summary>
        /// 获取所有派梯设备
        /// </summary>
        /// <returns>派梯设备信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<HandleElevatorDeviceModel>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return DataResponse<HandleElevatorDeviceModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改派梯设备
        /// </summary>
        /// <returns>派梯设备信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<HandleElevatorDeviceModel>> AddOrEditAsync(HandleElevatorDeviceModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_HandleElevatorDeviceController.WorkModel = "AddOrEditAsync";
            _ApiSendServer._Elevator_HandleElevatorDeviceController._HandleElevatorDeviceModel = model;
            _ApiSendServer.List_Elevator_HandleElevatorDevice.Add(_ApiSendServer._Elevator_HandleElevatorDeviceController);
            return DataResponse<HandleElevatorDeviceModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<HandleElevatorDeviceModel>.Ok(result);
            */
        }

        /// <summary>
        /// 删除派梯设备
        /// </summary>
        /// <returns>派梯设备信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(HandleElevatorDeviceModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_HandleElevatorDeviceController.WorkModel = "DeleteAsync";
            _ApiSendServer._Elevator_HandleElevatorDeviceController._HandleElevatorDeviceModel = model;
            _ApiSendServer.List_Elevator_HandleElevatorDevice.Add(_ApiSendServer._Elevator_HandleElevatorDeviceController);
            return VoidResponse.Ok();
            /*
            await _service.DeleteAsync(model.Id);
            return VoidResponse.Ok();
            */
        }

        /// <summary>
        /// 获取所有派梯设备
        /// </summary>
        /// <returns>派梯设备信息</returns>
        [HttpGet("inputDevices")]
        public async Task<DataResponse<List<HandleElevatorInputDeviceModel>>> GetCardDevicesAsync([FromQuery] string cardType, [FromQuery] string handleElevatorDeviceId)
        {
            var results = await _handleElevatorInputDeviceService.GetStaticAsync(cardType, handleElevatorDeviceId);
            return DataResponse<List<HandleElevatorInputDeviceModel>>.Ok(results);
        }
    }
}
