using KT.Common.WebApi.HttpApi;
using KT.Common.WebApi.HttpModel;
using KT.Quanta.Service.Devices.Hikvision;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Quanta.WebApi.Common.Filters;
using KT.Quanta.WebApi.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Elevator
{
    /// <summary>
    /// 用户
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PassRecordController : ControllerBase
    {
        private ILogger<PassRecordController> _logger;
        private IHikvisionResponseHandler _hikvisionResponseHandler;
        private ApiSendServer _ApiSendServer;

        public PassRecordController(IHikvisionResponseHandler hikvisionResponseHandler,
            ILogger<PassRecordController> logger
            , ApiSendServer _apiSendServer)
        {
            _hikvisionResponseHandler = hikvisionResponseHandler;
            _logger = logger;
            _ApiSendServer = _apiSendServer;
        }

        /// <summary>
        /// 上传数据
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("hikvisionPush")]
        public async Task<VoidResponse> HikvisionPushAsync(PassRecordModel passRecord)
        {
            //进入API队列
            _ApiSendServer._Elevator_PassRecordController.WorkModel = "HikvisionPushAsync";
            _ApiSendServer._Elevator_PassRecordController._PassRecordModel = passRecord;
            _ApiSendServer.List_Elevator_PassRecord.Add(_ApiSendServer._Elevator_PassRecordController);
            return VoidResponse.Ok();
            /*
            await _hikvisionResponseHandler.UploadPassRecordAsync(passRecord);
            return VoidResponse.Ok();
            */
        }
    }
}
