using KT.Common.Core.Utils;
using KT.WinPak.SDK.IServices;
using KT.WinPak.SDK.Queries;
using Microsoft.Extensions.Logging;
using NCIHelperLib;
using Newtonsoft.Json;
using System;
using System.Threading;

namespace KT.WinPak.SDK.Services
{

    public class AllSdkService : IAllSdkService
    {
        private static object locker = new object();

        private ILogger<AllSdkService> _logger;
        private ApplicationClass _app;
        private WPAccountClass _wpAccountClass;
        private CardHolderClass _cardHolderClass;
        private AccessLevelClass _accessLevelClass;
        private CardClass _cardClass;
        private HWDeviceClass _hwDeviceClass;

        public AllSdkService(ILogger<AllSdkService> logger)
        {
            _logger = logger;

            LoadClass();
        }

        public void LoadClass()
        {
            _app = new ApplicationClass();
            _wpAccountClass = new WPAccountClass();
            _cardHolderClass = new CardHolderClass();
            _accessLevelClass = new AccessLevelClass();
            _cardClass = new CardClass();
            _hwDeviceClass = new HWDeviceClass();
        }

        public WPAccountClass GetWPAccountClass()
        {
            lock (locker)
            {
                try
                {
                    return JsonConvert.DeserializeObject<WPAccountClass>(JsonConvert.SerializeObject(_wpAccountClass, JsonUtil.JsonSettings), JsonUtil.JsonSettings);
                }
                catch (Exception ex)
                {
                    _logger.LogError("创建WinPak对象错误：{0} ", ex);
                    //_wpAccountClass = new WPAccountClass();
                    Thread.Sleep(100);
                    return GetWPAccountClass();
                }
            }
        }
        public CardHolderClass GetCardHolderClass()
        {
            lock (locker)
            {
                try
                {
                    return JsonConvert.DeserializeObject<CardHolderClass>(JsonConvert.SerializeObject(_cardHolderClass, JsonUtil.JsonSettings), JsonUtil.JsonSettings);
                }
                catch (Exception ex)
                {
                    _logger.LogError("创建WinPak对象错误：{0} ", ex);
                    //_cardHolderClass = new CardHolderClass();
                    Thread.Sleep(100);
                    return GetCardHolderClass();
                }
            }
        }
        public AccessLevelClass GetAccessLevelClass()
        {
            lock (locker)
            {
                try
                {
                    return JsonConvert.DeserializeObject<AccessLevelClass>(JsonConvert.SerializeObject(_accessLevelClass, JsonUtil.JsonSettings), JsonUtil.JsonSettings);
                }
                catch (Exception ex)
                {
                    _logger.LogError("创建WinPak对象错误：{0} ", ex);
                    //_accessLevelClass = new AccessLevelClass();
                    Thread.Sleep(100);
                    return GetAccessLevelClass();
                }
            }
        }
        public CardClass GetCardClass()
        {
            lock (locker)
            {
                try
                {
                    return JsonConvert.DeserializeObject<CardClass>(JsonConvert.SerializeObject(_cardClass, JsonUtil.JsonSettings), JsonUtil.JsonSettings);
                }
                catch (Exception ex)
                {
                    _logger.LogError("创建WinPak对象错误：{0} ", ex);
                    //_cardClass = new CardClass();
                    Thread.Sleep(100);
                    return GetCardClass();
                }
            }
        }
        public HWDeviceClass GetHWDeviceClass()
        {
            lock (locker)
            {
                try
                {
                    return JsonConvert.DeserializeObject<HWDeviceClass>(JsonConvert.SerializeObject(_hwDeviceClass, JsonUtil.JsonSettings), JsonUtil.JsonSettings);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("创建WinPak对象错误：{0} ", ex);
                    //_hwDeviceClass = new HWDeviceClass();
                    Thread.Sleep(100);
                    return GetHWDeviceClass();
                }
            }
        }




        public LoginQuery Login(LoginQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.Login(query.bstrUserName, query.bstrPassword, query.bstrDomainName, out int plUserID);

                query.plUserID = plUserID;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }

        public GetAccountsQuery GetAccounts(GetAccountsQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAccounts(out object vAccounts);
                _logger.LogInformation("执行结果：name:{0} vAccounts:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vAccounts, JsonUtil.JsonPrintSettings));
                query.vAccounts = vAccounts;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetCardsByAccountNameQuery GetCardsByAccountName(GetCardsByAccountNameQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetCardsByAccountName(query.bstrAcctName, out object vCards);
                _logger.LogInformation("执行结果：name:{0} vCards:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vCards, JsonUtil.JsonPrintSettings));
                query.SetValues(vCards);
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAccessLevelsByAccountNameQuery GetAccessLevelsByAccountName(GetAccessLevelsByAccountNameQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAccessLevelsByAccountName(query.bstrAccountName, out object vAccessLevels);
                _logger.LogInformation("执行结果：name:{0} vAccessLevels:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vAccessLevels, JsonUtil.JsonPrintSettings));
                query.SetValues(vAccessLevels);
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetMaxCardNumberLengthQuery GetMaxCardNumberLength(GetMaxCardNumberLengthQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetMaxCardNumberLength(out int pCardnoLength);

                query.pCardnoLength = pCardnoLength;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAccessLevelTypeQuery GetAccessLevelType(GetAccessLevelTypeQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAccessLevelType(out int pAccLevelType);

                query.pAccLevelType = pAccLevelType;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetCardNumericQuery GetCardNumeric(GetCardNumericQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetCardNumeric(out bool pbCardNumeric);

                query.pbCardNumeric = pbCardNumeric;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public AddCardQuery AddCard(AddCardQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.AddCard(query.pcard, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public DeleteCardQuery DeleteCard(DeleteCardQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.DeleteCard(query.bstrCardno, query.bstrAcctName, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetCardByCardNumberQuery GetCardByCardNumber(GetCardByCardNumberQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetCardbyCardNumber(query.bstrCardno, query.bstrAcctName, out object vcard);
                _logger.LogInformation("执行结果：name:{0} vcard:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vcard, JsonUtil.JsonPrintSettings));
                query.vcard = (ICard)vcard;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }

        public EditCardQuery EditCard(EditCardQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.EditCard(query.bstrCardno, query.bstrAcctName, query.pcard, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public AddAccessLevelQuery AddAccessLevel(AddAccessLevelQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.AddAccessLevel(query.pAccesslevel, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public EditAccessLevelQuery EditAccessLevel(EditAccessLevelQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.EditAccessLevel(query.bstrAccl, query.pAccesslevel, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }

        public DeleteAccessLevelQuery DeleteAccessLevel(DeleteAccessLevelQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.DeleteAccessLevel(query.bstrAcclName, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }

        public DeleteALQuery DeleteAL(DeleteALQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.DeleteAL(query.dwoldAcceslevelID, query.dwnewAcceslevelID, query.bMultiple);

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public IsolateAccessLevelQuery IsolateAccessLevel(IsolateAccessLevelQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.IsolateAccessLevel(query.bstrAcclName, out object vCards, out int pStatus);
                _logger.LogInformation("执行结果：name:{0} vCards:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vCards, JsonUtil.JsonPrintSettings));
                query.vCards = vCards;
                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAccessLevelForReassignQuery GetAccessLevelForReassign(GetAccessLevelForReassignQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAccesslevelsForReassign(query.bstrAccountName, query.bstrExAcclName, out object vAccessLevels);
                _logger.LogInformation("执行结果：name:{0} vAccessLevels:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vAccessLevels, JsonUtil.JsonPrintSettings));
                query.vAccessLevels = vAccessLevels;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public ReassignAccessLevelQuery ReassignAccessLevel(ReassignAccessLevelQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.ReassignAccessLevel(query.bstrAcctName, query.bstrExAcclName, query.bstrNewAcclName, query.vCards, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public ConfigureAccessLevelQuery ConfigureAccessLevel(ConfigureAccessLevelQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.ConfigureAccessLevel(query.bstrAcclName, query.vReaders, query.bstrTZName, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetTimeZonesByAccountNameQuery GetTimeZonesByAccountName(GetTimeZonesByAccountNameQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetTimeZonesByAccountName(query.bstrAcctName, out object vTimezones);
                _logger.LogInformation("执行结果：name:{0} vTimezones:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vTimezones, JsonUtil.JsonPrintSettings));
                query.SetValues(vTimezones);
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        /// <summary>
        /// 
        /// GetConfiguredWPDomains
        /// </summary>

        public GetWPDSNQuery GetWPDSN(GetWPDSNQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetConfiguredWPDomains(out object vDomains);
                _logger.LogInformation("执行结果：name:{0} vDomains:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vDomains, JsonUtil.JsonPrintSettings));
                query.vDomains = vDomains;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetReadersByAccountNameQuery GetReadersByAccountName(GetReadersByAccountNameQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetReadersByAccountName(query.bstrAcctName, out object vReaders);
                _logger.LogInformation("执行结果：name:{0} vReaders:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vReaders, JsonUtil.JsonPrintSettings));
                query.SetValues(vReaders);
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetADVDetailsByAccountNameQuery GetADVDetailsByAccountName(GetADVDetailsByAccountNameQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetADVDetailsByAccountName(query.bstrAcctName, out object vDevices);
                _logger.LogInformation("执行结果：name:{0} vDevices:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vDevices, JsonUtil.JsonPrintSettings));
                query.SetValues(vDevices);
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAcctIDByHIDQuery GetAcctIDByHID(GetAcctIDByHIDQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAcctIDByHID(query.lHID, out int pAcctID);

                query.pAcctID = pAcctID;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAvailableTimeZonesOfReaderQuery GetAvailableTimeZonesOfReader(GetAvailableTimeZonesOfReaderQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAvailableTimeZonesOfReader(query.bstrReaderName, out object vTimezones);
                _logger.LogInformation("执行结果：name:{0} vTimezones:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vTimezones, JsonUtil.JsonPrintSettings));
                query.vTimezones = vTimezones;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAssociatedTimeZoneOfReaderQuery GetAssociatedTimeZoneOfReader(GetAssociatedTimeZoneOfReaderQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAssociatedTimeZoneOfReader(query.bstrAcclName, query.bstrReader, out object vTimezone);
                _logger.LogInformation("执行结果：name:{0} vTimezone:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vTimezone, JsonUtil.JsonPrintSettings));
                query.vTimezone = vTimezone;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAccessAreaBranchesByAccountNameQuery GetAccessAreaBranchesByAccountName(GetAccessAreaBranchesByAccountNameQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAccessAreaBranchesByAccountName(query.bstrAcctName, out object vBranches);
                _logger.LogInformation("执行结果：name:{0} vBranches:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vBranches, JsonUtil.JsonPrintSettings));
                query.vBranches = vBranches;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetReadersInAccessAreaBranchQuery GetReadersInAccessAreaBranch(GetReadersInAccessAreaBranchQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetReadersInAccessAreaBranch(query.bstrAcctName, query.bstrBranchName, out object vReaders);
                _logger.LogInformation("执行结果：name:{0} vReaders:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vReaders, JsonUtil.JsonPrintSettings));
                query.vReaders = vReaders;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAvailableTimezonesOfBranchQuery GetAvailableTimezonesOfBranch(GetAvailableTimezonesOfBranchQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAvailableTimezonesOfBranch(query.bstrAcctName, query.bstrBranchName, out object vTimezones);
                _logger.LogInformation("执行结果：name:{0} vTimezones:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vTimezones, JsonUtil.JsonPrintSettings));
                query.vTimezones = vTimezones;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetCardHoldersByAccountNameQuery GetCardHoldersByAccountName(GetCardHoldersByAccountNameQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetCardHoldersByAccountName(query.bstrAcctName, out object vCardHolders);
                _logger.LogInformation("执行结果：name:{0} vCardHolders:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vCardHolders, JsonUtil.JsonPrintSettings));
                query.SetValues(vCardHolders);
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetNoteFieldTemplateDetailsByAccountQuery GetNoteFieldTemplateDetailsByAccount(GetNoteFieldTemplateDetailsByAccountQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetNoteFieldTemplateDetailsByAccount(query.bstrAcctName, out object vNFTemplates);
                _logger.LogInformation("执行结果：name:{0} vNFTemplates:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vNFTemplates, JsonUtil.JsonPrintSettings));
                query.vNFTemplates = vNFTemplates;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public AddCardHolderQuery AddCardHolder(AddCardHolderQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.AddCardHolder(query.pCH, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public EditCardHolderQuery EditCardHolder(EditCardHolderQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.EditCardHolder(query.lCardholderID, query.pCH, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public DeleteCardHolderQuery DeleteCardHolder(DeleteCardHolderQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.DeleteCardHolder(query.lCHId, query.bDelCard, query.bDelImage, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public LogoutQuery Logout(LogoutQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.Logout();

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAccessLevelByNameQuery GetAccessLevelByName(GetAccessLevelByNameQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAccessLevelByName(query.bstrAcclName, out object VAccessLevel);
                _logger.LogInformation("执行结果：name:{0} VAccessLevel:{1} ", query.GetType().Name, JsonConvert.SerializeObject(VAccessLevel, JsonUtil.JsonPrintSettings));
                query.VAccessLevel = (IAccessLevel)VAccessLevel;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetCardHolderByCardHolderIDQuery GetCardHolderByCardHolderID(GetCardHolderByCardHolderIDQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetCardHolderByCardHolderID(query.lCHId, out object vCardHolder);
                _logger.LogInformation("执行结果：name:{0} vCardHolder:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vCardHolder, JsonUtil.JsonPrintSettings));
                query.vCardHolder = vCardHolder;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAccountNameByAcctIDQuery GetAccountNameByAcctID(GetAccountNameByAcctIDQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAccountNameByAcctID(query.lAccountID, out string pBstrAcctName);

                query.pBstrAcctName = pBstrAcctName;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAccessLevelNameByIDQuery GetAccessLevelNameByID(GetAccessLevelNameByIDQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAccessLevelNameByID(query.lAcclID, out string pBstrAcclName);

                query.pBstrAcclName = pBstrAcclName;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetCardsByCHIDQuery GetCardsByCHID(GetCardsByCHIDQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetCardsByCHID(query.lCHId, out object vCards);
                _logger.LogInformation("执行结果：name:{0} vCards:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vCards, JsonUtil.JsonPrintSettings));
                query.vCards = vCards;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public DeletePhotoQuery DeletePhoto(DeletePhotoQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.DeletePhoto(query.lCHId, query.lIndex, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public DeleteSignatureQuery DeleteSignature(DeleteSignatureQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.DeleteSignature(query.lCHId, query.lIndex, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetCardsWithoutCHIDByAcctIDQuery GetCardsWithoutCHIDByAcctID(GetCardsWithoutCHIDByAcctIDQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetCardsWithoutCHIDByAcctID(query.lAcctID, out object vCards);
                _logger.LogInformation("执行结果：name:{0} vCards:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vCards, JsonUtil.JsonPrintSettings));
                query.vCards = vCards;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAccountByAcctIDQuery GetAccountByAcctID(GetAccountByAcctIDQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAccountByAcctID(query.lAcctID, out object vAccount);
                _logger.LogInformation("执行结果：name:{0} vAccount:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vAccount, JsonUtil.JsonPrintSettings));
                query.vAccount = vAccount;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetPanelsByAcctIDQuery GetPanelsByAcctID(GetPanelsByAcctIDQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetPanelsByAcctID(query.lAcctID, out object vPanels);
                _logger.LogInformation("执行结果：name:{0} vPanels:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vPanels, JsonUtil.JsonPrintSettings));
                query.vPanels = vPanels;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetOutputsByPanelIDQuery GetOutputsByPanelID(GetOutputsByPanelIDQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetOutputsByPanelID(query.lAcctID, query.lPanelID, out object vOutputs);
                _logger.LogInformation("执行结果：name:{0} vOutputs:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vOutputs, JsonUtil.JsonPrintSettings));
                query.vOutputs = vOutputs;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetGroupsByPanelIDQuery GetGroupsByPanelID(GetGroupsByPanelIDQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetGroupsByPanelID(query.lAcctID, query.lPanelID, out object vGroups);
                _logger.LogInformation("执行结果：name:{0} vGroups:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vGroups, JsonUtil.JsonPrintSettings));
                query.vGroups = vGroups;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAvailableTimezonesOfPanelQuery GetAvailableTimezonesOfPanel(GetAvailableTimezonesOfPanelQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAvailableTimezonesOfPanel(query.lAcctID, query.lPanelID, out object vTimezones);
                _logger.LogInformation("执行结果：name:{0} vTimezones:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vTimezones, JsonUtil.JsonPrintSettings));
                query.vTimezones = vTimezones;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public ConfigureOutputTimezoneQuery ConfigureOutputTimezone(ConfigureOutputTimezoneQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.ConfigureOutputTimezone(query.lAcctID, query.lPanelID, query.lOutputID, query.lTimezoneID, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAssociatedTimezoneOfOutputQuery GetAssociatedTimezoneOfOutput(GetAssociatedTimezoneOfOutputQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAssociatedTimezoneOfOutput(query.lAcctID, query.lPanelID, query.lOutputID, out object vTimezone);
                _logger.LogInformation("执行结果：name:{0} vTimezone:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vTimezone, JsonUtil.JsonPrintSettings));
                query.vTimezone = vTimezone;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public ConfigureGroupTimezoneQuery ConfigureGroupTimezone(ConfigureGroupTimezoneQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.ConfigureGroupTimezone(query.lAcctID, query.lPanelID, query.lGroupID, query.lTimezoneID, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAssociatedTimezoneOfGroupQuery GetAssociatedTimezoneOfGroup(GetAssociatedTimezoneOfGroupQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAssociatedTimezoneOfGroup(query.lAcctID, query.lPanelID, query.lGroupID, out object vTimezone);
                _logger.LogInformation("执行结果：name:{0} vTimezone:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vTimezone, JsonUtil.JsonPrintSettings));
                query.vTimezone = vTimezone;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetDeviceNameByHWDeviceIDQuery GetDeviceNameByHWDeviceID(GetDeviceNameByHWDeviceIDQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetDeviceNameByHWDeviceID(query.lHWDeviceID, out string pBstrDeviceName);

                query.pBstrDeviceName = pBstrDeviceName;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetDevNameByDeviceIDQuery GetDevNameByDeviceID(GetDevNameByDeviceIDQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetDevNameByDeviceID(query.lDeviceID, out string pBstrDeviceName);

                query.pBstrDeviceName = pBstrDeviceName;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetTimezoneNameByIDQuery GetTimezoneNameByID(GetTimezoneNameByIDQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetTimezoneNameByID(query.lTmezoneID, out string pBstrTimezone);

                query.pBstrTimezone = pBstrTimezone;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public AddTimezoneQuery AddTimezone(AddTimezoneQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.AddTimezone(query.pTz, out int pTZID, out int pStatus);

                query.pTZID = pTZID;
                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public EditTimeZoneQuery EditTimeZone(EditTimeZoneQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.EditTimeZone(query.bstrExistTimeZoneName, query.bstrAccountName, query.pTimeZone, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public DeleteTimeZoneQuery DeleteTimeZone(DeleteTimeZoneQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.DeleteTimeZone(query.lTimezoneID, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetTimeZoneByNameQuery GetTimeZoneByName(GetTimeZoneByNameQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetTimeZoneByName(query.bstrTZName, out object vTZ);
                _logger.LogInformation("执行结果：name:{0} vTZ:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vTZ, JsonUtil.JsonPrintSettings));
                query.vTZ = vTZ;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public ConfigureTimeZoneRangeQuery ConfigureTimeZoneRange(ConfigureTimeZoneRangeQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.ConfigureTimeZoneRange(query.lAcctID, query.lTimezoneID, query.vTZs, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetTimeZoneRangesByTZIDQuery GetTimeZoneRangesByTZID(GetTimeZoneRangesByTZIDQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetTimeZoneRangesByTZID(query.lTimezoneID, out object vTZRanges);
                _logger.LogInformation("执行结果：name:{0} vTZRanges:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vTZRanges, JsonUtil.JsonPrintSettings));
                query.vTZRanges = vTZRanges;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public DeleteTimeZoneRangeQuery DeleteTimeZoneRange(DeleteTimeZoneRangeQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.DeleteTimeZoneRange(query.lTimezoneID, query.lTZRangeID, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public ConfigurePanelTimeZoneQuery ConfigurePanelTimeZone(ConfigurePanelTimeZoneQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.ConfigurePanelTimeZone(query.lAcctID, query.lPanelID, query.vTimezones, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public IsolateOperatorsForTZReassignQuery IsolateOperatorsForTZReassign(IsolateOperatorsForTZReassignQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.IsolateOperatorsForTZReassign(query.lTimezoneID, out object vOperators, out int pStatus);
                _logger.LogInformation("执行结果：name:{0} vOperators:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vOperators, JsonUtil.JsonPrintSettings));
                query.vOperators = vOperators;
                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public IsolatePanelsForTZDeleteQuery IsolatePanelsForTZDelete(IsolatePanelsForTZDeleteQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.IsolatePanelsForTZDelete(query.lAcctID, query.lTimezoneID, out object vPanels, out int pStatus);
                _logger.LogInformation("执行结果：name:{0} vPanels:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vPanels, JsonUtil.JsonPrintSettings));
                query.vPanels = vPanels;
                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public IsolateAccessLevelsForTZReassignQuery IsolateAccessLevelsForTZReassign(IsolateAccessLevelsForTZReassignQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.IsolateAccessLevelsForTZReassign(query.lAcctID, query.lTimezoneID, out object vAccessLevels, out int pStatus);
                _logger.LogInformation("执行结果：name:{0} vAccessLevels:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vAccessLevels, JsonUtil.JsonPrintSettings));
                query.vAccessLevels = vAccessLevels;
                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public IsolateActionGroupsForTZReassignQuery IsolateActionGroupsForTZReassign(IsolateActionGroupsForTZReassignQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.IsolateActionGroupsForTZReassign(query.lAcctID, query.lTimezoneID, out object vActionGroups, out int pStatus);
                _logger.LogInformation("执行结果：name:{0} vActionGroups:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vActionGroups, JsonUtil.JsonPrintSettings));
                query.vActionGroups = vActionGroups;
                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public IsolateCardsForTZReassignQuery IsolateCardsForTZReassign(IsolateCardsForTZReassignQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.IsolateCardsForTZReassign(query.lAcctID, query.lTimezoneID, out object vCards, out int pStatus);
                _logger.LogInformation("执行结果：name:{0} vCards:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vCards, JsonUtil.JsonPrintSettings));
                query.vCards = vCards;
                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public IsolateADVsForTZReassignQuery IsolateADVsForTZReassign(IsolateADVsForTZReassignQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.IsolateADVsForTZReassign(query.lAcctID, query.lTimezoneID, out object vADVs, out int pStatus);
                _logger.LogInformation("执行结果：name:{0} vADVs:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vADVs, JsonUtil.JsonPrintSettings));
                query.vADVs = vADVs;
                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetTZsForOperatorReassignQuery GetTZsForOperatorReassign(GetTZsForOperatorReassignQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetTZsForOperatorReassign(query.lExistTimeZoneID, out object vTimezones);
                _logger.LogInformation("执行结果：name:{0} vTimezones:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vTimezones, JsonUtil.JsonPrintSettings));
                query.vTimezones = vTimezones;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetTZsForReassignQuery GetTZsForReassign(GetTZsForReassignQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetTZsForReassign(query.lTimezoneID, query.lExistTimeZoneID, out object vTimezones);
                _logger.LogInformation("执行结果：name:{0} vTimezones:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vTimezones, JsonUtil.JsonPrintSettings));
                query.vTimezones = vTimezones;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public DeletePanelTZQuery DeletePanelTZ(DeletePanelTZQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.DeletePanelTZ(query.lAcctID, query.lOldTimeZoneID, query.vPanels, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public ReassignOperatorTZQuery ReassignOperatorTZ(ReassignOperatorTZQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.ReassignOperatorTZ(query.lNewTimeZoneID, query.vOperators, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public ReassignAccessLevelTZQuery ReassignAccessLevelTZ(ReassignAccessLevelTZQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.ReassignAccessLevelTZ(query.lAcctID, query.lOldTimeZoneID, query.lNewTimeZoneID, query.vAccessLevels, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public ReassignActionGroupTZQuery ReassignActionGroupTZ(ReassignActionGroupTZQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.ReassignActionGroupTZ(query.lAcctID, query.lOldTimeZoneID, query.lNewTimeZoneID, query.vActionGroups, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public ReassignCardTZQuery ReassignCardTZ(ReassignCardTZQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.ReassignCardTZ(query.lAcctID, query.lOldTimeZoneID, query.lNewTimeZoneID, query.vCards, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public ReassignADVTZQuery ReassignADVTZ(ReassignADVTZQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.ReassignADVTZ(query.lAcctID, query.lOldTimeZoneID, query.lNewTimeZoneID, query.vADVs, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public AddHolidayQuery AddHoliday(AddHolidayQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.AddHoliday(query.pMasterHoliday, out int pHolID, out int pStatus);

                query.pHolID = pHolID;
                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public EditHolidayQuery EditHoliday(EditHolidayQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.EditHoliday(query.ExHolName, query.pMasterHoliday, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public DeleteHolidayQuery DeleteHoliday(DeleteHolidayQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.DeleteHoliday(query.lHolID, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public AddHolidayGroupQuery AddHolidayGroup(AddHolidayGroupQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.AddHolidayGroup(query.pHolidayGroup, query.vHolidays, query.vMasterHolidays, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public EditHolidayGroupQuery EditHolidayGroup(EditHolidayGroupQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.EditHolidayGroup(query.bstrExistHolGrpName, query.pHolidayGroup, query.vHolidays, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public DeleteHolidayGroupQuery DeleteHolidayGroup(DeleteHolidayGroupQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.DeleteHolidayGroup(query.lHolGrpID, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetHolidayGroupsByAcctIDQuery GetHolidayGroupsByAcctID(GetHolidayGroupsByAcctIDQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetHolidayGroupsByAcctID(query.lAcctID, out object vHolidayGroups);
                _logger.LogInformation("执行结果：name:{0} vHolidayGroups:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vHolidayGroups, JsonUtil.JsonPrintSettings));
                query.vHolidayGroups = vHolidayGroups;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetHolidaysByHolidayGroupIDQuery GetHolidaysByHolidayGroupID(GetHolidaysByHolidayGroupIDQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetHolidaysByHolidayGroupID(query.lHolGrpID, out object vHolidays);
                _logger.LogInformation("执行结果：name:{0} vHolidays:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vHolidays, JsonUtil.JsonPrintSettings));
                query.vHolidays = vHolidays;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetHolidayByIDQuery GetHolidayByID(GetHolidayByIDQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetHolidayByID(query.lHolID, out object vHoliday);
                _logger.LogInformation("执行结果：name:{0} vHoliday:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vHoliday, JsonUtil.JsonPrintSettings));
                query.vHoliday = vHoliday;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public ConfigurePanelHolidayGroupQuery ConfigurePanelHolidayGroup(ConfigurePanelHolidayGroupQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.ConfigurePanelHolidayGroup(query.lAcctID, query.lPanelID, query.vHolidayGroupIDs, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetConfiguredHolidayGroupsByPanelQuery GetConfiguredHolidayGroupsByPanel(GetConfiguredHolidayGroupsByPanelQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetConfiguredHolidayGroupsByPanel(query.lPanelID, out object vHolidayGroups);
                _logger.LogInformation("执行结果：name:{0} vHolidayGroups:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vHolidayGroups, JsonUtil.JsonPrintSettings));
                query.vHolidayGroups = vHolidayGroups;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetDirectPointTZDetailsofReaderQuery GetDirectPointTZDetailsofReader(GetDirectPointTZDetailsofReaderQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetDirectPointTZDetailsofReader(query.lReaderID, out int lDeviceID, out int lTZID);

                query.lDeviceID = lDeviceID;
                query.lTZID = lTZID;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public ConnectWPDatabaseQuery ConnectWPDatabase(ConnectWPDatabaseQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.ConnectWPDatabase(query.bstrUserName, query.bstrPassword, query.bstrDomainName, out int pStatus, query.lUserID);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public DisconnectWPDatabaseQuery DisconnectWPDatabase(DisconnectWPDatabaseQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.DisConnectWPDatabase();

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public DisconnectQuery Disconnect(DisconnectQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.Disconnect();

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public BulkDeleteCardsQuery BulkDeleteCards(BulkDeleteCardsQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.BulkDeleteCards(query.sStartNo, query.sStopNo, query.lAccountID, query.lOperID, query.sOpName);

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public BulkAddCardsQuery BulkAddCards(BulkAddCardsQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.BulkAddCards(query.sStartNo, query.sStopNo, query.lAccountID, query.lCardStatus, query.dtActivationDate, query.dtExpirationDate, query.lOperID, query.sOpName, query.bMultiple, query.alAccessLevelIDs);

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public AddUpdateCardQuery AddUpdateCard(AddUpdateCardQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.AddUpdateCard(query.dwRecordID, query.sCardNo, query.lAccountID, query.lCardStatus, query.lissue, query.lCardholderID, query.PIN1, query.dtActivationDate, query.dtExpirationDate, query.Backdrop1ID, query.Backdrop2ID, query.bMultiple, query.alAccessLevelIDs, query.bTempCard, query.iNXCardType, query.nUsageLimits, query.bLimitedCard);

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public AddUpdateALQuery AddUpdateAL(AddUpdateALQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.AddUpdateAL(query.dwAcceslevelID, query.sName, query.sDesc, query.lAccountID, query.anReaderIDs, query.anReaderTimeZones, query.anReaderGroups);

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetPhotoSizeQuery GetPhotoSize(GetPhotoSizeQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetPhotoSize(query.dwCardHolderID, query.PhotoNo, ref query.pdwFileSize);

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetPhotoQuery GetPhoto(GetPhotoQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetPhoto(query.dwCardHolderID, query.PhotoNo, ref query.pFileData, ref query.pdwFileSize);

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetSigSizeQuery GetSigSize(GetSigSizeQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetSigSize(query.dwCardHolderID, query.SigNo, ref query.pdwFileSize);

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetSigQuery GetSig(GetSigQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetSig(query.dwCardHolderID, query.SigNo, ref query.pFileData, ref query.pdwFileSize);

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public DeleteSigQuery DeleteSig(DeleteSigQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.DeleteSig(query.dwCardHolderID, query.SigNo);

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public ImportPhotoQuery ImportPhoto(ImportPhotoQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.ImportPhoto(query.dwCardHolderID, query.PhotoNo, ref query.pFileData, query.dwFileSize);

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public ImportSigQuery ImportSig(ImportSigQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.ImportSig(query.dwCardHolderID, query.SigNo, ref query.pFileData, query.dwFileSize);

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public IsConnectedQuery IsConnected(IsConnectedQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.IsConnected(ref query.pbConnected);

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public IsGroupCheckedQuery IsGroupChecked(IsGroupCheckedQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.IsGroupChecked(query.AccountID, query.iPanelNo, ref query.pGroupCheck);

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetWPDBServerTZoffsetQuery GetWPDBServerTZoffset(GetWPDBServerTZoffsetQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetWPDBServerTZoffset(ref query.pDBTzoffset);

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetReaderTZDetailsByAccountIdQuery GetReaderTZDetailsByAccountId(GetReaderTZDetailsByAccountIdQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetReaderTZDetailsByAccountId(query.lAccountID);

            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetWPDBServerTZQuery GetWPDBServerTZ(GetWPDBServerTZQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetWPDBServerTZ(out string strTZName, out int iDSTEnabled);

                query.strTZName = strTZName;
                query.iDSTEnabled = iDSTEnabled;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAvailableGroupsofReaderQuery GetAvailableGroupsofReader(GetAvailableGroupsofReaderQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAvailableGroupsofReader(query.bstrReaderName, out object vGroups);
                _logger.LogInformation("执行结果：name:{0} vGroups:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vGroups, JsonUtil.JsonPrintSettings));
                query.vGroups = vGroups;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAssociatedGroupofReaderQuery GetAssociatedGroupofReader(GetAssociatedGroupofReaderQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAssociatedGroupofReader(query.bstrAcclName, query.bstrReader, out int lGroupID);

                query.lGroupID = lGroupID;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public ConfigureEntranceAccessQuery ConfigureEntranceAccess(ConfigureEntranceAccessQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.ConfigureEntranceAccess(query.bstrAcclName, query.bstrReaderName, query.bstrTZName, query.bstrGroupName, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAvailableTimeZonesOfAccessReaderQuery GetAvailableTimeZonesOfAccessReader(GetAvailableTimeZonesOfAccessReaderQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAvailableTimeZonesOfAccessReader(query.bstrAcctName, query.bstrReaderName, out object vTimezones);
                _logger.LogInformation("执行结果：name:{0} vTimezones:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vTimezones, JsonUtil.JsonPrintSettings));
                query.vTimezones = vTimezones;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetCurrentOperatorQuery GetCurrentOperator(GetCurrentOperatorQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetCurrentOperator(out int plOperatorID, out string pbstrOpName);

                query.plOperatorID = plOperatorID;
                query.pbstrOpName = pbstrOpName;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetConfiguredTimezonesByPanelQuery GetConfiguredTimezonesByPanel(GetConfiguredTimezonesByPanelQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetConfiguredTimezonesByPanel(query.lPanelID, out object vTimezones);
                _logger.LogInformation("执行结果：name:{0} vTimezones:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vTimezones, JsonUtil.JsonPrintSettings));
                query.vTimezones = vTimezones;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAllAccessLevelsQuery GetAllAccessLevels(GetAllAccessLevelsQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAllAccessLevels(out object vAccessLevels);
                _logger.LogInformation("执行结果：name:{0} vAccessLevels:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vAccessLevels, JsonUtil.JsonPrintSettings));
                query.vAccessLevels = vAccessLevels;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAllTimezonesQuery GetAllTimezones(GetAllTimezonesQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAllTimezones(out object vTimezones);
                _logger.LogInformation("执行结果：name:{0} vTimezones:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vTimezones, JsonUtil.JsonPrintSettings));
                query.vTimezones = vTimezones;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public CreateAccessLevelQuery CreateAccessLevel(CreateAccessLevelQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.CreateAccessLevel(query.bstrAcclName, query.bstrAcclDesc, query.vAcctlist, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public CreateTimezoneQuery CreateTimezone(CreateTimezoneQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.CreateTimezone(query.bstrTZName, query.bstrTZDesc, query.vAcctlist, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public IsolatePanelsForHGDeleteQuery IsolatePanelsForHGDelete(IsolatePanelsForHGDeleteQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.IsolatePanelsForHGDelete(query.lAcctID, query.lHolGrpID, out object vPanels, out int pStatus);
                _logger.LogInformation("执行结果：name:{0} vPanels:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vPanels, JsonUtil.JsonPrintSettings));
                query.vPanels = vPanels;
                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAccessTreeByNameQuery GetAccessTreeByName(GetAccessTreeByNameQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAccessTreeByName(query.bstrAcclName, out string pbstrAccessTree);

                query.pbstrAccessTree = pbstrAccessTree;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetCardHolderSearchFieldsByAccountNameQuery GetCardHolderSearchFieldsByAccountName(GetCardHolderSearchFieldsByAccountNameQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetCardHolderSearchFieldsByAccountName(query.bstrAcctName, out object vCHSearchFields);
                _logger.LogInformation("执行结果：name:{0} vCHSearchFields:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vCHSearchFields, JsonUtil.JsonPrintSettings));
                query.vCHSearchFields = vCHSearchFields;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetCardHoldersOnSearchQuery GetCardHoldersOnSearch(GetCardHoldersOnSearchQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetCardHoldersOnSearch(query.bstrAcctName, query.vInputSearchFields, query.vFieldData, query.vCompareType, out object vCardHolders);
                _logger.LogInformation("执行结果：name:{0} vCardHolders:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vCardHolders, JsonUtil.JsonPrintSettings));
                query.vCardHolders = vCardHolders;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetConfiguredWPDomainsQuery GetConfiguredWPDomains(GetConfiguredWPDomainsQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetConfiguredWPDomains(out object vDomains);
                _logger.LogInformation("执行结果：name:{0} vDomains:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vDomains, JsonUtil.JsonPrintSettings));
                query.vDomains = vDomains;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public ConfigureOutputTimezoneExQuery ConfigureOutputTimezoneEx(ConfigureOutputTimezoneExQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.ConfigureOutputTimezoneEx(query.lAcctID, query.lPanelID, query.lOutputID, query.iLockUnlock, query.lTimezoneID, out int pStatus);

                query.pStatus = pStatus;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }
        public GetAssociatedTimezoneOfOutputExQuery GetAssociatedTimezoneOfOutputEx(GetAssociatedTimezoneOfOutputExQuery query)
        {
            _logger.LogInformation("开始执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            lock (locker)
            {
                _app.GetAssociatedTimezoneOfOutputEx(query.lAcctID, query.lPanelID, query.lOutputID, query.iLockUnlock, out object vTimezone);
                _logger.LogInformation("执行结果：name:{0} vTimezone:{1} ", query.GetType().Name, JsonConvert.SerializeObject(vTimezone, JsonUtil.JsonPrintSettings));
                query.vTimezone = vTimezone;
            }
            _logger.LogInformation("结束执行操作操作：name:{0} data:{1} ", query.GetType().Name, JsonConvert.SerializeObject(query, JsonUtil.JsonPrintSettings));
            return query;
        }

    }
}
