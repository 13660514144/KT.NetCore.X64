using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Quanta.DistributeDatas;
using KT.Quanta.Service.Hubs;
using KT.Quanta.Service.Models;
using KT.Elevator.Unit.Entity.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using KT.Quanta.Service.Turnstile.Dtos;
using KT.Quanta.Common.Enums;

namespace KT.Quanta.Service.Devices.Quanta
{
    /// <summary>
    /// 康塔电梯远程服务
    /// </summary>
    public class QuantaTurnstileDataRemoteService : ITurnstileDataRemoteService
    {
        private ILogger<QuantaTurnstileDataRemoteService> _logger;
        private IQuantaTurnstileCardDeviceDistributeDataService _cardDeviceDistribute;
        private IQuantaTurnstilePassRightDistributeDataService _passRightDistribute;
        private IQuantaTurnstileRightGroupDistributeDataService _rightGroupDistribute;

        public QuantaTurnstileDataRemoteService(ILogger<QuantaTurnstileDataRemoteService> logger,
            IQuantaTurnstileCardDeviceDistributeDataService cardDeviceDistribute,
            IQuantaTurnstilePassRightDistributeDataService passRightDistribute,
            IQuantaTurnstileRightGroupDistributeDataService rightGroupDistribute)
        {
            _logger = logger;
            _cardDeviceDistribute = cardDeviceDistribute;
            _passRightDistribute = passRightDistribute;
            _rightGroupDistribute = rightGroupDistribute;
        }

        public async Task AddOrUpdateCardDeviceAsync(IRemoteDevice remoteDevice, TurnstileCardDeviceModel model)
        {            
            if (model.BrandModel == BrandModelEnum.QSCS_R811.Value
              || model.BrandModel == BrandModelEnum.QSCS_BXEN.Value
              || model.BrandModel == BrandModelEnum.QSCS_BX5N.Value
              || model.BrandModel == BrandModelEnum.QSCS_BX5X.Value
              || model.BrandModel == BrandModelEnum.NLS_FM25_R.Value
              || model.BrandModel == BrandModelEnum.QIACS_QT660_R.Value
              || model.BrandModel == BrandModelEnum.QIACS_R824.Value
              || model.BrandModel == BrandModelEnum.QIACS_R992.Value)
            {
                await _cardDeviceDistribute.AddOrUpdateAsync(remoteDevice.RemoteDeviceInfo, model);
            }
            else
            {
                _logger.LogWarning($"当前读卡器不推送到边缘处理器：读卡器类型不符合推送类型要求：brandModel:{model.BrandModel} ");
            }
        }

        public async Task AddOrUpdateCardDeviceRightGroupAsync(IRemoteDevice remoteDevice, TurnstileCardDeviceRightGroupModel model)
        {
            await _rightGroupDistribute.AddOrUpdateAsync(remoteDevice.RemoteDeviceInfo, model);
        }

        public async Task AddOrUpdatePassRightAsync(IRemoteDevice remoteDevice, TurnstilePassRightModel model, FaceInfoModel faceInfo)
        {
            await _passRightDistribute.AddOrUpdateAsync(remoteDevice.RemoteDeviceInfo, model, faceInfo);
        }

        public async Task DeleteCardDeviceAsync(IRemoteDevice remoteDevice, string id, long time)
        {
            await _cardDeviceDistribute.DeleteAsync(remoteDevice.RemoteDeviceInfo, id, time);
        }

        public async Task DeleteCardDeviceRightGroupAsync(IRemoteDevice remoteDevice, string id, long time)
        {
            await _rightGroupDistribute.DeleteAsync(remoteDevice.RemoteDeviceInfo, id, time);
        }

        public async Task DeletePassRightAsync(IRemoteDevice remoteDevice, TurnstilePassRightModel model)
        {
            await _passRightDistribute.DeleteAsync(remoteDevice.RemoteDeviceInfo, model);
        }

        public Task<int> GetOutputNumAsync(IRemoteDevice remoteDevice)
        {
            throw new System.NotImplementedException();
        }
    }
}
