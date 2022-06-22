using KT.Common.WebApi.IServices;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;

namespace KT.Common.WebApi.Services
{
    public class RecordService : IRecordService
    {
        private Timer _bakTimer;
        private ILogger<RecordService> _logger;
        private long _serviceRecordTime = 100000;

        public RecordService(ILogger<RecordService> logger)
        {
            _logger = logger;
        }

        public void Start()
        {
            _bakTimer = new Timer(BakCallBack, null, -1, _serviceRecordTime);
        }

        private void BakCallBack(object state)
        {
            _logger.LogInformation($"Windows服务：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }
    }
}

//public class RecordService : IHostedService, IDisposable
//    {
//        private readonly Timer _bakTimer;
//        private ILogger<RecordService> _logger;
//        private long _serviceRecordTime = 100000;

//        public RecordService(ILogger<RecordService> logger)
//        {
//            _bakTimer = new Timer(BakCallBack, null, -1, _serviceRecordTime);
//            _logger = logger;
//        }

//        private void BakCallBack(object state)
//        {
//            _logger.LogInformation($"Windows服务：{DateTime.Now:yyyy-MM-dd HH:mm:ss}");
//        }

//        public Task StartAsync(CancellationToken cancellationToken)
//        {
//            _bakTimer.Change(0, _serviceRecordTime); 
//        }

//        public Task StopAsync(CancellationToken cancellationToken)
//        {
//            _bakTimer.Change(-1, _serviceRecordTime); 
//        }

//        public void Dispose()
//        {
//            _bakTimer.Dispose();
//        }
//    }
//}
