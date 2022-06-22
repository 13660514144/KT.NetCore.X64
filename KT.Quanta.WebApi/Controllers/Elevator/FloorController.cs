using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpApi;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Quanta.WebApi.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Elevator
{
    /// <summary>
    /// 楼层
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class FloorController : ControllerBase
    {
        private readonly ILogger<FloorController> _logger;
        private IFloorService _cardDeviceService;
        private ApiSendServer _ApiSendServer;
        public FloorController(ILogger<FloorController> logger,
            IFloorService cardDeviceService
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _cardDeviceService = cardDeviceService;
            _ApiSendServer = _apiSendServer;
        }

        /// <summary>
        /// 获取所有楼层
        /// </summary>
        /// <returns>楼层信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<FloorModel>>> GetAllAsync()
        {
            var result = await _cardDeviceService.GetAllAsync();
            return DataResponse<List<FloorModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有楼层
        /// </summary>
        /// <returns>楼层信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<FloorModel>> GetByIdAsync(string id)
        {
            var result = await _cardDeviceService.GetByIdAsync(id);
            return DataResponse<FloorModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改楼层
        /// </summary>
        /// <returns>楼层信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<FloorModel>> AddOrEditAsync(FloorModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_FloorController.WorkModel = "AddOrEditAsync";
            _ApiSendServer._Elevator_FloorController._FloorModel = model;
            _ApiSendServer.List_Elevator_Floor.Add(_ApiSendServer._Elevator_FloorController);
            return DataResponse<FloorModel>.Ok(null);
            /*
            var result = await _cardDeviceService.AddOrEditAsync(model);
            return DataResponse<FloorModel>.Ok(result);
            */
        }

        /// <summary>
        /// 删除楼层
        /// </summary>
        /// <returns>楼层信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(FloorModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_FloorController.WorkModel = "DeleteAsync";
            _ApiSendServer._Elevator_FloorController._FloorModel = model;
            _ApiSendServer.List_Elevator_Floor.Add(_ApiSendServer._Elevator_FloorController);
            return VoidResponse.Ok();
            /*
            await _cardDeviceService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
            */
        }

        /// <summary>
        /// 获取所有楼层
        /// </summary>
        /// <returns>楼层信息</returns>
        [HttpGet("getByEdificeId")]
        public async Task<DataResponse<List<FloorModel>>> GetByEdificeIdAsync(string id)
        {
            var result = await _cardDeviceService.GetByEdificeIdAsync(id);
            return DataResponse<List<FloorModel>>.Ok(result);
        }

        /// <summary>
        /// 新增或修改楼层
        /// </summary>
        /// <returns>楼层信息</returns>
        [HttpPost("editDirections")]
        public async Task<DataResponse<List<FloorModel>>> EditDirectionsAsync(List<FloorModel> models)
        {
            //进入API队列
            _ApiSendServer._Elevator_FloorController.WorkModel = "EditDirectionsAsync";
            _ApiSendServer._Elevator_FloorController._FloorModels.Clear();
            _ApiSendServer._Elevator_FloorController._FloorModels.AddRange(models);
            _ApiSendServer.List_Elevator_Floor.Add(_ApiSendServer._Elevator_FloorController);
            return DataResponse<List<FloorModel>>.Ok(null);
            /*
            var result = await _cardDeviceService.EditDirectionsAsync(models);
            return DataResponse<List<FloorModel>>.Ok(result);
            */
        }

    }
}
