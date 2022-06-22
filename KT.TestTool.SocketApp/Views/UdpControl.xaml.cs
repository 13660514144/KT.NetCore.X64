using KT.Common.WpfApp.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace KT.TestTool.SocketApp.Views
{
    /// <summary>
    /// UdpControl.xaml 的交互逻辑
    /// </summary>
    [NavigationItem]
    public partial class UdpControl : UserControl
    {
        private UdpControlViewModel _viewModel;
        private SocketSettings _socketSettings;
        private ILogger<UdpControl> _logger;

        /// <summary>
        /// 用于UDP接收的网络服务类
        /// </summary>
        private UdpClient _udpClient;
        private IPEndPoint _localPoint;
        private IPEndPoint _sendPoint;
        /// <summary>
        /// 线程：不断监听UDP报文
        /// </summary>
        private Task _udpTask;

        public UdpControl(ILogger<UdpControl> logger, UdpControlViewModel viewModel, SocketSettings socketSettings)
        {
            InitializeComponent();

            _viewModel = viewModel;
            _socketSettings = socketSettings;
            _logger = logger;

            this.DataContext = _viewModel;
        }

        private void Btn_StartReceiveUdpData_Click(object sender, RoutedEventArgs e)
        {
            // 未监听的情况，开始监听
            _viewModel.SocketInfo.IsStarted = true;

            IPAddress sendIp = IPAddress.Parse(_viewModel.SocketInfo.SendIp);
            _sendPoint = new IPEndPoint(sendIp, _viewModel.SocketInfo.SendPort);

            IPAddress ip = IPAddress.Parse(_viewModel.SocketInfo.LocalIp);
            // 本机IP和监听端口号
            _localPoint = new IPEndPoint(ip, _viewModel.SocketInfo.LocalPort);
            _udpClient = new UdpClient(_localPoint);
            _udpTask = ReceiveMessage();
            _viewModel.ScrollMessage.InsertTop("UDP监听器已成功启动");
        }


        /// <summary>
        /// 发送数据
        /// </summary> 
        public void SendMessage(string message)
        {
            var bytes = Encoding.ASCII.GetBytes(message);
            Task.Run(() =>
            {
                _viewModel.ScrollMessage.InsertTop($"UDP发送器发送数据：address:{_sendPoint.Address} ");
                try
                {
                    _udpClient.Send(bytes, bytes.Length, _sendPoint);
                }
                catch (Exception ex)
                {
                    _viewModel.ScrollMessage.InsertTop($"UDP发送器发送数据失败：address:{_sendPoint.Address} ");
                }
            });
        }

        /// <summary>
        /// 接收数据
        /// </summary> 
        private Task ReceiveMessage()
        {
            return Task.Run(() =>
            {
                IPEndPoint remoteIpep = new IPEndPoint(IPAddress.Any, 0);
                while (_viewModel.SocketInfo.IsStarted)
                {
                    try
                    {
                        //无数据读取不操作，否则中断时会报错
                        if (_udpClient.Available <= 0)
                        {
                            continue;
                        }
                        byte[] bytRecv = _udpClient.Receive(ref remoteIpep);
                        string message = Encoding.ASCII.GetString(bytRecv, 0, bytRecv.Length);
                        _viewModel.ScrollMessage.InsertTop("接收数据：point:{0} message:{1} ", remoteIpep.ToString(), message);
                    }
                    catch (Exception ex)
                    {
                        _viewModel.ScrollMessage.InsertTop(ex.Message);
                        break;
                    }
                    Thread.Sleep(100);
                }
            });
        }

        private void Btn_SendUdpData_Click(object sender, RoutedEventArgs e)
        {
            SendMessage(_viewModel.SocketInfo.SendText);
        }

        private void Btn_CloseReceiveTcpData_Click(object sender, RoutedEventArgs e)
        {
            // 正在监听的情况，终止监听
            _viewModel.SocketInfo.IsStarted = false;
            _udpTask.Wait();
            _udpClient.Close();
            _viewModel.ScrollMessage.InsertTop("UDP监听器已成功关闭");
        }
    }
}
