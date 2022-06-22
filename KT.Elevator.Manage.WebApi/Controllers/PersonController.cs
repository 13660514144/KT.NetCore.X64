using KT.Common.WebApi.HttpApi;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.WebApi.Controllers
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

        public PersonController(ILogger<PersonController> logger,
            IPersonService personService)
        {
            _logger = logger;
            _personService = personService;
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
            var result = await _personService.AddOrEditAsync(model);
            return DataResponse<PersonModel>.Ok(result);
        }

        /// <summary>
        /// 删除读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync(PersonModel model)
        {
            await _personService.DeleteAsync(model.Id);
            return VoidResponse.Ok();
        }
    }
}
