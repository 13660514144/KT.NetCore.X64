using KT.Common.WebApi.HttpApi;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Turnstile.Dtos;
using KT.Quanta.Service.Turnstile.IServices;
using KT.Quanta.WebApi.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Turnstile
{
    /// <summary>
    /// 边缘处理器
    /// </summary>
    [ApiController]
    [Route("turnstile/processor")]
    public class TurnstileProcessorController : ControllerBase
    {
        private readonly ILogger<TurnstileProcessorController> _logger;
        private ITurnstileProcessorService _service;
        private ApiSendServer _ApiSendServer;
        public TurnstileProcessorController(ILogger<TurnstileProcessorController> logger,
            ITurnstileProcessorService processorService
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _service = processorService;
            _ApiSendServer = _apiSendServer;
        }

        /// <summary>
        /// 获取所有边缘处理器
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<TurnstileProcessorModel>>> GetAllAsync()
        {
            var result = await _service.GetFromDeviceTypeAsync();
            return DataResponse<List<TurnstileProcessorModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有边缘处理器
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpGet("getStatic")]
        public async Task<DataResponse<List<TurnstileProcessorModel>>> GetStaticAsync()
        {
            var result = await _service.GetStaticListAsync();
            return DataResponse<List<TurnstileProcessorModel>>.Ok(result);
        }

        /// <summary>
        /// 获取边缘处理器详情
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<TurnstileProcessorModel>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return DataResponse<TurnstileProcessorModel>.Ok(result);
        }

        /// <summary>
        /// 新增边缘处理器
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpPost("add")]
        public async Task<DataResponse<TurnstileProcessorModel>> AddAsync(TurnstileProcessorModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileProcessor.WorkModel = "AddAsync";
            _ApiSendServer._TurnstileProcessor._TurnstileProcessorModel = model;
            _ApiSendServer.List__TurnstileProcessor.Add(_ApiSendServer._TurnstileProcessor);
            return DataResponse<TurnstileProcessorModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<TurnstileProcessorModel>.Ok(result);
            */
        }

        /// <summary>
        /// 修改边缘处理器
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpPost("edit")]
        public async Task<DataResponse<TurnstileProcessorModel>> EditAsync(TurnstileProcessorModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileProcessor.WorkModel = "EditAsync";
            _ApiSendServer._TurnstileProcessor._TurnstileProcessorModel = model;
            _ApiSendServer.List__TurnstileProcessor.Add(_ApiSendServer._TurnstileProcessor);
            return DataResponse<TurnstileProcessorModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<TurnstileProcessorModel>.Ok(result);
            */
        }

        /// <summary>
        /// 新增或修改边缘处理器
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<TurnstileProcessorModel>> AddOrEditAsync(TurnstileProcessorModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileProcessor.WorkModel = "AddOrEditAsync";
            _ApiSendServer._TurnstileProcessor._TurnstileProcessorModel = model;
            _ApiSendServer.List__TurnstileProcessor.Add(_ApiSendServer._TurnstileProcessor);
            return DataResponse<TurnstileProcessorModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<TurnstileProcessorModel>.Ok(result);
            */
        }

        /// <summary>
        /// 删除边缘处理器
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(TurnstileProcessorModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstileProcessor.WorkModel = "DeleteAsync";
            _ApiSendServer._TurnstileProcessor._TurnstileProcessorModel = model;
            _ApiSendServer.List__TurnstileProcessor.Add(_ApiSendServer._TurnstileProcessor);
            return VoidResponse.Ok();
            /*
            await _service.DeleteAsync(model.Id);
            return VoidResponse.Ok();
            */
        }
    }
}
