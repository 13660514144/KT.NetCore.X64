using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpApi;
using KT.Common.WebApi.HttpModel;
using KT.Prowatch.Service.DllModels;
using KT.Prowatch.Service.Helpers;
using KT.Prowatch.Service.IServices;
using KT.Prowatch.Service.Models;
using KT.Proxy.BackendApi.Apis;
using KT.TestTool.TestApp.HttpApis;
using KT.WinPak.SDK.Models;
using Microsoft.Extensions.Logging;
using ProwatchAPICS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.TestTool.TestApp.Apis
{
    public class ProwatchSdkApi : BackendApiBase, IProwatchApi
    {
        private IProwatchService _prowatchService;
        private ILoginUserService _loginUserService;

        public ProwatchSdkApi(ILogger<ProwatchSdkApi> logger,
            IProwatchService prowatchService,
            ILoginUserService loginUserService)
            : base(logger)
        {
            _prowatchService = prowatchService;
            _loginUserService = loginUserService;
        }

        public async Task<CardData> CardDetailAsync(string baseUrl, string token, string cardNo)
        {
            CardData card = _prowatchService.CardDetail(cardNo);
            return card;
        }

        public async Task<string> AddPersonCard(string baseUrl, string token, PersonCardModel model)
        {
            var result = _prowatchService.AddPersonCard(model);
            return result?.Person?.Id;
        }

        public async Task RemovePersonCardAsync(string baseUrl, string token, RemovePersonCardModel model)
        {
            _prowatchService.RemovePersonCard(model);
        }

        public async Task EditCardAsync(string baseUrl, string token, CardData model)
        {
            //要小写，否则返回正确但数据没有更改
            model.PersonId = model.PersonId.IsNull().ToLower();
            model.CompanyId = model.CompanyId.IsNull().ToLower();

            bool result = ApiHelper.PWApi.UpdateCard(model.ToSPA(), model.CompanyId);
            if (!result)
            {
                throw CustomException.Run("修改卡失败！");
            }
        }

        public async Task<List<CardData>> GetCardsAsync(string baseUrl, string token)
        {
            List<sPA_Card> data = new List<sPA_Card>();
            bool result = ApiHelper.PWApi.GetAllCards(string.Empty, ref data);
            if (!result)
            {
                throw CustomException.Run("获取所有卡失败！");
            }
            return data.ToModels();
        }

        public async Task<List<PersonData>> GetPersonsAsync(string baseUrl, string token)
        {
            List<sPA_Person> data = new List<sPA_Person>();
            bool result = ApiHelper.PWApi.GetAllPersons(ref data);
            if (!result)
            {
                throw CustomException.Run("获取所有人员失败！");
            }
            return data.ToModels();
        }

        public async Task<List<CompanyData>> GetCompanies(string baseUrl, string token)
        {
            List<sPA_Company> data = new List<sPA_Company>();
            bool result = ApiHelper.PWApi.GetAllCompanys(string.Empty, ref data);
            if (!result)
            {
                throw CustomException.Run("获取所有公司失败！");
            }
            return data.ToModels();
        }

        public async Task<List<BadgeTypeData>> GetBadgeTypes(string baseUrl, string token)
        {
            List<sPA_BadgeType> data = new List<sPA_BadgeType>();
            bool result = ApiHelper.PWApi.GetAllBadgeTypes(string.Empty, ref data);
            if (!result)
            {
                throw CustomException.Run("获取所有卡类型失败！");
            }
            return data.ToModels();
        }

        public async Task<List<AccessCodeData>> GetAcCodes(string baseUrl, string token, string personId, string cardNo)
        {
            List<sPA_AccessCode> data = new List<sPA_AccessCode>();
            bool result = ApiHelper.PWApi.GetAllACCodes(personId, cardNo, ref data);
            if (!result)
            {
                throw CustomException.Run("获取所有访问码失败！");
            }
            return data.ToModels();
        }

        public async Task AddAcCodeToCard(string baseUrl, string token, ACCodeToCardModel model)
        {
            bool result = ApiHelper.PWApi.AddACCodeToCard(model?.ACCodeId.IsNull(), model?.CardNo.IsNull());
            if (!result)
            {
                throw CustomException.Run("向卡中添加访问码失败！");
            }
        }

        public async Task RemoveAcCodeFromCard(string baseUrl, string token, ACCodeToCardModel model)
        {
            bool result = ApiHelper.PWApi.RemoveACCodeFromCard(model?.ACCodeId.IsNull(), model?.CardNo.IsNull());
            if (!result)
            {
                throw CustomException.Run("移除卡中的访问码失败！");
            }
        }

        public async Task<CompanyData> GetCardCompanyAsync(string baseUrl, string token, string cardNo)
        {
            List<sPA_Company> data = new List<sPA_Company>();
            bool result = ApiHelper.PWApi.GetAllCompanys(cardNo.IsNull(), ref data);
            if (!result)
            {
                throw CustomException.Run("获取卡公司失败！");
            }
            var company = data?.FirstOrDefault()?.ToModel();
            return company;
        }

        public async Task<bool> IsExistAcCodeByCardNo(string baseUrl, string token, string cardNo, string acCodeId)
        {
            return _prowatchService.IsExistAcCodeByCardNo(cardNo, acCodeId);
        }

        public async Task<TokenResponse> LoginAsync(string baseUrl, string token, LoginUserModel model)
        {
            var result = await _loginUserService.LoginAsync(model);
            return result;
        }

        public async Task<FileInfoModel> GetQrAsync(string baseUrl, string content)
        {
            content = StringUtil.UrlEncode(content);
            var result = await GetFileAsync($"{baseUrl}/front/qr/create?content={content}");
            return result;
        }




        //public async Task EditCardAsync(string baseUrl, string token, CardModel model)
        //{
        //    var tokenHeader = new Dictionary<string, string>();
        //    tokenHeader.Add("token", token);
        //    var url = baseUrl + "/card/edit";
        //    await base.PostAsync(url, model, tokenHeader);
        //}

        //public async Task<CardModel> CardDetailAsync(string baseUrl, string token, string cardNumber)
        //{
        //    var tokenHeader = new Dictionary<string, string>();
        //    tokenHeader.Add("token", token);
        //    var url = baseUrl + "/card/detail?cardNumber=" + cardNumber;
        //    return await base.GetAsync<CardModel>(url, tokenHeader);
        //}
    }
}
