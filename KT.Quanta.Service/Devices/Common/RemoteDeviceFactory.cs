using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Hikvision;
using KT.Quanta.Service.Devices.Hitachi;
using KT.Quanta.Service.Devices.Kone;
using KT.Quanta.Service.Devices.Kone.Models;
using KT.Quanta.Service.Devices.Mitsubishi.Elip;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw;
using KT.Quanta.Service.Devices.Quanta;
using KT.Quanta.Service.Devices.Schindler;
using KT.Quanta.Service.Devices.Schindler.Models;
using KT.Quanta.Service.Devices.Self;
using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Common
{
    public class RemoteDeviceFactory : IRemoteDeviceFactory
    {
        private readonly ILogger<RemoteDeviceFactory> _logger;
        private IServiceScopeFactory _serviceScopeFactory;

        public RemoteDeviceFactory(ILogger<RemoteDeviceFactory> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<List<IRemoteDevice>> CreatorAsync(RemoteDeviceModel model)
        {
            _logger.LogInformation($"初始化远程设备：{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");
            using var scope = _serviceScopeFactory.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var remoteDevices = new List<IRemoteDevice>();
            //梯控边缘处理器
            if (model.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR.Value)
            {
                //康塔边缘处理器
                if (model.BrandModel == BrandModelEnum.QSCS_3600P.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<QuantaRemoteDevice>();

                    remoteDevice.ElevatorDataRemoteService = serviceProvider.GetRequiredService<QuantaElevatorDataRemoteService>();
                    remoteDevice.ElevatorDisplayRemoteService = serviceProvider.GetRequiredService<QuantaElevatorDisplayRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    remoteDevices.Add(remoteDevice);
                }
                //海康梯控主机
                else if (model.BrandModel == BrandModelEnum.HIKVISION_DS_K2210.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<HikvisionElevatorRemoteDevice>();
                    remoteDevice.ElevatorDataRemoteService = serviceProvider.GetRequiredService<HikvisionElevatorDataRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.HIKVISION.Value, CommunicateModeTypeEnum.SDK.Value);
                    remoteDevices.Add(remoteDevice);
                }
            }
            //闸机边缘处理器
            else if (model.DeviceType == DeviceTypeEnum.TURNSTILE_PROCESSOR.Value)
            {
                //康塔边缘处理器
                if (model.BrandModel == BrandModelEnum.QSCS_3600P.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<QuantaRemoteDevice>();
                    remoteDevice.TurnstileDataRemoteService = serviceProvider.GetRequiredService<QuantaTurnstileDataRemoteService>();
                    remoteDevice.TurnstileDisplayRemoteService = serviceProvider.GetRequiredService<QuantaTurnstileDisplayRemoteService>();
                    remoteDevice.ElevatorDisplayRemoteService = serviceProvider.GetRequiredService<QuantaElevatorDisplayRemoteService>();
                    remoteDevice.TurnstileOperateRemoteService = serviceProvider.GetRequiredService<QuantaTurnstileOperateRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    remoteDevices.Add(remoteDevice);
                }
                //海康梯控主机
                else if (model.BrandModel == BrandModelEnum.HIKVISION_DS_K2210.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<HikvisionTurnstileRemoteDevice>();
                    remoteDevice.TurnstileDataRemoteService = serviceProvider.GetRequiredService<HikvisionTurnstileDataRemoteService>();
                    remoteDevice.TurnstileDisplayRemoteService = serviceProvider.GetRequiredService<QuantaTurnstileDisplayRemoteService>();
                    remoteDevice.ElevatorDisplayRemoteService = serviceProvider.GetRequiredService<QuantaElevatorDisplayRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.HIKVISION.Value, CommunicateModeTypeEnum.SDK.Value);
                    remoteDevices.Add(remoteDevice);
                }
            }
            //梯控读卡器
            else if (model.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR_CARD_DEVICE.Value)
            {
                //海康面板机7寸
                if (model.BrandModel == BrandModelEnum.HIKVISION_DS_K1T672MW.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<HikvisionElevatorRemoteDevice>();
                    remoteDevice.ElevatorDataRemoteService = serviceProvider.GetRequiredService<HikvisionElevatorDataRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.HIKVISION.Value, CommunicateModeTypeEnum.SDK.Value);
                    remoteDevices.Add(remoteDevice);
                }
                //海康面板机10寸
                else if (model.BrandModel == BrandModelEnum.HIKVISION_DS_K5604Z_ZZH.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<HikvisionElevatorRemoteDevice>();
                    remoteDevice.ElevatorDataRemoteService = serviceProvider.GetRequiredService<HikvisionElevatorDataRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.HIKVISION.Value, CommunicateModeTypeEnum.SDK.Value);
                    remoteDevices.Add(remoteDevice);
                }
                //海康面板机10寸
                else if (model.BrandModel == BrandModelEnum.HIKVISION_DS_5607Z_ZZH.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<HikvisionElevatorRemoteDevice>();
                    remoteDevice.ElevatorDataRemoteService = serviceProvider.GetRequiredService<HikvisionElevatorDataRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.HIKVISION.Value, CommunicateModeTypeEnum.SDK.Value);
                    remoteDevices.Add(remoteDevice);
                }
            }
            //闸机读卡器
            else if (model.DeviceType == DeviceTypeEnum.TURNSTILE_PROCESSOR_CARD_DEVICE.Value)
            {
                //海康面板机7寸
                if (model.BrandModel == BrandModelEnum.HIKVISION_DS_K1T672MW.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<HikvisionTurnstileRemoteDevice>();
                    remoteDevice.TurnstileDataRemoteService = serviceProvider.GetRequiredService<HikvisionTurnstileDataRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.HIKVISION.Value, CommunicateModeTypeEnum.SDK.Value);
                    remoteDevices.Add(remoteDevice);
                }
                //海康面板机10寸
                else if (model.BrandModel == BrandModelEnum.HIKVISION_DS_K5604Z_ZZH.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<HikvisionTurnstileRemoteDevice>();
                    remoteDevice.TurnstileDataRemoteService = serviceProvider.GetRequiredService<HikvisionTurnstileDataRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.HIKVISION.Value, CommunicateModeTypeEnum.SDK.Value);
                    remoteDevices.Add(remoteDevice);
                }
                //海康面板机10寸
                else if (model.BrandModel == BrandModelEnum.HIKVISION_DS_5607Z_ZZH.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<HikvisionTurnstileRemoteDevice>();
                    remoteDevice.TurnstileDataRemoteService = serviceProvider.GetRequiredService<HikvisionTurnstileDataRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.HIKVISION.Value, CommunicateModeTypeEnum.SDK.Value);
                    remoteDevices.Add(remoteDevice);
                }
            }
            //闸机派梯显示屏
            else if (model.DeviceType == DeviceTypeEnum.ELEVATOR_GATE_DISPLAY.Value)
            {
                if (model.BrandModel == BrandModelEnum.QSCS_2050WVH_GS.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<QuantaRemoteDevice>();
                    remoteDevice.ElevatorDisplayRemoteService = serviceProvider.GetRequiredService<QuantaElevatorDisplayRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    remoteDevices.Add(remoteDevice);
                }
                else if (model.BrandModel == BrandModelEnum.QIACS_2050WVH_GS.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<QuantaRemoteDevice>();
                    remoteDevice.ElevatorDisplayRemoteService = serviceProvider.GetRequiredService<QuantaElevatorDisplayRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    remoteDevices.Add(remoteDevice);
                }
            }
            //电梯厅选层器
            else if (model.DeviceType == DeviceTypeEnum.ELEVATOR_SELECTOR.Value)
            {
                if (model.BrandModel == BrandModelEnum.QSCS_DLS81.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<QuantaRemoteDevice>();
                    remoteDevice.ElevatorSelectorRemoteService = serviceProvider.GetRequiredService<QuantaElevatorSelectorRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    remoteDevices.Add(remoteDevice);
                }
                else if (model.BrandModel == BrandModelEnum.QIACS_DLS81.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<QuantaRemoteDevice>();
                    remoteDevice.ElevatorSelectorRemoteService = serviceProvider.GetRequiredService<QuantaElevatorSelectorRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    remoteDevices.Add(remoteDevice);
                }
                else if (model.BrandModel == BrandModelEnum.RF_AX400.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<SelfRemoteDevice>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevices.Add(remoteDevice);
                }
                else if (model.BrandModel == BrandModelEnum.SCHINDLER_PORT_SELECTOR.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<SchindlerElevatorSelectorRemoteDevice>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevices.Add(remoteDevice);
                }
                else if (model.BrandModel == BrandModelEnum.KONE_DCS_SELECTOR.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<KoneElevatorSelectorRemoteDevice>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevices.Add(remoteDevice);
                }
                else if (model.BrandModel == BrandModelEnum.MITSUBISHI_ELSGW_SELECTOR.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<MitsubishiElsgwElevatorSelectorRemoteDevice>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevices.Add(remoteDevice);
                }
                else if (model.BrandModel == BrandModelEnum.MITSUBISHI_ELIP_SELECTOR.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<MitsubishiElipElevatorSelectorRemoteDevice>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevices.Add(remoteDevice);
                }
            }
            //派梯客户端 2021-06-18  增加
            else if (model.DeviceType == DeviceTypeEnum.ELEVATOR_CLIENT.Value)
            {
                var remoteDevice = serviceProvider.GetRequiredService<QuantaRemoteDevice>();
                remoteDevice.ElevatorDataRemoteService = serviceProvider.GetRequiredService<QuantaElevatorDataRemoteService>();
                remoteDevice.ElevatorDisplayRemoteService = serviceProvider.GetRequiredService<QuantaElevatorDisplayRemoteService>();
                remoteDevice.ElevatorSelectorRemoteService = serviceProvider.GetRequiredService<QuantaElevatorSelectorRemoteService>();
                remoteDevice.RemoteDeviceInfo = model;
                remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                remoteDevices.Add(remoteDevice);
            }
            //二次派梯一体机
            else if (model.DeviceType == DeviceTypeEnum.ELEVATOR_SECONDARY.Value)
            {
                if (model.BrandModel == BrandModelEnum.QSCS_DLS81.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<QuantaRemoteDevice>();
                    remoteDevice.ElevatorDataRemoteService = serviceProvider.GetRequiredService<QuantaElevatorDataRemoteService>();
                    remoteDevice.ElevatorDisplayRemoteService = serviceProvider.GetRequiredService<QuantaElevatorDisplayRemoteService>();
                    remoteDevice.ElevatorSelectorRemoteService = serviceProvider.GetRequiredService<QuantaElevatorSelectorRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    remoteDevices.Add(remoteDevice);
                }
                else if (model.BrandModel == BrandModelEnum.QIACS_DLS81.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<QuantaRemoteDevice>();
                    remoteDevice.ElevatorDataRemoteService = serviceProvider.GetRequiredService<QuantaElevatorDataRemoteService>();
                    remoteDevice.ElevatorDisplayRemoteService = serviceProvider.GetRequiredService<QuantaElevatorDisplayRemoteService>();
                    remoteDevice.ElevatorSelectorRemoteService = serviceProvider.GetRequiredService<QuantaElevatorSelectorRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    remoteDevices.Add(remoteDevice);
                }
            }
            //电梯组
            else if (model.DeviceType == DeviceTypeEnum.ELEVATOR_SERVER_GROUP.Value)
            {
                //通力
                if (model.BrandModel == BrandModelEnum.KONE_DCS.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<KoneElevatorGroupRemoteDevice>();
                    remoteDevice.HandleElevatorRemoteService = serviceProvider.GetRequiredService<KoneHandleElevatorRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevices.Add(remoteDevice);
                }
                //迅达
                else if (model.BrandModel == BrandModelEnum.SCHINDLER_PORT.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<SchindlerElevatorGroupRemoteDevice>();
                    remoteDevice.HandleElevatorRemoteService = serviceProvider.GetRequiredService<SchindlerHandleElevatorRemoteService>();
                    remoteDevice.ElevatorDataRemoteService = serviceProvider.GetRequiredService<SchindlerElevatorDataRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevices.Add(remoteDevice);
                }
                //三菱ELSGW
                else if (model.BrandModel == BrandModelEnum.MITSUBISHI_ELSGW.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<MitsubishiElsgwElevatorGroupRemoteDevice>();
                    remoteDevice.HandleElevatorRemoteService = serviceProvider.GetRequiredService<MitsubishiElsgwHandleElevatorRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevices.Add(remoteDevice);
                }
                //三菱ELIP
                else if (model.BrandModel == BrandModelEnum.MITSUBISHI_ELIP.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<MitsubishiElipElevatorGroupRemoteDevice>();
                    remoteDevice.HandleElevatorRemoteService = serviceProvider.GetRequiredService<MitsubishiElipHandleElevatorRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevices.Add(remoteDevice);
                }
                //日立
                if (model.BrandModel == BrandModelEnum.HITACHI_DFRS.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<HitachiElevatorGroupRemoteDevice>();
                    remoteDevice.HandleElevatorRemoteService = serviceProvider.GetRequiredService<HitachiHandleElevatorRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevices.Add(remoteDevice);
                }
            }
            //电梯服务
            else if (model.DeviceType == DeviceTypeEnum.ELEVATOR_SERVER.Value)
            {
                //通力
                if (model.BrandModel == BrandModelEnum.KONE_DCS.Value)
                {
                    //派梯
                    if (model.ModuleType == KoneModuleTypeEnum.RCGIF.Value)
                    {
                        var remoteDevice = serviceProvider.GetRequiredService<KoneElevatorServerRemoteDevice>();
                        remoteDevice.HandleElevatorRemoteService = serviceProvider.GetRequiredService<KoneHandleElevatorRemoteService>();
                        remoteDevice.RemoteDeviceInfo = model;
                        remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.KONE_RCGIF.Value, CommunicateModeTypeEnum.TCP.Value);
                        remoteDevices.Add(remoteDevice);
                    }
                    //选层器面板
                    else if (model.ModuleType == KoneModuleTypeEnum.ELI.Value)
                    {
                        var remoteDevice = serviceProvider.GetRequiredService<KoneElevatorServerRemoteDevice>();
                        remoteDevice.HandleElevatorRemoteService = serviceProvider.GetRequiredService<KoneHandleElevatorRemoteService>();
                        remoteDevice.RemoteDeviceInfo = model;
                        remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.KONE_ELI.Value, CommunicateModeTypeEnum.TCP.Value);
                        remoteDevices.Add(remoteDevice);
                    }
                }
                //迅达
                else if (model.BrandModel == BrandModelEnum.SCHINDLER_PORT.Value)
                {
                    //数据同步
                    if (model.ModuleType == SchindlerModuleTypeEnum.SYNC.Value)
                    {
                        var remoteDevice = serviceProvider.GetRequiredService<SchindlerElevatorServerRemoteDevice>();
                        remoteDevice.ElevatorDataRemoteService = serviceProvider.GetRequiredService<SchindlerElevatorDataRemoteService>();
                        remoteDevice.RemoteDeviceInfo = model;
                        remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.SCHINDLER_SYNC.Value, CommunicateModeTypeEnum.TCP.Value);
                        remoteDevices.Add(remoteDevice);
                    }
                    //派梯
                    else if (model.ModuleType == SchindlerModuleTypeEnum.DISPATCH.Value)
                    {
                        var remoteDevice = serviceProvider.GetRequiredService<SchindlerElevatorServerRemoteDevice>();
                        remoteDevice.HandleElevatorRemoteService = serviceProvider.GetRequiredService<SchindlerHandleElevatorRemoteService>();
                        remoteDevice.RemoteDeviceInfo = model;
                        remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.SCHINDLER_DISPATCH.Value, CommunicateModeTypeEnum.TCP.Value);
                        remoteDevices.Add(remoteDevice);
                    }
                    //记录
                    else if (model.ModuleType == SchindlerModuleTypeEnum.RECORD.Value)
                    {
                        var remoteDevice = serviceProvider.GetRequiredService<SchindlerElevatorServerRemoteDevice>();
                        remoteDevice.ElevatorRecordRemoteService = serviceProvider.GetRequiredService<SchindlerElevatorRecordRemoteService>();
                        remoteDevice.RemoteDeviceInfo = model;
                        remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.SCHINDLER_RECORD.Value, CommunicateModeTypeEnum.TCP.Value);
                        remoteDevices.Add(remoteDevice);
                    }
                }
                //三菱ELSGW
                if (model.BrandModel == BrandModelEnum.MITSUBISHI_ELSGW.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<MitsubishiElsgwElevatorServerRemoteDevice>();
                    remoteDevice.HandleElevatorRemoteService = serviceProvider.GetRequiredService<MitsubishiElsgwHandleElevatorRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.MITSUBISHI_ELSGW.Value, CommunicateModeTypeEnum.TCP.Value);
                    remoteDevices.Add(remoteDevice);
                }
                //三菱E-LIP
                if (model.BrandModel == BrandModelEnum.MITSUBISHI_ELIP.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<MitsubishiElipElevatorServerRemoteDevice>();
                    remoteDevice.HandleElevatorRemoteService = serviceProvider.GetRequiredService<MitsubishiElipHandleElevatorRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.MITSUBISHI_ELIP.Value, CommunicateModeTypeEnum.TCP.Value);
                    remoteDevices.Add(remoteDevice);
                }
                //日立
                if (model.BrandModel == BrandModelEnum.HITACHI_DFRS.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<QuantaRemoteDevice>();
                    remoteDevice.ElevatorDataRemoteService = serviceProvider.GetRequiredService<HitachiElevatorDataRemoteService>();
                    remoteDevice.HandleElevatorRemoteService = serviceProvider.GetRequiredService<HitachiHandleElevatorRemoteService>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevice.RemoteDeviceInfo.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    remoteDevices.Add(remoteDevice);
                }
            }
            //本体屏
            else if (model.DeviceType == DeviceTypeEnum.ELEVATOR_SELF_DISPLAY.Value)
            {
                if (model.BrandModel == BrandModelEnum.QIACS_2050WVH_GS.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<SelfRemoteDevice>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevices.Add(remoteDevice);
                }
                else if (model.BrandModel == BrandModelEnum.QSCS_2050WVH_GS.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<SelfRemoteDevice>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevices.Add(remoteDevice);
                }
                else if (model.BrandModel == BrandModelEnum.QIACS_SCREEN100.Value)
                {
                    var remoteDevice = serviceProvider.GetRequiredService<SelfRemoteDevice>();
                    remoteDevice.RemoteDeviceInfo = model;
                    remoteDevices.Add(remoteDevice);
                }
            }

            if (remoteDevices?.FirstOrDefault() == null)
            {
                _logger.LogError($"初始化远程设备出错：message:not found the brand model. data:{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");
                return null;
            }

            foreach (var item in remoteDevices)
            {
                await item.InitAsync(item.RemoteDeviceInfo);
            }

            return remoteDevices;
        }

        private List<CommunicateDeviceInfoModel> SetCommunicateTypeByDeviceType(List<CommunicateDeviceInfoModel> communicateDeviceInfos,
            string communicateDeviceType,
            string communicateModeType)
        {
            foreach (var item in communicateDeviceInfos)
            {
                item.CommunicateDeviceType = communicateDeviceType;
                item.CommunicateModeType = communicateModeType;
            }
            return communicateDeviceInfos;
        }

        public RemoteDeviceModel RefreshCommunicateTypeByDeviceType(RemoteDeviceModel model)
        {
            //梯控边缘处理器
            if (model.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR.Value)
            {
                //康塔边缘处理器
                if (model.BrandModel == BrandModelEnum.QSCS_3600P.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    return model;
                }
                //海康梯控主机
                else if (model.BrandModel == BrandModelEnum.HIKVISION_DS_K2210.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.HIKVISION.Value, CommunicateModeTypeEnum.SDK.Value);
                    return model;
                }
            }
            //闸机边缘处理器
            else if (model.DeviceType == DeviceTypeEnum.TURNSTILE_PROCESSOR.Value)
            {
                //康塔边缘处理器
                if (model.BrandModel == BrandModelEnum.QSCS_3600P.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    return model;
                }
                //海康梯控主机
                else if (model.BrandModel == BrandModelEnum.HIKVISION_DS_K2210.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.HIKVISION.Value, CommunicateModeTypeEnum.SDK.Value);
                    return model;
                }
            }
            //梯控读卡器
            else if (model.DeviceType == DeviceTypeEnum.ELEVATOR_PROCESSOR_CARD_DEVICE.Value)
            {
                //海康面板机7寸
                if (model.BrandModel == BrandModelEnum.HIKVISION_DS_K1T672MW.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.HIKVISION.Value, CommunicateModeTypeEnum.SDK.Value);
                    return model;
                }
                //海康面板机10寸
                else if (model.BrandModel == BrandModelEnum.HIKVISION_DS_K5604Z_ZZH.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.HIKVISION.Value, CommunicateModeTypeEnum.SDK.Value);
                    return model;
                }
                //海康面板机10寸
                else if (model.BrandModel == BrandModelEnum.HIKVISION_DS_5607Z_ZZH.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.HIKVISION.Value, CommunicateModeTypeEnum.SDK.Value);
                    return model;
                }
            }
            //闸机读卡器
            else if (model.DeviceType == DeviceTypeEnum.TURNSTILE_PROCESSOR_CARD_DEVICE.Value)
            {
                //海康面板机7寸
                if (model.BrandModel == BrandModelEnum.HIKVISION_DS_K1T672MW.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.HIKVISION.Value, CommunicateModeTypeEnum.SDK.Value);
                    return model;
                }
                //海康面板机10寸
                else if (model.BrandModel == BrandModelEnum.HIKVISION_DS_K5604Z_ZZH.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.HIKVISION.Value, CommunicateModeTypeEnum.SDK.Value);
                    return model;
                }
                //海康面板机10寸
                else if (model.BrandModel == BrandModelEnum.HIKVISION_DS_5607Z_ZZH.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.HIKVISION.Value, CommunicateModeTypeEnum.SDK.Value);
                    return model;
                }
            }
            //闸机派梯显示屏
            else if (model.DeviceType == DeviceTypeEnum.ELEVATOR_GATE_DISPLAY.Value)
            {
                if (model.BrandModel == BrandModelEnum.QSCS_2050WVH_GS.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    return model;
                }
                else if (model.BrandModel == BrandModelEnum.QIACS_2050WVH_GS.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    return model;
                }
            }
            //电梯厅选层器
            else if (model.DeviceType == DeviceTypeEnum.ELEVATOR_SELECTOR.Value)
            {
                if (model.BrandModel == BrandModelEnum.QSCS_DLS81.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    return model;
                }
                else if (model.BrandModel == BrandModelEnum.QIACS_DLS81.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    return model;
                }
                //派梯客户端
                else if (model.BrandModel == BrandModelEnum.HITACHI_ELE_3600.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    return model;
                }
                else if (model.BrandModel == BrandModelEnum.RF_AX400.Value)
                {
                    return model;
                }
                else if (model.BrandModel == BrandModelEnum.SCHINDLER_PORT_SELECTOR.Value)
                {
                    return model;
                }
                else if (model.BrandModel == BrandModelEnum.KONE_DCS_SELECTOR.Value)
                {
                    return model;
                }
                else if (model.BrandModel == BrandModelEnum.HITACHI_DFRS_SELECTOR.Value)
                {
                    return model;
                }
                else if (model.BrandModel == BrandModelEnum.MITSUBISHI_ELSGW_SELECTOR.Value)
                {
                    return model;
                }
                else if (model.BrandModel == BrandModelEnum.MITSUBISHI_ELIP_SELECTOR.Value)
                {
                    return model;
                }
                else if (model.BrandModel == BrandModelEnum.HITACHI_DFRS_SELECTOR.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    return model;
                }
            }
            //二次派梯一体机
            else if (model.DeviceType == DeviceTypeEnum.ELEVATOR_SECONDARY.Value)
            {
                if (model.BrandModel == BrandModelEnum.QSCS_DLS81.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    return model;
                }
                else if (model.BrandModel == BrandModelEnum.QIACS_DLS81.Value)
                {
                    model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                    return model;
                }
            }
            //派梯客户端
            else if (model.DeviceType==DeviceTypeEnum.ELEVATOR_CLIENT.Value)
            {
                model.CommunicateDeviceInfos = SetCommunicateTypeByDeviceType(model.CommunicateDeviceInfos, CommunicateDeviceTypeEnum.QUANTA.Value, CommunicateModeTypeEnum.SIGNAL_R.Value);
                return model;
            }
            else if (model.DeviceType == DeviceTypeEnum.ELEVATOR_SELF_DISPLAY.Value)
            {
                if (model.BrandModel == BrandModelEnum.QIACS_SCREEN100.Value)
                {
                    //本体屏不存在设备连接
                    return model;
                }
            }
            _logger.LogError($"初始化远程设备通信类型出错：message:not found the brand model. data:{JsonConvert.SerializeObject(model, JsonUtil.JsonPrintSettings)} ");

            return model;
        }
    }
}
