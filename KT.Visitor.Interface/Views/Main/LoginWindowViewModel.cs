using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Common.WpfApp.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Visitor.Common.Helpers;
using KT.Visitor.Common.Settings;
using KT.Visitor.Common.ViewModels;
using KT.Visitor.Data.Entity;
using KT.Visitor.Data.IServices;
using KT.Visitor.Interface.Controls.BaseWindows;
using KT.Visitor.Interface.Views.Setting;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver.Core;
using Prism.Commands;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.User
{
    public class LoginWindowViewModel : PropertyChangedBase
    {
        public ICommand LoginCommand { get; private set; }

        private Action<string> _setPasswordAction;
        private Func<string> _getPasswordAction;
        private Action<string> _loginEndAction;

        private string _account;
        private bool _isRememberPassword;

        private string _accountError;
        private string _passwordError;
        public SystemInfoViewModel _systemInfo;

        private ILoginApi _loginApi;
        private ILoginUserDataService _loginUserDataService;
        private ILogger _logger;
        private DialogHelper _dialogHelper;
        private ConfigHelper _configHelper;
        private IFunctionApi _functionApi;
        private AppSettings _appSettings;
        public LoginWindowViewModel()
        {
            _loginApi = ContainerHelper.Resolve<ILoginApi>();
            _loginUserDataService = ContainerHelper.Resolve<ILoginUserDataService>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _dialogHelper = ContainerHelper.Resolve<DialogHelper>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _functionApi = ContainerHelper.Resolve<IFunctionApi>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            SystemInfo = ContainerHelper.Resolve<SystemInfoViewModel>();

            LoginCommand = new DelegateCommand(Login);
        }

        private async void Login()
        {
            try
            {
                await LoginAsync();
                _logger.LogInformation("登录成功！");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "登录失败");
                ContainerHelper.Resolve<MessageWarnBox>().ShowMessage($"{ex.Message}", "登录失败");
            }
        }

        public async Task InitAsync(Action<string> setPasswordAction, Func<string> getPasswordAction, Action<string> loginEndAction)
        {
            _setPasswordAction = setPasswordAction;
            _getPasswordAction = getPasswordAction;
            _loginEndAction = loginEndAction;

            await SetRemeberUserAsync();
        }

        private async Task AccountChangedAsync()
        {
            if (!string.IsNullOrEmpty(Account))
            {
                var user = await _loginUserDataService.GetByAccountAsync(Account);
                //比较账号，防止查询时间过长多次重复
                if (!string.IsNullOrEmpty(user?.Password) && _account == user.Account)
                {
                    _setPasswordAction?.Invoke(user.Password);
                    IsRememberPassword = true;
                    return;
                }
            }
            _setPasswordAction?.Invoke(string.Empty);
            IsRememberPassword = false;
        }

        public void Setting()
        {
            var systemLoginWindow = ContainerHelper.Resolve<SystemLoginWindow>();
            var result = _dialogHelper.ShowDialog(systemLoginWindow);
            if (result.HasValue && result.Value)
            {
                // 弹出操作窗口
                var systemSettingWindow = ContainerHelper.Resolve<SystemSettingWindow>();
                _dialogHelper.ShowDialog(systemSettingWindow);
            }
        }

        public async Task SetRemeberUserAsync()
        {
            //查询出保存的密码 
            var lastUser = await _loginUserDataService.GetLastAsync();
            if (lastUser != null && !string.IsNullOrEmpty(lastUser.Account))
            {
                Account = lastUser.Account;
                _setPasswordAction?.Invoke(lastUser.Password);
                IsRememberPassword = !string.IsNullOrEmpty(lastUser.Password);
            }
        }

        public async Task LoginAsync()
        {
            _logger.LogDebug("开始登录！！");
            var password = _getPasswordAction?.Invoke();
            if (string.IsNullOrEmpty(Account))
            {
                AccountError = "登录名不能为空";
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                PasswordError = "密码不能为空";
                return;
            }

            _logger.LogDebug("开始服务器登录！！");
            LoginResponse ACC=await _loginApi.LoginAsync(Account, password);
            _appSettings.Js.Token = ACC.Token;
            _appSettings.Js.Secret = ACC.Secret;
            _appSettings.Js.LoginName = ACC.Name;
            _appSettings.Js.Account = ACC.Account;
            LoginUserEntity userinfo = new LoginUserEntity();
            userinfo.Account = Account;
            if (IsRememberPassword)
            {
                userinfo.Password = password;
            }
            else
            {
                userinfo.Password = string.Empty;
            }

            _logger.LogDebug("保存本地密码！！");
            await _loginUserDataService.AddOrUpdateAsync(userinfo);

            //登录完成
            _loginEndAction?.Invoke(userinfo.Account);
        }

        public string Account
        {
            get
            {
                return _account;
            }

            set
            {
                _account = value;
                AccountError = null;
                NotifyPropertyChanged();
                _ = AccountChangedAsync();
            }
        }

        public bool IsRememberPassword
        {
            get
            {
                return _isRememberPassword;
            }

            set
            {
                _isRememberPassword = value;
                NotifyPropertyChanged();
            }
        }

        public string AccountError
        {
            get
            {
                return _accountError;
            }

            set
            {
                _accountError = value;
                NotifyPropertyChanged();
            }
        }

        public string PasswordError
        {
            get
            {
                return _passwordError;
            }

            set
            {
                _passwordError = value;
                NotifyPropertyChanged();
            }
        }

        public SystemInfoViewModel SystemInfo
        {
            get
            {
                return _systemInfo;
            }

            set
            {
                _systemInfo = value;
                NotifyPropertyChanged();
            }
        }
    }
}
