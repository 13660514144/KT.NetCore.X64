using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpApi;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Elevator
{
    /// <summary>
    /// 统计
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private IReportService _reportService;

        public ReportController(ILogger<ReportController> logger,
            IReportService reportService)
        {
            _logger = logger;
            _reportService = reportService;
        }

        /// <summary>
        /// 获取所有大厦
        /// </summary>
        /// <returns>大厦信息</returns>
        [HttpGet("deviceStates")]
        public async Task<DataResponse<List<DeviceStateModel>>> GetAllAsync()
        {
            var result = await _reportService.GetAllDeviceStatesAsync();
            return DataResponse<List<DeviceStateModel>>.Ok(result);
        }
    }
}
