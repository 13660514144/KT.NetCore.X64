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
    /// 大厦
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class EdificeController : ControllerBase
    {
        private readonly ILogger<EdificeController> _logger;
        private IEdificeService _cardDeviceService;

        public EdificeController(ILogger<EdificeController> logger,
            IEdificeService cardDeviceService)
        {
            _logger = logger;
            _cardDeviceService = cardDeviceService;
        }

        /// <summary>
        /// 获取所有大厦
        /// </summary>
        /// <returns>大厦信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<EdificeModel>>> GetAllAsync()
        {
            var result = await _cardDeviceService.GetAllAsync();
            return DataResponse<List<EdificeModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有大厦
        /// </summary>
        /// <returns>大厦信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<EdificeModel>> GetByIdAsync(string id)
        {
            var result = await _cardDeviceService.GetByIdAsync(id);
            return DataResponse<EdificeModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改大厦
        /// </summary>
        /// <returns>大厦信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<EdificeModel>> AddOrEditAsync(EdificeModel model)
        {
            var result = await _cardDeviceService.AddOrEditAsync(model);
            return DataResponse<EdificeModel>.Ok(result);
        }

        /// <summary>
        /// 删除大厦
        /// </summary>
        /// <returns>大厦信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(EdificeModel model)
        {
            await _cardDeviceService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }

        /// <summary>
        /// 获取所有大厦
        /// </summary>
        /// <returns>大厦信息</returns>
        [HttpGet("allWithFloor")]
        public async Task<DataResponse<List<EdificeModel>>> GetAllWithFloorAsync()
        {
            var result = await _cardDeviceService.GetAllWithFloorAsync();
            return DataResponse<List<EdificeModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有大厦
        /// </summary>
        /// <returns>大厦信息</returns>
        [HttpGet("detailWithFloor")]
        public async Task<DataResponse<EdificeModel>> GetWithFloorByIdAsync(string id)
        {
            var result = await _cardDeviceService.GetWithFloorByIdAsync(id);
            return DataResponse<EdificeModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改大厦
        /// </summary>
        /// <returns>大厦信息</returns>
        [HttpPost("addOrEditWithFloor")]
        public async Task<DataResponse<EdificeModel>> AddOrEditWithFloorAsync(EdificeModel model)
        {
            var result = await _cardDeviceService.AddOrEditWithFloorAsync(model);
            return DataResponse<EdificeModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改大厦
        /// </summary>
        /// <returns>大厦信息</returns>
        [HttpPost("deleteWithFloor")]
        public async Task<VoidResponse> DeleteWithFloor(EdificeModel model)
        {
            await _cardDeviceService.DeleteWithFloorAsync(model.Id);
            return VoidResponse.Ok();
        }
    }
}
