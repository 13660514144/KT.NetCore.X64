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
using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KT.TestTool.TestApp.Apis
{
    public class ProwatchWebApi : BackendApiBase, IProwatchApi
    {
        public ProwatchWebApi(ILogger<ProwatchWebApi> logger)
            : base(logger)
        {
        }

        public async Task<CardData> CardDetailAsync(string baseUrl, string token, string cardNo)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = $"{ baseUrl }/queryCards?field=CARDNO&oper={"=".UrlEncode()}&value={cardNo}";
            var results = await base.GetAsync<List<CardData>>(url, tokenHeader);
            return results?.FirstOrDefault();
        }

        public async Task<string> AddPersonCard(string baseUrl, string token, PersonCardModel model)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = baseUrl + "/addPersonCard";
            var result = await base.PostAsync<string>(url, model, tokenHeader);
            return result;
        }

        public async Task RemovePersonCardAsync(string baseUrl, string token, RemovePersonCardModel model)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = baseUrl + "/removePersonCard";
            await base.PostAsync(url, model, tokenHeader);
        }

        public async Task EditCardAsync(string baseUrl, string token, CardData model)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = baseUrl + "/updateCard";
            await base.PostAsync(url, model, tokenHeader);
        }

        public async Task<List<CardData>> GetCardsAsync(string baseUrl, string token)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = baseUrl + "/getAllCards";
            var result = await base.GetAsync<List<CardData>>(url, tokenHeader);
            return result;
        }

        public async Task<List<PersonData>> GetPersonsAsync(string baseUrl, string token)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = baseUrl + "/getAllPersons";
            var result = await base.GetAsync<List<PersonData>>(url, tokenHeader);
            return result;
        }

        public async Task<List<CompanyData>> GetCompanies(string baseUrl, string token)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = baseUrl + "/getAllCompanys?cardNo=";
            var result = await base.GetAsync<List<CompanyData>>(url, tokenHeader);
            return result;
        }

        public async Task<List<BadgeTypeData>> GetBadgeTypes(string baseUrl, string token)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = baseUrl + "/getAllBadgeTypes?personId=";
            var result = await base.GetAsync<List<BadgeTypeData>>(url, tokenHeader);
            return result;
        }

        public async Task<List<AccessCodeData>> GetAcCodes(string baseUrl, string token, string personId = "", string cardNo = "")
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = $"{baseUrl}/getAllACCodes?personId={personId}&cardNo={cardNo}";
            var result = await base.GetAsync<List<AccessCodeData>>(url, tokenHeader);
            return result;
        }

        public async Task AddAcCodeToCard(string baseUrl, string token, ACCodeToCardModel model)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = $"{baseUrl}/addACCodeToCard";
            await base.PostAsync(url, model, tokenHeader);
        }

        public async Task RemoveAcCodeFromCard(string baseUrl, string token, ACCodeToCardModel model)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = $"{baseUrl}/removeACCodeFromCard";
            await base.PostAsync(url, model, tokenHeader);
        }

        public async Task<CompanyData> GetCardCompanyAsync(string baseUrl, string token, string cardNo)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = baseUrl + "/getAllCompanys?cardNo=" + cardNo;
            var results = await base.GetAsync<List<CompanyData>>(url, tokenHeader);
            return results?.FirstOrDefault();
        }

        public async Task<bool> IsExistAcCodeByCardNo(string baseUrl, string token, string cardNo, string acCodeId)
        {
            var data = await GetAcCodes(baseUrl, token, cardNo: cardNo.IsNull());
            if (data != null && data.FirstOrDefault(x => x.Id == acCodeId) != null)
            {
                return true;
            }
            return false;
        }

        public async Task<TokenResponse> LoginAsync(string baseUrl, string token, LoginUserModel model)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = $"{baseUrl}/init";
            var result = await base.PostAsync<TokenResponse>(url, model, tokenHeader);
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
