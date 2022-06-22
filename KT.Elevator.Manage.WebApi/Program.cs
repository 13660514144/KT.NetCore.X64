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
    /// ��������
    /// New-Service -Name KT.Elevator.Manage.WebApi -BinaryPathName C:\QuantaPush\ElevatorWebApi\KT.Elevator.Manage.WebApi.exe -Description "QuantaData Elevator �ݿط�������رգ�������س����޷����У�" -DisplayName "QuantaData Elevator �ݿط���" -StartupType Automatic
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
            //��ʼ�����ݲ�����mac����ִ�����ݲ�ѯ����
            using (var scope = webHost.Services.CreateScope())
            {
                ILoggerFactory loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("Program");

                //Ǩ�����ݿ⣬mac����������ʱ�������Ӻ�ִ��
                logger.LogInformation("Database Migrate Started!");
                //������ʵֻ�Ǵӳ�������л�ȡDbcontext
                var context = scope.ServiceProvider.GetRequiredService<ElevatorDbContext>();
                await context.Database.EnsureCreatedAsync();
                logger.LogInformation("Database Migrate End!");

                // ��ʼ����Ե��������û���򴴽�����ǰ�豸�����Ե�������б�
                var processorService = scope.ServiceProvider.GetRequiredService<IProcessorService>();
                await processorService.InitLoadAsync();

                // ��ʼ����Ե��������û���򴴽�����ǰ�豸�����Ե�������б�
                var handleElevatorDeviceService = scope.ServiceProvider.GetRequiredService<IHandleElevatorDeviceService>();
                await handleElevatorDeviceService.InitLoadAsync();

                // ���绯��Ե�����������豸
                var handleElevatorInputDeviceService = scope.ServiceProvider.GetRequiredService<IHandleElevatorInputDeviceService>();
                await handleElevatorInputDeviceService.InitLoadAsync();

                // ����Sokcet���� 
                var seekSendHelper = scope.ServiceProvider.GetRequiredService<SeekSendHelper>();
                seekSendHelper.Start();

                // ��ʱ����
                var jobSchedulerHelper = scope.ServiceProvider.GetRequiredService<JobSchedulerHelper>();
                await jobSchedulerHelper.StartAsync();
                 
                logger.LogInformation("��ʼ�����ݳɹ���");
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

