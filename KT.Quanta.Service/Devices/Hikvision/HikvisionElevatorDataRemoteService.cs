using KT.Common.Core.Utils;
using KT.Elevator.Unit.Entity.Entities;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Hikvision.Models;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Services;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Hikvision
{
    /// <summary>
    /// 海康电梯远程数据服务
    /// </summary>
    public class HikvisionElevatorDataRemoteService : IElevatorDataRemoteService
    {
        private ILogger<HikvisionElevatorDataRemoteService> _logger;
        private IServiceScopeFactory _serviceScopeFactory;

        public HikvisionElevatorDataRemoteService(ILogger<HikvisionElevatorDataRemoteService> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task AddOrUpdateCardDeviceAsync(IRemoteDevice remoteDevice, CardDeviceModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCardDeviceAsync(IRemoteDevice remoteDevice, string id, long time)
        {
            throw new NotImplementedException();
        }

        public Task AddOrUpdateHandleElevatorDeviceAsync(IRemoteDevice remoteDevice, UnitHandleElevatorDeviceModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteHandleElevatorDeviceAsync(IRemoteDevice remoteDevice, string id)
        {
            throw new NotImplementedException();
        }

        public Task AddOrUpdatePassRightAsync(IRemoteDevice remoteDevice, PassRightModel model, FaceInfoModel face, PassRightModel oldModel)
        {
            if (model.Person == null)
            {
                _logger.LogWarning($"Hikvision推送权限人员信息为空！ ");
            }

            var queueData = new HikvisionAddOrUpdatePassRightQuery<PassRightModel>();
            queueData.Model = model;
            queueData.Face = face;
            queueData.OldModel = oldModel;

            var queueModel = new HikvisionDeviceExecuteQueueModel();
            queueModel.DistributeType = HikvisionDeviceExecuteDistributeTypeEnum.AddOrUpdatePassRight.Value;
            queueModel.RemoteDevice = remoteDevice;
            queueModel.Data = queueData;

            var hikvisionRemoteDevice = remoteDevice as HikvisionElevatorRemoteDevice;
            hikvisionRemoteDevice.AddExecuteAsync(queueModel);

            return Task.CompletedTask;
        }

        public async Task ExecuteAddOrUpdatePassRightAsync(IRemoteDevice remoteDevice, HikvisionDeviceExecuteQueueModel queueModel)
        {
            var data = (HikvisionAddOrUpdatePassRightQuery<PassRightModel>)queueModel.Data;
            //新增卡
            foreach (var item in remoteDevice.CommunicateDevices)
            {
                var hikvisionSdkService = item.GetLoginUserClient<IHikvisionSdkService>();
                await AddOrUpdatePassRightAsync(remoteDevice, hikvisionSdkService, data.Model, data.Face);
            }
        }

        private async Task AddOrUpdatePassRightAsync(IRemoteDevice remoteDevice, IHikvisionSdkService hikvisionSdkService, PassRightModel model, FaceInfoModel face)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var personCard = new HikvisionPersonCardQuery();
            if (model.Person == null)
            {
                if (string.IsNullOrEmpty(model.PersonId))
                {
                    _logger.LogWarning($"Hikvision推送权限人员Id为空！ ");
                }
                else
                {
                    var personService = scope.ServiceProvider.GetRequiredService<IPersonService>();
                    model.Person = await personService.GetByIdAsync(model.PersonId);
                }
            }

            if (model.Person != null)
            {
                personCard.EmployeeNo = Convert.ToUInt32(model.Person.Id);
                personCard.EmployeeName = model.Person.Name;
            }
            else
            {
                _logger.LogWarning($"Hikvision推送权限人员信息为空，使用卡号[{model.Sign}]作为人员编号与名称！ ");
                personCard.EmployeeNo = Convert.ToUInt32(model.Sign);
                personCard.EmployeeName = model.Sign;
            }

            var processorService = scope.ServiceProvider.GetRequiredService<IProcessorService>();

            var processorId = string.IsNullOrEmpty(remoteDevice.RemoteDeviceInfo.ParentId) ?
                remoteDevice.RemoteDeviceInfo.DeviceId : remoteDevice.RemoteDeviceInfo.ParentId;

            //绽楼层输出映射
            var processor = await processorService.GetWithProcessorFloorsByIdAsync(processorId);
            if (processor?.ProcessorFloors?.FirstOrDefault() != null)
            {
                if (model.Floors?.FirstOrDefault() != null && model.Floor != null)
                {
                    var processorFloor = processor.ProcessorFloors.FirstOrDefault(x => x.FloorId == model.Floor.Id);
                    if (processorFloor != null)
                    {
                        personCard.DoorRights[processorFloor.SortId - 1] = 1;
                    }
                    else
                    {
                        _logger.LogWarning($"楼层映射未绑定：floorId:{model.Floor.Id} ");
                    }

                    foreach (var item in model.Floors)
                    {
                        processorFloor = processor.ProcessorFloors.FirstOrDefault(x => x.FloorId == item.Id);
                        if (processorFloor != null)
                        {
                            personCard.DoorRights[processorFloor.SortId - 1] = 1;
                        }
                        else
                        {
                            _logger.LogWarning($"楼层映射未绑定：floorId:{item.Id} ");
                        }
                    }
                }
                else
                {
                    _logger.LogWarning($"权限无可去楼层！！！");
                }
            }
            else
            {
                _logger.LogWarning($"无楼层映射！！！");
                //默认权限
                personCard.DoorRights[0] = 1;
            }

            if (!personCard.DoorRights.Any(x => x == 1))
            {
                _logger.LogWarning($"无楼层映射，使用默认权限！！");
                //默认权限
                personCard.DoorRights[0] = 1;
            }

            _logger.LogInformation($"Hikvision开始分发通行权限：" +
                $"remoteDevice:{JsonConvert.SerializeObject(remoteDevice.RemoteDeviceInfo, JsonUtil.JsonPrintSettings)} " +
                $"passRight:{model.Print()} " +
                $"faceUrl:{face?.FaceUrl} ");

            if (model.AccessType == AccessTypeEnum.FACE.Value)
            {
                if (face == null)
                {
                    _logger.LogError($"Hikvision人脸授权人脸信息不能为空！");
                    return;
                }
                //人脸要关联卡号
                if (string.IsNullOrEmpty(model.PersonId))
                {
                    _logger.LogWarning($"Hikvision不存在关联人员id，继续下发权限！");
                    personCard.CardNo = model.Sign.PadLeft(10, '0');
                    await hikvisionSdkService.SetCardAsync(model.Sign, personCard);
                }
                else
                {
                    var passRightService = scope.ServiceProvider.GetRequiredService<IPassRightService>();
                    var passRights = await passRightService.GetByPersonIdAsync(model.PersonId);
                    var cardRight = passRights?.FirstOrDefault(x => x.AccessType == AccessTypeEnum.IC_CARD.Value || x.AccessType == AccessTypeEnum.QR_CODE.Value);
                    if (cardRight != null)
                    {
                        personCard.CardNo = cardRight.Sign.PadLeft(10, '0');
                    }
                    else
                    {
                        _logger.LogWarning($"Hikvision未查找到人员关联的卡！");
                        personCard.CardNo = model.Sign.PadLeft(10, '0');
                        await hikvisionSdkService.SetCardAsync(model.PersonId, personCard);
                    }
                }

                ////存在旧人脸数据先删除
                //var oldFace = await hikvisionSdkService.GetFaceAsync(personCard.CardNo);
                //if (!string.IsNullOrEmpty(oldFace))
                //{
                //    await hikvisionSdkService.DeleteFaceAsync(personCard.CardNo);
                //}

                ////不存在卡号数据先增加，添加人脸必需先加卡号
                //var oldCard = await hikvisionSdkService.GetCardAsync(personCard.CardNo);
                //if (oldCard == null)
                //{
                //    await hikvisionSdkService.SetCardAsync(personCard);
                //}

                //新增人脸
                var faceFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, face.FaceUrl);

                var query = new HikvisionSetFaceQuery();
                query.CardNo = personCard.CardNo;
                query.FacePath = faceFullName;
                query.CardReaderNo = personCard.CardRightPlan;
                if (string.IsNullOrEmpty(model.PersonId))
                {
                    await hikvisionSdkService.SetFaceAsync(model.Sign, query);
                }
                else
                {
                    await hikvisionSdkService.SetFaceAsync(model.PersonId, query);
                }
            }
            else
            {
                //新增卡号权限
                personCard.CardNo = model.Sign.PadLeft(10, '0');

                ////存在旧人脸先删除人脸，删除人脸后才能删除卡号
                //var oldFace = await hikvisionSdkService.GetFaceAsync(personCard.CardNo);
                //if (!string.IsNullOrEmpty(oldFace))
                //{
                //    await hikvisionSdkService.DeleteFaceAsync(personCard.CardNo);
                //}

                ////存在旧卡号数据先删除
                //var oldCard = await hikvisionSdkService.GetCardAsync(personCard.CardNo);
                //if (oldCard != null)
                //{
                //    await hikvisionSdkService.DeleteCardAsync(personCard.CardNo);
                //}

                try
                {
                    //根据人员id当卡号新增变更卡号时先删除旧卡号 //先删除人脸再删除卡
                    var personIdSign = model.PersonId.PadLeft(10, '0');
                    if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_K1T672MW.Value
                        || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_K5604Z_ZZH.Value
                        || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_5607Z_ZZH.Value)
                    {
                        var query = new HikvisionDeleteFaceQuery();
                        query.CardNo = personIdSign;
                        await hikvisionSdkService.DeleteFaceAsync(model.PersonId, query);
                    }
                    await hikvisionSdkService.DeleteCardAsync(model.PersonId, personIdSign);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"添加正常卡先删除旧人员id卡号与人脸失败：ex:{ex} ");
                }

                //添加卡号
                await hikvisionSdkService.SetCardAsync(model.PersonId, personCard);
            }
        }

        public Task DeletePassRightAsync(IRemoteDevice remoteDevice, PassRightModel model)
        {
            var queueModel = new HikvisionDeviceExecuteQueueModel();
            queueModel.DistributeType = HikvisionDeviceExecuteDistributeTypeEnum.DeletePassRight.Value;
            queueModel.RemoteDevice = remoteDevice;
            queueModel.Data = model;

            var hikvisionRemoteDevice = remoteDevice as HikvisionElevatorRemoteDevice;
            hikvisionRemoteDevice.AddExecuteAsync(queueModel);

            return Task.CompletedTask;
        }

        public async Task ExecuteDeletePassRightAsync(IRemoteDevice remoteDevice, HikvisionDeviceExecuteQueueModel queueModel)
        {
            var model = (PassRightModel)queueModel.Data;
            var sign = model.Sign.PadLeft(10, '0');

            foreach (var item in remoteDevice.CommunicateDevices)
            {
                var hikvisionSdkService = item.GetLoginUserClient<IHikvisionSdkService>();

                ////先删人脸再删除卡号，存在旧人脸先删除人脸，删除人脸后才能删除卡号
                //var oldFace = await hikvisionSdkService.GetFaceAsync(sign);
                //if (!string.IsNullOrEmpty(oldFace))
                //{
                //    await hikvisionSdkService.DeleteFaceAsync(sign);
                //}

                ////存在旧卡号数据先删除
                //var oldCard = await hikvisionSdkService.GetCardAsync(sign);
                //if (oldCard != null)
                //{
                //    await hikvisionSdkService.DeleteCardAsync(sign);
                //}

                //先删除人脸再删除卡
                if (remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_K1T672MW.Value
                    || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_K5604Z_ZZH.Value
                    || remoteDevice.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_5607Z_ZZH.Value)
                {
                    var query = new HikvisionDeleteFaceQuery();
                    query.CardNo = sign;
                    await hikvisionSdkService.DeleteFaceAsync(model.PersonId, query);
                }

                await hikvisionSdkService.DeleteCardAsync(model.PersonId, sign);
            }
        }

        public async Task<int> GetOutputNumAsync(IRemoteDevice remoteDevice)
        {
            var service = remoteDevice.CommunicateDevices.FirstOrDefault().GetLoginUserClient<IHikvisionSdkService>();
            return await service.GetCurACSDeviceDoorNumAsync();
        }
    }
}
