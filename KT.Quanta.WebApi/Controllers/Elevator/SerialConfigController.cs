using KT.Common.WebApi.HttpApi;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Services;
using KT.Quanta.WebApi.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Elevator
{
    /// <summary>
    /// 串口配置
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SerialConfigController : ControllerBase
    {
        private readonly ILogger<SerialConfigController> _logger;
        private ISerialConfigService _serialConfigService;
        private SerialConfigModel _serialConfig;
        private ApiSendServer _ApiSendServer;
        public SerialConfigController(ILogger<SerialConfigController> logger,
            ISerialConfigService serialConfigService,
            IOptions<SerialConfigModel> serialConfig
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _serialConfigService = serialConfigService;
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
            var result = await _serialConfigService.GetAllAsync();
            return DataResponse<List<SerialConfigModel>>.Ok(result);
        }

        /// <summary>
        /// 获取串口配置详情
        /// </summary>
        /// <returns>串口配置信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<SerialConfigModel>> GetByIdAsync(string id)
        {
            var result = await _serialConfigService.GetByIdAsync(id);
            return DataResponse<SerialConfigModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改串口配置
        /// </summary>
        /// <returns>串口配置信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<SerialConfigModel>> AddOrEditAsync(SerialConfigModel model)
        {
            //进入API队列
            _ApiSendServer._SerialConfigController.WorkModel = "AddOrEditAsync";
            _ApiSendServer._SerialConfigController._SerialConfigModel = model;
            _ApiSendServer.List_Elevator_SerialConfig.Add(_ApiSendServer._SerialConfigController);
            return DataResponse<SerialConfigModel>.Ok(null);
            /*
            var result = await _serialConfigService.AddOrEditAsync(model);
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
            _ApiSendServer._SerialConfigController.WorkModel = "DeleteAsync";
            _ApiSendServer._SerialConfigController._SerialConfigModel = model;
            _ApiSendServer.List_Elevator_SerialConfig.Add(_ApiSendServer._SerialConfigController);
            return VoidResponse.Ok();
            /*
            await _serialConfigService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
            */
        }
    }
}
