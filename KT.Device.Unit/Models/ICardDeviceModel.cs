namespace KT.Device.Unit.CardReaders.Models
{
    public interface ICardDeviceModel : ISerialDeviceModel
    {
        /// <summary>
        /// 设备类型
        /// </summary>
        string BrandModel { get; set; }

        /// <summary>
        /// 卡类型
        /// </summary>
        string DeviceType { get; set; }
    }
}
