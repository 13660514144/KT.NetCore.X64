using KT.Common.Tool.CleanFile.Helpers;
using KT.Common.Tool.CleanFile.Models;
using KT.Common.WebApi.Helpers;
using KT.Proxy.BackendApi.Apis;
using KT.WinPak.Commom.Helpers;
using KT.WinPak.Data.Base;
using KT.WinPak.Data.Daos;
using KT.WinPak.Data.IDaos;
using KT.WinPak.SDK;
using KT.WinPak.SDK.Entities;
using KT.WinPak.SDK.IServices;
using KT.WinPak.SDK.ISqlDaos;
using KT.WinPak.SDK.Services;
using KT.WinPak.SDK.Settings;
using KT.WinPak.SDK.SqlDaos;
using KT.WinPak.SDK.SqlSerivces;
using KT.WinPak.Service.IServices;
using KT.WinPak.Service.Models;
using KT.WinPak.Service.Services;
using KT.WinPak.WebApi.Common.Filters;
using KT.WinPak.WebApi.Common.Queues;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace KT.WinPak.WebApi
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

            //配置文件
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings").Bind);
            services.Configure<CleanFileSettings>(Configuration.GetSection("CleanFileSettings").Bind);

            // 配置本地数据库，设置绝对路径，避免Api生成目录在当前项目下与发布版本目录不一致
            string dbPath = Path.Combine(AppContext.BaseDirectory, "LocalData.db");
            services.AddDbContext<WinPakSqliteContext>(options => options.UseSqlite(@"Data Source=" + dbPath, p => p.MigrationsAssembly("KT.WinPak.WebApi")));

            services.AddDbContext<WINPAKPROContext>();

            //Local Data
            services.AddScoped<ILoginUserDataDao, LoginUserDataDao>();
            services.AddScoped<IUserTokenDataDao, UserTokenDataDao>();
            services.AddScoped<ILoginUserDataService, LoginUserDataService>();
            services.AddScoped<IUserTokenDataService, UserTokenDataService>();

            //HttpProxy
            services.AddSingleton<OpenApi>();

            //SDK
            services.AddSingleton<IAllSdkService, AllSdkService>();
            services.AddSingleton<AccwEventSdk>();

            //SQL Dao 
            services.AddScoped<ICardHolderSqlDao, CardHolderSqlDao>();
            services.AddScoped<ICardSqlDao, CardSqlDao>();
            services.AddScoped<IHWDeviceSqlDao, DeviceSqlDao>();
            services.AddScoped<ITimeZoneSqlDao, TimeZoneSqlDao>();
            services.AddScoped<IAccessLevelSqlDao, AccessLevelSqlDao>();

            //SQL Serivce 
            services.AddScoped<ICardHolderSqlService, CardHolderSqlService>();
            services.AddScoped<ICardSqlService, CardSqlService>();
            services.AddScoped<IHWDeviceSqlService, HWDeviceSqlService>();
            services.AddScoped<ITimeZoneSqlService, TimeZoneSqlService>();
            services.AddScoped<IAccessLevelSqlService, AccessLevelSqlService>();

            //SDK Serivce 
            services.AddScoped<ICardHolderSdkService, CardHolderSdkService>();
            services.AddScoped<ICardSdkService, CardSdkService>();
            services.AddScoped<IHWDeviceSdkService, HWDeviceSdkService>();
            services.AddScoped<ITimeZoneSdkService, TimeZoneSdkService>();
            services.AddScoped<IAccessLevelSdkService, AccessLevelSdkService>();

            // service
            services.AddScoped<IUserService, UserService>();

            //其它
            services.AddSingleton<SingletonExecuteQueue>();
            services.AddSingleton<SystemTimeHelper>();
            services.AddSingleton<CleanFileHelper>();

            // 添加全局拦截器
            services.AddMvc(option =>
            {
                // 异常捕获
                option.Filters.Add<Common.Filters.GlobalExceptionFilter>();
                // 队列单例执行，并发COM组件会出错
                option.Filters.Add<SingletonExecuteFilter>();
                //// 访问记录捕获
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

            //拦截所有访问
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
                //退出应用时注销当前用户
                MasterInfo.Logout?.Invoke();
            });
            hostApplicationLifetime.ApplicationStopped.Register(() =>
            {
                Console.WriteLine("ApplicationStopped------------------------------------------------------------------------------------");
            });
        }
    }
}
