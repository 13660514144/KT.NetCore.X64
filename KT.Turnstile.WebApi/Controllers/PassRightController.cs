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
using KT.Turnstile.Manage.Service.Services;

namespace KT.Turnstile.Manage.WebApi.Controllers
{
    /// <summary>
    /// 通行权限
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PassRightController : ControllerBase
    {
        private readonly ILogger<PassRightController> _logger;
        private IPassRightService _service;

        public PassRightController(ILogger<PassRightController> logger,
            IPassRightService passRightService)
        {
            _logger = logger;
            _service = passRightService;
        }

        /// <summary>
        /// 获取所有通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<PassRightModel>>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return DataResponse<List<PassRightModel>>.Ok(result);
        }

        /// <summary>
        /// 获取通行权限详情
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<PassRightModel>> GetByIdAsync(string id)
        {
            var result = await _service.GetByIdAsync(id);
            return DataResponse<PassRightModel>.Ok(result);
        }

        /// <summary>
        /// 新增通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("add")]
        public async Task<DataResponse<PassRightModel>> AddAsync(PassRightModel model)
        {
            var result = await _service.AddOrEditAsync(model);

            return DataResponse<PassRightModel>.Ok(result);
        }

        /// <summary>
        /// 修改通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("edit")]
        public async Task<DataResponse<PassRightModel>> EditAsync(PassRightModel model)
        {
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<PassRightModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<PassRightModel>> AddOrEditAsync(PassRightModel model)
        {
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<PassRightModel>.Ok(result);
        }

        /// <summary>
        /// 删除通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(PassRightModel model)
        {
            await _service.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }
    }
}
