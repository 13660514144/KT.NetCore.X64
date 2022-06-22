using KT.Proxy.BackendApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Apis
{
    public class SelfBlacklistApi : BackendApiBase, IBlacklistApi
    {
        public SelfBlacklistApi(ILogger<SelfBlacklistApi> logger) : base(logger)
        {
        }

        public Task AddAsync(BlacklistModel black)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task EditAsync(BlacklistModel model)
        {
            throw new NotImplementedException();
        }

        public Task<PageData<BlacklistModel>> GetBlacksAsync(BlacklistSearchModel blacklistSearch)
        {
            throw new NotImplementedException();
        }

        public Task<BlacklistModel> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> IsBlackAsync(CheckBlacklistModel model)
        {
            var result = await PostAsync<bool>("/self/block/exist", model);
            return result;
        }
    }
}
