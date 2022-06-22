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
    /// 大厦
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class EdificeController : ControllerBase
    {
        private readonly ILogger<EdificeController> _logger;
        private IEdificeService _cardDeviceService;
        private ApiSendServer _ApiSendServer;
        public EdificeController(ILogger<EdificeController> logger,
            IEdificeService cardDeviceService
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _cardDeviceService = cardDeviceService;
            _ApiSendServer = _apiSendServer;
        }

        /// <summary>
        /// 获取所有大厦
        /// </summary>
        /// <returns>大厦信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<EdificeModel>>> GetAllAsync()
        {
            var result = await _cardDeviceService.GetAllAsync();
            return DataResponse<List<EdificeModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有大厦
        /// </summary>
        /// <returns>大厦信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<EdificeModel>> GetByIdAsync(string id)
        {
            var result = await _cardDeviceService.GetByIdAsync(id);
            return DataResponse<EdificeModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改大厦
        /// </summary>
        /// <returns>大厦信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<EdificeModel>> AddOrEditAsync(EdificeModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_EdificeController.WorkModel = "AddOrEditAsync";
            _ApiSendServer._Elevator_EdificeController._EdificeModel = model;
            _ApiSendServer.List_Elevator_Edifice.Add(_ApiSendServer._Elevator_EdificeController);
            return DataResponse<EdificeModel>.Ok(null);
            /*
            var result = await _cardDeviceService.AddOrEditAsync(model);
            return DataResponse<EdificeModel>.Ok(result);
            */
        }

        /// <summary>
        /// 删除大厦
        /// </summary>
        /// <returns>大厦信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(EdificeModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_EdificeController.WorkModel = "DeleteAsync";
            _ApiSendServer._Elevator_EdificeController._EdificeModel = model;
            _ApiSendServer.List_Elevator_Edifice.Add(_ApiSendServer._Elevator_EdificeController);
            return VoidResponse.Ok();
            /*
            await _cardDeviceService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
            */
        }

        /// <summary>
        /// 获取所有大厦
        /// </summary>
        /// <returns>大厦信息</returns>
        [HttpGet("allWithFloor")]
        public async Task<DataResponse<List<EdificeModel>>> GetAllWithFloorAsync()
        {
            var result = await _cardDeviceService.GetAllWithFloorAsync();
            return DataResponse<List<EdificeModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有大厦
        /// </summary>
        /// <returns>大厦信息</returns>
        [HttpGet("detailWithFloor")]
        public async Task<DataResponse<EdificeModel>> GetWithFloorByIdAsync(string id)
        {
            var result = await _cardDeviceService.GetWithFloorByIdAsync(id);
            return DataResponse<EdificeModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改大厦
        /// </summary>
        /// <returns>大厦信息</returns>
        [HttpPost("addOrEditWithFloor")]
        public async Task<DataResponse<EdificeModel>> AddOrEditWithFloorAsync(EdificeModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_EdificeController.WorkModel = "AddOrEditWithFloorAsync";
            _ApiSendServer._Elevator_EdificeController._EdificeModel = model;
            _ApiSendServer.List_Elevator_Edifice.Add(_ApiSendServer._Elevator_EdificeController);
            return DataResponse<EdificeModel>.Ok(null);
            /*
            var result = await _cardDeviceService.AddOrEditWithFloorAsync(model);
            return DataResponse<EdificeModel>.Ok(result);
            */
        }

        /// <summary>
        /// 新增或修改大厦
        /// </summary>
        /// <returns>大厦信息</returns>
        [HttpPost("deleteWithFloor")]
        public async Task<VoidResponse> DeleteWithFloor(EdificeModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_EdificeController.WorkModel = "DeleteWithFloor";
            _ApiSendServer._Elevator_EdificeController._EdificeModel = model;
            _ApiSendServer.List_Elevator_Edifice.Add(_ApiSendServer._Elevator_EdificeController);
            return VoidResponse.Ok();
            /*
            await _cardDeviceService.DeleteWithFloorAsync(model.Id);
            return VoidResponse.Ok();
            */
        }

        /// <summary>
        /// 新增或修改楼层
        /// </summary>
        /// <returns>楼层信息</returns>
        [HttpPost("editDirections")]
        public async Task<DataResponse<List<EdificeModel>>> EditDirectionsAsync(List<EdificeModel> models)
        {
            //进入API队列            
            _ApiSendServer._Elevator_EdificeController.WorkModel = "EditDirectionsAsync";
            _ApiSendServer._Elevator_EdificeController._EdificeModels.Clear();
            _ApiSendServer._Elevator_EdificeController._EdificeModels.AddRange(models);
            _ApiSendServer.List_Elevator_Edifice.Add(_ApiSendServer._Elevator_EdificeController);
            return DataResponse<List<EdificeModel>>.Ok(null);
            /*
            var result = await _cardDeviceService.EditDirectionsAsync(models);
            return DataResponse<List<EdificeModel>>.Ok(result);
            */
        }
    }
}
