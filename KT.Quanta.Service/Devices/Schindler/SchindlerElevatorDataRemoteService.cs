using KT.Common.Core.Utils;
using KT.Elevator.Unit.Entity.Entities;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Schindler.Models;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Schindler
{
    public class SchindlerElevatorDataRemoteService : IElevatorDataRemoteService
    {
        private ILogger<SchindlerElevatorDataRemoteService> _logger;
        private SchindlerSettings _schindlerSettings;
        private IServiceScopeFactory _serviceScopeFactory;
        private readonly SchindlerDatabaseQueue _schindlerDatabaseQueue;
        private readonly IElevatorGroupFloorDao _elevatorGroupFloorDao;

        public SchindlerElevatorDataRemoteService(ILogger<SchindlerElevatorDataRemoteService> logger,
            IOptions<SchindlerSettings> schindlerSettings,
            IServiceScopeFactory serviceScopeFactory,
            SchindlerDatabaseQueue schindlerDatabaseQueue,
            IElevatorGroupFloorDao elevatorGroupFloorDao)
        {
            _logger = logger;
            _schindlerSettings = schindlerSettings.Value;
            _serviceScopeFactory = serviceScopeFactory;
            _schindlerDatabaseQueue = schindlerDatabaseQueue;
            _elevatorGroupFloorDao = elevatorGroupFloorDao;
        }
        public Task AddOrUpdateCardDeviceAsync(IRemoteDevice remoteDevice, CardDeviceModel model)
        {
            throw new NotImplementedException();
        }

        public Task AddOrUpdateHandleElevatorDeviceAsync(IRemoteDevice remoteDevice, UnitHandleElevatorDeviceModel model)
        {
            throw new NotImplementedException();
        }

        public async Task AddOrUpdatePassRightAsync(IRemoteDevice remoteDevice, PassRightModel model, FaceInfoModel face, PassRightModel oldModel)
        {
            //人脸下发权限
            using var scope = _serviceScopeFactory.CreateScope();
            if (model.AccessType == AccessTypeEnum.FACE.Value)
            {
                ////人脸为空也继续授权
                //if (face == null)
                //{
                //    _logger.LogError($"迅达人脸授权人脸信息为空！");
                //    return;
                //}
                //人脸要关联卡号
                if (string.IsNullOrEmpty(model.PersonId))
                {
                    _logger.LogWarning($"迅达不存在关联人员id，继续下发权限！");
                }
                else
                {
                    var passRightService = scope.ServiceProvider.GetRequiredService<IPassRightService>();
                    var passRights = await passRightService.GetByPersonIdAsync(model.PersonId);
                    var cardRight = passRights?.FirstOrDefault(x => x.AccessType == AccessTypeEnum.IC_CARD.Value || x.AccessType == AccessTypeEnum.QR_CODE.Value);
                    if (cardRight != null)
                    {
                        _logger.LogInformation($"迅达存在卡权限，人脸不用再下发权限！");
                        return;
                    }
                    else
                    {
                        _logger.LogWarning($"迅达不存在卡权限，人脸需下发权限！");
                    }
                }
            }

            //下发卡 
            var request = new SchindlerDatabaseChangeInsertPersonRequest();
            var personId = model.PersonId;
            if (!string.IsNullOrEmpty(model.Person?.Id))
            {
                personId = model.Person.Id;
            }
            if (string.IsNullOrEmpty(personId))
            {
                personId = model.Sign;
            }
            request.PersonId = long.Parse(personId);
            var elevatorGroupFloor = await _elevatorGroupFloorDao.GetByElevatorGroupIdAndFloorIdAsync(remoteDevice.RemoteDeviceInfo.DeviceId, model.Floor.Id);
            if (elevatorGroupFloor == null)
            {
                throw new Exception($"电梯组楼层为空：elevatorGroupId:{remoteDevice.RemoteDeviceInfo.DeviceId} floorId:{model.Floor.Id} ");
            }
            request.ProfileName = string.Format(_schindlerSettings.PersonProfileTemplate, Convert.ToInt32(elevatorGroupFloor.RealFloorId));
            request.BadgeNumber1 = long.Parse(model.Sign);

            var name = PinyinUtil.GetNamePinyin(model.Person?.Name);
            request.FirstName = name?.FirstName;
            request.FamilyName = name?.LastName;

            _logger.LogInformation($"迅达开始下发卡信息：{JsonConvert.SerializeObject(request, JsonUtil.JsonPrintSettings)} ");

            var buffer = request.WriteToLocalBuffer();
            var value = request.GetWriteValue();

            _logger.LogInformation($"迅达电梯发送数据：{value} ");

            var bytes = request.GetBytes(value);

            //加入下发权限队列
            var distributeModel = new SchindlerDatabaseQueueModel();
            distributeModel.DistributeType = SchindlerDatabaseDistributeTypeEnum.ChangeOrInsertPerson.Value;
            distributeModel.RemoteDevice = remoteDevice;
            distributeModel.Datas = bytes;
            _schindlerDatabaseQueue.Add(distributeModel);

            //下发权限
            var zoneRequest = new SchindlerDatabaseSetZoneAccessRequest();
            zoneRequest.PersonId = request.PersonId;

            //旧楼层权限
            var oldPassableFloors = new List<FloorModel>();
            if (oldModel.Floors?.FirstOrDefault() != null)
            {
                oldPassableFloors.AddRange(oldModel.Floors);
            }
            if (oldModel != null)
            {
                //旧可去楼层  
                if (!oldPassableFloors.Any(x => x.Id == oldModel.Floor.Id))
                {
                    oldPassableFloors.Add(oldModel.Floor);
                }
            }

            //清除旧楼层权限
            if (oldPassableFloors?.FirstOrDefault() != null)
            {
                //所在楼层不清除,所在楼层在模板中添加，不再在Zone中添加，所以不能清除
                var oldRemovePassableFloors = oldPassableFloors.Where(x => x.Id != model.Floor.Id).ToList();

                var elevatorGroupFloors = await _elevatorGroupFloorDao.GetByElevatorGroupIdAndFloorIdsAsync(remoteDevice.RemoteDeviceInfo.DeviceId, oldRemovePassableFloors.Select(x => x.Id).ToList());
                if (elevatorGroupFloors?.FirstOrDefault() != null)
                {
                    zoneRequest.SetZones1 = elevatorGroupFloors.Select(x => x.RealFloorId).ToList().ToCommaString();
                }
            }

            zoneRequest.Access1 = SchindlerDatabaseSetZoneAccessEnum.None.Value;

            //下发可去楼层
            var newPassableInFloors = new List<FloorModel>();
            if (model.Floors?.FirstOrDefault() != null)
            {
                newPassableInFloors.AddRange(model.Floors);
            }
            if (newPassableInFloors.Any(x => x.Id == model.Floor.Id))
            {
                newPassableInFloors.RemoveAll(x => x.Id == model.Floor.Id);
            }

            ////下发新楼层权限
            //var newAddPassableFloors = newPassableInFloors.Where(x => !oldPassableFloors.Any(y => y == x)).ToList();
            //zoneRequest.SetZones2 = newAddPassableFloors.ToCommaString();
            if (newPassableInFloors?.FirstOrDefault() != null)
            {
                var elevatorGroupFloors = await _elevatorGroupFloorDao.GetByElevatorGroupIdAndFloorIdsAsync(remoteDevice.RemoteDeviceInfo.DeviceId, newPassableInFloors.Select(x => x.Id).ToList());
                if (elevatorGroupFloors?.FirstOrDefault() != null)
                {
                    //下发新楼层权限
                    zoneRequest.SetZones2 = elevatorGroupFloors.Select(x => x.RealFloorId).ToList().ToCommaString();
                }
            }
            zoneRequest.Access2 = SchindlerDatabaseSetZoneAccessEnum.Always.Value;

            _logger.LogInformation($"迅达开始下发卡权限：{JsonConvert.SerializeObject(zoneRequest, JsonUtil.JsonPrintSettings)} ");

            if (string.IsNullOrEmpty(zoneRequest.SetZones1) && string.IsNullOrEmpty(zoneRequest.SetZones2))
            {
                _logger.LogWarning($"迅达开始下发卡权限为空，不再继续执行！");
                return;
            }

            var zoneBuffer = zoneRequest.WriteToLocalBuffer();
            var zoneValue = zoneRequest.GetWriteValue();

            _logger.LogInformation($"迅达电梯发送数据：{zoneValue}");

            var zoneBytes = request.GetBytes(zoneValue);

            //加入下发权限队列
            var zoneDistributeModel = new SchindlerDatabaseQueueModel();
            zoneDistributeModel.DistributeType = SchindlerDatabaseDistributeTypeEnum.ChangePersonZone.Value;
            zoneDistributeModel.RemoteDevice = remoteDevice;
            zoneDistributeModel.Datas = zoneBytes;
            _schindlerDatabaseQueue.Add(zoneDistributeModel);
        }

        public Task DeleteCardDeviceAsync(IRemoteDevice remoteDevice, string id, long time)
        {
            throw new NotImplementedException();
        }

        public Task DeleteHandleElevatorDeviceAsync(IRemoteDevice remoteDevice, string id)
        {
            throw new NotImplementedException();
        }

        public async Task DeletePassRightAsync(IRemoteDevice remoteDevice, PassRightModel model)
        {
            //人脸下发权限
            using var scope = _serviceScopeFactory.CreateScope();
            if (model.AccessType == AccessTypeEnum.FACE.Value)
            {
                //人脸要关联卡号
                if (string.IsNullOrEmpty(model.PersonId))
                {
                    _logger.LogWarning($"迅达不存在关联人员id，继续下发权限！");
                }
                else
                {
                    var passRightService = scope.ServiceProvider.GetRequiredService<IPassRightService>();
                    var passRights = await passRightService.GetByPersonIdAsync(model.PersonId);
                    var cardRight = passRights?.FirstOrDefault(x => x.AccessType == AccessTypeEnum.IC_CARD.Value || x.AccessType == AccessTypeEnum.QR_CODE.Value);
                    if (cardRight != null)
                    {
                        _logger.LogInformation($"迅达存在卡权限，人脸不用下发删除权限！");
                        return;
                    }
                    else
                    {
                        _logger.LogWarning($"迅达不存在卡权限，人脸需下发删除权限！");
                    }
                }
            }

            //下发卡
            var request = new SchindlerDatabaseDeletePersonRequest();
            var personId = model.PersonId;
            if (!string.IsNullOrEmpty(model.Person?.Id))
            {
                personId = model.Person.Id;
            }
            if (string.IsNullOrEmpty(personId))
            {
                personId = model.Sign;
            }
            request.PersonId = long.Parse(personId);

            var name = PinyinUtil.GetNamePinyin(model.Person?.Name);
            request.FamilyName = name?.FirstName;

            _logger.LogInformation($"开始下发删除权限信息：{JsonConvert.SerializeObject(request, JsonUtil.JsonPrintSettings)} ");

            var value = request.GetWriteValue();

            _logger.LogInformation($"迅达电梯发送删除权限数据：{value} ");

            var bytes = request.GetBytes(value);

            //加入下发权限队列
            var distributeModel = new SchindlerDatabaseQueueModel();
            distributeModel.DistributeType = SchindlerDatabaseDistributeTypeEnum.DeletePerson.Value;
            distributeModel.RemoteDevice = remoteDevice;
            distributeModel.Datas = bytes;
            _schindlerDatabaseQueue.Add(distributeModel);
        }

        public Task<int> GetOutputNumAsync(IRemoteDevice remoteDevice)
        {
            throw new NotImplementedException();
        }
    }
}
