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
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private IPersonService _personService;
        private ApiSendServer _ApiSendServer;
        public PersonController(ILogger<PersonController> logger,
            IPersonService personService
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
        public async Task<DataResponse<List<PersonModel>>> GetAllAsync()
        {
            var result = await _personService.GetAllAsync();
            return DataResponse<List<PersonModel>>.Ok(result);
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<PersonModel>> GetByIdAsync(string id)
        {
            var result = await _personService.GetByIdAsync(id);
            return DataResponse<PersonModel>.Ok(result);
        }

        /// <summary>
        /// 新增或修改读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("addOrEdit")]
        public async Task<DataResponse<PersonModel>> AddOrEditAsync(PersonModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_PersonController.WorkModel = "AddOrEditAsync";
            _ApiSendServer._Elevator_PersonController._PersonModel = model;
            _ApiSendServer.List_Elevator_Person.Add(_ApiSendServer._Elevator_PersonController);
            return DataResponse<PersonModel>.Ok(null);
            /*
            var result = await _personService.AddOrEditAsync(model);
            return DataResponse<PersonModel>.Ok(result);
            */
            
        }

        /// <summary>
        /// 删除读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(PersonModel model)
        {
            //进入API队列
            _ApiSendServer._Elevator_PersonController.WorkModel = "DeleteAsync";
            _ApiSendServer._Elevator_PersonController._PersonModel = model;
            _ApiSendServer.List_Elevator_Person.Add(_ApiSendServer._Elevator_PersonController);
            return VoidResponse.Ok();
            /*
            await _personService.DeleteAsync(model.Id);
            return VoidResponse.Ok(); 
             */
            //限流

        }
    }
}
