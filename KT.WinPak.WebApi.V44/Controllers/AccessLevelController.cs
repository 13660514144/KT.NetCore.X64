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
using KT.WinPak.Service.IServices;
using KT.WinPak.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KT.WinPak.WebApi.Common;
using KT.Common.Core.Helpers;
using Microsoft.Extensions.Options;
using KT.WinPak.SDK.Settings;
using KT.WinPak.WebApi.Common.Helpers;

namespace KT.WinPak.WebApi.Controllers
{
    [ApiController]
    [Route("accessLevel")]
    [TypeFilter(typeof(LoginUserAttribute))]
    public class AccessLevelController : ControllerBase
    {
        private IAccessLevelSdkService _accessLevelSdkService;
        private IAccessLevelSqlService _accessLevelSqlService;
        private IUserService _userService;
        private ILogger<AccessLevelController> _logger;
        private AppSettings _appsettings;

        public AccessLevelController(IAccessLevelSdkService accessLevelSdkService,
            IAccessLevelSqlService accessLevelSqlService,
            IUserService userService,
            ILogger<AccessLevelController> logger,
            IOptions<AppSettings> appsettings)
        {
            _accessLevelSdkService = accessLevelSdkService;
            _accessLevelSqlService = accessLevelSqlService;
            _userService = userService;
            _logger = logger;
            _appsettings = appsettings.Value;
        }

        /// <summary>
        /// 获取访问级别
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public DataResponse<List<AccessLevelModel>> GetAll()
        {
            //var results = _accessLevelSdkService.GetAll();
            var results = _accessLevelSqlService.GetAll();
            return DataResponse<List<AccessLevelModel>>.Ok(results);
        }

        /// <summary>
        /// 添加门禁级别
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<DataResponse<AccessLevelModel>> AddAsync([FromBody] AccessLevelModel model)
        {
            model.AccountName = _appsettings.AccountName;
            return await RetryHelper.StartAttachAsync(_logger,
                _appsettings.RetryTimes,
                _appsettings.ReloginTimes,
                new Func<DataResponse<AccessLevelModel>>(() =>
                {
                    var result = _accessLevelSdkService.Add(model);
                    return DataResponse<AccessLevelModel>.Ok(result);
                }),
                new Action(() =>
                {
                    _userService.ReloadAndLoginApp();
                }),
                new Func<Exception, bool>((ex) =>
                {
                    return ErrorHelper.IsRpcError(ex);
                }));
        }

        /// <summary>
        /// 修改门禁级别
        /// </summary>
        /// <param name="bstrAccl"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<VoidResponse> EditAsync([FromBody] AccessLevelModel model)
        {
            model.AccountName = _appsettings.AccountName;
            return await RetryHelper.StartAttachAsync(_logger,
                _appsettings.RetryTimes,
                _appsettings.ReloginTimes,
                new Func<VoidResponse>(() =>
                {
                    _accessLevelSdkService.Edit(model.AccessLevelOldName, model);
                    return VoidResponse.Ok();
                }),
                new Action(() =>
                {
                    _userService.ReloadAndLoginApp();
                }),
                new Func<Exception, bool>((ex) =>
                {
                    return ErrorHelper.IsRpcError(ex);
                }));
        }

        /// <summary>
        /// 添加门禁级别
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync([FromBody] AccessLevelModel model)
        {
            return await RetryHelper.StartAttachAsync(_logger,
                _appsettings.RetryTimes,
                _appsettings.ReloginTimes,
                new Func<VoidResponse>(() =>
                {
                    _accessLevelSdkService.Delete(model.AccessLevelName);
                    return VoidResponse.Ok();
                }),
                new Action(() =>
                {
                    _userService.ReloadAndLoginApp();
                }),
                new Func<Exception, bool>((ex) =>
                {
                    return ErrorHelper.IsRpcError(ex);
                }));
        }

        /// <summary>
        /// 查询门禁级别
        /// </summary>
        /// <param name="accessLevelName"></param>
        /// <returns></returns>
        [HttpGet("detail")]
        public async Task<DataResponse<AccessLevelModel>> GetByNameAsync([FromQuery]string accessLevelName)
        {
            return await RetryHelper.StartAttachAsync(_logger,
                _appsettings.RetryTimes,
                _appsettings.ReloginTimes,
                new Func<DataResponse<AccessLevelModel>>(() =>
                {
                    var result = _accessLevelSdkService.GetByName(accessLevelName);
                    return DataResponse<AccessLevelModel>.Ok(result);
                }),
                new Action(() =>
                {
                    _userService.ReloadAndLoginApp();
                }),
                new Func<Exception, bool>((ex) =>
                {
                    return ErrorHelper.IsRpcError(ex);
                }));
        }

        /// <summary>
        /// 配置门禁与读卡器关系映射
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("configure")]
        public async Task<VoidResponse> ConfigureAsync([FromBody]ConfigureAccessLevelModel model)
        {
            return await RetryHelper.StartAttachAsync(_logger,
                _appsettings.RetryTimes,
                _appsettings.ReloginTimes,
                new Func<VoidResponse>(() =>
                {
                    var result = _accessLevelSdkService.Configure(model);
                    return VoidResponse.Ok();
                }),
                new Action(() =>
                {
                    _userService.ReloadAndLoginApp();
                }),
                new Func<Exception, bool>((ex) =>
                {
                    return ErrorHelper.IsRpcError(ex);
                }));
        }

        /// <summary>
        /// 配置门禁与读卡器关系映射
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("configureEntrance")]
        public async Task<VoidResponse> configureEntranceAsync([FromBody]ConfigureEntranceModel model)
        {
            return await RetryHelper.StartAttachAsync(_logger,
                _appsettings.RetryTimes,
                _appsettings.ReloginTimes,
                new Func<VoidResponse>(() =>
                {
                    var result = _accessLevelSdkService.ConfigureEntranceAccess(model);
                    return VoidResponse.Ok();
                }),
                new Action(() =>
                {
                    _userService.ReloadAndLoginApp();
                }),
                new Func<Exception, bool>((ex) =>
                {
                    return ErrorHelper.IsRpcError(ex);
                }));
        }

    }
}
