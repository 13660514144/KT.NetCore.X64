using KT.Turnstile.Model.Models;
using KT.Turnstile.Manage.Service.IDaos;
using System;
using System.Collections.Generic;
using System.Text;
using KT.Common.Core.Exceptions;
using System.Threading.Tasks;
using KT.Turnstile.Unit.Entity.Entities;
using KT.Turnstile.Manage.Service.Handlers;

namespace KT.Turnstile.Manage.Service.Services
{
    public class PassRecordService : IPassRecordService
    {
        private IPassRecordDao _passRecordDataDao;
        private PushRecordHandler _pushRecordHanlder;

        public PassRecordService(IPassRecordDao passRecordDataDao,
            PushRecordHandler pushRecordHanlder)
        {
            _passRecordDataDao = passRecordDataDao;
            _pushRecordHanlder = pushRecordHanlder;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _passRecordDataDao.HasInstanceByIdAsync(id);
        }

        public async Task<PassRecordModel> AddAsync(PassRecordModel model)
        {
            var entity = PassRecordModel.ToEntity(model);
            entity = await _passRecordDataDao.AddAsync(entity);
            model = PassRecordModel.SetModel(model, entity);
            return model;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _passRecordDataDao.DeleteAsync(id);
        }

        public async Task<PassRecordModel> EditAsync(PassRecordModel model)
        {
            var entity = await _passRecordDataDao.GetByIdAsync(model.Id);
            if (entity == null)
            {
                throw CustomException.Run("系统中找不到当前通行记录数据：id:{0} ", model.Id);
            }
           
            entity = PassRecordModel.SetEntity(entity, model);

            entity = await _passRecordDataDao.EditAsync(entity);

            var result = PassRecordModel.SetModel(model, entity);
            return result;
        }

        public async Task<List<PassRecordModel>> GetAllAsync()
        {
            var entities = await _passRecordDataDao.GetAllAsync();

            var models = PassRecordModel.ToModels(entities);

            return models;
        }

        public async Task<PassRecordModel> GetByIdAsync(string id)
        {
            var entity = await _passRecordDataDao.GetByIdAsync(id);
            var model = PassRecordModel.ToModel(entity);
            return model;
        }

        public async Task<PassRecordModel> GetLastAsync()
        {
            var entity = await _passRecordDataDao.SelectLastAsync();
            var model = PassRecordModel.ToModel(entity);
            return model;
        }

        public async Task PushPassRecord(TurnstileUnitPassRecordEntity unitPassRecord)
        {
            var passRecord = new PassRecordModel();
            passRecord.DeviceId = unitPassRecord.DeviceId;
            passRecord.DeviceType = unitPassRecord.DeviceType;
            passRecord.CardType = unitPassRecord.CardType;
            passRecord.CardNumber = unitPassRecord.CardNumber;
            passRecord.PassLocalTime = unitPassRecord.PassLocalTime;
            passRecord.PassTime = unitPassRecord.PassTime;
            passRecord.PassRightId = unitPassRecord.PassRightId;

            await _pushRecordHanlder.StartPushAsync(passRecord);
        }

        public async Task AddIfNoExistsAsync(PassRecordModel passRecord)
        {
            if (string.IsNullOrEmpty(passRecord.Id))
            {
                return;
            }
            var isExists = await _passRecordDataDao.HasInstanceAsync(x => x.Id == passRecord.Id);
            if (!isExists)
            {
                return;
            }
            await AddAsync(passRecord);
        }
    }
}
