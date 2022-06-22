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

namespace KT.Turnstile.Manage.WebApi.Controllers
{
    /// <summary>
    /// 继电器设备
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class RelayDeviceController : ControllerBase
    {
        private readonly ILogger<RelayDeviceController> _logger;
        private IRelayDeviceService _service;

        public RelayDeviceController(ILogger<RelayDeviceController> logger,
            IRelayDeviceService relayDeviceService)
        {
            _logger = logger;
            _service = relayDeviceService;
        }

        /// <summary>
        /// 获取所有继电器设备
        /// </summary>
        /// <returns>继电器设备信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<RelayDeviceModel>>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return DataResponse<List<RelayDeviceModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有继电器设备
        /// </summary>
        /// <returns>继电器设备信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<RelayDeviceModel>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return DataResponse<RelayDeviceModel>.Ok(result);
        }

        /// <summary>
        /// 新增继电器设备
        /// </summary>
        /// <returns>继电器设备信息</returns>
        [HttpPost("add")]
        public async Task<DataResponse<RelayDeviceModel>> AddAsync(RelayDeviceModel model)
        {
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<RelayDeviceModel>.Ok(result);
        }

        /// <summary>
        /// 修改继电器设备
        /// </summary>
        /// <returns>继电器设备信息</returns>
        [HttpPost("edit")]
        public async Task<DataResponse<RelayDeviceModel>> EditAsync(RelayDeviceModel model)
        {
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<RelayDeviceModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改继电器设备
        /// </summary>
        /// <returns>继电器设备信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<RelayDeviceModel>> AddOrEditAsync(RelayDeviceModel model)
        {
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<RelayDeviceModel>.Ok(result);
        }

        /// <summary>
        /// 删除继电器设备
        /// </summary>
        /// <returns>继电器设备信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(RelayDeviceModel model)
        {
            await _service.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }
    }
}
