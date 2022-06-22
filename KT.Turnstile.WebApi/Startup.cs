using KT.Common.WebApi.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.Turnstile.Manage.Service.Base;
using KT.Turnstile.Manage.Service.Daos;
using KT.Turnstile.Manage.Service.Distribute;
using KT.Turnstile.Manage.Service.Handlers;
using KT.Turnstile.Manage.Service.Helpers;
using KT.Turnstile.Manage.Service.Hubs;
using KT.Turnstile.Manage.Service.IDaos;
using KT.Turnstile.Manage.Service.IDistribute;
using KT.Turnstile.Manage.Service.IServices;
using KT.Turnstile.Manage.Service.Services;
using KT.Turnstile.Manage.WebApi.Common.Filters;
using KT.Turnstile.Model.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace KT.Turnstile.Manage.WebApi
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

            services.AddSignalR();

            //�����ļ�
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings").Bind);

            //// ���ñ������ݿ⣬���þ���·��������Api����Ŀ¼�ڵ�ǰ��Ŀ���뷢���汾Ŀ¼��һ��
            //string dbPath = Path.Combine(AppContext.BaseDirectory, "LocalData.db");
            //services.AddDbContext<SqliteContext>(options =>
            //{
            //    options.UseSqlite(@"Data Source=" + dbPath, p => p.MigrationsAssembly("KT.Turnstile.Manage.WebApi"));
            //    //options.ValidateScopes = false;
            //});
            ////services.AddDbContext<SqliteContext>();
            services.AddDbContext<QuantaDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("MysqlConnection"));
                //options.ValidateScopes = false;
            });

            services.AddSingleton<PushUrlHelper>();

            // Data ���ݿ���������ڲִ�
            services.AddScoped<ISerialConfigDao, SerialConfigDao>();
            services.AddScoped<IProcessorDao, ProcessorDao>();
            services.AddScoped<IPassRightDao, PassRightDao>();
            services.AddScoped<ICardDeviceDao, CardDeviceDao>();
            services.AddScoped<IRelayDeviceDao, RelayDeviceDao>();
            services.AddScoped<ISystemConfigDao, SystemConfigDao>();
            services.AddScoped<IDistributeErrorDao, DistributeErrorDao>();
            services.AddScoped<IPassRecordDao, PassRecordDao>();
            services.AddScoped<ICardDeviceRightGroupDao, CardDeviceRightGroupDao>();
            services.AddScoped<ILoginUserDao, LoginUserDao>(); 

            //Service ����ҵ���߼��������Ե���������ô˿�
            services.AddScoped<ISerialConfigService, SerialConfigService>();
            services.AddScoped<IProcessorService, ProcessorService>();
            services.AddScoped<IPassRightService, PassRightService>();
            services.AddScoped<ICardDeviceService, CardDeviceService>();
            services.AddScoped<IRelayDeviceService, RelayDeviceService>();
            services.AddScoped<ISystemConfigService, SystemConfigService>();
            services.AddScoped<IDistributeErrorService, DistributeErrorService>();
            services.AddScoped<IPassRecordService, PassRecordService>();
            services.AddScoped<ICardDeviceRightGroupService, CardDeviceRightGroupService>();
            services.AddScoped<ILoginUserService, LoginUserService>(); 

            //Distribute ����ҵ���߼���������
            services.AddScoped<IPassRightDistribute, PassRightDistribute>();
            services.AddScoped<IRightGroupDistribute, RightGroupDistribute>();
            services.AddScoped<ICardDeviceDistribute, CardDeviceDistribute>();

            services.AddSingleton<ProcessorDeviceList>();
            services.AddSingleton<SeekSendHelper>();
            services.AddSingleton<JobSchedulerHelper>();
            services.AddSingleton<PushRecordHandler>();

            // Thrid 
            services.AddSingleton<PushApi>();

            // ���ȫ��������
            services.AddMvc(option =>
            {
                // ȫ���쳣����
                option.Filters.Add<GlobalExceptionFilter>();
                //// ȫ�ַ��ʼ�¼����
                //option.Filters.Add<GlobalRecordFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline., EchoServerProgram echoServerProgram
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IHostApplicationLifetime hostApplicationLifetime,
            ILoggerFactory loggerFactory)
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
                endpoints.MapHub<DistributeHub>("/distribute");
            });

            hostApplicationLifetime.ApplicationStarted.Register(() =>
            {
                Console.WriteLine("ApplicationStarted------------------------------------------------------------------------------------");

                //logger.LogInformation("Application Started!");

                ////�������ݿ�
                //using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                //{
                //    //������ʵֻ�Ǵӳ�������л�ȡDbcontext
                //    var context = serviceScope.ServiceProvider.GetRequiredService<SqliteContext>();
                //    context.Database.Migrate();
                //}

                //logger.LogInformation("Application Started End!");
            });
            hostApplicationLifetime.ApplicationStopping.Register(() =>
            {
                Console.WriteLine("ApplicationStopping------------------------------------------------------------------------------------");
            });
            hostApplicationLifetime.ApplicationStopped.Register(() =>
            {
                Console.WriteLine("ApplicationStopping------------------------------------------------------------------------------------");
            });
        }
    }
}