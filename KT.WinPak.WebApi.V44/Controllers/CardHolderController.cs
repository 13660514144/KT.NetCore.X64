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
using KT.WinPak.Service.IServices;
using KT.WinPak.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KT.WinPak.WebApi.Common;
using KT.WinPak.SDK.Settings;
using Microsoft.Extensions.Options;
using KT.Common.Core.Helpers;
using KT.WinPak.WebApi.Common.Helpers;

namespace KT.WinPak.WebApi.Controllers
{
    /// <summary>
    /// 持卡人
    /// </summary>
    [ApiController]
    [TypeFilter(typeof(LoginUserAttribute))]
    [Route("cardHolder")]
    public class CardHolderController : ControllerBase
    {
        private ICardHolderSdkService _cardHolderSdkService;
        private ICardHolderSqlService _cardHolderSqlService;
        private IUserService _userService;
        private ILogger<CardHolderController> _logger;
        private AppSettings _appsettings;

        public CardHolderController(ICardHolderSdkService cardHolderSdkService,
            ICardHolderSqlService cardHolderSqlService,
            IUserService userService,
            ILogger<CardHolderController> logger,
            IOptions<AppSettings> appsettings)
        {
            _cardHolderSdkService = cardHolderSdkService;
            _cardHolderSqlService = cardHolderSqlService;
            _userService = userService;
            _logger = logger;
            _appsettings = appsettings.Value;
        }

        /// <summary>
        /// 获取所有持卡人
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public DataResponse<List<CardHolderModel>> GetAll()
        {
            //var result = _cardHolderSdkService.GetAll();
            var result = _cardHolderSqlService.GetAll();
            return DataResponse<List<CardHolderModel>>.Ok(result);
        }

        /// <summary>
        /// 新增持卡人
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<DataResponse<CardHolderModel>> AddAsync([FromBody] CardHolderModel model)
        {
            model.AccountName = _appsettings.AccountName;
            var result = await RetryHelper.StartAttachAsync(_logger,
                _appsettings.RetryTimes,
                _appsettings.ReloginTimes,
                new Func<DataResponse<CardHolderModel>>(() =>
                {
                    var result = _cardHolderSdkService.Add(model);
                    return DataResponse<CardHolderModel>.Ok(result);
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
        /// 修改持卡人
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<VoidResponse> EditAsync([FromBody] CardHolderModel model)
        {
            model.AccountName = _appsettings.AccountName;
            var result = await RetryHelper.StartAttachAsync(_logger,
                _appsettings.RetryTimes,
                _appsettings.ReloginTimes,
                new Func<VoidResponse>(() =>
                {
                    _cardHolderSdkService.Edit(model.CardHolderID, model);
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

            return result;
        }

        /// <summary>
        /// 删除持卡人
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync([FromBody] CardHolderModel model)
        {
            var result = await RetryHelper.StartAttachAsync(_logger,
                _appsettings.RetryTimes,
                _appsettings.ReloginTimes,
                new Func<VoidResponse>(() =>
                {
                    _cardHolderSdkService.Delete(model.CardHolderID, model.DeleteCardType, model.DeleteImageType);
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

            return result;
        }
    }
}
