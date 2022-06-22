using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Class
{
    public interface IHttpFactoryOwner
    {
        Task<string> GetFactory(Dictionary<string, string> parameters, string requestUri, string token);
        Task<string> PostFactory(string Json, string requestUri, string token);
    }
}
