using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Quanta.Unit.Common.Helpers
{
    /// <summary>
    /// 系统时间同步
    /// </summary>
    public class SystemTimeHelper : IAsyncDisposable
    {
        //系统时间同步
        private Timer _systemTimeSyncTimer;
        private Func<Task<long>> _utcMillisAction;

        private readonly ILogger _logger;

        public SystemTimeHelper(ILogger logger)
        {
            _logger = logger;
        }

        private async void SystemTimeSyncCallbackAsync(object state)
        {
            var result = await KT.Common.Core.Helpers.SystemTimeHelper.SetSystemTimeAsync(async () =>
            {
                return await _utcMillisAction?.Invoke();
            });
            if (!result)
            {
                _logger.LogWarning($"同步系统时间失败！");
            }
        }

        public Task StartAsync(Func<Task<long>> utcMillisAction)
        {
            _utcMillisAction = utcMillisAction;
            //时间同步
            _systemTimeSyncTimer = new Timer(SystemTimeSyncCallbackAsync, null, 10 * 1000, 60 * 1000);

            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            if (_systemTimeSyncTimer != null)
            {
                await _systemTimeSyncTimer.DisposeAsync();
            }
        }
    }
}
