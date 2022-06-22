using DotNetty.Transport.Channels;
using KT.Common.Core.Enums;
using KT.Common.Core.Utils;
using KT.Quanta.Entity.Kone;
using KT.Quanta.IDao.Kone;
using KT.Quanta.Service.Devices.Kone.Helpers;
using KT.Quanta.Service.Devices.Kone.Models;
using KT.Quanta.Service.Devices.Kone.Requests;
using KT.Quanta.Service.Devices.Quanta.DistributeDatas;
using KT.Quanta.Service.Devices.Quanta.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace KT.Quanta.Service.Devices.Kone
{
    /// <summary>
    /// 通力派梯返回结果操作
    /// 当前类对象对应每个连接只初始化一次
    /// </summary>
    public class KoneEliReponseHandler : IKoneEliReponseHandler
    {
        private ILogger<KoneEliReponseHandler> _logger;
        private IServiceScopeFactory _serviceScopeFactory;
        private readonly IKoneEliHandleElevatorSequenceList _koneEliHandleElevatorSequenceList;

        public KoneEliReponseHandler(ILogger<KoneEliReponseHandler> logger,
            IServiceScopeFactory serviceScopeFactory,
            IKoneEliHandleElevatorSequenceList koneEliHandleElevatorSequenceList)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _koneEliHandleElevatorSequenceList = koneEliHandleElevatorSequenceList;
        }

        /// <summary>
        /// 心跳 8000000011
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public Task HeartBeatAsync(Requests.KoneEliHeaderResponse headResponse, byte[] buffer)
        {
            var isLittleEndianess = headResponse.IsLittleEndianess();
            var data = new KoneEliHeartbeatMessageResponse();
            data.ReadFromBytes(buffer, isLittleEndianess);

            _logger.LogInformation($"派梯接收心跳数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

            return Task.CompletedTask;
        }

        /// <summary>
        /// 断开连接 800000001F
        /// </summary>
        /// <returns></returns>
        public Task DisconnectAsync(Requests.KoneEliHeaderResponse headResponse, byte[] buffer)
        {
            var isLittleEndianess = headResponse.IsLittleEndianess();
            var data = new KoneEliDisconnectMessageResponse();
            data.ReadFromBytes(buffer, isLittleEndianess);

            _logger.LogInformation($"派梯接收断开数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

            return Task.CompletedTask;
        }

        public async Task PaddleAsync(Requests.KoneEliHeaderResponse headResponse, Requests.KoneEliSubHeader1Response subHeadResponse)
        {
            var isLittleEndianess = headResponse.IsLittleEndianess();
            var data = new KoneEliDopMessageResponse();
            data.ReadFromBytes(subHeadResponse.Datas, isLittleEndianess);

            _logger.LogInformation($"派梯接收派梯结果数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

            //获取数据来源
            var sequence = _koneEliHandleElevatorSequenceList.Get(subHeadResponse.ResponseId);
            if (sequence == null)
            {
                _logger.LogError($"派梯接收派梯结果数据：未找到派梯对列数据！");
                return;
            }

            var elevatorDisplay = new ElevatorDisplayModel();
            elevatorDisplay.TerminalId = data.DopId;
            elevatorDisplay.DestinationFloor = data.DopFloorId.ToString();
            elevatorDisplay.ElevatorId = data.DopMessageId.ToString();
            elevatorDisplay.MessageId = sequence.MessageId;
            elevatorDisplay.HandleElevatorDeviceId = sequence.HandleElevatorDeviceId;
            elevatorDisplay.CardDeviceId = sequence.CardDeviceId;

            //显示派梯结果
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var quantaDisplayDistributeDataService = scope.ServiceProvider.GetRequiredService<IQuantaDisplayDistributeDataService>();
                await quantaDisplayDistributeDataService.HandleElevatorSuccess(elevatorDisplay);
            }
        }

        /// <summary>
        /// 操作状态，包含派梯、Mask设置
        /// </summary>
        /// <param name="headResponse"></param>
        /// <param name="subHeadResponse"></param>
        /// <returns></returns>
        public async Task StatusAsync(KoneEliHeaderResponse headResponse, KoneEliSubHeader1Response subHeadResponse, IChannelHandlerContext context)
        {
            var isLittleEndianess = headResponse.IsLittleEndianess();
            var data = new KoneEliStatusResponse();
            data.ReadFromBytes(subHeadResponse.Datas, isLittleEndianess);

            _logger.LogInformation($"派梯接收派梯结果数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

            //获取数据来源
            var sequence = _koneEliHandleElevatorSequenceList.Get(subHeadResponse.ResponseId);
            if (sequence == null)
            {
                _logger.LogError($"派梯接收派梯结果数据：未找到派梯对列数据！");
                return;
            }

            var elevatorDisplay = new ElevatorDisplayModel();
            elevatorDisplay.MessageId = sequence.MessageId;
            elevatorDisplay.HandleElevatorDeviceId = sequence.HandleElevatorDeviceId;
            elevatorDisplay.CardDeviceId = sequence.CardDeviceId;

            using var scope = _serviceScopeFactory.CreateScope();

            var dopMaskRecordDao = scope.ServiceProvider.GetRequiredService<IDopMaskRecordDao>();
            if (KoneEliSequenceTypeEnum.DopGlobalDefaultMask.Equals(sequence.Type))
            {
                _logger.LogInformation($"dop global mask result: code:{data.ResponseCode} ");

                await SaveRecordAsync(data, sequence, dopMaskRecordDao, context);

                //重发错误数据

                return;
            }
            else if (KoneEliSequenceTypeEnum.DopSepcificDefaultMask.Equals(sequence.Type))
            {
                _logger.LogInformation($"dop sepcific mask result: code:{data.ResponseCode} ");
                await SaveRecordAsync(data, sequence, dopMaskRecordDao, context);

                //重发错误数据

                return;
            }

            var quantaDisplayDistributeDataService = scope.ServiceProvider.GetRequiredService<IQuantaDisplayDistributeDataService>();
            if (data.ResponseCode == KoneEliStatusEnum.BackupGroup.Code)
            {
                return;
            }
            else if (data.ResponseCode != KoneEliStatusEnum.OK.Code)
            {
                var error = BaseEnum.FromCode<KoneEliStatusEnum>(data.ResponseCode);
                if (error != null)
                {
                    _logger.LogError($"操作错误：code:{data.ResponseCode} message:{error.Text} ");
                }
                else
                {
                    _logger.LogError($"操作错误：code:{data.ResponseCode} ");
                }

                //显示派梯结果 
                await quantaDisplayDistributeDataService.HandleElevatorError(elevatorDisplay, $"派梯失败：{error.Text} ");

                return;
            }
            else
            {
                //显示派梯结果 
                await quantaDisplayDistributeDataService.HandleElevatorStatus(elevatorDisplay);
            }
        }

        private async Task SaveRecordAsync(KoneEliStatusResponse data, KoneEliSequenceModel sequence, IDopMaskRecordDao dopMaskRecordDao, IChannelHandlerContext context)
        {
            var dopMaskRecord = new DopMaskRecordEntity();
            try
            {
                //保存记录
                dopMaskRecord.SequenceId = sequence.Id;
                var remoteAddress = (IPEndPoint)context.Channel.RemoteAddress;
                dopMaskRecord.ElevatorServer = $"{remoteAddress.Address.ToString().TrimStartString("::ffff:")}:{remoteAddress.Port}";
                dopMaskRecord.IsSuccess = (data.ResponseCode == KoneEliStatusEnum.BackupGroup.Code || data.ResponseCode == KoneEliStatusEnum.OK.Code);
                dopMaskRecord.Status = data.ResponseCode;
                dopMaskRecord.Type = sequence.Type.Value;
                dopMaskRecord.Operate = KoneEliMaskRecoredOperateEnum.Receive.Value;
                dopMaskRecord.SendData = JsonConvert.SerializeObject(sequence.Data, JsonUtil.JsonSettings);
                dopMaskRecord.ReceiveData = JsonConvert.SerializeObject(data, JsonUtil.JsonSettings);

                await dopMaskRecordDao.InsertAsync(dopMaskRecord);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"保存接收记录失败：{JsonConvert.SerializeObject(dopMaskRecord, JsonUtil.JsonPrintSettings)} ");
            }
        }

        public async Task DopClosedAsync(KoneEliHeaderResponse headResponse, KoneEliSubHeader1Response subHeadResponse)
        {
            var isLittleEndianess = headResponse.IsLittleEndianess();
            var data = new KoneEliAccessClosedForDopMessage();
            data.ReadFromBytes(subHeadResponse.Datas, isLittleEndianess);

            _logger.LogInformation($"派梯接收派梯结果数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonPrintSettings)} ");

            //获取数据来源
            var sequence = _koneEliHandleElevatorSequenceList.Get(subHeadResponse.ResponseId);
            if (sequence == null)
            {
                _logger.LogError($"派梯接收派梯结果数据：未找到派梯对列数据！");
            }

            var elevatorDisplay = new ElevatorDisplayModel();
            elevatorDisplay.TerminalId = data.DopId;
            elevatorDisplay.DestinationFloor = data.DestinationFloor.ToString();
            //elevatorDisplay.ElevatorId = data.DopId.ToString();
            elevatorDisplay.MessageId = sequence.MessageId;
            elevatorDisplay.HandleElevatorDeviceId = sequence.HandleElevatorDeviceId;
            elevatorDisplay.CardDeviceId = sequence.CardDeviceId;

            // 返回错误信息
            if (data.IsTimeoutExceeded != KoneEliIsTimeoutExceededEnum.No.Code)
            {
                var error = BaseEnum.FromCode<KoneEliIsTimeoutExceededEnum>(data.IsTimeoutExceeded);
                if (error != null)
                {
                    _logger.LogError($"操作错误:IsTimeoutExceeded:code:{data.IsTimeoutExceeded} message:{error.Text} ");
                }
                else
                {
                    _logger.LogError($"操作错误:IsTimeoutExceeded:code:{error.Text} ");
                }

                //显示派梯结果
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var quantaDisplayDistributeDataService = scope.ServiceProvider.GetRequiredService<IQuantaDisplayDistributeDataService>();
                    await quantaDisplayDistributeDataService.HandleElevatorError(elevatorDisplay, $"派梯失败：{error.Text} ");
                }

                return;
            }
            // 返回错误信息
            else if (data.DestinationFloor == 0)
            {
                _logger.LogError($"操作错误选择目的楼层为空！ ");

                //显示派梯结果
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var quantaDisplayDistributeDataService = scope.ServiceProvider.GetRequiredService<IQuantaDisplayDistributeDataService>();
                    await quantaDisplayDistributeDataService.HandleElevatorError(elevatorDisplay, $"派梯失败：选择目的楼层为空！");
                }

                return;
            }

            //显示派梯结果
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var quantaDisplayDistributeDataService = scope.ServiceProvider.GetRequiredService<IQuantaDisplayDistributeDataService>();
                await quantaDisplayDistributeDataService.HandleElevatorSuccess(elevatorDisplay);
            }
        }
    }
}
