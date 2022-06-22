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
    /// 楼层
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FloorController : ControllerBase
    {
        private readonly ILogger<FloorController> _logger;
        private IFloorService _cardDeviceService;

        public FloorController(ILogger<FloorController> logger,
            IFloorService cardDeviceService)
        {
            _logger = logger;
            _cardDeviceService = cardDeviceService;
        }

        /// <summary>
        /// 获取所有楼层
        /// </summary>
        /// <returns>楼层信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<FloorModel>>> GetAllAsync()
        {
            var result = await _cardDeviceService.GetAllAsync();
            return DataResponse<List<FloorModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有楼层
        /// </summary>
        /// <returns>楼层信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<FloorModel>> GetByIdAsync(string id)
        {
            var result = await _cardDeviceService.GetByIdAsync(id);
            return DataResponse<FloorModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改楼层
        /// </summary>
        /// <returns>楼层信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<FloorModel>> AddOrEditAsync(FloorModel model)
        {
            var result = await _cardDeviceService.AddOrEditAsync(model);
            return DataResponse<FloorModel>.Ok(result);
        }

        /// <summary>
        /// 删除楼层
        /// </summary>
        /// <returns>楼层信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(FloorModel model)
        {
            await _cardDeviceService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }

        /// <summary>
        /// 获取所有楼层
        /// </summary>
        /// <returns>楼层信息</returns>
        [HttpGet("getByEdificeId")]
        public async Task<DataResponse<List<FloorModel>>> GetByEdificeIdAsync(string id)
        {
            var result = await _cardDeviceService.GetByEdificeIdAsync(id);
            return DataResponse<List<FloorModel>>.Ok(result);
        }

    }
}
