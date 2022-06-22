using KT.Turnstile.Manage.Service.Base;
using KT.Turnstile.Manage.Service.Helpers;
using KT.Turnstile.Manage.Service.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace KT.Turnstile.Manage.WebApi
{
    public class Program
    {
        /// <summary>
        /// 启动程序
        /// New-Service -Name KT.Turnstile.Manage.WebApi -BinaryPathName D:\QuantaPush\turnstile-manage-api-win-x86-stand\KT.Turnstile.Manage.WebApi.exe -Description "QuantaData Turnstile 闸机服务，请务关闭，否则相关程序无法运行！" -DisplayName "QuantaData Turnstile 闸机服务" -StartupType Automatic
        /// </summary>
        public static async Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var webHost = CreateHostBuilder(args).Build();

            await InitDataAsync(webHost);
            await webHost.RunAsync();
        }

        private static async Task InitDataAsync(IHost webHost)
        {
            //初始化数据操作，mac环境执行数据查询出错
            using (var scope = webHost.Services.CreateScope())
            {
                ILoggerFactory loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("Program");

                //迁移数据库，mac环境在启动时创建会延后执行
                logger.LogInformation("Database Migrate Started!");
                //这里其实只是从程序服务中获取Dbcontext
                var context = scope.ServiceProvider.GetRequiredService<QuantaDbContext>();
                await context.Database.EnsureCreatedAsync();
                logger.LogInformation("Database Migrate End!");

                ////初始化边缘处理器，没有则创建。当前设备加入边缘处理器列表
                var processorService = scope.ServiceProvider.GetRequiredService<IProcessorService>();
                await processorService.InitProcessorAsync();
                //开启Sokcet服务 
                //var seekReceiveHelper = scope.ServiceProvider.GetRequiredService<SeekReceiveHelper>();
                //seekReceiveHelper.Start();
                var seekSendHelper = scope.ServiceProvider.GetRequiredService<SeekSendHelper>();
                seekSendHelper.Start();
                //定时操作
                var jobSchedulerHelper = scope.ServiceProvider.GetRequiredService<JobSchedulerHelper>();
                await jobSchedulerHelper.StartAsync();

                logger.LogInformation("初始化数据成功！");
            };
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

