using KT.Common.WebApi.HttpApi;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.WebApi.Controllers
{
    /// <summary>
    /// 人员信息表
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProcessorFloorController : ControllerBase
    {
        private readonly ILogger<ProcessorFloorController> _logger;
        private IProcessorFloorService _personService;

        public ProcessorFloorController(ILogger<ProcessorFloorController> logger,
            IProcessorFloorService personService)
        {
            _logger = logger;
            _personService = personService;
        }
        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<ProcessorFloorModel>>> GetAllAsync()
        {
            var result = await _personService.GetAllAsync();
            return DataResponse<List<ProcessorFloorModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("getInit")]
        public async Task<DataResponse<List<ProcessorModel>>> GetInitAsync()
        {
            var result = await _personService.GetInitAsync();
            return DataResponse<List<ProcessorModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("getInitByProcessorId")]
        public async Task<DataResponse<List<ProcessorFloorModel>>> GetInitByProcessorIdAsync(string id)
        {
            var result = await _personService.GetInitByProcessorIdAsync(id);
            return DataResponse<List<ProcessorFloorModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<ProcessorFloorModel>> GetByIdAsync(string id)
        {
            var result = await _personService.GetByIdAsync(id);
            return DataResponse<ProcessorFloorModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<ProcessorFloorModel>> AddOrEditAsync(ProcessorFloorModel model)
        {
            var result = await _personService.AddOrEditAsync(model);
            return DataResponse<ProcessorFloorModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("addOrEditByProcessorId")]
        public async Task<DataResponse<ProcessorModel>> AddOrEditAsync(ProcessorModel model)
        {
            var result = await _personService.AddOrEditByProcessorIdAsync(model);
            return DataResponse<ProcessorModel>.Ok(result);
        }

        /// <summary>
        /// 删除读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(ProcessorFloorModel model)
        {
            await _personService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }

        /// <summary>
        /// 删除读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("deleteByProcessorId")]
        public async Task<VoidResponse> DeleteByProcessorIdAsync(ProcessorModel model)
        {
            await _personService.DeleteByProcessorIdAsync(model.Id);
            return VoidResponse.Ok();
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("detailByProcessorId")]
        public async Task<DataResponse<List<ProcessorFloorModel>>> GetByProcessorIdAsync(string id)
        {
            var result = await _personService.GetByProcessorIdAsync(id);
            return DataResponse<List<ProcessorFloorModel>>.Ok(result);
        }
    }
}
