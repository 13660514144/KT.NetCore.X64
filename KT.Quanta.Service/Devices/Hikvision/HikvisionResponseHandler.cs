using KT.Common.Core.Utils;
using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Handlers;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Hikvision
{
    public class HikvisionResponseHandler : IHikvisionResponseHandler
    {
        private ILogger<HikvisionResponseHandler> _logger;
        private PushRecordHandler _pushRecordHandler;
        private IProcessorFloorDao _processorFloorDao;
        private RemoteDeviceList _remoteDeviceList;
        private IPersonDao _personDao;

        public HikvisionResponseHandler(PushRecordHandler pushRecordHandler,
            ILogger<HikvisionResponseHandler> logger,
            IProcessorFloorDao processorFloorDao,
            RemoteDeviceList remoteDeviceList,
            IPersonDao personDao)
        {
            _pushRecordHandler = pushRecordHandler;
            _logger = logger;
            _processorFloorDao = processorFloorDao;
            _remoteDeviceList = remoteDeviceList;
            _personDao = personDao;
        }

        public async Task UploadPassRecordAsync(PassRecordModel passRecord)
        {
            //打印日记 
            _logger.LogInformation($"事件上传原始数据：{JsonConvert.SerializeObject(passRecord, JsonUtil.JsonPrintSettings)} ");

            //卡号去掉补0
            passRecord.PassRightSign = ConvertUtil.ToInt32(passRecord.PassRightSign, 0).ToString();

            //人脸权限卡号为关联人员ic卡或二维码，查询出相应的人脸权限
            if (passRecord.AccessType == AccessTypeEnum.FACE.Value)
            {
                var person = await _personDao.GetWithPassRightsByAsync(passRecord.PassRightSign);
                var faceRight = person?.PassRights?.FirstOrDefault(x => x.Sign == passRecord.PassRightSign && x.AccessType == passRecord.AccessType);
                if (faceRight == null)
                {
                    faceRight = person?.PassRights?.FirstOrDefault(x => x.AccessType == AccessTypeEnum.FACE.Value);
                    if (faceRight != null)
                    {
                        passRecord.PassRightSign = faceRight.Sign;
                    }
                }
            }

            var remoteDevices = await _remoteDeviceList.GetByIpAndPortAsync(passRecord.DeviceId);
            if (remoteDevices?.FirstOrDefault() == null)
            {
                _logger.LogError($"未找到事件上传设备：communicateId:{passRecord.DeviceId} ");
            }

            foreach (var item in remoteDevices)
            {
                ////梯控主机时间戳转换
                //if (item.RemoteDeviceInfo.BrandModel == BrandModelEnum.HIKVISION_DS_K2210.Value)
                //{
                //    passRecord.PassLocalTime = DateTimeUtil.ToZoneDateTimeString(passRecord.PassLocalTime, "yyyy-MM-dd HH:mm:ss");
                //}

                passRecord.DeviceId = item.RemoteDeviceInfo.DeviceId;
                passRecord.DeviceType = item.RemoteDeviceInfo.CardDeviceType;

                if (string.IsNullOrEmpty(passRecord.DeviceType))
                {
                    passRecord.DeviceType = CardDeviceTypeEnum.IC_QR.Value;
                }

                //查找输出映射关系，可能为闸机，不存在映射，为空不显示
                if (!string.IsNullOrEmpty(passRecord.Remark))
                {
                    var processorFloorRealId = ConvertUtil.ToInt32(passRecord.Remark, 0);
                    var processorFloor = await _processorFloorDao.GetWithFloorByProcessorIdAndSortIdAsync(passRecord.DeviceId, processorFloorRealId);

                    if (!string.IsNullOrEmpty(processorFloor?.Floor?.Name))
                    {
                        passRecord.Remark = processorFloor.Floor.Name;
                    }
                    else
                    {
                        passRecord.Remark = string.Empty;
                    }
                }
                await _pushRecordHandler.StartPushAsync(passRecord);
            }
        }
    }
}
