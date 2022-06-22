using KT.Elevator.Manage.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace KT.Elevator.Manage.Service.Devices.Kone
{
    /// <summary>
    /// 派梯设备
    /// </summary>
    public class KoneHandleDevice
    {
        public HandleElevatorDeviceModel HandleElevatorDevice { get; private set; }

        private KoneServerGroup _serverGroup;

        private ILogger<KoneHandleDevice> _logger;
        private IServiceProvider _serviceProvider;

        public KoneHandleDevice(ILogger<KoneHandleDevice> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public void Init(HandleElevatorDeviceModel handleElevatorDevice)
        {
            HandleElevatorDevice = handleElevatorDevice;

            InitServerGroup();
        }

        private void InitServerGroup()
        {
            _serverGroup = _serviceProvider.GetRequiredService<KoneServerGroup>();
            _serverGroup.Init(HandleElevatorDevice.ElevatorGroup);
        }

        /// <summary>
        /// 目地层派梯
        /// </summary>
        /// <param name="sourceFloorId">来源楼层</param>
        /// <param name="destinationFloorId">目标楼层</param>
        /// <param name="terminalId">终端</param>
        internal void Handle(string sourceFloorId, string destinationFloorId, ushort terminalId)
        {
            int TerminalId = terminalId;
             
            int DestCallType = 0;
            int SourceSide = 255;
            int SourceFloor = Convert.ToInt32(sourceFloorId);
            int DestinationSide = 255;

            int DestinationFloor = Convert.ToInt32(destinationFloorId);

            _serverGroup.CallPaddle(TerminalId, DestCallType, SourceSide, SourceFloor, DestinationFloor, DestinationSide);
        }


    }
}
