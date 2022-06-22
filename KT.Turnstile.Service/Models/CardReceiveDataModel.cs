namespace KT.Turnstile.Model.Models
{
    /// <summary>
    /// 接收数据
    /// </summary>
    public class CardReceiveDataModel : BaseTurnstileModel
    {
        /// <summary>
        /// 设备类型
        /// 根据设备类型判断处理方式
        /// </summary>
        public CardDeviceModel Device { get; set; }

        /// <summary>
        /// 接收到的数据
        /// 数据格式是已经转换成能正常显示的格式
        /// </summary>
        public string ReceiveData { get; set; }
    }
}
