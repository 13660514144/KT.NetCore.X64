using KT.Common.WebApi.HttpApi;
using KT.Quanta.Service.Turnstile.Dtos;
using KT.Quanta.Service.Turnstile.Services;
using KT.Quanta.WebApi.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Turnstile
{
    /// <summary>
    /// 读卡器设备
    /// </summary>
    [ApiController]
    [Route("turnstile/cardDeviceRightGroup")]
    public class TurnstileCardDeviceRightGroupController : ControllerBase
    {
        private readonly ILogger<TurnstileCardDeviceRightGroupController> _logger;
        private ITurnstileCardDeviceRightGroupService _service;
        private ApiSendServer _ApiSendServer;
        public TurnstileCardDeviceRightGroupController(ILogger<TurnstileCardDeviceRightGroupController> logger,
            ITurnstileCardDeviceRightGroupService cardDeviceRightGroupService
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _service = cardDeviceRightGroupService;
            _ApiSendServer = _apiSendServer;
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<TurnstileCardDeviceRightGroupModel>>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return DataResponse<List<TurnstileCardDeviceRightGroupModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<TurnstileCardDeviceRightGroupModel>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return DataResponse<TurnstileCardDeviceRightGroupModel>.Ok(result);
        }

        /// <summary>
        /// 新增读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("add")]
        public async Task<DataResponse<TurnstileCardDeviceRightGroupModel>> AddAsync(TurnstileCardDeviceRightGroupModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileCardDeviceRightGroup.WorkModel = "AddAsync";
            _ApiSendServer._TurnstileCardDeviceRightGroup._TurnstileCardDeviceRightGroupModel = model;
            _ApiSendServer.List_TurnstileCardDeviceRightGroup.Add(_ApiSendServer._TurnstileCardDeviceRightGroup);
            return DataResponse<TurnstileCardDeviceRightGroupModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<TurnstileCardDeviceRightGroupModel>.Ok(result);
            */
        }

        /// <summary>
        /// 修改读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("edit")]
        public async Task<DataResponse<TurnstileCardDeviceRightGroupModel>> EditAsync(TurnstileCardDeviceRightGroupModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileCardDeviceRightGroup.WorkModel = "EditAsync";
            _ApiSendServer._TurnstileCardDeviceRightGroup._TurnstileCardDeviceRightGroupModel = model;
            _ApiSendServer.List_TurnstileCardDeviceRightGroup.Add(_ApiSendServer._TurnstileCardDeviceRightGroup);
            return DataResponse<TurnstileCardDeviceRightGroupModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<TurnstileCardDeviceRightGroupModel>.Ok(result);
            */
        }

        /// <summary>
        /// 新增或修改读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<TurnstileCardDeviceRightGroupModel>> AddOrEditAsync(TurnstileCardDeviceRightGroupModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileCardDeviceRightGroup.WorkModel = "AddOrEditAsync";
            _ApiSendServer._TurnstileCardDeviceRightGroup._TurnstileCardDeviceRightGroupModel = model;
            _ApiSendServer.List_TurnstileCardDeviceRightGroup.Add(_ApiSendServer._TurnstileCardDeviceRightGroup);
            return DataResponse<TurnstileCardDeviceRightGroupModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<TurnstileCardDeviceRightGroupModel>.Ok(result);
            */
        }

        /// <summary>
        /// 删除读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(TurnstileCardDeviceRightGroupModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileCardDeviceRightGroup.WorkModel = "DeleteAsync";
            _ApiSendServer._TurnstileCardDeviceRightGroup._TurnstileCardDeviceRightGroupModel = model;
            _ApiSendServer.List_TurnstileCardDeviceRightGroup.Add(_ApiSendServer._TurnstileCardDeviceRightGroup);
            return VoidResponse.Ok();
            /*
            await _service.DeleteAsync(model.Id);
            return VoidResponse.Ok();
            */
        }
    }
}
