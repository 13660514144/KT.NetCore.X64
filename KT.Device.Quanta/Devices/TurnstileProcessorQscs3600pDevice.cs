using AutoMapper;
using KT.Common.Core.Utils;
using KT.Common.Event;
using KT.Common.Netty.Common;
using KT.Device.Quanta.Convertors;
using KT.Device.Quanta.Events;
using KT.Device.Quanta.Models;
using KT.Quanta.Common.Models;
using KT.Quanta.Common.QuantaModule;
using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace KT.Device.Quanta.Devices
{
    public class TurnstileProcessorQscs3600pDevice : ITurnstileProcessorQscs3600pDevice
    {
        private ProcessorModel _processor;
        private string _communicatorKey;

        private readonly IEventAggregator _eventAggregator;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<TurnstileProcessorQscs3600pDevice> _logger;
        private readonly IDeviceList _deviceList;
        private readonly IMapper _mapper;

        public TurnstileProcessorQscs3600pDevice(IEventAggregator eventAggregator,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<TurnstileProcessorQscs3600pDevice> logger,
            IDeviceList deviceList,
            IMapper mapper)
        {
            _eventAggregator = eventAggregator;
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
            _deviceList = deviceList;
            _mapper = mapper;
        }

        public async Task CreateAsync(ProcessorEntity entity)
        {
            _logger.LogInformation($"创建边缘处理器Netty连接：{JsonConvert.SerializeObject(entity, JsonUtil.JsonPrintSettings)} ");

            //本地数据
            _processor = _mapper.Map<ProcessorModel>(entity);
            _communicatorKey = $"{_processor.IpAddress}:{_processor.Port + 1}";

            //通行数据
            _eventAggregator.GetEvent<PassDisplayEvent>().Subscribe(PassShowAsync);
            _eventAggregator.GetEvent<HandleElevatorDisplayEvent>().Subscribe(HandledElevatorDisplayAsync);

            //加入队列，防止销毁 
            await _deviceList.AddAsync(_processor.Id, this);

            //创建通信设备
            var communicatorEventModel = new CareateQscs3600pCommunicatorModel();
            communicatorEventModel.RemoteIp = _processor.IpAddress;
            communicatorEventModel.RemotePort = _processor.Port;
            _eventAggregator.GetEvent<CreateQscs3600pCommunicatorEvent>().Publish(communicatorEventModel);
        }

        public Task UpdateAsync(ProcessorEntity entity)
        {
            //本地数据
            _processor = _mapper.Map<ProcessorModel>(entity);
            _communicatorKey = $"{_processor.IpAddress}:{_processor.Port + 1}";

            //创建通信设备
            var communicatorEventModel = new CareateQscs3600pCommunicatorModel();
            communicatorEventModel.RemoteIp = _processor.IpAddress;
            communicatorEventModel.RemotePort = _processor.Port;
            _eventAggregator.GetEvent<CreateQscs3600pCommunicatorEvent>().Publish(communicatorEventModel);

            return Task.CompletedTask;
        }

        private void PassShowAsync(PassDisplayModel model)
        {
            if (model.DisplayDeivceId != _processor?.Id)
            {
                return;
            }

            //表头
            var header = new QuantaNettyHeader();
            header.Module = QuantaCommandEnum.Pass.Module.Code;
            header.Command = QuantaCommandEnum.Pass.Code;

            //数据
            var request = PassDisplayConvertor.ToRequest(model);
            header.Datas = request.GetBytes();

            var sendData = new QuantaSendDataModel();
            //TODO:端口加1
            sendData.CommunicatorKey = _communicatorKey;
            sendData.QuantaNettyHeader = header;

            //发送数据
            _eventAggregator.GetEvent<PassDisplaySendEvent>().Publish(sendData);
        }

        private void HandledElevatorDisplayAsync(HandleElevatorDisplayModel model)
        {
            if (model.DeviceId != _processor?.Id)
            {
                return;
            }

            //表头
            var header = new QuantaNettyHeader();
            header.Module = QuantaCommandEnum.HandleElevator.Module.Code;
            header.Command = QuantaCommandEnum.HandleElevator.Code;

            //数据
            var request = HandleElevatorDisplayConvertor.ToRequest(model);
            header.Datas = request.GetBytes();

            var sendData = new QuantaSendDataModel();
            //TODO:端口加1
            sendData.CommunicatorKey = _communicatorKey;
            sendData.QuantaNettyHeader = header;

            //发送数据
            _eventAggregator.GetEvent<HandleElevatorDisplaySendEvent>().Publish(sendData);
        }
    }
}
