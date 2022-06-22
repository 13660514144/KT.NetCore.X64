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
    /// 读卡器设备
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CardDeviceController : ControllerBase
    {
        private readonly ILogger<CardDeviceController> _logger;
        private ICardDeviceService _cardDeviceService;
        private ApiSendServer _ApiSendServer;
        public CardDeviceController(ILogger<CardDeviceController> logger,
            ICardDeviceService cardDeviceService
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _cardDeviceService = cardDeviceService;
            _ApiSendServer = _apiSendServer;
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<CardDeviceModel>>> GetAllAsync()
        {
            var result = await _cardDeviceService.GetFromDeviceTypeAsync();
            return DataResponse<List<CardDeviceModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<CardDeviceModel>> GetByIdAsync(string id)
        {
            var result = await _cardDeviceService.GetByIdAsync(id);
            return DataResponse<CardDeviceModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<CardDeviceModel>> AddOrEditAsync(CardDeviceModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_CardDeviceController.WorkModel = "AddOrEditAsync";
            _ApiSendServer._Elevator_CardDeviceController._CardDeviceModel = model;
            _ApiSendServer.List_Elevator_CardDevice.Add(_ApiSendServer._Elevator_CardDeviceController);
            return DataResponse<CardDeviceModel>.Ok(null);
            /*
            var result = await _cardDeviceService.AddOrEditAsync(model);
            return DataResponse<CardDeviceModel>.Ok(result);
            */
        }

        /// <summary>
        /// 删除读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(CardDeviceModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_CardDeviceController.WorkModel = "DeleteAsync";
            _ApiSendServer._Elevator_CardDeviceController._CardDeviceModel = model;
            _ApiSendServer.List_Elevator_CardDevice.Add(_ApiSendServer._Elevator_CardDeviceController);
            return VoidResponse.Ok();
            /*
            await _cardDeviceService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
            */
        }
    }
}
