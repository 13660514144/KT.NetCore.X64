using KT.Common.Core.Utils;
using KT.Elevator.Manage.Service.Handlers;
using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Hikvision
{
    public class HikvisionService : IHikvisionService
    {
        private ILogger<HikvisionService> _logger;
        private PushRecordHandler _pushRecordHandler;
        private IProcessorFloorDao _processorFloorDao;
        public HikvisionService(PushRecordHandler pushRecordHandler,
            ILogger<HikvisionService> logger,
            IProcessorFloorDao processorFloorDao)
        {
            _pushRecordHandler = pushRecordHandler;
            _logger = logger;
            _processorFloorDao = processorFloorDao;
        }

        public async Task UploadPassRecordAsync(PassRecordModel passRecord)
        {
            //打印日记
            var repassRecord = (PassRecordModel)passRecord.Clone();
            repassRecord.FaceImage = null;
            _logger.LogInformation($"事件上传原始数据：{JsonConvert.SerializeObject(repassRecord, JsonUtil.JsonSettings)} ");

            if (!string.IsNullOrEmpty(passRecord.Remark))
            {
                var processorFloorRealId = Convert.ToInt32(passRecord.Remark);
                var processorFloor = await _processorFloorDao.GetWithFloorByProcessorIdAndSortIdAsync( passRecord.DeviceId ,processorFloorRealId);

                if (!string.IsNullOrEmpty(processorFloor?.Floor?.Name))
                {
                    passRecord.Remark = processorFloor.Floor.Name;
                }
            }

#if DEBUG
            //TODOO 暂时不上传数据
            return;
#endif
            await _pushRecordHandler.StartPushAsync(passRecord);
        }
    }
}
