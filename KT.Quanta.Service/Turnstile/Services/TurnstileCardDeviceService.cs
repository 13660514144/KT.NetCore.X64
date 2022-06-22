using AutoMapper;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.Turnstile.Dtos;
using KT.Quanta.Service.Turnstile.IServices;
using KT.Turnstile.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Turnstile.Services
{
    public class TurnstileCardDeviceService : ITurnstileCardDeviceService
    {
        private ICardDeviceDao _cardDeviceDataDao;
        private ITurnstileCardDeviceDeviceDistributeService _cardDeviceDistribute;
        private RemoteDeviceList _remoteDeviceList;
        private readonly IMapper _mapper;

        public TurnstileCardDeviceService(ICardDeviceDao cardDeviceDataDao,
            ITurnstileCardDeviceDeviceDistributeService cardDeviceDistribute,
            RemoteDeviceList remoteDeviceList,
            IMapper mapper)
        {
            _cardDeviceDataDao = cardDeviceDataDao;
            _cardDeviceDistribute = cardDeviceDistribute;
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

            //分发数据
            await _cardDeviceDistribute.DeleteAsync(model.Id, model.EditedTime);
        }

        public async Task<TurnstileCardDeviceModel> AddOrEditAsync(TurnstileCardDeviceModel model)
        {
            model.DeviceType = DeviceTypeEnum.TURNSTILE_PROCESSOR_CARD_DEVICE.Value;

            var entity = await _cardDeviceDataDao.GetByIdAsync(model.Id);
            if (entity == null)
            {
                entity = TurnstileCardDeviceModel.ToEntity(model);
                entity = await _cardDeviceDataDao.AddAsync(entity);
            }
            else
            {
                entity = TurnstileCardDeviceModel.SetEntity(entity, model);
                entity = await _cardDeviceDataDao.EditAsync(entity);
            }
            model = _mapper.Map(entity, model);

            //正常判断是不远程设备根据通信方式，目前无通信方式暂时根据类型来判断
            if (model.BrandModel == BrandModelEnum.HIKVISION_DS_K1T672MW.Value
                || model.BrandModel == BrandModelEnum.HIKVISION_DS_K5604Z_ZZH.Value
                || model.BrandModel == BrandModelEnum.HIKVISION_DS_5607Z_ZZH.Value)
            {
                //加入远程设备列表
                var remoteDeviceInfo = TurnstileCardDeviceModel.ToRemoteDevice(model);
                await _remoteDeviceList.AddOrUpdateContentAsync(remoteDeviceInfo);
            }

            //分发数据
            await _cardDeviceDistribute.AddOrUpdateAsync(model);

            return model;
        }


        public async Task<List<TurnstileCardDeviceModel>> GetFromDeviceTypeAsync()
        {
            var entities = await _cardDeviceDataDao.GetByDeviceTypeAsync(DeviceTypeEnum.TURNSTILE_PROCESSOR_CARD_DEVICE.Value);

            var models = _mapper.Map<List<TurnstileCardDeviceModel>>(entities);

            return models;
        }

        public async Task<TurnstileCardDeviceModel> GetByIdAsync(string id)
        {
            var entity = await _cardDeviceDataDao.GetByIdAsync(id);
            var model = _mapper.Map<TurnstileCardDeviceModel>(entity);
            return model;
        }

        public async Task<List<TurnstileUnitCardDeviceEntity>> GetUnitByProcessorId(string processorId)
        {
            var results = new List<TurnstileUnitCardDeviceEntity>();
            var entities = await _cardDeviceDataDao.GetByProcessorIdAsync(processorId);

            if (entities == null || entities.FirstOrDefault() == null)
            {
                return results;
            }

            foreach (var item in entities)
            {
                var result = new TurnstileUnitCardDeviceEntity();
                result.Id = item.Id;
                result.BrandModel = item.BrandModel;
                result.DeviceType = item.CardDeviceType;
                result.PortName = item.PortName;

                result.RelayDeviceIp = item.RelayDevice?.IpAddress;
                result.RelayDevicePort = item.RelayDevice?.Port ?? 0;
                result.RelayDeviceOut = item.RelayDeviceOut;
                result.HandleElevatorDeviceId = item.HandleElevatorDeviceId;

                result.EditedTime = item.EditedTime;

                if (item.SerialConfig != null)
                {
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
