using KT.Common.WebApi.HttpApi;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Quanta.WebApi.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Elevator
{
    /// <summary>
    /// 人员信息表
    /// </summary>
    [ApiController]
    [Route("turnstile/person")]
    public class TurnstilePersonController : ControllerBase
    {
        private readonly ILogger<TurnstilePersonController> _logger;
        private ITurnstilePersonService _personService;
        private ApiSendServer _ApiSendServer;
        public TurnstilePersonController(ILogger<TurnstilePersonController> logger,
            ITurnstilePersonService personService
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
        public async Task<DataResponse<List<TurnstilePersonModel>>> GetAllAsync()
        {
            var result = await _personService.GetAllAsync();
            return DataResponse<List<TurnstilePersonModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<TurnstilePersonModel>> GetByIdAsync(string id)
        {
            var result = await _personService.GetByIdAsync(id);
            return DataResponse<TurnstilePersonModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<TurnstilePersonModel>> AddOrEditAsync(TurnstilePersonModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstilePerson.WorkModel = "AddOrEditAsync";
            _ApiSendServer._TurnstilePerson._TurnstilePersonModel = model;
            _ApiSendServer.List_TurnstilePerson.Add(_ApiSendServer._TurnstilePerson);
            return DataResponse<TurnstilePersonModel>.Ok(null);
            /*
            var result = await _personService.AddOrEditAsync(model);
            return DataResponse<TurnstilePersonModel>.Ok(result);
            */
        }

        /// <summary>
        /// 删除读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(TurnstilePersonModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstilePerson.WorkModel = "DeleteAsync";
            _ApiSendServer._TurnstilePerson._TurnstilePersonModel = model;
            _ApiSendServer.List_TurnstilePerson.Add(_ApiSendServer._TurnstilePerson);
            return VoidResponse.Ok();
            /*
            await _personService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
            */           
        }
    }
}
