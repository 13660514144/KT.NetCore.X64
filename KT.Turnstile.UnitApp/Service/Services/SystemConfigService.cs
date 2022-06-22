using KT.Common.Core.Utils;
using KT.Turnstile.Unit.ClientApp.Dao.IDaos;
using KT.Turnstile.Unit.Entity.Entities;
using KT.Turnstile.Unit.Entity.Enums;
using KT.Turnstile.Unit.Entity.Models;
using KT.Turnstile.Unit.ClientApp.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Service.Services
{
    public class SystemConfigService : ISystemConfigService
    {
        private ISystemConfigDao _systemConfigDao;
        public SystemConfigService(ISystemConfigDao systemConfigDao)
        {
            _systemConfigDao = systemConfigDao;
        }

        public async Task AddOrUpdateAsync(UnitSystemConfigModel model)
        {
            // 服务器ip
            await AddOrUpdateAsync(SystemConfigEnum.SERVER_IP, model.ServerIp);

            // 服务器端口
            await AddOrUpdateAsync(SystemConfigEnum.SERVER_PORT, model.ServerPort);

            // 当前客户端ip
            await AddOrUpdateAsync(SystemConfigEnum.CLIENT_IP, model.ClientIp);

            // 服务器事件上传地址
            await AddOrUpdateAsync(SystemConfigEnum.PUSH_ADDRESS, model.PushAddress);

            // 最后同步数据时间
            await AddOrUpdateAsync(SystemConfigEnum.LAST_SYNC_TIME, model.LastSyncTime);
        }

        public async Task AddOrUpdateAsync(SystemConfigEnum keyEnum, object value)
        {
            var oldEntity = await _systemConfigDao.SelectFirstByLambdaAsync(x => x.Key == keyEnum.Value);
            if (oldEntity == null)
            {
                oldEntity = new TurnstileUnitSystemConfigEntity();
                oldEntity.Key = keyEnum.Value;
                oldEntity.Value = value.IsNull();
                await _systemConfigDao.InsertAsync(oldEntity);
            }
            else
            {
                oldEntity.Value = value.IsNull();
                await _systemConfigDao.AttachAsync(oldEntity);
            }
        }


        public async Task AddOrUpdatesAsync(List<TurnstileUnitSystemConfigEntity> entities)
        {
            foreach (var item in entities)
            {
                var oldEntity = await _systemConfigDao.SelectFirstByLambdaAsync(x => x.Key == item.Value);
                if (oldEntity == null)
                {
                    await _systemConfigDao.InsertAsync(item);
                }
                else
                {
                    oldEntity.Value = item.Value;
                    await _systemConfigDao.AttachAsync(oldEntity);
                }
            }
        }

        /// <summary>
        /// 获取最新配置文件
        /// </summary>
        /// <returns></returns>
        public async Task<UnitSystemConfigModel> GetAsync()
        {
            var entities = await _systemConfigDao.SelectAllAsync();
            var result = new UnitSystemConfigModel();
            if (entities == null)
            {
                return result;
            }
            foreach (var item in entities)
            {
                // 服务器ip
                if (item.Key == SystemConfigEnum.SERVER_IP.Value)
                {
                    result.ServerIp = item.Value;
                    continue;
                }

                // 服务器端口
                else if (item.Key == SystemConfigEnum.SERVER_PORT.Value)
                {
                    result.ServerPort = ConvertUtil.ToInt32(item.Value, 0);
                    continue;
                }

                // 当前客户端ip
                if (item.Key == SystemConfigEnum.CLIENT_IP.Value)
                {
                    result.ClientIp = item.Value;
                    continue;
                }

                // 服务器事件上传地址
                else if (item.Key == SystemConfigEnum.PUSH_ADDRESS.Value)
                {
                    result.PushAddress = item.Value;
                    continue;
                }

                // 最后同步时间
                else if (item.Key == SystemConfigEnum.LAST_SYNC_TIME.Value)
                {
                    result.LastSyncTime = ConvertUtil.ToLong(item.Value, 0);
                    continue;
                }
            }
            return result;
        }
    }
}
