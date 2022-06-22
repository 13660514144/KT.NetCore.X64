using KT.Turnstile.Unit.ClientApp.Dao.IDaos;
using KT.Turnstile.Unit.Entity.Entities;
using KT.Turnstile.Unit.ClientApp.Service.IServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using KT.Common.Core.Utils;

namespace KT.Turnstile.Unit.ClientApp.Service.Services
{
    public class RightGroupService : IRightGroupService
    {
        private IRightGroupDao _dao;
        private ILogger _logger;

        public RightGroupService(IRightGroupDao dao,
            ILogger logger)
        {
            _dao = dao;
            _logger = logger;
        }

        public async Task AddOrUpdateAsync(List<TurnstileUnitRightGroupEntity> entities)
        {
            if (entities == null)
            {
                return;
            }
            foreach (var entity in entities)
            {
                try
                {
                    await AddOrUpdateAsync(entity);
                }
                catch (Exception ex)
                {
                    _logger.LogError("新增或修改权限组错误：ex:{0} ", ex);
                }
            }
        }

        public async Task AddOrUpdateAsync(TurnstileUnitRightGroupEntity entity)
        {
            var oldEntity = await _dao.GetWithDetailsByIdAsync(entity.Id);
            if (oldEntity != null)
            {
                if (oldEntity.EditedTime > entity.EditedTime)
                {
                    _logger.LogWarning($"数据未更改：oldEditedTime:{oldEntity.EditedTime} EditedTime:{entity.EditedTime} ");
                    return;
                }

                oldEntity.EditedTime = entity.EditedTime;

                if (oldEntity.Details?.FirstOrDefault() != null)
                {
                    var remoteDetailIds = new List<string>();
                    foreach (var item in oldEntity.Details)
                    {
                        var newItem = entity.Details?.FirstOrDefault(x => x.Id == item.Id);

                        if (newItem == null)
                        {
                            remoteDetailIds.Add(item.Id);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(item.CardDeviceId) || item.CardDeviceId == "0")
                            {
                                newItem.CardDeviceId = newItem.CardDevice?.Id;
                            }
                            item.CardDevice = await _dao.SelectRelevanceObjectAsync<TurnstileUnitCardDeviceEntity>(newItem.CardDeviceId);
                            if (item.CardDevice == null)
                            {
                                _logger.LogWarning($"未找到读卡器设备并删除当条明细：cardDeviceId:{newItem.CardDeviceId} ");
                                remoteDetailIds.Add(item.Id);
                                continue;
                            }
                            item.CardDeviceId = item.CardDevice.Id;
                        }
                    }

                    if (!string.IsNullOrEmpty(remoteDetailIds.FirstOrDefault()))
                    {
                        oldEntity.Details.RemoveAll(x => remoteDetailIds.Any(y => x.Id == y));
                    }
                }
                else
                {
                    oldEntity.Details = new List<TurnstileUnitRightGroupDetailEntity>();
                }


                if (entity.Details?.FirstOrDefault() != null)
                {
                    var addDetails = new List<TurnstileUnitRightGroupDetailEntity>();
                    foreach (var item in entity.Details)
                    {
                        if (oldEntity.Details.Any(x => x.Id == item.Id))
                        {
                            continue;
                        }
                        if (string.IsNullOrEmpty(item.CardDeviceId) || item.CardDeviceId == "0")
                        {
                            item.CardDeviceId = item.CardDevice?.Id;
                        }
                        var cardDevice = await _dao.SelectRelevanceObjectAsync<TurnstileUnitCardDeviceEntity>(item.CardDeviceId);
                        if (cardDevice == null)
                        {
                            _logger.LogWarning($"未找到读卡器设备：cardDeviceId:{item.CardDeviceId} ");
                            continue;
                        }

                        var detail = await _dao.SelectRelevanceObjectAsync<TurnstileUnitRightGroupDetailEntity>(item.Id);
                        if (detail != null)
                        {
                            detail.CardDevice = cardDevice;
                            detail.CardDeviceId = cardDevice.Id;
                            detail.RightGroup = oldEntity;
                            detail.RightGroupId = oldEntity.Id;

                            addDetails.Add(detail);
                        }
                        else
                        {
                            item.CardDevice = cardDevice;
                            item.CardDeviceId = cardDevice.Id;
                            item.RightGroup = oldEntity;
                            item.RightGroupId = oldEntity.Id;

                            addDetails.Add(item);
                        }
                    }

                    if (addDetails.FirstOrDefault() != null)
                    {
                        oldEntity.Details.AddRange(addDetails);
                    }
                }

                await _dao.UpdateAsync(oldEntity);
            }
            else
            {
                if (entity.Details?.FirstOrDefault() != null)
                {
                    var newDetails = entity.Details;
                    entity.Details = new List<TurnstileUnitRightGroupDetailEntity>();
                    foreach (var item in newDetails)
                    {
                        var detail = await _dao.SelectRelevanceObjectAsync<TurnstileUnitRightGroupDetailEntity>(item.Id);
                        if (detail == null)
                        {
                            detail = item;
                        }

                        if (string.IsNullOrEmpty(item.CardDeviceId) || item.CardDeviceId == "0")
                        {
                            item.CardDeviceId = item.CardDevice?.Id;
                        }
                        detail.CardDevice = await _dao.SelectRelevanceObjectAsync<TurnstileUnitCardDeviceEntity>(item.CardDeviceId);
                        if (detail.CardDevice == null)
                        {
                            _logger.LogWarning($"未找到读卡器设备：cardDeviceId:{item.CardDeviceId} ");
                            continue;
                        }
                        detail.CardDeviceId = detail.CardDevice.Id;
                        entity.Details.Add(detail);
                    }
                }
                await _dao.InsertAsync(entity);
            }
        }

        public async Task DeleteAsync(string id, long editTime)
        {
            var oldEntity = await _dao.GetWithDetailsByIdAsync(id);
            if (oldEntity != null)
            {
                if (oldEntity.EditedTime > editTime)
                {
                    _logger.LogWarning($"数据未更改：oldEditedTime:{oldEntity.EditedTime} EditedTime:{editTime} ");
                    return;
                }
                await _dao.DeleteAsync(oldEntity);
            }
        }

        public async Task<List<TurnstileUnitRightGroupEntity>> GetAll()
        {
            return await _dao.SelectAllAsync();
        }
    }
}
