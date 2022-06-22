using KT.Proxy.WebApi.Backend.Apis;
using KT.Visitor.Data.Entity;
using KT.Visitor.Data.IServices;
using KT.Visitor.Interface.Views.Setting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Panuon.UI.Silver;
using Panuon.UI.Silver.Core;
using Prism.Commands;
using Prism.Ioc;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KT.Visitor.Interface.Views.User
{
    public class LoginWindowViewModel : PropertyChangedBase
    {
        public ICommand LoginCommand { get; private set; }

        private Action<string> _setPasswordAction;
        private Func<string> _getPasswordAction;
        private Action _loginEndAction;

        private string _account;
        private bool _isRememberPassword;

        private string _accountError;
        private string _passwordError;

        private LoginApi _loginApi;
        private ILoginUserDataService _loginUserDataService;
        private ILogger<LoginWindowViewModel> _logger;
        private IContainerProvider _containerProvider;

        public LoginWindowViewModel(LoginApi loginApi,
            ILoginUserDataService loginUserDataService,
            ILogger<LoginWindowViewModel> logger,
            IContainerProvider containerProvider)
        {
            _loginApi = loginApi;
            _loginUserDataService = loginUserDataService;
            _logger = logger;
            _containerProvider = containerProvider;


            LoginCommand = new DelegateCommand(Login);
        }

        private void Login()
        {
            _ = LoginAsync();
        }

        public async Task InitAsync(Action<string> setPasswordAction, Func<string> getPasswordAction, Action loginEndAction)
        {
            _setPasswordAction = setPasswordAction;
            _getPasswordAction = getPasswordAction;
            _loginEndAction = loginEndAction;

            await SetRemeberUserAsync();
        }

        private void AccountChanged()
        {
            Task.Run(async () =>
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
            });
        }

        public void Setting()
        {
            var systemLoginWindow = _containerProvider.Resolve<SystemLoginWindow>();
            var result = systemLoginWindow.ShowDialog();
            if (result.HasValue && result.Value)
            {
                // 弹出操作窗口
                var systemSettingWindow = _containerProvider.Resolve<IntegrateSystemSettingWindow>();
                systemSettingWindow.ShowDialog();
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

            await _loginApi.LoginAsync(Account, password);

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

            await _loginUserDataService.AddOrUpdateAsync(userinfo);
            _logger.LogInformation("用户密码保存成功", userinfo);

            _loginEndAction?.Invoke();
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
                AccountChanged();
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
    }
}
