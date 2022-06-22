using KT.Turnstile.Model.Models;
using KT.Turnstile.Manage.Service.IDaos;
using System;
using System.Collections.Generic;
using System.Text;
using KT.Common.Core.Exceptions;
using KT.Turnstile.Common.Enums;
using System.Linq;
using KT.Turnstile.Entity.Entities;
using KT.Common.Core.Utils;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using KT.Turnstile.Unit.Entity.Entities;
using KT.Turnstile.Manage.Service.IDistribute;

namespace KT.Turnstile.Manage.Service.Services
{
    public class PassRightService : IPassRightService
    {
        private IPassRightDao _passRightDataDao;
        private ILogger<PassRightService> _logger;
        private IPassRightDistribute _passRightDistribute;

        public PassRightService(IPassRightDao passRightDataDao,
            ILogger<PassRightService> logger,
            IPassRightDistribute passRightDistribute)
        {
            _passRightDataDao = passRightDataDao;
            _logger = logger;
            _passRightDistribute = passRightDistribute;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _passRightDataDao.HasInstanceByIdAsync(id);
        }

        public async Task<PassRightModel> AddAsync(PassRightModel model)
        {
            var entity = PassRightModel.ToEntity(model);

            entity = await _passRightDataDao.AddAsync(entity);
            model = PassRightModel.SetModel(model, entity);
            return model;
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _passRightDataDao.SelectByIdAsync(id);
            if (entity != null)
            {
                await _passRightDataDao.DeleteAsync(id);

                //分发数据 
                await _passRightDistribute.DeleteAsync(entity.Id, entity.EditedTime);
            }
        }

        public async Task<PassRightModel> AddOrEditAsync(PassRightModel model)
        {
            var entity = await _passRightDataDao.GetByIdAsync(model.Id);
            if (entity == null)
            {
                entity = PassRightModel.ToEntity(model);

                entity = await _passRightDataDao.AddAsync(entity);
                model = PassRightModel.SetModel(model, entity);
            }
            else
            {
                entity = PassRightModel.SetEntity(entity, model);

                entity = await _passRightDataDao.EditAsync(entity);

                model = PassRightModel.SetModel(model, entity);
            }

            //分发数据 
            await _passRightDistribute.AddOrUpdate(model);

            return model;
        }

        public async Task<List<PassRightModel>> GetAllAsync()
        {
            var entities = await _passRightDataDao.GetAllAsync();

            var models = PassRightModel.ToModels(entities);

            return models;
        }

        public async Task<PassRightModel> GetByIdAsync(string id)
        {
            var entity = await _passRightDataDao.GetByIdAsync(id);
            var model = PassRightModel.ToModel(entity);
            return model;
        }

        public async Task<List<TurnstileUnitPassRightEntity>> GetUnitPageAsync(int page, int size)
        {
            var results = new List<TurnstileUnitPassRightEntity>();
            var entities = await _passRightDataDao.GetPageAsync(page, size);

            if (entities == null || entities.FirstOrDefault() == null)
            {
                return results;
            }

            foreach (var item in entities)
            {
                var result = new TurnstileUnitPassRightEntity();
                result.Id = item.Id;
                result.CardNumber = item.CardNumber;
                result.TimeStart = item.TimeNow;
                result.TimeEnd = item.TimeOut;
                result.EditedTime = item.EditedTime;

                results.Add(result);

                if (item.RelationCardDeviceRightGroups == null
                    || item.RelationCardDeviceRightGroups.FirstOrDefault() == null)
                {
                    _logger.LogWarning($"通行权限关联通行权限组为空：passRight:{item.Id} ");
                    continue;
                }

                foreach (var obj in item.RelationCardDeviceRightGroups)
                {
                    var passRightDetail = new TurnstileUnitPassRightDetailEntity();
                    passRightDetail.Id = obj.Id;
                    passRightDetail.EditedTime = obj.EditedTime;

                    passRightDetail.RightGroup = new TurnstileUnitRightGroupEntity();
                    passRightDetail.RightGroup.Id = obj.CardDeviceRightGroup.Id;

                    result.Details.Add(passRightDetail);
                }
            }

            return results;
        }

        public async Task<CheckPassRightModel> HasRightAsync(string cardNumber, string cardDeviceId)
        {
            CheckPassRightModel passRecord = new CheckPassRightModel();
            passRecord.PassTime = DateTimeUtil.UtcNowMillis();

            //简单查询通行权限，多进多出
            var nultipleTimesRightList = await _passRightDataDao.GetHasRightListAsync(cardNumber, passRecord.PassTime);
            if (nultipleTimesRightList != null && nultipleTimesRightList.FirstOrDefault() != null)
            {
                foreach (var item in nultipleTimesRightList)
                {
                    _logger.LogInformation("通行权限信息1：id:{0} cardNo:{1} timeOut:{2} ===========================================",
                        item.Id, item.CardNumber, item.TimeOut);
                    //通道没有关联时可通行所有通道
                    if (item.RelationCardDeviceRightGroups == null || item.RelationCardDeviceRightGroups.FirstOrDefault() == null)
                    {
                        _logger.LogInformation("关联通行权限组信息2：无关联通行权限组信息！");
                        continue;
                    }

                    foreach (var obj in item.RelationCardDeviceRightGroups)
                    {
                        _logger.LogInformation("通行关联通行权组信息3：id:{0} ", obj.Id);
                        if (obj.CardDeviceRightGroup == null)
                        {
                            _logger.LogInformation("通行权限组信息4：无通行权限组信息！");
                            continue;
                        }
                        _logger.LogInformation("通行权组信息5： id:{0} name:{1} ", obj.CardDeviceRightGroup.Id, obj.CardDeviceRightGroup.Name);
                        if (obj.CardDeviceRightGroup.RelationCardDevices == null || obj.CardDeviceRightGroup.RelationCardDevices.FirstOrDefault() == null)
                        {
                            _logger.LogInformation("权限组关联读卡器信息5：权限组关联读卡器信息！");
                            continue;
                        }

                        foreach (var rcd in obj.CardDeviceRightGroup.RelationCardDevices)
                        {
                            _logger.LogInformation("行权组关联读卡器信息6：id:{0} ", rcd.Id);
                            if (rcd.CardDevice == null)
                            {
                                _logger.LogInformation("读卡器信息7：无读卡器信息信息！");
                                continue;
                            }
                            if (rcd.CardDevice.Id != cardDeviceId)
                            {
                                _logger.LogInformation("读卡器信息8：权限读卡器Id:{0} 与刷卡Id:{1} 不等!", rcd.CardDevice.Id, cardDeviceId);
                                continue;
                            }
                            //通行     
                            _logger.LogInformation("读卡器信息00000：可正常通行=================================！");
                            passRecord.HasRight = true;
                            passRecord.Type = PassRecordTypeEnum.MULTIPLE_TIMES_PASS;
                            passRecord.PassRight = PassRightModel.SetModel(passRecord.PassRight, item);
                            return passRecord;
                        }
                    }
                }
            }
            else
            {
                _logger.LogInformation("通行权限信息1： 无通行权限 cardNumber:{0} passTime:{1} ", cardNumber, passRecord.PassTime);
            }


            //以下为无效记录查询
            //根据卡号获取所有有权限的数据
            var lastEntity = await _passRightDataDao.GetLastAsync(cardNumber);
            //无权限
            if (lastEntity == null)
            {
                passRecord.HasRight = false;
                passRecord.Type = PassRecordTypeEnum.CARD_NOT_RIGHT;
                return passRecord;
            }
            //过期
            else if (lastEntity.TimeOut < passRecord.PassTime)
            {
                passRecord.HasRight = false;
                passRecord.Type = PassRecordTypeEnum.TIME_OUT;
                return passRecord;
            }
            //通道没有关联时可通行所有通道
            if (lastEntity.RelationCardDeviceRightGroups == null
                || lastEntity.RelationCardDeviceRightGroups.FirstOrDefault() == null
                || lastEntity.RelationCardDeviceRightGroups.FirstOrDefault(x =>
                x.CardDeviceRightGroup.RelationCardDevices.FirstOrDefault(x =>
                x.CardDevice.Id == cardDeviceId) == null) != null)
            {
                passRecord.HasRight = false;
                passRecord.Type = PassRecordTypeEnum.PASS_AISLE_NOT_RIGHT;
                return passRecord;
            }

            ////无当前通道权限
            //else if (lastEntity.RelationCardDeviceRightGroups != null
            //            && lastEntity.RelationCardDeviceRightGroups.FirstOrDefault() != null
            //            && lastEntity.RelationCardDeviceRightGroups.FirstOrDefault(x => x.CardDeviceRightGroup.Id == passAisleId) == null)
            //{
            //    passRecord.IsPassed = false;
            //    passRecord.Type = PassRecordTypeEnum.PASS_AISLE_NOT_RIGHT.Value;
            //    return passRecord;
            //}
            ////卡类型无效
            //else if (lastEntity.RelationCardDevices.FirstOrDefault(x => x.CardDevice.Id == cardDeviceId) == null)
            //{
            //    passRecord.IsPassed = false;
            //    passRecord.Type = PassRecordTypeEnum.CARD_TYPE_NOT_RIGHT.Value;
            //    return passRecord;
            //}
            ////已经通行
            //else if (lastEntity.PassType == PassRightTypeEnum.ONE_TIMES.Value
            //    && lastEntity.PassRecords.FirstOrDefault(x => x.CardDevice.PassDirection == passDirection) != null)
            //{
            //    passRecord.IsPassed = false;
            //    passRecord.Type = PassRecordTypeEnum.ALREADY_PASSED.Value;
            //    return passRecord;
            //}

            _logger.LogInformation("获取不到正确的权限状态：data:{0} ", JsonConvert.SerializeObject(lastEntity, JsonUtil.JsonSettings));

            passRecord.HasRight = false;
            passRecord.Type = PassRecordTypeEnum.CARD_NOT_RIGHT;
            return passRecord;
        }
    }
}
