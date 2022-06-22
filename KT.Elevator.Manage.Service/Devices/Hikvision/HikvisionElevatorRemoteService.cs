using KT.Common.Core.Utils;
using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Devices.Hikvision.Models;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Hikvision
{
    /// <summary>
    /// 海康电梯远程数据服务
    /// </summary>
    public class HikvisionElevatorRemoteService : IElevatorRemoteService
    {
        private ILogger<HikvisionElevatorRemoteService> _logger;
        private IServiceScopeFactory _serviceScopeFactory;

        public HikvisionElevatorRemoteService(ILogger<HikvisionElevatorRemoteService> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task AddOrUpdateCardDeviceAsync(IRemoteDevice remoteDevice, CardDeviceModel model)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCardDeviceAsync(IRemoteDevice remoteDevice, string id, long time)
        {
            throw new NotImplementedException();
        }

        public async Task AddOrUpdateHandleElevatorDeviceAsync(IRemoteDevice remoteDevice, UnitHandleElevatorDeviceModel model)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteHandleElevatorDeviceAsync(IRemoteDevice remoteDevice, string id)
        {
            throw new NotImplementedException();
        }

        public async Task AddOrUpdatePassRightAsync(IRemoteDevice remoteDevice, PassRightModel model, FaceInfoModel face)
        {
            //新增卡
            var service = (IHikvisionSdkService)remoteDevice.LoginUserClient;
            var personCard = new HikvisionPersonCard();
            personCard.EmployeeNo = Convert.ToUInt32(model.Person.Number);
            personCard.EmployeeName = model.Person.Name;
            personCard.CardNo = model.Sign.PadLeft(10, '0');
            var result = await service.SetCardAsync(personCard);
            if (result)
            {
                _logger.LogInformation($"推送权限到远程设备成功：" +
                    $"remoteDevice:{ JsonConvert.SerializeObject(remoteDevice.RemoteDeviceInfo, JsonUtil.JsonSettings)} " +
                    $"personCard:{JsonConvert.SerializeObject(personCard, JsonUtil.JsonSettings)} ");
            }
            else
            {
                //存记录，下次推送
                _logger.LogWarning($"推送权限到远程设备失败：" +
                    $"remoteDevice:{ JsonConvert.SerializeObject(remoteDevice.RemoteDeviceInfo, JsonUtil.JsonSettings)} " +
                    $"personCard:{JsonConvert.SerializeObject(personCard, JsonUtil.JsonSettings)} ");
            }

            if (face != null)
            {
                //新增人脸
                await service.SetFaceAsync(personCard.CardNo, face.SourceUrl, personCard.CardRightPlan);
            }
        }

        public async Task DeletePassRightAsync(IRemoteDevice remoteDevice, PassRightModel model)
        {
            var service = (IHikvisionSdkService)remoteDevice.LoginUserClient;
            if (model.AccessType == CardTypeEnum.FACE.Value)
            {
                var cardPassRights = model.Person?.PassRights?.Where(x => x.AccessType == CardTypeEnum.IC_CARD.Value || x.AccessType == CardTypeEnum.QR_CODE.Value).ToList();
                if (cardPassRights?.FirstOrDefault() == null)
                {
                    model.Person?.PassRights?.ForEach(x => x.Person = null);
                    _logger.LogWarning($"删除人脸不存在的卡号权限：passRight:{JsonConvert.SerializeObject(model, JsonUtil.JsonSettings)} ");
                }
                foreach (var item in cardPassRights)
                {
                    await service.DeleteFaceAsync(item.Sign);
                }
            }
            else
            {
                await service.DeleteCardAsync(model.Sign);
            }
        }

        public async Task<int> GetOutputNumAsync(IRemoteDevice remoteDevice)
        {
            var service = (IHikvisionSdkService)remoteDevice.LoginUserClient;
            return await service.GetCurACSDeviceDoorNumAsync();
        }


    }
}