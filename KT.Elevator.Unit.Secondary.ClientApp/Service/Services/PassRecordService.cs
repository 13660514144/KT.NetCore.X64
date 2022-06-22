using KT.Common.Core.Utils;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using KT.Elevator.Unit.Secondary.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Secondary.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Secondary.ClientApp.Service.IServices;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Secondary.ClientApp.Service.Services
{
    public class PassRecordService : IPassRecordService
    {
        private IPassRecordDao _dao;
        private ILogger _logger;
        private HubHelper _hubHelper;
        private AppSettings _appSettings;

        public PassRecordService(IPassRecordDao dao,
            ILogger logger,
            HubHelper hubHelper,
           AppSettings appSettings)
        {
            _dao = dao;
            _logger = logger;
            _hubHelper = hubHelper;
            _appSettings = appSettings;
        }

        public async Task<UnitPassRecordEntity> GetLast()
        {
            return await _dao.SelectLastAsync();
        }

        public async Task AddAsync(UnitPassRecordEntity entity)
        {
            await _dao.InsertAsync(entity);
        }

        public async Task Delete(string id)
        {
            var oldEntity = await _dao.SelectByIdAsync(id);
            if (oldEntity != null)
            {
                await _dao.DeleteAsync(oldEntity);
            }
        }
        
        public async void PushAsync(UnitHandleElevatorModel handleElevator)
        {
            _logger.LogInformation($"通行记录原始数据：{JsonConvert.SerializeObject(handleElevator, JsonUtil.JsonPrintSettings)} ");

            UnitPassRecordEntity passRecord = new UnitPassRecordEntity();
            passRecord.PassRightId = handleElevator.HandleElevatorRight?.PassRightId;
            passRecord.CreatedTime = passRecord.EditedTime = DateTimeUtil.UtcNowMillis();
            passRecord.PassTime = handleElevator.PassTime;
            passRecord.UnitDeviceId = handleElevator.HandleElevatorDeviceId;
            passRecord.UnitDeviceType =  _appSettings.DeviceType;
            passRecord.FaceImage = handleElevator.FaceImage;
            passRecord.FaceImageSize = handleElevator.FaceImageSize;


            ///// 人脸通行时的抓拍图片
            //passRecord.File = string.Empty;
            /// 设备ID，除了人脸摄像头、门禁读卡器是第三方系统的设备ID，其余都系本系统的设备ID
            passRecord.DeviceId = handleElevator.DeviceId;
            /// 设备类型
            passRecord.DeviceType = handleElevator.DeviceType;
            /// 通行类型
            passRecord.AccessType = handleElevator.AccessType;
            /// 通行码，IC卡、二维码、人脸ID
            passRecord.CardNumber = handleElevator.HandleElevatorRight?.PassRightSign;
            /// 通行时间，2019-11-06 15:20:45 
            passRecord.PassLocalTime = passRecord.PassTime.ToZoneDateTimeByMillis()?.ToSecondString();
            /// 扩展字段，在人脸摄像头时，该字段是人脸服务器的token
            passRecord.Extra = handleElevator.Extra;
            /// 派梯楼层
            passRecord.Remark = handleElevator.DestinationFloorName;

            try
            {
                await _hubHelper.PushPassRecordAsync(passRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError("上传数据错误：{0} ", ex);
                //_ = PresencePassRecordAsync(passRecord);
            }
        }

        /// <summary>
        /// 上传记录错误，保存到本地
        /// </summary>
        /// <param name="passRecord"></param>
        /// <returns></returns>
        private async Task PresencePassRecordAsync(UnitPassRecordEntity passRecord)
        {
            try
            {
                //1、判断是否有权限操作
                await AddAsync(passRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError("保存通行记录失败： data:{0} ex:{1} ", JsonConvert.SerializeObject(passRecord, JsonUtil.JsonPrintSettings), ex);
            }
        }

        public async Task<UnitPassRecordEntity> GetBySignAndAccessTypeAsync(string sign, string accessType)
        {
            var entity = await _dao.SelectFirstByLambdaAsync(x => x.CardNumber == sign && x.AccessType == accessType);

            return entity;
        }
    }
}
