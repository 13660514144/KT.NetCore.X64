using KT.Elevator.Manage.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KT.Elevator.Manage.Service.Devices.Kone
{
    /// <summary>
    /// 派梯设备列表
    /// </summary>
    public class KoneHandleDeviceList
    {
        private ConcurrentDictionary<string, KoneHandleDevice> _koneHandleDevices;
        private IServiceProvider _serviceProvider;

        public KoneHandleDeviceList(IServiceProvider serviceProvider)
        {
            _koneHandleDevices = new ConcurrentDictionary<string, KoneHandleDevice>();
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 初始化所有数据
        /// </summary>
        /// <param name="handleElevatorDevices"></param>
        public void Init(List<HandleElevatorDeviceModel> handleElevatorDevices)
        {
            _koneHandleDevices.Clear();
            using (var scope = _serviceProvider.CreateScope())
            {
                foreach (var item in handleElevatorDevices)
                {
                    var device = scope.ServiceProvider.GetRequiredService<KoneHandleDevice>();
                    device.Init(item);
                    _koneHandleDevices.AddOrUpdate(item.Id, device, UpdateValue(device));
                }
            }
        }

        public KoneHandleDevice AddOrUpdate(HandleElevatorDeviceModel handleElevatorDevice)
        {
            KoneHandleDevice device = null;
            using (var scope = _serviceProvider.CreateScope())
            {
                device = scope.ServiceProvider.GetRequiredService<KoneHandleDevice>();
                device.Init(handleElevatorDevice);
                _koneHandleDevices.AddOrUpdate(handleElevatorDevice.Id, device, UpdateValue(device));
            }

            return device;
        }

        private Func<string, KoneHandleDevice, KoneHandleDevice> UpdateValue(KoneHandleDevice data)
        {
            return (key, oldData) => data;
        }

        /// <summary>
        /// 获取派梯设备
        /// </summary>
        /// <param name="id">派梯设备Id</param>
        /// <returns></returns>
        public KoneHandleDevice GetById(string id)
        {
            return _koneHandleDevices.GetValueOrDefault(id);
        }
    }
}
