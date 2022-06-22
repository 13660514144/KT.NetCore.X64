using KT.Common.Tool.CleanFile.Helpers;
using KT.Common.Tool.CleanFile.Models;
using KT.WinPak.Commom.Helpers;
using KT.WinPak.Data.Base;
using KT.WinPak.SDK.Models;
using KT.WinPak.SDK.Settings;
using KT.WinPak.Service.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace KT.WinPak.WebApi
{
    /// <summary>
    /// 启动程序
    /// New-Service -Name KT.WinPak.WebApi -BinaryPathName D:\QuantaPush\winpak-web-api-win-x64-stand\KT.WinPak.WebApi.exe -Description "QuantaData WinPak 门禁服务，请务关闭，否则相关程序无法运行！" -DisplayName "QuantaData WinPak 门禁服务" -StartupType Automatic
    /// </summary>
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webHost = CreateHostBuilder(args).Build();

            await InitDataAsync(webHost);

            await webHost.RunAsync();
        }

        private static async Task InitDataAsync(IHost webHost)
        {
            //初始化数据操作
            using (var scope = webHost.Services.CreateScope())
            {
                ILoggerFactory loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("Program");

                //迁移数据库，mac环境在启动时创建会延后执行
                logger.LogInformation("Database Migrate Started!");
                //这里其实只是从程序服务中获取Dbcontext
                var context = scope.ServiceProvider.GetRequiredService<WinPakSqliteContext>();
                await context.Database.EnsureCreatedAsync();
                logger.LogInformation("Database Migrate End!");

                ///初始化数据 
                IServiceScopeFactory serviceScopeFactory = scope.ServiceProvider.GetRequiredService<IServiceScopeFactory>();
                IOptions<AppSettings> optionAppSettings = scope.ServiceProvider.GetRequiredService<IOptions<AppSettings>>();
                UserLoginAsync(logger, serviceScopeFactory, optionAppSettings.Value);

                //系统时间同步
                var systemTimeHelper = scope.ServiceProvider.GetRequiredService<SystemTimeHelper>();
                await systemTimeHelper.StartAsync(() =>
                {
                    return SdkStaticInfo.PushUrl;
                });

                // 文件清除
                var cleanFileHelper = scope.ServiceProvider.GetRequiredService<CleanFileHelper>();
                var cleanFileSettings = scope.ServiceProvider.GetRequiredService<IOptions<CleanFileSettings>>();
                await cleanFileHelper.StartAsync(cleanFileSettings.Value);

                logger.LogInformation("初始化程序完成!");
            };
        }

        private static async void UserLoginAsync(ILogger logger, IServiceScopeFactory serviceScopeFactory, AppSettings appSettings)
        {
            await Task.Delay(appSettings.StartDelayLoginSecond * 1000);

            using (var scope = serviceScopeFactory.CreateScope())
            {
                logger.LogInformation($"初始化登录!");
                ///初始化数据 
                IUserService userService = scope.ServiceProvider.GetRequiredService<IUserService>();
                userService.Login();
                logger.LogInformation("初始化登录成功!");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
