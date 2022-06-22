using KT.Common.Core.Utils;
using KT.Device.Unit.CardReaders.Models;
using KT.Turnstile.Unit.Entity.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Turnstile.Unit.ClientApp.Service.Helpers
{
    /// <summary>
    /// 默认读卡器读卡器设备(单例)
    /// </summary>
    public class ElevatorDisplayDeviceClient
    {
        //串口
        private SerialPort _serialPort;

        private readonly ElevatorDisplayDeviceDataAnalyze _dataAnalyze;
        private readonly ILogger _logger;
        private readonly IEventAggregator _eventAggregator;
        private readonly ElevatorDisplayDeviceSettings _elevatorDisplayDeviceSettings;

        public ElevatorDisplayDeviceClient(ILogger logger,
            IEventAggregator eventAggregator,
            ElevatorDisplayDeviceDataAnalyze dataAnalyze,
            ElevatorDisplayDeviceSettings elevatorDisplayDeviceSettings)
        {
            _logger = logger;
            _eventAggregator = eventAggregator;
            _dataAnalyze = dataAnalyze;
            _elevatorDisplayDeviceSettings = elevatorDisplayDeviceSettings;
        }

        /// <summary>
        /// 初始化读卡器设备
        /// </summary>
        /// <param name="obj">设备</param>
        /// <param name="cardReceiveHandler">接收数据操作</param>
        public Task StartAsync()
        {
            _logger.LogInformation($"{_elevatorDisplayDeviceSettings.SerialSettings.PortName} 串口设备开始创建：{JsonConvert.SerializeObject(_elevatorDisplayDeviceSettings, JsonUtil.JsonPrintSettings)} ");
            //初始化串口信息
            _serialPort = SerialDeviceModelConvert.ToSerialPort(_elevatorDisplayDeviceSettings.SerialSettings);

            //数据接收事件
            _serialPort.DataReceived += _serialPort_DataReceived;

            _logger.LogInformation($"{_serialPort.PortName} 串口设备连接开始！");

            //连接串口
            SerialConnect();

            return Task.CompletedTask;
        }

        private void SerialConnect()
        {
            try
            {
                //打开读卡器设备                                                                             
                _serialPort.DtrEnable = true;
                _serialPort.RtsEnable = true;

                //如果打开状态，则先关闭一下
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                _serialPort.Open();

                _logger.LogInformation($"{_serialPort.PortName} 串口设备连接完成！");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{_serialPort.PortName} 串口设备连接失败：ex:{ex} ");
            }
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender">串口</param>
        /// <param name="e">事件</param>
        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // 执行操作接收数据
            // 获取当前接收数据的串口
            SerialPort sp = sender as SerialPort;
            // 数据的长度
            int len = sp.BytesToRead;
            if (len == 0)
            {
                Thread.Sleep(100);
                return;
            }
            byte[] datas = new byte[len];
            // 读取数据
            sp.Read(datas, 0, len);
            try
            {
                // 转换编码格式，获取数据结果 
                if (datas.Length > 0)
                {
                    _logger.LogInformation($"串口接收数据：port:{sp.PortName} datas:{datas.ToCommaPrintString()}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("接收读卡器设备数据出错：ex:{0} ", ex);
            }
        }

        public void SendAsync(ElevatorDisplayModel elevatorDisplay)
        {
            try
            {

                if (!_elevatorDisplayDeviceSettings.IsEnable)
                {
                    return;
                }

                _logger.LogInformation($"{_elevatorDisplayDeviceSettings.SerialSettings.PortName} 串口设备发送数据：{JsonConvert.SerializeObject(elevatorDisplay, JsonUtil.JsonPrintSettings)} ");

                if (_serialPort == null)
                {
                    StartAsync();
                }

                var bytes = _dataAnalyze.SendAnalyze(elevatorDisplay);

                if (!_serialPort.IsOpen)
                {
                    SerialConnect();
                    if (!_serialPort.IsOpen)
                    {
                        _logger.LogError("读卡器设备未连接！");
                        return;
                    }
                }
                _serialPort.Write(bytes, 0, bytes.Count());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{_elevatorDisplayDeviceSettings.SerialSettings.PortName} 串口设备发送失败！");
            }
        }

        public Task DisposeAsync()
        {
            //串口
            //关闭串口连接
            if (_serialPort != null)
            {
                _serialPort.Close();
                _serialPort.Dispose();
            }

            _serialPort = null;

            return Task.CompletedTask;
        }
    }
}
