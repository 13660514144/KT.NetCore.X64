using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KT.Common.WebApi.HttpApi;
using KT.Common.Core.Utils;
using KT.WinPak.WebApi.V48.Common.Filters;
using KT.WinPak.SDK.V48;
using KT.WinPak.SDK.V48.IServices;
using KT.WinPak.SDK.V48.Models;
using KT.WinPak.SDK.V48.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KT.WinPak.WebApi.V48.Common;
using KT.WinPak.SDK.V48.Settings;
using KT.WinPak.Service.V48.IServices;
using Microsoft.Extensions.Options;

namespace KT.WinPak.WebApi.V48.Controllers
{
    /// <summary>
    /// 时区
    /// </summary>
    [ApiController]
    [Route("zoneTime")]
    [TypeFilter(typeof(LoginUserAttribute))]
    public class ZoneTimeController : ControllerBase
    {
        private ITimeZoneSdkService _zoneTimeSdkService;
        private ITimeZoneSqlService _zoneTimeSqlService;
        private IUserService _userService;
        private ILogger<ZoneTimeController> _logger;
        private AppSettings _appsettings;

        public ZoneTimeController(ITimeZoneSdkService zoneTimeSdkService,
            ITimeZoneSqlService zoneTimeSqlService,
            IUserService userService,
            ILogger<ZoneTimeController> logger,
            IOptions<AppSettings> appsettings)
        {
            _zoneTimeSdkService = zoneTimeSdkService;
            _zoneTimeSqlService = zoneTimeSqlService;
            _userService = userService;
            _logger = logger;
            _appsettings = appsettings.Value;
        }

        /// <summary>
        /// 获取设置列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public DataResponse<List<TimeZoneModel>> GetAll()
        {
            //var result = _zoneTimeSdkService.GetAll();
            var result = _zoneTimeSqlService.GetAll();
            return DataResponse<List<TimeZoneModel>>.Ok(result);
        }
    }
}
