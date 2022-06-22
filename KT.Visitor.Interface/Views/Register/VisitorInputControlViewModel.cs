using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Common.Views.Helper;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Models;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver.Core;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.Register
{
    public class VisitorInputControlViewModel : BindableBase
    {
        public AccompanyVisitorControl AccompanyVisitorControl { get; private set; }
        public AccompanyVisitorsWindowViewModel AccompanyVisitorsWindowViewModel { get; private set; }

        public ICommand AddAccompanyCardCommand { get; private set; }
        public ICommand AddAccompanyPhotoCommand { get; private set; }
        public ICommand AddAccompanyCommand { get; private set; }
        //提交授权
        public ICommand ConfirmCommand { get; private set; }
        //取消授权
        public ICommand CancelCommand { get; private set; }

        public AddAccompanyCardWindowViewModel AddAccompanyCardWindowViewModel { get; private set; }
        public AddAccompanyPhotoWindowViewModel AddAccompanyPhotoWindowViewModel { get; private set; }


        private Action<string, AccompanyVisitorControlViewModel> _scanAndSetPersonAction { get; set; }

        //录入陪同访客数量
        private int _inputAccompanyNum = 0;
        //录入陪同访客显示访客姓名
        private string _inputAccompanyNames;

        //陪同访客数量
        private int _numAccompanyNum = 0;
        private int _cardAccompanyNum = 0;
        private int _photoAccompanyNum = 0;

        //陪同访客是否需要登记证件
        private bool _isCheckRetinueCert;
        //选择的公司，用于黑名单较验
        private CompanyViewModel _company;
        //授权时限
        private AuthorizeTimeLimitViewModel _timeLimitVM;
        //正在提交
        private bool _isSubmiting;

        //访问原因
        private ObservableCollection<ItemsCheckViewModel> _visitReasons;
        private IFunctionApi _functionApi;
        private VistitorConfigHelper _vistitorConfigHelper;
        private DialogHelper _dialogHelper;
        private IEventAggregator _eventAggregator;
        private IBlacklistApi _blacklistApi;
        private readonly ILogger _logger;

        public VisitorInputControlViewModel()
        {
            _functionApi = ContainerHelper.Resolve<IFunctionApi>();
            _vistitorConfigHelper = ContainerHelper.Resolve<VistitorConfigHelper>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _blacklistApi = ContainerHelper.Resolve<IBlacklistApi>();
            _logger = ContainerHelper.Resolve<ILogger>();

            AddAccompanyCardCommand = new DelegateCommand(AddAccompanyCard);
            AddAccompanyPhotoCommand = new DelegateCommand(AddAccompanyPhoto);
            AddAccompanyCommand = new DelegateCommand(AddAccompany);
            ConfirmCommand = new DelegateCommand(Confirm);
            CancelCommand = new DelegateCommand(Cancel);

            AccompanyVisitorControl = ContainerHelper.Resolve<AccompanyVisitorControl>();
            AccompanyVisitorControl.ViewModel.IsMain = true;
            TimeLimitVM = ContainerHelper.Resolve<AuthorizeTimeLimitViewModel>();
            
            //接收公司选择，用于较验黑名单
            _eventAggregator.GetEvent<CompanyCheckedEvent>().Subscribe(CompanyChecked);
            _eventAggregator.GetEvent<CompanySelectedEvent>().Subscribe(CompanyChecked);
            //授权完成，清理数据
            _eventAggregator.GetEvent<RegistedSuccessEvent>().Subscribe(Clear);
            //取消提交
            _eventAggregator.GetEvent<CancelSubmitEvent>().Subscribe(CancelSubmit);
        }

        private void CancelSubmit()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IsSubmiting = false;
            });
        }

        private void ExceptionError(Exception obj)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IsSubmiting = false;
            });
        }

        private void Clear()
        {
            _company = null;
            InitControl(_scanAndSetPersonAction);
            //异步 加载数据
            InitDataAsync();
            //陪同访客，需要登记证件
            AccompanyVisitorsWindowViewModel?.Init();
            //陪同访客，卡号输入
            AddAccompanyCardWindowViewModel?.ClearAccompany();
            //陪同访客，拍照录入
            AddAccompanyPhotoWindowViewModel?.ClearAccompany();
            //授权时限
            TimeLimitVM = ContainerHelper.Resolve<AuthorizeTimeLimitViewModel>();

            //陪同访客，人数输入
            InputAccompanyNum = 0;
            InputAccompanyNames = string.Empty;
            NumAccompanyNum = 0;
            CardAccompanyNum = 0;
            PhotoAccompanyNum = 0;

            //取消提交按钮禁用
            IsSubmiting = false;
            _eventAggregator.GetEvent<ExceptionEvent>().Unsubscribe(ExceptionError);
        }

        private void Cancel()
        {
            _eventAggregator.GetEvent<CancelRegisteEvent>().Publish();
        }

        private void CompanyChecked(CompanyViewModel company)
        {
            IsSubmiting = false;
            _company = company;
        }

        /// <summary>
        /// 提交登记  授权
        /// </summary>
        private void Confirm()
        {
            _logger.LogInformation("提交预约1");
            var submitParameter = new SubmitParameterModel();
            //主访客
            submitParameter.VisitorInfo = AccompanyVisitorControl.ViewModel.VisitorInfo;
            if (!string.IsNullOrEmpty(submitParameter.VisitorInfo.IcCard))
            {
                long Crn = Convert.ToInt64(submitParameter.VisitorInfo.IcCard);
                string Card = Crn.ToString();
                submitParameter.VisitorInfo.IcCard = Card;
            }
            //访问原由
            submitParameter.VisiteReason = VisitReasons.FirstOrDefault(x => x.IsChecked)?.Name;
            //头像路径
            submitParameter.ImageUrl = AccompanyVisitorControl.ViewModel.TakePictureControl.ViewModel.CaptureImage?.ImageUrl;
            submitParameter.Image = AccompanyVisitorControl.ViewModel.TakePictureControl.ViewModel.CaptureImage?.Image;
            //陪同访客，需要登记证件
            submitParameter.Accompanies = AccompanyVisitorsWindowViewModel?.GetVisitorTeams();
            //陪同访客，人数输入
            submitParameter.NumAccompanies = GetVisitorTeams(submitParameter.VisitorInfo?.Name);
            //陪同访客，卡号输入
            submitParameter.CardAccompanies = AddAccompanyCardWindowViewModel?.GetVisitorTeams(submitParameter.VisitorInfo?.Name);
            //陪同访客，拍照录入
            submitParameter.PhotoAccompanies = AddAccompanyPhotoWindowViewModel?.GetVisitorTeams(submitParameter.VisitorInfo?.Name);
            //授权时限
            submitParameter.AuthorizeTimeLimit = TimeLimitVM;

            _logger.LogInformation("提交预约2");

            //提交出错取消按钮禁用
            _eventAggregator.GetEvent<ExceptionEvent>().Subscribe(ExceptionError);
            //禁用提交按钮
            IsSubmiting = true;

            //提交访客登记
            _eventAggregator.GetEvent<SubmitRegisteEvent>().Publish(submitParameter);

            _logger.LogInformation("提交预约3");
        }

        /// <summary>
        /// 添加陪同访客
        /// </summary>
        private void AddAccompany()
        {
            var window = ContainerHelper.Resolve<AccompanyVisitorsWindow>();
            if (AccompanyVisitorsWindowViewModel != null)
            {
                window.SetViewModel(AccompanyVisitorsWindowViewModel, _scanAndSetPersonAction);
            }
            else
            {
                AccompanyVisitorsWindowViewModel = window.ViewModel;
                AccompanyVisitorsWindowViewModel.ScanAndSetPersonAction = _scanAndSetPersonAction;

                window.Init();
            }
            _dialogHelper.ShowDialog(window);

            InputAccompanyNames = AccompanyVisitorsWindowViewModel.VisitorControls
                .Select(x => x.ViewModel.VisitorInfo.Name)
                .ToList()
                .RightDecorates("，");
        }

        /// <summary>
        /// 发卡
        /// </summary>
        private void AddAccompanyCard()
        {
            var window = ContainerHelper.Resolve<AddAccompanyCardWindow>();
            if (AddAccompanyCardWindowViewModel != null)
            {
                window.SetViewModel(AddAccompanyCardWindowViewModel);
            }
            else
            {
                AddAccompanyCardWindowViewModel = window.ViewModel;
            }
            InputPageHelepr.CurrentInputName = InputPageHelepr.CARD_ACCOMPANY_INPUT;
            _dialogHelper.ShowDialog(window);

            InputPageHelepr.CurrentInputName = InputPageHelepr.VISITOR_INPUT;
            CardAccompanyNum = AddAccompanyCardWindowViewModel.AccompanyNum;
        }

        /// <summary>
        /// 拍照
        /// </summary>
        private void AddAccompanyPhoto()
        {
            var window = ContainerHelper.Resolve<AddAccompanyPhotoWindow>();
            if (AddAccompanyPhotoWindowViewModel != null)
            {
                window.SetViewModel(AddAccompanyPhotoWindowViewModel);
            }
            else
            {
                AddAccompanyPhotoWindowViewModel = window.ViewModel;
            }

            window.FocusCardNumber();
            InputPageHelepr.CurrentInputName = InputPageHelepr.PHOTO_ACCOMPANY_INPUT;
            _dialogHelper.ShowDialog(window);

            InputPageHelepr.CurrentInputName = InputPageHelepr.VISITOR_INPUT;
            PhotoAccompanyNum = AddAccompanyPhotoWindowViewModel.AccompanyNum;
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <returns></returns>
        public void InitControl(Action<string, AccompanyVisitorControlViewModel> scanAndSetPersonAction)
        {
            _scanAndSetPersonAction = scanAndSetPersonAction;
            AccompanyVisitorControl.ViewModel.ScanAndSetPersonAction = _scanAndSetPersonAction;

            //异步 加载数据
            InitDataAsync();
        }

        public async void InitDataAsync()
        {
            //初始化数据
            var visitorConfig = await _functionApi.GetConfigParmsAsync();
            var visitReasons = _vistitorConfigHelper.SetVisitReasons(visitorConfig?.AccessReasons);

            Application.Current.Dispatcher.Invoke(() =>
            {
                VisitReasons = visitReasons;
                IsCheckRetinueCert = visitorConfig.CheckRetinueCert;
            });
        }

        internal List<VisitorInfoModel> GetVisitorTeams(string name)
        {
            var visitorTeams = new List<VisitorInfoModel>();
            if (NumAccompanyNum > 0)
            {
                for (int i = 1; i <= NumAccompanyNum; i++)
                {
                    var accompany = new VisitorInfoModel();
                    accompany.Name = $"{name}-陪同访客{i}";
                    accompany.Gender = "MALE";
                    visitorTeams.Add(accompany);
                }
            }
            return visitorTeams;
        }

        /// <summary>
        /// 验证是否是黑名单
        /// </summary>
        /// <param name="idCode"></param>
        /// <returns></returns>
        public async Task<bool> IsBacklistByIdCodeAsync(string idCode)
        {
            //验证黑名单  
            if (_company != null)
            {
                var checkBack = new CheckBlacklistModel();
                checkBack.Phone = string.Empty;
                checkBack.IdNumber = idCode;
                checkBack.CompanyId = _company.Id;
                checkBack.FloorId = _company.FloorId;

                var isBlack = await _blacklistApi.IsBlackAsync(checkBack);
                if (isBlack)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ContainerHelper.Resolve<MessageWarnBox>().ShowMessage("抱歉，疑似黑名单用户，不能登记！");
                    });
                }
            }
            return false;
        }

        public ObservableCollection<ItemsCheckViewModel> VisitReasons
        {
            get => _visitReasons;
            set
            {
                SetProperty(ref _visitReasons, value);
            }
        }


        public bool IsCheckRetinueCert
        {
            get => _isCheckRetinueCert;
            set
            {
                SetProperty(ref _isCheckRetinueCert, value);
            }
        }
        public AuthorizeTimeLimitViewModel TimeLimitVM
        {
            get => _timeLimitVM;
            set
            {
                SetProperty(ref _timeLimitVM, value);
            }
        }

        public bool IsSubmiting
        {
            get => _isSubmiting;
            set
            {
                SetProperty(ref _isSubmiting, value);
            }
        }

        public int InputAccompanyNum
        {
            get => _inputAccompanyNum;
            set
            {
                SetProperty(ref _inputAccompanyNum, value);
            }
        }

        public string InputAccompanyNames
        {
            get => _inputAccompanyNames;
            set
            {
                SetProperty(ref _inputAccompanyNames, value);
            }
        }

        public int NumAccompanyNum
        {
            get => _numAccompanyNum;
            set
            {
                SetProperty(ref _numAccompanyNum, value);
            }
        }
        public int CardAccompanyNum
        {
            get => _cardAccompanyNum;
            set
            {
                SetProperty(ref _cardAccompanyNum, value);
            }
        }
        public int PhotoAccompanyNum
        {
            get => _photoAccompanyNum;
            set
            {
                SetProperty(ref _photoAccompanyNum, value);
            }
        }
    }
}
