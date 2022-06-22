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
    /// 边缘处理器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProcessorController : ControllerBase
    {
        private readonly ILogger<ProcessorController> _logger;
        private IProcessorService _service;

        public ProcessorController(ILogger<ProcessorController> logger,
            IProcessorService processorService)
        {
            _logger = logger;
            _service = processorService;
        }

        /// <summary>
        /// 获取所有边缘处理器
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpGet("all")]
        public DataResponse<List<ProcessorModel>> GetAllAsync()
        {
            var result = _service.GetStaticListAsync();
            return DataResponse<List<ProcessorModel>>.Ok(result);
        }

        /// <summary>
        /// 获取边缘处理器详情
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<ProcessorModel>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return DataResponse<ProcessorModel>.Ok(result);
        }

        /// <summary>
        /// 新增边缘处理器
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpPost("add")]
        public async Task<DataResponse<ProcessorModel>> AddAsync(ProcessorModel model)
        {
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<ProcessorModel>.Ok(result);
        }

        /// <summary>
        /// 修改边缘处理器
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpPost("edit")]
        public async Task<DataResponse<ProcessorModel>> EditAsync(ProcessorModel model)
        {
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<ProcessorModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改边缘处理器
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<ProcessorModel>> AddOrEditAsync(ProcessorModel model)
        {
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<ProcessorModel>.Ok(result);
        }

        /// <summary>
        /// 删除边缘处理器
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(ProcessorModel model)
        {
            await _service.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }
    }
}
