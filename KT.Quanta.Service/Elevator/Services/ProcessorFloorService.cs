using AutoMapper;
using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
{
    public class ProcessorFloorService : IProcessorFloorService
    {
        private IProcessorFloorDao _dao;
        private IProcessorDao _processorDao;
        private IFloorDao _floorDao;
        private IElevatorDeviceInfoDeviceDistributeService _deviceInfoDistributeDeviceService;
        private readonly IMapper _mapper;

        public ProcessorFloorService(IProcessorFloorDao dao,
            IProcessorDao processorDao,
            IFloorDao floorDao,
            IElevatorDeviceInfoDeviceDistributeService deviceInfoDistributeDeviceService,
            IMapper mapper)
        {
            _dao = dao;
            _processorDao = processorDao;
            _floorDao = floorDao;
            _deviceInfoDistributeDeviceService = deviceInfoDistributeDeviceService;
            _mapper = mapper;
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

            model = _mapper.Map(entity, model);

            return model;
        }

        public async Task<List<ProcessorFloorModel>> GetAllAsync()
        {
            var entities = await _dao.SelectAllAsync();

            var models = _mapper.Map<List<ProcessorFloorModel>>(entities);

            return models;
        }

        public async Task<ProcessorFloorModel> GetByIdAsync(string id)
        {
            var entity = await _dao.SelectByIdAsync(id);
            var model = _mapper.Map<ProcessorFloorModel>(entity);
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

                        if (!string.IsNullOrEmpty(processorFloor.Floor?.Id))
                        {
                            processorFloor.Floor = await _floorDao.SelectByIdAsync(processorFloor.Floor.Id);
                        }

                        processor.ProcessorFloors.Add(processorFloor);
                    }
                    else
                    {
                        item.Id = oldProcessorFloor.Id;
                        oldProcessorFloor = ProcessorFloorModel.SetEntity(oldProcessorFloor, item);
                        oldProcessorFloor.Processor = processor;
                        oldProcessorFloor.EditedTime = now;

                        if (!string.IsNullOrEmpty(oldProcessorFloor.Floor?.Id))
                        {
                            oldProcessorFloor.Floor = await _floorDao.SelectByIdAsync(oldProcessorFloor.Floor.Id);
                        }

                        processor.ProcessorFloors.Add(oldProcessorFloor);
                    }
                }
            }

            await _processorDao.AttachAsync(processor);

            var result = _mapper.Map<ProcessorModel>(processor);
            return result;
        }

        public async Task DeleteByProcessorIdAsync(string id)
        {
            var processor = await _processorDao.GetWithFloorsByIdAsync(id);
            processor.ProcessorFloors.Clear();
            await _processorDao.AttachAsync(processor);
        }


        public async Task<ProcessorModel> GetByProcessorIdAsync(string processorId)
        {
            var processor = await _processorDao.GetWithFloorsByIdAsync(processorId);
            var deviceFloors = await GetInitByProcessorIdAsync(processorId);
            if (deviceFloors?.FirstOrDefault() == null)
            {
                return _mapper.Map<ProcessorModel>(processor);
            }
            var floors = processor?.ProcessorFloors;
            if (floors == null)
            {
                floors = new List<ProcessorFloorEntity>();
            }
            processor.ProcessorFloors = new List<ProcessorFloorEntity>();
            foreach (var item in deviceFloors)
            {
                var oldFloor = floors.FirstOrDefault(x => x.SortId == item.SortId);
                if (oldFloor != null)
                {
                    processor.ProcessorFloors.Add(oldFloor);
                }
                else
                {
                    var floor = new ProcessorFloorEntity();
                    floor.Id = IdUtil.NewId();
                    floor.SortId = item.SortId;
                    floor.Processor = processor;
                    floor.CreatedTime = floor.EditedTime = DateTimeUtil.UtcNowMillis();

                    processor.ProcessorFloors.Add(floor);
                }
            }

            await _processorDao.UpdateAsync(processor);

            //var entities = await _dao.SelectByLambdaAsync(x => x.ProcessorId == processorId);

            var models = _mapper.Map<ProcessorModel>(processor);

            return models;
        }

        public async Task<List<ProcessorModel>> GetInitAsync()
        {
            return await _deviceInfoDistributeDeviceService.GetProcessorFloorsAsync();
        }

        public async Task<List<ProcessorFloorModel>> GetInitByProcessorIdAsync(string processorId)
        {
            return await _deviceInfoDistributeDeviceService.GetInitOutputByProcessorIdAsync(processorId);
        }
    }
}
