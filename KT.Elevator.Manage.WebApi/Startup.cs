using AutoMapper;
using KT.Common.WebApi.Helpers;
using KT.Elevator.Manage.Base;
using KT.Elevator.Manage.Service.Daos;
using KT.Elevator.Manage.Service.Devices.Common;
using KT.Elevator.Manage.Service.Devices.Quanta.DistributeDatas;
using KT.Elevator.Manage.Service.Devices.DeviceDistributes;
using KT.Elevator.Manage.Service.Devices.Hikvision;
using KT.Elevator.Manage.Service.Devices.Kone;
using KT.Elevator.Manage.Service.Devices.Quanta;
using KT.Elevator.Manage.Service.Handlers;
using KT.Elevator.Manage.Service.Helpers;
using KT.Elevator.Manage.Service.Hubs;
using KT.Elevator.Manage.Service.IDaos;
using KT.Elevator.Manage.Service.IServices;
using KT.Elevator.Manage.Service.Models;
using KT.Elevator.Manage.Service.Services;
using KT.Elevator.Manage.WebApi.Common.Filters;
using KT.Proxy.BackendApi.Apis;
using KT.Proxy.QuantaApi.Turnstile;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace KT.Elevator.Manage.WebApi
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
            // 在当前作用域的所有程序集里面扫描AutoMapper的配置文件；参数类型是Assembly类型的数组 表示AutoMapper将在这些程序集数组里面遍历寻找所有继承了Profile类的配置文件             
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSignalR().AddHubOptions<DistributeHub>(options =>
            {
                options.MaximumReceiveMessageSize = long.MaxValue;
            });

            //配置文件
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings").Bind);

            //// 配置本地数据库，设置绝对路径，避免Api生成目录在当前项目下与发布版本目录不一致
            //string dbPath = Path.Combine(AppContext.BaseDirectory, "LocalData.db");
            //services.AddDbContext<SqliteContext>(options =>
            //{
            //    options.UseSqlite(@"Data Source=" + dbPath, p => p.MigrationsAssembly("KT.Elevator.Manage.WebApi"));
            //    //options.ValidateScopes = false;
            //});
            ////services.AddDbContext<SqliteContext>();
            services.AddDbContext<ElevatorDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("MysqlConnection"));
                //options.ValidateScopes = false;
            });

            services.AddSingleton<PushUrlHelper>();
            services.AddScoped<KoneHandleDevice>();
            services.AddScoped<KoneServerGroup>();
            services.AddScoped<KoneServer>();
            services.AddScoped<QuantaDisplayDistributeDataService>();

            // Data 数据库操作，用于仓储
            services.AddScoped<ISerialConfigDao, SerialConfigDao>();
            services.AddScoped<IProcessorDao, ProcessorDao>();
            services.AddScoped<IPassRightDao, PassRightDao>();
            services.AddScoped<ICardDeviceDao, CardDeviceDao>();
            services.AddScoped<ISystemConfigDao, SystemConfigDao>();
            services.AddScoped<IDistributeErrorDao, DistributeErrorDao>();
            services.AddScoped<IPassRecordDao, PassRecordDao>();
            services.AddScoped<ILoginUserDao, LoginUserDao>();
            services.AddScoped<IEdificeDao, EdificeDao>();
            services.AddScoped<IFloorDao, FloorDao>();
            services.AddScoped<IElevatorGroupDao, ElevatorGroupDao>();
            services.AddScoped<IElevatorServerDao, ElevatorServerDao>();
            services.AddScoped<IHandleElevatorDeviceDao, HandleElevatorDeviceDao>();
            services.AddScoped<IPersonDao, PersonDao>();
            services.AddScoped<IElevatorInfoDao, ElevatorInfoDao>();
            services.AddScoped<IProcessorFloorDao, ProcessorFloorDao>();

            //Service 数据业务逻辑处理，与边缘处理器共用此库
            services.AddScoped<ISerialConfigService, SerialConfigService>();
            services.AddScoped<IProcessorService, ProcessorService>();
            services.AddScoped<IPassRightService, PassRightService>();
            services.AddScoped<ICardDeviceService, CardDeviceService>();
            services.AddScoped<ISystemConfigService, SystemConfigService>();
            services.AddScoped<IDistributeErrorService, DistributeErrorService>();
            services.AddScoped<IPassRecordService, PassRecordService>();
            services.AddScoped<ILoginUserService, LoginUserService>();
            services.AddScoped<IEdificeService, EdificeService>();
            services.AddScoped<IFloorService, FloorService>();
            services.AddScoped<IElevatorGroupService, ElevatorGroupService>();
            services.AddScoped<IElevatorServerService, ElevatorServerService>();
            services.AddScoped<IHandleElevatorDeviceService, HandleElevatorDeviceService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IElevatorInfoService, ElevatorInfoService>();
            services.AddScoped<IProcessorFloorService, ProcessorFloorService>();

            //Distribute 特殊业务逻辑处理及代理
            services.AddScoped<IQuantaPassRightDistributeDataService, QuantaPassRightDistributeDataService>();
            services.AddScoped<IQuantaCardDeviceDistributeDataService, QuantaCardDeviceDistributeDataService>();
            services.AddScoped<IQuantaHandleElevatorDeviceDistributeDataService, QuantaHandleElevatorDeviceDistributeDataService>();
            services.AddScoped<IQuantaDisplayDistributeDataService, QuantaDisplayDistributeDataService>();
            services.AddScoped<IPassRightDeviceDistributeService, PassRightDeviceDistributeService>();
            services.AddScoped<ICardDeviceDeviceDistributeService, CardDeviceDeviceDistributeService>();
            services.AddScoped<IHandleElevatorDeviceDeviceDistributeService, HandleElevatorDeviceDeviceDistributeService>();
            services.AddScoped<IDeviceInfoDeviceDistributeService, DeviceInfoDeviceDistributeService>();
            services.AddScoped<IHandleElevatorInputDeviceDao, HandleElevatorInputDeviceDao>();
            services.AddScoped<IHandleElevatorInputDeviceService, HandleElevatorInputDeviceService>();
            services.AddScoped<IFaceInfoDao, FaceInfoDao>();
            services.AddScoped<IFaceInfoService, FaceInfoService>();
            services.AddScoped<IHikvisionService, HikvisionService>();

            services.AddSingleton<RemoteDeviceList>();
            services.AddSingleton<SeekSendHelper>();
            services.AddSingleton<JobSchedulerHelper>();
            services.AddSingleton<PushRecordHandler>();
            services.AddSingleton<KoneHandleDeviceList>();
            services.AddSingleton<HandleElevatorInputDeviceList>();
            services.AddSingleton<IRemoteDeviceFactory, RemoteDeviceFactory>();

            services.AddScoped<DistributeHelper>();
            services.AddTransient<QuantaRemoteDevice>();
            services.AddTransient<HikvisionRemoteDevice>();
            //services.AddTransient<QuantaRemoteDeviceService>();
            services.AddTransient<QuantaElevatorRemoteService>();
            services.AddTransient<QuantaDisplayRemoteService>();
            services.AddTransient<HikvisionElevatorRemoteService>();
            services.AddTransient<IHikvisionSdkService, HikvisionSdkService>();

            //Quanta api
            services.AddScoped<IInternalApi, InternalApi>();

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