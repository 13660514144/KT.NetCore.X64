using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace KT.Elevator.Manage.Service.Helpers
{
    public class PushUrlHelper
    {
        private AppSettings _appSettings;
        private IServiceProvider _serviceProvider;
        public PushUrlHelper(IServiceProvider serviceProvider, IOptions<AppSettings> appSetting)
        {
            _serviceProvider = serviceProvider;
            _appSettings = appSetting.Value;
        }

        private object _locker = new object();
        private string _pushUrl;
        public string PushUrl
        {
            get
            {
                if (string.IsNullOrEmpty(_pushUrl))
                {
                    lock (_locker)
                    {
                        //当前存在值
                        if (string.IsNullOrEmpty(_pushUrl))
                        {
                            //当前登录用户
                            _pushUrl = MasterInfo.LoginUser?.ServerAddress;

                            //本地数据库查找
                            if (string.IsNullOrEmpty(_pushUrl))
                            {
                                using (var scope = _serviceProvider.CreateScope())
                                {
                                    var loginUserDataService = scope.ServiceProvider.GetRequiredService<ILoginUserService>();
                                    var result = loginUserDataService.GetLastAsync().Result;
                                    if (result != null)
                                    {
                                        _pushUrl = result.ServerAddress;
                                    }
                                }
                            }
                            //默认配置
                            if (string.IsNullOrEmpty(_pushUrl))
                            {
                                _pushUrl = _appSettings.DefaultPushAddress;
                            }
                        }
                    }
                }
                return _pushUrl;
            }
            set
            {
                _pushUrl = value;
            }
        }
    }
}
