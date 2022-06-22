using KT.Quanta.Common.Enums;

namespace KT.Device.Unit.CardReaders.Models
{
    public class CardDeviceSettings
    {
        /// <summary>
        /// 是否大端模式
        /// </summary>
        public bool R824IcCardBigEndian { get; set; } = false;

        /// <summary>
        /// 二维码规则
        /// 西塔：XITA
        /// 康塔：QUANTA
        /// <see cref="KT.Quanta.Common.Enums.QrCodeTypeEnum"/>
        /// </summary>
        public string QrCodeType { get; set; } = QrCodeTypeEnum.Quanta.Value;

        /// <summary>
        /// 二维码加密密钥
        /// 西塔：Radio888ATTeBDsPKmGYM4Ud
        /// 康塔：%QUANTA@DATA$COM
        /// </summary>
        public string QrCodeKey { get; set; } = "Radio888ATTeBDsPKmGYM4Ud";
    }
}
