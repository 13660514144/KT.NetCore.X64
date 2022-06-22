using KT.Common.Core.Utils;
using KT.Quanta.Common.Models;
using KT.Turnstile.Manage.Service.Handlers;
using KT.Turnstile.Manage.Service.Services;
using KT.Turnstile.Model.Models;
using KT.Turnstile.Model.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.Service.Helpers
{
    /// <summary>
    /// 定时工作
    /// </summary>
    public class JobSchedulerHelper
    {
        private PushRecordHandler _pushRecordHanlder;
        private ProcessorDeviceList _processorDeviceList;
        private IServiceProvider _serviceProvider;
        private SeekSendHelper _seekUdpHelper;
        private AppSettings _appSettings;

        public JobSchedulerHelper(PushRecordHandler pushRecordHanlder,
            ProcessorDeviceList processorDeviceList,
            IServiceProvider serviceProvider,
            SeekSendHelper seekUdpHelper,
            IOptions<AppSettings> appSettings)
        {
            _pushRecordHanlder = pushRecordHanlder;
            _processorDeviceList = processorDeviceList;
            _serviceProvider = serviceProvider;
            _seekUdpHelper = seekUdpHelper;
            _appSettings = appSettings.Value;
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
            //1.判断离线
            ChangeOfficeLine();

            //2.判断事件上传
            await CheckPushAsync();
        }

        private void ChangeOfficeLine()
        {
            var timeNow = DateTimeUtil.UtcNowMillis();
            _processorDeviceList.ExecAll(async (processor) =>
            {
                //离线 //定时发送数据连接
                if (!processor.IsOnline)
                {
                    var seekSocket = new SeekSocketModel();
                    seekSocket.ClientIp = processor.IpAddress;
                    seekSocket.ClientPort = processor.Port;

                    await _seekUdpHelper.SendDataAsync(seekSocket);
                }
            });
        }

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
