using AutoMapper;
using KT.Elevator.Manage.Service.Devices.Quanta.DistributeDatas;
using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Services
{
    public class ElevatorGroupService : IElevatorGroupService
    {
        private IElevatorGroupDao _cardDeviceDataDao;
        private IQuantaHandleElevatorDeviceDistributeDataService _handleElevatorDeviceDistribute;
        private IMapper _mapper;

        public ElevatorGroupService(IElevatorGroupDao cardDeviceDataDao,
            IQuantaHandleElevatorDeviceDistributeDataService handleElevatorDeviceDistribute,
            IMapper mapper)
        {
            _cardDeviceDataDao = cardDeviceDataDao;
            _handleElevatorDeviceDistribute = handleElevatorDeviceDistribute;
            _mapper = mapper;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _cardDeviceDataDao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _cardDeviceDataDao.DeleteByIdAsync(id);
        }

        public async Task<ElevatorGroupModel> AddOrEditAsync(ElevatorGroupModel model)
        {
            var entity = await _cardDeviceDataDao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = ElevatorGroupModel.ToEntity(model);
                await _cardDeviceDataDao.AddAsync(entity);
            }
            else
            {
                entity = ElevatorGroupModel.SetEntity(entity, model);
                await _cardDeviceDataDao.EditAsync(entity);
            }

            model = ElevatorGroupModel.SetModel(model, entity, entity.ElevatorServers);

            return model;
        }

        public async Task<List<ElevatorGroupModel>> GetAllAsync()
        {
            var entities = await _cardDeviceDataDao.GetAllAsync();

            var models = ElevatorGroupModel.ToModels(entities);

            return models;
        }

        public async Task<List<ElevatorGroupModel>> GetAllWithElevatorInfosAsync()
        {
            var entities = await _cardDeviceDataDao.GetAllWithElevatorInfosAsync();
            var models = ElevatorGroupModel.ToModels(entities);

            return models;
        }

        public async Task<ElevatorGroupModel> GetByIdAsync(string id)
        {
            var entity = await _cardDeviceDataDao.GetByIdAsync(id);
            var model = ElevatorGroupModel.ToModel(entity, entity.ElevatorServers);
            return model;
        }

    }
}
