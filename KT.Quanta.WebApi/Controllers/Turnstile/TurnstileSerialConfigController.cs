using KT.Common.WebApi.HttpApi;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Services;
using KT.Quanta.WebApi.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Turnstile
{
    /// <summary>
    /// 串口配置
    /// </summary>
    [ApiController]
    [Route("turnstile/serialConfig")]
    public class TurnstileSerialConfigController : ControllerBase
    {
        private readonly ILogger<TurnstileSerialConfigController> _logger;
        private ISerialConfigService _service;
        private SerialConfigModel _serialConfig;
        private ApiSendServer _ApiSendServer;
        public TurnstileSerialConfigController(ILogger<TurnstileSerialConfigController> logger,
            ISerialConfigService serialConfigService,
            IOptions<SerialConfigModel> serialConfig
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _service = serialConfigService;
            _serialConfig = serialConfig.Value;
            _ApiSendServer = _apiSendServer;
        }

        /// <summary>
        /// 获取所有串口配置
        /// </summary>
        /// <returns>串口配置信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<SerialConfigModel>>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return DataResponse<List<SerialConfigModel>>.Ok(result);
        }

        /// <summary>
        /// 获取串口配置详情
        /// </summary>
        /// <returns>串口配置信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<SerialConfigModel>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return DataResponse<SerialConfigModel>.Ok(result);
        }

        /// <summary>
        /// 新增串口配置
        /// </summary>
        /// <returns>串口配置信息</returns>
        [HttpPost("add")]
        public async Task<DataResponse<SerialConfigModel>> AddAsync(SerialConfigModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileSerialConfig.WorkModel = "AddAsync";
            _ApiSendServer._TurnstileSerialConfig._SerialConfigModel = model;
            _ApiSendServer.List__TurnstileSerialConfig.Add(_ApiSendServer._TurnstileSerialConfig);
            return DataResponse<SerialConfigModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<SerialConfigModel>.Ok(result);
            */
        }

        /// <summary>
        /// 修串口配置
        /// </summary>
        /// <returns>串口配置信息</returns>
        [HttpPost("edit")]
        public async Task<DataResponse<SerialConfigModel>> EditAsync(SerialConfigModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileSerialConfig.WorkModel = "EditAsync";
            _ApiSendServer._TurnstileSerialConfig._SerialConfigModel = model;
            _ApiSendServer.List__TurnstileSerialConfig.Add(_ApiSendServer._TurnstileSerialConfig);
            return DataResponse<SerialConfigModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<SerialConfigModel>.Ok(result);
            */
        }

        /// <summary>
        /// 新增或修串口配置
        /// </summary>
        /// <returns>串口配置信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<SerialConfigModel>> AddOrEditAsync(SerialConfigModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileSerialConfig.WorkModel = "AddOrEditAsync";
            _ApiSendServer._TurnstileSerialConfig._SerialConfigModel = model;
            _ApiSendServer.List__TurnstileSerialConfig.Add(_ApiSendServer._TurnstileSerialConfig);
            return DataResponse<SerialConfigModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<SerialConfigModel>.Ok(result);
            */
        }

        /// <summary>
        /// 删除串口配置
        /// </summary>
        /// <returns>串口配置信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(SerialConfigModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileSerialConfig.WorkModel = "DeleteAsync";
            _ApiSendServer._TurnstileSerialConfig._SerialConfigModel = model;
            _ApiSendServer.List__TurnstileSerialConfig.Add(_ApiSendServer._TurnstileSerialConfig);
            return VoidResponse.Ok();
            /*
            await _service.DeleteAsync(model.Id);
            return VoidResponse.Ok();
            */
        }
    }
}
