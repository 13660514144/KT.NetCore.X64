using KT.Common.WebApi.HttpApi;
using KT.Turnstile.Model.Models;
using KT.Turnstile.Manage.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.WebApi.Controllers
{
    /// <summary>
    /// 读卡器设备
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CardDeviceRightGroupController : ControllerBase
    {
        private readonly ILogger<CardDeviceRightGroupController> _logger;
        private ICardDeviceRightGroupService _service;

        public CardDeviceRightGroupController(ILogger<CardDeviceRightGroupController> logger,
            ICardDeviceRightGroupService cardDeviceRightGroupService)
        {
            _logger = logger;
            _service = cardDeviceRightGroupService;
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<CardDeviceRightGroupModel>>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return DataResponse<List<CardDeviceRightGroupModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<CardDeviceRightGroupModel>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return DataResponse<CardDeviceRightGroupModel>.Ok(result);
        }

        /// <summary>
        /// 新增读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("add")]
        public async Task<DataResponse<CardDeviceRightGroupModel>> AddAsync(CardDeviceRightGroupModel model)
        {
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<CardDeviceRightGroupModel>.Ok(result);
        }

        /// <summary>
        /// 修改读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("edit")]
        public async Task<DataResponse<CardDeviceRightGroupModel>> EditAsync(CardDeviceRightGroupModel model)
        {
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<CardDeviceRightGroupModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<CardDeviceRightGroupModel>> AddOrEditAsync(CardDeviceRightGroupModel model)
        {
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<CardDeviceRightGroupModel>.Ok(result);
        }

        /// <summary>
        /// 删除读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(CardDeviceRightGroupModel model)
        {
            await _service.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }
    }
}
