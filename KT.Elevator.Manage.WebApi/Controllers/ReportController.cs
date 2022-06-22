using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpApi;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.WebApi.Controllers
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
        public DataResponse<List<DeviceStateModel>> GetAllAsync()
        {
            var result = _reportService.GetAllDeviceStates();
            return DataResponse<List<DeviceStateModel>>.Ok(result);
        }
    }
}
