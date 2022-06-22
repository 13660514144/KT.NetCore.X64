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
    /// 派梯设备
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HandleElevatorDeviceController : ControllerBase
    {
        private readonly ILogger<HandleElevatorDeviceController> _logger;
        private IHandleElevatorDeviceService _service;
        private IHandleElevatorInputDeviceService _handleElevatorInputDeviceService;

        public HandleElevatorDeviceController(ILogger<HandleElevatorDeviceController> logger,
            IHandleElevatorDeviceService service,
            IHandleElevatorInputDeviceService handleElevatorInputDeviceService)
        {
            _logger = logger;
            _service = service;
            _handleElevatorInputDeviceService = handleElevatorInputDeviceService;
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
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<HandleElevatorDeviceModel>.Ok(result);
        }

        /// <summary>
        /// 删除派梯设备
        /// </summary>
        /// <returns>派梯设备信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(HandleElevatorDeviceModel model)
        {
            await _service.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }

        /// <summary>
        /// 获取所有派梯设备
        /// </summary>
        /// <returns>派梯设备信息</returns>
        [HttpGet("inputDevices")]
        public async Task<DataResponse<List<HandleElevatorInputDeviceModel>>> GetCardDevicesAsync([FromQuery]string cardType, [FromQuery]string handleElevatorDeviceId)
        {
            var results = await _handleElevatorInputDeviceService.GetStaticAsync(cardType, handleElevatorDeviceId);
            return DataResponse<List<HandleElevatorInputDeviceModel>>.Ok(results);
        }
    }
}
