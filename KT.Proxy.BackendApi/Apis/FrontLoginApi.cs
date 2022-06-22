
using KangTa.Visitor.Proxy.ServiceApi.Modes;
using KT.Proxy.BackendApi.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace KT.Proxy.BackendApi.Apis
{
    public class FrontLoginApi : BackendApiBase, ILoginApi
    {
        public FrontLoginApi(ILogger<FrontLoginApi> logger) : base(logger)
        {
        }

        public async Task<LoginResponse> LoginAsync(string account, string password)
        {
            var postdata = new { account, password };
            var result = await PostAsync<LoginResponse>("/front/checkin", postdata, isHasBaseHeader: false);

            MasterUser.Token = result?.Token;
            MasterUser.Secret = result?.Secret;
            MasterUser.LoginName = result?.Name;
            
            return result;
        }

        public async Task LogOutAsync()
        {
            await base.ExecutePostAsync("/front/checkin");

            MasterUser.Token = null;
            MasterUser.Secret = null;
        }
    }
}
