using AutoMapper;
using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Kone.Models;
using KT.Quanta.Service.Devices.Quanta.DistributeDatas;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Services
{
    public class ElevatorGroupService : IElevatorGroupService
    {
        private IElevatorGroupDao _dao;
        private IQuantaHandleElevatorDeviceDistributeDataService _handleElevatorDeviceDistribute;
        private RemoteDeviceList _remoteDeviceList;
        private IMapper _mapper;
        private AppSettings _appSettings;
        private ILogger<ElevatorGroupService> _logger;
        private readonly CommunicateDeviceList _communicateDeviceList;

        public ElevatorGroupService(IElevatorGroupDao cardDeviceDataDao,
            IQuantaHandleElevatorDeviceDistributeDataService handleElevatorDeviceDistribute,
            IMapper mapper,
            RemoteDeviceList remoteDeviceList,
            IOptions<AppSettings> appSettings,
            ILogger<ElevatorGroupService> logger,
            CommunicateDeviceList communicateDeviceList)
        {
            _dao = cardDeviceDataDao;
            _handleElevatorDeviceDistribute = handleElevatorDeviceDistribute;
            _mapper = mapper;
            _remoteDeviceList = remoteDeviceList;
            _appSettings = appSettings.Value;
            _logger = logger;
            _communicateDeviceList = communicateDeviceList;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _dao.HasInstanceByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            await _dao.DeleteByIdAsync(id);
        }

        public async Task<ElevatorGroupModel> AddOrEditAsync(ElevatorGroupModel model)
        {
            var entity = await _dao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = ElevatorGroupModel.ToEntity(model);
                await _dao.AddAsync(entity);
            }
            else
            {
                entity = ElevatorGroupModel.SetEntity(entity, model);
                await _dao.EditAsync(entity);
            }

            model = _mapper.Map(entity, model);

            return model;
        }

        public async Task<List<ElevatorGroupModel>> GetAllAsync()
        {
            var entities = await _dao.GetAllAsync();

            var models = _mapper.Map<List<ElevatorGroupModel>>(entities);

            return models;
        }

        public async Task<List<ElevatorGroupModel>> GetAllWithFloorsAsync()
        {
            var entities = await _dao.GetAllWithFloorsAsync();

            var models = _mapper.Map<List<ElevatorGroupModel>>(entities);

            //对楼层重新排序
            if (models?.FirstOrDefault() != null)
            {
                foreach (var item in models)
                {
                    item.ElevatorGroupFloors = item.ElevatorGroupFloors?.OrderBy(x => ConvertUtil.ToInt32(x.Floor?.PhysicsFloor, 0)).ToList();
                }
            }

            return models;
        }

        public async Task<List<ElevatorGroupModel>> GetAllWithElevatorInfosAsync()
        {
            var entities = await _dao.GetAllWithElevatorInfosAsync();
            var models = _mapper.Map<List<ElevatorGroupModel>>(entities);

            return models;
        }

        public async Task<ElevatorGroupModel> GetByIdAsync(string id)
        {
            var entity = await _dao.GetByIdAsync(id);
            var model = _mapper.Map<ElevatorGroupModel>(entity);
            return model;
        }

        public async Task InitLoadAsync()
        {
            //初始化所有本地边缘处理器
            var entities = await _dao.GetAllWithElevatorServersAndInfosAsync();
            var models = _mapper.Map<List<ElevatorGroupModel>>(entities);

            //var remoteDevices = ElevatorGroupModel.ToRemoteDevices(models);
            foreach (var item in models)
            {
                // 不能异步
                await InitRemoteDeviceAsync(item);

                await LogKoneElevatorAsync();
            }
        }

        private async Task LogKoneElevatorAsync()
        {
            var koneServerRemoteDeviceCount = await _remoteDeviceList.GetCountAsync(x =>
            x.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SERVER.Value
             && (x.RemoteDeviceInfo.ModuleType == KoneModuleTypeEnum.ELI.Value
                  || x.RemoteDeviceInfo.ModuleType == KoneModuleTypeEnum.RCGIF.Value));

            var koneServerCommunicateDeviceCount = await _communicateDeviceList.GetCountAsync(x =>
                x.CommunicateDeviceInfo.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_ELI.Value
                || x.CommunicateDeviceInfo.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_RCGIF.Value);

            _logger.LogInformation($"初始化电梯组完成：koneServerRemoteDeviceCount:{koneServerRemoteDeviceCount} koneServerCommunicateDeviceCount:{koneServerCommunicateDeviceCount} ");
        }

        private async Task InitRemoteDeviceAsync(ElevatorGroupModel item)
        {
            var group = ElevatorGroupModel.ToRemoteDevice(item);
            await _remoteDeviceList.AddAsync(group);

            foreach (var obj in item.ElevatorServers)
            {
                // 电梯服务不能异步初始化，否则通力可能电梯组未初始化完成，Mask发送查找不到电梯组
                await InitElevatorServiceAsync(item, group, obj);
            }
        }

        private async Task InitElevatorServiceAsync(ElevatorGroupModel item, RemoteDeviceModel group, ElevatorServerModel obj)
        {
            var server = ElevatorServerModel.ToRemoteDevice(obj);

            server.BrandModel = group.BrandModel;
            server.ParentId = item.Id;
            var elevatorMap = _appSettings.ElevatorServerMaps.FirstOrDefault(x => x.BrandModel == server.BrandModel
                    && server.CommunicateDeviceInfos.Any(y => y.Port == x.Port));
            if (elevatorMap == null)
            {
                _logger.LogWarning($"电梯映射不存在：brandModel:{group.BrandModel} " +
                    $"ports:{server.CommunicateDeviceInfos?.Select(x => x.Port).ToList().ToCommaString()} ");
            }
            else
            {
                server.ModuleType = elevatorMap.ModuleType;
            }
            await _remoteDeviceList.AddAsync(server);
        }

        public async Task<ElevatorGroupModel> GetWithFloorsAsync(string id)
        {
            var entity = await _dao.GetWithFloorsByIdAsync(id);
            var model = _mapper.Map<ElevatorGroupModel>(entity);
            return model;
        }
    }
}
