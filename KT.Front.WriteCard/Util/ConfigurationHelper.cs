using Microsoft.Extensions.Configuration;
using System.IO;

namespace KT.Front.WriteCard.Util
{
    public class ConfigurationHelper
    {
        public static IConfiguration GetConfiguration()
        {
            //获取配置文件
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            //构建配置文件
            return builder.Build();
        }
    }
}
