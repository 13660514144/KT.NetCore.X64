using KT.Common.WebApi.IServices;
using KT.Common.WebApi.Services;
using KT.Prowatch.Service.Base;
using KT.Prowatch.Service.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace KT.Prowatch.WebApi
{
    /// <summary>
    /// 启动程序
    /// New-Service -Name KT.Prowatch.WebApi -BinaryPathName C:\QuantaPush\ProwatchWebApi\KT.Prowatch.WebApi.exe -Description "QuantaData Prowatch 门禁服务，请务关闭，否则相关程序无法运行！" -DisplayName "QuantaData Prowatch 门禁服务" -StartupType Automatic
    /// </summary>
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var hostBuilder = CreateHostBuilder(args);
            var webHost = hostBuilder.Build();
            await InitDataAsync(webHost);

            await webHost.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static async Task InitDataAsync(IHost webHost)
        {
            //初始化数据操作
            using (var scope = webHost.Services.CreateScope())
            {
                ILoggerFactory loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("Program");

                //迁移数据库，mac环境在启动时创建会延后执行
                logger.LogInformation("Database Migrate Started!");
                //这里其实只是从程序服务中获取Dbcontext
                var context = scope.ServiceProvider.GetRequiredService<ProwatchSqliteContext>();
                await context.Database.EnsureCreatedAsync();
                logger.LogInformation("Database Migrate End!");

                ///初始化数据 
                var userService = scope.ServiceProvider.GetRequiredService<ILoginUserService>();
                await userService.LoginLastAsync();

                ///初始化数据 
                var recordService = scope.ServiceProvider.GetRequiredService<IRecordService>();
                recordService.Start();

                logger.LogInformation("初始化登录成功!");
            };
        }
    }
}
