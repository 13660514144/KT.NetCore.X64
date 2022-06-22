using KT.Common.Core.Exceptions;
using KT.Common.Core.Helpers;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Elevator.Unit.Entity.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
{
    public class DistributeErrorService : IDistributeErrorService
    {
        private readonly IDistributeErrorDao _distributeErrorDao;
        private readonly AppSettings _appSettings;
        private readonly ILogger<DistributeErrorService> _logger;

        public DistributeErrorService(IDistributeErrorDao relayDeviceDataDao,
            IOptions<AppSettings> appSettings,
            ILogger<DistributeErrorService> logger)
        {
            _distributeErrorDao = relayDeviceDataDao;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _distributeErrorDao.HasInstanceByIdAsync(id);
        }
        public async Task<bool> IsExistsByDeviceIdAsync(string deviceId)
        {
            var error = await _distributeErrorDao.SelectFirstByLambdaAsync(x => x.DeviceId == deviceId);
            return error == null;
        }

        public async Task AddRetryAsync(DistributeErrorModel model)
        {
            await RetryHelper.StartOperateAsync(_logger,
               _appSettings.ErrorRetimes,
               new Func<Task>(async () =>
               {
                   var entity = DistributeErrorModel.ToEntity(model);
                   await _distributeErrorDao.AddAsync(entity);
               }));
        }
        public async Task DeleteAsync(string id)
        {
            await _distributeErrorDao.DeleteByIdAsync(id);
        }

        public async Task<DistributeErrorModel> EditAsync(DistributeErrorModel model)
        {
            var entity = await _distributeErrorDao.GetByIdAsync(model.Id);
            if (entity == null)
            {
                throw CustomException.Run("系统中找不到当前分发数据错误数据：id:{0} ", model.Id);
            }
            else
            {
                entity = DistributeErrorModel.SetEntity(entity, model);
                await _distributeErrorDao.EditAsync(entity);
            }

            model = DistributeErrorModel.SetModel(model, entity);

            return model;
        }

        public async Task<List<DistributeErrorModel>> GetAllAsync()
        {
            var entities = await _distributeErrorDao.GetAllAsync();

            var models = DistributeErrorModel.ToModels(entities);

            return models;
        }

        public async Task<DistributeErrorModel> GetByIdAsync(string id)
        {
            var entity = await _distributeErrorDao.GetByIdAsync(id);
            var model = DistributeErrorModel.ToModel(entity);
            return model;
        }

        public async Task<int> AddErrorTimesAsync(string id)
        {
            var entity = await _distributeErrorDao.SelectByIdAsync(id);
            entity.ErrorTimes += 1;
            await _distributeErrorDao.AttachAsync(entity);
            return entity.ErrorTimes;
        }

        public async Task<List<UnitErrorModel>> GetUnitPageAndDeleteByDeviceIdAsync(string deviceId, int page, int size)
        {
            var results = new List<UnitErrorModel>();
            var entities = await _distributeErrorDao.SelectOrderPageByLambdaAsync(x => x.DeviceId == deviceId, x => x.EditedTime, page, size);
            if (results == null)
            {
                return results;
            }
            foreach (var item in entities)
            {
                var unitError = new UnitErrorModel();
                unitError.Type = item.Type;
                unitError.DataContent = item.DataContent;
                unitError.EditedTime = item.EditedTime;

                results.Add(unitError);
            }
            await _distributeErrorDao.DeleteAsync(entities);
            return results;
        }
    }
}
