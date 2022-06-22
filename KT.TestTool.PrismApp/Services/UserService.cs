using KT.Common.WpfApp.Helpers;
using KT.Tests.PrismApp.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace KT.Tests.PrismApp.Services
{
    public class UserService : IUserService
    {
        private Log4gHelper _logger;
        public UserService(Log4gHelper logger)
        {
            _logger = logger;
        }
        public void Login(string account, string password)
        {
            _logger.Logger.Error(account);
            MessageBox.Show($"login {account} {password}  ");
        }
    }
}
