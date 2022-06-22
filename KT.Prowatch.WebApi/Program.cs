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
    /// ��������
    /// New-Service -Name KT.Prowatch.WebApi -BinaryPathName C:\QuantaPush\ProwatchWebApi\KT.Prowatch.WebApi.exe -Description "QuantaData Prowatch �Ž���������رգ�������س����޷����У�" -DisplayName "QuantaData Prowatch �Ž�����" -StartupType Automatic
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
            //��ʼ�����ݲ���
            using (var scope = webHost.Services.CreateScope())
            {
                ILoggerFactory loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("Program");

                //Ǩ�����ݿ⣬mac����������ʱ�������Ӻ�ִ��
                logger.LogInformation("Database Migrate Started!");
                //������ʵֻ�Ǵӳ�������л�ȡDbcontext
                var context = scope.ServiceProvider.GetRequiredService<ProwatchSqliteContext>();
                await context.Database.EnsureCreatedAsync();
                logger.LogInformation("Database Migrate End!");

                ///��ʼ������ 
                var userService = scope.ServiceProvider.GetRequiredService<ILoginUserService>();
                await userService.LoginLastAsync();

                ///��ʼ������ 
                var recordService = scope.ServiceProvider.GetRequiredService<IRecordService>();
                recordService.Start();

                logger.LogInformation("��ʼ����¼�ɹ�!");
            };
        }
    }
}
