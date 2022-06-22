using KT.Common.WebApi.HttpApi;
using KT.Quanta.Service.Turnstile.Dtos;
using KT.Quanta.Service.Turnstile.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HelperTools;
using RestSharp;
using KT.Quanta.WebApi.Common.Helper;

namespace KT.Quanta.WebApi.Controllers.Turnstile
{
    /// <summary>
    /// 读卡器设备
    /// </summary>
    [ApiController]
    [Route("turnstile/cardDevice")]
    public class TurnstileCardDeviceController : ControllerBase
    {
        private readonly ILogger<TurnstileCardDeviceController> _logger;
        private ITurnstileCardDeviceService _service;
        private ApiSendServer _ApiSendServer;
        public TurnstileCardDeviceController(ILogger<TurnstileCardDeviceController> logger,
            ITurnstileCardDeviceService cardDeviceService
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _service = cardDeviceService;
            _ApiSendServer = _apiSendServer;
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<TurnstileCardDeviceModel>>> GetAllAsync()
        {
            var result = await _service.GetFromDeviceTypeAsync();
            return DataResponse<List<TurnstileCardDeviceModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<TurnstileCardDeviceModel>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return DataResponse<TurnstileCardDeviceModel>.Ok(result);
        }

        /// <summary>
        /// 新增读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("add")]
        public async Task<DataResponse<TurnstileCardDeviceModel>> AddAsync(TurnstileCardDeviceModel model)
        {
            _logger.LogInformation($"TurnstileCardDeviceModel={JsonConvert.SerializeObject(model)}");
            //进入API队列
            _ApiSendServer._TurnstileCardDevice.WorkModel = "AddAsync";
            _ApiSendServer._TurnstileCardDevice._TurnstileCardDeviceModel = model;
            _ApiSendServer.List_TurnstileCardDevice.Add(_ApiSendServer._TurnstileCardDevice);
            return DataResponse<TurnstileCardDeviceModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<TurnstileCardDeviceModel>.Ok(result);
            */
        }

        /// <summary>
        /// 修改读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("edit")]
        public async Task<DataResponse<TurnstileCardDeviceModel>> EditAsync(TurnstileCardDeviceModel model)
        {
            _logger.LogInformation($"TurnstileCardDeviceModel={JsonConvert.SerializeObject(model)}");
            //进入API队列
            _ApiSendServer._TurnstileCardDevice.WorkModel = "EditAsync";
            _ApiSendServer._TurnstileCardDevice._TurnstileCardDeviceModel = model;
            _ApiSendServer.List_TurnstileCardDevice.Add(_ApiSendServer._TurnstileCardDevice);
            return DataResponse<TurnstileCardDeviceModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<TurnstileCardDeviceModel>.Ok(result);
            */
        }

        /// <summary>
        /// 新增或修改读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<TurnstileCardDeviceModel>> AddOrEditAsync(TurnstileCardDeviceModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileCardDevice.WorkModel = "AddOrEditAsync";
            _ApiSendServer._TurnstileCardDevice._TurnstileCardDeviceModel = model;
            _ApiSendServer.List_TurnstileCardDevice.Add(_ApiSendServer._TurnstileCardDevice);
            return DataResponse<TurnstileCardDeviceModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<TurnstileCardDeviceModel>.Ok(result);
            */
        }

        /// <summary>
        /// 删除读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(TurnstileCardDeviceModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileCardDevice.WorkModel = "DeleteAsync";
            _ApiSendServer._TurnstileCardDevice._TurnstileCardDeviceModel = model;
            _ApiSendServer.List_TurnstileCardDevice.Add(_ApiSendServer._TurnstileCardDevice);
            return VoidResponse.Ok();
            /*
            await _service.DeleteAsync(model.Id);
            return VoidResponse.Ok();
            */
        }
    }
}
