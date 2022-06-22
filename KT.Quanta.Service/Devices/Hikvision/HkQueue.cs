using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Hikvision.Models;
using KT.Quanta.Service.Helpers;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Services;
using KT.Quanta.Service.Turnstile.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Hikvision
{
    public class HkQueue : ITurnstileDataRemoteService
    {
        private ILogger<HikvisionTurnstileDataRemoteService> _logger;
        private IServiceScopeFactory _serviceScopeFactory;
        private int WaitSleep;
        public HkQueue(IServiceScopeFactory serviceScopeFactory)
        {            
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async void Init()
        {
            Listhkmodel = new List<hkmodel>();
            Models = new hkmodel();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 50;
            timer.Elapsed += RunQueue;
            timer.Enabled = true;
            timer.Start();
        }
        public async void RunQueue(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (Listhkmodel.Count > 0)
                {
                    hkmodel M = new hkmodel();
                    M = Listhkmodel[0];
                    Listhkmodel.RemoveAt(0);
                    switch (M.Mothed)
                    {
                        case "ADD":
                            await AddOrUpdatePassRightAsync(M.remoteDevice, M.hikvisionSdkService, M.model, M.face);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        public Task AddOrUpdateCardDeviceAsync(IRemoteDevice remoteDevice, TurnstileCardDeviceModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCardDeviceAsync(IRemoteDevice remoteDevice, string id, long time)
        {
            throw new NotImplementedException();
        }

        public Task DeleteHandleTurnstileDeviceAsync(IRemoteDevice remoteDevice, string id)
        {
            throw new NotImplementedException();
        }

        public Task AddOrUpdatePassRightAsync(IRemoteDevice remoteDevice, TurnstilePassRightModel model, FaceInfoModel face)
        {
            return Task.CompletedTask;
        }

        public async Task ExecuteAddOrUpdatePassRightAsync(IRemoteDevice remoteDevice,
            HikvisionDeviceExecuteQueueModel queueModel)
        {
            
        }
        private async Task AddOrUpdatePassRightAsync(IRemoteDevice remoteDevice, IHikvisionSdkService hikvisionSdkService, TurnstilePassRightModel model, FaceInfoModel face)
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
                    var personService = scope.ServiceProvider.GetRequiredService<ITurnstilePersonService>();
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
            personCard.DoorRights[0] = 1;

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

                

                if (!string.IsNullOrEmpty(model.PersonId))
                {
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
                }

                //添加卡号
                await hikvisionSdkService.SetCardAsync(model.PersonId, personCard);
            }
        }
        public Task DeletePassRightAsync(IRemoteDevice remoteDevice, TurnstilePassRightModel model)
        {          
            return Task.CompletedTask;
        }

        public async Task ExecuteDeletePassRightAsync(IRemoteDevice remoteDevice, HikvisionDeviceExecuteQueueModel queueModel)
        {            
        }

        public async Task<int> GetOutputNumAsync(IRemoteDevice remoteDevice)
        {
            return 0;
        }


        public Task AddOrUpdateCardDeviceRightGroupAsync(IRemoteDevice remoteDevice, TurnstileCardDeviceRightGroupModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCardDeviceRightGroupAsync(IRemoteDevice remoteDevice, string id, long time)
        {
            throw new NotImplementedException();
        }
        public class hkmodel
        {
            public IRemoteDevice remoteDevice { get; set; }
            public IHikvisionSdkService hikvisionSdkService { get; set; }
            public TurnstilePassRightModel model { get; set; } = new TurnstilePassRightModel();
            public FaceInfoModel face { get; set; } = new FaceInfoModel();
            public string Mothed { get; set; }
        }
        public hkmodel Models;
        public List<hkmodel> Listhkmodel;
    }
}
