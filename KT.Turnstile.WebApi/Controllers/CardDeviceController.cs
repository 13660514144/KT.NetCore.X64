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
    /// 读卡器设备
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CardDeviceController : ControllerBase
    {
        private readonly ILogger<CardDeviceController> _logger;
        private ICardDeviceService _service;

        public CardDeviceController(ILogger<CardDeviceController> logger,
            ICardDeviceService cardDeviceService)
        {
            _logger = logger;
            _service = cardDeviceService;
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<CardDeviceModel>>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return DataResponse<List<CardDeviceModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<CardDeviceModel>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return DataResponse<CardDeviceModel>.Ok(result);
        }

        /// <summary>
        /// 新增读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("add")]
        public async Task<DataResponse<CardDeviceModel>> AddAsync(CardDeviceModel model)
        {
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<CardDeviceModel>.Ok(result);
        }

        /// <summary>
        /// 修改读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("edit")]
        public async Task<DataResponse<CardDeviceModel>> EditAsync(CardDeviceModel model)
        {
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<CardDeviceModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<CardDeviceModel>> AddOrEditAsync(CardDeviceModel model)
        {
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<CardDeviceModel>.Ok(result);
        }

        /// <summary>
        /// 删除读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(CardDeviceModel model)
        {
            await _service.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }
    }
}
