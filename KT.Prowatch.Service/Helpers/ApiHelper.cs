using KT.Prowatch.Service.Extensions;

namespace KT.Prowatch.Service.Helpers
{
    /// <summary>
    /// 调用动态库方法
    /// </summary>
    public class ApiHelper
    {
        private static ProwatchApiProvider pwapi;
        /// <summary>
        /// Prowathc 动态库api
        /// </summary>
        public static ProwatchApiProvider PWApi
        {
            get
            {
                if (pwapi == null)
                {
                    lock (locker)
                    {
                        if (pwapi == null)
                        {
                            pwapi = new ProwatchApiProvider();
                        }
                    }
                }
                return pwapi;
            }
        }
        private static object locker = new object();
        private ApiHelper()
        {

        }
    }
}