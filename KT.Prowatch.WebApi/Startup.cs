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
            //使用NewtonsoftJson，防止数字Id无法自动转换成字符串
            services.AddControllers().AddNewtonsoftJson();

            //配置文件
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings").Bind);

            // 配置本地数据库，设置绝对路径，避免Api生成目录在当前项目下与发布版本目录不一致
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

            // 添加全局拦截器
            services.AddMvc(option =>
            {
                // 异常捕获
                option.Filters.Add<GlobalExceptionFilter>();
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
