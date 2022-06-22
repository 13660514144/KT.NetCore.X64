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
            //ʹ��NewtonsoftJson����ֹ����Id�޷��Զ�ת�����ַ���
            services.AddControllers().AddNewtonsoftJson();
            // �ڵ�ǰ����������г�������ɨ��AutoMapper�������ļ�������������Assembly���͵����� ��ʾAutoMapper������Щ���������������Ѱ�����м̳���Profile��������ļ�             
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSignalR().AddHubOptions<DistributeHub>(options =>
            {
                options.MaximumReceiveMessageSize = long.MaxValue;
            });

            //�����ļ�
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings").Bind);

            //// ���ñ������ݿ⣬���þ���·��������Api����Ŀ¼�ڵ�ǰ��Ŀ���뷢���汾Ŀ¼��һ��
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

            // Data ���ݿ���������ڲִ�
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

            //Service ����ҵ���߼��������Ե���������ô˿�
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

            //Distribute ����ҵ���߼���������
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