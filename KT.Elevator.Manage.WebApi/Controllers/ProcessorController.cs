using KT.Common.WebApi.HttpApi;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.WebApi.Controllers
{
    /// <summary>
    /// 边缘处理器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProcessorController : ControllerBase
    {
        private readonly ILogger<ProcessorController> _logger;
        private IProcessorService _processorService;

        public ProcessorController(ILogger<ProcessorController> logger,
            IProcessorService processorService)
        {
            _logger = logger;
            _processorService = processorService;
        }

        /// <summary>
        /// 获取所有边缘处理器
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<ProcessorModel>>> GetAllAsync()
        {
            var result = await _processorService.GetAllAsync();
            return DataResponse<List<ProcessorModel>>.Ok(result);
        }

        /// <summary>
        /// 获取边缘处理器详情
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<ProcessorModel>> GetByIdAsync(string id)
        {
            var result = await _processorService.GetByIdAsync(id);
            return DataResponse<ProcessorModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改边缘处理器
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<ProcessorModel>> AddOrEditAsync(ProcessorModel model)
        {
            var result = await _processorService.AddOrEditAsync(model);
            return DataResponse<ProcessorModel>.Ok(result);
        }

        /// <summary>
        /// 删除边缘处理器
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(ProcessorModel model)
        {
            await _processorService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }
    }
}
