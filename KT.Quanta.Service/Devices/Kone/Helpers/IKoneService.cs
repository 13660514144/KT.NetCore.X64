using KT.Quanta.Model.Kone;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Kone.Helpers
{
    public interface IKoneService
    {
        Task DeleteDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model, string addressKey);
        Task DeleteDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model, string addressKey);
        Task<KoneSystemConfigModel> GetSystemConfigAsync();
        Task SetDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model, string addressKey);
        Task SetDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model, string addressKey);
        Task SetSystemConfigAsync(KoneSystemConfigModel model);
        Task SetDopMaskAsync(string remoteIp, int remotePort);
        Task DeleteAndSaveDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model);
        Task SetAndSaveDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model);
        Task DeleteAndSaveDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model);
        Task SetAndSaveDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model);
        Task<DopSpecificDefaultAccessMaskModel> GetDopSpecificMaskByIdAsync(string id);
        Task<List<DopSpecificDefaultAccessMaskModel>> GetAllDopSpecificMasksWithElevatorGroupAndHandleElevatorDeviceAsync();
        Task<DopGlobalDefaultAccessMaskModel> GetDopGlobalMaskByIdAsync(string id);
        Task<List<DopGlobalDefaultAccessMaskModel>> GetAllDopGlobalMasksWithElevatorGroupAsync();
        Task RefreshKoneConfigHekperAsync();
        Task DeleteRoyalDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model);
        Task DeleteRoyalDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model);
        Task SafelyColseHostAsync();
        Task<List<EliPassRightHandleElevatorDeviceCallTypeModel>> GetEliPassRightHandleElevatorDeviceCallTypesByPassRightSign(string passRightSign);
        Task AddOrEditEliPassRightHandleElevatorDeviceCallTypes(List<EliPassRightHandleElevatorDeviceCallTypeModel> models);
        Task DeleteEliPassRightHandleElevatorDeviceCallTypes(List<EliPassRightHandleElevatorDeviceCallTypeModel> models);

        Task<List<RcgifPassRightHandleElevatorDeviceCallTypeModel>> GetRcgifPassRightHandleElevatorDeviceCallTypesByPassRightSign(string passRightSign);

        Task AddOrEditRcgifPassRightHandleElevatorDeviceCallTypes(List<RcgifPassRightHandleElevatorDeviceCallTypeModel> models);
        Task DeleteRcgifPassRightHandleElevatorDeviceCallTypes(List<RcgifPassRightHandleElevatorDeviceCallTypeModel> models);
        Task<List<EliOpenAccessForDopMessageTypeModel>> GetEliOpenAccessForDopMessageTypesByPassRightSign(string passRightSign);
        Task AddOrEditEliOpenAccessForDopMessageTypes(List<EliOpenAccessForDopMessageTypeModel> models);
        Task DeleteEliOpenAccessForDopMessageTypes(List<EliOpenAccessForDopMessageTypeModel> models);
    }
}
