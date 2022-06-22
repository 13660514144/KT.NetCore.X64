using AutoMapper;
using KT.Common.Core.Enums;
using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Common.Data.Models;
using KT.Common.Tool.CleanFile.Helpers;
using KT.Common.Tool.CleanFile.Models;
using KT.Common.WpfApp.Dependency;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Proxy.QuantaApi.Apis;
using KT.Proxy.QuantaApi.Helpers;
using KT.Quanta.Kone.ToolApp.Enums;
using KT.Quanta.Kone.ToolApp.Events;
using KT.Quanta.Kone.ToolApp.Models;
using KT.Quanta.Kone.ToolApp.Views;
using KT.Quanta.Kone.ToolApp.Views.Details;
using KT.Quanta.Model.Elevator;
using KT.Quanta.Model.Elevator.Dtos;
using KT.Quanta.Model.Kone;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using Prism.Ioc;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace KT.Quanta.Kone.ToolApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private ILogger<App> _logger;
        private IEventAggregator _eventAggregator;

        public App()
        {
            _logger = new Log4gHelper<App>();

            //全局异常
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            new EventWaitHandle(false, EventResetMode.AutoReset, AppDomain.CurrentDomain.FriendlyName, out bool createNew);

            if (!createNew)
            {
                MessageBox.Show("已经存在运行的应用程序，不能同时运行多个应用程序！");
                App.Current.Shutdown();
                Environment.Exit(0);
            }

            base.OnStartup(e);
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            _logger?.LogError($"App_DispatcherUnhandledException Error：{JsonConvert.SerializeObject(e.Exception, JsonUtil.JsonPrintSettings)} ");

            var exception = e.Exception.GetInner();
            MessageBox.Show(exception.Message);

            //发布错误事件
            _eventAggregator?.GetEvent<ExceptionEvent>().Publish(exception);

            //标识事件已提交
            e.Handled = true;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (e.ExceptionObject as Exception);
            _logger?.LogError($"CurrentDomain_UnhandledException Error：{JsonConvert.SerializeObject(ex, JsonUtil.JsonPrintSettings)} ");

            var exception = ex.GetInner();
            Dispatcher.Invoke(() =>
            {
                MessageBox.Show(exception.Message);
            });

            //发布错误事件
            _eventAggregator?.GetEvent<ExceptionEvent>().Publish(exception);
        }
        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            _logger?.LogError($"TaskScheduler_UnobservedTaskException Error：{JsonConvert.SerializeObject(e.Exception, JsonUtil.JsonPrintSettings)} ");

            //异步处理错误消息
            TaskSchedulerUnobservedTaskExceptionExecAsync(e);

            //将异常标识为已经观察到 
            e.SetObserved();
        }

        private Task TaskSchedulerUnobservedTaskExceptionExecAsync(UnobservedTaskExceptionEventArgs e)
        {
            return Task.Run(() =>
            {
                var exception = e.GetInner();

                Dispatcher.Invoke(() =>
                {
                    MessageBox.Show(exception.Message);
                });

                //发布错误事件
                _eventAggregator?.GetEvent<ExceptionEvent>().Publish(exception);
            });
        }

        private async void RegistedAsync()
        {
            _eventAggregator = Container.Resolve<IEventAggregator>();

            var appSettings = Container.Resolve<AppSettings>();
            RequestHelper.BackendBaseUrl = appSettings.ServerAddress;

            await Task.CompletedTask;
        }

        private async void InitDataAsync()
        {
            //文件清除
            var cleanFileHelper = Container.Resolve<CleanFileHelper>();
            var cleanFileSettings = Container.Resolve<CleanFileSettings>();
            await cleanFileHelper.StartAsync(cleanFileSettings);
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver((viewType) =>
            {
                //获取View名称
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;

                //获取ViewModel名称
                var suffix = viewName.EndsWith("View") ? "Model" : "ViewModel";
                var viewModelName = $"{viewName}{suffix}, {viewAssemblyName}";

                _logger.LogInformation($"ViewModel绑定：viewName：{viewName} viewModelName：{viewModelName}");

                //返回结果
                return Type.GetType(viewModelName);
            });
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var logger = new Log4gHelper();

            containerRegistry.RegisterInstance(Container);

            ContainerHelper.SetProvider(logger, Container);

            //确定值的类
            containerRegistry.RegisterInstance<ILogger>(logger);

            //公用组件
            var appSettings = PrismDependency.GetConfiguration().GetSection("AppSettings").Get<AppSettings>();
            containerRegistry.RegisterInstance(appSettings);
            var cleanFileSettings = PrismDependency.GetConfiguration().GetSection("CleanFileSettings").Get<CleanFileSettings>();
            containerRegistry.RegisterInstance(cleanFileSettings);

            ConnectedStateEnum.Connected.Code = appSettings.ConnectMaskState;
            ConnectedStateEnum.DisConnected.Code = appSettings.DisonnectMaskState;
            ConnectedStateEnum.DisconnectedAndConnectedMaskState.Code = appSettings.DisconnectedAndConnectedMaskState;

            //后台Api接口
            containerRegistry.Register<BackendApiBase>();
            containerRegistry.Register<IEdificeApi, EdificeApi>();
            containerRegistry.RegisterInstance<ILogger<EdificeApi>>(new Log4gHelper<EdificeApi>());
            containerRegistry.Register<IElevatorGroupApi, ElevatorGroupApi>();
            containerRegistry.RegisterInstance<ILogger<ElevatorGroupApi>>(new Log4gHelper<ElevatorGroupApi>());
            containerRegistry.Register<IKoneApi, KoneApi>();
            containerRegistry.RegisterInstance<ILogger<KoneApi>>(new Log4gHelper<KoneApi>());
            containerRegistry.Register<IFloorApi, FloorApi>();
            containerRegistry.RegisterInstance<ILogger<FloorApi>>(new Log4gHelper<FloorApi>());
            containerRegistry.Register<IPassRightApi, PassRightApi>();
            containerRegistry.RegisterInstance<ILogger<PassRightApi>>(new Log4gHelper<PassRightApi>());
            containerRegistry.Register<IHandleElevatorDeviceApi, HandleElevatorDeviceApi>();
            containerRegistry.RegisterInstance<ILogger<HandleElevatorDeviceApi>>(new Log4gHelper<HandleElevatorDeviceApi>());
            containerRegistry.Register<IPassRightAccessibleFloorApi, PassRightAccessibleFloorApi>();
            containerRegistry.RegisterInstance<ILogger<PassRightAccessibleFloorApi>>(new Log4gHelper<PassRightAccessibleFloorApi>());
            containerRegistry.Register<IPassRightDestinationFloorApi, PassRightDestinationFloorApi>();
            containerRegistry.RegisterInstance<ILogger<PassRightDestinationFloorApi>>(new Log4gHelper<PassRightDestinationFloorApi>());
            containerRegistry.Register<IHandleElevatorDeviceAuxiliaryApi, HandleElevatorDeviceAuxiliaryApi>();
            containerRegistry.RegisterInstance<ILogger<HandleElevatorDeviceAuxiliaryApi>>(new Log4gHelper<HandleElevatorDeviceAuxiliaryApi>());
            containerRegistry.Register<IDopMaskRecordApi, DopMaskRecordApi>();
            containerRegistry.RegisterInstance<ILogger<DopMaskRecordApi>>(new Log4gHelper<DopMaskRecordApi>());

            // Page Navigation
            containerRegistry.RegisterForNavigation<FloorDirectionControl>();
            containerRegistry.RegisterForNavigation<EditDopSpecificMaskControl>();
            containerRegistry.RegisterForNavigation<DopSpecificMaskListControl>();
            containerRegistry.RegisterForNavigation<EditDopGlobalMaskControl>();
            containerRegistry.RegisterForNavigation<DopGlobalMaskListControl>();
            containerRegistry.RegisterForNavigation<PassRightListControl>();
            containerRegistry.RegisterForNavigation<SettingControl>();
            containerRegistry.RegisterForNavigation<PassRightAccessibleFloorControl>();
            containerRegistry.RegisterForNavigation<PassRightDestinationFloorControl>();
            containerRegistry.RegisterForNavigation<HandleElevatorDeviceControl>();
            containerRegistry.RegisterForNavigation<ClearRoyalMaskControl>();
            containerRegistry.RegisterForNavigation<OperationControl>();
            containerRegistry.RegisterForNavigation<DopMaskRecordListControl>();
            containerRegistry.RegisterForNavigation<DopGlobalMaskRecordDetailControl>();
            containerRegistry.RegisterForNavigation<DopSpecificMaskRecordDetailControl>();
            containerRegistry.RegisterForNavigation<EditEliMessageTypeControl>();
            containerRegistry.RegisterForNavigation<EditEliCallTypeControl>();
            containerRegistry.RegisterForNavigation<EditRcgifCallTypeControl>();

            containerRegistry.RegisterSingleton<CleanFileHelper>();

            //AutoMapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EdificeViewModel, EdificeModel>()
                   .ForMember(dest => dest.EditedTime, opt => opt.Ignore());

                cfg.CreateMap<EdificeModel, EdificeViewModel>()
                   .ForMember(dest => dest.IsFrontAllChecked, opt => opt.MapFrom(src => GetIsFrontAllChecked(src.Floors)))
                   .ForMember(dest => dest.IsRearAllChecked, opt => opt.MapFrom(src => GetIsRearAllChecked(src.Floors)));

                cfg.CreateMap<FloorViewModel, FloorModel>()
                   .ForMember(dest => dest.IsPublic, opt => opt.Ignore())
                   .ForMember(dest => dest.EdificeId, opt => opt.Ignore())
                   .ForMember(dest => dest.Edifice, opt => opt.Ignore())
                   .ForMember(dest => dest.EditedTime, opt => opt.Ignore());

                cfg.CreateMap<FloorModel, FloorViewModel>();

                cfg.CreateMap<FloorViewModel, DopSpecificDefaultAccessFloorMaskViewModel>()
                   .ForMember(dest => dest.Floor, opt => opt.MapFrom(src => src))
                   .ForMember(dest => dest.FloorId, opt => opt.MapFrom(src => src.Id))
                   .ForMember(dest => dest.IsDestinationFront, opt => opt.Ignore())
                   .ForMember(dest => dest.IsDestinationRear, opt => opt.Ignore());

                cfg.CreateMap<FloorViewModel, DopGlobalDefaultAccessFloorMaskViewModel>()
                   .ForMember(dest => dest.Floor, opt => opt.MapFrom(src => src))
                   .ForMember(dest => dest.FloorId, opt => opt.MapFrom(src => src.Id))
                   .ForMember(dest => dest.IsDestinationFront, opt => opt.Ignore())
                   .ForMember(dest => dest.IsDestinationRear, opt => opt.Ignore())
                   .ForMember(dest => dest.IsSourceFront, opt => opt.Ignore())
                   .ForMember(dest => dest.IsSourceRear, opt => opt.Ignore());

                cfg.CreateMap<ElevatorGroupModel, ElevatorGroupViewModel>();
                cfg.CreateMap<ElevatorGroupViewModel, ElevatorGroupModel>()
                   .ForMember(dest => dest.BrandModel, opt => opt.Ignore())
                   .ForMember(dest => dest.Version, opt => opt.Ignore())
                   .ForMember(dest => dest.Edifice, opt => opt.Ignore())
                   .ForMember(dest => dest.ElevatorGroupFloorIds, opt => opt.Ignore())
                   .ForMember(dest => dest.ElevatorServerIds, opt => opt.Ignore())
                   .ForMember(dest => dest.ElevatorServers, opt => opt.Ignore())
                   .ForMember(dest => dest.ElevatorInfoIds, opt => opt.Ignore())
                   .ForMember(dest => dest.ElevatorInfos, opt => opt.Ignore())
                   .ForMember(dest => dest.EditedTime, opt => opt.Ignore())
                   .ForMember(dest => dest.ElevatorGroupFloors, opt => opt.MapFrom(src => src.ElevatorGroupFloors));

                cfg.CreateMap<ElevatorGroupFloorModel, ElevatorGroupFloorViewModel>();
                cfg.CreateMap<ElevatorGroupFloorViewModel, ElevatorGroupFloorModel>()
                   .ForMember(dest => dest.EditedTime, opt => opt.Ignore())
                   .ForMember(dest => dest.ElevatorGroupId, opt => opt.Ignore())
                   .ForMember(dest => dest.ElevatorGroup, opt => opt.Ignore());

                cfg.CreateMap(typeof(PageData<>), typeof(PageDataViewModel<>), MemberList.Source);
                cfg.CreateMap(typeof(PageQueryDataViewModel<,>), typeof(PageQuery<>));

                cfg.CreateMap<PassRightModel, PassRightViewModel>()
                   .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Person.Name))
                   .ForMember(dest => dest.FloorNames, opt => opt.MapFrom(src => GetFloorNames(src.Floors)));

                cfg.CreateMap<HandleElevatorDeviceModel, HandleElevatorDeviceViewModel>()
                   .ForMember(dest => dest.FloorId, opt => opt.MapFrom(src => src.Floor.Id))
                   .ForMember(dest => dest.FloorName, opt => opt.MapFrom(src => src.Floor.Name))
                   .ForMember(dest => dest.PhysicsFloor, opt => opt.MapFrom(src => src.Floor.PhysicsFloor))
                   .ForMember(dest => dest.IsFront, opt => opt.MapFrom(src => true))
                   .ForMember(dest => dest.IsRear, opt => opt.MapFrom(src => true));

                cfg.CreateMap<HandleElevatorDeviceViewModel, HandleElevatorDeviceModel>()
                   .ForMember(dest => dest.DeviceId, opt => opt.Ignore())
                   .ForMember(dest => dest.ProcessorId, opt => opt.Ignore())
                   .ForMember(dest => dest.Processor, opt => opt.Ignore())
                   .ForMember(dest => dest.DeviceType, opt => opt.Ignore())
                   .ForMember(dest => dest.BrandModel, opt => opt.Ignore())
                   .ForMember(dest => dest.CommunicateType, opt => opt.Ignore())
                   .ForMember(dest => dest.IpAddress, opt => opt.Ignore())
                   .ForMember(dest => dest.Port, opt => opt.Ignore())
                   .ForMember(dest => dest.FaceAppId, opt => opt.Ignore())
                   .ForMember(dest => dest.FaceSdkKey, opt => opt.Ignore())
                   .ForMember(dest => dest.FaceActivateCode, opt => opt.Ignore())
                   .ForMember(dest => dest.Floor, opt => opt.Ignore())
                   .ForMember(dest => dest.ElevatorGroupId, opt => opt.Ignore())
                   .ForMember(dest => dest.ElevatorGroup, opt => opt.Ignore())
                   .ForMember(dest => dest.EditedTime, opt => opt.Ignore());

                cfg.CreateMap<DopGlobalDefaultAccessMaskModel, DopGlobalDefaultAccessMaskViewModel>()
                   .ForMember(desc => desc.ConnectedState, opt => opt.MapFrom(src => PublicBaseEnum.FromCode<ConnectedStateEnum>(src.ConnectedState)));
                cfg.CreateMap<DopGlobalDefaultAccessMaskViewModel, DopGlobalDefaultAccessMaskModel>()
                   .ForMember(desc => desc.ConnectedState, opt => opt.MapFrom(src => src.ConnectedState.Code))
                   .ForMember(dest => dest.ElevatorGroupId, opt => opt.MapFrom(src => src.ElevatorGroup.Id));
                cfg.CreateMap<DopGlobalDefaultAccessFloorMaskModel, DopGlobalDefaultAccessFloorMaskViewModel>();
                cfg.CreateMap<DopGlobalDefaultAccessFloorMaskViewModel, DopGlobalDefaultAccessFloorMaskModel>();

                cfg.CreateMap<DopSpecificDefaultAccessMaskModel, DopSpecificDefaultAccessMaskViewModel>()
                   .ForMember(desc => desc.ConnectedState, opt => opt.MapFrom(src => PublicBaseEnum.FromCode<ConnectedStateEnum>(src.ConnectedState)));
                cfg.CreateMap<DopSpecificDefaultAccessMaskViewModel, DopSpecificDefaultAccessMaskModel>()
                   .ForMember(desc => desc.ConnectedState, opt => opt.MapFrom(src => src.ConnectedState.Code))
                   .ForMember(dest => dest.ElevatorGroupId, opt => opt.MapFrom(src => src.ElevatorGroup.Id))
                   .ForMember(dest => dest.HandleElevatorDeviceId, opt => opt.MapFrom(src => src.HandleElevatorDevice.Id));
                cfg.CreateMap<DopSpecificDefaultAccessFloorMaskModel, DopSpecificDefaultAccessFloorMaskViewModel>();
                cfg.CreateMap<DopSpecificDefaultAccessFloorMaskViewModel, DopSpecificDefaultAccessFloorMaskModel>();

                cfg.CreateMap<KoneSystemConfigModel, KoneSystemConfigViewModel>()
                   .ForMember(dest => dest.OpenAccessForDopMessageType, opt =>
                        opt.MapFrom(src =>
                            BaseEnum.FromCode<OpenAccessForDopMessageTypeEnum>(src.OpenAccessForDopMessageType)))
                   .ForMember(dest => dest.StandardCallType, opt =>
                        opt.MapFrom(src =>
                            BaseEnum.FromCode<StandardCallTypeEnum>(src.StandardCallType)))
                   .ForMember(dest => dest.AcsBypassCallType, opt =>
                        opt.MapFrom(src =>
                            BaseEnum.FromCode<AcsBypassCallTypeEnum>(src.AcsBypassCallType)));
                cfg.CreateMap<KoneSystemConfigViewModel, KoneSystemConfigModel>()
                   .ForMember(desc => desc.OpenAccessForDopMessageType, opt => opt.MapFrom(src => src.OpenAccessForDopMessageType.Code))
                   .ForMember(desc => desc.StandardCallType, opt => opt.MapFrom(src => src.StandardCallType.Code))
                   .ForMember(desc => desc.AcsBypassCallType, opt => opt.MapFrom(src => src.AcsBypassCallType.Code));

                cfg.CreateMap<HandleElevatorDeviceAuxiliaryModel, HandleElevatorDeviceAuxiliaryViewModel>();
                cfg.CreateMap<HandleElevatorDeviceAuxiliaryViewModel, HandleElevatorDeviceAuxiliaryModel>()
                   .ForMember(dest => dest.EditedTime, opt => opt.Ignore());

                cfg.CreateMap<PassRightAccessibleFloorDetailModel, PassRightAccessibleFloorDetailViewModel>();
                cfg.CreateMap<PassRightAccessibleFloorDetailViewModel, PassRightAccessibleFloorDetailModel>()
                   .ForMember(dest => dest.PassRightAccessibleFloor, opt => opt.Ignore())
                   .ForMember(dest => dest.PassRightAccessibleFloorId, opt => opt.Ignore())
                   .ForMember(dest => dest.EditedTime, opt => opt.Ignore());

                cfg.CreateMap<PassRightAccessibleFloorModel, PassRightAccessibleFloorViewModel>()
                   .ForMember(dest => dest.ElevatorGroup, opt => opt.Ignore());
                cfg.CreateMap<PassRightAccessibleFloorViewModel, PassRightAccessibleFloorModel>()
                   .ForMember(dest => dest.EditedTime, opt => opt.Ignore());

                cfg.CreateMap<PassRightDestinationFloorModel, PassRightDestinationFloorViewModel>()
                   .ForMember(dest => dest.ElevatorGroup, opt => opt.Ignore());
                cfg.CreateMap<PassRightDestinationFloorViewModel, PassRightDestinationFloorModel>()
                   .ForMember(dest => dest.FloorId, opt => opt.MapFrom(src => src.Floor.Id))
                   .ForMember(dest => dest.EditedTime, opt => opt.Ignore());

                cfg.CreateMap<DopMaskRecordQuery, DopMaskRecordQueryViewModel>();
                cfg.CreateMap<DopMaskRecordQueryViewModel, DopMaskRecordQuery>();
                cfg.CreateMap<DopMaskRecordModel, DopMaskRecordViewModel>();


                cfg.CreateMap<EliOpenAccessForDopMessageTypeModel, EliOpenAccessForDopMessageTypeViewModel>()
                   .ForMember(dest => dest.MessageType, opt =>
                        opt.MapFrom(src =>
                            BaseEnum.FromCode<OpenAccessForDopMessageTypeEnum>(src.MessageType)));
                cfg.CreateMap<EliOpenAccessForDopMessageTypeViewModel, EliOpenAccessForDopMessageTypeModel>()
                   .ForMember(desc => desc.MessageType, opt => opt.MapFrom(src => src.MessageType.Code));

                cfg.CreateMap<EliPassRightHandleElevatorDeviceCallTypeModel, EliPassRightHandleElevatorDeviceCallTypeViewModel>()
                   .ForMember(dest => dest.CallType, opt =>
                        opt.MapFrom(src =>
                            BaseEnum.FromCode<StandardCallTypeEnum>(src.CallType)));
                cfg.CreateMap<EliPassRightHandleElevatorDeviceCallTypeViewModel, EliPassRightHandleElevatorDeviceCallTypeModel>()
                   .ForMember(desc => desc.CallType, opt => opt.MapFrom(src => src.CallType.Code));

                cfg.CreateMap<RcgifPassRightHandleElevatorDeviceCallTypeModel, RcgifPassRightHandleElevatorDeviceCallTypeViewModel>()
                   .ForMember(dest => dest.CallType, opt =>
                        opt.MapFrom(src =>
                            BaseEnum.FromCode<AcsBypassCallTypeEnum>(src.CallType)));
                cfg.CreateMap<RcgifPassRightHandleElevatorDeviceCallTypeViewModel, RcgifPassRightHandleElevatorDeviceCallTypeModel>()
                   .ForMember(desc => desc.CallType, opt => opt.MapFrom(src => src.CallType.Code));


            });

            // only during development, validate your mappings; remove it before release
            configuration.AssertConfigurationIsValid();
            // use DI (http://docs.automapper.org/en/latest/Dependency-injection.html) or create the mapper yourself
            var mapper = configuration.CreateMapper();
            containerRegistry.RegisterInstance(mapper);

            RegistedAsync();

            InitDataAsync();
        }

        private string GetFloorNames(List<FloorModel> floors)
        {
            var names = floors?.Select(x => x.Name).ToList().ToCommaString();
            return names;
        }

        private bool GetIsFrontAllChecked(List<FloorModel> floors)
        {
            return !floors.Any(x => !x.IsFront);
        }

        private bool GetIsRearAllChecked(List<FloorModel> floors)
        {
            return !floors.Any(x => !x.IsRear);
        }
    }
}
