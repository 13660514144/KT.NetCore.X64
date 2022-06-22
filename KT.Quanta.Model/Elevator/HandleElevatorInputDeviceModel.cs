using KT.Quanta.Service.Entities;
using Newtonsoft.Json;

namespace KT.Quanta.Service.Models
{
    public class HandleElevatorInputDeviceModel : BaseQuantaModel
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 通行类型 
        /// </summary>
        [JsonIgnore]
        public string AccessType { get; set; }

        /// <summary>
        /// 设备类型 
        /// </summary>
        [JsonProperty("cardType")]
        public string DeviceType { get; set; }

        /// <summary>
        /// 串口名
        /// </summary>
        public string PortName { get; set; }

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline { get; set; }

        /// <summary>
        /// 关联派梯设备Id
        /// </summary>
        public string HandleElevatorDeviceId { get; set; }

        /// <summary>
        /// 关联派梯设备
        /// </summary>
        public HandleElevatorDeviceModel HandleElevatorDevice { get; set; }

        public static HandleElevatorInputDeviceEntity ToEntity(HandleElevatorInputDeviceModel model)
        {
            var entity = new HandleElevatorInputDeviceEntity();
            entity = SetEntity(entity, model);
            return entity;
        }

        public static HandleElevatorInputDeviceEntity SetEntity(HandleElevatorInputDeviceEntity entity, HandleElevatorInputDeviceModel model)
        {
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.AccessType = model.AccessType;
            entity.DeviceType = model.DeviceType;
            entity.PortName = model.PortName;
            model.EditedTime = entity.EditedTime;

            //关联派梯设备
            if (!string.IsNullOrEmpty(model.HandleElevatorDeviceId))
            {
                if (entity.HandElevatorDevice == null || entity.HandElevatorDevice.Id != model.HandleElevatorDeviceId)
                {
                    entity.HandElevatorDevice = new HandleElevatorDeviceEntity();
                    entity.HandElevatorDevice.Id = model.HandleElevatorDeviceId;
                }
            }

            return entity;
        }
    }
}
