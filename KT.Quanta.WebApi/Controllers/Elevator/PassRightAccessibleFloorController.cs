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
    public class PassRightAccessibleFloorController : ControllerBase
    {
        private readonly ILogger<PassRightAccessibleFloorController> _logger;
        private IPassRightAccessibleFloorService _service;
        private ApiSendServer _ApiSendServer;
        public PassRightAccessibleFloorController(ILogger<PassRightAccessibleFloorController> logger,
            IPassRightAccessibleFloorService service
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _service = service;
            _ApiSendServer = _apiSendServer;
        }

        [HttpGet("GetWithDetailBySign")]
        public async Task<DataResponse<List<PassRightAccessibleFloorModel>>> GetWithDetailBySignAsync(string sign)
        {
            var results = await _service.GetWithDetailBySignAsync(sign);
            return DataResponse<List<PassRightAccessibleFloorModel>>.Ok(results);
        }
        /// <summary>
        /// 新增或修改通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("AddOrEdits")]
        public async Task AddOrEditsAsync(List<PassRightAccessibleFloorModel> models)
        {
            //进入API队列
            _ApiSendServer._Elevator_PassRightAccessibleFloorController.WorkModel = "AddOrEditsAsync";
            _ApiSendServer._Elevator_PassRightAccessibleFloorController._PassRightModels.Clear();
            _ApiSendServer._Elevator_PassRightAccessibleFloorController._PassRightModels.AddRange(models);
            _ApiSendServer.List_Elevator_PassRightAccessibleFloor.Add(_ApiSendServer._Elevator_PassRightAccessibleFloorController);
            
            //await _service.AddOrEditsAsync(models);
        }

        [HttpPost("DeleteBySign")]
        public async Task DeleteBySignAsync(PassRightModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_PassRightAccessibleFloorController.WorkModel = "DeleteBySignAsync";
            _ApiSendServer._Elevator_PassRightAccessibleFloorController._PassRightModel = model;
            _ApiSendServer.List_Elevator_PassRightAccessibleFloor.Add(_ApiSendServer._Elevator_PassRightAccessibleFloorController);

            //await _service.DeleteBySignAsync(model.Sign);
        }

        [HttpPost("DeleteBySignAndElevatorGroupId")]
        public async Task DeleteBySignAndElevatorGroupIdAsync(PassRightAccessibleFloorModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_PassRightAccessibleFloorController.WorkModel = "DeleteBySignAndElevatorGroupIdAsync";
            _ApiSendServer._Elevator_PassRightAccessibleFloorController._PassRightAccessibleFloorModel = model;
            _ApiSendServer.List_Elevator_PassRightAccessibleFloor.Add(_ApiSendServer._Elevator_PassRightAccessibleFloorController);

            //await _service.DeleteBySignAndElevatorGroupIdAsync(model.Sign, model.ElevatorGroupId);
        }

    }
}
