using KT.Common.WebApi.HttpApi;
using KT.Quanta.Service.Turnstile.Dtos;
using KT.Quanta.Service.Turnstile.IServices;
using KT.Quanta.WebApi.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Turnstile
{
    /// <summary>
    /// 继电器设备
    /// </summary>
    [ApiController]
    [Route("turnstile/relayDevice")]
    public class TurnstileRelayDeviceController : ControllerBase
    {
        private readonly ILogger<TurnstileRelayDeviceController> _logger;
        private ITurnstileRelayDeviceService _service;
        private ApiSendServer _ApiSendServer;
        public TurnstileRelayDeviceController(ILogger<TurnstileRelayDeviceController> logger,
            ITurnstileRelayDeviceService relayDeviceService
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _service = relayDeviceService;
            _ApiSendServer = _apiSendServer;
        }

        /// <summary>
        /// 获取所有继电器设备
        /// </summary>
        /// <returns>继电器设备信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<TurnstileRelayDeviceModel>>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return DataResponse<List<TurnstileRelayDeviceModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有继电器设备
        /// </summary>
        /// <returns>继电器设备信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<TurnstileRelayDeviceModel>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return DataResponse<TurnstileRelayDeviceModel>.Ok(result);
        }

        /// <summary>
        /// 新增继电器设备
        /// </summary>
        /// <returns>继电器设备信息</returns>
        [HttpPost("add")]
        public async Task<DataResponse<TurnstileRelayDeviceModel>> AddAsync(TurnstileRelayDeviceModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileRelayDevice.WorkModel = "AddAsync";
            _ApiSendServer._TurnstileRelayDevice._TurnstileRelayDeviceModel = model;
            _ApiSendServer.List__TurnstileRelayDevice.Add(_ApiSendServer._TurnstileRelayDevice);
            return DataResponse<TurnstileRelayDeviceModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<TurnstileRelayDeviceModel>.Ok(result);
            */
        }

        /// <summary>
        /// 修改继电器设备
        /// </summary>
        /// <returns>继电器设备信息</returns>
        [HttpPost("edit")]
        public async Task<DataResponse<TurnstileRelayDeviceModel>> EditAsync(TurnstileRelayDeviceModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileRelayDevice.WorkModel = "EditAsync";
            _ApiSendServer._TurnstileRelayDevice._TurnstileRelayDeviceModel = model;
            _ApiSendServer.List__TurnstileRelayDevice.Add(_ApiSendServer._TurnstileRelayDevice);
            return DataResponse<TurnstileRelayDeviceModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<TurnstileRelayDeviceModel>.Ok(result);
            */
        }

        /// <summary>
        /// 新增或修改继电器设备
        /// </summary>
        /// <returns>继电器设备信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<TurnstileRelayDeviceModel>> AddOrEditAsync(TurnstileRelayDeviceModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileRelayDevice.WorkModel = "AddOrEditAsync";
            _ApiSendServer._TurnstileRelayDevice._TurnstileRelayDeviceModel = model;
            _ApiSendServer.List__TurnstileRelayDevice.Add(_ApiSendServer._TurnstileRelayDevice);
            return DataResponse<TurnstileRelayDeviceModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<TurnstileRelayDeviceModel>.Ok(result);
            */
        }

        /// <summary>
        /// 删除继电器设备
        /// </summary>
        /// <returns>继电器设备信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(TurnstileRelayDeviceModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileRelayDevice.WorkModel = "DeleteAsync";
            _ApiSendServer._TurnstileRelayDevice._TurnstileRelayDeviceModel = model;
            _ApiSendServer.List__TurnstileRelayDevice.Add(_ApiSendServer._TurnstileRelayDevice);
            return VoidResponse.Ok();
            /*
            await _service.DeleteAsync(model.Id);
            return VoidResponse.Ok();
            */
        }
    }
}
