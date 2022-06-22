using KT.Quanta.Service.Hubs;
using KT.Quanta.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using KT.Elevator.Unit.Entity.Models;
using KT.Quanta.Common.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using KT.Quanta.Service.Elevator.Dtos;
using System;
using KT.Quanta.Service.Devices.Common;

namespace KT.Quanta.Service.IServices
{
    /// <summary>
    /// 派梯设备信息
    /// </summary>
    public interface IHandleElevatorDeviceService
    {
        /// <summary>
        /// 检查对象是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(string id);

        /// <summary>
        /// 获取所有派梯设备
        /// </summary>
        /// <returns>派梯设备列表</returns>
        Task<List<HandleElevatorDeviceModel>> GetAllAsync();

        /// <summary>
        /// 根据Id获取派梯设备信息
        /// </summary>
        /// <param name="id">派梯设备Id</param>
        /// <returns>派梯设备信息详情</returns>
        Task<HandleElevatorDeviceModel> GetByIdAsync(string id);

        /// <summary>
        /// 修改派梯设备
        /// </summary>
        /// <param name="model">派梯设备详情</param>
        /// <returns>派梯设备详情</returns>
        Task<HandleElevatorDeviceModel> AddOrEditAsync(HandleElevatorDeviceModel model);

        /// <summary>
        /// 物理删除派梯设备
        /// </summary>
        /// <param name="id">派梯设备Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);

        /// <summary>
        /// 目地层派梯
        /// </summary>
        /// <param name="floorId">上地楼层Id</param>
        /// <param name="handleElevatorDeviceId">派梯设备Id</param> 
        Task HandleElevator(UnitManualHandleElevatorRequestModel handleElevator);
        Task<List<HandleElevatorDeviceModel>> GetByElevatorGroupIdAsync(string elevatorGroupId);

        /// <summary>
        /// 目地层派梯
        /// </summary>
        /// <param name="floorId">上地楼层Id</param>
        /// <param name="handleElevatorDeviceId">派梯设备Id</param> 
        Task SecondaryHandleElevator(UnitManualHandleElevatorRequestModel handleElevator
            , DistributeHandleElevatorModel Dbmodel);

        /// <summary>
        /// 开机加载数据
        /// </summary>
        Task InitLoadAsync();

        /// <summary>
        /// 查询派梯设备，包含楼层
        /// </summary>
        /// <param name="deviceId">设备Id</param>
        /// <returns>派梯设备详情</returns>
        Task<UnitHandleElevatorDeviceModel> GetUnitByDeviceId(string deviceId);

        /// <summary>
        /// 根据权限派梯
        /// </summary>
        /// <param name="rightHandleElevator"></param> 
        Task<HandleElevatorDisplayModel> RightHandleElevator(UintRightHandleElevatorRequestModel rightHandleElevator, string messageKey,string PersonId="");


        Task<HandleElevatorDisplayModel>  ManualHandleElevator(UnitManualHandleElevatorRequestModel handleElevator, string PersonId = "");
    }
}
