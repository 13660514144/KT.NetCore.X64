using KT.Common.Netty.Common;
using KT.Quanta.Common.Models;

namespace KT.Quanta.Unit.Model.Reponses
{
    /// <summary>
    /// 分发错误记录，边缘处理器重新连接时继续分发
    /// </summary>
    public class ErrorReponse : QuantaSerializer
    {
        /// <summary>
        /// 推送类型 
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Json格式分发数据
        /// </summary>
        public string DataContent { get; set; }

        /// <summary>
        /// 为上传的数据更新时间，用于排序
        /// </summary>
        public long EditedTime { get; set; }

        protected override void Read()
        {
            Type = ReadString();
            DataContent = ReadString();
            EditedTime = ReadLong();
        }

        protected override void Write()
        {
            WriteString(Type);
            WriteString(DataContent);
            WriteLong(EditedTime);
        }
    }
}
