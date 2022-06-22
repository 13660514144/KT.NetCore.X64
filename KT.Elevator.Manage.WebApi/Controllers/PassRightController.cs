using KT.Common.WebApi.HttpApi;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Manage.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.WebApi.Controllers
{
    /// <summary>
    /// 通行权限
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PassRightController : ControllerBase
    {
        private readonly ILogger<PassRightController> _logger;
        private IPassRightService _passRightService;

        public PassRightController(ILogger<PassRightController> logger,
            IPassRightService passRightService)
        {
            _logger = logger;
            _passRightService = passRightService;
        }

        /// <summary>
        /// 获取所有通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<PassRightModel>>> GetAllAsync()
        {
            var result = await _passRightService.GetAllAsync();
            return DataResponse<List<PassRightModel>>.Ok(result);
        }

        /// <summary>
        /// 获取通行权限详情
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<PassRightModel>> GetByIdAsync(string id)
        {
            var result = await _passRightService.GetByIdAsync(id);
            return DataResponse<PassRightModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<PassRightModel>> AddOrEditAsync(PassRightModel model)
        {
            var result = await _passRightService.AddOrEditAsync(model);

            return DataResponse<PassRightModel>.Ok(result);
        }

        /// <summary>
        /// 删除通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(PassRightModel model)
        {
            await _passRightService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }

        /// <summary>
        /// 删除通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("deleteBySign")]
        public async Task<VoidResponse> DeleteBySignAsync(PassRightModel model)
        {
            await _passRightService.DeleteBySignAsync(model.Sign);
            return VoidResponse.Ok();
        }

        /// <summary>
        /// 获取通行权限详情
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpGet("detailBySign")]
        public async Task<DataResponse<PassRightModel>> GetBySignAsync(string sign)
        {
            var result = await _passRightService.GetBySignAsync(sign);
            return DataResponse<PassRightModel>.Ok(result);
        }

    }
}
