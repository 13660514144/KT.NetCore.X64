using KT.Turnstile.Unit.Entity.Entities;
using KT.Turnstile.Unit.ClientApp.Dao.IDaos;
using KT.Turnstile.Unit.ClientApp.Service.IServices;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KT.Common.WpfApp.Helpers;
using KT.Common.Data.Models;
using KT.Common.Core.Utils;

namespace KT.Turnstile.Unit.ClientApp.Service.Services
{
    public class PassRightService : IPassRightService
    {
        private IPassRightDao _dao;
        private ILogger _logger;
        private IContainerProvider _containerProvider;

        public PassRightService(IPassRightDao dao,
            ILogger logger,
            IContainerProvider containerProvider)
        {
            _dao = dao;
            _logger = logger;
            _containerProvider = containerProvider;
        }

        public async Task AddOrUpdateAsync(List<TurnstileUnitPassRightEntity> entities)
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
                    _logger.LogError("新增或修改权限错误：ex:{0} ", ex);
                }
            }
        }

        public async Task AddOrUpdateAsync(TurnstileUnitPassRightEntity entity)
        {
            ////处理明细 
            //var details = new List<TurnstileUnitPassRightDetailEntity>();
            //if (entity.Details?.FirstOrDefault() != null)
            //{
            //    foreach (var item in entity.Details)
            //    {
            //        var rightGroup = await _dao.SelectRelevanceObjectAsync<TurnstileUnitRightGroupEntity>(item.RightGroup?.Id);
            //        if (rightGroup == null)
            //        {
            //            _logger.LogWarning($"未找到通行权限组！");
            //            continue;
            //        }
            //        if (string.IsNullOrEmpty(item.Id))
            //        {
            //            item.Id = IdUtil.NewId();
            //        }
            //        item.RightGroup = rightGroup;

            //        details.Add(item);
            //    }
            //}

            ////检查更新或修改
            //var oldEntity = await _dao.GetWidthDetailsByIdAsync(entity.Id);
            //if (oldEntity != null)
            //{
            //    if (oldEntity.EditedTime > entity.EditedTime)
            //    {
            //        _logger.LogWarning($"数据未更改：oldEditedTime:{oldEntity.EditedTime} EditedTime:{entity.EditedTime} ");
            //        return;
            //    }

            //    oldEntity.TimeStart = entity.TimeStart;
            //    oldEntity.TimeEnd = entity.TimeEnd;
            //    oldEntity.EditedTime = entity.EditedTime;

            //    oldEntity.Details = details;
            //    await _dao.AttachAsync(oldEntity);
            //}
            //else
            //{
            //    entity.Details = details;
            //    await _dao.InsertAsync(entity);
            //}


            var oldEntity = await _dao.GetWithDetailsByIdAsync(entity.Id);
            if (oldEntity != null)
            {
                if (oldEntity.EditedTime > entity.EditedTime)
                {
                    _logger.LogWarning($"数据未更改：oldEditedTime:{oldEntity.EditedTime} EditedTime:{entity.EditedTime} ");
                    return;
                }

                oldEntity.TimeStart = entity.TimeStart;
                oldEntity.TimeEnd = entity.TimeEnd;
                oldEntity.EditedTime = entity.EditedTime;

                if (oldEntity.Details?.FirstOrDefault() != null)
                {
                    var removeDetailIds = new List<string>();
                    foreach (var item in oldEntity.Details)
                    {
                        var newItem = entity.Details?.FirstOrDefault(x => x.Id == item.Id);
                        if (newItem == null)
                        {
                            removeDetailIds.Add(item.Id);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(newItem.RightGroupId) || newItem.RightGroupId == "0")
                            {
                                newItem.RightGroupId = newItem.RightGroup.Id;
                            }
                            item.RightGroup = await _dao.SelectRelevanceObjectAsync<TurnstileUnitRightGroupEntity>(newItem.RightGroupId);
                            if (item.RightGroup == null)
                            {
                                _logger.LogWarning($"未找到读卡器设备并删除当条明细：cardDeviceId:{newItem.RightGroupId} ");
                                removeDetailIds.Add(item.Id);
                                continue;
                            }
                            item.RightGroupId = item.RightGroup.Id;
                        }
                    }

                    if (!string.IsNullOrEmpty(removeDetailIds.FirstOrDefault()))
                    {
                        oldEntity.Details.RemoveAll(x => removeDetailIds.Any(y => x.Id == y));
                    }
                }
                else
                {
                    oldEntity.Details = new List<TurnstileUnitPassRightDetailEntity>();
                }


                if (entity.Details?.FirstOrDefault() != null)
                {
                    var addDetails = new List<TurnstileUnitPassRightDetailEntity>();
                    foreach (var item in entity.Details)
                    {
                        if (oldEntity.Details.Any(x => x.Id == item.Id))
                        {
                            continue;
                        }
                        if (string.IsNullOrEmpty(item.RightGroupId) || item.RightGroupId == "0")
                        {
                            item.RightGroupId = item.RightGroup.Id;
                        }
                        var rightGroup = await _dao.SelectRelevanceObjectAsync<TurnstileUnitRightGroupEntity>(item.RightGroupId);
                        if (rightGroup == null)
                        {
                            _logger.LogWarning($"未找到权限组：rightGroupId:{item.RightGroupId} ");
                            continue;
                        }

                        var detail = await _dao.SelectRelevanceObjectAsync<TurnstileUnitPassRightDetailEntity>(item.Id);
                        if (detail != null)
                        {
                            detail.RightGroup = rightGroup;
                            detail.RightGroupId = rightGroup.Id;
                            detail.PassRight = oldEntity;
                            detail.PassRightId = oldEntity.Id;

                            addDetails.Add(detail);
                        }
                        else
                        {
                            item.RightGroup = rightGroup;
                            item.RightGroupId = rightGroup.Id;
                            item.PassRight = oldEntity;
                            item.PassRightId = oldEntity.Id;

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
                    entity.Details = new List<TurnstileUnitPassRightDetailEntity>();
                    foreach (var item in newDetails)
                    {
                        var detail = await _dao.SelectRelevanceObjectAsync<TurnstileUnitPassRightDetailEntity>(item.Id);
                        if (detail == null)
                        {
                            detail = item;
                        }
                        if (string.IsNullOrEmpty(item.RightGroupId) || item.RightGroupId == "0")
                        {
                            item.RightGroupId = item.RightGroup?.Id;
                        }
                        detail.RightGroup = await _dao.SelectRelevanceObjectAsync<TurnstileUnitRightGroupEntity>(item.RightGroupId);
                        if (detail.RightGroup == null)
                        {
                            _logger.LogWarning($"未找到权限组：rightGroupId:{item.RightGroupId} ");
                            continue;
                        }
                        detail.RightGroupId = detail.RightGroup.Id;
                        entity.Details.Add(detail);
                    }
                }
                await _dao.InsertAsync(entity);
            }
        }

        public async Task Delete(string id, long editTime)
        {
            //删除旧数据 
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

        public async Task Deletesync(TurnstileUnitPassRightEntity passRight)
        {
            await _dao.DeleteAsync(passRight, false);

            await _dao.SaveChangesAsync();
        }

        public async Task<TurnstileUnitPassRightEntity> GetByCardNubmerAndCardDeviceId(string cardNumber, string cardDeviceId)
        {
            return await _dao.GetByCardNubmerAndCardDeviceId(cardNumber, cardDeviceId);
        }

        public async Task<PageData<TurnstileUnitPassRightEntity>> GetPageWithDetailAsync(int page, int size)
        {
            return await _dao.GetPageWithDetailsAsync(page, size);
        }
    }
}
