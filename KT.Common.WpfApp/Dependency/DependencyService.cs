using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KT.Common.WpfApp.Dependency
{
    /// <summary>
    /// 依赖注入服务
    /// </summary>
    public class DependencyService
    {
        /// <summary>
        /// 服务提供程序
        /// </summary>
        public static IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// 默认配置文件appsettings.json
        /// </summary>
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// 所有服务
        /// </summary>
        public static ServiceCollection Services { get; set; }

        /// <summary>
        /// 开启服务注入
        /// </summary>
        /// <param name="action">添加服务</param>
        public static void StartServices(Action<ServiceCollection> action)
        {
            //创建服务集合
            Services = new ServiceCollection();

            //设置配置文件
            if (File.Exists(Path.Combine(AppContext.BaseDirectory, "appsettings.json")))
            {
                //获取配置文件
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                //构建配置文件
                Configuration = builder.Build();

                //配置文件注入
                Services.Configure<IConfiguration>((configuration) =>
                {
                    configuration = Configuration;
                });
            }

            //服务注入
            action.Invoke(Services);

            //构建ServiceProvider对象
            ServiceProvider = Services.BuildServiceProvider();
            Services.AddSingleton(ServiceProvider);
        }
    }
}
