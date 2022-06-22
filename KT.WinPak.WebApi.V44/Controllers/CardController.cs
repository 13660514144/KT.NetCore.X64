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
using KT.Common.Core.Exceptions;
using KT.WinPak.WebApi.Common;
using KT.Common.Core.Helpers;
using KT.WinPak.SDK.Settings;
using Microsoft.Extensions.Options;
using KT.WinPak.WebApi.Common.Helpers;

namespace KT.WinPak.WebApi.Controllers
{
    [ApiController]
    [Route("card")]
    [TypeFilter(typeof(LoginUserAttribute))]
    public class CardController : ControllerBase
    {
        private ICardSdkService _cardSdkService;
        private ICardSqlService _cardSqlService;
        private IUserService _userService;
        private ILogger<CardController> _logger;
        private AppSettings _appsettings;

        public CardController(ICardSdkService cardSdkService,
            ICardSqlService cardSqlService,
            IUserService userService,
            ILogger<CardController> logger,
            IOptions<AppSettings> appsettings)
        {
            _cardSdkService = cardSdkService;
            _cardSqlService = cardSqlService;
            _userService = userService;
            _logger = logger;
            _appsettings = appsettings.Value;
        }

        /// <summary>
        /// 获取所有卡
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public DataResponse<List<CardModel>> GetAll()
        {
            //var result = _cardSdkService.GetAll();
            var result = _cardSqlService.GetAll();
            return DataResponse<List<CardModel>>.Ok(result);
        }

        /// <summary>
        /// 修改卡
        /// </summary>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<DataResponse<CardModel>> AddAsync([FromBody] CardModel model)
        {
            model.AccountName = _appsettings.AccountName;
            return await RetryHelper.StartAttachAsync(_logger,
                _appsettings.RetryTimes,
                _appsettings.ReloginTimes,
                new Func<DataResponse<CardModel>>(() =>
                {
                    var result = _cardSdkService.Add(model);
                    return DataResponse<CardModel>.Ok(result);
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
        /// 修改卡
        /// </summary>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<VoidResponse> EditAsync([FromBody] CardModel model)
        {
            model.AccountName = _appsettings.AccountName;
            return await RetryHelper.StartAttachAsync(_logger,
                _appsettings.RetryTimes,
                _appsettings.ReloginTimes,
                new Func<VoidResponse>(() =>
                {
                    _cardSdkService.Edit(model.CardNumber, model.AccountName, model);
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
        /// 删除卡
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete")]
        public async Task<VoidResponse> DeleteAsync([FromBody] CardModel model)
        {
            model.AccountName = _appsettings.AccountName;
            return await RetryHelper.StartAttachAsync(_logger,
               _appsettings.RetryTimes,
               _appsettings.ReloginTimes,
               new Func<VoidResponse>(() =>
               {
                   _cardSdkService.Delete(model.CardNumber, model.AccountName);
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
        /// 新增持卡人与卡
        /// </summary>
        /// <returns></returns>
        [HttpPost("addWithCardHolder")]
        public async Task<DataResponse<CardAndCardHolderModel>> AddWithCardHolderAsync([FromBody] CardAndCardHolderModel model)
        {
            model.Card.AccountName = _appsettings.AccountName;
            model.CardHolder.AccountName = _appsettings.AccountName;
            var result = await RetryHelper.StartAttachAsync(_logger,
               _appsettings.RetryTimes,
               _appsettings.ReloginTimes,
               new Func<CardAndCardHolderModel>(() =>
               {
                   return _cardSdkService.AddWithCardHolder(model);
               }),
               new Action(() =>
               {
                   _userService.ReloadAndLoginApp();
               }),
                new Func<Exception, bool>((ex) =>
                {
                    return ErrorHelper.IsRpcError(ex);
                }));

            if (!result.CardHolder.IsOperateSuccess || !result.Card.IsOperateSuccess)
            {
                throw CustomException.Run(result.CardHolder.OperateMessage + result.Card.OperateMessage);
            }
            return DataResponse<CardAndCardHolderModel>.Ok(result);
        }

        /// <summary>
        /// 删除持卡人与卡
        /// </summary>
        /// <returns></returns>
        [HttpPost("deleteWithCardHolder")]
        public async Task<DataResponse<CardAndCardHolderModel>> DeleteWithCardHolderAsync([FromBody] CardAndCardHolderModel model)
        {
            model.Card.AccountName = _appsettings.AccountName;
            model.CardHolder.AccountName = _appsettings.AccountName;
            var result =await RetryHelper.StartAttachAsync(_logger,
               _appsettings.RetryTimes,
               _appsettings.ReloginTimes,
               new Func<CardAndCardHolderModel>(() =>
               {
                   return _cardSdkService.DeleteWithCardHolder(model);
               }),
               new Action(() =>
               {
                   _userService.ReloadAndLoginApp();
               }),
                new Func<Exception, bool>((ex) =>
                {
                    return ErrorHelper.IsRpcError(ex);
                }));

            if (!result.Card.IsOperateSuccess || !result.CardHolder.IsOperateSuccess)
            {
                throw CustomException.Run(result.CardHolder.OperateMessage + result.Card.OperateMessage);
            }
            return DataResponse<CardAndCardHolderModel>.Ok(result);
        }

        /// <summary>
        /// 批量修改持卡人与卡
        /// </summary>
        /// <returns></returns>
        [HttpPost("addWithCardHolders")]
        public async Task<DataResponse<List<CardAndCardHolderModel>>> AddWithCardHoldersAsync([FromBody] List<CardAndCardHolderModel> models)
        {
            var result = await RetryHelper.StartAttachAsync(_logger,
               _appsettings.RetryTimes,
               _appsettings.ReloginTimes,
               new Func<DataResponse<List<CardAndCardHolderModel>>>(() =>
               {
                   var result = _cardSdkService.AddWithCardHolders(_appsettings.AccountName, models);
                   return DataResponse<List<CardAndCardHolderModel>>.Ok(result);
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
        /// 批量删除持卡人与卡
        /// </summary>
        /// <returns></returns>
        [HttpPost("deleteWithCardHolders")]
        public async Task<DataResponse<List<CardAndCardHolderModel>>> DeleteWithCardHoldersAsync([FromBody] List<CardAndCardHolderModel> models)
        {
            var result =await RetryHelper.StartAttachAsync(_logger,
               _appsettings.RetryTimes,
               _appsettings.ReloginTimes,
               new Func<DataResponse<List<CardAndCardHolderModel>>>(() =>
               {
                   var result = _cardSdkService.DeleteWithCardHolders(_appsettings.AccountName, models);
                   return DataResponse<List<CardAndCardHolderModel>>.Ok(result);
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
        /// 根据卡号查询卡信息
        /// </summary>
        /// <returns>卡信息</returns>
        [HttpGet("detail")]
        public async Task<DataResponse<CardModel>> GetByCardNumberAsync([FromQuery] string cardNumber)
        {
            var result =await RetryHelper.StartAttachAsync(_logger,
               _appsettings.RetryTimes,
               _appsettings.ReloginTimes,
               new Func<DataResponse<CardModel>>(() =>
               {
                   var result = _cardSdkService.GetCardByCardNumber(cardNumber, _appsettings.AccountName);
                   return DataResponse<CardModel>.Ok(result);
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
