using KT.Proxy.BackendApi.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Apis
{
    /// <summary>
    /// 黑名单
    /// </summary>
    public class FrontBlacklistApi : BackendApiBase, IBlacklistApi
    {
        public FrontBlacklistApi(ILogger<FrontBlacklistApi> logger) : base(logger)
        {
        }

        /// <summary>
        /// 添加黑名单
        /// </summary>
        /// <param name="black"></param>
        /// <returns></returns>
        public async Task AddAsync(BlacklistModel black)
        {
            await base.ExecutePostAsync("/front/block/add", black);
        }

        /// <summary>
        /// 获取黑名单列表
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        public async Task<PageData<BlacklistModel>> GetBlacksAsync(BlacklistSearchModel blacklistSearch)
        {
            var result = await PostAsync<PageData<BlacklistModel>>("/front/block/list", blacklistSearch);

            return result;
        }

        public async Task DeleteAsync(long id)
        {
            await ExecutePostAsync("/front/block/del", new { id });
        }

        /// <summary>
        /// 是否是黑名单
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="idNumber"></param>
        /// <returns></returns>
        public async Task<bool> IsBlackAsync(CheckBlacklistModel model)
        {
            var result = await PostAsync<bool>("/front/block/exist", model);
            return result;
        }

        public async Task<BlacklistModel> GetByIdAsync(long id)
        {
            return await PostAsync<BlacklistModel>("/front/block/info", new { id });
        }

        public async Task EditAsync(BlacklistModel model)
        {
            await ExecutePostAsync("/front/block/edit", model);
        }
    }
}
