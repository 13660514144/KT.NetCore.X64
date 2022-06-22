using KT.Common.Core.Utils;
using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Handlers;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Manage.Service.Services;
using KT.Quanta.Common.Models;
using log4net.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Helpers
{
    /// <summary>
    /// 定时工作
    /// </summary>
    public class JobSchedulerHelper
    {
        private PushRecordHandler _pushRecordHanlder;
        private RemoteDeviceList _remoteDeviceList;
        private IServiceProvider _serviceProvider;
        private SeekSendHelper _seekUdpHelper;
        private AppSettings _appSettings;
        private ILogger<JobSchedulerHelper> _logger;

        public JobSchedulerHelper(PushRecordHandler pushRecordHanlder,
            RemoteDeviceList processorDeviceList,
            IServiceProvider serviceProvider,
            SeekSendHelper seekUdpHelper,
            IOptions<AppSettings> appSettings,
            ILogger<JobSchedulerHelper> logger)
        {
            _pushRecordHanlder = pushRecordHanlder;
            _remoteDeviceList = processorDeviceList;
            _serviceProvider = serviceProvider;
            _seekUdpHelper = seekUdpHelper;
            _appSettings = appSettings.Value;
            _logger = logger;
        }

        // 定时发送 
        private Timer _jobTimer;

        public static PassRecordModel FirstRecord { get; set; }

        public async Task StartAsync()
        {
            _jobTimer = new Timer(JobCallBack, null, 10, _appSettings.CheckOnlineTime);

            //检查是否存在通行记录
            var serivce = _serviceProvider.GetRequiredService<IPassRecordService>();
            FirstRecord = await serivce.GetLastAsync();
        }

        private async void JobCallBack(object state)
        {
            try
            {
                //1.判断离线
                await ChangeOfficeLineAsync();

                //2.判断事件上传
                await CheckPushAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"时间Job错误：ex:{ex} ");
            }

        }

        /// <summary>
        /// 检查离线设备
        /// </summary> 
        private async Task ChangeOfficeLineAsync()
        {
            var timeNow = DateTimeUtil.UtcNowMillis();
            await _remoteDeviceList.ExecAllAsync(async (remoteDevice) =>
            {
                await remoteDevice.CheckAndLinkAsync();

                ////离线 //定时发送数据连接
                //if (!remoteDevice.RemoteDeviceInfo.IsOnline
                //  && (remoteDevice.RemoteDeviceInfo.ProductType == RemoteDeviceTypeEnum.ELEVATOR_SECONDARY.Value
                //      || remoteDevice.RemoteDeviceInfo.ProductType == RemoteDeviceTypeEnum.ELEVATOR_PROCESSOR.Value
                //      || remoteDevice.RemoteDeviceInfo.ProductType == RemoteDeviceTypeEnum.ELEVATOR_GATE_DISPLAY.Value))
                //{
                //    var seekSocket = new SeekSocketModel();
                //    seekSocket.Header = _appSettings.SeekHeader;
                //    seekSocket.ClientIp = remoteDevice.RemoteDeviceInfo.IpAddress;
                //    seekSocket.ClientPort = remoteDevice.RemoteDeviceInfo.Port;

                //    await _seekUdpHelper.SendDataAsync(seekSocket);
                //} 
            });
        }

        /// <summary>
        /// 检查上传记录，上传成功后查询出所有未上传的数据重新上传
        /// </summary> 
        private async Task CheckPushAsync()
        {
            //2.判断事件上传 
            if (FirstRecord != null)
            {
                var result = await _pushRecordHanlder.StartPushAsync(FirstRecord);
                if (result)
                {
                    FirstRecord = null;
                    await _pushRecordHanlder.PushErrorAsync();
                }
            }
        }
    }
}
