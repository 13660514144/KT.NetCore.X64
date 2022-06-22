using KT.Common.WebApi.HttpModel;
using KT.Prowatch.Service.DllModels;
using KT.Prowatch.Service.Models;
using KT.WinPak.SDK.Models;
using ProwatchAPICS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.TestTool.TestApp.HttpApis
{
    public interface IProwatchApi
    {
        Task<CardData> CardDetailAsync(string baseUrl, string token, string cardNumber);
        Task<string> AddPersonCard(string baseUrl, string token, PersonCardModel model);
        Task RemovePersonCardAsync(string baseUrl, string token, RemovePersonCardModel model);
        Task EditCardAsync(string baseUrl, string token, CardData model);
        Task<List<CardData>> GetCardsAsync(string baseUrl, string token);
        Task<List<PersonData>> GetPersonsAsync(string baseUrl, string token);
        Task<List<CompanyData>> GetCompanies(string baseUrl, string token);
        Task<List<BadgeTypeData>> GetBadgeTypes(string baseUrl, string token);
        Task<List<AccessCodeData>> GetAcCodes(string baseUrl, string token, string personId = "", string cardNo = "");
        Task AddAcCodeToCard(string baseUrl, string token, ACCodeToCardModel model);
        Task RemoveAcCodeFromCard(string baseUrl, string token, ACCodeToCardModel model);
        Task<CompanyData> GetCardCompanyAsync(string baseUrl, string token, string cardNo);
        Task<bool> IsExistAcCodeByCardNo(string baseUrl, string token, string cardNo, string acCodeId);
        Task<TokenResponse> LoginAsync(string baseUrl, string token, LoginUserModel model);
        Task<FileInfoModel> GetQrAsync(string baseUrl, string content);
    }
}
