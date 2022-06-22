using KT.Common.Core.Utils;
using KT.Common.Data.Models;
using KT.Common.WpfApp.Helpers;
using KT.Common.WpfApp.ViewModels;
using KT.Elevator.Unit.Entity.Models;
using KT.Elevator.Unit.Secondary.ClientApp.Events;
using KT.Elevator.Unit.Secondary.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Secondary.ClientApp.Service.IServices;
using KT.Elevator.Unit.Secondary.ClientApp.ViewModels;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using HelperTools;
using Newtonsoft.Json;
using ContralServer.CfgFileRead;
using Newtonsoft.Json.Linq;
using KT.Elevator.Unit.Secondary.ClientApp.Views.Controls;

namespace KT.Elevator.Unit.Secondary.ClientApp.Views
{
    public class DestinationPanelControlViewModel : BindableBase
    {
        private ICommand _handleElevatorCommand;
        public ICommand HandleElevatorCommand => _handleElevatorCommand ??= new DelegateCommand<string>(HandleElevatorAsync);

        private ICommand _previousPageCommand;
        public ICommand PreviousPageCommand => _previousPageCommand ??= new DelegateCommand(PreviousPageAsync);

        private ICommand _nextPageCommand;
        public ICommand NextPageCommand => _nextPageCommand ??= new DelegateCommand(NextPageAsync);

        //private ObservableCollection<FloorViewModel> _floors;
        //private PageNavViewModel<UnitFloorEntity> _pageNav;

        private UnitHandleElevatorModel _handleElevator;
        public List<FloorViewModel> _allFloors;
        private PageDataViewModel<FloorViewModel> _pageData;

        private IPassRightService _passRightService;
        private AppSettings _appSettings;
        private IFloorService _floorService;
        private ConfigHelper _configHelper;
        private HubHelper _hubHelper;
        private IEventAggregator _eventAggregator;
        private ILogger _log;        
        public DestinationPanelControlViewModel()
        {
            _passRightService = ContainerHelper.Resolve<IPassRightService>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _floorService = ContainerHelper.Resolve<IFloorService>();
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _hubHelper = ContainerHelper.Resolve<HubHelper>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
            _log = ContainerHelper.Resolve<ILogger>();
            PageData = new PageDataViewModel<FloorViewModel>();
            //_viewmodel= ContainerHelper.Resolve<MainWindow>();
        }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="sign">卡号或人脸Id</param>
        /// <param name="rightType">权限类型</param>
        public Task InitAsync(UnitHandleElevatorModel handleElevator, ObservableCollection<FloorViewModel> floors)
        {
            _handleElevator = handleElevator;
            _allFloors = floors.ToList();

            SetPageData(1);

            return Task.CompletedTask;
        }
        public Task InitAsyncOwrite(UnitHandleElevatorModel handleElevator, ObservableCollection<FloorViewModel> floors)
        {
            return Task.CompletedTask;
        }

        private void SetPageData(int page)
        {
            if (page <= 0)
            {
                PageData.Page = 1;
                throw new Exception($"已经没有上一页了！");
            }

            //楼层分页显示
            PageData.Page = page;
            PageData.Size = _appSettings.PageSize;
            PageData.Totals = _allFloors.Count;
            PageData.Pages = PageData.Totals.GetPages(PageData.Size);

            if (page > PageData.Pages)
            {
                PageData.Page = PageData.Pages;
                throw new Exception($"已经没有下一页了！");
            }

            var list = _allFloors.OrderBy(x => x.RealFloorId).OrderBy(x => x.Sort).Skip((PageData.Page - 1) * PageData.Size).Take(PageData.Size).ToList();
            PageData.List = new ObservableCollection<FloorViewModel>(list);
            PageData.InitPageDetails();
        }

        private void PreviousPageAsync()
        {
            SetPageData(PageData.Page - 1);
        }

        private void NextPageAsync()
        {
            SetPageData(PageData.Page + 1);
        }

        public async Task HandleElevatorAsync(string id)
        {
            //获取派梯楼层
            var floor = _allFloors.FirstOrDefault(x => x.Id == id);
            //_log.LogInformation($"获取派梯楼层 data=>{JsonConvert.SerializeObject(_allFloors)}==>string id={id}");
            /*if (floor?.PassRight != null)
            {
                _handleElevator.HandleElevatorRight = new UnitHandleElevatorRightModel();
                _handleElevator.HandleElevatorRight.PassRightId = floor.PassRight.Id;
                _handleElevator.HandleElevatorRight.PassRightSign = floor.Sign; //floor.PassRight.Sign;
                _handleElevator.Sign = floor.Sign; //floor.PassRight.Sign;
            }*/
            _handleElevator.HandleElevatorRight = new UnitHandleElevatorRightModel();
            _handleElevator.HandleElevatorRight.PassRightId = null;// floor.PassRight.Id;
            _handleElevator.HandleElevatorRight.PassRightSign = floor.Sign; //floor.PassRight.Sign;
            _handleElevator.Sign = floor.Sign;
            _handleElevator.AccessType = floor.AccessType;
            _handleElevator.DeviceType = floor.DeviceType;
            _handleElevator.DestinationFloorId = floor.DestinationFloorId;
            _handleElevator.DestinationFloorName = floor.Name;
            _handleElevator.HandleElevatorDeviceId = floor.HandDeviceid;
            _handleElevator.SourceFloorId = floor.Sourceid;
            _handleElevator.DeviceId= floor.HandDeviceid; 
            _log.LogInformation($"floor==>{JsonConvert.SerializeObject(floor)}");
            if (floor.DestinationFloorId.Trim() == floor.Sourceid.Trim())
            {
                _eventAggregator.GetEvent<WarnTipEvent>().Publish("你已在目的楼层");
                _eventAggregator.GetEvent<NotRightEvent>().Publish();
                return;
            }
            //_eventAggregator.GetEvent<HandledElevatorEvent>().Publish(_handleElevator);

            /*2021-06-12 test*/
            //"ElevatorType": "1",  1：日立  2：三菱  3：通力
            string ElevatorType = AppConfigurtaionServices.Configuration["ElevatorType"].ToString().Trim();
            _log.LogInformation($"可去楼层选择派梯 data=>{JsonConvert.SerializeObject(_handleElevator)}");            
                                    
            switch (ElevatorType)
            {
                case "1":
                    //日立
                    var DisElevitor = ContainerHelper.Resolve<Dispatch>();
                    /*DisElevitor.DispatchElevitor(_handleElevator.Sign, 
                        floor.RealFloorId, 
                        floor.ElevatorGroupId);*/
                    _eventAggregator.GetEvent<HandledElevatorEvent>().Publish(_handleElevator);
                    DisElevitor.FloorDispElevitor(floor, _handleElevator);
                    break;
                case "2":
                    //三菱
                    try
                    {
                        UnitManualHandleElevatorRequestModel Handele = new UnitManualHandleElevatorRequestModel();
                        Handele.Sign = floor.Sign;
                        Handele.AccessType = floor.AccessType;
                        Handele.DeviceType = floor.DeviceType;
                        Handele.DeviceId = floor.HandDeviceid;
                        Handele.HandleElevatorDeviceId = floor.HandDeviceid;
                        Handele.DestinationFloorId = floor.DestinationFloorId;
                        Handele.SourceFloorId = floor.Sourceid;
                        Handele.ElevatorGroupId = floor.ElevatorGroupId;
                        Handele.DestinationFloorIds = new List<string>();
                        Handele.DestinationFloorIds.Add(floor.DestinationFloorId);

                        /*floor 对象继承全部请求数据
                         * var handle = DisElevitor.DispatchElevitor_M(_handleElevator.Sign,
                            floor.RealFloorId, 
                            floor.ElevatorGroupId,floor.DestinationFloorId,
                            floor.Sourceid);
                        */
                        _log.LogInformation($"三菱派梯数据{JsonConvert.SerializeObject(Handele)}");
                        /* API方法
                        Task.Run(async () =>
                            {
                                var Result = await DisElevitor.ApiElevatorServer(Handele);
                            }
                        );
                        */
                        //singr方法
                        _eventAggregator.GetEvent<HandledElevatorEvent>().Publish(_handleElevator);
                        await _hubHelper.HandleElevatorAsync(_handleElevator);
                        _log.LogInformation($"API 派梯返回");
                    }
                    catch (Exception ex)
                    {
                        _log.LogInformation($"API 派梯返回{ex}");
                    }
                    break;
                case "3":
                    break;
            }                      
            //await _hubHelper.HandleElevatorAsync(handle);          
           //await _hubHelper.HandleElevatorAsync(_handleElevator);
        }

        public PageDataViewModel<FloorViewModel> PageData
        {
            get
            {
                return _pageData;
            }

            set
            {
                SetProperty(ref _pageData, value);
            }
        }
    }
}
