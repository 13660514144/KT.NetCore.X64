using KT.Turnstile.Manage.Service.IServices;
using KT.Turnstile.Model.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Turnstile.Manage.Service.Helpers
{
    public class PushUrlHelper
    {
        private IServiceProvider _serviceProvider;
        public PushUrlHelper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
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
