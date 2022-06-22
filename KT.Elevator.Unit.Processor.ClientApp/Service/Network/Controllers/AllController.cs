using KT.Common.WpfApp.Helpers;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using KT.Elevator.Unit.Processor.ClientApp.Events;
using KT.Elevator.Unit.Processor.ClientApp.Service.Helpers;
using KT.Elevator.Unit.Processor.ClientApp.Service.IServices;
using KT.Elevator.Unit.Processor.ClientApp.Service.Network.Helpers;
using KT.Quanta.Common.Models;
using Microsoft.Extensions.Logging;
using Prism.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Elevator.Unit.Processor.ClientApp.Service.Network.Controllers
{
    /// <summary>
    /// 所有操作
    /// </summary>
    [NettyHttpModule(ModuleEnum.AllValue)]
    public class AllController
    {
        private ConfigHelper _configHelper;
        private ILogger _logger;
        private AppSettings _appSettings;
        private IEventAggregator _eventAggregator;

        /// <summary>
        /// 构造函数注入对象
        /// </summary> 
        public AllController()
        {
            _configHelper = ContainerHelper.Resolve<ConfigHelper>();
            _logger = ContainerHelper.Resolve<ILogger>();
            _appSettings = ContainerHelper.Resolve<AppSettings>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();
        }

        /// <summary>
        /// 新增或修改权限
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [NettyHttpAction(AllActionEnum.AddOrEditPassRightsValue)]
        public async Task AddOrEditPassRightsAsync(List<UnitPassRightEntity> datas)
        {
            var service = ContainerHelper.Resolve<IPassRightService>();
            datas = await service.AddOrUpdateAsync(datas);
            _eventAggregator.GetEvent<AddOrEditPassRightsEvent>().Publish(datas);
        }

        /// <summary>
        /// 新增或修改权限
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NettyHttpAction(AllActionEnum.AddOrEditPassRightValue)]
        public async Task AddOrEditPassRightAsync(UnitPassRightEntity data)
        {
            var service = ContainerHelper.Resolve<IPassRightService>();
            data = await service.AddOrUpdateAsync(data);

            _eventAggregator.GetEvent<AddOrEditPassRightsEvent>().Publish(new List<UnitPassRightEntity>() { data });
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NettyHttpAction(AllActionEnum.DeletePassRightValue)]
        public async Task DeletePassRightAsync(DeleteNormalModel data)
        {
            var service = ContainerHelper.Resolve<IPassRightService>();
            await service.Delete(data.Id, data.EditTime);

            _eventAggregator.GetEvent<DeletePassRightEvent>().Publish(data.Id);
        }

        /// <summary>
        /// 新增或修改读卡器
        /// </summary>
        /// <param name="datas"></param>
        /// <returns></returns>
        [NettyHttpAction(AllActionEnum.AddOrEditCardDevicesValue)]
        public async Task AddOrEditCardDevicesAsync(List<UnitCardDeviceEntity> datas)
        {
            var service = ContainerHelper.Resolve<ICardDeviceService>();
            await service.AddOrUpdateAsync(datas);
        }

        /// <summary>
        /// 新增或修改读卡器
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NettyHttpAction(AllActionEnum.AddOrEditCardDeviceValue)]
        public async Task AddOrEditCardDeviceAsync(UnitCardDeviceEntity data)
        {
            var service = ContainerHelper.Resolve<ICardDeviceService>();
            await service.AddOrUpdateAsync(data);
        }

        /// <summary>
        /// 删除读卡器
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NettyHttpAction(AllActionEnum.DeleteCardDeviceValue)]
        public async Task DeleteCardDeviceAsync(DeleteNormalModel data)
        {
            var service = ContainerHelper.Resolve<ICardDeviceService>();
            await service.DeleteAsync(data.Id, data.EditTime);
        }

        /// <summary>
        /// 派梯结果返回
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NettyHttpAction(AllActionEnum.HandleElevatorSuccessValue)]
        public Task HandleElevatorSuccessAsync(HandleElevatorDisplayModel data)
        {
            _eventAggregator.GetEvent<HandledElevatorDisplayEvent>().Publish(data);

            return Task.CompletedTask;
        }

        /// <summary>
        /// 新增或修改派梯设备
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NettyHttpAction(AllActionEnum.AddOrEditHandleElevatorDeviceValue)]
        public async Task AddOrEditHandleElevatorDeviceAsync(UnitHandleElevatorDeviceModel model)
        {
            //配置本地文件 
            var systemConfigService = ContainerHelper.Resolve<ISystemConfigService>();
            _configHelper.LocalConfig = await systemConfigService.AddOrEditFromDeviceAsync(model, _configHelper.LocalConfig);

            // 新增电梯组
            var elevatorGroupService = ContainerHelper.Resolve<IElevatorGroupService>();
            await elevatorGroupService.AddOrEditFromDeviceAsync(model);

            // 新增或修改派梯设备
            var handleElevatorDeviceService = ContainerHelper.Resolve<IHandleElevatorDeviceService>();
            await handleElevatorDeviceService.AddOrEditAsync(model);
        }

        /// <summary>
        /// 删除派梯设备,目前时间不起作用
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NettyHttpAction(AllActionEnum.DeleteHandleElevatorDeviceValue)]
        public async Task DeleteHandleElevatorDeviceAsync(DeleteNormalModel data)
        {
            var service = ContainerHelper.Resolve<IHandleElevatorDeviceService>();
            await service.DeleteAsync(data.Id);
        }

        /// <summary>
        /// 派梯
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [NettyHttpAction(AllActionEnum.RightHandleElevatorValue)]
        public Task RightHandleElevatorAsync(UintRightHandleElevatorRequestModel data)
        {
            var handleModel = new UnitHandleElevatorModel();
            handleModel.AccessType = data.AccessType;
            handleModel.DeviceId = data.DeviceId;
            handleModel.DeviceType = data.DeviceType;

            handleModel.HandleElevatorRight = new UnitHandleElevatorRightModel();
            handleModel.HandleElevatorRight.PassRightSign = data.Sign;

            _eventAggregator.GetEvent<CardInputedEvent>().Publish(handleModel);

            return Task.CompletedTask;
        }
    }
}
