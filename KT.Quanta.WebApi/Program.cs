using KT.Common.Tool.CleanFile.Helpers;
using KT.Common.Tool.CleanFile.Models;
using KT.Device.Quanta;
using KT.Quanta.Service.Daos;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Kone.Helpers;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Clients;
using KT.Quanta.Service.Devices.Mitsubishi.Toward.ElipClients;
using KT.Quanta.Service.Devices.Mitsubishi.Toward.ElsgwServers;
using KT.Quanta.Service.Helpers;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Quanta.WebApi.Common.Helper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Threading.Tasks;
using HelperTools;
using Newtonsoft.Json.Linq;
using KT.Quanta.WebApi.Common.WsSocket;
using KT.Quanta.Service.Devices.Hikvision;
using System.IO;

namespace KT.Quanta.WebApi
{
    /// <summary>
    /// 启动程序
    /// New-Service -Name KT.Quanta.WebApi -BinaryPathName D:\QuantaPublish\quanta-web-api-win-x86-stand\KT.Quanta.WebApi.exe -Description "QuantaData 设备通信服务，请务关闭，否则相关程序无法运行！" -DisplayName "QuantaData 设备通信服务" -StartupType Automatic
    /// </summary>
    public class Program
    {
        private static IHost _host;
        public static int OnlyDataFlg=0;
        public static async Task StopHostAsync()
        {
            await _host.StopAsync();
        }

        public static async Task Main(string[] args)
        {
            //JObject O = AppConfig.GetConfig("DataConfig.json");
            //OnlyDataFlg = (int)O["OnlyDataRight"];
            //CreateHostBuilder(args).Build().Run();
            _host = CreateHostBuilder(args).Build();
            await InitDataAsync(_host);

            await _host.RunAsync();
        }

        private static async Task InitDataAsync(IHost webHost)
        {
            //初始化数据操作，mac环境执行数据查询出错
            using var scope = webHost.Services.CreateScope();

            ILoggerFactory loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("Program");
            try
            {

                await InitDataAsync(scope, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Initialize date error!");
            }
        }

        private static async Task InitDataAsync(IServiceScope scope, ILogger logger)
        {
            //迁移数据库，mac环境在启动时创建会延后执行
            logger.LogInformation("Database Migrate Started!");
            //这里其实只是从程序服务中获取Dbcontext
            var context = scope.ServiceProvider.GetRequiredService<QuantaDbContext>();
            await context.Database.EnsureCreatedAsync();
            logger.LogInformation("Database Migrate End!");
            //分流服务 2021-10-19
            var _ApiSendServer = scope.ServiceProvider.GetRequiredService<ApiSendServer>();
            _ApiSendServer.Init();
            var _DistirbQueueRun = scope.ServiceProvider.GetRequiredService<DistirbQueue>();
            _DistirbQueueRun.Init();
            var _DistirbQueueSchindler = scope.ServiceProvider.GetRequiredService<DistirbQueueSchindler>();
            _DistirbQueueSchindler.Init();
    
            var _HkQueue = scope.ServiceProvider.GetRequiredService<HkQueue>();
            _HkQueue.Init();
            //分流服务 2021-10-19
            //2021-10-29  websocket 数据传送专用
            var _WsSocket = scope.ServiceProvider.GetRequiredService<WsSocket>();
            _WsSocket.WsSocket_Init();
            //2021-10-29  websocket 数据传送专用

            //var sendclient = scope.ServiceProvider.GetRequiredService<SendClient>();
            //sendclient.Init();
            //流量监控

            // 初始化边缘处理器，没有则创建。当前设备加入边缘处理器列表
            var processorService = scope.ServiceProvider.GetRequiredService<IProcessorService>();
            await processorService.InitLoadAsync();
            //if (Program.OnlyDataFlg == 0)
            //{
                // 初始化派梯设备
                var handleElevatorDeviceService = scope.ServiceProvider.GetRequiredService<IHandleElevatorDeviceService>();
                await handleElevatorDeviceService.InitLoadAsync();

                //初始化电梯组
                var elevatorGroupService = scope.ServiceProvider.GetRequiredService<IElevatorGroupService>();
                await elevatorGroupService.InitLoadAsync();

                // 初如化电梯入设备
                var handleElevatorInputDeviceService = scope.ServiceProvider.GetRequiredService<IHandleElevatorInputDeviceService>();
                await handleElevatorInputDeviceService.InitLoadAsync();                

            //}
            //初始化设备
            await DeviceQuantaModuleExtensions.InitializeDeviceQuantaAsync(scope.ServiceProvider);
            //系统时间同步
            var systemTimeHelper = scope.ServiceProvider.GetRequiredService<SystemTimeHelper>();
            await systemTimeHelper.StartAsync();

            // 电梯服务转换  三菱
            var appSettings = scope.ServiceProvider.GetRequiredService<IOptions<AppSettings>>();
            if (appSettings.Value.Mitsubishi.Towards?.FirstOrDefault() != null)
            {
                var communicateDeviceList = scope.ServiceProvider.GetRequiredService<CommunicateDeviceList>();

                var mitsubishiTowardGroupList = scope.ServiceProvider.GetRequiredService<MitsubishiTowardElsgwUdpServerHostList>();

                foreach (var item in appSettings.Value.Mitsubishi.Towards)
                {
                    // 启动ELSGW电梯服务
                    var mitsubishiTowardElsgwUdpServerHost = scope.ServiceProvider.GetRequiredService<IMitsubishiTowardElsgwUdpServerHost>();

                    // 获取已经包含的电梯
                    await communicateDeviceList.ExecuteAsync((communicateDevice) =>
                    {
                        //检查电梯连接是否存在，存在则使用已存在连接服务，不存在则创建
                        if (item.ElevatorIp == communicateDevice.CommunicateDeviceInfo.IpAddress
                          && item.ElevatorPort == communicateDevice.CommunicateDeviceInfo.Port)
                        {
                            mitsubishiTowardElsgwUdpServerHost.MitsubishiElipClientHost = communicateDevice.GetLoginUserClient<IMitsubishiElipClientHost>();
                        }

                        return Task.CompletedTask;
                    });

                    //电梯连接不存在电梯组中，则创建连接
                    if (mitsubishiTowardElsgwUdpServerHost.MitsubishiElipClientHost == null)
                    {
                        // 连接E-LIP电梯服务
                        mitsubishiTowardElsgwUdpServerHost.MitsubishiElipClientHost = scope.ServiceProvider.GetRequiredService<IMitsubishiTowardElipTcpClientHost>();
                        await mitsubishiTowardElsgwUdpServerHost.MitsubishiElipClientHost.InitAsync(item.ElevatorIp, item.ElevatorPort);
                    }

                    //修改电梯转换数据
                    await mitsubishiTowardElsgwUdpServerHost.MitsubishiElipClientHost.SetLocalPortAsync(item.ClientPort);

                    //Toward Elsgw Udp服务放最后初始化，否则Elip客户端服务无法赋值到ElipRequestHandler
                    await mitsubishiTowardElsgwUdpServerHost.InitAsync(item.ServerPort);

                    //将电梯组加入队列
                    await mitsubishiTowardGroupList.AddAsync(mitsubishiTowardElsgwUdpServerHost);
                }
            }
            // 通力电梯加载配置
            var koneService = scope.ServiceProvider.GetRequiredService<IKoneService>();
            await koneService.RefreshKoneConfigHekperAsync();
            // 定时操作（最后开启）
            var jobSchedulerHelper = scope.ServiceProvider.GetRequiredService<JobSchedulerHelper>();
            await jobSchedulerHelper.StartAsync();


            // 文件清除
            var cleanFileHelper = scope.ServiceProvider.GetRequiredService<CleanFileHelper>();
            var cleanFileSettings = scope.ServiceProvider.GetRequiredService<IOptions<CleanFileSettings>>();
            await cleanFileHelper.StartAsync(cleanFileSettings.Value);

            logger.LogInformation("初始化数据成功！");
          
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseWindowsService()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .UseWebRoot(Directory.GetCurrentDirectory() + @"\wwwroot\")
                    ;
                });
    }

    //$acl = Get-Acl "{EXE PATH}"
    //$aclRuleArgs = {DOMAIN OR COMPUTER NAME\USER}, "Read,Write,ReadAndExecute", "ContainerInherit,ObjectInherit", "None", "Allow"
    //$accessRule = New-Object System.Security.AccessControl.FileSystemAccessRule($aclRuleArgs)
    //$acl.SetAccessRule($accessRule)
    //$acl | Set-Acl "{EXE PATH}"

    //New-Service -Name {SERVICE NAME} -BinaryPathName {EXE FILE PATH} -Credential {DOMAIN OR COMPUTER NAME\USER} -Description "{DESCRIPTION}" -DisplayName "{DISPLAY NAME}" -StartupType Automatic
}
