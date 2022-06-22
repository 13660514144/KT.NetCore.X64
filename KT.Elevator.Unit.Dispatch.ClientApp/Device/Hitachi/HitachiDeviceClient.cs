using KT.Device.Unit.CardReaders.Datas;
using KT.Device.Unit.CardReaders.Events;
using KT.Device.Unit.CardReaders.Models;
using KT.Elevator.Unit.Dispatch.ClientApp.Device.Hitachi;
using KT.Elevator.Unit.Dispatch.ClientApp.Events;
using KT.Elevator.Unit.Dispatch.Entity.Models;
using Microsoft.Extensions.Logging;
using Prism.Events;
using System;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KT.Device.Unit.Devices
{
    /// <summary>
    /// 默认读卡器读卡器设备
    /// </summary>
    public class HitachiDeviceClient
    {
        //串口
        private SerialPort _serialPort;

        private HitachiDataAnalyze _dataAnalyze;

        private readonly ILogger _logger;
        private readonly IEventAggregator _eventAggregator;

        public HitachiDeviceClient(ILogger logger,
            IEventAggregator eventAggregator,
            HitachiDataAnalyze dataAnalyze)
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
        public Task StartAsync(HitachiDeviceModel obj)
        {
            if (!(obj is ICardDeviceModel))
            {
                throw new Exception($"设备数据类型错误：type:{ obj.GetType().FullName} need:{typeof(ICardDeviceModel).FullName} ");
            }

            //初始化串口信息
            _serialPort = SerialDeviceModelConvert.ToSerialPort(obj);

            //数据接收事件
            _serialPort.DataReceived += _Hitach_DataReceived;

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

        public async Task UpdateAsync(HitachiDeviceModel obj)
        {
            await this.DisposeAsync();
            await StartAsync(obj);
        }

        /// <summary>
        /// 接收数据 2021-06-13 Ver
        /// </summary>
        /// <param name="sender">串口</param>
        /// <param name="e">事件</param>

        public void _Hitach_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string Msg;
            System.IO.Ports.SerialPort sp = sender as System.IO.Ports.SerialPort;
            // 数据的长度
            int Rx_Len = sp.BytesToRead;
            if (Rx_Len == 0)
            {
                Thread.Sleep(100);
                return;
            }
            byte[] datas = new byte[Rx_Len];
            // 读取数据
            sp.Read(datas, 0, Rx_Len);
            //解析数据，
            if ((Rx_Len == 9) && (datas[0] == 0xAC) && (datas[8] == 0xCA))//查询包 长度9
            {
                //判断选层器状态
                if (datas[7] == (datas[0] ^ datas[1] ^ datas[2] ^ datas[3] ^ datas[4] ^ datas[5] ^ datas[6]))
                {
                    if ((datas[6] & 0x01) == 0x00) //选层器工作正常
                    {
                        //正常不记录，异常才记录
                    }
                    else
                    {
                        _logger.LogError("\r\n==>通信箱选层器状态【异常】");
                    }
                }
            }
            //是否通信箱输出包
            else if ((Rx_Len == 14) && (datas[0] == 0xAC) && (datas[13] == 0xCA))
            {
                if (datas[12] == (datas[0] ^ datas[1] ^ datas[2] ^ datas[3] ^ datas[4]
                    ^ datas[5] ^ datas[6] ^ datas[7] ^ datas[8] ^ datas[9] ^ datas[10]
                    ^ datas[11]))
                {
                    //组装结果应答包
                    byte[] AckByte = new byte[5];
                    AckByte[0] = 0xAC;
                    AckByte[1] = 0x5B;
                    AckByte[2] = datas[2];
                    AckByte[3] = (byte)(AckByte[0] ^ AckByte[1] ^ AckByte[2]);
                    AckByte[4] = 0xCA;
                    _logger.LogInformation("\r\n==>应答包数据：" + BitConverter.ToString(AckByte));

                    if (datas[2] == 0x01)
                    {
                        //这个时候写输出屏
                        _logger.LogInformation("\r\n ==> 派梯中");
                    }
                    else if (datas[2] == 0x02)
                    {
                        byte[] elename = new byte[4];//梯号
                        for (int i = 0; i < 4; i++)
                        {
                            elename[i] = datas[4 + i];
                        }
                        try
                        {

                            if ((elename[0] == (byte)0x00) && (elename[1] == (byte)0x00)
                                && (elename[2] == (byte)0x00)
                                && (elename[3] == (byte)0x00))
                            {
                                Msg = "\r\n==>" + DateTime.Now.ToString("yyy-MM-dd hh:mm:ss fff") + "派梯失败（无梯号返回）\r\n";

                                _logger.LogInformation(Msg);
                            }
                            else
                            {

                                Msg = "\r\n==>" + DateTime.Now.ToString() + $"派梯成功==>{datas[3].ToString()}==>梯号:" + System.Text.Encoding.Default.GetString(elename) + "\r\n";

                                _logger.LogInformation(Msg);
                                //应答通信箱
                                sp.Write(AckByte, 0, AckByte.Length);
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError($"\r\n==>Error:{ex.Message.ToString()}");
                        }

                    }
                    else if (datas[2] == 0x03)
                    {
                        Msg = "\r\n==>" + DateTime.Now.ToString("yyy-MM-dd hh:mm:ss fff") + "派梯失败（无效操作）\r\n";
                        _logger.LogError(Msg);
                    }
                    else
                    {
                        Msg = "\r\n==>" + DateTime.Now.ToString("yyy-MM-dd hh:mm:ss fff") + "派梯失败（未知错误）\r\n";
                        _logger.LogError(Msg);
                    }
                }
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
                var receive = _dataAnalyze.Analyze(sp.PortName, datas);
                if (receive == null)
                {
                    _logger.LogWarning("Hitachi 接收读卡器设备数据为空：PortName:{0} ", sp.PortName);
                    return;
                }

                //数据处理 
                _eventAggregator.GetEvent<HitachiConfirmSendEvent>().Publish(receive);
                _eventAggregator.GetEvent<HandleElevatorReceiveEvent>().Publish(receive);
            }
            catch (Exception ex)
            {
                _logger.LogError("接收读卡器设备数据出错：ex:{0} ", ex);
            }
        }

        public void SendAsync(byte[] bytes)
        {
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
