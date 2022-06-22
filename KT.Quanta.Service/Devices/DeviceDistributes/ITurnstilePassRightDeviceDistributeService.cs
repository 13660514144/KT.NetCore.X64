using KT.Quanta.Service.Entities;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.DeviceDistributes
{
    /// <summary>
    /// 分发权限
    /// </summary>
    public interface ITurnstilePassRightDeviceDistributeService
    {
        /// <summary>
        /// 新增或修改权限
        /// </summary>
        /// <param name="model">权限</param>
        /// <param name="face">人脸，人脸权限时不能为空</param>
        Task AddOrUpdateAsync(TurnstilePassRightModel model, FaceInfoModel face = null);

        /// <summary>
        /// 删除设备权限
        /// </summary>
        /// <param name="model">权限</param>
        Task DeleteAsync(TurnstilePassRightModel model);

        /// <summary>
        /// 删除指定的设备权限
        /// </summary>
        /// <param name="deleteDeviceIds">分发设备ids</param>
        /// <param name="model">权限</param>
        Task DeleteAsync(List<string> deleteDeviceIds, TurnstilePassRightModel model);

        /// <summary>
        /// 新增或修改权限
        /// </summary>
        /// <param name="addDeviceIds">分发设备ids</param>
        /// <param name="model">权限</param>
        /// <param name="face">人脸，人脸权限时不能为空</param>
        Task AddOrUpdateAsync(List<string> addOrEditDeviceIds, TurnstilePassRightModel model, FaceInfoModel face = null);
    }
}