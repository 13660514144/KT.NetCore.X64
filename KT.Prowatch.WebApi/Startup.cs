using KT.Common.WebApi.Filters;
using KT.Common.WebApi.Helpers;
using KT.Common.WebApi.IServices;
using KT.Common.WebApi.Services;
using KT.Prowatch.Service.Base;
using KT.Prowatch.Service.Daos;
using KT.Prowatch.Service.Helpers;
using KT.Prowatch.Service.IDaos;
using KT.Prowatch.Service.IServices;
using KT.Prowatch.Service.Models;
using KT.Prowatch.Service.Services;
using KT.Prowatch.WebApi.Common.Filters;
using KT.Proxy.BackendApi.Apis;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace KT.Prowatch.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //ʹ��NewtonsoftJson����ֹ����Id�޷��Զ�ת�����ַ���
            services.AddControllers().AddNewtonsoftJson();

            //�����ļ�
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings").Bind);

            // ���ñ������ݿ⣬���þ���·��������Api����Ŀ¼�ڵ�ǰ��Ŀ���뷢���汾Ŀ¼��һ��
            string dbPath = Path.Combine(AppContext.BaseDirectory, "LocalData.db");
            services.AddDbContext<ProwatchSqliteContext>(options => options.UseSqlite(@"Data Source=" + dbPath, p => p.MigrationsAssembly("KT.Prowatch.WebApi")));

            services.AddDbContext<ProwatchContext>();

            services.AddScoped<ILoginUserDao, LoginUserDao>();
            services.AddScoped<IUserTokenDao, UserTokenDao>();
            services.AddScoped<IProwatchDao, ProwatchDao>();
            services.AddScoped<ILoginUserService, LoginUserService>();
            services.AddScoped<IUserTokenService, UserTokenService>();
            services.AddScoped<IProwatchService, ProwatchService>();
            services.AddScoped<IPushEventService, PushEventService>();

            //HttpProxy
            services.AddSingleton<OpenApi>();
            services.AddSingleton<InitHelper>();
            services.AddSingleton<DownloadCardStateQueue>();

            services.AddSingleton<IRecordService, RecordService>();

            // ���ȫ��������
            services.AddMvc(option =>
            {
                // �쳣����
                option.Filters.Add<GlobalExceptionFilter>();
                //// ���ʼ�¼����
                //option.Filters.Add<GlobalRecordFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime hostApplicationLifetime, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();
            var logger = loggerFactory.CreateLogger("Startup.Configure");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //�������з���
            app.UseGlobalRecord();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            hostApplicationLifetime.ApplicationStarted.Register(() =>
            {
                logger.LogInformation("Application Started!");
            });
            hostApplicationLifetime.ApplicationStopping.Register(() =>
            {
                //�˳�Ӧ��ʱע����ǰ�û�
                MasterInfo.Logout?.Invoke();
            });
            hostApplicationLifetime.ApplicationStopped.Register(() =>
            {
                Console.WriteLine("ApplicationStopped------------------------------------------------------------------------------------");
            });
        }
    }
}
