using KT.Common.Core.Exceptions;
using KT.Common.Core.Helpers;
using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Services
{
    public class DistributeErrorService : IDistributeErrorService
    {
        private IDistributeErrorDao _distributeErrorDao;
        private AppSettings _appSettings;
        private ILogger<DistributeErrorService> _logger; 

        public DistributeErrorService(IDistributeErrorDao relayDeviceDataDao,
            IOptions<AppSettings> appSettings,
            ILogger<DistributeErrorService> logger )
        {
            _distributeErrorDao = relayDeviceDataDao;
            _appSettings = appSettings.Value;
            _logger = logger; 
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _distributeErrorDao.HasInstanceByIdAsync(id);
        }
        public async Task<bool> IsExistsByDeviceKeyAsync(string deviceKey)
        {
            var error = await _distributeErrorDao.SelectFirstByLambdaAsync(x => x.DeviceKey == deviceKey);
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

                   ////更改边缘处理器错误数据记录 
                   //var processor = await _processorDataService.GetByKeyAsync(entity.DeviceKey);
                   //if (processor != null && !processor.HasDistributeError)
                   //{
                   //    await _processorDataService.EditHasDistributeErrorByKeyAsync(entity.DeviceKey, true);
                   //}
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
            await _distributeErrorDao.AttachUpdateAsync(entity);
            return entity.ErrorTimes;
        }

        public async Task<List<UnitErrorModel>> GetUnitPageAndDeleteByProcessorKeyAsync(string processorKey, int page, int size)
        {
            var results = new List<UnitErrorModel>();
            var entities = await _distributeErrorDao.SelectOrderPageByLambdaAsync(x => x.DeviceKey == processorKey, x => x.EditedTime, page, size);
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
