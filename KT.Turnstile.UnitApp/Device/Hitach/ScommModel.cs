using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.Threading;
using Microsoft.Extensions.Logging;
using System.Linq;
using KT.Common.WpfApp.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using KT.Quanta.Common.Models;
using System.Threading.Tasks;
using Prism.Events;
using KT.Turnstile.Unit.ClientApp.Events;
using KT.Common.Core.Utils;

namespace HelperTools
{
    public class ScommModel
    {
        public HitichSendComm SendPara;
        private ILogger _logger;
        public List<System.IO.Ports.SerialPort> ListComms;       
        public HandleElevatorDisplayModel _DisplayModel;
        private IEventAggregator _eventAggregator;
        public bool _ElevatorWorkFlg;  //电梯工作状态
        public List<QueueElevaor> _QueueElevaor;//派梯队列
        public QueueElevaor Queue;
        public long UTCTIME;
        public List<byte> Buffer;
        public List<WaitOverQue> WaitOver;
        
        public byte[] _SearchAnswer;
        public bool IfLocal;
        public List<ListWaitKey> ListMsg;
        public string Uri = AppConfigurtaionServices.Configuration["AppSettings:ServerIp"].ToString();
        public string Port = AppConfigurtaionServices.Configuration["AppSettings:ServerPort"].ToString();

        public ScommModel()
        {
            SendPara = new HitichSendComm();
            _DisplayModel = new HandleElevatorDisplayModel();
            Queue = new QueueElevaor();
            WaitOver = new List<WaitOverQue>();            
            _logger = ContainerHelper.Resolve<ILogger>();
            _eventAggregator = ContainerHelper.Resolve<IEventAggregator>();            
        }

        /// <summary>
        /// 封装发送数据
        /// </summary>
        /// <param name="handleElevator"></param>
        /// <returns></returns>
        public byte[] AutoSendAnalyze()
        {
            try
            {
                var bytes = new List<byte>();
                //帧头
                bytes.Add(0xAC);
                //帧类型
                bytes.Add(0x5C);

                //系列卡号                    
                var cardBytes = BitConverter.GetBytes(Convert.ToUInt32(SendPara.Sign));
                bytes.AddRange(cardBytes.Reverse());
                //手动楼层
                bytes.AddRange(new byte[8]);
                //保留
                byte[] reserveBytes = new byte[27];
                for (int x = 0; x < 27; x++)
                {
                    reserveBytes[x] = 0xFF;
                }
                bytes.AddRange(reserveBytes);

                //自动楼层
                bytes.Add(Convert.ToByte(SendPara.Floor));

                //xor
                bytes.Add(0x00);

                //帧尾
                bytes.Add(0xCA);
                byte[] Abyte = bytes.ToArray();
                for (int i = 0; i < 42; i++)
                {
                    Abyte[42] ^= Abyte[i];
                }
                return Abyte;
            }
            catch (Exception ex)
            {
                _logger.LogError($"\r\n==>Error:{ex.Message.ToString()}");
            }
            return null;
        }

        /// <summary>
        /// 向串口发送数据,指定串号
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="_serialPort"></param>
        public void SendAsync(byte[] bytes, System.IO.Ports.SerialPort Scomm)
        {
            try
            {
                if (!Scomm.IsOpen)
                {
                    SerialConnect(Scomm);
                    if (!Scomm.IsOpen)
                    {
                        _logger.LogInformation("\r\n=>>串口未连接！");
                        return;
                    }
                }
                _logger.LogInformation($"\r\n==>派梯数据包：{BitConverter.ToString(bytes)}");
                Scomm.Write(bytes, 0, bytes.Count());
            }
            catch (Exception ex)
            {
                _logger.LogError($"\r\n==>Error:{ex.Message}");
            }
        }
        /*通讯端口初始化  2021-06-28*/

        /// <summary>
        /// 初始串口 日立电梯
        /// </summary>
        public void HipatchStart()
        {
            _logger.LogInformation($"日立串口初始");
            _ElevatorWorkFlg = true;//可用
            _QueueElevaor = new List<QueueElevaor>();
            ListComms = new List<SerialPort>();
            Buffer = new List<byte>();
            //_Sleep = Convert.ToInt32(AppConfigurtaionServices.Configuration["sleep"].ToString().Trim());
            UTCTIME = 0;
            IfLocal = true;//默认本地派提
            ListMsg = new List<ListWaitKey>() ;//第三方队列ID
            _SearchAnswer = SearchAnswer();
            string[] PortArray = AppConfigurtaionServices.Configuration["ElevatorSerialPort"].ToString().Split(',');
            for (int x = 0; x < PortArray.Length; x++)
            {
                System.IO.Ports.SerialPort Sc = new System.IO.Ports.SerialPort();
                Sc.PortName = PortArray[x];
                Sc.BaudRate = 9600;
                Sc.DataBits = 8;
                Sc.StopBits = (StopBits)1;// System.IO.Ports.StopBits.One;
                Sc.Parity = (Parity)0;// System.IO.Ports.Parity.None;
                Sc.ReadTimeout = 2000;
                //Sc.Encoding = Encoding.GetEncoding("UTF-8");
                //Sc.WriteBufferSize = 1024;
                //Sc.ReadBufferSize = 1024;
                //Sc.ReceivedBytesThreshold = 1;    

                ListComms.Add(Sc);
            }
            OpenSerial();

        }
        public void OpenSerial()
        {
            try
            {

                for (int x = 0; x < ListComms.Count; x++)
                {
                    ListComms[x].DataReceived += _Scomm_DataReceived;// new SerialDataReceivedEventHandler(_serialPort_DataReceived);
                    ListComms[x].RtsEnable = true;
                    ListComms[x].DtrEnable = true;
                    SerialConnect(ListComms[x]);
                    Thread.Sleep(50);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"\r\n==Error Comm OpenSerial:>{ex.Message.ToString()}");
            }
        }
        public void SerialConnect(System.IO.Ports.SerialPort Scomm)
        {
            try
            {
                //打开串口                                                                                         
                //如果打开状态，则先关闭一下
                if (Scomm.IsOpen)
                {
                    Scomm.Close();
                }
                Scomm.Open();
                Thread.Sleep(50);
                _logger.LogInformation($"\r\nCom Open True==>{Scomm.PortName}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"\r\n==Error comm SerialConnect==>{ex.Message.ToString()}");
            }
        }
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender">串口</param>
        /// <param name="e">事件</param>

        public void _Scomm_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string Msg;
            // 数据的长度
            lock (this)
            {
                try
                {
                    System.IO.Ports.SerialPort sp = (System.IO.Ports.SerialPort)sender;
                    int Rx_Len = sp.BytesToRead;
                    if (Rx_Len == 0)
                    {
                        return;
                    }
                    byte[] datas = new byte[Rx_Len];
                    sp.Read(datas, 0, Rx_Len);


                    for (int xx = 0; xx < Rx_Len; xx++)
                    {
                        Buffer.Add(datas[xx]);
                    }
    
                    int bufferlen = Buffer.Count;
                    if (bufferlen < 9)
                    {
                        return;
                    }
                    byte[] _BufByte = new byte[bufferlen];
                    //9位以后开始判断,把缓冲写给交换LIST，适时清缓冲
                    for (int y = 0; y < bufferlen; y++)
                    {
                        _BufByte[y] = Buffer[y];
                    }
                    Buffer.Clear();
                    /**/


                    int _BufByteLen = _BufByte.Length;
     
                    if (_BufByteLen == 9
                        && (_BufByte[0] == 0xAC) && (_BufByte[1] == 0x5A) && (_BufByte[8] == 0xCA))
                    {
                        //判断选层器状态
                        if (_BufByte[7] == (_BufByte[0] ^ _BufByte[1] ^ _BufByte[2] ^ _BufByte[3]
                            ^ _BufByte[4] ^ _BufByte[5] ^ _BufByte[6]))
                        {
                            if ((_BufByte[6] & 0x01) == 0x00) //选层器工作正常
                            {
                                int N = _QueueElevaor.Count;
                                if (N > 0 && _ElevatorWorkFlg)//从队列中选择首项任务派梯
                                {
                                    UTCTIME = DateTimeUtil.UtcNowMillis() / 1000;
                                    QueueElevaor S = _QueueElevaor[0];
                                    _QueueElevaor.RemoveAt(0);
                                    _ElevatorWorkFlg = false;                      
                                    SendAsync(S.Instructions, ListComms[S.ScommFlg]);
                                }
                                else//不派梯应答
                                {
                                    _ElevatorWorkFlg = true;
                                    sp.Write(_SearchAnswer, 0, _SearchAnswer.Length);
                                }
                            }
                            else
                            {
                                _logger.LogError("\r\n==>通信箱选层器状态【异常】");
                            }
                        }
                        else
                        {
                            _ElevatorWorkFlg = true;
                            sp.Write(_SearchAnswer, 0, _SearchAnswer.Length);
                        }
                    }
                    else if ((_BufByteLen >= 14) && (_BufByte[0] == 0xAC) &&
                        (_BufByte[1] == 0x5B) && (_BufByte[13] == 0xCA))
                    {
                        if (_BufByte[12] == (_BufByte[0] ^ _BufByte[1] ^ _BufByte[2] ^ _BufByte[3] ^ _BufByte[4]
                            ^ _BufByte[5] ^ _BufByte[6] ^ _BufByte[7] ^ _BufByte[8] ^ _BufByte[9] ^ _BufByte[10]
                            ^ _BufByte[11]))
                        {
                            //组装结果应答包                    
                            byte[] AckByte = SendAnswer(_BufByte[2]);
                            sp.Write(AckByte, 0, AckByte.Length);

                            if (_BufByte[2] == 0x01)
                            {
                                //这个时候写输出屏
                                _ElevatorWorkFlg = false;
                                _logger.LogInformation("\r\n ==> 派梯中");
                            }
                            else if (_BufByte[2] == 0x02)//完成派梯
                            {
                                _ElevatorWorkFlg = true;
                                byte[] elename = new byte[4];//梯号
                                for (int i = 0; i < 4; i++)
                                {
                                    elename[i] = _BufByte[4 + i];
                                }
                                try
                                {
                                    if ((elename[0] == (byte)0x00) && (elename[1] == (byte)0x00)
                                        && (elename[2] == (byte)0x00)
                                        && (elename[3] == (byte)0x00))//梯号0不成功
                                    {
                                        _DisplayModel.DestinationFloorName = "请稍后重派";
                                        _DisplayModel.ElevatorName = "繁忙";
                                        //_eventAggregator.GetEvent<HandledElevatorDisplayEvent>().Publish(_DisplayModel);
                                    }
                                    else
                                    {

                                        //应答通信箱                                    
                                        byte[] Num = new byte[1];
                                        Num[0] = elename[0];
                                        //回写
                                        _DisplayModel.ElevatorName = System.Text.Encoding.Default.GetString(Num);
                                        _eventAggregator.GetEvent<HandledElevatorDisplayEvent>().Publish(_DisplayModel);
                                        Msg = $"\r\n==>{DateTime.Now.ToString()}派梯成功==>{_BufByte[3].ToString()}==>梯号:{System.Text.Encoding.Default.GetString(Num)}  原始梯号数据{BitConverter.ToString(elename)}\r\n";
                                        _logger.LogInformation(Msg);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError($"\r\n==>Error:{ex.Message.ToString()}");
                                }
                            }
                            else if (_BufByte[2] == 0x03)
                            {
                                _ElevatorWorkFlg = true;
                                _DisplayModel.DestinationFloorName = "请稍后重派";
                                _DisplayModel.ElevatorName = "繁忙";
                                //_eventAggregator.GetEvent<HandledElevatorDisplayEvent>().Publish(_DisplayModel);
                            }
                            else
                            {
                                _ElevatorWorkFlg = true;
                                _DisplayModel.DestinationFloorName = "请稍后重派";
                                _DisplayModel.ElevatorName = "繁忙";
                                //_eventAggregator.GetEvent<HandledElevatorDisplayEvent>().Publish(_DisplayModel);
                            }
                        }
                    }
                    else//未知状态是否写日志
                    {
                        _ElevatorWorkFlg = true;
                        sp.Write(_SearchAnswer, 0, _SearchAnswer.Length);
                    }
                    /**/
                }
                catch (Exception ex)
                {
                    _ElevatorWorkFlg = true;
                    Msg = $"\r\n==>串口错误：{ex}";
                    _logger.LogError(Msg);
                }
                ClearBuf();
            }
        }
        
        private string DisApiGetData(string CollerName, Dictionary<string, string> Obj)
        {
            string Url = $"http://{Uri}:{Port}/";
            string Result = string.Empty;
            try
            {
                HostReqModel HostReq = new HostReqModel();
                Result = HostReq.SendHttpRequest($"{Url}{CollerName}", JsonConvert.SerializeObject(Obj));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"\r\nAPI 错误 Result==>{JsonConvert.SerializeObject(Result)}");
                _logger.LogInformation($"\r\nAPI 错误==>{ex}");
                dynamic JsonObj = new DynamicObj();
                JsonObj.code = "000";
                JsonObj.Flg = "0";
                JsonObj.Message = ex.Message.ToString();
                JsonObj.Data = new JArray();
                Result = JsonConvert.SerializeObject(JsonObj._values);
            }
            return Result;
        }
        private void ClearBuf()
        {
            long Ttime = DateTimeUtil.UtcNowMillis() / 1000;
            int Len = _QueueElevaor.Count - 1;
            //清除队列中大于3秒的派梯请求
            for (int x = Len; x >= 0; x--)
            {
                if (Ttime - (_QueueElevaor[x].UtcTime / 1000) > 3)
                {
                    _QueueElevaor.RemoveAt(x);
                }
            }
            //离上一次派梯大于一秒，还原为可派梯状态
            if (UTCTIME > 0 && (Ttime - UTCTIME > 1))
            {
                _ElevatorWorkFlg = true;
            }
        }
        /// <summary>
        /// 查询应答包
        /// </summary>
        /// <returns></returns>
        private byte[] SearchAnswer()
        {
            byte[] AckByte = new byte[44];
            //组包,这里由于是模拟的,仅仅模拟自动派梯的情况
            AckByte[0] = 0xAC;
            AckByte[1] = 0x5C;

            AckByte[2] = 0x00;
            AckByte[3] = 0x00;
            AckByte[4] = 0x00;
            AckByte[5] = 0x00;

            AckByte[6] = 0x00;
            AckByte[7] = 0x00;
            AckByte[8] = 0x00;
            AckByte[9] = 0x00;
            AckByte[10] = 0x00;
            AckByte[11] = 0x00;
            AckByte[12] = 0x00;
            AckByte[13] = 0x00;

            for (int i = 0; i < 27; i++)
            {
                AckByte[14 + i] = 0xFF;
            }

            AckByte[41] = 0;//楼层不派梯 为0

            AckByte[42] = 0;
            for (int i = 0; i < 42; i++)
            {
                AckByte[42] ^= AckByte[i];
            }
            AckByte[43] = 0xCA;
            return AckByte;
        }
        /// <summary>
        /// 结果应答包
        /// </summary>
        /// <returns></returns>
        private byte[] SendAnswer(byte buf)
        {
            //组装结果应答包
            byte[] AckByte = new byte[5];
            AckByte[0] = 0xAC;
            AckByte[1] = 0x5B;
            AckByte[2] = buf;
            AckByte[3] = (byte)(AckByte[0] ^ AckByte[1] ^ AckByte[2]);
            AckByte[4] = 0xCA;
            return AckByte;
        }
    }
}
