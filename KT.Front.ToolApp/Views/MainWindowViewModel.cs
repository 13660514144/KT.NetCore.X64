using KT.Proxy.BackendApi.Helpers;
using KT.Proxy.BackendApi.Models;
using KT.Front.ToolApp.Views;
using KT.Front.WriteCard.Entity;
using KT.Front.WriteCard.SDK;
using KT.Front.WriteCard.Services;
using KT.Front.WriteCard.Util;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static KT.Front.WriteCard.Entity.CommonClass;
using System.Linq;

namespace KT.Front.ToolApp.Views
{

    public class MainWindowViewModel : BindableBase
    {
        #region 私有变量 
        private IniHelper _iniHelper;
        #endregion

        #region 字段初始化     
        private string _title = "KTSmartTool";
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        private string _deviceCode;
        public string DeviceCode
        {
            get
            {
                if (_deviceCode == null)
                {
                    _deviceCode = string.Empty;
                }
                else
                {
                    _deviceCode = _deviceCode.TrimStart('0');
                }
                return _deviceCode;
            }
            set
            {
                _deviceCode = value;
                RaisePropertyChanged();
            }
        }

        private string _serverAddress;
        public string ServerAddress
        {
            get { return _serverAddress; }
            set
            {
                _serverAddress = value;
                RaisePropertyChanged();
            }
        }

        private List<DeviceType> _devices = new List<DeviceType> { new DeviceType { Id = 1, Name = "QIACS-D300M-FM" } };
        public List<DeviceType> Devices
        {
            get { return _devices; }
            set
            {
                _devices = value;
                RaisePropertyChanged();
            }
        }

        private List<WriteRule> _writeRules = new List<WriteRule> { new HitachiRule() };
        public List<WriteRule> WriteRules
        {
            get { return _writeRules; }
            set
            {
                _writeRules = value;
                RaisePropertyChanged();
            }
        }

        private bool _isServerConnect;
        public bool IsServerConnect
        {
            get { return _isServerConnect; }
            set
            {
                _isServerConnect = value;
                RaisePropertyChanged();
            }
        }

        private bool _isDeviceConnect;
        public bool IsDeviceConnect
        {
            get { return _isDeviceConnect; }
            set
            {
                _isDeviceConnect = value;
                RaisePropertyChanged();
            }
        }

        private bool? _isWriteCardSuccess;
        public bool? IsWriteCardSuccess
        {
            get { return _isWriteCardSuccess; }
            set
            {
                _isWriteCardSuccess = value;
                RaisePropertyChanged();
            }
        }

        private bool _isWriting;
        public bool IsWriting
        {
            get { return _isWriting; }
            set
            {
                _isWriting = value;
                RaisePropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private bool _isWrongToastShow;
        public bool IsWrongToastShow
        {
            get { return _isWrongToastShow; }
            set
            {
                _isWrongToastShow = value;
                RaisePropertyChanged();
            }
        }

        private bool _isCorrectToastShow;
        public bool IsCorrectToastShow
        {
            get { return _isCorrectToastShow; }
            set
            {
                _isCorrectToastShow = value;
                RaisePropertyChanged();
            }
        }

        private WriteRule _currentRule;
        public WriteRule CurrentRule
        {
            get { return _currentRule; }
            set
            {
                _currentRule = value;
                RaisePropertyChanged();
            }
        }

        private DeviceType _currentDevice;
        public DeviceType CurrentDevice
        {
            get { return _currentDevice; }
            set
            {
                _currentDevice = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 命令初始化
        private ICommand _serverConnectCommand;
        public ICommand ServerConnectCommand => _serverConnectCommand ??= new DelegateCommand(ServerConnectAsync);

        private ICommand _diviceConnectCommand;
        public ICommand DiviceConnectCommand => _diviceConnectCommand ??= new DelegateCommand(DiviceConnectAsync);

        private ICommand _writePermissionsCommand;
        public ICommand WritePermissionsCommand => _writePermissionsCommand ??= new DelegateCommand(WritePermissionAsync);

        private ICommand _openConfirmToInitWindowCommand;
        public ICommand OpenConfirmToInitWindowCommand => _openConfirmToInitWindowCommand ??= new DelegateCommand(OpenConfirmToInitWindowAsync);

        private ICommand _enterCommand;
        public ICommand EnterCommand => _enterCommand ??= new DelegateCommand(WritePermissionAsync);

        #endregion

        private readonly IElevatorAuthInfoService _elevatorAuthInfoService;
        public MainWindowViewModel(IElevatorAuthInfoService elevatorAuthInfoService)
        {
            _elevatorAuthInfoService = elevatorAuthInfoService;

            _iniHelper = new IniHelper();
            CurrentRule = this.WriteRules[0];
            CurrentDevice = this.Devices[0];
            InitData();
        }

        #region 公共方法
        public void InitData()
        {
            ServerAddress = _iniHelper.Get("ServerAddress");
            RequestHelper.BackendBaseUrl = ServerAddress;
        }

        public async void ShowSucessAsync(string str)
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                IsCorrectToastShow = true;
                Name = str;

                //提示显示4秒钟后再隐藏。 
                await Task.Delay(4000);
                IsCorrectToastShow = false;
            });
        }

        public async void ShowWrongAsync(string str)
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                IsWrongToastShow = true;
                //提示显示4秒钟后再隐藏。
                Name = str;

                await Task.Delay(4000);
                IsWrongToastShow = false;
            });
        }

        public async void ServerConnectAsync()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                IsWriteCardSuccess = null;
                if (this.IsServerConnect)
                {
                    this.IsServerConnect = false;
                }
                else
                {
                    _iniHelper.Set("ServerAddress", ServerAddress);
                    this.IsServerConnect = true;
                }
            });

            await Task.CompletedTask;
        }

        public async void DiviceConnectAsync()
        {
            IsWriteCardSuccess = null;
            await D300M.ConnectAsync();
            IsDeviceConnect = true;
        }

        //TODO 找的第三方Loading控件不支持异步动画
        public async void WritePermissionAsync()
        {
            try
            {
                if (!IsDeviceConnect && !IsServerConnect)
                {
                    return;
                }

                IsWriting = true;
                RequestHelper.BackendBaseUrl = ServerAddress;
                var elevatorAuthInfo = await _elevatorAuthInfoService.GetElevatorAuthInfoAsync(DeviceCode.TrimStart('0'));

                if (elevatorAuthInfo == null)
                {
                    ShowWrongAsync("无人员关联信息");
                    return;
                }

                //写卡
                var writeResult = await CurrentRule.WriteCardAsync(elevatorAuthInfo.AuthInfos.Select(x => x.ElevatorFloorId).ToList(), DeviceCode);

                Application.Current.Dispatcher.Invoke(delegate
                {
                    IsWriting = false;
                    if (writeResult)
                    {
                        ShowSucessAsync(elevatorAuthInfo.Name + "写入成功");
                    }
                    else
                    {
                        ShowWrongAsync("操作失败，连接失败");
                    }
                });
            }
            catch (Exception ex)
            {
                ShowWrongAsync(ex.Message);
                IsWriting = false;
            }
        }

        public async void OpenConfirmToInitWindowAsync()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (!IsDeviceConnect && !IsServerConnect)
                {
                    return;
                }
                var confirmToInitWindow = new ConfirmToInitWindow(CurrentRule);
                confirmToInitWindow.Show();
            });

            await Task.CompletedTask;
        }
        #endregion
    }
}
