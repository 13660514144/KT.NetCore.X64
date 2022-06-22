using KT.Quanta.Model.Kone;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Proxy.QuantaApi.Apis
{
    public class KoneApi : BackendApiBase, IKoneApi
    {
        public KoneApi(ILogger<KoneApi> logger) : base(logger)
        {
        }

        public async Task<DopSpecificDefaultAccessMaskModel> GetDopSpecificMaskAsync(string id)
        {
            return await base.GetAsync<DopSpecificDefaultAccessMaskModel>($"Kone/GetDopSpecificMask?id={id}");
        }

        public async Task<List<DopSpecificDefaultAccessMaskModel>> GetAllDopSpecificMasksWithElevatorGroupAndHandleElevatorDeviceAsync()
        {
            return await base.GetAsync<List<DopSpecificDefaultAccessMaskModel>>("Kone/GetAllDopSpecificMasksWithElevatorGroupAndHandleElevatorDevice");
        }

        public async Task SetDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model)
        {
            await base.PostAsync("Kone/SetDopSpecificMask", model);
        }

        public async Task DeleteDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model)
        {
            await base.PostAsync("Kone/DeleteDopSpecificMask", model);
        }


        public async Task DeleteRoyalDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model)
        {
            await base.PostAsync("Kone/DeleteRoyalDopSpecificMask", model);
        }

        public async Task<DopGlobalDefaultAccessMaskModel> GetDopGlobalMaskAsync(string id)
        {
            return await base.GetAsync<DopGlobalDefaultAccessMaskModel>($"Kone/GetDopGlobalMask?id={id}");
        }

        public async Task<List<DopGlobalDefaultAccessMaskModel>> GetAllDopGlobalMasksWithElevatorGroupAsync()
        {
            return await base.GetAsync<List<DopGlobalDefaultAccessMaskModel>>("Kone/GetAllDopGlobalMasksWithElevatorGroup");
        }

        public async Task SetDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model)
        {
            await base.PostAsync("Kone/SetDopGlobalMask", model);
        }

        public async Task DeleteDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model)
        {
            await base.PostAsync("Kone/DeleteDopGlobalMask", model);
        }

        public async Task DeleteRoyalDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model)
        {
            await base.PostAsync("Kone/DeleteRoyalDopGlobalMask", model);
        }

        public async Task SetSystemConfigAsync(KoneSystemConfigModel model)
        {
            await base.PostAsync("Kone/SetSystemConfig", model);
        }
        public async Task<KoneSystemConfigModel> GetSystemConfigAsync()
        {
            return await base.GetAsync<KoneSystemConfigModel>("Kone/GetSystemConfig");
        }

        public async Task SafelyColseHostAsync()
        {
            await base.PostAsync("Kone/SafelyColseHost");
        }



        public async Task<List<EliPassRightHandleElevatorDeviceCallTypeModel>> GetEliPassRightHandleElevatorDeviceCallTypesByPassRightSign(string passRightSign)
        {
            return await base.GetAsync<List<EliPassRightHandleElevatorDeviceCallTypeModel>>($"Kone/GetEliPassRightHandleElevatorDeviceCallTypesByPassRightSign?passRightSign={passRightSign}");
        }

        public async Task AddOrEditEliPassRightHandleElevatorDeviceCallType(EliPassRightHandleElevatorDeviceCallTypeModel model)
        {
            await base.PostAsync("Kone/AddOrEditEliPassRightHandleElevatorDeviceCallType", model);
        }

        public async Task DeleteEliPassRightHandleElevatorDeviceCallType(EliPassRightHandleElevatorDeviceCallTypeModel model)
        {
            await base.PostAsync("Kone/DeleteEliPassRightHandleElevatorDeviceCallType", model);
        }

        public async Task AddOrEditEliPassRightHandleElevatorDeviceCallTypes(List<EliPassRightHandleElevatorDeviceCallTypeModel> models)
        {
            await base.PostAsync("Kone/AddOrEditEliPassRightHandleElevatorDeviceCallTypes", models);
        }

        public async Task DeleteEliPassRightHandleElevatorDeviceCallTypes(List<EliPassRightHandleElevatorDeviceCallTypeModel> models)
        {
            await base.PostAsync("Kone/DeleteEliPassRightHandleElevatorDeviceCallTypes", models);
        }


        public async Task<List<RcgifPassRightHandleElevatorDeviceCallTypeModel>> GetRcgifPassRightHandleElevatorDeviceCallTypesByPassRightSign(string passRightSign)
        {
            return await base.GetAsync<List<RcgifPassRightHandleElevatorDeviceCallTypeModel>>($"Kone/GetRcgifPassRightHandleElevatorDeviceCallTypesByPassRightSign?passRightSign={passRightSign}");
        }

        public async Task AddOrEditRcgifPassRightHandleElevatorDeviceCallType(RcgifPassRightHandleElevatorDeviceCallTypeModel model)
        {
            await base.PostAsync("Kone/AddOrEditRcgifPassRightHandleElevatorDeviceCallType", model);
        }

        public async Task DeleteRcgifPassRightHandleElevatorDeviceCallType(RcgifPassRightHandleElevatorDeviceCallTypeModel model)
        {
            await base.PostAsync("Kone/DeleteRcgifPassRightHandleElevatorDeviceCallType", model);
        }

        public async Task AddOrEditRcgifPassRightHandleElevatorDeviceCallTypes(List<RcgifPassRightHandleElevatorDeviceCallTypeModel> models)
        {
            await base.PostAsync("Kone/AddOrEditRcgifPassRightHandleElevatorDeviceCallTypes", models);
        }

        public async Task DeleteRcgifPassRightHandleElevatorDeviceCallTypes(List<RcgifPassRightHandleElevatorDeviceCallTypeModel> models)
        {
            await base.PostAsync("Kone/DeleteRcgifPassRightHandleElevatorDeviceCallTypes", models);
        }


        public async Task<List<EliOpenAccessForDopMessageTypeModel>> GetEliOpenAccessForDopMessageTypesByPassRightSign(string passRightSign)
        {
            return await base.GetAsync<List<EliOpenAccessForDopMessageTypeModel>>($"Kone/GetEliOpenAccessForDopMessageTypesByPassRightSign?passRightSign={passRightSign}");
        }

        public async Task AddOrEditEliOpenAccessForDopMessageType(EliOpenAccessForDopMessageTypeModel model)
        {
            await base.PostAsync("Kone/AddOrEditEliOpenAccessForDopMessageType", model);
        }

        public async Task DeleteEliOpenAccessForDopMessageType(EliOpenAccessForDopMessageTypeModel model)
        {
            await base.PostAsync("Kone/DeleteEliOpenAccessForDopMessageType", model);
        }

        public async Task AddOrEditEliOpenAccessForDopMessageTypes(List<EliOpenAccessForDopMessageTypeModel> models)
        {
            await base.PostAsync("Kone/AddOrEditEliOpenAccessForDopMessageTypes", models);
        }

        public async Task DeleteEliOpenAccessForDopMessageTypes(List<EliOpenAccessForDopMessageTypeModel> models)
        {
            await base.PostAsync("Kone/DeleteEliOpenAccessForDopMessageTypes", models);
        }

    }
}
