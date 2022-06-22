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
            //使用NewtonsoftJson，防止数字Id无法自动转换成字符串
            services.AddControllers().AddNewtonsoftJson();

            services.AddSignalR();

            //配置文件
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings").Bind);

            //// 配置本地数据库，设置绝对路径，避免Api生成目录在当前项目下与发布版本目录不一致
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

            // Data 数据库操作，用于仓储
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

            //Service 数据业务逻辑处理，与边缘处理器共用此库
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

            //Distribute 特殊业务逻辑处理及代理
            services.AddScoped<IPassRightDistribute, PassRightDistribute>();
            services.AddScoped<IRightGroupDistribute, RightGroupDistribute>();
            services.AddScoped<ICardDeviceDistribute, CardDeviceDistribute>();

            services.AddSingleton<ProcessorDeviceList>();
            services.AddSingleton<SeekSendHelper>();
            services.AddSingleton<JobSchedulerHelper>();
            services.AddSingleton<PushRecordHandler>();

            // Thrid 
            services.AddSingleton<PushApi>();

            // 添加全局拦截器
            services.AddMvc(option =>
            {
                // 全局异常捕获
                option.Filters.Add<GlobalExceptionFilter>();
                //// 全局访问记录捕获
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

            //拦截所有访问
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

                ////更新数据库
                //using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                //{
                //    //这里其实只是从程序服务中获取Dbcontext
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