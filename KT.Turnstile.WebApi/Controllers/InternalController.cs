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
    /// 内部使用接口控制器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class InternalController : ControllerBase
    {
        private readonly ILogger<InternalController> _logger;
        private IProcessorService _processorService;

        public InternalController(ILogger<InternalController> logger,
            IProcessorService processorService)
        {
            _logger = logger;
            _processorService = processorService;
        }

        /// <summary>
        /// 获取边缘处理器
        /// </summary>
        /// <param name="id">边缘处理器Id</param>
        /// <returns>边缘处理器详细信息</returns>
        [HttpGet("processor/get")]
        public async Task<DataResponse<ProcessorModel>> GetProcessorByIdAsync(string id)
        {
            var result = await _processorService.GetByIdAsync(id);
            return DataResponse<ProcessorModel>.Ok(result);
        }
    }
}
