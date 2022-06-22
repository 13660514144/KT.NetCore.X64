using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Devices.Quanta.DistributeDatas;
using KT.Elevator.Manage.Service.Entities;
using KT.Elevator.Manage.Service.Helpers;
using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Services
{
    public class HandleElevatorInputDeviceService : IHandleElevatorInputDeviceService
    {
        private IHandleElevatorInputDeviceDao _cardDeviceDataDao;
        private IHandleElevatorDeviceDao _handleElevatorDeviceDao;
        private IQuantaHandleElevatorDeviceDistributeDataService _cardDeviceDistribute;
        private HandleElevatorInputDeviceList _handleElevatorInputDeviceList;
        private RemoteDeviceList _remoteDeviceList;

        public HandleElevatorInputDeviceService(IHandleElevatorInputDeviceDao cardDeviceDataDao,
            IQuantaHandleElevatorDeviceDistributeDataService cardDeviceDistribute,
            HandleElevatorInputDeviceList handleElevatorInputDeviceList,
            RemoteDeviceList remoteDeviceList,
            IHandleElevatorDeviceDao handleElevatorDeviceDao)
        {
            _cardDeviceDataDao = cardDeviceDataDao;
            _cardDeviceDistribute = cardDeviceDistribute;
            _handleElevatorInputDeviceList = handleElevatorInputDeviceList;
            _remoteDeviceList = remoteDeviceList;
            _handleElevatorDeviceDao = handleElevatorDeviceDao;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _cardDeviceDataDao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            var model = await _cardDeviceDataDao.DeleteReturnWidthHandleElevatorDeviceAsync(id);
        }

        public async Task<HandleElevatorInputDeviceModel> AddOrEditAsync(HandleElevatorInputDeviceModel model)
        {
            var entity = await _cardDeviceDataDao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = HandleElevatorInputDeviceModel.ToEntity(model);
                await _cardDeviceDataDao.AddAsync(entity);
            }
            else
            {
                entity = HandleElevatorInputDeviceModel.SetEntity(entity, model);
                await _cardDeviceDataDao.EditAsync(entity);
            }

            model = HandleElevatorInputDeviceModel.SetModel(model, entity);

            return model;
        }

        public async Task<List<HandleElevatorInputDeviceModel>> GetAllAsync()
        {
            var entities = await _cardDeviceDataDao.GetAllAsync();

            var models = HandleElevatorInputDeviceModel.ToModels(entities);

            return models;
        }

        public async Task<HandleElevatorInputDeviceModel> GetByIdAsync(string id)
        {
            var entity = await _cardDeviceDataDao.GetByIdAsync(id);
            var model = HandleElevatorInputDeviceModel.ToModel(entity);
            return model;
        }


        public async Task<List<HandleElevatorInputDeviceModel>> GetStaticAsync(string cardType, string handleElevatorDeviceId)
        {
            var results = await _handleElevatorInputDeviceList.GetAllAsync((inputDevice) =>
            {
                if ((string.IsNullOrEmpty(cardType) || inputDevice.AccessType == cardType)
                && (string.IsNullOrEmpty(handleElevatorDeviceId) || inputDevice.HandleElevatorDevice?.Id == handleElevatorDeviceId))
                {
                    return inputDevice;
                }
                return null;
            });

            return results;


            //var query = _cardDeviceDataDao.SelectAll();
            //if (!string.IsNullOrEmpty(type))
            //{
            //    query = query.Where(x => x.CardType == type);
            //}
            //if (!string.IsNullOrEmpty(handleElevatorDeviceId))
            //{
            //    query = query.Where(x => x.HandElevatorDevice.Id == handleElevatorDeviceId);
            //}
            //var entities = await query.ToListAsync();
            //var models = HandleElevatorInputDeviceModel.ToModels(entities);
            //return models;
        }

        public async Task InitLoadAsync()
        {
            var models = await GetAllAsync();
            _handleElevatorInputDeviceList.Init(models);
        }

        public async Task AddOrEditUnitAsync(List<UnitHandleElevatorInputDeviceEntity> inputDevices)
        {
            if (inputDevices == null || inputDevices.FirstOrDefault() == null)
            {
                return;
            }

            foreach (var item in inputDevices)
            {
                var remoteDevice = await _remoteDeviceList.GetByKeyAsync(item.HandDeviceKey);
                if (remoteDevice == null)
                {

                    continue;
                }
                var oldEntity = await _cardDeviceDataDao.SelectFirstByLambdaAsync(x => x.HandElevatorDevice.Id == remoteDevice.RemoteDeviceInfo.DeviceId && x.AccessType == item.AccessType);
                if (oldEntity != null)
                {
                    oldEntity.AccessType = item.AccessType;
                    oldEntity.DeviceType = item.DeviceType;
                    oldEntity.Name = item.Name;
                    oldEntity.PortName = item.PortName;
                    oldEntity.HandElevatorDevice = await _handleElevatorDeviceDao.GetByIdAsync(remoteDevice.RemoteDeviceInfo.DeviceId);
                    await _cardDeviceDataDao.AttachUpdateAsync(oldEntity);
                }
                else
                {
                    oldEntity = new HandleElevatorInputDeviceEntity();
                    oldEntity.AccessType = item.AccessType;
                    oldEntity.DeviceType = item.DeviceType;
                    oldEntity.Name = item.Name;
                    oldEntity.PortName = item.PortName;
                    oldEntity.HandElevatorDevice = await _handleElevatorDeviceDao.GetByIdAsync(remoteDevice.RemoteDeviceInfo.DeviceId);
                    await _cardDeviceDataDao.AddAsync(oldEntity);
                }
                var model = HandleElevatorInputDeviceModel.ToModel(oldEntity);
                _handleElevatorInputDeviceList.AddOrUpdate(model);

            }
        }
    }
}
