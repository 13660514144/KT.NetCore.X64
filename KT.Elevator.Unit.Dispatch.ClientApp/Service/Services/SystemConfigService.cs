using KT.Common.Core.Utils;
using KT.Elevator.Unit.Dispatch.ClientApp.Dao.IDaos;
using KT.Elevator.Unit.Dispatch.Entity.Entities;
using KT.Elevator.Unit.Dispatch.Entity.Enums;
using KT.Elevator.Unit.Dispatch.Entity.Models;
using KT.Elevator.Unit.Dispatch.ClientApp.Service.IServices;
using System.Collections.Generic;
using System.Threading.Tasks; 

namespace KT.Elevator.Unit.Dispatch.ClientApp.Service.Services
{
    public class SystemConfigService : ISystemConfigService
    {
        private ISystemConfigDao _systemConfigDao;
        public SystemConfigService(ISystemConfigDao systemConfigDao)
        {
            _systemConfigDao = systemConfigDao;
        }

        public async Task AddOrUpdateAsync(UnitDispatchSystemConfigModel model)
        {
            // 服务器ip
            await AddOrUpdateAsync(UnitDispatchSystemConfigEnum.SERVER_IP, model.ServerIp);

            // 服务器端口
            await AddOrUpdateAsync(UnitDispatchSystemConfigEnum.SERVER_PORT, model.ServerPort);

            // 服务器事件上传地址
            await AddOrUpdateAsync(UnitDispatchSystemConfigEnum.PUSH_ADDRESS, model.PushAddress);

            // 最后同步数据时间
            await AddOrUpdateAsync(UnitDispatchSystemConfigEnum.LAST_SYNC_TIME, model.LastSyncTime);

            // Id主键，用于当前派梯设备id
            await AddOrUpdateAsync(UnitDispatchSystemConfigEnum.HANDLE_ELEVATOR_DEVICE_ID, model.HandleElevatorDeviceId);

            //所在楼层
            await AddOrUpdateAsync(UnitDispatchSystemConfigEnum.DEVICE_FLOOR_ID, model.DeviceFloorId);

            // 人脸AppID
            await AddOrUpdateAsync(UnitDispatchSystemConfigEnum.FACE_APP_ID, model.FaceAppId);

            // 人脸SDK KEY
            await AddOrUpdateAsync(UnitDispatchSystemConfigEnum.FACE_SDK_KEY, model.FaceSdkKey);

            // 人脸激活码
            await AddOrUpdateAsync(UnitDispatchSystemConfigEnum.FACE_ACTIVATE_CODE, model.FaceActivateCode);

            // 关联电梯组Id
            await AddOrUpdateAsync(UnitDispatchSystemConfigEnum.ELEVATOR_GROUP_ID, model.ElevatorGroupId);

        }

        public async Task AddOrUpdateAsync(UnitDispatchSystemConfigEnum keyEnum, object value)
        {
            var oldEntity = await _systemConfigDao.SelectFirstByLambdaAsync(x => x.Key == keyEnum.Value);
            if (oldEntity == null)
            {
                oldEntity = new UnitDispatchSystemConfigEntity();
                oldEntity.Key = keyEnum.Value;
                oldEntity.Value = value.IsNull();
                await _systemConfigDao.InsertAsync(oldEntity);
            }
            else
            {
                oldEntity.Value = value.IsNull();
                await _systemConfigDao.AttachAsync(oldEntity);
            }
        }


        public async Task AddOrUpdatesAsync(List<UnitDispatchSystemConfigEntity> entities)
        {
            foreach (var item in entities)
            {
                var oldEntity = await _systemConfigDao.SelectFirstByLambdaAsync(x => x.Key == item.Value);
                if (oldEntity == null)
                {
                    await _systemConfigDao.InsertAsync(item);
                }
                else
                {
                    oldEntity.Value = item.Value;
                    await _systemConfigDao.AttachAsync(oldEntity);
                }
            }
        }

        /// <summary>
        /// 获取最新配置文件
        /// </summary>
        /// <returns></returns>
        public async Task<UnitDispatchSystemConfigModel> GetAsync()
        {
            var entities = await _systemConfigDao.SelectAllAsync();
            var result = new UnitDispatchSystemConfigModel();
            if (entities == null)
            {
                return result;
            }
            foreach (var item in entities)
            {
                // 服务器ip
                if (item.Key == UnitDispatchSystemConfigEnum.SERVER_IP.Value)
                {
                    result.ServerIp = item.Value;
                    continue;
                }

                // 服务器端口
                else if (item.Key == UnitDispatchSystemConfigEnum.SERVER_PORT.Value)
                {
                    result.ServerPort = ConvertUtil.ToInt32(item.Value, 0);
                    continue;
                }

                // 服务器事件上传地址
                else if (item.Key == UnitDispatchSystemConfigEnum.PUSH_ADDRESS.Value)
                {
                    result.PushAddress = item.Value;
                    continue;
                }

                // 最后同步时间
                else if (item.Key == UnitDispatchSystemConfigEnum.LAST_SYNC_TIME.Value)
                {
                    result.LastSyncTime = ConvertUtil.ToLong(item.Value, 0);
                    continue;
                }

                // Id主键，用于当前派梯设备id
                else if (item.Key == UnitDispatchSystemConfigEnum.HANDLE_ELEVATOR_DEVICE_ID.Value)
                {
                    result.HandleElevatorDeviceId = item.Value;
                }

                // 所在楼层
                else if (item.Key == UnitDispatchSystemConfigEnum.DEVICE_FLOOR_ID.Value)
                {
                    result.DeviceFloorId = item.Value;
                }

                // 人脸AppID
                else if (item.Key == UnitDispatchSystemConfigEnum.FACE_APP_ID.Value)
                {
                    result.FaceAppId = item.Value;
                }

                // 人脸SDK KEY
                else if (item.Key == UnitDispatchSystemConfigEnum.FACE_SDK_KEY.Value)
                {
                    result.FaceSdkKey = item.Value;
                }

                // 人脸激活码
                else if (item.Key == UnitDispatchSystemConfigEnum.FACE_ACTIVATE_CODE.Value)
                {
                    result.FaceActivateCode = item.Value;
                }

                // 关联电梯组Id
                else if (item.Key == UnitDispatchSystemConfigEnum.ELEVATOR_GROUP_ID.Value)
                {
                    result.ElevatorGroupId = item.Value;
                }

            }
            return result;
        }
    }
}
