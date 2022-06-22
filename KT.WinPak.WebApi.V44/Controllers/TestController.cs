using KT.Common.WebApi.HttpApi;
using KT.Common.Core.Utils;
using KT.WinPak.Data.Models;
using KT.WinPak.SDK;
using KT.WinPak.SDK.IServices;
using KT.WinPak.SDK.Models;
using KT.WinPak.SDK.Queries;
using KT.WinPak.SDK.Services;
using KT.WinPak.Service.IServices;
using KT.WinPak.Service.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace KT.WinPak.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private ILogger<TestController> _logger;
        private IAllSdkService _allSdkService;
        private ICardSdkService _cardSdkService;
        private ICardHolderSdkService _cardHolderSdkService;
        public TestController(ILogger<TestController> logger, IAllSdkService allSdkService, ICardSdkService cardSdkService, ICardHolderSdkService cardHolderSdkService)
        {
            _logger = logger;
            _allSdkService = allSdkService;
            _cardSdkService = cardSdkService;
            _cardHolderSdkService = cardHolderSdkService;
        }

        [HttpGet]
        public string Index()
        {
            return DateTimeUtil.UtcNowMillis().ToString();
        }

        ///// <summary>
        ///// 获取所有卡
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("deleteAllCard")]
        //public VoidResponse DeleteAllCard()
        //{
        //    var results = _cardSdkService.GetAll();
        //    foreach (var item in results)
        //    {
        //        try
        //        {
        //            _cardSdkService.Delete(item.CardNumber, item.AccountName);
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError("删除所有卡：", ex);
        //        }
        //    }
        //    return VoidResponse.Ok();
        //}

        ///// <summary>
        ///// 获取所有卡
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("deleteAllHolder")]
        //public VoidResponse DeleteAllHolder()
        //{
        //    var results = _cardHolderSdkService.GetAll();
        //    foreach (var item in results)
        //    {
        //        try
        //        {
        //            _cardHolderSdkService.Delete(item.CardHolderID, 1, 1);
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError("删除所有卡：", ex);
        //        }
        //    }
        //    return VoidResponse.Ok();
        //}

        ///// <summary>
        ///// 获取所有卡
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("deleteCardAndHolders")]
        //public VoidResponse DeleteCardAndHolders(int start, int end)
        //{
        //    _cardSdkService.DeleteCardAndHolders(start, end);
        //    return VoidResponse.Ok();
        //}

    }
}