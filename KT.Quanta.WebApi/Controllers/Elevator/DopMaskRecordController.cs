using KT.Common.Data.Models;
using KT.Common.WebApi.HttpApi;
using KT.Quanta.Model.Kone;
using KT.Quanta.Service.Elevator.Services;
using KT.Quanta.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Elevator
{
    [ApiController]
    [Route("[controller]")]
    public class DopMaskRecordController : ControllerBase
    {
        private readonly ILogger<DopMaskRecordController> _logger;
        private IDopMaskRecordService _service;

        public DopMaskRecordController(ILogger<DopMaskRecordController> logger,
            IDopMaskRecordService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备信息</returns>
        [HttpPost("list")]
        public async Task<DataResponse<PageData<DopMaskRecordModel>>> GetAllAsync(PageQuery<DopMaskRecordQuery> pageQuery)
        {
            var result = await _service.GetListAsync(pageQuery);
            return DataResponse<PageData<DopMaskRecordModel>>.Ok(result);
        }
    }
}
