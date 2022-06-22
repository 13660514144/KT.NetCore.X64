using KT.Common.Core.Helpers;
using KT.Common.WebApi.HttpApi;
using KT.Quanta.Common.Enums;
using KT.Quanta.Model.Kone;
using KT.Quanta.Service.Devices.Common;
using KT.Quanta.Service.Devices.Kone.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KT.Quanta.WebApi.Controllers.Elevator
{
    /// <summary>
    /// 通力
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class KoneController : ControllerBase
    {
        private IKoneService _koneService;
        private readonly ILogger<KoneController> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public KoneController(IKoneService koneService,
            ILogger<KoneController> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _koneService = koneService;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpGet("GetDopSpecificMask")]
        public async Task<DataResponse<DopSpecificDefaultAccessMaskModel>> GetDopSpecificMaskAsync(string id)
        {
            var result = await _koneService.GetDopSpecificMaskByIdAsync(id);
            return DataResponse<DopSpecificDefaultAccessMaskModel>.Ok(result);
        }

        [HttpGet("GetAllDopSpecificMasksWithElevatorGroupAndHandleElevatorDevice")]
        public async Task<DataResponse<List<DopSpecificDefaultAccessMaskModel>>> GetAllDopSpecificMasksWithElevatorGroupAndHandleElevatorDeviceAsync()
        {
            var result = await _koneService.GetAllDopSpecificMasksWithElevatorGroupAndHandleElevatorDeviceAsync();
            return DataResponse<List<DopSpecificDefaultAccessMaskModel>>.Ok(result);
        }

        [HttpPost("DeleteDopSpecificMask")]
        public async Task DeleteDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model)
        {
            await _koneService.DeleteAndSaveDopSpecificMaskAsync(model);
        }

        [HttpPost("DeleteRoyalDopSpecificMask")]
        public async Task DeleteRoyalDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model)
        {
            await _koneService.DeleteRoyalDopSpecificMaskAsync(model);
        }

        [HttpPost("SetDopSpecificMask")]
        public async Task SetDopSpecificMaskAsync(DopSpecificDefaultAccessMaskModel model)
        {
            await _koneService.SetAndSaveDopSpecificMaskAsync(model);
        }

        [HttpGet("GetDopGlobalMask")]
        public async Task<DataResponse<DopGlobalDefaultAccessMaskModel>> GetDopGlobalMaskAsync(string id)
        {
            var result = await _koneService.GetDopGlobalMaskByIdAsync(id);
            return DataResponse<DopGlobalDefaultAccessMaskModel>.Ok(result);
        }

        [HttpGet("GetAllDopGlobalMasksWithElevatorGroup")]
        public async Task<DataResponse<List<DopGlobalDefaultAccessMaskModel>>> GetAllDopGlobalMasksWithElevatorGroupAsync()
        {
            var result = await _koneService.GetAllDopGlobalMasksWithElevatorGroupAsync();
            return DataResponse<List<DopGlobalDefaultAccessMaskModel>>.Ok(result);
        }

        [HttpPost("DeleteDopGlobalMask")]
        public async Task DeleteDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model)
        {
            await _koneService.DeleteAndSaveDopGlobalMaskAsync(model);
        }

        [HttpPost("DeleteRoyalDopGlobalMask")]
        public async Task DeleteRoyalDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model)
        {
            await _koneService.DeleteRoyalDopGlobalMaskAsync(model);
        }

        [HttpPost("SetDopGlobalMask")]
        public async Task SetDopGlobalMaskAsync(DopGlobalDefaultAccessMaskModel model)
        {
            await _koneService.SetAndSaveDopGlobalMaskAsync(model);
        }

        [HttpPost("SetSystemConfig")]
        public async Task SetSystemConfigAsync(KoneSystemConfigModel model)
        {
            await _koneService.SetSystemConfigAsync(model);
        }

        [HttpGet("GetSystemConfig")]
        public async Task<DataResponse<KoneSystemConfigModel>> GetSystemConfigAsync()
        {
            var result = await _koneService.GetSystemConfigAsync();
            return DataResponse<KoneSystemConfigModel>.Ok(result);
        }

        [HttpPost("SafelyColseHost")]
        public async Task SafelyColseHostAsync()
        {
            try
            {
                await _koneService.SafelyColseHostAsync();

                //关闭所有连接设备
                using var scope = _serviceScopeFactory.CreateScope();
                var communicateDevices = scope.ServiceProvider.GetRequiredService<CommunicateDeviceList>();
                await communicateDevices.ExecuteAsync(async (communicateDevice) =>
                {
                    if (communicateDevice != null)
                    {
                        if (communicateDevice.CommunicateDeviceInfo.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_ELI.Value
                            || communicateDevice.CommunicateDeviceInfo.CommunicateDeviceType == CommunicateDeviceTypeEnum.KONE_RCGIF.Value)
                        {
                            return;
                        }
                        Task.Run(() =>
                        {
                            communicateDevice.CloseAsync();
                        });
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "关闭服务出错");
            }

            await Task.Delay(10 * 1000);

            Environment.Exit(-1);

            //EnvironmentExitHelper.Start();

            //try
            //{

            ////关闭Host
            //await Program.StopHostAsync();
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "关闭Host出错");
            //}
        }


        [HttpGet("GetEliPassRightHandleElevatorDeviceCallTypesByPassRightSign")]
        public async Task<DataResponse<List<EliPassRightHandleElevatorDeviceCallTypeModel>>> GetEliPassRightHandleElevatorDeviceCallTypesByPassRightSign(string passRightSign)
        {
            var results = await _koneService.GetEliPassRightHandleElevatorDeviceCallTypesByPassRightSign(passRightSign);

            return DataResponse<List<EliPassRightHandleElevatorDeviceCallTypeModel>>.Ok(results);
        }

        [HttpPost("AddOrEditEliPassRightHandleElevatorDeviceCallType")]
        public async Task AddOrEditEliPassRightHandleElevatorDeviceCallType(EliPassRightHandleElevatorDeviceCallTypeModel model)
        {
            var models = new List<EliPassRightHandleElevatorDeviceCallTypeModel>() { model };
            await _koneService.AddOrEditEliPassRightHandleElevatorDeviceCallTypes(models);
        }

        [HttpPost("DeleteEliPassRightHandleElevatorDeviceCallType")]
        public async Task DeleteEliPassRightHandleElevatorDeviceCallType(EliPassRightHandleElevatorDeviceCallTypeModel model)
        {
            var models = new List<EliPassRightHandleElevatorDeviceCallTypeModel>() { model };
            await _koneService.DeleteEliPassRightHandleElevatorDeviceCallTypes(models);
        }

        [HttpPost("AddOrEditEliPassRightHandleElevatorDeviceCallTypes")]
        public async Task AddOrEditEliPassRightHandleElevatorDeviceCallTypes(List<EliPassRightHandleElevatorDeviceCallTypeModel> models)
        {
            await _koneService.AddOrEditEliPassRightHandleElevatorDeviceCallTypes(models);
        }

        [HttpPost("DeleteEliPassRightHandleElevatorDeviceCallTypes")]
        public async Task DeleteEliPassRightHandleElevatorDeviceCallTypes(List<EliPassRightHandleElevatorDeviceCallTypeModel> models)
        {
            await _koneService.DeleteEliPassRightHandleElevatorDeviceCallTypes(models);
        }


        [HttpGet("GetRcgifPassRightHandleElevatorDeviceCallTypesByPassRightSign")]
        public async Task<DataResponse<List<RcgifPassRightHandleElevatorDeviceCallTypeModel>>> GetRcgifPassRightHandleElevatorDeviceCallTypesByPassRightSign(string passRightSign)
        {
            var results = await _koneService.GetRcgifPassRightHandleElevatorDeviceCallTypesByPassRightSign(passRightSign);
            return DataResponse<List<RcgifPassRightHandleElevatorDeviceCallTypeModel>>.Ok(results);
        }

        [HttpPost("AddOrEditRcgifPassRightHandleElevatorDeviceCallType")]
        public async Task AddOrEditRcgifPassRightHandleElevatorDeviceCallTypes(RcgifPassRightHandleElevatorDeviceCallTypeModel model)
        {
            var models = new List<RcgifPassRightHandleElevatorDeviceCallTypeModel>() { model };
            await _koneService.AddOrEditRcgifPassRightHandleElevatorDeviceCallTypes(models);
        }

        [HttpPost("DeleteRcgifPassRightHandleElevatorDeviceCallType")]
        public async Task DeleteRcgifPassRightHandleElevatorDeviceCallTypes(RcgifPassRightHandleElevatorDeviceCallTypeModel model)
        {
            var models = new List<RcgifPassRightHandleElevatorDeviceCallTypeModel>() { model };
            await _koneService.DeleteRcgifPassRightHandleElevatorDeviceCallTypes(models);
        }

        [HttpPost("AddOrEditRcgifPassRightHandleElevatorDeviceCallTypes")]
        public async Task AddOrEditRcgifPassRightHandleElevatorDeviceCallTypes(List<RcgifPassRightHandleElevatorDeviceCallTypeModel> models)
        {
            await _koneService.AddOrEditRcgifPassRightHandleElevatorDeviceCallTypes(models);
        }

        [HttpPost("DeleteRcgifPassRightHandleElevatorDeviceCallTypes")]
        public async Task DeleteRcgifPassRightHandleElevatorDeviceCallTypes(List<RcgifPassRightHandleElevatorDeviceCallTypeModel> models)
        {
            await _koneService.DeleteRcgifPassRightHandleElevatorDeviceCallTypes(models);
        }


        [HttpGet("GetEliOpenAccessForDopMessageTypesByPassRightSign")]
        public async Task<DataResponse<List<EliOpenAccessForDopMessageTypeModel>>> GetEliOpenAccessForDopMessageTypesByPassRightSign(string passRightSign)
        {
            var results = await _koneService.GetEliOpenAccessForDopMessageTypesByPassRightSign(passRightSign);
            return DataResponse<List<EliOpenAccessForDopMessageTypeModel>>.Ok(results);
        }

        [HttpPost("AddOrEditEliOpenAccessForDopMessageType")]
        public async Task AddOrEditEliOpenAccessForDopMessageType(EliOpenAccessForDopMessageTypeModel model)
        {
            var models = new List<EliOpenAccessForDopMessageTypeModel>() { model };
            await _koneService.AddOrEditEliOpenAccessForDopMessageTypes(models);
        }

        [HttpPost("DeleteEliOpenAccessForDopMessageType")]
        public async Task DeleteEliOpenAccessForDopMessageType(EliOpenAccessForDopMessageTypeModel model)
        {
            var models = new List<EliOpenAccessForDopMessageTypeModel>() { model };
            await _koneService.DeleteEliOpenAccessForDopMessageTypes(models);
        }

        [HttpPost("AddOrEditEliOpenAccessForDopMessageTypes")]
        public async Task AddOrEditEliOpenAccessForDopMessageTypes(List<EliOpenAccessForDopMessageTypeModel> models)
        {
            await _koneService.AddOrEditEliOpenAccessForDopMessageTypes(models);
        }

        [HttpPost("DeleteEliOpenAccessForDopMessageTypes")]
        public async Task DeleteEliOpenAccessForDopMessageTypes(List<EliOpenAccessForDopMessageTypeModel> models)
        {
            await _koneService.DeleteEliOpenAccessForDopMessageTypes(models);
        }
    }
}
