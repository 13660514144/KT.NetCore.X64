using ContralServer.CfgFileRead;
using KT.Quanta.Model.Elevator.Dtos;
using KT.Quanta.Service.Devices.Hikvision;
using KT.Quanta.Service.IServices;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Services;
using KT.Quanta.Service.Turnstile.Dtos;
using KT.Quanta.Service.Turnstile.IServices;
using KT.Quanta.Service.Turnstile.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Common.Helper
{
    public class ApiSendServer
    {
       
        private IServiceProvider _serviceProvider;
        private int WaitSleep;

        private ICardDeviceService _cardDeviceService;
        private IEdificeService _ediftService;
        private IElevatorGroupService _evatorGroupService;
        private IElevatorInfoService _elevatorInfoService;
        private IElevatorServerService _ElevatorService;
        private IFloorService _FloorService;
        private IHandleElevatorDeviceAuxiliaryService _Auxiliaryservice;
        private IHandleElevatorDeviceService _ElevatorDeviceService;
        private IHikvisionResponseHandler _hikvisionResponseHandler;
        private IPassRightAccessibleFloorService _PassRightAccessservice;
        private IPassRightService _passRightService;
        private IPassRightDestinationFloorService _PassRightDestservice;
        private IPersonService _personService;
        private IProcessorService _processorService;
        private IProcessorFloorService _ProcessorFloorService;
        private ISerialConfigService _serialConfigService;
        private ITurnstileCardDeviceService _TurnstileCardDeviceservice;
        private ITurnstileCardDeviceRightGroupService _TurnstileCardDeviceRightGroupService;
        private ITurnstilePassRightService _TurnstilePassRightService;
        private ITurnstilePersonService _TurnstilePersonService;
        private ITurnstileProcessorService _ProcessorService;
        private ITurnstileRelayDeviceService _RelayDeviceService;
        private ISerialConfigService _SerialConfService;
        public ApiSendServer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            var _scope = _serviceProvider.CreateScope();            
            
            WaitSleep = Convert.ToInt32(AppConfigurtaionServices.Configuration["WaitSleep"].ToString());
            //--------------
            _cardDeviceService = _scope.ServiceProvider.GetRequiredService<ICardDeviceService>();
            _ediftService = _scope.ServiceProvider.GetRequiredService<IEdificeService>();
            _evatorGroupService=_scope.ServiceProvider.GetRequiredService<IElevatorGroupService>();
            _elevatorInfoService = _scope.ServiceProvider.GetRequiredService<IElevatorInfoService>();
            _ElevatorService = _scope.ServiceProvider.GetRequiredService<IElevatorServerService>();
            _FloorService = _scope.ServiceProvider.GetRequiredService<IFloorService>();
            _Auxiliaryservice = _scope.ServiceProvider.GetRequiredService<IHandleElevatorDeviceAuxiliaryService>();
            _ElevatorDeviceService = _scope.ServiceProvider.GetRequiredService<IHandleElevatorDeviceService>();
            _hikvisionResponseHandler = _scope.ServiceProvider.GetRequiredService<IHikvisionResponseHandler>();
            _PassRightAccessservice = _scope.ServiceProvider.GetRequiredService<IPassRightAccessibleFloorService>(); ;
            _passRightService = _scope.ServiceProvider.GetRequiredService<IPassRightService>();
            _PassRightDestservice= _scope.ServiceProvider.GetRequiredService<IPassRightDestinationFloorService>();
            _personService = _scope.ServiceProvider.GetRequiredService<IPersonService>();
            _processorService = _scope.ServiceProvider.GetRequiredService<IProcessorService>();
            _ProcessorFloorService = _scope.ServiceProvider.GetRequiredService<IProcessorFloorService>();
            _serialConfigService = _scope.ServiceProvider.GetRequiredService<ISerialConfigService>();
            _TurnstileCardDeviceservice = _scope.ServiceProvider.GetRequiredService<ITurnstileCardDeviceService>();
            _TurnstileCardDeviceRightGroupService = _scope.ServiceProvider.GetRequiredService<ITurnstileCardDeviceRightGroupService>();
            _TurnstilePassRightService = _scope.ServiceProvider.GetRequiredService<ITurnstilePassRightService>();
            _TurnstilePersonService = _scope.ServiceProvider.GetRequiredService<ITurnstilePersonService>();
            _ProcessorService = _scope.ServiceProvider.GetRequiredService<ITurnstileProcessorService>();
            _RelayDeviceService = _scope.ServiceProvider.GetRequiredService<ITurnstileRelayDeviceService>();
            _SerialConfService = _scope.ServiceProvider.GetRequiredService<ISerialConfigService>();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            _Elevator_CardDeviceController = new Elevator_CardDeviceController();
            List_Elevator_CardDevice = new List<Elevator_CardDeviceController>();

            _Elevator_EdificeController = new Elevator_EdificeController();
            List_Elevator_Edifice = new  List<Elevator_EdificeController>();

            _Elevator_ElevatorGroupController = new Elevator_ElevatorGroupController();
            List_Elevator_ElevatorGroup = new  List<Elevator_ElevatorGroupController>();

            _Elevator_ElevatorInfoController = new Elevator_ElevatorInfoController();
            List_Elevator_ElevatorInfo = new List<Elevator_ElevatorInfoController>();

            _Elevator_ElevatorServerController = new Elevator_ElevatorServerController();
            List_Elevator_ElevatorServer = new List<Elevator_ElevatorServerController>();

            _Elevator_FloorController = new Elevator_FloorController();
            List_Elevator_Floor = new List<Elevator_FloorController>();

            _Elevator_Elevator_HandleElevatorDeviceAuxiliaryController = new Elevator_HandleElevatorDeviceAuxiliaryController();
            List_Elevator_HandleElevatorDeviceAuxiliary = new List<Elevator_HandleElevatorDeviceAuxiliaryController>();

            _Elevator_HandleElevatorDeviceController = new Elevator_HandleElevatorDeviceController();
            List_Elevator_HandleElevatorDevice = new List<Elevator_HandleElevatorDeviceController>();

            _Elevator_PassRecordController = new Elevator_PassRecordController();
            List_Elevator_PassRecord = new List<Elevator_PassRecordController>();

            _Elevator_PassRightAccessibleFloorController = new Elevator_PassRightAccessibleFloorController();
            List_Elevator_PassRightAccessibleFloor = new List<Elevator_PassRightAccessibleFloorController>();

            _Elevator_PassRightController = new Elevator_PassRightController();
            List_Elevator_PassRight = new List<Elevator_PassRightController>();

            _Elevator_PassRightDestinationFloorController = new Elevator_PassRightDestinationFloorController();
            List_Elevator_PassRightDestinationFloor = new List<Elevator_PassRightDestinationFloorController>();

            _Elevator_PersonController = new Elevator_PersonController();
            List_Elevator_Person = new List<Elevator_PersonController>();

            _ProcessorController = new Elevator_ProcessorController();
            List_Elevator_Processor = new List<Elevator_ProcessorController>();

            _ProcessorFloorController = new Elevator_ProcessorFloorController();
            List_Elevator_ProcessorFloor = new List<Elevator_ProcessorFloorController>();

            _SerialConfigController = new Elevator_SerialConfigController();
            List_Elevator_SerialConfig = new List<Elevator_SerialConfigController>();

            _TurnstileCardDevice = new _TurnstileCardDeviceController();
            List_TurnstileCardDevice = new List<_TurnstileCardDeviceController>();

            _TurnstileCardDeviceRightGroup = new _TurnstileCardDeviceRightGroupController();
            List_TurnstileCardDeviceRightGroup = new List<_TurnstileCardDeviceRightGroupController>();

            _TurnstilePassRight = new _TurnstilePassRightController();
            List_TurnstilePassRight = new List<_TurnstilePassRightController>();

            _TurnstilePerson = new _TurnstilePersonController();
            List_TurnstilePerson = new List<_TurnstilePersonController>();

            _TurnstileProcessor = new _TurnstileProcessorController();
            List__TurnstileProcessor = new List<_TurnstileProcessorController>();

            _TurnstileRelayDevice = new _TurnstileRelayDeviceController();
            List__TurnstileRelayDevice = new List<_TurnstileRelayDeviceController>();

            _TurnstileSerialConfig = new _TurnstileSerialConfigController();
            List__TurnstileSerialConfig = new List<_TurnstileSerialConfigController>();

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = WaitSleep;
            timer.Elapsed += SendToClientQueue;
            timer.Enabled = true;
            timer.Start();
        }
        /// <summary>
        /// 更新表并写入signlra 队列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async void SendToClientQueue(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (List_Elevator_CardDevice.Count > 0)
                {
                    Elevator_CardDeviceController T1 = new Elevator_CardDeviceController();
                    T1.WorkModel = List_Elevator_CardDevice[0].WorkModel;
                    T1._CardDeviceModel = List_Elevator_CardDevice[0]._CardDeviceModel;
                    List_Elevator_CardDevice.RemoveAt(0);
                    if (T1.WorkModel == "AddOrEditAsync")
                    {
                        await _cardDeviceService.AddOrEditAsync(T1._CardDeviceModel);
                    }
                    else if (T1.WorkModel == "DeleteAsync")
                    {
                        await _cardDeviceService.DeleteAsync(T1._CardDeviceModel.Id);
                    }
                }
                else if (List_Elevator_Edifice.Count > 0)
                {
                    Elevator_EdificeController T1 = new Elevator_EdificeController();
                    T1.WorkModel = List_Elevator_Edifice[0].WorkModel;
                    T1._EdificeModel = List_Elevator_Edifice[0]._EdificeModel;
                    T1._EdificeModels.AddRange(List_Elevator_Edifice[0]._EdificeModels);
                    List_Elevator_Edifice.RemoveAt(0);
                    switch (T1.WorkModel)
                    {
                        case "AddOrEditAsync":
                            await _ediftService.AddOrEditAsync(T1._EdificeModel);
                            break;
                        case "DeleteAsync":
                            await _ediftService.DeleteAsync(T1._EdificeModel.Id);
                            break;
                        case "AddOrEditWithFloorAsync":
                            await _ediftService.AddOrEditWithFloorAsync(T1._EdificeModel);
                            break;
                        case "DeleteWithFloor":
                            await _ediftService.DeleteWithFloorAsync(T1._EdificeModel.Id);
                            break;
                        case "EditDirectionsAsync":
                            await _ediftService.EditDirectionsAsync(T1._EdificeModels);
                            break;
                    }
                }
                else if (List_Elevator_ElevatorGroup.Count > 0)
                {
                    Elevator_ElevatorGroupController T1 = new Elevator_ElevatorGroupController();
                    T1.WorkModel = List_Elevator_ElevatorGroup[0].WorkModel;
                    T1._ElevatorGroupModel = List_Elevator_ElevatorGroup[0]._ElevatorGroupModel;
                    List_Elevator_ElevatorGroup.RemoveAt(0);
                    switch (T1.WorkModel)
                    {
                        case "AddOrEditAsync":
                            await _evatorGroupService.AddOrEditAsync(T1._ElevatorGroupModel);
                            break;
                        case "DeleteAsync":
                            await _evatorGroupService.DeleteAsync(T1._ElevatorGroupModel.Id);
                            break;
                    }
                }
                else if (List_Elevator_ElevatorInfo.Count > 0)
                {
                    Elevator_ElevatorInfoController T1 = new Elevator_ElevatorInfoController();
                    T1.WorkModel = List_Elevator_ElevatorInfo[0].WorkModel;
                    T1._ElevatorInfoModel = List_Elevator_ElevatorInfo[0]._ElevatorInfoModel;
                    T1._ElevatorGroupModel = List_Elevator_ElevatorInfo[0]._ElevatorGroupModel;
                    List_Elevator_ElevatorInfo.RemoveAt(0);
                    switch (T1.WorkModel)
                    {
                        case "AddOrEditAsync":
                            await _elevatorInfoService.AddOrEditAsync(T1._ElevatorInfoModel);
                            break;
                        case "AddOrEditByElevatorGroupId":
                            await _elevatorInfoService.AddOrEditByElevatorGroupId(T1._ElevatorGroupModel);
                            break;
                        case "DeleteAsync":
                            await _elevatorInfoService.DeleteAsync(T1._ElevatorInfoModel.Id);
                            break;
                        case "DeleteByElevatorGroupId":
                            await _elevatorInfoService.DeleteByElevatorGroupId(T1._ElevatorGroupModel.Id);
                            break;
                    }
                }
                else if (List_Elevator_ElevatorServer.Count > 0)
                {
                    Elevator_ElevatorServerController T1 = new Elevator_ElevatorServerController();
                    T1.WorkModel = List_Elevator_ElevatorServer[0].WorkModel;
                    T1._ElevatorServerModel = List_Elevator_ElevatorServer[0]._ElevatorServerModel;
                    List_Elevator_ElevatorServer.RemoveAt(0);
                    switch (T1.WorkModel)
                    {
                        case "AddOrEditAsync":
                            await _ElevatorService.AddOrEditAsync(T1._ElevatorServerModel);
                            break;
                        case "DeleteAsync":
                            await _ElevatorService.DeleteAsync(T1._ElevatorServerModel.Id);
                            break;
                    }
                }
                else if (List_Elevator_Floor.Count > 0)
                {
                    Elevator_FloorController T1 = new Elevator_FloorController();
                    T1.WorkModel = List_Elevator_Floor[0].WorkModel;
                    T1._FloorModel = List_Elevator_Floor[0]._FloorModel;
                    T1._FloorModels.AddRange(List_Elevator_Floor[0]._FloorModels);
                    List_Elevator_Floor.RemoveAt(0);
                    switch (T1.WorkModel)
                    {
                        case "AddOrEditAsync":
                            await _FloorService.AddOrEditAsync(T1._FloorModel);
                            break;
                        case "DeleteAsync":
                            await _FloorService.DeleteAsync(T1._FloorModel.Id);
                            break;
                        case "EditDirectionsAsync":
                            await _FloorService.EditDirectionsAsync(T1._FloorModels);
                            break;
                    }
                }
                else if (List_Elevator_HandleElevatorDeviceAuxiliary.Count > 0)
                {
                    Elevator_HandleElevatorDeviceAuxiliaryController T1 = new Elevator_HandleElevatorDeviceAuxiliaryController();
                    T1.WorkModel = List_Elevator_HandleElevatorDeviceAuxiliary[0].WorkModel;
                    T1._HandleElevatorDeviceAuxiliaryModels.AddRange(List_Elevator_HandleElevatorDeviceAuxiliary[0]._HandleElevatorDeviceAuxiliaryModels);
                    List_Elevator_HandleElevatorDeviceAuxiliary.RemoveAt(0);
                    await _Auxiliaryservice.AddOrEditsAsync(T1._HandleElevatorDeviceAuxiliaryModels);
                }
                else if (List_Elevator_HandleElevatorDevice.Count > 0)
                {
                    Elevator_HandleElevatorDeviceController T1 = new Elevator_HandleElevatorDeviceController();
                    T1.WorkModel = List_Elevator_HandleElevatorDevice[0].WorkModel;
                    T1._HandleElevatorDeviceModel = List_Elevator_HandleElevatorDevice[0]._HandleElevatorDeviceModel;
                    List_Elevator_HandleElevatorDevice.RemoveAt(0);
                    switch (T1.WorkModel)
                    {
                        case "AddOrEditAsync":
                            await _ElevatorDeviceService.AddOrEditAsync(T1._HandleElevatorDeviceModel);
                            break;
                        case "DeleteAsync":
                            await _ElevatorDeviceService.DeleteAsync(T1._HandleElevatorDeviceModel.Id);
                            break;
                    }
                }
                else if (List_Elevator_PassRecord.Count > 0)
                {
                    Elevator_PassRecordController T1 = new Elevator_PassRecordController();
                    T1.WorkModel = List_Elevator_PassRecord[0].WorkModel;
                    T1._PassRecordModel = List_Elevator_PassRecord[0]._PassRecordModel;
                    List_Elevator_PassRecord.RemoveAt(0);
                    await _hikvisionResponseHandler.UploadPassRecordAsync(T1._PassRecordModel);
                }
                else if (List_Elevator_PassRightAccessibleFloor.Count > 0)
                {
                    Elevator_PassRightAccessibleFloorController T1 = new Elevator_PassRightAccessibleFloorController();
                    T1.WorkModel = List_Elevator_PassRightAccessibleFloor[0].WorkModel;
                    T1._PassRightModel = List_Elevator_PassRightAccessibleFloor[0]._PassRightModel;
                    T1._PassRightAccessibleFloorModel = List_Elevator_PassRightAccessibleFloor[0]._PassRightAccessibleFloorModel;
                    T1._PassRightModels.AddRange(List_Elevator_PassRightAccessibleFloor[0]._PassRightModels);
                    List_Elevator_PassRightAccessibleFloor.RemoveAt(0);
                    switch (T1.WorkModel)
                    {
                        case "AddOrEditsAsync":
                            await _PassRightAccessservice.AddOrEditsAsync(T1._PassRightModels);
                            break;
                        case "DeleteBySignAsync":
                            await _PassRightAccessservice.DeleteBySignAsync(T1._PassRightModel.Sign);
                            break;
                        case "DeleteBySignAndElevatorGroupIdAsync":
                            await _PassRightAccessservice.DeleteBySignAndElevatorGroupIdAsync(T1._PassRightAccessibleFloorModel.Sign, T1._PassRightAccessibleFloorModel.ElevatorGroupId);
                            break;
                    }
                }
                else if (List_Elevator_PassRight.Count > 0)
                {
                    Elevator_PassRightController T1 = new Elevator_PassRightController();
                    T1.WorkModel = List_Elevator_PassRight[0].WorkModel;
                    T1._PassRightModel = List_Elevator_PassRight[0]._PassRightModel;
                    List_Elevator_PassRight.RemoveAt(0);
                    switch (T1.WorkModel)
                    {
                        case "AddOrEditAsync":
                            await _passRightService.AddOrEditAsync(T1._PassRightModel);
                            break;
                        case "DeleteAsync":
                            await _passRightService.DeleteAsync(T1._PassRightModel.Id);
                            break;
                        case "DeleteBySignAsync":
                            await _passRightService.DeleteBySignAsync(T1._PassRightModel.Sign);
                            break;
                    }
                }
                else if (List_Elevator_PassRightDestinationFloor.Count > 0)
                {
                    Elevator_PassRightDestinationFloorController T1 = new Elevator_PassRightDestinationFloorController();
                    T1.WorkModel = List_Elevator_PassRightDestinationFloor[0].WorkModel;
                    T1._PassRightModel = List_Elevator_PassRightDestinationFloor[0]._PassRightModel;
                    T1._PassRightDestinationFloorModel = List_Elevator_PassRightDestinationFloor[0]._PassRightDestinationFloorModel;
                    T1._PassRightDestinationFloorModels.AddRange(List_Elevator_PassRightDestinationFloor[0]._PassRightDestinationFloorModels);
                    List_Elevator_PassRightDestinationFloor.RemoveAt(0);
                    switch (T1.WorkModel)
                    {
                        case "AddOrEditsAsync":
                            await _PassRightDestservice.AddOrEditsAsync(T1._PassRightDestinationFloorModels);
                            break;
                        case "DeleteBySignAsync":
                            await _PassRightDestservice.DeleteBySignAsync(T1._PassRightModel.Sign);
                            break;
                        case "DeleteBySignAndElevatorGroupIdAsync":
                            await _PassRightDestservice.DeleteBySignAndElevatorGroupIdAsync(T1._PassRightDestinationFloorModel.Sign, T1._PassRightDestinationFloorModel.ElevatorGroupId);
                            break;
                    }
                }
                else if (List_Elevator_Person.Count > 0)
                {
                    Elevator_PersonController T1 = new Elevator_PersonController();
                    T1.WorkModel = List_Elevator_Person[0].WorkModel;
                    T1._PersonModel = List_Elevator_Person[0]._PersonModel;
                    List_Elevator_Person.RemoveAt(0);
                    switch (T1.WorkModel)
                    {
                        case "AddOrEditAsync":
                            await _personService.AddOrEditAsync(T1._PersonModel);
                            break;
                        case "DeleteAsync":
                            await _personService.DeleteAsync(T1._PersonModel.Id);
                            break;
                    }
                }
                else if (List_Elevator_Processor.Count > 0)
                {
                    Elevator_ProcessorController T1 = new Elevator_ProcessorController();
                    T1.WorkModel = List_Elevator_Processor[0].WorkModel;
                    T1._ProcessorModel = List_Elevator_Processor[0]._ProcessorModel;
                    List_Elevator_Processor.RemoveAt(0);
                    switch (T1.WorkModel)
                    {
                        case "AddOrEditAsync":
                            await _processorService.AddOrEditAsync(T1._ProcessorModel);
                            break;
                        case "DeleteAsync":
                            await _processorService.DeleteAsync(T1._ProcessorModel.Id);
                            break;
                    }
                }
                else if (List_Elevator_ProcessorFloor.Count > 0)
                {
                    Elevator_ProcessorFloorController T1 = new Elevator_ProcessorFloorController();
                    T1.WorkModel = List_Elevator_ProcessorFloor[0].WorkModel;
                    T1._ProcessorModel = List_Elevator_ProcessorFloor[0]._ProcessorModel;
                    T1._ProcessorFloorModel = List_Elevator_ProcessorFloor[0]._ProcessorFloorModel;
                    List_Elevator_ProcessorFloor.RemoveAt(0);
                    switch (T1.WorkModel)
                    {
                        case "AddOrEditAsyncFloor":
                            await _ProcessorFloorService.AddOrEditAsync(T1._ProcessorFloorModel);
                            break;
                        case "AddOrEditAsync":
                            await _ProcessorFloorService.AddOrEditByProcessorIdAsync(T1._ProcessorModel);
                            break;
                        case "DeleteAsync":
                            await _ProcessorFloorService.DeleteAsync(T1._ProcessorFloorModel.Id);
                            break;
                        case "DeleteByProcessorIdAsync":
                            await _ProcessorFloorService.DeleteByProcessorIdAsync(T1._ProcessorModel.Id);
                            break;
                    }
                }
                else if (List_Elevator_SerialConfig.Count > 0)
                {
                    Elevator_SerialConfigController T1 = new Elevator_SerialConfigController();
                    T1.WorkModel = List_Elevator_SerialConfig[0].WorkModel;
                    T1._SerialConfigModel = List_Elevator_SerialConfig[0]._SerialConfigModel;
                    List_Elevator_SerialConfig.RemoveAt(0);
                    switch (T1.WorkModel)
                    {
                        case "AddOrEditAsync":
                            await _serialConfigService.AddOrEditAsync(T1._SerialConfigModel);
                            break;
                        case "DeleteAsync":
                            await _serialConfigService.DeleteAsync(T1._SerialConfigModel.Id);
                            break;
                    }
                }
                else if (List_TurnstileCardDevice.Count > 0)
                {
                    _TurnstileCardDeviceController T1 = new _TurnstileCardDeviceController();
                    T1.WorkModel = List_TurnstileCardDevice[0].WorkModel;
                    T1._TurnstileCardDeviceModel = List_TurnstileCardDevice[0]._TurnstileCardDeviceModel;
                    List_TurnstileCardDevice.RemoveAt(0);
                    if (T1.WorkModel == "AddOrEditAsync" || T1.WorkModel == "EditAsync" || T1.WorkModel == "AddAsync")
                    {
                        await _TurnstileCardDeviceservice.AddOrEditAsync(T1._TurnstileCardDeviceModel);
                    }
                    else if (T1.WorkModel == "DeleteAsync")
                    {
                        await _TurnstileCardDeviceservice.DeleteAsync(T1._TurnstileCardDeviceModel.Id);
                    }
                }
                else if (List_TurnstileCardDeviceRightGroup.Count > 0)
                {
                    _TurnstileCardDeviceRightGroupController T1 = new _TurnstileCardDeviceRightGroupController();
                    T1.WorkModel = List_TurnstileCardDeviceRightGroup[0].WorkModel;
                    T1._TurnstileCardDeviceRightGroupModel = List_TurnstileCardDeviceRightGroup[0]._TurnstileCardDeviceRightGroupModel;
                    List_TurnstileCardDeviceRightGroup.RemoveAt(0);
                    if (T1.WorkModel == "AddOrEditAsync" || T1.WorkModel == "EditAsync" || T1.WorkModel == "AddAsync")
                    {
                        await _TurnstileCardDeviceRightGroupService.AddOrEditAsync(T1._TurnstileCardDeviceRightGroupModel);
                    }
                    else if (T1.WorkModel == "DeleteAsync")
                    {
                        await _TurnstileCardDeviceRightGroupService.DeleteAsync(T1._TurnstileCardDeviceRightGroupModel.Id);
                    }
                }
                else if (List_TurnstilePassRight.Count > 0)
                {
                    _TurnstilePassRightController T1 = new _TurnstilePassRightController();
                    T1.WorkModel = List_TurnstilePassRight[0].WorkModel;
                    T1._TurnstilePassRightModel = List_TurnstilePassRight[0]._TurnstilePassRightModel;
                    List_TurnstilePassRight.RemoveAt(0);
                    if (T1.WorkModel == "AddAsync" || T1.WorkModel == "EditAsync" || T1.WorkModel == "AddOrEditAsync")
                    {
                        await _TurnstilePassRightService.AddOrEditAsync(T1._TurnstilePassRightModel);
                    }
                    else if (T1.WorkModel == "DeleteAsync")
                    {
                        await _TurnstilePassRightService.DeleteAsync(T1._TurnstilePassRightModel);
                    }
                    else if (T1.WorkModel == "DeleteBySignAsync")
                    {
                        await _TurnstilePassRightService.DeleteBySignAsync(T1._TurnstilePassRightModel);
                    }
                }
                else if (List_TurnstilePerson.Count > 0)
                {
                    _TurnstilePersonController T1 = new _TurnstilePersonController();
                    T1.WorkModel = List_TurnstilePerson[0].WorkModel;
                    T1._TurnstilePersonModel = List_TurnstilePerson[0]._TurnstilePersonModel;
                    List_TurnstilePerson.RemoveAt(0);
                    if (T1.WorkModel == "AddOrEditAsync")
                    {
                        await _TurnstilePersonService.AddOrEditAsync(T1._TurnstilePersonModel);
                    }
                    else if (T1.WorkModel == "DeleteAsync")
                    {
                        await _TurnstilePersonService.DeleteAsync(T1._TurnstilePersonModel.Id);
                    }
                }
                else if (List__TurnstileProcessor.Count > 0)
                {
                    _TurnstileProcessorController T1 = new _TurnstileProcessorController();
                    T1.WorkModel = List__TurnstileProcessor[0].WorkModel;
                    T1._TurnstileProcessorModel = List__TurnstileProcessor[0]._TurnstileProcessorModel;
                    List__TurnstileProcessor.RemoveAt(0);
                    if (T1.WorkModel == "AddOrEditAsync" || T1.WorkModel == "EditAsync" || T1.WorkModel == "AddAsync")
                    {
                        await _ProcessorService.AddOrEditAsync(T1._TurnstileProcessorModel);
                    }
                    else if (T1.WorkModel == "DeleteAsync")
                    {
                        await _ProcessorService.DeleteAsync(T1._TurnstileProcessorModel.Id);
                    }
                }
                else if (List__TurnstileRelayDevice.Count > 0)
                {
                    _TurnstileRelayDeviceController T1 = new _TurnstileRelayDeviceController();
                    T1.WorkModel = List__TurnstileRelayDevice[0].WorkModel;
                    T1._TurnstileRelayDeviceModel = List__TurnstileRelayDevice[0]._TurnstileRelayDeviceModel;
                    List__TurnstileRelayDevice.RemoveAt(0);
                    if (T1.WorkModel == "AddOrEditAsync" || T1.WorkModel == "EditAsync" || T1.WorkModel == "AddAsync")
                    {
                        await _RelayDeviceService.AddOrEditAsync(T1._TurnstileRelayDeviceModel);
                    }
                    else if (T1.WorkModel == "DeleteAsync")
                    {
                        await _RelayDeviceService.DeleteAsync(T1._TurnstileRelayDeviceModel.Id);
                    }
                }
                else if (List__TurnstileSerialConfig.Count > 0)
                {
                    _TurnstileSerialConfigController T1 = new _TurnstileSerialConfigController();
                    T1.WorkModel = List__TurnstileSerialConfig[0].WorkModel;
                    T1._SerialConfigModel = List__TurnstileSerialConfig[0]._SerialConfigModel;
                    List__TurnstileSerialConfig.RemoveAt(0);
                    if (T1.WorkModel == "AddAsync" || T1.WorkModel == "EditAsync" || T1.WorkModel == "AddOrEditAsync")
                    {
                        await _SerialConfService.AddOrEditAsync(T1._SerialConfigModel);
                    }
                    else if (T1.WorkModel == "DeleteAsync")
                    {
                        await _SerialConfService.DeleteAsync(T1._SerialConfigModel.Id);
                    }
                }
            }
            catch (Exception ex)
            { 
            }
        }
        //成员
        /// <summary>
        /// 电梯卡类型配置
        /// </summary>
        public class Elevator_CardDeviceController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public CardDeviceModel _CardDeviceModel { get; set; } = new CardDeviceModel();
        }
        public Elevator_CardDeviceController _Elevator_CardDeviceController;
        public List<Elevator_CardDeviceController> List_Elevator_CardDevice;

        /// <summary>
        /// 大厦配置
        /// </summary>
        public class Elevator_EdificeController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public EdificeModel _EdificeModel { get; set; } = new EdificeModel();
            public List<EdificeModel> _EdificeModels { get; set; } = new List<EdificeModel>();
        }
        public Elevator_EdificeController _Elevator_EdificeController;
        public List<Elevator_EdificeController> List_Elevator_Edifice;

        /// <summary>
        /// 电梯组配置
        /// </summary>
        public class Elevator_ElevatorGroupController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public ElevatorGroupModel _ElevatorGroupModel { get; set; } = new ElevatorGroupModel();
        }
        public Elevator_ElevatorGroupController _Elevator_ElevatorGroupController;
        public List<Elevator_ElevatorGroupController> List_Elevator_ElevatorGroup;

        /// <summary>
        /// 电梯信息配置
        /// </summary>
        public class Elevator_ElevatorInfoController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public ElevatorInfoModel _ElevatorInfoModel { get; set; } = new ElevatorInfoModel();
            public ElevatorGroupModel _ElevatorGroupModel { get; set; } = new ElevatorGroupModel();
        }
        public Elevator_ElevatorInfoController _Elevator_ElevatorInfoController;
        public List<Elevator_ElevatorInfoController> List_Elevator_ElevatorInfo;

        /// <summary>
        /// 电梯服务配置
        /// </summary>
        public class Elevator_ElevatorServerController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public ElevatorServerModel _ElevatorServerModel { get; set; } = new ElevatorServerModel();
        }
        public Elevator_ElevatorServerController _Elevator_ElevatorServerController;
        public List<Elevator_ElevatorServerController> List_Elevator_ElevatorServer;

        /// <summary>
        /// 楼层信息务配置
        /// </summary>
        public class Elevator_FloorController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public FloorModel _FloorModel { get; set; } = new FloorModel();
            public List<FloorModel> _FloorModels { get; set; } = new List<FloorModel>();
        }
        public Elevator_FloorController _Elevator_FloorController;
        public List<Elevator_FloorController> List_Elevator_Floor;

        /// <summary>
        /// 通行权限
        /// </summary>
        public class Elevator_HandleElevatorDeviceAuxiliaryController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public HandleElevatorDeviceAuxiliaryModel _HandleElevatorDeviceAuxiliaryModel { get; set; } = new HandleElevatorDeviceAuxiliaryModel();
            public List<HandleElevatorDeviceAuxiliaryModel> _HandleElevatorDeviceAuxiliaryModels { get; set; } = new List<HandleElevatorDeviceAuxiliaryModel>();
        }
        public Elevator_HandleElevatorDeviceAuxiliaryController _Elevator_Elevator_HandleElevatorDeviceAuxiliaryController;
        public List<Elevator_HandleElevatorDeviceAuxiliaryController> List_Elevator_HandleElevatorDeviceAuxiliary;

        /// <summary>
        /// 派梯设备
        /// </summary>
        public class Elevator_HandleElevatorDeviceController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public HandleElevatorDeviceModel _HandleElevatorDeviceModel { get; set; } = new HandleElevatorDeviceModel();

        }
        public Elevator_HandleElevatorDeviceController _Elevator_HandleElevatorDeviceController;
        public List<Elevator_HandleElevatorDeviceController> List_Elevator_HandleElevatorDevice;

        /// <summary>
        /// 上传数据
        /// </summary>
        public class Elevator_PassRecordController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public PassRecordModel _PassRecordModel { get; set; } = new PassRecordModel();

        }
        public Elevator_PassRecordController _Elevator_PassRecordController;
        public List<Elevator_PassRecordController> List_Elevator_PassRecord;

        /// <summary>
        /// 通行权限
        /// </summary>
        public class Elevator_PassRightAccessibleFloorController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public PassRightModel _PassRightModel { get; set; } = new PassRightModel();
            public PassRightAccessibleFloorModel _PassRightAccessibleFloorModel { get; set; } = new PassRightAccessibleFloorModel();
            public List<PassRightAccessibleFloorModel> _PassRightModels { get; set; } = new List<PassRightAccessibleFloorModel>();
        }
        public Elevator_PassRightAccessibleFloorController _Elevator_PassRightAccessibleFloorController;
        public List<Elevator_PassRightAccessibleFloorController> List_Elevator_PassRightAccessibleFloor;

        /// <summary>
        /// 通行权限
        /// </summary>
        public class Elevator_PassRightController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public PassRightModel _PassRightModel { get; set; } = new PassRightModel();
        }
        public Elevator_PassRightController _Elevator_PassRightController;
        public List<Elevator_PassRightController> List_Elevator_PassRight;

        /// <summary>
        /// 通行权限
        /// </summary>
        public class Elevator_PassRightDestinationFloorController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public PassRightDestinationFloorModel _PassRightDestinationFloorModel { get; set; } = new PassRightDestinationFloorModel();
            public List<PassRightDestinationFloorModel> _PassRightDestinationFloorModels { get; set; } = new List<PassRightDestinationFloorModel>();
            public PassRightModel _PassRightModel { get; set; } = new PassRightModel();
        }
        public Elevator_PassRightDestinationFloorController _Elevator_PassRightDestinationFloorController;
        public List<Elevator_PassRightDestinationFloorController> List_Elevator_PassRightDestinationFloor;

        /// <summary>
        /// 人员
        /// </summary>
        public class Elevator_PersonController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public PersonModel _PersonModel { get; set; } = new PersonModel();
        }
        public Elevator_PersonController _Elevator_PersonController;
        public List<Elevator_PersonController> List_Elevator_Person;

        /// <summary>
        /// 边缘处理器
        /// </summary>
        public class Elevator_ProcessorController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public ProcessorModel _ProcessorModel { get; set; } = new ProcessorModel();
            public List<ProcessorModel> _ProcessorModels { get; set; } = new List<ProcessorModel>();
        }
        public Elevator_ProcessorController _ProcessorController;
        public List<Elevator_ProcessorController> List_Elevator_Processor;

        /// <summary>
        /// 边缘处理器
        /// </summary>
        public class Elevator_ProcessorFloorController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public ProcessorFloorModel _ProcessorFloorModel { get; set; } = new ProcessorFloorModel();
            public ProcessorModel _ProcessorModel { get; set; } = new ProcessorModel();
        }
        public Elevator_ProcessorFloorController _ProcessorFloorController;
        public List<Elevator_ProcessorFloorController> List_Elevator_ProcessorFloor;

        /// <summary>
        /// 边缘处理器配置
        /// </summary>
        public class Elevator_SerialConfigController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public SerialConfigModel _SerialConfigModel { get; set; } = new SerialConfigModel();
        }
        public Elevator_SerialConfigController _SerialConfigController;
        public List<Elevator_SerialConfigController> List_Elevator_SerialConfig;

        /// <summary>
        /// 边缘处理器-读卡器配置配置
        /// </summary>
        public class _TurnstileCardDeviceController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public TurnstileCardDeviceModel _TurnstileCardDeviceModel { get; set; } = new TurnstileCardDeviceModel();
        }
        public _TurnstileCardDeviceController _TurnstileCardDevice;
        public List<_TurnstileCardDeviceController> List_TurnstileCardDevice;

        /// <summary>
        /// 读卡器设备
        /// </summary>
        public class _TurnstileCardDeviceRightGroupController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public TurnstileCardDeviceRightGroupModel _TurnstileCardDeviceRightGroupModel { get; set; } = new TurnstileCardDeviceRightGroupModel();
        }
        public _TurnstileCardDeviceRightGroupController _TurnstileCardDeviceRightGroup;
        public List<_TurnstileCardDeviceRightGroupController> List_TurnstileCardDeviceRightGroup;

        /// <summary>
        /// 通行权限
        /// </summary>
        public class _TurnstilePassRightController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public TurnstilePassRightModel _TurnstilePassRightModel { get; set; } = new TurnstilePassRightModel();
        }
        public _TurnstilePassRightController _TurnstilePassRight;
        public List<_TurnstilePassRightController> List_TurnstilePassRight;

        /// <summary>
        /// 人员信息表
        /// </summary>
        public class _TurnstilePersonController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public TurnstilePersonModel _TurnstilePersonModel { get; set; } = new TurnstilePersonModel();
        }
        public _TurnstilePersonController _TurnstilePerson;
        public List<_TurnstilePersonController> List_TurnstilePerson;

        /// <summary>
        /// 边原处理器
        /// </summary>
        public class _TurnstileProcessorController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public TurnstileProcessorModel _TurnstileProcessorModel { get; set; } = new TurnstileProcessorModel();
        }
        public _TurnstileProcessorController _TurnstileProcessor;
        public List<_TurnstileProcessorController> List__TurnstileProcessor;

        /// <summary>
        /// 继电器设备
        /// </summary>
        public class _TurnstileRelayDeviceController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public TurnstileRelayDeviceModel _TurnstileRelayDeviceModel { get; set; } = new TurnstileRelayDeviceModel();
        }
        public _TurnstileRelayDeviceController _TurnstileRelayDevice;
        public List<_TurnstileRelayDeviceController> List__TurnstileRelayDevice;

        /// <summary>
        /// 串口配置
        /// </summary>
        public class _TurnstileSerialConfigController
        {
            /// <summary>
            /// 模式 add edit addoredit
            /// </summary>
            public string WorkModel { get; set; }
            public SerialConfigModel _SerialConfigModel { get; set; } = new SerialConfigModel();
        }
        public _TurnstileSerialConfigController _TurnstileSerialConfig;
        public List<_TurnstileSerialConfigController> List__TurnstileSerialConfig;
    }    
}
