using KT.Common.WebApi.HttpApi;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Manage.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.WebApi.Controllers
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

        public SerialConfigController(ILogger<SerialConfigController> logger,
            ISerialConfigService serialConfigService,
            IOptions<SerialConfigModel> serialConfig)
        {
            _logger = logger;
            _serialConfigService = serialConfigService;
            _serialConfig = serialConfig.Value;
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
            var result = await _serialConfigService.AddOrEditAsync(model);
            return DataResponse<SerialConfigModel>.Ok(result);
        }
         
        /// <summary>
        /// 删除串口配置
        /// </summary>
        /// <returns>串口配置信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(SerialConfigModel model)
        {
            await _serialConfigService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }
    }
}
