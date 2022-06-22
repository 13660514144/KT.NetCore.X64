using KT.Common.Data.Models;
using KT.Common.WebApi.HttpApi;
using KT.Quanta.Model.Elevator.Dtos;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Services;
using KT.Quanta.WebApi.Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace KT.Quanta.WebApi.Controllers.Elevator
{
    /// <summary>
    /// 通行权限
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PassRightController : ControllerBase
    {
        private readonly ILogger<PassRightController> _logger;
        private IPassRightService _passRightService;
        private ApiSendServer _ApiSendServer;
        public PassRightController(ILogger<PassRightController> logger,
            IPassRightService passRightService
            , ApiSendServer _apiSendServer)
        {
            _logger = logger;
            _passRightService = passRightService;
            _ApiSendServer = _apiSendServer;
        }

        /// <summary>
        /// 获取所有通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<PassRightModel>>> GetAllAsync()
        {
            var result = await _passRightService.GetAllAsync();
            return DataResponse<List<PassRightModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpGet("allPage")]
        public async Task<DataResponse<PageData<PassRightModel>>> GetAllPageAsync(int page, int size, string name, string sign)
        {
            var result = await _passRightService.GetAllPageAsync(page, size, name, sign);
            return DataResponse<PageData<PassRightModel>>.Ok(result);
        }

        /// <summary>
        /// 获取通行权限详情
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<PassRightModel>> GetByIdAsync(string id)
        {
            var result = await _passRightService.GetByIdAsync(id);
            return DataResponse<PassRightModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<PassRightModel>> AddOrEditAsync(PassRightModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_PassRightController.WorkModel = "AddOrEditAsync";
            _ApiSendServer._Elevator_PassRightController._PassRightModel = model;
            _ApiSendServer.List_Elevator_PassRight.Add(_ApiSendServer._Elevator_PassRightController);
            return DataResponse<PassRightModel>.Ok(null);
            /*
            var result = await _passRightService.AddOrEditAsync(model);            
            return DataResponse<PassRightModel>.Ok(result);
            */
            
            //限流

        }
 
        /// <summary>
        /// 删除通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(PassRightModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_PassRightController.WorkModel = "DeleteAsync";
            _ApiSendServer._Elevator_PassRightController._PassRightModel = model;
            _ApiSendServer.List_Elevator_PassRight.Add(_ApiSendServer._Elevator_PassRightController);
            return VoidResponse.Ok();
            /*
            await _passRightService.DeleteAsync(model.Id);
            return VoidResponse.Ok();      
            */
            //限流
        }

        /// <summary>
        /// 删除通行权限
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpPost("deleteBySign")]
        public async Task<VoidResponse> DeleteBySignAsync(PassRightModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_PassRightController.WorkModel = "DeleteBySignAsync";
            _ApiSendServer._Elevator_PassRightController._PassRightModel = model;
            _ApiSendServer.List_Elevator_PassRight.Add(_ApiSendServer._Elevator_PassRightController);
            return VoidResponse.Ok();
            /*
            await _passRightService.DeleteBySignAsync(model.Sign);
            return VoidResponse.Ok();
            */
        }

        /// <summary>
        /// 获取通行权限详情
        /// </summary>
        /// <returns>通行权限信息</returns>
        [HttpGet("detailBySign")]
        public async Task<DataResponse<PassRightModel>> GetBySignAsync(string sign)
        {
            var result = await _passRightService.GetBySignAsync(sign);
            return DataResponse<PassRightModel>.Ok(result);
        }

    }
}
