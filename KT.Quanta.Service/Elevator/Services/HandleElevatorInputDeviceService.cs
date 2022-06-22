using AutoMapper;
using KT.Common.Core.Utils;
using KT.Elevator.Unit.Entity.Entities;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Quanta.DistributeDatas;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Helpers;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
{
    public class HandleElevatorInputDeviceService : IHandleElevatorInputDeviceService
    {
        private IHandleElevatorInputDeviceDao _cardDeviceDataDao;
        private IHandleElevatorDeviceDao _handleElevatorDeviceDao;
        private IQuantaHandleElevatorDeviceDistributeDataService _cardDeviceDistribute;
        private HandleElevatorInputDeviceList _handleElevatorInputDeviceList;
        private RemoteDeviceList _remoteDeviceList;
        private ILogger<HandleElevatorInputDeviceService> _logger;
        private readonly IMapper _mapper;

        public HandleElevatorInputDeviceService(IHandleElevatorInputDeviceDao cardDeviceDataDao,
            IQuantaHandleElevatorDeviceDistributeDataService cardDeviceDistribute,
            HandleElevatorInputDeviceList handleElevatorInputDeviceList,
            RemoteDeviceList remoteDeviceList,
            IHandleElevatorDeviceDao handleElevatorDeviceDao,
            ILogger<HandleElevatorInputDeviceService> logger,
            IMapper mapper)
        {
            _cardDeviceDataDao = cardDeviceDataDao;
            _cardDeviceDistribute = cardDeviceDistribute;
            _handleElevatorInputDeviceList = handleElevatorInputDeviceList;
            _remoteDeviceList = remoteDeviceList;
            _handleElevatorDeviceDao = handleElevatorDeviceDao;
            _logger = logger;
            _mapper = mapper;
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

            model = _mapper.Map(entity, model);

            return model;
        }

        public async Task<List<HandleElevatorInputDeviceModel>> GetAllAsync()
        {
            var entities = await _cardDeviceDataDao.GetAllAsync();

            var models = _mapper.Map<List<HandleElevatorInputDeviceModel>>(entities);

            return models;
        }

        public async Task<HandleElevatorInputDeviceModel> GetByIdAsync(string id)
        {
            var entity = await _cardDeviceDataDao.GetByIdAsync(id);
            var model = _mapper.Map<HandleElevatorInputDeviceModel>(entity);
            return model;
        }


        public async Task<List<HandleElevatorInputDeviceModel>> GetStaticAsync(string cardType, string handleElevatorDeviceId)
        {
            //读卡器
            var results = await _handleElevatorInputDeviceList.GetAllAsync((inputDevice) =>
            {
                if ((string.IsNullOrEmpty(cardType) || inputDevice.AccessType == cardType)
                && (string.IsNullOrEmpty(handleElevatorDeviceId) || inputDevice.HandleElevatorDevice?.Id == handleElevatorDeviceId))
                {
                    return inputDevice;
                }
                return null;
            });

            if (results == null)
            {
                results = new List<HandleElevatorInputDeviceModel>();
            }

            //海康设备
            var faceRemoteDevices = await _remoteDeviceList.GetAllAsync(x => x.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_K1T672MW.Value
                            || x.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_K5604Z_ZZH.Value
                            || x.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_5607Z_ZZH.Value);

            if (faceRemoteDevices?.FirstOrDefault() != null)
            {
                foreach (var item in faceRemoteDevices)
                {
                    var faceDevice = new HandleElevatorInputDeviceModel();
                    faceDevice.Id = item.RemoteDeviceInfo.DeviceId;
                    faceDevice.Name = item.RemoteDeviceInfo.Name;
                    faceDevice.AccessType = AccessTypeEnum.FACE.Value;
                    faceDevice.DeviceType = item.RemoteDeviceInfo.CardDeviceType;
                    faceDevice.IsOnline = item.CommunicateDevices.Any(y => y.CommunicateDeviceInfo.IsOnline);
                    faceDevice.PortName = item.CommunicateDevices
                        .Select(x => $"{x.CommunicateDeviceInfo?.IpAddress}{GetPortString(x.CommunicateDeviceInfo?.Port)}")
                        .ToList()
                        .ToCommaString();
                    results.Add(faceDevice);
                }
            }


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
        private string GetPortString(int? port)
        {
            return (port.HasValue && port.Value > 0) ? $":{port }" : string.Empty;
        }

        public async Task InitLoadAsync()
        {
            var models = await GetAllAsync();
            _handleElevatorInputDeviceList.Init(models);
        }

        public async Task AddOrEditUnitAsync(List<UnitHandleElevatorInputDeviceEntity> inputDevices, IRemoteDevice remoteDevice)
        {
            if (inputDevices == null || inputDevices.FirstOrDefault() == null)
            {
                return;
            }

            foreach (var item in inputDevices)
            {
                var oldEntity = await _cardDeviceDataDao.SelectFirstByLambdaAsync(x => x.HandElevatorDevice.Id == remoteDevice.RemoteDeviceInfo.DeviceId && x.AccessType == item.AccessType);
                if (oldEntity != null)
                {
                    oldEntity.AccessType = item.AccessType;
                    oldEntity.DeviceType = item.DeviceType;
                    oldEntity.Name = item.Name;
                    oldEntity.PortName = item.PortName;
                    oldEntity.HandElevatorDevice = await _handleElevatorDeviceDao.GetByIdAsync(remoteDevice.RemoteDeviceInfo.DeviceId);
                    await _cardDeviceDataDao.AttachAsync(oldEntity);
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

                var model = _mapper.Map<HandleElevatorInputDeviceModel>(oldEntity);
                _handleElevatorInputDeviceList.AddOrUpdate(model);
            }
        }
    }
}
