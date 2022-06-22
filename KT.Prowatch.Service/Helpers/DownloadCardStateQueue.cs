using KT.Common.Core.Utils;
using KT.Prowatch.Service.IServices;
using KT.Prowatch.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Prowatch.Service.Helpers
{
    /// <summary>
    /// 卡下载操作队列（静态列表）
    /// </summary>
    public class DownloadCardStateQueue
    {
        private Queue<DownloadCardStateModel> _downloadCardStates;

        private ILogger<DownloadCardStateQueue> _logger;
        private IServiceProvider _serviceProvider;
        private IProwatchService _prowatchService;
        private AppSettings _appSettings;
        public DownloadCardStateQueue(ILogger<DownloadCardStateQueue> logger,
            IServiceProvider serviceProvider,
            IOptions<AppSettings> appSettings)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _appSettings = appSettings.Value;

            _downloadCardStates = new Queue<DownloadCardStateModel>();

            Task.Run(async () =>
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    _prowatchService = scope.ServiceProvider.GetRequiredService<IProwatchService>();

                    try
                    {
                        await StartAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"重复下载卡失败：ex:{ex} ");
                        await StartAsync();
                    }
                }
            });
        }

        public void Enqueue(DownloadCardStateModel state)
        {
            _downloadCardStates.Enqueue(state);
        }

        private async Task StartAsync()
        {
            if (_downloadCardStates.Count > 0)
            {
                var state = _downloadCardStates.Dequeue();
                var dateTimeNow = DateTimeUtil.UtcNowMillis();
                var minExcTime = (state.AddTime + (long)(state.Times * _appSettings.RedownloadCardStateSecondTime * 1000));
                if (minExcTime <= dateTimeNow)
                {
                    //下载卡状态
                    var downloadResult = await _prowatchService.DownloadCardStateAsync(state.PersonId, state.CardNo, state.StateCode);
                    var dateTimeNow2 = DateTimeUtil.UtcNowMillis();
                    var timeSpan = (dateTimeNow2 - dateTimeNow) / 1000M;

                    _logger.LogInformation($"重新更新卡完成：" +
                        $"updateResult:{downloadResult} " +
                        $"stateCode:{state.StateCode} " +
                        $"times:{state.Times} " +
                        $"personId:{state.PersonId} " +
                        $"cardNo:{state.CardNo} " +
                        $"time:{dateTimeNow2} " +
                        $"timeSpan:{timeSpan} ");

                    //次数未完成重新加入队列
                    if (state.Times <= _appSettings.RedownloadCardStateTimes)
                    {
                        state.Times++;
                        state.AddTime = dateTimeNow2;
                        _downloadCardStates.Enqueue(state);
                    }

                    //等待间隔时间
                    await Task.Delay((int)(_appSettings.RedownloadCardStateIntervalSecondTime * 1000));
                }
                else
                {
                    _downloadCardStates.Enqueue(state);
                    await Task.Delay((int)(_appSettings.RedownloadCardStateFreeWaitSecondTime * 1000));
                }
            }
            else
            {
                await Task.Delay((int)(_appSettings.RedownloadCardStateFreeWaitSecondTime * 1000));
            }
            await StartAsync();
        }
    }
}
