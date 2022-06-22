
using KT.Common.Core.Exceptions;
using KT.Common.Core.Utils;
using KT.Elevator.Manage.Service.Devices.Quanta.DistributeDatas;
using KT.Elevator.Manage.Service.Devices.Kone.Models;
using KT.Elevator.Manage.Service.Devices.Kone.Network;
using KT.Elevator.Manage.Service.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Elevator.Manage.Service.Devices.Kone
{
    /// <summary>
    /// 电梯服务
    /// </summary>
    public class KoneServer
    {
        private ElevatorServerModel _elevatorServer;

        // 错误
        private Action<Exception> _errorHandler { get; set; }

        //接收数据
        private KoneEntity _info;
        //socket客户端
        private SocketClient _client;

        private ILogger<KoneServer> _logger;
        private IServiceScopeFactory _serviceScopeFactory;

        public KoneServer(ILogger<KoneServer> logger,
            IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Init(ElevatorServerModel elevatorServer)
        {
            _elevatorServer = elevatorServer;

            InitSocket();

            ListenHeartbit();
        }

        private void InitSocket()
        {
            _info = new KoneEntity();
            _client = new SocketClient();
            _client.ErrorHandler = ErrorHandler;
            _client.ReceivedHandler = ReceiveHandlerAsync;
            _client.Connect(_elevatorServer.IpAddress, _elevatorServer.Port);
        }

        /// <summary>
        /// 输出信息
        /// </summary>
        private void ErrorHandler(Exception ex)
        {
            _errorHandler?.Invoke(ex);
        }

        /// <summary>
        /// 接收服务器信息
        /// </summary>
        private async Task ReceiveHandlerAsync(object sender, byte[] buffer)
        {
            _logger.LogInformation($"派梯接收数据：{buffer.ToCommaString()}");

            var action = new MsgEnity();
            action.Analysis(buffer);

            if (action.Endianess == "8000000011")
            {
                var data = new MsgHeartBeat();
                data.Analysis(buffer);

                _logger.LogInformation($"派梯接收心跳数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonSettings)} ");
            }
            else if (action.Endianess == "800000001F")
            {
                var data = new MsgDisconnect();
                data.Analysis(buffer);

                _logger.LogInformation($"派梯接收连接数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonSettings)} ");
            }
            else if (action.Endianess == "8000000042" || action.Endianess == "8000000040")
            {
                var data = new MsgPaddle();
                data.Analysis(buffer);

                _logger.LogInformation($"派梯接收派梯结果数据：{JsonConvert.SerializeObject(data, JsonUtil.JsonSettings)} ");

                //异步返回派梯结果
                CallPaddleResponseAsync(data);
            }
        }

        private async Task CallPaddleResponseAsync(MsgPaddle data)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var pushDataHubApi = scope.ServiceProvider.GetRequiredService<IQuantaDisplayDistributeDataService>();
                    await pushDataHubApi.HandleElevatorSuccess(data);
                }
            }
            catch (Exception ex)
            {
                var message = ex.GetInner();
                _logger.LogError($"回调派梯错误：message:{message.Message} ex:{ex} ");
            }
        }

        /// <summary>
        /// 派梯
        /// </summary>
        /// <param name="TerminalId">终端Id</param>
        /// <param name="DestCallType">呼叫类型</param>
        /// <param name="SourceFloor">来源楼</param>
        /// <param name="SourceSide">来源方</param>
        /// <param name="DestinationFloor">目的楼层</param>
        /// <param name="DestinationSide">目的地方</param>
        public void CallPaddle(int TerminalId, int DestCallType, int SourceFloor, int SourceSide, int DestinationFloor, int DestinationSide)
        {
            var model = new ResultPaddle();
            model.terminal_Id = TerminalId;
            model.dest_call_Type = DestCallType;

            model.source_floor = SourceFloor;
            model.source_side = SourceSide;
            model.destination_floor = DestinationFloor;
            model.destination_side = DestinationSide;
            model.call_state = 1;
            model.Lifts = 0;

            _logger.LogInformation($"开始派梯：{JsonConvert.SerializeObject(model, JsonUtil.JsonSettings)} ");

            var buffer = _info.calllift(model);
            Send(buffer);

            _logger.LogInformation($"结束派梯：{buffer.ToCommaString()} ");
        }

        /// <summary>
        /// 默认单位：秒
        /// </summary>
        public int SecondTimeout { get; set; } = 10;

        /// <summary>
        /// 默认单位：分钟
        /// </summary>
        public double MinuteTimeout
        {
            get
            {
                var minute = SecondTimeout / 60.0;
                return minute;
            }
            set
            {
                var Second = value * 60;
                SecondTimeout = Convert.ToInt32(Second);
            }
        }

        private Timer _timer = null;
        public void ListenHeartbit()
        {
            if (_timer == null)
            {
                _timer = new Timer(TimerElapsed, null, 0, 30000);
            }
        }

        private void TimerElapsed(object state)
        {
            if (_client == null)
            {
                InitSocket();
            }
            else if (!_client.Connected)
            {
                _client.ConnectSocket();
            }
            else
            {
                HearBeat();
            }
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="buffer"></param>
        private void Send(byte[] buffer)
        {
            if (!_client.Connected)
            {
                _client.ConnectSocket();
            }
            _client.Send(buffer);
        }

        /// <summary>
        /// 发送心跳包
        /// </summary>
        public void HearBeat()
        {
            var buffer = _info.HeartBeat();

            Send(buffer);

            var value = buffer.GetHexString();
            _logger.LogInformation(string.Format("Client.HeartBeat:{0}", value));
        }

        /// <summary>
        /// 发送断开命令
        /// </summary>
        /// <param name="reason"></param>
        public void Disconnect(int reason)
        {
            var buffer = _info.disconnet(reason);
            Send(buffer);

            var value = buffer.GetHexString();
            _logger.LogInformation(string.Format("Client.Disconnect:{0}", value));
        }
    }
}
