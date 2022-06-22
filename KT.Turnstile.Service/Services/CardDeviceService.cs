using KT.Common.Core.Exceptions;
using KT.Turnstile.Manage.Service.IServices;
using KT.Turnstile.Manage.Service.IDaos;
using KT.Turnstile.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KT.Turnstile.Unit.Entity.Entities;
using System.Linq;
using KT.Turnstile.Manage.Service.IDistribute;

namespace KT.Turnstile.Manage.Service.Services
{
    public class CardDeviceService : ICardDeviceService
    {
        private ICardDeviceDao _cardDeviceDataDao;
        private ICardDeviceDistribute _cardDeviceDistribute;

        public CardDeviceService(ICardDeviceDao cardDeviceDataDao,
            ICardDeviceDistribute cardDeviceDistribute)
        {
            _cardDeviceDataDao = cardDeviceDataDao;
            _cardDeviceDistribute = cardDeviceDistribute;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _cardDeviceDataDao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            var model = await _cardDeviceDataDao.DeleteReturnWidthProcessorAsync(id);

            //分发数据
            await _cardDeviceDistribute.DeleteAsync(model.Processor.ProcessorKey, model.Id, model.EditedTime);
        }

        public async Task<CardDeviceModel> AddOrEditAsync(CardDeviceModel model)
        {
            var entity = await _cardDeviceDataDao.GetByIdAsync(model.Id);
            if (entity == null)
            {
                entity = CardDeviceModel.ToEntity(model);
                entity = await _cardDeviceDataDao.AddAsync(entity);
            }
            else
            {
                entity = CardDeviceModel.SetEntity(entity, model);
                entity = await _cardDeviceDataDao.EditAsync(entity);
            }
            model = CardDeviceModel.SetModel(model, entity);

            //分发数据
            await _cardDeviceDistribute.AddOrUpdateAsync(model);

            return model;
        }


        public async Task<List<CardDeviceModel>> GetAllAsync()
        {
            var entities = await _cardDeviceDataDao.GetAllAsync();

            var models = CardDeviceModel.ToModels(entities);

            return models;
        }

        public async Task<CardDeviceModel> GetByIdAsync(string id)
        {
            var entity = await _cardDeviceDataDao.GetByIdAsync(id);
            var model = CardDeviceModel.ToModel(entity);
            return model;
        }

        public async Task<List<TurnstileUnitCardDeviceEntity>> GetUnitByProcessorId(string processorId)
        {
            var results = new List<TurnstileUnitCardDeviceEntity>();
            var entities = await _cardDeviceDataDao.GetByProcessorIdAsync(processorId);

            if (entities == null || entities.FirstOrDefault() == null)
            {
                return results;
            }

            foreach (var item in entities)
            {
                var result = new TurnstileUnitCardDeviceEntity();
                result.Id = item.Id;
                result.ProductType = item.Type;
                result.DeviceType = item.CardType;
                result.PortName = item.PortName;

                result.RelayDeviceIp = item.RelayDevice?.IpAddress;
                result.RelayDevicePort = item.RelayDevice?.Port ?? 0;
                result.RelayDeviceOut = item.RelayDeviceOut;
                result.HandleElevatorDeviceId = item.HandleElevatorDeviceId;
                  
                result.EditedTime = item.EditedTime;

                if (item.SerialConfig != null)
                {
                    result.Baudrate = item.SerialConfig.Baudrate;
                    result.Databits = item.SerialConfig.Databits;
                    result.Stopbits = item.SerialConfig.Stopbits;
                    result.Parity = item.SerialConfig.Parity;
                    result.ReadTimeout = item.SerialConfig.ReadTimeout;
                    result.Encoding = item.SerialConfig.Encoding;
                }
                results.Add(result);
            }

            return results;
        }
    }
}
