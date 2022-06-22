using KT.Common.Core.Utils;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.BackendApi.Models;
using KT.Visitor.Interface.Events;
using KT.Visitor.Interface.Helpers;
using KT.Visitor.Interface.Models;
using KT.Visitor.Interface.Views;
using KT.Visitor.Interface.Views.Auth;
using KT.Visitor.Interface.Views.Register;
using Panuon.UI.Silver.Core;
using Prism.Events;
using Prism.Regions;
using System.Windows.Input;

namespace KT.Visitor.IntegrateApp.Views.Register
{
    public class RegisterHomeControlViewModel : PropertyChangedBase
    {
        private VisitorRegisterControl _visitorRegisterControl;
        private IdentityAuthControl _identityAuthControl;
        private InviteAuthControl _inviteAuthControl;
        private VisitorRecordListControl _visitorRecordListControl;
        private VisitorImportRecordListControl _visitorImportRecordListControl;
        private VisitorBlacklistControl _visitorBlacklistControl;
        private AddBlacklistControl _addBlacklistControl;
        private VisitorDetailControl _visitorDetailControl;
        private VisitorImportDetailListControl _visitorImportDetailListControl;
      
        private OpenDoorTree _OpenDoorTree;
        private ToMessage _ToMessage;

        private ICommand _visitorRegisteCommand;
        public ICommand VisitorRegisteCommand => _visitorRegisteCommand ??= new DelegateCommand(VisitorRegiste);
        private ICommand _identityAuthCommand;
        public ICommand IdentityAuthCommand => _identityAuthCommand ??= new DelegateCommand(IdentityAuth);
        private ICommand _inviteAuthCommand;
        public ICommand InviteAuthCommand => _inviteAuthCommand ??= new DelegateCommand(InviteAuth);
        private ICommand _visitorRecordListCommand;
        public ICommand VisitorRecordListCommand => _visitorRecordListCommand ??= new DelegateCommand(VisitorRecordList);
        private ICommand _visitorBlacklistCommand;
        public ICommand VisitorBlacklistCommand => _visitorBlacklistCommand ??= new DelegateCommand(VisitorBlacklist);

        
        private ICommand _ControlDoorCommand;
        public ICommand ControlDoorCommand => _ControlDoorCommand ??= new DelegateCommand(ControlDoor);
        
        private ICommand _MessageCommand;
        public ICommand MessageCommand => _MessageCommand ??= new DelegateCommand(Message);
        
        private string _operatingName;
        private IRegion _contentRegion;

        private IEventAggregator _eventAggregator;
        private IRegionManager _regionManager;

        public RegisterHomeControlViewModel()
        {
            _visitorRegisterControl = ContainerHelper.Resolve<VisitorRegisterControl>();
            _identityAuthControl = ContainerHelper.Resolve<IdentityAuthControl>();
            _inviteAuthControl = ContainerHelper.Resolve<InviteAuthControl>();
            _visitorRecordListControl = ContainerHelper.Resolve<VisitorRecordListControl>();
            _visitorBlacklistControl = ContainerHelper.Resolve<VisitorBlacklistControl>();

            _visitorImportRecordListControl = ContainerHelper.Resolve<VisitorImportRecordListControl>();
            _addBlacklistControl = ContainerHelper.Resolve<AddBlacklistControl>();
            _visitorDetailControl = ContainerHelper.Resolve<VisitorDetailControl>();
            _visitorImportDetailListControl = ContainerHelper.Resolve<VisitorImportDetailListControl>();

            _OpenDoorTree = ContainerHelper.Resolve<OpenDoorTree>();
            _ToMessage = ContainerHelper.Resolve<ToMessage>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();

            //授权完成
            _eventAggregator.GetEvent<RegistedSuccessEvent>().Subscribe(EndRegisted);

            //链接
            _eventAggregator.GetEvent<NavLinkEvent>().Subscribe(SubscribeLink);
            //新增黑名单
            _eventAggregator.GetEvent<AddBlacklistEvent>().Subscribe(AddBlacklist);

        }

        /// <summary>
        /// 完成注册
        /// </summary>
        private void EndRegisted()
        {
            //转到访客登记 
            OperatingName = "访客登记";
            _contentRegion.Activate(_visitorRegisterControl);
        }

        private bool _isLoadRegion = false;
        public void ViewLoadedInit(IRegionManager regionManager)
        {
            if (!_isLoadRegion)
            {
                _regionManager = regionManager;
                _contentRegion = _regionManager.Regions[RegionNameHelper.RegisterHomeContentRegion];

                _contentRegion.Add(_visitorRegisterControl);
                _contentRegion.Add(_identityAuthControl);
                _contentRegion.Add(_inviteAuthControl);
                _contentRegion.Add(_visitorRecordListControl);
                _contentRegion.Add(_visitorBlacklistControl);
                _contentRegion.Add(_visitorImportRecordListControl);
                _contentRegion.Add(_addBlacklistControl);
                _contentRegion.Add(_visitorDetailControl);
                _contentRegion.Add(_visitorImportDetailListControl);

                _contentRegion.Add(_OpenDoorTree);
                _contentRegion.Add(_ToMessage);
                _isLoadRegion = true;
            }

            OperatingName = "访客登记";
            _contentRegion.Activate(_visitorRegisterControl);
        }

        /// <summary>
        /// 添加黑名单
        /// </summary>
        private async void AddBlacklist()
        {
            await _addBlacklistControl.ViewModel.InitAsync();
            _contentRegion.Activate(_addBlacklistControl);
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="navLink">页面跳转参数</param>
        private async void SubscribeLink(NavLinkModel navLink)
        {
            // 跳转到访客登记页面 
            if (navLink.View.Value == FontNavEnum.VISITOR_REGISTE.Value)
            {
                //当前页面为选择公司页面
                InputPageHelepr.CurrentInputName = InputPageHelepr.VISTIOR_SELECT_COMPANY;

                VisitorRegiste();
            }
            // 跳转到访客记录 
            else if (navLink.View.Value == FontNavEnum.VISITOR_RECORD.Value)
            {
                InputPageHelepr.CurrentInputName = InputPageHelepr.VISITOR_RECORD;

                VisitorRecordList();
            }
            // 跳转到黑名单      
            else if (navLink.View.Value == FontNavEnum.BLACKLIST.Value)
            {
                InputPageHelepr.CurrentInputName = InputPageHelepr.BLACKLIST;

                VisitorBlacklist();
            }
            // 修改黑名单      
            else if (navLink.View.Value == FontNavEnum.EDIT_BLACKLIST.Value)
            {
                InputPageHelepr.CurrentInputName = InputPageHelepr.EDIT_BLACKLIST;

                await _addBlacklistControl.ViewModel.RefreshEditAsync(navLink.Data.ToLong(),navLink.ImgServer);
                _addBlacklistControl.Imgpara = navLink.ImgServer;                
                _contentRegion.Activate(_addBlacklistControl);
                EditFace Imghead = new EditFace
                {
                    EditFlg = true,
                    UrlImg= navLink.ImgServer
                };
                _eventAggregator.GetEvent<EditFaceEvent>().Publish(Imghead);
            }
            // 访客详情    
            else if (navLink.View.Value == FontNavEnum.VISITOR_DETAIL.Value)
            {
                InputPageHelepr.CurrentInputName = InputPageHelepr.VISITOR_DETAIL;

                await _visitorDetailControl.InitAsync(navLink.Data.ToLong());
                _contentRegion.Activate(_visitorDetailControl);
            }
            // 访客详情    
            else if (navLink.View.Value == FontNavEnum.VISITOR_IMPORT_DETAIL.Value)
            {
                InputPageHelepr.CurrentInputName = InputPageHelepr.VISITOR_IMPORT_DETAIL;

                await _visitorImportDetailListControl.InitAsync((VisitorImportModel)navLink.Data);
                _contentRegion.Activate(_visitorImportDetailListControl);
            }
            // 访客详情    
            else if (navLink.View.Value == FontNavEnum.VISITOR_IMPORT.Value)
            {
                InputPageHelepr.CurrentInputName = InputPageHelepr.VISITOR_IMPORT;

                //激活访客列表页面
                _visitorImportRecordListControl.InitViewModel();
                _contentRegion.Activate(_visitorImportRecordListControl);
            }
            
            // OPEN OR CLOSE DOOR    
            else if (navLink.View.Value == FontNavEnum.CONTROL_DOOR.Value)
            {
                InputPageHelepr.CurrentInputName = InputPageHelepr.CONTROL_DOOR;
                ControlDoor();
            }
            
            // SEND MESSAGE
            else if (navLink.View.Value == FontNavEnum.MESSAGE.Value)
            {
                InputPageHelepr.CurrentInputName = InputPageHelepr.MESSAGE;
                Message();
            }
            
        }
        
        private void Message()
        {
            OperatingName = "通知";
            _contentRegion.Activate(_ToMessage);
            InputPageHelepr.CurrentInputName = InputPageHelepr.MESSAGE;
            //_eventAggregator.GetEvent<InviteAuthLinkEvent>().Publish();
        }
        
        private void ControlDoor()
        {
            OperatingName = "门禁开关控制";            
            _contentRegion.Activate(_OpenDoorTree);
            InputPageHelepr.CurrentInputName = InputPageHelepr.CONTROL_DOOR;
            //_eventAggregator.GetEvent<InviteAuthLinkEvent>().Publish();
        }
        
        private void VisitorRegiste()
        {
            OperatingName = "访客登记";
            _contentRegion.Activate(_visitorRegisterControl);

            //当前页面为选择公司页面
            InputPageHelepr.CurrentInputName = InputPageHelepr.VISTIOR_SELECT_COMPANY;
            //清空所有数据
            _eventAggregator.GetEvent<RegistedSuccessEvent>().Publish();
        }

        private void IdentityAuth()
        {
            OperatingName = "身份验证";
            _contentRegion.Activate(_identityAuthControl);

            InputPageHelepr.CurrentInputName = InputPageHelepr.IDENTITY_CHECK;
            _eventAggregator.GetEvent<IdentityAuthLinkEvent>().Publish();
        }

        private void InviteAuth()
        {
            OperatingName = "邀约验证";
            _contentRegion.Activate(_inviteAuthControl);

            InputPageHelepr.CurrentInputName = InputPageHelepr.INVITE_CHECK;
            _eventAggregator.GetEvent<InviteAuthLinkEvent>().Publish();
        }

        private void VisitorRecordList()
        {
            OperatingName = "访客记录";
            _visitorRecordListControl.InitViewModel();
            _contentRegion.Activate(_visitorRecordListControl);

            InputPageHelepr.CurrentInputName = InputPageHelepr.VISITOR_RECORD;
            _eventAggregator.GetEvent<InviteAuthLinkEvent>().Publish();
        }

        private void VisitorBlacklist()
        {
            OperatingName = "黑名单";
            _visitorBlacklistControl.InitViewModel();
            _contentRegion.Activate(_visitorBlacklistControl);

            InputPageHelepr.CurrentInputName = InputPageHelepr.BLACKLIST;
            _eventAggregator.GetEvent<InviteAuthLinkEvent>().Publish();
        }
        public string OperatingName
        {
            get
            {
                return _operatingName;
            }

            set
            {
                _operatingName = value;
                NotifyPropertyChanged();
            }
        }
    }
}
