using KT.Elevator.Manage.Base;
using KT.Elevator.Manage.Service.Helpers;
using KT.Elevator.Manage.Service.IServices;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.WebApi
{
    /// <summary>
    /// 启动程序
    /// New-Service -Name KT.Elevator.Manage.WebApi -BinaryPathName C:\QuantaPush\ElevatorWebApi\KT.Elevator.Manage.WebApi.exe -Description "QuantaData Elevator 梯控服务，请务关闭，否则相关程序无法运行！" -DisplayName "QuantaData Elevator 梯控服务" -StartupType Automatic
    /// </summary>
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //CreateHostBuilder(args).Build().Run();
            var webHost = CreateHostBuilder(args).Build();

            //Console.WriteLine(DependencyContext.Default.Target.Framework);
            //DependencyContext.Default.RuntimeLibraries.Where(x => x.Name.Contains("Microsoft.NETCore.App"))
            //    .ToList().ForEach(x => Console.WriteLine($"{x.Name} {x.Version}"));

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
                var context = scope.ServiceProvider.GetRequiredService<ElevatorDbContext>();
                await context.Database.EnsureCreatedAsync();
                logger.LogInformation("Database Migrate End!");

                // 初始化边缘处理器，没有则创建。当前设备加入边缘处理器列表
                var processorService = scope.ServiceProvider.GetRequiredService<IProcessorService>();
                await processorService.InitLoadAsync();

                // 初始化边缘处理器，没有则创建。当前设备加入边缘处理器列表
                var handleElevatorDeviceService = scope.ServiceProvider.GetRequiredService<IHandleElevatorDeviceService>();
                await handleElevatorDeviceService.InitLoadAsync();

                // 初如化边缘处理器输入设备
                var handleElevatorInputDeviceService = scope.ServiceProvider.GetRequiredService<IHandleElevatorInputDeviceService>();
                await handleElevatorInputDeviceService.InitLoadAsync();

                // 开启Sokcet服务 
                var seekSendHelper = scope.ServiceProvider.GetRequiredService<SeekSendHelper>();
                seekSendHelper.Start();

                // 定时操作
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

    //$acl = Get-Acl "{EXE PATH}"
    //$aclRuleArgs = {DOMAIN OR COMPUTER NAME\USER}, "Read,Write,ReadAndExecute", "ContainerInherit,ObjectInherit", "None", "Allow"
    //$accessRule = New-Object System.Security.AccessControl.FileSystemAccessRule($aclRuleArgs)
    //$acl.SetAccessRule($accessRule)
    //$acl | Set-Acl "{EXE PATH}"

    //New-Service -Name {SERVICE NAME} -BinaryPathName {EXE FILE PATH} -Credential {DOMAIN OR COMPUTER NAME\USER} -Description "{DESCRIPTION}" -DisplayName "{DISPLAY NAME}" -StartupType Automatic
}

