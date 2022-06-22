using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KT.Common.WebApi.HttpApi;
using KT.Common.Data.Models;
using KT.Turnstile.Model.Models;
using KT.Turnstile.Manage.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using KT.Turnstile.Manage.Service.Services;

namespace KT.Turnstile.Manage.WebApi.Controllers
{
    /// <summary>
    /// 串口配置
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SerialConfigController : ControllerBase
    {
        private readonly ILogger<SerialConfigController> _logger;
        private ISerialConfigService _service;
        private SerialConfigModel _serialConfig;

        public SerialConfigController(ILogger<SerialConfigController> logger,
            ISerialConfigService serialConfigService,
            IOptions<SerialConfigModel> serialConfig)
        {
            _logger = logger;
            _service = serialConfigService;
            _serialConfig = serialConfig.Value;
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
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<SerialConfigModel>.Ok(result);
        }

        /// <summary>
        /// 修串口配置
        /// </summary>
        /// <returns>串口配置信息</returns>
        [HttpPost("edit")]
        public async Task<DataResponse<SerialConfigModel>> EditAsync(SerialConfigModel model)
        {
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<SerialConfigModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修串口配置
        /// </summary>
        /// <returns>串口配置信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<SerialConfigModel>> AddOrEditAsync(SerialConfigModel model)
        {
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<SerialConfigModel>.Ok(result);
        }

        /// <summary>
        /// 删除串口配置
        /// </summary>
        /// <returns>串口配置信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(SerialConfigModel model)
        {
            await _service.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }
    }
}
