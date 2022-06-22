using KT.Common.Core.Utils;
using KT.Visitor.Data.Entity;
using KT.Visitor.Data.Enums;
using KT.Visitor.Data.IDaos;
using KT.Visitor.Data.IServices;
using KT.Visitor.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Visitor.Data.Services
{
    public class SystemConfigDataService : ISystemConfigDataService
    {
        private ISystemConfigDataDao _systemConfigDao;
        public SystemConfigDataService(ISystemConfigDataDao systemConfigDao)
        {
            _systemConfigDao = systemConfigDao;
        }

        public async Task AddOrUpdateAsync(SystemConfigModel model)
        {
            // 证件阅读器
            await AddOrUpdateAsync(SystemConfigEnum.READER, model.Reader);

            // 打印机
            await AddOrUpdateAsync(SystemConfigEnum.PRINTER, model.Printer);

            // 读卡机
            await AddOrUpdateAsync(SystemConfigEnum.CARD_DEVICE, model.CardDevice);

            // 发卡机
            await AddOrUpdateAsync(SystemConfigEnum.CARD_ISSUE_METHOD, model.CardIssueMethod);

            // 服务器地址
            await AddOrUpdateAsync(SystemConfigEnum.SERVER_ADDRESS, model.ServerAddress);

            // 系统名称
            await AddOrUpdateAsync(SystemConfigEnum.SYSTEM_NAME, model.SystemName);

            // 系统LogoUrl地址
            await AddOrUpdateAsync(SystemConfigEnum.SYSTEM_LOGO_URL, model.SystemLogoUrl);
        }

        public async Task AddOrUpdateAsync(SystemConfigEnum keyEnum, object value)
        {
            var oldEntity = await _systemConfigDao.SelectByKeyAsync(keyEnum.Value);
            if (oldEntity == null)
            {
                oldEntity = new SystemConfigEntity();
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


        public async Task AddOrUpdatesAsync(List<SystemConfigEntity> entities)
        {
            foreach (var item in entities)
            {
                var oldEntity = await _systemConfigDao.SelectByKeyAsync(item.Key);
                if (oldEntity == null)
                {
                    await _systemConfigDao.InsertAsync(item);
                }
                else
                {
                    oldEntity.Value = item.Value;
                    await _systemConfigDao.UpdateAsync(oldEntity);
                }
            }
        }

        /// <summary>
        /// 获取最新配置文件
        /// </summary>
        /// <returns></returns>
        public async Task<SystemConfigModel> GetAsync()
        {
            var entities = await _systemConfigDao.SelectAllAsync();
            var result = new SystemConfigModel();
            if (entities == null)
            {
                return result;
            }
            foreach (var item in entities)
            {
                // 证件阅读器
                if (item.Key == SystemConfigEnum.READER.Value)
                {
                    result.Reader = item.Value;
                    continue;
                }
                // 打印机
                else if (item.Key == SystemConfigEnum.PRINTER.Value)
                {
                    result.Printer = item.Value;
                    continue;
                }
                // 读卡器
                else if (item.Key == SystemConfigEnum.CARD_DEVICE.Value)
                {
                    result.CardDevice = item.Value;
                    continue;
                }
                // 发卡机
                else if (item.Key == SystemConfigEnum.CARD_ISSUE_METHOD.Value)
                {
                    result.CardIssueMethod = item.Value;
                    continue;
                }
                // 服务器地址
                else if (item.Key == SystemConfigEnum.SERVER_ADDRESS.Value)
                {
                    result.ServerAddress = item.Value;
                    continue;
                }
                // 系统名称
                else if (item.Key == SystemConfigEnum.SYSTEM_NAME.Value)
                {
                    result.SystemName = item.Value;
                    continue;
                }
                // 系统LogoUrl地址
                else if (item.Key == SystemConfigEnum.SYSTEM_LOGO_URL.Value)
                {
                    result.SystemLogoUrl = item.Value;
                    continue;
                }
                // 上传文件最大值(kb)
                else if (item.Key == SystemConfigEnum.UPLOAD_FILE_SIZE.Value)
                {
                    result.UploadFileSize = ConvertUtil.ToDouble(item.Value, result.UploadFileSize);
                    continue;
                } 
            }
            return result;
        }
    }
}
