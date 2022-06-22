using KT.Common.WebApi.HttpModel;
using KT.Proxy.BackendApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KT.Proxy.BackendApi.Apis
{
    public interface IFunctionApi
    {
        Task<VisitorConfigParms> GetConfigParmsAsync();
        Task<FileInfoModel> GetQrAsync(string content);
        Task<FileInfoModel> GetPictureAsync(string url);
        Task<VisitorSettingModel> GetSystemSetting();
        Task HeartbeatAsync();
        Task SendNotifyAsync(NotifyModel model);
        Task<long> GetSystemTime();
    }
}
