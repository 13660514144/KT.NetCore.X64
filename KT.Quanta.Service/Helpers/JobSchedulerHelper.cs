using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Kone.Models;
using KT.Quanta.Service.Handlers;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Helpers
{
    /// <summary>
    /// 定时工作
    /// </summary>
    public class JobSchedulerHelper
    {
        private PushRecordHandler _pushRecordHanlder;
        private RemoteDeviceList _remoteDeviceList;
        private CommunicateDeviceList _communicateDeviceList;
        private IServiceScopeFactory _serviceScopeFactory;
        private AppSettings _appSettings;
        private ILogger<JobSchedulerHelper> _logger;
        private readonly KoneSettings _koneSettings;

        public JobSchedulerHelper(PushRecordHandler pushRecordHanlder,
            RemoteDeviceList remoteDeviceList,
            IServiceScopeFactory serviceScopeFactory,
            IOptions<AppSettings> appSettings,
            ILogger<JobSchedulerHelper> logger,
            CommunicateDeviceList communicateDeviceList,
            IOptions<KoneSettings> koneSettings)
        {
            _pushRecordHanlder = pushRecordHanlder;
            _remoteDeviceList = remoteDeviceList;
            _serviceScopeFactory = serviceScopeFactory;
            _appSettings = appSettings.Value;
            _logger = logger;
            _communicateDeviceList = communicateDeviceList;
            _koneSettings = koneSettings.Value;
        }

        // 定时发送 
        private Timer _jobTimer;
        private Timer _koneTimer;
        private bool _isCheckOfficeLine = false;
        private bool _isCheckPush = false;
        /// <summary>
        /// 从数据库加载记录
        /// </summary>
        private bool _isLoadRecord = false;

        public static PassRecordModel FirstRecord { get; set; }

        public Task StartAsync()
        {
            _jobTimer = new Timer(JobCallBack, null, (int)(_appSettings.CheckOnlineDelaySecondTime * 1000), (int)(_appSettings.CheckOnlineSecondTime * 1000));
            _koneTimer = new Timer(KoneJobCallBack, null, (int)(_koneSettings.CheckOnlineDelaySecondTime * 1000), (int)(_koneSettings.CheckOnlineSecondTime * 1000));

            return Task.CompletedTask;
        }

        private async void KoneJobCallBack(object state)
        {
            await KoneDeviceHeartAsync();
        }

        private async Task KoneDeviceHeartAsync()
        {
            await _communicateDeviceList.AsyncExecuteAsync((communicateDevice) =>
            {
                return IsKone(communicateDevice);
            },

            async communicateDevice =>
            {
                await communicateDevice.CheckAndLinkAsync();
                await communicateDevice.HeartbeatAsync();
            });

            var koneCommunicateDeviceListCount = await _communicateDeviceList.GetCountAsync((x) =>
            {
                if (x.CommunicateDeviceInfo.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_ELI.Value
                 || x.CommunicateDeviceInfo.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_RCGIF.Value)
                {
                    return true;
                }
                return false;
            });

            var koneRemoteDeviceListCount = await _remoteDeviceList.GetCountAsync((x) =>
            {
                if (x.RemoteDeviceInfo.DeviceType == DeviceTypeEnum.ELEVATOR_SERVER.Value
                 && (x.RemoteDeviceInfo.ModuleType == KoneModuleTypeEnum.ELI.Value
                      || x.RemoteDeviceInfo.ModuleType == KoneModuleTypeEnum.RCGIF.Value))
                {
                    return true;
                }
                return false;
            });

            //if (koneCommunicateDeviceListCount != 10)
            //{
            //    _logger.LogError($"kone远程连接数量出错: koneCommunicateDeviceListCount:{koneCommunicateDeviceListCount}");
            //}
            //if (koneRemoteDeviceListCount != 10)
            //{
            //    _logger.LogError($"kone远程设备数量出错: koneRemoteDeviceListCount:{koneRemoteDeviceListCount}");
            //}
        }

        private async void JobCallBack(object state)
        {
            try
            {
                //1.判断离线
                ChangeOfficeLineAsync();

                //2.设备心跳
                DeviceHeartAsync();

                //3.判断事件上传
                CheckPushAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"时间Job错误：ex:{ex} ");
            }

            await Task.CompletedTask;
        }

        private async void DeviceHeartAsync()
        {
            await _communicateDeviceList.AsyncExecuteAsync((communicateDevice) =>
            {
                return !IsKone(communicateDevice);
            },
            async communicateDevice =>
            {
                await communicateDevice.HeartbeatAsync();
            });
        }

        private bool IsKone(ICommunicateDevice communicateDevice)
        {
            if (communicateDevice.CommunicateDeviceInfo.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_ELI.Value
                || communicateDevice.CommunicateDeviceInfo.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_RCGIF.Value)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 检查离线设备
        /// </summary> 
        private async void ChangeOfficeLineAsync()
        {
            if (_isCheckOfficeLine)
            {
                return;
            }
            _isCheckOfficeLine = true;

            try
            {
                var timeNow = DateTimeUtil.UtcNowMillis();
                await _communicateDeviceList.ExecuteAsync(
                    async (communicateDevice) =>
                    {
                        int execMillis;
                        if (communicateDevice.CommunicateDeviceInfo.ReloginTimes > 30)
                        {
                            execMillis = 30 * 3 * 1000;
                        }
                        else
                        {
                            execMillis = communicateDevice.CommunicateDeviceInfo.ReloginTimes * 3 * 1000;
                        }

                        var spanMillis = timeNow - communicateDevice.CommunicateDeviceInfo.LastLoginTime;
                        if (spanMillis < execMillis)
                        {
                            //错误距离上次登录间隔时间不足
                            return;
                        }

                        //最后登录时间
                        communicateDevice.CommunicateDeviceInfo.LastLoginTime = timeNow;

                        _logger.LogInformation($"{communicateDevice.CommunicateDeviceInfo.IpAddress}:{communicateDevice.CommunicateDeviceInfo.Port} 检测连接状态！");
                        await communicateDevice.CheckAndLinkAsync();
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError($"连接远程设备出错：{ex} ");
            }
            finally
            {
                _isCheckOfficeLine = false;
            }
        }

        /// <summary>
        /// 检查上传记录，上传成功后查询出所有未上传的数据重新上传
        /// </summary> 
        private async void CheckPushAsync()
        {
            if (FirstRecord == null)
            {
                //从数据库加载一次记录，如果上传成功后继续再继续获取
                //遇到错误记录会直接赋值，不用从数据库中确定存不存在错误记录
                if (!_isLoadRecord)
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        //检查是否存在通行记录
                        var serivce = scope.ServiceProvider.GetRequiredService<IPassRecordService>();
                        FirstRecord = await serivce.GetLastAsync();
                    }
                    _isLoadRecord = true;
                }
            }

            if (FirstRecord == null)
            {
                return;
            }

            if (_isCheckPush)
            {
                return;
            }
            _isCheckPush = true;

            try
            {
                //2.判断事件上传 
                var result = await _pushRecordHanlder.StartPushAsync(FirstRecord);
                if (result)
                {
                    FirstRecord = null;
                    await _pushRecordHanlder.PushErrorAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"检查上传记录出错：{ex} ");
            }
            finally
            {
                _isCheckPush = false;
            }
        }
    }
}
