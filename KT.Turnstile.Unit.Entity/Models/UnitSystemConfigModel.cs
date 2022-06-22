namespace KT.Turnstile.Unit.Entity.Models
{
    public class UnitSystemConfigModel
    {
        /// <summary>
        /// 服务器ip
        /// </summary>
        public string ServerIp { get; set; }

        /// <summary>
        /// 服务器端口
        /// </summary>
        public int ServerPort { get; set; }

        /// <summary>
        /// 服务器ip
        /// </summary>
        public string ClientIp { get; set; }

        /// <summary>
        /// 服务器事件上传地址
        /// </summary>
        public string PushAddress { get; set; }

        /// <summary>
        /// 最后同步数据时间
        /// </summary>
        public long LastSyncTime { get; set; }

        /// <summary>
        /// 错误重试次数
        /// </summary>
        public int ErrorRetimes { get; set; }

        /// <summary>
        /// 错误销毁次数，累计错误到一定次数不再执行重试操作
        /// </summary>
        public int ErrorDestroyRetimes { get; set; }

        public UnitSystemConfigModel()
        {
            ErrorRetimes = 5;
            ErrorDestroyRetimes = 20;
        }
    }
}
