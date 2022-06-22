using KangTa.Visitor.Proxy.ServiceApi.Modes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Apis
{
    public interface ILoginApi
    {
        Task<LoginResponse> LoginAsync(string account, string password);
        Task LogOutAsync();
    }
}
