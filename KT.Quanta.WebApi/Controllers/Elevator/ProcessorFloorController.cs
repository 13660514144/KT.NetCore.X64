using KT.Common.WebApi.HttpApi;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Quanta.WebApi.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Elevator
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
        private ApiSendServer _ApiSendServer;
        public ProcessorFloorController(ILogger<ProcessorFloorController> logger,
            IProcessorFloorService personService
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _personService = personService;
            _ApiSendServer = _apiSendServer;
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
            //进入API队列
            _ApiSendServer._ProcessorFloorController.WorkModel = "AddOrEditAsyncFloor";
            _ApiSendServer._ProcessorFloorController._ProcessorFloorModel = model;
            _ApiSendServer.List_Elevator_ProcessorFloor.Add(_ApiSendServer._ProcessorFloorController);
            return DataResponse<ProcessorFloorModel>.Ok(null);
            /*
            var result = await _personService.AddOrEditAsync(model);
            return DataResponse<ProcessorFloorModel>.Ok(result);
            */
        }

        /// <summary>
        /// 新增或修改读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("addOrEditByProcessorId")]
        public async Task<DataResponse<ProcessorModel>> AddOrEditAsync(ProcessorModel model)
        {
            //进入API队列
            _ApiSendServer._ProcessorFloorController.WorkModel = "AddOrEditAsync";
            _ApiSendServer._ProcessorFloorController._ProcessorModel = model;
            _ApiSendServer.List_Elevator_ProcessorFloor.Add(_ApiSendServer._ProcessorFloorController);
            return DataResponse<ProcessorModel>.Ok(null);
            /*
            var result = await _personService.AddOrEditByProcessorIdAsync(model);
            return DataResponse<ProcessorModel>.Ok(result);
            */
        }

        /// <summary>
        /// 删除读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(ProcessorFloorModel model)
        {
            //进入API队列
            _ApiSendServer._ProcessorFloorController.WorkModel = "DeleteAsync";
            _ApiSendServer._ProcessorFloorController._ProcessorFloorModel = model;
            _ApiSendServer.List_Elevator_ProcessorFloor.Add(_ApiSendServer._ProcessorFloorController);
            return VoidResponse.Ok();
            /*
            await _personService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
            */
        }

        /// <summary>
        /// 删除读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("deleteByProcessorId")]
        public async Task<VoidResponse> DeleteByProcessorIdAsync(ProcessorModel model)
        {
            //进入API队列
            _ApiSendServer._ProcessorFloorController.WorkModel = "DeleteByProcessorIdAsync";
            _ApiSendServer._ProcessorFloorController._ProcessorModel = model;
            _ApiSendServer.List_Elevator_ProcessorFloor.Add(_ApiSendServer._ProcessorFloorController);
            return VoidResponse.Ok();
            /*
            await _personService.DeleteByProcessorIdAsync(model.Id);
            return VoidResponse.Ok();
            */
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("detailByProcessorId")]
        public async Task<DataResponse<ProcessorModel>> GetByProcessorIdAsync(string id)
        {
            var result = await _personService.GetByProcessorIdAsync(id);
            if (result == null)
            {
                result = new ProcessorModel();
                result.Id = id;
                result.ProcessorFloorIds = new List<string>();
                result.ProcessorFloors = new List<ProcessorFloorModel>();
            }
            return DataResponse<ProcessorModel>.Ok(result);
        }
    }
}
