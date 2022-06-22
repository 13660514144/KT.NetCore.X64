using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.WebApi.HttpModel;
using KT.Common.WpfApp.Utils;
using KT.Common.WpfApp.ViewModels;
using KT.Prowatch.Service.DllModels;
using KT.Prowatch.Service.Enums;
using KT.Prowatch.Service.Helpers;
using KT.Prowatch.Service.Models;
using KT.TestTool.TestApp.Apis;
using KT.TestTool.TestApp.Common.JsonData;
using KT.TestTool.TestApp.Helpers;
using KT.TestTool.TestApp.HttpApis;
using KT.TestTool.TestApp.Models;
using KT.TestTool.TestApp.Settings;
using KT.TestTool.TestApp.ViewModels;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.Tools.Printer.Models;
using KT.WinPak.SDK.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProwatchAPICS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;

namespace KT.TestTool.TestApp.Views.Prowatch
{
    public class ProwatchTestControlViewModel : BindableBase
    {
        /// <summary>
        /// 显示消息
        /// </summary>
        public ScrollMessageViewModel ScrollMessage { get; set; }
        private Action<Action> _dispatcherAction;

        //访问模式
        public ICommand ProwatchAccessTypeChangedCommand { get; private set; }
        //初始化
        public ICommand InitAppCommand { get; private set; }
        //添加人员与卡
        public ICommand AddCardCommand { get; private set; }
        //添加人员与卡并启用
        public ICommand AddCardAndEnableCommand { get; private set; }
        //授权
        public ICommand AuthorizeCommand { get; private set; }
        //取消授权
        public ICommand CancelAuthorizeCommand { get; private set; }
        //禁用
        public ICommand EnablesCommand { get; private set; }
        //启用
        public ICommand DisablesCommand { get; private set; }
        //添加和删除人员与卡
        public ICommand AddAndDeleteCardCommand { get; private set; }
        //删除人员与卡
        public ICommand DeleteCardCommand { get; private set; }
        //修改卡门禁级别
        public ICommand EditCardAccessLevelCommand { get; private set; }
        //查询并修改卡门禁级别
        public ICommand SearchEditCardAccessLevelCommand { get; private set; }
        //查询测试
        public ICommand SearchCommand { get; private set; }
        //确定
        public ICommand ConfirmCommand { get; private set; }
        //激活
        public ICommand EnableCommand { get; private set; }
        //禁用
        public ICommand DisableCommand { get; private set; }
        //启用选中的访问码
        public ICommand EnableACCodeCommand { get; private set; }
        //刷新所有访问码
        public ICommand RefreshAllACCodeCommand { get; private set; }
        //禁用访问码
        public ICommand DisableACCodeCommand { get; private set; }
        //刷新访问码列表
        public ICommand RefreshCardACCodeCommand { get; private set; }
        //清除记录
        public ICommand ClearWarnCommand { get; private set; }
        //开启事件
        public ICommand StartMonitorCommand { get; private set; }
        //随机打印
        public ICommand RandomPrintCommand { get; private set; }
        //全部打印
        public ICommand AllPrintCommand { get; private set; }
        //自定义打印
        public ICommand CustomPrintCommand { get; private set; }
        //显示自定义
        public ICommand ShowCustomQrCodeCommand { get; private set; }
        //显示所有二维码
        public ICommand ShowAllQrCodeCommand { get; private set; }
        //授权并显示二维码
        public ICommand AuthorizeShowQrCodeCommand { get; private set; }

        private ConnectViewModel _connect;
        private string _cardNo;
        private AccessCodeData _enableACCodeValue;
        private AccessCodeData _disableACCodeValue;
        private ObservableCollection<AccessCodeData> _allACCodes;
        private ObservableCollection<AccessCodeData> _cardACCodes;
        private string _statusCode;
        private int _printParameter;

        //属性字段
        private string _baseUrl;
        private string _token;
        private int _startCardNo;
        private int _endCardNo;
        private List<CompanyData> _companies;
        private List<BadgeTypeData> _badgeTypes;
        private List<CardStateEnum> _cardStates;
        private CompanyData _company;
        private CardStateEnum _cardState;
        private BadgeTypeData _badgeType;
        private List<AccessCodeData> _acCodes;
        private AccessCodeData _acCode;
        private List<ProwatchAccessTypeEnum> _prowatchAccessTypes;
        private ProwatchAccessTypeEnum _prowatchAccessType;
        private IServiceProvider _serviceProvider;

        private ProwatchSettings _prowatchSettings;
        private IProwatchApi _prowatchApi;
        private ProwatchPrintHelper _prowatchPrintHelper;
        private PrintHandler _printHandler;
        //private ProwatchQrShowWindow _prowatchQrShowWindow;

        public ProwatchTestControlViewModel(ScrollMessageViewModel scrollMessageViewModel,
            IServiceProvider serviceProvider,
            IOptions<ProwatchSettings> prowatchSettings,
            ProwatchPrintHelper prowatchPrintHelper,
            PrintHandler printHandler)
        {
            _token = "";
            //_startCardNo = 1000100000;
            //_endCardNo = 1000129999;
            //_startCardNo = 1000130000;
            //_endCardNo = 1000139999;
            _startCardNo = 100;
            _endCardNo = 999;

            _printParameter = 30;

            ScrollMessage = scrollMessageViewModel;
            _serviceProvider = serviceProvider;
            _prowatchSettings = prowatchSettings.Value;
            _prowatchPrintHelper = prowatchPrintHelper;
            _printHandler = printHandler;


            _baseUrl = _prowatchSettings.BaseUrl;

            //_prowatchQrShowWindow = prowatchQrShowWindow;

            _connect = new ConnectViewModel(_prowatchSettings.LoginUser);
            //访问模式
            ProwatchAccessTypeChangedCommand = new DelegateCommand(ProwatchAccessTypeChanged);
            //初始化
            InitAppCommand = new DelegateCommand(InitAppAsync);
            //添加人员与卡
            AddCardCommand = new DelegateCommand(StartAddCard);
            //添加人员与卡并启用
            AddCardAndEnableCommand = new DelegateCommand(StartAddCardAndEnable);
            //授权
            AuthorizeCommand = new DelegateCommand(StartAuthorize);
            //取消授权
            CancelAuthorizeCommand = new DelegateCommand(StartCancelAuthorize);
            //禁用
            EnablesCommand = new DelegateCommand(StartEnables);
            //启用
            DisablesCommand = new DelegateCommand(StartDisables);
            //添加和删除人员与卡
            AddAndDeleteCardCommand = new DelegateCommand(AddAndDeleteCard);
            //删除人员与卡
            DeleteCardCommand = new DelegateCommand(DeleteCard);
            //修改卡门禁级别
            EditCardAccessLevelCommand = new DelegateCommand(EditCardAccessLevel);
            //查询并修改卡门禁级别
            SearchEditCardAccessLevelCommand = new DelegateCommand(SearchCardAccessLevel);
            //查询测试
            SearchCommand = new DelegateCommand(Search);
            //确定
            ConfirmCommand = new DelegateCommand(ConfirmAsync);
            //激活
            EnableCommand = new DelegateCommand(EnableAsync);
            //禁用
            DisableCommand = new DelegateCommand(DisableAsync);
            //启用选中的访问码
            EnableACCodeCommand = new DelegateCommand(EnableACCodeAsync);
            //刷新所有访问码
            RefreshAllACCodeCommand = new DelegateCommand(RefreshAllACCodeAsync);
            //禁用访问码
            DisableACCodeCommand = new DelegateCommand(DisableACCodeAsync);
            //刷新访问码列表
            RefreshCardACCodeCommand = new DelegateCommand(RefreshCardACCodeAsync);
            //清除记录
            ClearWarnCommand = new DelegateCommand(ClearWarn);
            //开启事件
            StartMonitorCommand = new DelegateCommand(StartMonitorAsync);
            //随机打印
            RandomPrintCommand = new DelegateCommand(RandomPrintAsync);
            //全部打印
            AllPrintCommand = new DelegateCommand(AllPrintAsync);
            //自定义打印
            CustomPrintCommand = new DelegateCommand(CustomPrintAsync);
            //显示自定义
            ShowCustomQrCodeCommand = new DelegateCommand(ShowCustomQrCode);
            //显示所有
            ShowAllQrCodeCommand = new DelegateCommand(ShowAllQrCode);
            //授权并显示
            AuthorizeShowQrCodeCommand = new DelegateCommand(AuthorizeShowQrCode);
        }

        private void ShowCustomQrCode()
        {
            var prowatchQrShowWindow = _serviceProvider.GetRequiredService<ProwatchQrShowWindow>();
            // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            prowatchQrShowWindow.ViewModel.StartShowAsync(PrintParameter, PrintParameter, Connect.ServerAddress, 10000, 10000);

            prowatchQrShowWindow.ShowDialog();
        }

        private void AuthorizeShowQrCode()
        {
            var prowatchQrShowWindow = _serviceProvider.GetRequiredService<ProwatchQrShowWindow>();
            // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            prowatchQrShowWindow.ViewModel.StartAuthorizeAndShowAsync(StartCardNo, EndCardNo, Connect.ServerAddress, AddAndAuthorize);

            prowatchQrShowWindow.ShowDialog();
        }

        private void ShowAllQrCode()
        {
            var prowatchQrShowWindow = _serviceProvider.GetRequiredService<ProwatchQrShowWindow>();
            // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            prowatchQrShowWindow.ViewModel.StartShowAsync(StartCardNo, EndCardNo, Connect.ServerAddress, 2, 0.7M);

            prowatchQrShowWindow.ShowDialog();
        }

        private async Task RandomPrintAsync()
        {
            var randomResult = NumberUtil.GetRandom(StartCardNo, EndCardNo, PrintParameter);
            foreach (var item in randomResult)
            {
                await PrintCardNoAsync(item);
            }
        }

        private async Task AllPrintAsync()
        {
            for (int i = StartCardNo; i <= EndCardNo; i++)
            {
                await PrintCardNoAsync(i);
            }
        }

        private async Task CustomPrintAsync()
        {
            await PrintCardNoAsync(PrintParameter);
        }

        private async Task PrintCardNoAsync(int cardNo)
        {
            var qrValue = _prowatchPrintHelper.GetCardNoValue(cardNo);

            var imageInfo = await _prowatchApi.GetQrAsync(Connect.ServerAddress, qrValue);
            var bitmap = ImageConvert.BytesToBitmap(imageInfo.Bytes);

            VisitQRCodePrintModel printObj = new VisitQRCodePrintModel();
            printObj.CompanyName = cardNo.ToString();
            printObj.QrImage = bitmap;

            await _printHandler.PrintApply(printObj);
        }

        private void ProwatchAccessTypeChanged()
        {
            if (ProwatchAccessType == ProwatchAccessTypeEnum.SDK_API)
            {
                _prowatchApi = _serviceProvider.GetRequiredService<ProwatchSdkApi>();
                ScrollMessage.InsertTop("切换访问模式成功：sdk api ");
            }
            else
            {
                _prowatchApi = _serviceProvider.GetRequiredService<ProwatchWebApi>();
                ScrollMessage.InsertTop("切换访问模式成功：web api ");
            }
        }

        public void Init(Action<Action> dispatcherAction)
        {
            _dispatcherAction = dispatcherAction;
            CardStates = CardStateEnum.Items;
            CardState = CardStateEnum.DISABLE;

            ProwatchAccessTypes = ProwatchAccessTypeEnum.Items;
            ProwatchAccessType = ProwatchAccessTypeEnum.WEB_API;

            _prowatchApi = _serviceProvider.GetRequiredService<ProwatchWebApi>();
        }

        private async Task InitAppAsync()
        {
            var user = Connect.ToModel();
            user.ServerAddress = user.ServerAddress + "/openapi/access/log/push";
            TokenResponse tokenResponse = await _prowatchApi.LoginAsync(BaseUrl, Token, user);

            ScrollMessage.InsertTop("初始化连接成功！");

            Token = tokenResponse.Token;

            //获取所有访问码
            List<AccessCodeData> data = await _prowatchApi.GetAcCodes(BaseUrl, Token);
            if (data == null || data.FirstOrDefault() == null)
            {
                CardACCodes = new ObservableCollection<AccessCodeData>();
                ScrollMessage.InsertTop("刷新访问码列表失败！");
            }
            else
            {
                AllACCodes = new ObservableCollection<AccessCodeData>(data);
            }

            Companies = await _prowatchApi.GetCompanies(BaseUrl, Token);
            BadgeTypes = await _prowatchApi.GetBadgeTypes(BaseUrl, Token);
            AcCodes = await _prowatchApi.GetAcCodes(BaseUrl, Token);
        }

        private async Task RefreshCardACCodeAsync()
        {
            List<AccessCodeData> data = await _prowatchApi.GetAcCodes(BaseUrl, Token, cardNo: CardNo.IsNull());
            if (data == null || data.FirstOrDefault() == null)
            {
                CardACCodes = new ObservableCollection<AccessCodeData>();
                ScrollMessage.InsertTop("刷新访问码列表失败！");
            }
            else
            {
                AllACCodes = new ObservableCollection<AccessCodeData>(data);
            }
            ScrollMessage.InsertTop("刷新访问码列表成功！");
        }

        private async Task EnableACCodeAsync()
        {
            ACCodeToCardModel model = new ACCodeToCardModel();
            model.ACCodeId = (EnableACCodeValue?.Id).IsNull();
            model.CardNo = CardNo.IsNull();
            await _prowatchApi.AddAcCodeToCard(BaseUrl, Token, model);
            ScrollMessage.InsertTop(string.Format("启用访问码成功！访问码Id：{0} , 访问码描述：{1}, 卡号：{2} ", EnableACCodeValue?.Id, EnableACCodeValue?.Desc, CardNo));
            await RefreshCardACCodeAsync();
        }

        private async Task DisableACCodeAsync()
        {
            ACCodeToCardModel model = new ACCodeToCardModel();
            model.ACCodeId = (EnableACCodeValue?.Id).IsNull();
            model.CardNo = CardNo.IsNull();
            await _prowatchApi.RemoveAcCodeFromCard(BaseUrl, Token, model);
            ScrollMessage.InsertTop(string.Format("禁用访问码成功！访问码Id：{0} , 访问码描述：{1}, 卡号：{2} ", DisableACCodeValue?.Id, DisableACCodeValue?.Desc, CardNo));
            await RefreshCardACCodeAsync();
        }

        private async Task RefreshAllACCodeAsync()
        {
            List<AccessCodeData> data = await _prowatchApi.GetAcCodes(BaseUrl, Token);
            if (data == null || data.FirstOrDefault() == null)
            {
                CardACCodes = new ObservableCollection<AccessCodeData>();
                ScrollMessage.InsertTop("刷新访问码列表失败！");
            }
            else
            {
                AllACCodes = new ObservableCollection<AccessCodeData>(data);
            }
            ScrollMessage.InsertTop("获取所有访问码成功！");
        }

        private void ClearWarn()
        {
            ScrollMessage.Clear();
        }

        private async Task StartMonitorAsync()
        {
            if (ProwatchAccessType == ProwatchAccessTypeEnum.WEB_API)
            {
                //api访问需登录本地
                var prowatchSdkApi = _serviceProvider.GetRequiredService<ProwatchSdkApi>();
                var user = Connect.ToModel();
                user.ServerAddress = user.ServerAddress + "/openapi/access/log/push";
                TokenResponse tokenResponse = await prowatchSdkApi.LoginAsync(BaseUrl, Token, user);
            }

            //本地事件监听
            ApiHelper.PWApi.realEventHandler = new RealEventHandler(this.RealEvent);
            bool bRet = ApiHelper.PWApi.StartRecvRealEvent();
            if (!bRet)
            {
                ScrollMessage.InsertTop("开启监听事件失败！");
                return;
            }
            ScrollMessage.InsertTop("开启监听事件成功！");
        }

        private void RealEvent(sPA_Event paEvent)
        {
            EventData eventData = paEvent.ToModel();
            _dispatcherAction.Invoke(() =>
            {
                ScrollMessage.InsertTop(JsonConvert.SerializeObject(eventData));
            });
        }

        private async Task EnableAsync()
        {
            var card = await GetCardAsync();
            card.StatCode = CardStateEnum.ENABLE.Value;

            await _prowatchApi.EditCardAsync(BaseUrl, Token, card);

            ScrollMessage.InsertTop("启用卡成功：" + card.CardNo);
            StatusCode = card.StatCode;
        }

        private async Task DisableAsync()
        {
            var card = await GetCardAsync();
            if (card == null)
            {
                return;
            }
            card.StatCode = CardStateEnum.DISABLE.Value;

            await _prowatchApi.EditCardAsync(BaseUrl, Token, card);

            ScrollMessage.InsertTop("禁用卡成功：" + card.CardNo);
            StatusCode = card.StatCode;
        }

        private async Task<CardData> GetCardAsync()
        {
            CardData card = await _prowatchApi.CardDetailAsync(BaseUrl, Token, CardNo);

            if (card == null)
            {
                ScrollMessage.InsertTop("查询卡失败：不存在该卡号的卡！");
                return null;
            }
            ScrollMessage.InsertTop("查询卡成功：" + JsonConvert.SerializeObject(card));

            CompanyData company = await _prowatchApi.GetCardCompanyAsync(BaseUrl, Token, card.CardNo);

            ScrollMessage.InsertTop("查询卡所属公司成功：" + JsonConvert.SerializeObject(company));

            card.CompanyId = company.Id;
            return card;
        }

        private async Task ConfirmAsync()
        {
            var card = await GetCardAsync();
            if (card == null)
            {
                return;
            }
            StatusCode = card.StatCode;

            await RefreshCardACCodeAsync();
        }

        private async Task<CardData> SelectCardAsync(string cardNo)
        {
            var startTime1 = DateTime.Now;
            try
            {
                var oldModel = await _prowatchApi.CardDetailAsync(BaseUrl, Token, cardNo);
                var timeSpan1 = (DateTime.Now - startTime1).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    if (oldModel == null)
                    {
                        ScrollMessage.InsertTop("查询卡不存在：timespan:{0}s cardNo:{1}", timeSpan1, cardNo.ToString());
                    }
                    else
                    {
                        ScrollMessage.InsertTop("查询卡已存在：timespan:{0}s cardNo:{1}", timeSpan1, cardNo.ToString());
                    }
                });
                return oldModel;
            }
            catch (Exception ex)
            {
                var timeSpan1 = (DateTime.Now - startTime1).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    var exception = ex.InnerException?.InnerException ?? ex.InnerException ?? ex;
                    ScrollMessage.InsertTop("查询卡失败：timespan:{0}s cardNo:{1} message:{2} ", timeSpan1, cardNo, exception.Message);
                });
                return null;
            }
        }


        private async Task<CompanyData> SelectCardCompanyAsync(string cardNo)
        {
            var startTime1 = DateTime.Now;
            try
            {
                CompanyData oldModel = await _prowatchApi.GetCardCompanyAsync(BaseUrl, Token, cardNo);
                var timeSpan1 = (DateTime.Now - startTime1).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    if (oldModel == null)
                    {
                        ScrollMessage.InsertTop("查询卡所属公司不存在：timespan:{0}s cardNo:{1}", timeSpan1, cardNo.ToString());
                    }
                    else
                    {
                        ScrollMessage.InsertTop("查询卡所属公司已存在：timespan:{0}s cardNo:{1}", timeSpan1, cardNo.ToString());
                    }
                });
                return oldModel;
            }
            catch (Exception ex)
            {
                var timeSpan1 = (DateTime.Now - startTime1).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("查询卡所属公司失败：timespan:{0}s cardNo:{1} message:{2} ", timeSpan1, cardNo, ex.GetBaseInnerMessage());
                });
                return null;
            }
        }
        private async Task<PersonCardModel> AddCardAsync(string cardNo, string activationDate, string expirationDate)
        {
            var model = PersonCardModel.Create();
            model.Card.CompanyId = Company.Id;
            model.Card.PersonId = string.Empty;
            model.Card.CardNo = cardNo;
            model.Card.StatCode = CardState.Value;
            model.Card.PinCode = string.Empty;
            model.Card.IssueDate = activationDate;
            model.Card.ExpireDate = expirationDate;

            model.Person.BadgeTypeId = BadgeType.Id;
            model.Person.FirstName = model.Card.CardNo;
            model.Person.LastName = "测试人员";
            model.Person.IssueDate = activationDate;
            model.Person.ExpireDate = expirationDate;

            var startTime = DateTime.Now;
            try
            {
                var personId = await _prowatchApi.AddPersonCard(BaseUrl, Token, model);
                model.Person.Id = personId;
                model.Card.PersonId = personId;

                var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("新增人员与卡成功：timespan:{0}s ", timeSpan);
                });
            }
            catch (Exception ex)
            {
                var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("新增人员与卡失败： timespan:{0}s cardNo:{1} message:{2} ", timeSpan, cardNo, ex.GetBaseInnerMessage());
                });
            }
            return model;
        }
        private async Task DeleteCardAndPersonAsync(string cardNo, string personId)
        {
            var removePersonCard = new RemovePersonCardModel();
            removePersonCard.CardNo = cardNo;

            removePersonCard.PersonId = personId;

            var startTime2 = DateTime.Now;
            try
            {
                await _prowatchApi.RemovePersonCardAsync(BaseUrl, Token, removePersonCard);
                var timeSpan2 = (DateTime.Now - startTime2).TotalSeconds;

                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("删除卡与持卡人成功：timespan:{0}s cardNo:{1} ", timeSpan2, cardNo);
                });
            }
            catch (Exception ex)
            {
                var timeSpan2 = (DateTime.Now - startTime2).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("删除卡与持卡人失败：timespan:{0}s cardNo:{1} message:{2} ", timeSpan2, cardNo, ex.GetBaseInnerMessage());
                });
            }
        }

        internal void StartAddCard()
        {
            ScrollMessage.InsertTopShape("开始人员与卡添加！！！！！！！！！！！！！！！！！！！");
            var activationDate = DateTime.Now.ToString("yyyy-MM-dd");
            var expirationDate = DateTime.Now.AddYears(5).ToString("yyyy-MM-dd");
            Task.Run(async () =>
            {
                for (int i = StartCardNo; i <= EndCardNo; i++)
                {
                    var oldModel = await SelectCardAsync(i.ToString());
                    if (oldModel != null)
                    {
                        continue;
                    }

                    await AddCardAsync(i.ToString(), activationDate, expirationDate);
                }
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTopShape($"人员与卡添加完成！！！！！！！！！！！！！！！！！！！");
                });
            });
        }
        internal void StartAddCardAndEnable()
        {
            ScrollMessage.InsertTopShape("开始添加人员与卡并启用！！！！！！！！！！！！！！！！！！！");
            var activationDate = DateTime.Now.ToString("yyyy-MM-dd");
            var expirationDate = DateTime.Now.AddYears(5).ToString("yyyy-MM-dd");
            Task.Run(async () =>
            {
                for (int i = StartCardNo; i <= EndCardNo; i++)
                {
                    await AddAndAuthorize(activationDate, expirationDate, i);
                }
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTopShape($"添加人员与卡并启用完成！！！！！！！！！！！！！！！！！！！");
                });
            });
        }

        public async Task AddAndAuthorize(string activationDate, string expirationDate, int i)
        {
            var oldModel = await SelectCardAsync(i.ToString());
            if (oldModel == null)
            {
                var cardModel = await AddCardAsync(i.ToString(), activationDate, expirationDate);
                oldModel = cardModel.Card;
            }
            await Authorize(oldModel, activationDate);
        }

        internal void AddAndDeleteCard()
        {
            ScrollMessage.InsertTopShape("开始人员与卡添加与删除！！！！！！！！！！！！！！！！！！！");
            var activationDate = DateTime.Now.ToString("yyyy-MM-dd");
            var expirationDate = DateTime.Now.AddYears(5).ToString("yyyy-MM-dd");
            Task.Run(async () =>
            {
                for (int repeatTimes = 1; repeatTimes < 10000; repeatTimes++)
                {
                    //新增卡
                    if (repeatTimes % 2 == 1)
                    {
                        _dispatcherAction.Invoke(() =>
                        {
                            ScrollMessage.InsertTopShape($"开始人员与卡添加 times:{repeatTimes}！！！！！！！！！！！！！！！！！！！");
                        });
                        for (int i = StartCardNo; i <= EndCardNo; i++)
                        {
                            var oldModel = await SelectCardAsync(i.ToString());
                            if (oldModel != null)
                            {
                                continue;
                            }

                            await AddCardAsync(i.ToString(), activationDate, expirationDate);
                        }
                        _dispatcherAction.Invoke(() =>
                        {
                            ScrollMessage.InsertTopShape($"人员与卡添加完成 times:{repeatTimes}！！！！！！！！！！！！！！！！！！！");
                        });
                    }

                    //删除卡
                    else
                    {
                        _dispatcherAction.Invoke(() =>
                        {
                            ScrollMessage.InsertTopShape($"开始人员与卡删除 times:{repeatTimes}！！！！！！！！！！！！！！！！！！！");
                        });
                        for (int i = StartCardNo; i <= EndCardNo; i++)
                        {
                            CardData oldModel = await SelectCardAsync(i.ToString());
                            if (oldModel == null)
                            {
                                continue;
                            }
                            await DeleteCardAndPersonAsync(oldModel.CardNo, oldModel.PersonId);
                        }
                        _dispatcherAction.Invoke(() =>
                        {
                            ScrollMessage.InsertTopShape($"人员与卡删除完成 times:{repeatTimes}！！！！！！！！！！！！！！！！！！！");
                        });
                    }
                }
            });
        }

        internal void DeleteCard()
        {
            ScrollMessage.InsertTopShape($"开始人员与卡删除  ！！！！！！！！！！！！！！！！！！！");
            var activationDate = DateTime.Now.ToString("yyyy-MM-dd");
            var expirationDate = DateTime.Now.AddYears(5).ToString("yyyy-MM-dd");
            Task.Run(async () =>
            {
                for (int i = StartCardNo; i <= EndCardNo; i++)
                {
                    CardData oldModel = await SelectCardAsync(i.ToString());
                    if (oldModel == null)
                    {
                        continue;
                    }
                    await DeleteCardAndPersonAsync(oldModel.CardNo, oldModel.PersonId);
                }
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTopShape($"人员与卡删除完成！！！！！！！！！！！！！！！！！！！");
                });
            });
        }

        internal void SearchCardAccessLevel()
        {
            ScrollMessage.InsertTopShape("开始查询并修改卡门禁级别！！！！！！！！！！！！！！！！！！！");
            Task.Run(async () =>
            {
                var datas = JsonDataHelper.GetWinPakCards();
                var multileAccessLevels = new List<string>()
                {
                    "混合分组",
                    "管理平台默认门禁级别[不可删除]"
                };

                var oneAccessLevels = new List<string>()
                {
                    "管理平台默认门禁级别[不可删除]"
                };
                CardData model = null;

                for (int repeatTimes = 1; repeatTimes < 10000; repeatTimes++)
                {
                    _dispatcherAction.Invoke(() =>
                    {
                        ScrollMessage.InsertTopShape($"开始查询并修改卡门禁级别 times:{repeatTimes}！！！！！！！！！！！！！！！！！！！");
                    });
                    for (int i = StartCardNo; i <= EndCardNo; i++)
                    {
                        var startTime1 = DateTime.Now;
                        try
                        {
                            model = await _prowatchApi.CardDetailAsync(BaseUrl, Token, i.ToString());
                            var timeSpan1 = (DateTime.Now - startTime1).TotalSeconds;
                            if (model == null)
                            {
                                _dispatcherAction.Invoke(() =>
                                {
                                    ScrollMessage.InsertTop("查询卡不存在：timespan:{0}s cardNo:{1}", timeSpan1, i.ToString());
                                });
                                continue;
                            }
                            _dispatcherAction.Invoke(() =>
                            {
                                ScrollMessage.InsertTop("查询卡成功：timespan:{0}s cardNo:{1}", timeSpan1, i.ToString());
                            });
                        }
                        catch (Exception ex)
                        {
                            var timeSpan1 = (DateTime.Now - startTime1).TotalSeconds;
                            _dispatcherAction.Invoke(() =>
                            {
                                ScrollMessage.InsertTop("查询卡失败：timespan:{0}s cardNo:{1} message:{2} ", timeSpan1, i.ToString(), ex.GetBaseInnerMessage());
                            });
                            continue;
                        }


                        var startTime2 = DateTime.Now;
                        try
                        {
                            await _prowatchApi.EditCardAsync(BaseUrl, Token, model);
                            var timeSpan2 = (DateTime.Now - startTime2).TotalSeconds;

                            _dispatcherAction.Invoke(() =>
                            {
                                ScrollMessage.InsertTop("修改卡门禁级别成功：timespan:{0}s cardNo:{1} ", timeSpan2, model.CardNo);
                            });
                        }
                        catch (Exception ex)
                        {
                            var timeSpan2 = (DateTime.Now - startTime2).TotalSeconds;
                            _dispatcherAction.Invoke(() =>
                            {
                                ScrollMessage.InsertTop("修改卡门禁级别失败：timespan:{0}s cardNo:{1} message:{2} ", timeSpan2, model.CardNo, ex.GetBaseInnerMessage());
                            });
                        }
                    }
                }

                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTopShape("查询并修改卡门禁级别完成！！！！！！！！！！！！！！！！！！！");
                });
            });
        }


        internal void StartAuthorize()
        {
            ScrollMessage.InsertTopShape("开始授权！！！！！！！！！！！！！！！！！！！");
            var activationDate = DateTime.Now.ToString("yyyy-MM-dd");
            var expirationDate = DateTime.Now.AddYears(5).ToString("yyyy-MM-dd");
            Task.Run(async () =>
            {
                for (int i = StartCardNo; i <= EndCardNo; i++)
                {
                    var oldModel = await SelectCardAsync(i.ToString());
                    if (oldModel == null)
                    {
                        continue;
                    }
                    await Authorize(oldModel, activationDate);
                }
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTopShape($"授权完成！！！！！！！！！！！！！！！！！！！");
                });
            });
        }

        private async Task Authorize(CardData oldModel, string activationDate)
        {
            var company = await SelectCardCompanyAsync(oldModel.CardNo);
            if (company == null)
            {
                return;
            }
            oldModel.CompanyId = company.Id;
            await AddAcCodeToCardAsync(oldModel);
            await EnableCardAsync(oldModel, activationDate);
        }

        private async Task AddAcCodeToCardAsync(CardData oldModel)
        {
            var model = new ACCodeToCardModel();
            model.CardNo = oldModel.CardNo;
            model.ACCodeId = AcCode.Id;

            var startTime = DateTime.Now;
            try
            {
                var isExistsAcCode = await _prowatchApi.IsExistAcCodeByCardNo(BaseUrl, Token, model.CardNo, model.ACCodeId);
                if (isExistsAcCode)
                {
                    var timeSpan1 = (DateTime.Now - startTime).TotalSeconds;
                    _dispatcherAction.Invoke(() =>
                    {
                        ScrollMessage.InsertTop("卡片中已存在访问码：timespan:{0}s ", timeSpan1);
                    });
                    return;
                }
                await _prowatchApi.AddAcCodeToCard(BaseUrl, Token, model);
                var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("向卡片中添加访问码成功：timespan:{0}s ", timeSpan);
                });
            }
            catch (Exception ex)
            {
                var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("向卡片中添加访问码失败： timespan:{0}s cardNo:{1} message:{2} ", timeSpan, oldModel.CardNo, ex.GetBaseInnerMessage());
                });
            }
        }

        private async Task EnableCardAsync(CardData oldModel, string activationDate)
        {
            var startTime = DateTime.Now;
            if (oldModel.StatCode == CardStateEnum.ENABLE.Value)
            {
                var timeSpan1 = (DateTime.Now - startTime).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("卡已启用：timespan:{0}s ", timeSpan1);
                });
            }
            oldModel.IssueDate = activationDate;
            oldModel.StatCode = CardStateEnum.ENABLE.Value;

            try
            {
                await _prowatchApi.EditCardAsync(BaseUrl, Token, oldModel);
                var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("启用卡成功：timespan:{0}s ", timeSpan);
                });
            }
            catch (Exception ex)
            {
                var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("启用卡失败： timespan:{0}s cardNo:{1} message:{2} ", timeSpan, oldModel.CardNo, ex.GetBaseInnerMessage());
                });
            }
        }

        internal void StartCancelAuthorize()
        {
            ScrollMessage.InsertTopShape("开始取消授权！！！！！！！！！！！！！！！！！！！");
            var activationDate = DateTime.Now.ToString("yyyy-MM-dd");
            var expirationDate = DateTime.Now.AddYears(5).ToString("yyyy-MM-dd");
            Task.Run(async () =>
            {
                for (int i = StartCardNo; i <= EndCardNo; i++)
                {
                    var oldModel = await SelectCardAsync(i.ToString());
                    if (oldModel == null)
                    {
                        continue;
                    }
                    await CancelAuthorize(oldModel, activationDate);
                }
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTopShape($"取消授权完成！！！！！！！！！！！！！！！！！！！");
                });
            });
        }

        private async Task CancelAuthorize(CardData oldModel, string activationDate)
        {
            var company = await SelectCardCompanyAsync(oldModel.CardNo);
            if (company == null)
            {
                return;
            }
            oldModel.CompanyId = company.Id;
            await RemoveAcCodeFromCardAsync(oldModel);
            await DisableCardAsync(oldModel, activationDate);
        }

        private async Task RemoveAcCodeFromCardAsync(CardData oldModel)
        {
            var model = new ACCodeToCardModel();
            model.CardNo = oldModel.CardNo;
            model.ACCodeId = AcCode.Id;

            var startTime = DateTime.Now;
            try
            {
                var isExistsAcCode = await _prowatchApi.IsExistAcCodeByCardNo(BaseUrl, Token, model.CardNo, model.ACCodeId);
                if (!isExistsAcCode)
                {
                    var timeSpan1 = (DateTime.Now - startTime).TotalSeconds;
                    _dispatcherAction.Invoke(() =>
                    {
                        ScrollMessage.InsertTop("卡片中不存在访问码：timespan:{0}s ", timeSpan1);
                    });
                    return;
                }
                await _prowatchApi.RemoveAcCodeFromCard(BaseUrl, Token, model);
                var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("删除卡中的访问码成功：timespan:{0}s ", timeSpan);
                });
            }
            catch (Exception ex)
            {
                var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("删除卡中的访问码失败： timespan:{0}s cardNo:{1} message:{2} ", timeSpan, oldModel.CardNo, ex.GetBaseInnerMessage());
                });
            }
        }

        private async Task DisableCardAsync(CardData oldModel, string activationDate)
        {
            var startTime = DateTime.Now;
            if (oldModel.StatCode == CardStateEnum.DISABLE.Value)
            {
                var timeSpan1 = (DateTime.Now - startTime).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("卡已禁用：timespan:{0}s ", timeSpan1);
                });
            }
            oldModel.IssueDate = activationDate;
            oldModel.StatCode = CardStateEnum.DISABLE.Value;

            try
            {
                await _prowatchApi.EditCardAsync(BaseUrl, Token, oldModel);
                var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("禁用卡成功：timespan:{0}s ", timeSpan);
                });
            }
            catch (Exception ex)
            {
                var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("禁用卡失败： timespan:{0}s cardNo:{1} message:{2} ", timeSpan, oldModel.CardNo, ex.GetBaseInnerMessage());
                });
            }
        }

        internal void StartEnables()
        {
            ScrollMessage.InsertTopShape("开始启用卡！！！！！！！！！！！！！！！！！！！");
            var activationDate = DateTime.Now.ToString("yyyy-MM-dd");
            var expirationDate = DateTime.Now.AddYears(5).ToString("yyyy-MM-dd");
            Task.Run(async () =>
            {
                for (int i = StartCardNo; i <= EndCardNo; i++)
                {
                    var oldModel = await SelectCardAsync(i.ToString());
                    if (oldModel == null)
                    {
                        continue;
                    }
                    var company = await SelectCardCompanyAsync(oldModel.CardNo);
                    if (company == null)
                    {
                        return;
                    }
                    oldModel.CompanyId = company.Id;
                    await EnableCardAsync(oldModel, activationDate);
                }
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTopShape($"启用卡完成！！！！！！！！！！！！！！！！！！！");
                });
            });
        }

        internal void StartDisables()
        {
            ScrollMessage.InsertTopShape("开始禁用卡！！！！！！！！！！！！！！！！！！！");
            var activationDate = DateTime.Now.ToString("yyyy-MM-dd");
            var expirationDate = DateTime.Now.AddYears(5).ToString("yyyy-MM-dd");
            Task.Run(async () =>
            {
                for (int i = StartCardNo; i <= EndCardNo; i++)
                {
                    var oldModel = await SelectCardAsync(i.ToString());
                    if (oldModel == null)
                    {
                        continue;
                    }
                    var company = await SelectCardCompanyAsync(oldModel.CardNo);
                    if (company == null)
                    {
                        continue;
                    }
                    oldModel.CompanyId = company.Id;
                    await DisableCardAsync(oldModel, activationDate);
                }
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTopShape($"禁用卡完成！！！！！！！！！！！！！！！！！！！");
                });
            });
        }

        internal void EditCardAccessLevel()
        {
            ScrollMessage.InsertTopShape("开始修改卡门禁级别！！！！！！！！！！！！！！！！！！！");
            Task.Run(async () =>
            {
                var datas = JsonDataHelper.GetProwatchCards();
                var multileAccessLevels = new List<string>()
                {
                    "混合分组",
                    "管理平台默认门禁级别[不可删除]"
                };

                var oneAccessLevels = new List<string>()
                {
                    "管理平台默认门禁级别[不可删除]"
                };

                foreach (var item in datas)
                {
                    var startTime = DateTime.Now;

                    try
                    {
                        await _prowatchApi.EditCardAsync(BaseUrl, Token, item);

                        var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                        _dispatcherAction.Invoke(() =>
                        {
                            ScrollMessage.InsertTop("修改卡门禁级别成功：timespan:{0}s ", timeSpan);
                        });
                    }
                    catch (Exception ex)
                    {
                        var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                        _dispatcherAction.Invoke(() =>
                        {
                            ScrollMessage.InsertTop("修改卡门禁级别失败：timespan:{0}s message:{1} ", timeSpan, ex.GetBaseInnerMessage());
                        });
                    }
                }

                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTopShape("修改卡门禁级别完成！！！！！！！！！！！！！！！！！！！");
                });
            });
        }


        internal void Search()
        {
            ScrollMessage.InsertTopShape("开始查询！！！！！！！！！！！！！！！！！！！");
            Task.Run(async () =>
            {
                for (int i = StartCardNo; i <= EndCardNo; i++)
                {
                    //var func = new Func<Task<int>>(async () =>
                    //{
                    //    var result = await _prowatchApi.GetAccessLevelsAsync(BaseUrl, Token); 
                    //    return result.Count;
                    //});
                    //GetTest("查询门禁级别", func);

                    var func = new Func<Task<int>>(async () =>
                   {
                       var result = await _prowatchApi.GetCardsAsync(BaseUrl, Token);
                       return result.Count;
                   });
                    await GetTestAsync("查询卡", func);

                    func = new Func<Task<int>>(async () =>
                    {
                        var result = await _prowatchApi.GetPersonsAsync(BaseUrl, Token);
                        return result.Count;
                    });
                    await GetTestAsync("查询持卡人", func);

                    //func = new Func<int>(async () =>
                    //{
                    //    var result = await _prowatchApi.GetReadersAsync(BaseUrl, Token);
                    //    return result.Count;
                    //});
                    //GetTestAsync("查询读卡器", func);

                    //func = new Func<int>(() =>
                    //{
                    //    var result = await _prowatchApi.GetTimeZonesAsync(BaseUrl, Token);
                    //    return result.Count;
                    //});
                    //GetTestAsync("查询时区", func);
                }
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTopShape("查询完成！！！！！！！！！！！！！！！！！！！");
                });
            });
        }

        private async Task GetTestAsync(string title, Func<Task<int>> execFunc)
        {
            var startTime = DateTime.Now;
            try
            {
                int total = await execFunc.Invoke();

                var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("{0}成功：timespan:{1}s total:{2} ", title, timeSpan, total);
                });
            }
            catch (Exception ex)
            {
                var timeSpan = (DateTime.Now - startTime).TotalSeconds;
                _dispatcherAction.Invoke(() =>
                {
                    ScrollMessage.InsertTop("{0}失败： timespan:{1}s message:{2} ", title, timeSpan, ex.GetBaseInnerMessage());
                });
            }
        }

        public ConnectViewModel Connect
        {
            get
            {
                return _connect;
            }

            set
            {
                SetProperty(ref _connect, value);
            }
        }

        public string CardNo
        {
            get
            {
                return _cardNo;
            }

            set
            {
                SetProperty(ref _cardNo, value);
            }
        }

        public AccessCodeData EnableACCodeValue
        {
            get
            {
                return _enableACCodeValue;
            }

            set
            {
                SetProperty(ref _enableACCodeValue, value);
            }
        }
        public AccessCodeData DisableACCodeValue
        {
            get
            {
                return _disableACCodeValue;
            }

            set
            {
                SetProperty(ref _disableACCodeValue, value);
            }
        }

        public ObservableCollection<AccessCodeData> AllACCodes
        {
            get
            {
                return _allACCodes;
            }

            set
            {
                SetProperty(ref _allACCodes, value);
            }
        }

        public ObservableCollection<AccessCodeData> CardACCodes
        {
            get
            {
                return _cardACCodes;
            }

            set
            {
                SetProperty(ref _cardACCodes, value);
            }
        }



        public string StatusCode
        {
            get
            {
                return _statusCode;
            }

            set
            {
                SetProperty(ref _statusCode, value);
            }
        }

        public List<CompanyData> Companies
        {
            get
            {
                return _companies;
            }

            set
            {
                SetProperty(ref _companies, value);
            }
        }

        public List<CardStateEnum> CardStates
        {
            get
            {
                return _cardStates;
            }

            set
            {
                SetProperty(ref _cardStates, value);
            }
        }

        public CompanyData Company
        {
            get
            {
                return _company;
            }

            set
            {
                SetProperty(ref _company, value);
            }
        }

        public CardStateEnum CardState
        {
            get
            {
                return _cardState;
            }

            set
            {
                SetProperty(ref _cardState, value);
            }
        }



        public string BaseUrl
        {
            get { return _baseUrl; }
            set
            {
                SetProperty(ref _baseUrl, value);
            }
        }

        public string Token
        {
            get { return _token; }
            set
            {
                SetProperty(ref _token, value);
            }
        }

        public int StartCardNo
        {
            get { return _startCardNo; }
            set
            {
                SetProperty(ref _startCardNo, value);
            }
        }

        public int EndCardNo
        {
            get { return _endCardNo; }
            set
            {
                SetProperty(ref _endCardNo, value);
            }
        }

        public List<BadgeTypeData> BadgeTypes
        {
            get
            {
                return _badgeTypes;
            }

            set
            {
                SetProperty(ref _badgeTypes, value);
            }
        }

        public BadgeTypeData BadgeType
        {
            get
            {
                return _badgeType;
            }

            set
            {
                SetProperty(ref _badgeType, value);
            }
        }

        public List<AccessCodeData> AcCodes
        {
            get
            {
                return _acCodes;
            }

            set
            {
                SetProperty(ref _acCodes, value);
            }
        }

        public AccessCodeData AcCode
        {
            get
            {
                return _acCode;
            }

            set
            {
                SetProperty(ref _acCode, value);
            }
        }

        public List<ProwatchAccessTypeEnum> ProwatchAccessTypes
        {
            get
            {
                return _prowatchAccessTypes;
            }

            set
            {
                SetProperty(ref _prowatchAccessTypes, value);
            }
        }

        public ProwatchAccessTypeEnum ProwatchAccessType
        {
            get
            {
                return _prowatchAccessType;
            }

            set
            {
                SetProperty(ref _prowatchAccessType, value);
            }
        }

        public int PrintParameter
        {
            get
            {
                return _printParameter;
            }

            set
            {
                SetProperty(ref _printParameter, value);
            }
        }
    }
}
