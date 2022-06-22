using AutoMapper;
using KT.Elevator.Unit.Entity.Entities;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
{
    public class CardDeviceService : ICardDeviceService
    {
        private ICardDeviceDao _cardDeviceDataDao;
        private IElevatorCardDeviceDeviceDistributeService _cardDeviceDistributeService;
        private RemoteDeviceList _remoteDeviceList;
        private readonly IMapper _mapper;

        public CardDeviceService(ICardDeviceDao cardDeviceDataDao,
            IElevatorCardDeviceDeviceDistributeService cardDeviceDistributeService,
            RemoteDeviceList remoteDeviceList,
            IMapper mapper)
        {
            _cardDeviceDataDao = cardDeviceDataDao;
            _cardDeviceDistributeService = cardDeviceDistributeService;
            _remoteDeviceList = remoteDeviceList;
            _mapper = mapper;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _cardDeviceDataDao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            var model = await _cardDeviceDataDao.DeleteReturnWidthProcessorAsync(id);

            //清除静态列表数据
            await _remoteDeviceList.RemoveByIdAsync(id);

            //分发数据
            _cardDeviceDistributeService.DeleteAsync(model.Processor.Id, model.Id, model.EditedTime);
        }

        public async Task<CardDeviceModel> AddOrEditAsync(CardDeviceModel model)
        {
            model.DeviceType = DeviceTypeEnum.ELEVATOR_PROCESSOR_CARD_DEVICE.Value;

            var entity = await _cardDeviceDataDao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = CardDeviceModel.ToEntity(model);
                await _cardDeviceDataDao.AddAsync(entity);
            }
            else
            {
                entity = CardDeviceModel.SetEntity(entity, model);
                await _cardDeviceDataDao.EditAsync(entity);
            }

            entity.Processor.CardDevices = null;
            model = _mapper.Map(entity, model);

            //正常判断是不远程设备根据通信方式，目前无通信方式暂时根据类型来判断
            if (model.BrandModel == BrandModelEnum.HIKVISION_DS_K1T672MW.Value
                || model.BrandModel == BrandModelEnum.HIKVISION_DS_K5604Z_ZZH.Value
                || model.BrandModel == BrandModelEnum.HIKVISION_DS_5607Z_ZZH.Value)
            {
                //加入远程设备列表
                var remoteDeviceInfo = CardDeviceModel.ToRemoteDevice(model);
                await _remoteDeviceList.AddOrUpdateContentAsync(remoteDeviceInfo);
            }

            //分发数据
            _cardDeviceDistributeService.AddOrUpdateAsync(model);

            return model;
        }

        public async Task<List<CardDeviceModel>> GetFromDeviceTypeAsync()
        {
            var entities = await _cardDeviceDataDao.GetByDeviceTypeAsync(DeviceTypeEnum.ELEVATOR_PROCESSOR_CARD_DEVICE.Value);

            var models = _mapper.Map<List<CardDeviceModel>>(entities);

            return models;
        }

        public async Task<CardDeviceModel> GetByIdAsync(string id)
        {
            var entity = await _cardDeviceDataDao.GetByIdAsync(id);
            var model = _mapper.Map<CardDeviceModel>(entity);
            return model;
        }

        public async Task<List<UnitCardDeviceEntity>> GetUnitByProcessorId(string processorId)
        {
            var results = new List<UnitCardDeviceEntity>();
            var entities = await _cardDeviceDataDao.SelectByLambdaAsync(x => x.Processor.Id == processorId);

            if (entities == null || entities.FirstOrDefault() == null)
            {
                return results;
            }

            foreach (var item in entities)
            {
                var result = new UnitCardDeviceEntity();
                result.Id = item.Id;
                result.BrandModel = item.BrandModel;
                result.DeviceType = item.CardDeviceType;
                result.PortName = item.PortName;
                result.HandleDeviceId = item.HandleElevatorDevice?.Id;

                if (item.SerialConfig != null)
                {
                    result.SerialConfigId = item.SerialConfig.Id;
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
