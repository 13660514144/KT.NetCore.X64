using DotNetty.Buffers;
using DotNetty.Transport.Channels;
using KT.Common.Netty.Common;
using KT.Common.Netty.Servers;
using KT.Quanta.Common.QuantaModule;
using KT.Quanta.Unit.Model.Requests;
using KT.Turnstile.Unit.ClientApp.Convetors;
using KT.Turnstile.Unit.ClientApp.Events;
using Microsoft.Extensions.Logging;
using Prism.Events;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Server
{
    public class QuantaServerHandler : QuantaServerHandlerBase, IQuantaServerHandler
    {
        private readonly IContainerProvider _containerProvider;
        private readonly IEventAggregator _eventAggregator;

        public QuantaServerHandler(ILogger<QuantaServerHandler> logger,
            IContainerProvider containerProvider,
            IEventAggregator eventAggregator)
            : base(logger)
        {
            _containerProvider = containerProvider;
            _eventAggregator = eventAggregator;
        }

        public override IQuantaNettyActionManager GetActionManager()
        {
            var actionManager = _containerProvider.Resolve<IQuantaNettyActionManager>();
            actionManager.AddAsync(QuantaCommandEnum.HandleElevator.Module.Code, QuantaCommandEnum.HandleElevator.Code, new Func<IChannel, QuantaNettyHeader, Task>((channl, header) =>
            {
                var data = new HandleElevatorDisplayRequest();
                data.ReadFromBytes(header.Datas);

                var model = HandleElevatorDisplayConvertor.ToModel(data);
                _eventAggregator.GetEvent<HandledElevatorDisplayEvent>().Publish(model);

                return Task.CompletedTask;
            }));
            actionManager.AddAsync(QuantaCommandEnum.Pass.Module.Code, QuantaCommandEnum.Pass.Code, new Func<IChannel, QuantaNettyHeader, Task>((channl, header) =>
            {
                var data = new PassDisplayRequest();
                data.ReadFromBytes(header.Datas);

                var model = PassDisplayConvertor.ToModel(data);
                _eventAggregator.GetEvent<PassDisplayEvent>().Publish(model);

                return Task.CompletedTask;
            }));
            return actionManager;
        }
    }
}