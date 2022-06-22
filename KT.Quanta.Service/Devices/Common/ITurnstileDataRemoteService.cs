using KT.Quanta.Service.Devices.Hikvision;
using KT.Quanta.Service.Models;
using KT.Quanta.Service.Turnstile.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Common
{
    /// <summary>
    /// 闸机数据推送服务
    /// </summary>
    public interface ITurnstileDataRemoteService
    {
        Task AddOrUpdateCardDeviceAsync(IRemoteDevice remoteDevice, TurnstileCardDeviceModel model);
        Task DeleteCardDeviceAsync(IRemoteDevice remoteDevice, string id, long time);
        Task AddOrUpdatePassRightAsync(IRemoteDevice remoteDevice, TurnstilePassRightModel model, FaceInfoModel faceInfo);
        Task DeletePassRightAsync(IRemoteDevice remoteDevice, TurnstilePassRightModel model);
        Task AddOrUpdateCardDeviceRightGroupAsync(IRemoteDevice remoteDevice, TurnstileCardDeviceRightGroupModel model);
        Task DeleteCardDeviceRightGroupAsync(IRemoteDevice remoteDevice, string id, long time);
        Task<int> GetOutputNumAsync(IRemoteDevice remoteDevice);
    }
}
