using KT.Common.WebApi.HttpApi;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Quanta.WebApi.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Elevator
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
        private ApiSendServer _ApiSendServer;
        public ProcessorController(ILogger<ProcessorController> logger,
            IProcessorService processorService
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _processorService = processorService;
            _ApiSendServer = _apiSendServer;
        }

        /// <summary>
        /// 获取所有边缘处理器
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<ProcessorModel>>> GetAllAsync()
        {
            var result = await _processorService.GetFromDeviceTypeAsync();
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
            //进入API队列
            _ApiSendServer._ProcessorController.WorkModel = "AddOrEditAsync";
            _ApiSendServer._ProcessorController._ProcessorModel = model;
            _ApiSendServer.List_Elevator_Processor.Add(_ApiSendServer._ProcessorController);
            return DataResponse<ProcessorModel>.Ok(null);
            /*
            var result = await _processorService.AddOrEditAsync(model);
            return DataResponse<ProcessorModel>.Ok(result);
            */
        }

        /// <summary>
        /// 删除边缘处理器
        /// </summary>
        /// <returns>边缘处理器信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(ProcessorModel model)
        {
            //进入API队列
            _ApiSendServer._ProcessorController.WorkModel = "DeleteAsync";
            _ApiSendServer._ProcessorController._ProcessorModel = model;
            _ApiSendServer.List_Elevator_Processor.Add(_ApiSendServer._ProcessorController);
            return VoidResponse.Ok();
            /*
            await _processorService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
            */
        }
    }
}
