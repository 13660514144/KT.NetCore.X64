using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Elevator.Common.Enums;
using KT.Elevator.Manage.Service.Handlers;
using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Services
{
    public class PassRecordService : IPassRecordService
    {
        private IPassRecordDao _passRecordDataDao;
        private PushRecordHandler _pushRecordHanlder;
        private IServiceProvider _serviceProvider;

        public PassRecordService(IPassRecordDao passRecordDataDao,
            PushRecordHandler pushRecordHanlder,
            IServiceProvider serviceProvider)
        {
            _passRecordDataDao = passRecordDataDao;
            _pushRecordHanlder = pushRecordHanlder;
            _serviceProvider = serviceProvider;
        }

        public async Task<bool> IsExistsAsync(string id)
        {
            return await _passRecordDataDao.HasInstanceByIdAsync(id);
        }

        public async Task<PassRecordModel> AddAsync(PassRecordModel model)
        {
            var entity = PassRecordModel.ToEntity(model);

            await _passRecordDataDao.InsertAsync(entity);

            return model;
        }

        public async Task DeleteAsync(string id)
        {
            await _passRecordDataDao.DeleteByIdAsync(id);
        }

        public async Task<PassRecordModel> AddOrEditAsync(PassRecordModel model)
        {
            var entity = await _passRecordDataDao.SelectByIdAsync(model.Id);
            if (entity == null)
            {
                entity = PassRecordModel.ToEntity(model);
                await _passRecordDataDao.InsertAsync(entity);
            }
            else
            {
                entity = PassRecordModel.SetEntity(entity, model);
                await _passRecordDataDao.AttachUpdateAsync(entity);
            }
            return model;
        }

        public async Task<List<PassRecordModel>> GetAllAsync()
        {
            var entities = await _passRecordDataDao.SelectAllAsync();

            var models = PassRecordModel.ToModels(entities);

            return models;
        }

        public async Task<PassRecordModel> GetByIdAsync(string id)
        {
            var entity = await _passRecordDataDao.SelectByIdAsync(id);
            var model = PassRecordModel.ToModel(entity);
            return model;
        }

        public async Task<PassRecordModel> GetLastAsync()
        {
            var entity = await _passRecordDataDao.SelectLastAsync();
            var model = PassRecordModel.ToModel(entity);
            return model;
        }

        /// <summary>
        /// 上传客户端传上来的通行记录到后台
        /// </summary>
        /// <param name="unitPassRecord"></param> 
        public async Task PushPassRecord(UnitPassRecordEntity unitPassRecord)
        {
            var passRecord = new PassRecordModel();
            passRecord.DeviceId = unitPassRecord.DeviceId;
            passRecord.DeviceType = unitPassRecord.DeviceType;
            passRecord.AccessType = unitPassRecord.AccessType;
            passRecord.PassRightSign = unitPassRecord.CardNumber;
            passRecord.PassLocalTime = unitPassRecord.PassLocalTime;
            passRecord.PassTime = unitPassRecord.PassTime;
            passRecord.Extra = unitPassRecord.Extra;
            passRecord.WayType = unitPassRecord.WayType;
            passRecord.Remark = unitPassRecord.Remark;
            passRecord.PassRightId = unitPassRecord.PassRightId;

            passRecord.FaceImage = unitPassRecord.FaceImage;
            passRecord.FaceImageSize = unitPassRecord.FaceImageSize;

            //设备id为空则从设备中提取
            if (string.IsNullOrEmpty(passRecord.DeviceId))
            {
                //if(unitPassRecord .UnitDeviceType == RemoteDeviceTypeEnum.ELEVATOR_SECONDARY.Value)
                var deviceService = _serviceProvider.GetRequiredService<IHandleElevatorInputDeviceDao>();
                var device = await deviceService.GetByDeviceIdAndCardTypeAsync(unitPassRecord.UnitDeviceId, unitPassRecord.DeviceType);

                passRecord.DeviceId = device?.Id;
            }
            if (passRecord.DeviceType == DeviceTypeEnum.ELEVATOR_SECONDARY.Value)
            {
                //if(unitPassRecord .UnitDeviceType == RemoteDeviceTypeEnum.ELEVATOR_SECONDARY.Value)
                var deviceService = _serviceProvider.GetRequiredService<IHandleElevatorInputDeviceDao>();
                var device = await deviceService.GetByDeviceIdAndCardTypeAsync(unitPassRecord.UnitDeviceId, DeviceTypeEnum.FACE_CAMERA.Value);

                passRecord.Extra = device?.Id;
            }
             
            await _pushRecordHanlder.StartPushAsync(passRecord);

            ////摄像机上传两次数据
            //if (unitPassRecord.DeviceType == DeviceTypeEnum.FACE_CAMERA.Value
            //    && unitPassRecord.DeviceId != unitPassRecord.UnitDeviceId)
            //{
            //    var devicePassRecord = JsonConvert.DeserializeObject<PassRecordModel>(JsonConvert.SerializeObject(passRecord, JsonUtil.JsonSettings), JsonUtil.JsonSettings);
            //    //以派梯设备作为设备Id
            //    devicePassRecord.DeviceType = RemoteDeviceTypeEnum.ELEVATOR_SECONDARY.Value;
            //    devicePassRecord.DeviceId = unitPassRecord.UnitDeviceId;
            //    //上传数据
            //    await _pushRecordHanlder.StartPushAsync(devicePassRecord);
            //}
        }

        /// <summary>
        /// 向本地添加失败的上传记录
        /// </summary>
        /// <param name="passRecord"></param>
        public async Task AddIfNoExistsAsync(PassRecordModel passRecord)
        {
            if (!string.IsNullOrEmpty(passRecord.Id))
            {
                var isExists = await _passRecordDataDao.HasInstanceAsync(x => x.Id == passRecord.Id);
                if (isExists)
                {
                    return;
                }
            }
            await AddAsync(passRecord);
        }
    }
}
