using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace KT.Quanta.Service.Models
{
    public class HandleElevatorDeviceModel : BaseQuantaModel
    {
        /// <summary>
        /// 派梯设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string DeviceId { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        public string ProcessorId { get; set; }

        /// <summary>
        /// 边缘处理器
        /// </summary>
        public ProcessorModel Processor { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        [JsonProperty("equipmentType")]
        public string DeviceType { get; set; }

        /// <summary>
        /// 品牌型号
        /// </summary>
        [JsonProperty("type")]
        public string BrandModel { get; set; }

        /// <summary>
        /// 通信类型，如TCP、UDP
        /// </summary>
        public string CommunicateType { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 人脸AppID
        /// </summary>
        public string FaceAppId { get; set; }

        /// <summary>
        /// 人脸SDK KEY
        /// </summary>
        public string FaceSdkKey { get; set; }

        /// <summary>
        /// 人脸激活码
        /// </summary>
        public string FaceActivateCode { get; set; }

        /// <summary>
        /// 所在位置
        /// </summary>
        public string FloorId { get; set; }

        /// <summary>
        /// 关联所在楼层
        /// </summary>
        public FloorModel Floor { get; set; }

        /// <summary>
        /// 关联电梯组Id
        /// </summary>
        public string ElevatorGroupId { get; set; }

        /// <summary>
        /// 关联电梯组
        /// </summary>
        public ElevatorGroupModel ElevatorGroup { get; set; }

        /// <summary>
        /// 派梯终端，具体的COM口，对应到日立通讯箱 2021.05.25
        /// </summary>        
        public string CommBox { get; set; }

        public static HandleElevatorDeviceEntity ToEntity(HandleElevatorDeviceModel model)
        {
            var entity = new HandleElevatorDeviceEntity();
            entity = SetEntity(entity, model);
            return entity;
        }
 
        public static HandleElevatorDeviceEntity SetEntity(HandleElevatorDeviceEntity entity, HandleElevatorDeviceModel model)
        {
            entity.Id = model.Id;
            entity.DeviceId = model.DeviceId;
            entity.Name = model.Name;
            entity.BrandModel = model.BrandModel;
            entity.DeviceType = model.DeviceType;
            entity.CommunicateType = model.CommunicateType;
            entity.IpAddress = model.IpAddress;
            entity.Port = model.Port;
            entity.FaceAppId = model.FaceAppId;
            entity.FaceSdkKey = model.FaceSdkKey;
            entity.FaceActivateCode = model.FaceActivateCode;
            entity.EditedTime = model.EditedTime;
            entity.CommBox = model.CommBox;
            //关联所在楼层
            if (!string.IsNullOrEmpty(model.FloorId))
            {
                if (entity.Floor == null || entity.Floor.Id != model.FloorId)
                {
                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    entity.Floor = new FloorEntity();
                    entity.Floor.Id = model.FloorId;
                }
            }

            //关联电梯组
            if (!string.IsNullOrEmpty(model.ElevatorGroupId))
            {
                if (entity.ElevatorGroup == null || entity.ElevatorGroup.Id != model.ElevatorGroupId)
                {
                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    entity.ElevatorGroup = new ElevatorGroupEntity();
                    entity.ElevatorGroup.Id = model.ElevatorGroupId;
                }
            }

            //关联边缘处理器
            if (!string.IsNullOrEmpty(model.ProcessorId))
            {
                if (entity.Processor == null || entity.Processor.Id != model.ProcessorId)
                {
                    //修改关联数据时不能使用原来的关联实体类，否则修改出错 
                    entity.Processor = new ProcessorEntity();
                    entity.Processor.Id = model.ProcessorId;
                }
            }

            return entity;
        }
         
        public static List<RemoteDeviceModel> ToRemoteDevices(List<HandleElevatorDeviceModel> models)
        {
            var remoteDevices = new List<RemoteDeviceModel>();
            if (models == null || models.FirstOrDefault() == null)
            {
                return remoteDevices;
            }
            foreach (var item in models)
            {
                remoteDevices.Add(ToRemoteDevice(item));
            }
            return remoteDevices;
        }

        public static RemoteDeviceModel ToRemoteDevice(HandleElevatorDeviceModel model)
        {
            var remoteModel = new RemoteDeviceModel();
            remoteModel.Name = model.Name;
            remoteModel.DeviceType = model.DeviceType;
            remoteModel.BrandModel = model.BrandModel;
            remoteModel.DeviceId = model.Id;
            remoteModel.RealId = model.DeviceId;
            remoteModel.ExtensionId = model.ProcessorId;

            remoteModel.ParentId = model.ElevatorGroup.Id;

            remoteModel.CommunicateDeviceInfos = new List<CommunicateDeviceInfoModel>();
            var communicateDeviceInfo = new CommunicateDeviceInfoModel();
            communicateDeviceInfo.IpAddress = model.IpAddress;
            communicateDeviceInfo.Port = model.Port;
            communicateDeviceInfo.Account = model.FaceAppId;
            communicateDeviceInfo.Password = model.FaceSdkKey;
            remoteModel.CommunicateDeviceInfos.Add(communicateDeviceInfo);

            return remoteModel;
        }
    }
}
