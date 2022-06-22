using KT.Common.WebApi.HttpApi;
using KT.Quanta.Service.Turnstile.Dtos;
using KT.Quanta.Service.Turnstile.Services;
using KT.Quanta.WebApi.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Turnstile
{
    /// <summary>
    /// 通行权限
    /// </summary>
    [ApiController]
    [Route("turnstile/passRight")]
    public class TurnstilePassRightController : ControllerBase
    {
        private readonly ILogger<TurnstilePassRightController> _logger;
        private ITurnstilePassRightService _service;
        private ApiSendServer _ApiSendServer;
        public TurnstilePassRightController(ILogger<TurnstilePassRightController> logger,
            ITurnstilePassRightService passRightService
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _service = passRightService;
            _ApiSendServer = _apiSendServer;
        }

        /// <summary>
        /// 获取所有通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<TurnstilePassRightModel>>> GetAllAsync()
        {
            var result = await _service.GetAllAsync();
            return DataResponse<List<TurnstilePassRightModel>>.Ok(result);
        }

        /// <summary>
        /// 获取通行权限详情
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<TurnstilePassRightModel>> GetBySignAndTypeAsync(string sign, string type)
        {
            var result = await _service.GetBySignAndAccessTypeAsync(sign, type);
            return DataResponse<TurnstilePassRightModel>.Ok(result);
        }

        /// <summary>
        /// 新增通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("add")]
        public async Task<DataResponse<TurnstilePassRightModel>> AddAsync(TurnstilePassRightModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstilePassRight.WorkModel = "AddAsync";
            _ApiSendServer._TurnstilePassRight._TurnstilePassRightModel = model;
            _ApiSendServer.List_TurnstilePassRight.Add(_ApiSendServer._TurnstilePassRight);
            return DataResponse<TurnstilePassRightModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<TurnstilePassRightModel>.Ok(result);
            */
            
            //限流

        }

        /// <summary>
        /// 修改通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("edit")]
        public async Task<DataResponse<TurnstilePassRightModel>> EditAsync(TurnstilePassRightModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstilePassRight.WorkModel = "EditAsync";
            _ApiSendServer._TurnstilePassRight._TurnstilePassRightModel = model;
            _ApiSendServer.List_TurnstilePassRight.Add(_ApiSendServer._TurnstilePassRight);
            return DataResponse<TurnstilePassRightModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<TurnstilePassRightModel>.Ok(result);
            */
        }

        /// <summary>
        /// 新增或修改通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<TurnstilePassRightModel>> AddOrEditAsync(TurnstilePassRightModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstilePassRight.WorkModel = "AddOrEditAsync";
            _ApiSendServer._TurnstilePassRight._TurnstilePassRightModel = model;
            _ApiSendServer.List_TurnstilePassRight.Add(_ApiSendServer._TurnstilePassRight);
            return DataResponse<TurnstilePassRightModel>.Ok(null);
            /*
            var result = await _service.AddOrEditAsync(model);
            return DataResponse<TurnstilePassRightModel>.Ok(result);
            */
        }

        /// <summary>
        /// 删除通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(TurnstilePassRightModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstilePassRight.WorkModel = "DeleteAsync";
            _ApiSendServer._TurnstilePassRight._TurnstilePassRightModel = model;
            _ApiSendServer.List_TurnstilePassRight.Add(_ApiSendServer._TurnstilePassRight);
            return VoidResponse.Ok();
            /*
            await _service.DeleteAsync(model);
            return VoidResponse.Ok();
            */

        }

        /// <summary>
        /// 删除通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("deleteBySign")]
        public async Task<VoidResponse> DeleteBySignAsync(TurnstilePassRightModel model)
        {
            //进入API队列
            _ApiSendServer._TurnstilePassRight.WorkModel = "DeleteBySignAsync";
            _ApiSendServer._TurnstilePassRight._TurnstilePassRightModel = model;
            _ApiSendServer.List_TurnstilePassRight.Add(_ApiSendServer._TurnstilePassRight);
            return VoidResponse.Ok();
            /*
            await _service.DeleteBySignAsync(model);
            return VoidResponse.Ok();
            */
        }
    }
}
