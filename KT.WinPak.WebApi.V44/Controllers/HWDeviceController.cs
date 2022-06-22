using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KT.Common.WebApi.HttpApi;
using KT.Common.Core.Utils;
using KT.WinPak.WebApi.Common.Filters;
using KT.WinPak.SDK;
using KT.WinPak.SDK.IServices;
using KT.WinPak.SDK.Models;
using KT.WinPak.SDK.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KT.WinPak.WebApi.Common;
using KT.WinPak.SDK.Settings;
using KT.WinPak.Service.IServices;
using Microsoft.Extensions.Options;
using KT.Common.Core.Helpers;
using KT.WinPak.WebApi.Common.Helpers;

namespace KT.WinPak.WebApi.Controllers
{
    /// <summary>
    /// 设备
    /// </summary>
    [ApiController]
    [Route("hwDevice")]
    [TypeFilter(typeof(LoginUserAttribute))]
    public class HWDeviceController : ControllerBase
    {
        private IHWDeviceSdkService _hWDeviceSdkService;
        private IHWDeviceSqlService _hWDeviceSqlService;
        private IUserService _userService;
        private ILogger<HWDeviceController> _logger;
        private AppSettings _appsettings;

        public HWDeviceController(IHWDeviceSdkService hWDeviceSdkService,
            IHWDeviceSqlService hWDeviceSqlService,
            IUserService userService,
            ILogger<HWDeviceController> logger,
            IOptions<AppSettings> appsettings)
        {
            _hWDeviceSdkService = hWDeviceSdkService;
            _hWDeviceSqlService = hWDeviceSqlService;
            _userService = userService;
            _logger = logger;
            _appsettings = appsettings.Value;
        }

        /// <summary>
        /// 获取设置列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<DataResponse<List<HWDeviceModel>>> GetAllAsync()
        {
            var result = await RetryHelper.StartAttachAsync(_logger,
                _appsettings.RetryTimes,
                _appsettings.ReloginTimes,
                new Func<DataResponse<List<HWDeviceModel>>>(() =>
                {
                    var result = _hWDeviceSdkService.GetAll();
                    return DataResponse<List<HWDeviceModel>>.Ok(result);
                }),
                new Action(() =>
                {
                    _userService.ReloadAndLoginApp();
                }),
                new Func<Exception, bool>((ex) =>
                {
                    return ErrorHelper.IsRpcError(ex);
                }));

            return result;
        }

        /// <summary>
        /// 获取读卡器列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("readers")]
        public DataResponse<List<HWDeviceModel>> GetAllReaders()
        {
            //var result = _hWDeviceSdkService.GetAllReaders();
            var result = _hWDeviceSqlService.GetAllReaders();
            return DataResponse<List<HWDeviceModel>>.Ok(result);
        }
    }
}
