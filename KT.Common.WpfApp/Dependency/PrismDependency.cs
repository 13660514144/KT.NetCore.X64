using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KT.Common.WpfApp.Dependency
{
    public class PrismDependency
    {
        public static IConfiguration GetConfiguration()
        {
            //获取配置文件
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            //构建配置文件
            return builder.Build();
        }

    }
}
