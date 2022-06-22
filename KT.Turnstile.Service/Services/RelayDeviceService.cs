using KT.Common.Core.Exceptions;
using KT.Turnstile.Manage.Service.IServices;
using KT.Turnstile.Manage.Service.IDaos;
using KT.Turnstile.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.Services
{
    public class RelayDeviceService : IRelayDeviceService
    {
        private IRelayDeviceDao _relayDeviceDataDao;
        public RelayDeviceService(IRelayDeviceDao relayDeviceDataDao)
        {
            _relayDeviceDataDao = relayDeviceDataDao;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _relayDeviceDataDao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _relayDeviceDataDao.DeleteAsync(id);
        }

        public async Task<RelayDeviceModel> AddOrEditAsync(RelayDeviceModel model)
        {
            var entity = await _relayDeviceDataDao.GetByIdAsync(model.Id);
            if (entity == null)
            {
                entity = RelayDeviceModel.ToEntity(model);

                entity = await _relayDeviceDataDao.AddAsync(entity);

                model = RelayDeviceModel.SetModel(model, entity);
            }
            else
            {
                entity = RelayDeviceModel.SetEntity(entity, model);

                entity = await _relayDeviceDataDao.EditAsync(entity);

                model = RelayDeviceModel.SetModel(model, entity);
            }
            return model;
        }

        public async Task<List<RelayDeviceModel>> GetAllAsync()
        {
            var entities = await _relayDeviceDataDao.GetAllAsync();

            var models = RelayDeviceModel.ToModels(entities);

            return models;
        }

        public async Task<RelayDeviceModel> GetByIdAsync(string id)
        {
            var entity = await _relayDeviceDataDao.GetByIdAsync(id);
            var model = RelayDeviceModel.ToModel(entity);
            return model;
        }
    }
}
