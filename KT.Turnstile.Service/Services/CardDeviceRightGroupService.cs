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
using System.Threading.Tasks;
using KT.Turnstile.Unit.Entity.Entities;
using KT.Turnstile.Manage.Service.IDistribute;
using Microsoft.EntityFrameworkCore.Design;

namespace KT.Turnstile.Manage.Service.Services
{
    public class CardDeviceRightGroupService : ICardDeviceRightGroupService
    {
        private ICardDeviceRightGroupDao _passRightDataDao;
        private IRightGroupDistribute _rightGroupDistribute;

        public CardDeviceRightGroupService(ICardDeviceRightGroupDao passRightDataDao,
            IRightGroupDistribute rightGroupDistribute)
        {
            _passRightDataDao = passRightDataDao;
            _rightGroupDistribute = rightGroupDistribute;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _passRightDataDao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _passRightDataDao.GetByIdAsync(id);
            if (entity != null)
            {
                await _passRightDataDao.DeleteAsync(id);
            }

            //分发数据 
            await _rightGroupDistribute.Delete(entity.Id, entity.EditedTime);
        }

        public async Task<CardDeviceRightGroupModel> AddOrEditAsync(CardDeviceRightGroupModel model)
        {
            var entity = await _passRightDataDao.GetByIdAsync(model.Id);
            if (entity == null)
            {
                entity = CardDeviceRightGroupModel.ToEntity(model);
                entity = await _passRightDataDao.AddAsync(entity);
                model = CardDeviceRightGroupModel.SetModel(model, entity);
            }
            else
            {
                entity = CardDeviceRightGroupModel.SetEntity(entity, model);

                entity = await _passRightDataDao.EditAsync(entity);

                model = CardDeviceRightGroupModel.SetModel(model, entity);
            }

            //分发数据 
            await _rightGroupDistribute.AddOrUpdate(model);

            return model;
        }

        public async Task<List<CardDeviceRightGroupModel>> GetAllAsync()
        {
            var entities = await _passRightDataDao.GetAllAsync();

            var models = CardDeviceRightGroupModel.ToModels(entities);

            return models;
        }

        public async Task<CardDeviceRightGroupModel> GetByIdAsync(string id)
        {
            var entity = await _passRightDataDao.GetByIdAsync(id);
            var model = CardDeviceRightGroupModel.ToModel(entity);
            return model;
        }

        public async Task<List<TurnstileUnitRightGroupEntity>> GetUnitAllAsync(int page, int size)
        {
            var results = new List<TurnstileUnitRightGroupEntity>();
            var entities = await _passRightDataDao.GetPageAsync(page, size);
            if (entities == null)
            {
                return results;
            }

            foreach (var item in entities)
            {
                var result = new TurnstileUnitRightGroupEntity();
                result.Id = item.Id;
                result.EditedTime = item.EditedTime;

                results.Add(result);

                if (item.RelationCardDevices == null)
                {
                    continue;
                }
                foreach (var obj in item.RelationCardDevices)
                {
                    if (obj.CardDevice == null)
                    {
                        continue;
                    }
                    var detail = new TurnstileUnitRightGroupDetailEntity();
                    detail.EditedTime = item.EditedTime;

                    //detail.RightGroup = new UnitRightGroupEntity();
                    //detail.RightGroup.Id = item.Id;

                    detail.CardDevice = new TurnstileUnitCardDeviceEntity();
                    detail.CardDevice.Id = obj.CardDevice.Id;

                    result.Details.Add(detail);
                }
            }

            return results;
        }
    }
}
