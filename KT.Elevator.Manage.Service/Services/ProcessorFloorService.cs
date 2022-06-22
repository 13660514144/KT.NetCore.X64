using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Devices.DeviceDistributes;
using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Services
{
    public class ProcessorFloorService : IProcessorFloorService
    {
        private IProcessorFloorDao _dao;
        private IProcessorDao _processorDao;
        private IFloorDao _floorDao;
        private IDeviceInfoDeviceDistributeService _deviceInfoDistributeDeviceService;

        public ProcessorFloorService(IProcessorFloorDao dao,
            IProcessorDao processorDao,
            IFloorDao floorDao,
            IDeviceInfoDeviceDistributeService deviceInfoDistributeDeviceService)
        {
            _dao = dao;
            _processorDao = processorDao;
            _floorDao = floorDao;
            _deviceInfoDistributeDeviceService = deviceInfoDistributeDeviceService;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _dao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _dao.DeleteByIdAsync(id);
        }

        public async Task<ProcessorFloorModel> AddOrEditAsync(ProcessorFloorModel model)
        {
            var entity = await _dao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = ProcessorFloorModel.ToEntity(model);
                await _dao.AddAsync(entity);
            }
            else
            {
                entity = ProcessorFloorModel.SetEntity(entity, model);
                await _dao.EditAsync(entity);
            }

            model = ProcessorFloorModel.SetModel(model, entity);

            return model;
        }

        public async Task<List<ProcessorFloorModel>> GetAllAsync()
        {
            var entities = await _dao.SelectAllAsync();

            var models = ProcessorFloorModel.ToModels(entities);

            return models;
        }

        public async Task<ProcessorFloorModel> GetByIdAsync(string id)
        {
            var entity = await _dao.SelectByIdAsync(id);
            var model = ProcessorFloorModel.ToModel(entity);
            return model;
        }

        public async Task<ProcessorModel> AddOrEditByProcessorIdAsync(ProcessorModel model)
        {
            var now = DateTimeUtil.UtcNowMillis();
            var processor = await _processorDao.GetWithFloorsByIdAsync(model.Id);
            if (processor == null)
            {
                throw CustomException.Run($"找不到边缘处理器信息：id:{model.Id} ");
            }
            processor.ProcessorFloors = new List<Entities.ProcessorFloorEntity>();
            if (model.ProcessorFloors?.FirstOrDefault() != null)
            {
                foreach (var item in model.ProcessorFloors)
                {
                    var oldProcessorFloor = await _dao.GetWithFloorByProcessorIdAndSortIdAsync(model.Id, item.SortId);
                    if (oldProcessorFloor == null)
                    {
                        var processorFloor = ProcessorFloorModel.ToEntity(item);
                        if (processorFloor.Id.IsIdNull())
                        {
                            processorFloor.Id = IdUtil.NewId();
                        }
                        processorFloor.Processor = processor;
                        processorFloor.CreatedTime = processorFloor.EditedTime = now;

                        processorFloor.Floor = await _floorDao.SelectByIdAsync(processorFloor.Floor.Id);

                        processor.ProcessorFloors.Add(processorFloor);
                    }
                    else
                    {
                        item.Id = oldProcessorFloor.Id;
                        oldProcessorFloor = ProcessorFloorModel.SetEntity(oldProcessorFloor, item); 
                        oldProcessorFloor.Processor = processor;
                        oldProcessorFloor.EditedTime = now;

                        oldProcessorFloor.Floor = await _floorDao.SelectByIdAsync(oldProcessorFloor.Floor.Id);

                        processor.ProcessorFloors.Add(oldProcessorFloor);
                    }
                }
            }

            await _processorDao.AttachUpdateAsync(processor);
             
            var result = ProcessorModel.ToModel(processor);
            return result;
        }

        public async Task DeleteByProcessorIdAsync(string id)
        {
            var processor = await _processorDao.GetWithFloorsByIdAsync(id);
            processor.ProcessorFloors.Clear();
            await _processorDao.AttachUpdateAsync(processor);
        }

        public async Task<List<ProcessorFloorModel>> GetByProcessorIdAsync(string processorId)
        {
            var entities = await _dao.SelectByLambdaAsync(x => x.ProcessorId == processorId);

            var models = ProcessorFloorModel.ToModels(entities);

            return models;
        }

        public async Task<List<ProcessorModel>> GetInitAsync()
        {
            return await _deviceInfoDistributeDeviceService.GetProcessorFloorsAsync();
        }

        public async Task<List<ProcessorFloorModel>> GetInitByProcessorIdAsync(string id)
        {
            return await _deviceInfoDistributeDeviceService.GetInitByProcessorIdAsync(id);
        }
    }
}
