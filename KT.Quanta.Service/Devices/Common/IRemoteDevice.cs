using KT.Quanta.Service.Devices.Quanta;
using KT.Quanta.Service.Devices.Schindler;
using KT.Quanta.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Common
{
    /// <summary>
    /// 远程设备
    /// </summary>
    public interface IRemoteDevice
    {
        /// <summary>
        /// 远程设备信息
        /// </summary>
        RemoteDeviceModel RemoteDeviceInfo { get; }

        /// <summary>
        /// 登录客户端
        /// </summary>
        /// <returns></returns>
        List<ICommunicateDevice> CommunicateDevices { get; set; }

        /// <summary>
        /// 电梯数据
        /// </summary>
        IElevatorDataRemoteService ElevatorDataRemoteService { get; set; }

        /// <summary>
        /// 电梯记录服务
        /// </summary>
        SchindlerElevatorRecordRemoteService ElevatorRecordRemoteService { get; set; }

        /// <summary>
        /// 闸机数据
        /// </summary>
        ITurnstileDataRemoteService TurnstileDataRemoteService { get; set; }

        /// <summary>
        /// 派梯
        /// </summary>
        IHandleElevatorRemoteService HandleElevatorRemoteService { get; set; }

        /// <summary>
        /// 电梯显示
        /// </summary>
        IElevatorDisplayRemoteService ElevatorDisplayRemoteService { get; set; }

        /// <summary>
        /// 闸机显示
        /// </summary>
        ITurnstileDisplayRemoteService TurnstileDisplayRemoteService { get; set; }

        /// <summary>
        /// 电梯选层器
        /// </summary>
        IElevatorSelectorRemoteService ElevatorSelectorRemoteService { get; set; }

        /// <summary>
        /// 闸机操作
        /// </summary>
        ITurnstileOperateRemoteService TurnstileOperateRemoteService { get; set; }

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="remoteDeviceInfo">远程设备信息</param>
        Task InitAsync(RemoteDeviceModel remoteDeviceInfo);
    }
}
