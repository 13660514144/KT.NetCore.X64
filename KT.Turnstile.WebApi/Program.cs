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
        /// ��������
        /// New-Service -Name KT.Turnstile.Manage.WebApi -BinaryPathName D:\QuantaPush\turnstile-manage-api-win-x86-stand\KT.Turnstile.Manage.WebApi.exe -Description "QuantaData Turnstile բ����������رգ�������س����޷����У�" -DisplayName "QuantaData Turnstile բ������" -StartupType Automatic
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
            //��ʼ�����ݲ�����mac����ִ�����ݲ�ѯ����
            using (var scope = webHost.Services.CreateScope())
            {
                ILoggerFactory loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger("Program");

                //Ǩ�����ݿ⣬mac����������ʱ�������Ӻ�ִ��
                logger.LogInformation("Database Migrate Started!");
                //������ʵֻ�Ǵӳ�������л�ȡDbcontext
                var context = scope.ServiceProvider.GetRequiredService<QuantaDbContext>();
                await context.Database.EnsureCreatedAsync();
                logger.LogInformation("Database Migrate End!");

                ////��ʼ����Ե��������û���򴴽�����ǰ�豸�����Ե�������б�
                var processorService = scope.ServiceProvider.GetRequiredService<IProcessorService>();
                await processorService.InitProcessorAsync();
                //����Sokcet���� 
                //var seekReceiveHelper = scope.ServiceProvider.GetRequiredService<SeekReceiveHelper>();
                //seekReceiveHelper.Start();
                var seekSendHelper = scope.ServiceProvider.GetRequiredService<SeekSendHelper>();
                seekSendHelper.Start();
                //��ʱ����
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
}

