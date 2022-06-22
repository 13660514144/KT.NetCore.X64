using KT.Common.Core.Exceptions;
using KT.Common.Core.Helpers;
using KT.Turnstile.Manage.Service.IServices;
using KT.Turnstile.Manage.Service.IDaos;
using KT.Turnstile.Model.Models;
using KT.Turnstile.Model.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KT.Turnstile.Unit.Entity.Models;
using Microsoft.Extensions.DependencyInjection;

namespace KT.Turnstile.Manage.Service.Services
{
    public class DistributeErrorService : IDistributeErrorService
    {
        private IDistributeErrorDao _distributeErrorDao;
        private AppSettings _appSettings;
        private ILogger<DistributeErrorService> _logger;
        private IProcessorService _processorService;
        private IServiceProvider _serviceProvider;

        public DistributeErrorService(IDistributeErrorDao distributeErrorDao,
            IOptions<AppSettings> appSettings,
            ILogger<DistributeErrorService> logger,
            IProcessorService processorDataService,
            IServiceProvider serviceProvider)
        {
            _distributeErrorDao = distributeErrorDao;
            _appSettings = appSettings.Value;
            _logger = logger;
            _processorService = processorDataService;
            _serviceProvider = serviceProvider;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _distributeErrorDao.HasInstanceByIdAsync(id);
        }

        public async Task AddRetryAsync(DistributeErrorModel model)
        {
            await RetryHelper.StartOperateAsync(_logger,
               _appSettings.ErrorRetimes,
               new Action(async () =>
               {
                   using (var scope = _serviceProvider.CreateScope())
                   {
                       var distributeErrorDataDao = scope.ServiceProvider.GetRequiredService<IDistributeErrorDao>();
                       var entity = DistributeErrorModel.ToEntity(model);
                       entity = await distributeErrorDataDao.AddAsync(entity);

                       //更改边缘处理器错误数据记录 
                       var processorService = scope.ServiceProvider.GetRequiredService<IProcessorService>();
                       var processor = await processorService.GetByIdAsync(entity.Processor.Id);
                       if (processor != null && !processor.HasDistributeError)
                       {
                           await processorService.EditHasDistributeErrorAsync(entity.Processor.Id, true);
                       }
                   }
               }));
        }
        public async Task DeleteAsync(string id)
        {
            await _distributeErrorDao.DeleteAsync(id);
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

        public async Task<List<DistributeErrorModel>> GetByProcessorIdAsync(string id)
        {
            var entities = await _distributeErrorDao.GetByProcessorIdAsync(id);
            var models = DistributeErrorModel.ToModels(entities);
            return models;
        }

        public async Task<int> AddErrorTimesAsync(string id)
        {
            var entity = await _distributeErrorDao.GetByIdAsync(id);
            entity.ErrorTimes += 1;
            entity = await _distributeErrorDao.EditAsync(entity);
            return entity.ErrorTimes;
        }

        public async Task<List<UnitErrorModel>> GetUnitPageAndDeleteByProcessorKeyAsync(string processorKey, int page, int size)
        {
            var results = new List<UnitErrorModel>();
            var entities = await _distributeErrorDao.SelectOrderPageByLambdaAsync(x => x.Processor.ProcessorKey == processorKey, x => x.EditedTime, page, size);
            if (results == null)
            {
                return results;
            }
            foreach (var item in entities)
            {
                var unitError = new UnitErrorModel();
                unitError.Type = item.Type;
                unitError.DataContent = item.DataContent;
                unitError.EditTime = item.EditedTime;

                results.Add(unitError);
            }
            await _distributeErrorDao.DeleteAsync(entities);
            return results;
        }
    }
}
