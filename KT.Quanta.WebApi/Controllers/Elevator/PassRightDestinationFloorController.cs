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
    public class PassRightDestinationFloorController : ControllerBase
    {
        private readonly ILogger<PassRightDestinationFloorController> _logger;
        private IPassRightDestinationFloorService _service;
        private ApiSendServer _ApiSendServer;
        public PassRightDestinationFloorController(ILogger<PassRightDestinationFloorController> logger,
            IPassRightDestinationFloorService service
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _service = service;
            _ApiSendServer = _apiSendServer;
        }

        [HttpGet("GetWithDetailBySign")]
        public async Task<DataResponse<List<PassRightDestinationFloorModel>>> GetWithDetailBySignAsync(string sign)
        {
            var results = await _service.GetWithDetailBySignAsync(sign);
            return DataResponse<List<PassRightDestinationFloorModel>>.Ok(results);
        }
        /// <summary>
        /// 新增或修改通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("AddOrEdits")]
        public async Task AddOrEditsAsync(List<PassRightDestinationFloorModel> models)
        {
            //进入API队列
            _ApiSendServer._Elevator_PassRightDestinationFloorController.WorkModel = "AddOrEditsAsync";
            _ApiSendServer._Elevator_PassRightDestinationFloorController._PassRightDestinationFloorModels.Clear();
            _ApiSendServer._Elevator_PassRightDestinationFloorController._PassRightDestinationFloorModels.AddRange(models);
            _ApiSendServer.List_Elevator_PassRightDestinationFloor.Add(_ApiSendServer._Elevator_PassRightDestinationFloorController);
            //await _service.AddOrEditsAsync(models);
        }

        [HttpPost("DeleteBySign")]
        public async Task DeleteBySignAsync(PassRightModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_PassRightDestinationFloorController.WorkModel = "DeleteBySignAsync";            
            _ApiSendServer._Elevator_PassRightDestinationFloorController._PassRightModel= model;
            _ApiSendServer.List_Elevator_PassRightDestinationFloor.Add(_ApiSendServer._Elevator_PassRightDestinationFloorController);

            //await _service.DeleteBySignAsync(model.Sign);
        }

        [HttpPost("DeleteBySignAndElevatorGroupId")]
        public async Task DeleteBySignAndElevatorGroupIdAsync(PassRightDestinationFloorModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_PassRightDestinationFloorController.WorkModel = "DeleteBySignAndElevatorGroupIdAsync";
            _ApiSendServer._Elevator_PassRightDestinationFloorController._PassRightDestinationFloorModel = model;
            _ApiSendServer.List_Elevator_PassRightDestinationFloor.Add(_ApiSendServer._Elevator_PassRightDestinationFloorController);

            //await _service.DeleteBySignAndElevatorGroupIdAsync(model.Sign, model.ElevatorGroupId);
        }

    }
}
