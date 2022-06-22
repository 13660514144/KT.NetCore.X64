using KT.Common.Netty.Common;
using KT.Quanta.Common.Models;

namespace KT.Quanta.Unit.Model.Requests
{
    /// <summary>
    /// 系统配置
    /// </summary> 
    public class SystemConfigRequest : QuantaSerializer
    {
        /// <summary>
        /// Key值
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Value值
        /// </summary>
        public string Value { get; set; }

        protected override void Read()
        {
            Key = ReadString();
            Value = ReadString();
        }

        protected override void Write()
        {
            WriteString(Key);
            WriteString(Value);
        }
    }
}
