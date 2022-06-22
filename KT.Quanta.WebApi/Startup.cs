using AutoMapper;
using KT.Common.Event;
using KT.Common.Netty.Clients;
using KT.Common.Netty.Servers;
using KT.Common.Tool.CleanFile.Helpers;
using KT.Common.Tool.CleanFile.Models;
using KT.Common.WebApi.Helpers;
using KT.Device;
using KT.Device.Quanta;
using KT.Device.Quanta.Clients;
using KT.Elevator.Manage.Service.Services;
using KT.Proxy.BackendApi.Apis;
using KT.Quanta.Common.Enums;
using KT.Quanta.IDao.Kone;
using KT.Quanta.Service.Daos;
using KT.Quanta.Service.Daos.Kone;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.DeviceDistributes;
using KT.Quanta.Service.Devices.Hikvision;
using KT.Quanta.Service.Devices.Hikvision.Sdks;
using KT.Quanta.Service.Devices.Hitachi;
using KT.Quanta.Service.Devices.Hitachi.Handlers;
using KT.Quanta.Service.Devices.Hitachi.SendDatas;
using KT.Quanta.Service.Devices.Kone;
using KT.Quanta.Service.Devices.Kone.Clients;
using KT.Quanta.Service.Devices.Kone.Helpers;
using KT.Quanta.Service.Devices.Kone.Models;
using KT.Quanta.Service.Devices.Mitsubishi.Elip;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Clients;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Coders;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Handlers;
using KT.Quanta.Service.Devices.Mitsubishi.Elip.Helpers;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Clients;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Coders;
using KT.Quanta.Service.Devices.Mitsubishi.Elsgw.Handlers;
using KT.Quanta.Service.Devices.Mitsubishi.Toward.ElipClients;
using KT.Quanta.Service.Devices.Mitsubishi.Toward.ElsgwServers;
using KT.Quanta.Service.Devices.Mitsubishi.Toward.Handlers;
using KT.Quanta.Service.Devices.Quanta;
using KT.Quanta.Service.Devices.Quanta.DistributeDatas;
using KT.Quanta.Service.Devices.Quanta.Helpers;
using KT.Quanta.Service.Devices.Schindler;
using KT.Quanta.Service.Devices.Schindler.Clients;
using KT.Quanta.Service.Devices.Schindler.Helpers;
using KT.Quanta.Service.Devices.Schindler.Models;
using KT.Quanta.Service.Devices.Self;
using KT.Quanta.Service.Elevator.Services;
using KT.Quanta.Service.Handlers;
using KT.Quanta.Service.Helpers;
using KT.Quanta.Service.Hubs;
using KT.Quanta.Service.IDaos;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Services;
using KT.Quanta.Service.Turnstile.IServices;
using KT.Quanta.Service.Turnstile.Services;
using KT.Quanta.WebApi.Common.Filters;
using KT.Quanta.WebApi.Common.Helper;
using KT.Quanta.WebApi.Common.WsSocket;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
namespace KT.Quanta.WebApi
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
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                //忽略不是成员对象
                options.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                //循环引用强制序列化
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            //AutoMapper配置
            services.AddAutoMapper((config) =>
            {

            });
            // 在当前作用域的所有程序集里面扫描AutoMapper的配置文件；
            // 参数类型是Assembly类型的数组 表示AutoMapper将在这些程序集数组里面遍历寻找所有继承了Profile类的配置文件             
            var ownerAssemblies = AssemblyExtenstions.GetOwnerAll();
            services.AddAutoMapper(ownerAssemblies);
            //services.AddAutoMapper(typeof(MobanProfile).Assembly);
            services.AddSignalR().AddHubOptions<QuantaDistributeHub>(options =>
            {
                options.MaximumReceiveMessageSize = 1024*1024;// long.MaxValue;                
                options.ClientTimeoutInterval = TimeSpan.FromSeconds(60);
                options.EnableDetailedErrors = true;
            });
            //配置文件
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings").Bind);
            services.Configure<SchindlerSettings>(Configuration.GetSection("SchindlerSettings").Bind);
            services.Configure<KoneSettings>(Configuration.GetSection("KoneSettings").Bind);
            services.Configure<CleanFileSettings>(Configuration.GetSection("CleanFileSettings").Bind);

            
            services.AddDbContext<QuantaDbContext>(options =>
            {
                options.UseMySql(Configuration.GetConnectionString("MysqlConnection"),
                    b => b.MigrationsAssembly("KT.Quanta.Service"));
                
                //options.ValidateScopes = false;
            });

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
            services.AddScoped<IKoneService, KoneService>();

            //Service 数据业务逻辑处理，与边缘处理器共用此库
            services.AddScoped<ITurnstilePersonService, TurnstilePersonService>();
            services.AddScoped<ITurnstileProcessorService, TurnstileProcessorService>();
            services.AddScoped<ITurnstilePassRightService, TurnstilePassRightService>();
            services.AddScoped<ITurnstileCardDeviceService, TurnstileCardDeviceService>();
            services.AddScoped<ITurnstileRelayDeviceService, TurnstileRelayDeviceService>();
            services.AddScoped<ITurnstileCardDeviceRightGroupService, TurnstileCardDeviceRightGroupService>();
            services.AddScoped<ITurnstileCardDeviceDeviceDistributeService, TurnstileCardDeviceDeviceDistributeService>();
            services.AddScoped<ITurnstileCardDeviceRightGroupDeviceDistributeService, TurnstileCardDeviceRightGroupDeviceDistributeService>();
            services.AddScoped<ITurnstilePassRightDeviceDistributeService, TurnstilePassRightDeviceDistributeService>();
            services.AddScoped<IQuantaTurnstileCardDeviceDistributeDataService, QuantaTurnstileCardDeviceDistributeDataService>();
            services.AddScoped<IQuantaTurnstilePassRightDistributeDataService, QuantaTurnstilePassRightDistributeDataService>();
            services.AddScoped<IQuantaTurnstileRightGroupDistributeDataService, QuantaTurnstileRightGroupDistributeDataService>();
            services.AddScoped<ICardDeviceRightGroupDao, CardDeviceRightGroupDao>();
            services.AddScoped<IRelayDeviceDao, RelayDeviceDao>();
            services.AddScoped<IPassRightAccessibleFloorDao, PassRightAccessibleFloorDao>();
            services.AddScoped<IPassRightAccessibleFloorDetailDao, PassRightAccessibleFloorDetailDao>();
            services.AddScoped<IPassRightAccessibleFloorService, PassRightAccessibleFloorService>();
            services.AddScoped<IPassRightDestinationFloorDao, PassRightDestinationFloorDao>();
            services.AddScoped<IPassRightDestinationFloorService, PassRightDestinationFloorService>();
            services.AddScoped<IHandleElevatorDeviceAuxiliaryDao, HandleElevatorDeviceAuxiliaryDao>();
            services.AddScoped<IHandleElevatorDeviceAuxiliaryService, HandleElevatorDeviceAuxiliaryService>();
            services.AddScoped<IDopMaskRecordDao, DopMaskRecordDao>();
            services.AddScoped<IDopMaskRecordService, DopMaskRecordService>();
            services.AddScoped<IEliOpenAccessForDopMessageTypeDao, EliOpenAccessForDopMessageTypeDao>();
            services.AddScoped<IEliPassRightHandleElevatorDeviceCallTypeDao, EliPassRightHandleElevatorDeviceCallTypeDao>();
            services.AddScoped<IRcgifPassRightHandleElevatorDeviceCallTypeDao, RcgifPassRightHandleElevatorDeviceCallTypeDao>();
            services.AddScoped<IElevatorGroupFloorDao, ElevatorGroupFloorDao>();

            //Distribute 特殊业务逻辑处理及代理
            services.AddScoped<IQuantaPassRightDistributeDataService, QuantaPassRightDistributeDataService>();
            services.AddScoped<IQuantaCardDeviceDistributeDataService, QuantaCardDeviceDistributeDataService>();
            services.AddScoped<IQuantaHandleElevatorDeviceDistributeDataService, QuantaHandleElevatorDeviceDistributeDataService>();
            services.AddScoped<IQuantaDisplayDistributeDataService, QuantaDisplayDistributeDataService>();
            services.AddScoped<IElevatorPassRightDeviceDistributeService, ElevatorPassRightDeviceDistributeService>();
            services.AddScoped<IElevatorCardDeviceDeviceDistributeService, ElevatorCardDeviceDeviceDistributeService>();            
            services.AddScoped<IHandleElevatorDeviceDeviceDistributeService, HandleElevatorDeviceDeviceDistributeService>();            
            services.AddScoped<IElevatorDeviceInfoDeviceDistributeService, ElevatorDeviceInfoDeviceDistributeService>();
            services.AddScoped<IHandleElevatorInputDeviceDao, HandleElevatorInputDeviceDao>();
            services.AddScoped<IHandleElevatorInputDeviceService, HandleElevatorInputDeviceService>();
            services.AddScoped<IFaceInfoDao, FaceInfoDao>();
            services.AddScoped<IFaceInfoService, FaceInfoService>();
            services.AddScoped<IHikvisionResponseHandler, HikvisionResponseHandler>();
            services.AddScoped<ITurnstilePassRecordService, TurnstilePassRecordService>();

            services.AddSingleton<RemoteDeviceList>();
            services.AddSingleton<CommunicateDeviceList>();
            services.AddSingleton<JobSchedulerHelper>();
            services.AddSingleton<PushRecordHandler>();
            services.AddSingleton<HandleElevatorInputDeviceList>();
            services.AddSingleton<IRemoteDeviceFactory, RemoteDeviceFactory>();
            services.AddSingleton<ICommunicateDeviceFactory, CommunicateDeviceFactory>();
            services.AddSingleton<FloorHandleElevatorResponseList>();
            services.AddSingleton<PushUrlHelper>();
            services.AddSingleton<ElevatorPassRightDistributeQueue>();
            services.AddSingleton<TurnstilePassRightDistributeQueue>();

            services.AddTransient<SelfRemoteDevice>();

            services.AddScoped<DistributeHelper>();
            services.AddTransient<QuantaCommunicateDevice>();
            services.AddTransient<QuantaRemoteDevice>();
            services.AddTransient<QuantaElevatorDataRemoteService>();
            services.AddTransient<QuantaTurnstileDisplayRemoteService>();
            services.AddTransient<QuantaTurnstileOperateRemoteService>();
            services.AddTransient<QuantaElevatorDisplayRemoteService>();
            services.AddTransient<QuantaTurnstileDataRemoteService>();
            services.AddTransient<QuantaElevatorSelectorRemoteService>();
            services.AddTransient<QuantaDisplayDistributeDataService>();
            services.AddSingleton<IQuantaHandleElevatorSequenceList, QuantaHandleElevatorSequenceList>();

            //if (Program.OnlyDataFlg == 0)
            //{
                services.AddTransient<HikvisionCommunicateDevice>();
                services.AddTransient<CHCNetSDKService>();
                services.AddTransient<HikvisionElevatorRemoteDevice>();
                services.AddTransient<HikvisionElevatorDataRemoteService>();
                services.AddTransient<HikvisionElevatorDeviceExecuteQueue>();
                services.AddTransient<HikvisionTurnstileRemoteDevice>();
                services.AddTransient<HikvisionTurnstileDataRemoteService>();
                services.AddTransient<IHikvisionSdkService, HikvisionSdkService>();
                services.AddTransient<IHandleElevatorDeviceDistributeService, HandleElevatorDeviceDistributeService>();
                services.AddTransient<HikvisionSdkExecuteQueue>();
                services.AddTransient<HikvisionTurnstileDeviceExecuteQueue>();

                services.AddTransient<KoneHandleElevatorRemoteService>();
                services.AddTransient<KoneRcgifCommunicateDevice>();
                services.AddTransient<KoneEliCommunicateDevice>();
                services.AddTransient<IKoneRcgifReponseHandler, KoneRcgifReponseHandler>();
                services.AddTransient<IKoneRcgifClientHost, KoneRcgifClientHost>();
                services.AddTransient<IKoneRcgifClientHandler, KoneRcgifClientHandler>();
                services.AddTransient<KoneElevatorSelectorRemoteDevice>();
                services.AddTransient<KoneElevatorGroupRemoteDevice>();
                services.AddTransient<KoneElevatorServerRemoteDevice>();
                services.AddTransient<IKoneEliReponseHandler, KoneEliReponseHandler>();
                services.AddTransient<IKoneEliClientHost, KoneEliClientHost>();
                services.AddTransient<IKoneEliClientHandler, KoneEliClientHandler>();
                services.AddSingleton<IKoneRcgifHandleElevatorSequenceList, KoneRcgifHandleElevatorSequenceList>();
                services.AddSingleton<IKoneEliHandleElevatorSequenceList, KoneEliHandleElevatorSequenceList>();
                services.AddTransient<KoneEliClientFrameDecoder>();
                services.AddTransient<KoneEliClientFrameEncoder>();
                services.AddTransient<KoneRcgifClientFrameDecoder>();
                services.AddTransient<KoneRcgifClientFrameEncoder>();

                services.AddTransient<HitachiHandleElevatorRemoteService>();
                services.AddTransient<HitachiElevatorGroupRemoteDevice>();
                services.AddSingleton<IHitachiHandleElevatorSequenceList, HitachiHandleElevatorSequenceList>();
                services.AddTransient<HitachiElevatorDataRemoteService>();
                services.AddTransient<HitachiElevatorRemoteDevice>();
                services.AddTransient<IHitachiHandleElevatorDistributeDataService, HitachiHandleElevatorDistributeDataService>();
                services.AddTransient<IHitachiCallReponseHandler, HitachiCallReponseHandler>();

                services.AddTransient<SchindlerHandleElevatorRemoteService>();
                services.AddTransient<SchindlerCommunicateDevice>();
                services.AddTransient<ISchindlerReponseHandler, SchindlerReponseHandler>();
                services.AddTransient<ISchindlerDatabaseClientHost, SchindlerDatabaseClientHost>();
                services.AddTransient<ISchindlerDatabaseClientHandler, SchindlerDatabaseClientHandler>();
                services.AddTransient<ISchindlerDispatchClientHost, SchindlerDispatchClientHost>();
                services.AddTransient<ISchindlerDispatchClientHandler, SchindlerDispatchClientHandler>();
                services.AddTransient<ISchindlerReportClientHost, SchindlerReportClientHost>();
                services.AddTransient<ISchindlerReportClientHandler, SchindlerReportClientHandler>();
                services.AddTransient<SchindlerElevatorDataRemoteService>();
                services.AddTransient<SchindlerElevatorSelectorRemoteDevice>();
                services.AddTransient<SchindlerElevatorGroupRemoteDevice>();
                services.AddTransient<SchindlerElevatorServerRemoteDevice>();
                services.AddTransient<SchindlerElevatorRecordRemoteService>();
                services.AddSingleton<ISchindlerHandleElevatorSequenceList, SchindlerHandleElevatorSequenceList>();
                services.AddSingleton<SchindlerDatabaseQueue>();

                services.AddTransient<MitsubishiElsgwCommunicateDevice>();
                services.AddTransient<MitsubishiElsgwHandleElevatorRemoteService>();
                services.AddTransient<MitsubishiElsgwElevatorSelectorRemoteDevice>();
                services.AddTransient<MitsubishiElsgwElevatorGroupRemoteDevice>();
                services.AddTransient<MitsubishiElsgwElevatorServerRemoteDevice>();
                services.AddTransient<IMitsubishiElsgwClientHost, MitsubishiElsgwUdpClientHost>();
                services.AddTransient<IMitsubishiElsgwClientHandler, MitsubishiElsgwClientHandler>();
                services.AddTransient<IMitsubishiElsgwReponseHandler, MitsubishiElsgwReponseHandler>();
                services.AddTransient<IMitsubishiElsgwFrameDecoder, MitsubishiElsgwFrameDecoder>();
                services.AddTransient<IMitsubishiElsgwFrameEncoder, MitsubishiElsgwFrameEncoder>();

                services.AddTransient<MitsubishiElipFrameEncoder>();
                services.AddTransient<MitsubishiElipFrameEncoder>();
                services.AddTransient<MitsubishiElipFrameDecoder>();
                services.AddTransient<MitsubishiElipCommunicateDevice>();
                services.AddTransient<MitsubishiElipHandleElevatorRemoteService>();
                services.AddTransient<MitsubishiElipElevatorSelectorRemoteDevice>();
                services.AddTransient<MitsubishiElipElevatorGroupRemoteDevice>();
                services.AddTransient<MitsubishiElipElevatorServerRemoteDevice>();
                services.AddTransient<IMitsubishiElipClientHost, MitsubishiElipTcpClientHost>();
                services.AddTransient<IMitsubishiElipClientHandler, MitsubishiElipClientHandler>();
                services.AddTransient<IMitsubishiElipReponseHandler, MitsubishiElipReponseHandler>();

                services.AddSingleton<MitsubishiTowardElsgwUdpServerHostList>();
                services.AddTransient<IMitsubishiTowardElipTcpClientHost, MitsubishiTowardElipTcpClientHost>();
                services.AddTransient<IMitsubishiTowardElsgwUdpServerHost, MitsubishiTowardElsgwUdpServerHost>();
                services.AddTransient<IMitsubishiTowardElipClientHandler, MitsubishiTowardElipClientHandler>();
                services.AddTransient<IMitsubishiTowardElipResponseHandler, MitsubishiTowardElipResponseHandler>();
                services.AddTransient<IMitsubishiTowardElsgwServerHandler, MitsubishiTowardElsgwServerHandler>();
                services.AddTransient<IMitsubishiTowardElsgwRequestHandler, MitsubishiTowardElsgwRequestHandler>();
                services.AddSingleton<IMitsubishiElipHandleElevatorSequenceList, MitsubishiElipHandleElevatorSequenceList>();
            //}
            services.AddSingleton<IDeviceList, DeviceList>();
            services.AddSingleton<IEventAggregator, EventAggregator>();
            services.AddTransient<QuantaClientFrameEncoder>();
            services.AddTransient<QuantaClientFrameDecoder>();
            services.AddTransient<IQuantaClientHost, QuantaClientHost>();
            services.AddTransient<IQuantaClientHandler, QuantaClientHandler>();
            services.AddTransient<QuantaServerFrameEncoder>();
            services.AddTransient<QuantaServerFrameDecoder>();
            //services.AddTransient<IQuantaServerHost, QuantaServerHost>();
            //services.AddTransient<IQuantaServerHandler, QuantaServerHandler>(); 

            services.AddScoped<IDopGlobalDefaultAccessMaskDao, DopGlobalDefaultAccessMaskDao>();
            services.AddScoped<IDopSpecificDefaultAccessMaskDao, DopSpecificDefaultAccessMaskDao>();

            //注入设备
            services.RegisterDeviceQuantaTypes();

            // Thrid 
            services.AddSingleton<OpenApi>();
            services.AddSingleton<SystemTimeHelper>();
            services.AddSingleton<CleanFileHelper>();
            //第三方派梯回显队队 xianliu 
            services.AddSingleton<ElevatorMessageKey>();
            //services.AddSingleton<NetFlowTimer>();

            //2021-10-19  分流服务
            services.AddSingleton<ApiSendServer>();
            services.AddSingleton<DistirbQueue>();
            services.AddSingleton<DistirbQueueSchindler>();
            //2021-10-29  websocket 数据传送专用
            services.AddSingleton<WsSocket>();
            services.AddSingleton<HkQueue>();
            //2021-10-19  分流服务

            //services.AddSingleton<SendClient>();
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
            // 开启静态页面
            DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("index.html");
            app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();
          
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
                endpoints.MapHub<QuantaDistributeHub>("/distribute");
            });

            hostApplicationLifetime.ApplicationStarted.Register(() =>
            {
                Console.WriteLine("ApplicationStarted------------------------------------------------------------------------------------");
            });
            hostApplicationLifetime.ApplicationStopping.Register(async () =>
            {
                logger.LogInformation("ApplicationStopping------------------------------------------------------------------------------------");
                await StopRemoteDevices(app);
                logger.LogInformation("ApplicationStopping====================================================================================");
            });
            hostApplicationLifetime.ApplicationStopped.Register(async () =>
            {
                logger.LogInformation("ApplicationStopped------------------------------------------------------------------------------------");
                await StopRemoteDevices(app);
                logger.LogInformation("ApplicationStopped====================================================================================");
            });           
        }

        private static async Task StopRemoteDevices(IApplicationBuilder app)
        {
            var communicateDevices = app.ApplicationServices.GetRequiredService<CommunicateDeviceList>();

            //关闭通力电梯
            await communicateDevices.ExecuteAsync(async (communicateDevice) =>
            {
                if (communicateDevice != null)
                {
                    if (communicateDevice.CommunicateDeviceInfo.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_ELI.Value
                        || communicateDevice.CommunicateDeviceInfo.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_RCGIF.Value)
                    {
                        Task.Run(() =>
                        {
                            communicateDevice.CloseAsync();
                        });
                    }
                }
            });

            await Task.Delay(10 * 1000);

            //关闭所有连接设备
            await communicateDevices.ExecuteAsync(async (communicateDevice) =>
            {
                if (communicateDevice != null)
                {
                    if (communicateDevice.CommunicateDeviceInfo.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_ELI.Value
                        || communicateDevice.CommunicateDeviceInfo.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_RCGIF.Value)
                    {
                        return;
                    }
                    await communicateDevice.CloseAsync();
                }
            });

            //关闭三菱派梯转换
            var mitsubishiTowardElsgwUdpServerHosts = app.ApplicationServices.GetRequiredService<MitsubishiTowardElsgwUdpServerHostList>();
            await mitsubishiTowardElsgwUdpServerHosts.ExecuteAsync(async (mitsubishiTowardElsgwUdpServerHost) =>
            {
                if (mitsubishiTowardElsgwUdpServerHost != null)
                {
                    await mitsubishiTowardElsgwUdpServerHost.CloseAsync();
                }
            });
        }
    }
}
