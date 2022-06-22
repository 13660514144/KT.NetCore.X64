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
    /// 读卡器设备
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CardDeviceController : ControllerBase
    {
        private readonly ILogger<CardDeviceController> _logger;
        private ICardDeviceService _cardDeviceService;

        public CardDeviceController(ILogger<CardDeviceController> logger,
            ICardDeviceService cardDeviceService)
        {
            _logger = logger;
            _cardDeviceService = cardDeviceService;
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<CardDeviceModel>>> GetAllAsync()
        {
            var result = await _cardDeviceService.GetAllAsync();
            return DataResponse<List<CardDeviceModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<CardDeviceModel>> GetByIdAsync(string id)
        {
            var result = await _cardDeviceService.GetByIdAsync(id);
            return DataResponse<CardDeviceModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<CardDeviceModel>> AddOrEditAsync(CardDeviceModel model)
        {
            var result = await _cardDeviceService.AddOrEditAsync(model);
            return DataResponse<CardDeviceModel>.Ok(result);
        }

        /// <summary>
        /// 删除读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(CardDeviceModel model)
        {
            await _cardDeviceService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }
    }
}
