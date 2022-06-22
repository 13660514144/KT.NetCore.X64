
using KT.Prowatch.Service.DllModels;
using KT.Prowatch.Service.Helpers;
using KT.Prowatch.Service.IServices;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.BackendApi.Models;
using Microsoft.Extensions.Logging;
using ProwatchAPICS;
using System;
using System.Threading.Tasks;

namespace KT.Prowatch.Service.Services
{
    public class PushEventService : IPushEventService
    {
        private ILogger<PushEventService> _logger;
        private OpenApi _openApi;

        public PushEventService(ILogger<PushEventService> logger, OpenApi openApi)
        {
            _logger = logger;
            _openApi = openApi;
        }

        /// <summary>
        /// 初始化上传事件
        /// </summary>
        public void InitPush()
        {
            ApiHelper.PWApi.realEventHandler += new RealEventHandler(RealEvent);
            bool bRet = ApiHelper.PWApi.StartRecvRealEvent();
            if (!bRet)
            {
                _logger.LogInformation("初始化事件推送失败！");
            }
            else
            {
                _logger.LogInformation("初始化事件推送成功！");
            }
        }

        /// <summary>
        /// 事件接收
        /// </summary>
        /// <param name="eventData"></param>
        private void RealEvent(sPA_Event paEvent)
        {
            EventData eventData = paEvent.ToModel();
            Action action = new Action(async () =>
            {
                try
                {
                    if (eventData.Desc == "PASS7")
                    {
                        return;
                    }
                    await PushEventAsync(eventData);
                }
                catch (Exception ex)
                {
                    _logger.LogError("上传数据出错：" + ex.ToString());
                }
            });
            Task.Run(action);
        }

        /// <summary>
        /// 开始推送事件
        /// </summary>
        /// <param name="eventData"></param>
        private async Task PushEventAsync(EventData eventData)
        {
            PushPassRecordModel model = new PushPassRecordModel();
            model.File = null;
            model.EquipmentId = eventData.Location;
            model.Extra = string.Empty;
            model.EquipmentType = "ACCESS_READER";
            model.AccessType = "IC_CARD";
            model.AccessToken = eventData.CardNo;
            model.AccessDate = eventData.EvntDate;
            model.Remark = eventData.EvntDesc;

            string url = LoginHelper.Instance.CurrentConnect.ServerAddress;
            await _openApi.PushRecord(url, model);
        }
    }
}
