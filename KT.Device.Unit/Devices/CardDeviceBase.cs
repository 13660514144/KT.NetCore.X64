using KT.Common.Core.Utils;
using KT.Device.Unit.CardReaders.Datas;
using KT.Device.Unit.CardReaders.Events;
using KT.Device.Unit.CardReaders.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Prism.Events;
using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Device.Unit.Devices
{
    /// <summary>
    /// 默认读卡器读卡器设备
    /// </summary>
    public abstract class CardDeviceBase : ICardDeviceBase
    {
        //读卡器设备
        public object CardDeviceInfo { get; private set; }

        //串口
        public SerialPort SerialPort { get; private set; }

        private ICardDeviceAnalyze _dataAnalyze;

        private readonly ILogger _logger;
        private readonly IEventAggregator _eventAggregator;

        public CardDeviceBase(ILogger logger,
            IEventAggregator eventAggregator,
            ICardDeviceAnalyze dataAnalyze)
        {
            _logger = logger;
            _eventAggregator = eventAggregator;
            _dataAnalyze = dataAnalyze;
        }

        /// <summary>
        /// 初始化读卡器设备
        /// </summary>
        /// <param name="obj">设备</param>
        /// <param name="cardReceiveHandler">接收数据操作</param>
        public Task StartAsync(object obj)
        {
            if (!(obj is ICardDeviceModel))
            {
                throw new Exception($"设备数据类型错误：type:{ obj.GetType().FullName} need:{typeof(ICardDeviceModel).FullName} ");
            }

            //初始化读卡器设备
            CardDeviceInfo = obj;

            var cardDevice = (ICardDeviceModel)CardDeviceInfo;

            //初始化串口信息
            SerialPort = SerialDeviceModelConvert.ToSerialPort(cardDevice);

            //数据接收事件
            SerialPort.DataReceived += _serialPort_DataReceived;

            _logger.LogInformation($"{SerialPort.PortName} 串口设备连接开始！");
            try
            {
                //打开读卡器设备                                                                             
                SerialPort.DtrEnable = true;
                SerialPort.RtsEnable = true;

                //如果打开状态，则先关闭一下
                if (SerialPort.IsOpen)
                {
                    SerialPort.Close();
                }
                SerialPort.Open();

                _logger.LogInformation($"{SerialPort.PortName} 串口设备连接完成！");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{SerialPort.PortName} 串口设备连接失败：ex:{ex} ");
            }

            return Task.CompletedTask;
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
                //Thread.Sleep(50);
                return;
            }
            byte[] datas = new byte[len];
            // 读取数据
            sp.Read(datas, 0, len);
            //_logger.LogWarning("接收读卡器设备数据：PortName:{0} data{1}", sp.PortName,datas);
            try
            {
                // 转换编码格式，获取数据结果 
                var cardReceive = _dataAnalyze.Analyze(sp.PortName, datas);
                if (cardReceive == null || string.IsNullOrEmpty(cardReceive.CardNumber))
                {
                    _logger.LogWarning("接收读卡器设备数据为空：PortName:{0} ", sp.PortName);
                    return;
                }

                //数据处理
                var result = new CardDeviceAnalyzedModel();
                result.CardReceive = cardReceive;
                result.CardDeviceInfo = CardDeviceInfo;
                _logger.LogInformation($"CardDeviceAnalyzedEvent=读卡器接收数据结果：{JsonConvert.SerializeObject(result, JsonUtil.JsonPrintSettings)} ");
                _eventAggregator.GetEvent<CardDeviceAnalyzedEvent>().Publish(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("接收读卡器设备数据出错：ex:{0} ", ex);
            }
            Thread.Sleep(100);
        }

        public ValueTask DisposeAsync()
        {
            //设备
            CardDeviceInfo = null;
            //串口
            //关闭串口连接
            SerialPort.Close();
            SerialPort.Dispose();
            SerialPort = null;

            return new ValueTask();
        }
    }
}
