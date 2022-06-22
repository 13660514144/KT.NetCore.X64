using KT.Common.Data.Models;
using KT.Common.WebApi.HttpApi;
using KT.Quanta.Model.Elevator.Dtos;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Services;
using KT.Quanta.WebApi.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Elevator
{
    /// <summary>
    /// 通行权限
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HandleElevatorDeviceAuxiliaryController : ControllerBase
    {
        private readonly ILogger<HandleElevatorDeviceAuxiliaryController> _logger;
        private IHandleElevatorDeviceAuxiliaryService _service;
        private ApiSendServer _ApiSendServer;
        public HandleElevatorDeviceAuxiliaryController(ILogger<HandleElevatorDeviceAuxiliaryController> logger,
            IHandleElevatorDeviceAuxiliaryService service
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _service = service;
            _ApiSendServer = _apiSendServer;
        }
        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<HandleElevatorDeviceAuxiliaryModel>>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return DataResponse<List<HandleElevatorDeviceAuxiliaryModel>>.Ok(result);
        }
        /// <summary>
        /// 新增或修改通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("AddOrEdits")]
        public async Task AddOrEditsAsync(List<HandleElevatorDeviceAuxiliaryModel> models)
        {
            //进入API队列
            _ApiSendServer._Elevator_Elevator_HandleElevatorDeviceAuxiliaryController.WorkModel = "AddOrEditsAsync";
            _ApiSendServer._Elevator_Elevator_HandleElevatorDeviceAuxiliaryController._HandleElevatorDeviceAuxiliaryModels.Clear();
            _ApiSendServer._Elevator_Elevator_HandleElevatorDeviceAuxiliaryController._HandleElevatorDeviceAuxiliaryModels.AddRange(models);
            _ApiSendServer.List_Elevator_HandleElevatorDeviceAuxiliary.Add(_ApiSendServer._Elevator_Elevator_HandleElevatorDeviceAuxiliaryController);
            
            //await _service.AddOrEditsAsync(models);
        }

    }
}
