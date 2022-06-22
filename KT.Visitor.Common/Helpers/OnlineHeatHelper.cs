using KT.Proxy.BackendApi.Apis;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Visitor.Common.Helpers
{
    /// <summary>
    /// 在线心跳，单例
    /// </summary>
    public class OnlineHeatHelper
    {
        private Timer _timer;
        private IVisitorApi _visitorApi;
        private ILogger _logger;

        public OnlineHeatHelper(IVisitorApi visitorApi,
            ILogger logger)
        {
            _visitorApi = visitorApi;
            _logger = logger;
        }

        public void Start(int perSecondTime)
        {
            _timer = new Timer(OnlineTimerCallbackAsync, null, 3 * 1000, perSecondTime * 1000);
        }

        private async void OnlineTimerCallbackAsync(object state)
        {
            try
            {
                await _visitorApi.EquipmentHeartAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"EquipmentHeart:{ex}");
            }
        }
    }
}
