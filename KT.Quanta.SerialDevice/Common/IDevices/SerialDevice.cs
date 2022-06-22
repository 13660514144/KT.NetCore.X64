using KT.Quanta.SerialDevice.Common.Handlers;
using KT.Quanta.SerialDevice.Common.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;

namespace KT.Quanta.SerialDevice.Common.IDevices
{
    /// <summary>
    /// 串口设备
    /// </summary>
    /// <typeparam name="DATA">接收数据类型</typeparam>
    public abstract class SerialDevice<DATA> : ISerialDevice
    {
        private ILogger<SerialDevice<DATA>> _logger;
        public SerialDevice(ILogger<SerialDevice<DATA>> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 编码方式
        /// </summary>
        public Encoding Encoding { get; } = Encoding.UTF8;

        /// <summary>
        /// 串口对象
        /// </summary>
        public SerialPort SerialPort { get; } = new SerialPort();

        private ISerialDataAnalyze _dataAnalyze;
        private ISerialHandler _serialHandler;

        /// <summary>
        /// 初始化数据
        /// </summary>
        /// <param name="serialPort"></param>
        /// <param name="dataAnalyze"></param>
        /// <returns></returns>
        public async Task InitAsync(ISerialDataAnalyze dataAnalyze, ISerialHandler serialHandler)
        {
            _dataAnalyze = dataAnalyze;
            _serialHandler = serialHandler;

            SerialPort.DataReceived += SerialPort_DataReceived;
            await OpenAsync();
        }

        private async void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // 执行操作接收数据
            // 获取当前接收数据的串口
            SerialPort sp = sender as SerialPort;
            // 数据的长度
            int len = sp.BytesToRead;
            byte[] datas = new byte[len];
            // 读取数据
            sp.Read(datas, 0, len);
            try
            {
                // 转换编码格式，获取数据结果 
                var receiveData = new SerialMessageModel();
                receiveData.Address = sp.PortName;
                receiveData.Datas = datas;

                var result = _dataAnalyze.AnalyzeAsync<DATA>(receiveData);

                // 处理接收数据
                await _serialHandler.ReceiveAsync(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("接收读卡器设备数据出错：ex:{0} ", ex);
            }
            await Task.Delay(100);
        }


        public async void Dispose()
        {
            //关闭串口连接
            await CloseAsync();
            SerialPort.Dispose();
        }

        public async Task OpenAsync()
        {
            SerialPort.DtrEnable = true;
            SerialPort.RtsEnable = true;

            _logger.LogInformation($"串口设备连接：protName:{SerialPort.PortName} ");
            //如果打开状态，则先关闭一下
            if (SerialPort.IsOpen)
            {
                SerialPort.Close();
            }
            SerialPort.Open();

            _logger.LogInformation($"串口设备连接完成：protName:{ SerialPort.PortName} ");
        }

        public async Task CloseAsync()
        {
            _logger.LogInformation($"串口设备关闭连接：protName:{SerialPort.PortName} ");
            //如果打开状态，则先关闭一下
            if (SerialPort.IsOpen)
            {
                SerialPort.Close();
                _logger.LogInformation($"串口设备开启连接完成：protName:{ SerialPort.PortName} ");
            }
            else
            {
                _logger.LogInformation($"串口设备未开启：protName:{ SerialPort.PortName} ");
            }
        }
    }
}
