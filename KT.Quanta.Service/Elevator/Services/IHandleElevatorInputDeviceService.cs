using KT.Quanta.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using KT.Quanta.Service.Devices.Common;

namespace KT.Quanta.Service.IServices
{
    /// <summary>
    /// 读卡器设备信息
    /// </summary>
    public interface IHandleElevatorInputDeviceService
    {
        /// <summary>
        /// 检查对象是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> IsExistsAsync(string id);

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备列表</returns>
        Task<List<HandleElevatorInputDeviceModel>> GetAllAsync();

        /// <summary>
        /// 获取所有读卡器设备
        /// </summary>
        /// <returns>读卡器设备列表</returns>
        Task<List<HandleElevatorInputDeviceModel>> GetStaticAsync(string cardType, string handleElevatorDeviceId);

        /// <summary>
        /// 根据Id获取读卡器设备信息
        /// </summary>
        /// <param name="id">读卡器设备Id</param>
        /// <returns>读卡器设备信息详情</returns>
        Task<HandleElevatorInputDeviceModel> GetByIdAsync(string id);

        /// <summary>
        /// 修改读卡器设备
        /// </summary>
        /// <param name="model">读卡器设备详情</param>
        /// <returns>读卡器设备详情</returns>
        Task<HandleElevatorInputDeviceModel> AddOrEditAsync(HandleElevatorInputDeviceModel model);

        /// <summary>
        /// 物理删除读卡器设备
        /// </summary>
        /// <param name="id">读卡器设备Id</param>
        /// <returns>是否成功</returns>
        Task DeleteAsync(string id);

        Task InitLoadAsync();
        Task AddOrEditUnitAsync(List<UnitHandleElevatorInputDeviceEntity> inputDevices, IRemoteDevice remoteDevice);
    }
}
