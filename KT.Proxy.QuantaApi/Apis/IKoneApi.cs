using KT.Quanta.Model.Kone;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public interface IKoneApi
    {
        Task DeleteDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model);
        Task DeleteDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model);
        Task SetDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model);
        Task SetDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model);
        Task SetSystemConfigAsync(KoneSystemConfigModel model);
        Task<KoneSystemConfigModel> GetSystemConfigAsync();
        Task<DopSpecificDefaultAccessMaskModel> GetDopSpecificMaskAsync(string id);
        Task<List<DopSpecificDefaultAccessMaskModel>> GetAllDopSpecificMasksWithElevatorGroupAndHandleElevatorDeviceAsync();
        Task<DopGlobalDefaultAccessMaskModel> GetDopGlobalMaskAsync(string id);
        Task<List<DopGlobalDefaultAccessMaskModel>> GetAllDopGlobalMasksWithElevatorGroupAsync();
        Task DeleteRoyalDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model);
        Task DeleteRoyalDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model);
        Task SafelyColseHostAsync();


        Task<List<EliPassRightHandleElevatorDeviceCallTypeModel>> GetEliPassRightHandleElevatorDeviceCallTypesByPassRightSign(string passRightSign);
        Task AddOrEditEliPassRightHandleElevatorDeviceCallType(EliPassRightHandleElevatorDeviceCallTypeModel model);
        Task DeleteEliPassRightHandleElevatorDeviceCallType(EliPassRightHandleElevatorDeviceCallTypeModel model);
        Task AddOrEditEliPassRightHandleElevatorDeviceCallTypes(List<EliPassRightHandleElevatorDeviceCallTypeModel> models);
        Task DeleteEliPassRightHandleElevatorDeviceCallTypes(List<EliPassRightHandleElevatorDeviceCallTypeModel> models);
        Task<List<RcgifPassRightHandleElevatorDeviceCallTypeModel>> GetRcgifPassRightHandleElevatorDeviceCallTypesByPassRightSign(string passRightSign);
        Task AddOrEditRcgifPassRightHandleElevatorDeviceCallType(RcgifPassRightHandleElevatorDeviceCallTypeModel model);
        Task DeleteRcgifPassRightHandleElevatorDeviceCallType(RcgifPassRightHandleElevatorDeviceCallTypeModel model);
        Task AddOrEditRcgifPassRightHandleElevatorDeviceCallTypes(List<RcgifPassRightHandleElevatorDeviceCallTypeModel> models);
        Task DeleteRcgifPassRightHandleElevatorDeviceCallTypes(List<RcgifPassRightHandleElevatorDeviceCallTypeModel> models);
        Task<List<EliOpenAccessForDopMessageTypeModel>> GetEliOpenAccessForDopMessageTypesByPassRightSign(string passRightSign);
        Task AddOrEditEliOpenAccessForDopMessageType(EliOpenAccessForDopMessageTypeModel model);
        Task DeleteEliOpenAccessForDopMessageType(EliOpenAccessForDopMessageTypeModel model);
        Task AddOrEditEliOpenAccessForDopMessageTypes(List<EliOpenAccessForDopMessageTypeModel> models);
        Task DeleteEliOpenAccessForDopMessageTypes(List<EliOpenAccessForDopMessageTypeModel> models);
    }
}
