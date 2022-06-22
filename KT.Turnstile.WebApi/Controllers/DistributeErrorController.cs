using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpApi;
using KT.Turnstile.Model.Models;
using KT.Turnstile.Manage.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KT.Turnstile.Manage.WebApi.Controllers
{
    /// <summary>
    /// 分发数据
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DistributeErrorController : ControllerBase
    {
        private readonly ILogger<DistributeErrorController> _logger;
        private IDistributeErrorService _service;

        public DistributeErrorController(ILogger<DistributeErrorController> logger,
            IDistributeErrorService distributeService)
        {
            _logger = logger;
            _service = distributeService;
        }

        /// <summary>
        /// 获取所有通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<DistributeErrorModel>>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return DataResponse<List<DistributeErrorModel>>.Ok(result);
        }

        /// <summary>
        /// 获取通行权限详情
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<DistributeErrorModel>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return DataResponse<DistributeErrorModel>.Ok(result);
        }

        /// <summary>
        /// 删除通道
        /// </summary>
        /// <returns>通道信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(DistributeErrorModel model)
        {
            await _service.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }
    }
}
