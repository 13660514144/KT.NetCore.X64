using KT.Common.WebApi.HttpApi;
using KT.Proxy.BackendApi.Apis;
using KT.WinPak.SDK.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.TestTool.TestApp.Apis
{
    public class WinPakApi : BackendApiBase
    {
        public WinPakApi(ILogger<WinPakApi> logger) : base(logger)
        {

        }
        public async Task<CardAndCardHolderModel> AddCardAndCardHolderAsync(string baseUrl, string token, CardAndCardHolderModel model)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = baseUrl + "/card/addWithCardHolder";
            return await base.PostAsync<CardAndCardHolderModel>(url, model, tokenHeader);
        }

        public async Task DeleteWithCardHolderAsync(string baseUrl, string token, CardAndCardHolderModel model)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = baseUrl + "/card/deleteWithCardHolder";
            await base.PostAsync(url, model, tokenHeader);
        }

        public async Task<List<AccessLevelModel>> GetAccessLevelsAsync(string baseUrl, string token)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = baseUrl + "/accessLevel/all";
            return await base.GetAsync<List<AccessLevelModel>>(url, tokenHeader);
        }

        public async Task<List<CardModel>> GetCardsAsync(string baseUrl, string token)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = baseUrl + "/card/all";
            return await base.GetAsync<List<CardModel>>(url, tokenHeader);
        }

        public async Task<List<CardHolderModel>> GetCardHoldersAsync(string baseUrl, string token)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = baseUrl + "/cardHolder/all";
            return await base.GetAsync<List<CardHolderModel>>(url, tokenHeader);
        }

        public async Task<List<HWDeviceModel>> GetReadersAsync(string baseUrl, string token)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = baseUrl + "/hwDevice/readers";
            return await base.GetAsync<List<HWDeviceModel>>(url, tokenHeader);
        }

        public async Task<List<TimeZoneModel>> GetTimeZonesAsync(string baseUrl, string token)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = baseUrl + "/zoneTime/all";
            return await base.GetAsync<List<TimeZoneModel>>(url, tokenHeader);
        }

        public async Task EditCardAsync(string baseUrl, string token, CardModel model)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = baseUrl + "/card/edit";
            await base.PostAsync(url, model, tokenHeader);
        }
        public async Task<CardModel> CardDetailAsync(string baseUrl, string token, string cardNumber)
        {
            var tokenHeader = new Dictionary<string, string>();
            tokenHeader.Add("token", token);
            var url = baseUrl + "/card/detail?cardNumber=" + cardNumber;
            return await base.GetAsync<CardModel>(url, tokenHeader);
        }
    }
}
