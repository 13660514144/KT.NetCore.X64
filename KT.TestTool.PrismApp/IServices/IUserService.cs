using System;
using System.Collections.Generic;
using System.Text;

namespace KT.Tests.PrismApp.IServices
{
    public interface IUserService
    {
        void Login(string account, string password);
    }
}
