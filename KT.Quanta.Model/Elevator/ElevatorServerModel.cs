using KT.Quanta.Common.Enums;
using KT.Quanta.Service.Dtos;
using KT.Quanta.Service.Entities;
using System.Collections.Generic;
using System.Linq;

namespace KT.Quanta.Service.Models
{
    /// <summary>
    /// 电梯服务
    /// </summary>
    public class ElevatorServerModel : BaseQuantaModel
    {
        /// <summary>
        /// Ip地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 服务器账号
        /// </summary>
        public string PCAccount { get; set; }

        /// <summary>
        /// 服务器密码
        /// </summary>
        public string PCPassword { get; set; }

        /// <summary>
        /// 数据库账号
        /// </summary>
        public string DBAccount { get; set; }

        /// <summary>
        /// 数据库密码
        /// </summary>
        public string DBPassword { get; set; }

        /// <summary>
        /// 是否为主服务器
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        /// 关联电梯组Id
        /// </summary>
        public string ElevatorGroupId { get; set; }

        /// <summary>
        /// 关联电梯组
        /// </summary>
        public ElevatorGroupModel ElevatorGroup { get; set; }

        public static ElevatorServerEntity SetEntity(ElevatorServerEntity entity, ElevatorServerModel model)
        {
            if (model == null)
            {
                entity = null;
                return entity;
            }
            if (entity == null)
            {
                entity = new ElevatorServerEntity();
            }

            entity.Id = model.Id;
            entity.IpAddress = model.IpAddress;
            entity.Port = model.Port;
            entity.PCAccount = model.PCAccount;
            entity.PCPassword = model.PCPassword;
            entity.DBAccount = model.DBAccount;
            entity.DBPassword = model.DBPassword;
            entity.EditedTime = model.EditedTime;
            entity.IsMain = model.IsMain;

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

            return entity;
        }

        public static ElevatorServerEntity ToEntity(ElevatorServerModel model)
        {
            var entity = new ElevatorServerEntity();
            var result = SetEntity(entity, model);
            return result;
        }

        public static List<RemoteDeviceModel> ToRemoteDevices(List<ElevatorServerModel> models)
        {
            var remoteDevices = new List<RemoteDeviceModel>();
            if (models?.FirstOrDefault() != null)
            {
                foreach (var item in models)
                {
                    var remoteDevice = ToRemoteDevice(item);
                    remoteDevices.Add(remoteDevice);
                }
            }
            return remoteDevices;
        }

        public static RemoteDeviceModel ToRemoteDevice(ElevatorServerModel model)
        {
            var remoteModel = new RemoteDeviceModel();
            remoteModel.Name = $"电梯服务{model.IpAddress}:{model.Port}";
            remoteModel.DeviceType = DeviceTypeEnum.ELEVATOR_SERVER.Value;
            remoteModel.DeviceId = model.Id;

            remoteModel.CommunicateDeviceInfos = new List<CommunicateDeviceInfoModel>();
            var communicateDeviceInfo = new CommunicateDeviceInfoModel();
            communicateDeviceInfo.IpAddress = model.IpAddress;
            communicateDeviceInfo.Port = model.Port;
            communicateDeviceInfo.Account = model.PCAccount;
            communicateDeviceInfo.Password = model.PCPassword;

            remoteModel.CommunicateDeviceInfos.Add(communicateDeviceInfo);

            return remoteModel;
        }
    }
}
